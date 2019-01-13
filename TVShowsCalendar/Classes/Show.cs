using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Panels;
using ProjectImages = TVShowsCalendar.Properties.Resources;

namespace TVShowsCalendar.Classes
{
	public class Show : ISave
	{
		#region Public Events

		public event ShowManager.ShowChangeHandler ShowLoaded;

		public event EventHandler TMDbDataChanged;

		#endregion Public Events

		#region Private Fields

		private OneWayTask ShowLoadingTask = new OneWayTask();
		private int _id;

		#endregion Private Fields

		#region Public Properties

		internal IEnumerable<Episode> Episodes => Seasons.ConvertEnumerable(x => x.Episodes);

		public int Id { get => _id.If(0, TMDbData?.Id ?? 0); set => _id = value; }
		public DateTime LastCheck { get; set; } = DateTime.MinValue;

		public new string Name => (TMDbData?.Name ?? "Show").RemoveDoubleSpaces();

		public List<Season> Seasons { get; set; } = new List<Season>();

		public LightShow[] SimilarShows { get; set; }

		public TvShow tMDbData { get; set; }

		public string ZooqleURL { get; set; }

		#endregion Public Properties

		#region Internal Properties

		internal List<Cast> Cast { get; set; }

		internal List<Crew> Crew { get; set; }

		internal bool FirstLoad { get; set; } = true;

		internal Episode LastEpisode { get; set; }

		internal bool Loaded { get; set; }

		internal Episode NextEpisode { get; set; }

		internal bool Temporary { get; set; }

		internal TvShow TMDbData
		{
			get => tMDbData;
			set
			{
				Loaded = false;

				tMDbData = value;

				if (Id == 0)
					Id = value.Id;

				TMDbDataChanged?.Invoke(this, new EventArgs());

				Cast = TMDbData.Credits?.Cast?.OrderBy(x => x.Order).ToList() ?? new List<Cast>();
				Crew = TMDbData.Credits?.Crew?.OrderBy(x => string.IsNullOrWhiteSpace(x.ProfilePath) ? 0 : 1).ThenBy(x => x.Name).ToList() ?? new List<Crew>();

				SimilarShows = TMDbData.Similar?.Results?.Convert(LightShow.Convert).ToArray() ?? new LightShow[0];

				if (value != null)
					ShowLoadingTask.Run(LoadSeasons);
			}
		}

		internal string Years
		{
			get
			{
				if (Episodes == null)
					return "";

				var first = Episodes.Min(x => x.TMDbData.AirDate ?? DateTime.MaxValue).Year;

				if (!TMDbData.Status.AnyOf("Ended", "Canceled"))
					return $"{first} - Present";

				var last = Episodes.Max(x => x.TMDbData.AirDate ?? DateTime.MinValue).Year;
				return $"{first} - {last}";
			}
		}

		#endregion Internal Properties

		#region Public Constructors

		public Show()
		{ }

		public Show(TvShow tvShow)
			=> TMDbData = tvShow;

		public Show(LightShow searchTv)
			=> new Action(async () =>
			{
				Loaded = false;
				tMDbData = new TvShow() { Id = searchTv.Id };
				await CheckForChanges(false, true);
			}).RunInBackground(System.Threading.ThreadPriority.Lowest);

		#endregion Public Constructors

		#region Public Methods

		public void Delete()
		{
			Delete($"ShowsData/{Name.RegexRemove("[^\\w\\ ]").RegexReplace(" {1,}", "-")}-{Id.If(0, tMDbData.Id)}.scs");
		}

		public override bool Equals(object obj)
		{
			return obj is Show show && show.TMDbData?.Id == TMDbData?.Id;
		}

		public string GetFlavorText()
		{
			if (tMDbData == null)
				return "Loading Show Data..";

			if (tMDbData.Status == "Ended")
			{
				if (Seasons?.Any() ?? false)
					return $"Show ended on {(Seasons.Last().Episodes.Last().TMDbData.AirDate?.ToReadableString(format: ExtensionClass.DateFormat.TDMY))} with {Seasons.Count(x => x.SeasonNumber > 0)} seasons.";

				return "Show ended";
			}

			if (LastEpisode != null && LastEpisode != Episode.None)
			{
				if (NextEpisode == null || NextEpisode == Episode.None)
					return $"{tMDbData.Status}, last episode aired {new TimeSpan(DateTime.Now.Ticks - (long)LastEpisode.TMDbData.AirDate?.Ticks).ToReadableBigString()} ago on {LastEpisode.TMDbData.AirDate?.ToReadableString(format: ExtensionClass.DateFormat.TDMY)}.";
				if (LastEpisode.TMDbData.AirDate > DateTime.Today.AddDays(-4))
					return $"Episode {LastEpisode} aired {new TimeSpan(DateTime.Now.Ticks - (long)LastEpisode.TMDbData.AirDate?.Ticks).ToReadableBigString()} ago.";
			}

			if (NextEpisode != null && NextEpisode != Episode.None)
			{
				if (NextEpisode.AirState == AirStateEnum.ToBeAired)
					return $"Next Episode {NextEpisode} airs in {new TimeSpan((long)NextEpisode.TMDbData.AirDate?.Ticks - DateTime.Now.Ticks).ToReadableBigString()} on {NextEpisode.TMDbData.AirDate?.ToReadableString(format: ExtensionClass.DateFormat.TDMY)}.";

				return $"Next Episode {NextEpisode} still waiting for a release date.";
			}

			return tMDbData.Status;
		}

		public override int GetHashCode() => TMDbData?.Id ?? 0;

		public override void OnLoad()
		{
			foreach (var se in Seasons)
			{
				se.Show = this;
				foreach (var ep in se.Episodes)
					ep.Season = se;
			}

			if (TMDbData?.Credits != null)
			{
				if (TMDbData.Credits.Cast != null)
					Cast = TMDbData.Credits.Cast.OrderBy(x => x.Order).ToList();

				if (TMDbData.Credits.Crew != null)
					Crew = TMDbData.Credits.Crew;
			}

			if (tMDbData != null && LastCheck.AddDays(Data.Options.ShowsRefreshDays) >= DateTime.Now)
			{
				LastEpisode = Seasons.ConvertEnumerable(x => x.Episodes).OrderBy(x => x.SN * 100 + x.EN).LastThat(x => x.AirState == AirStateEnum.Aired) ?? Episode.None;
				NextEpisode = Seasons.ConvertEnumerable(x => x.Episodes).OrderBy(x => x.SN * 100 + x.EN).FirstThat(x => x.AirState == AirStateEnum.ToBeAired) ?? Episode.None;

				if (Id == 0)
					Id = tMDbData.Id;

				Loaded = true;
			}
			else
				new Action(async () => await CheckForChanges(false)).RunInBackground(5000);
		}

		public void Save()
		{
			if (Id.If(0, tMDbData.Id) != 0)
			{
				LastCheck = DateTime.Now;
				Save($"ShowsData/{Name.RegexRemove("[^\\w\\ ]").RegexReplace(" {1,}", "-")}-{Id.If(0, tMDbData.Id)}.scs", true);
				foreach (var item in Directory.GetFiles(Path.Combine(DocsFolder, "ShowsData"), $"*-{Id}.scs"))
					if (item != Path.Combine(DocsFolder, "ShowsData", $"{Name.RegexRemove("[^\\w\\ ]").RegexReplace(" {1,}", "-")}-{Id.If(0, tMDbData.Id)}.scs"))
						File.Delete(item);
			}
		}

		public void ShowStrip(bool showPage = true)
		{
			FlatToolStrip.Show(Data.Dashboard,
				new FlatStripItem("Play", () => this.GetCurrentWatchEpisode().Play(), image: ProjectImages.Tiny_Play, show: this.GetCurrentWatchEpisode()?.VidFile?.Exists ?? false),

				new FlatStripItem("Link", () =>
				{
					ZooqleLink();
				}, image: ProjectImages.Tiny_Link, show: string.IsNullOrWhiteSpace(ZooqleURL)),

				FlatStripItem.Empty,

				new FlatStripItem("Show Page", () => Data.Dashboard.PushPanel(null, new PC_ShowPage(this)), image: ProjectImages.Tiny_TV, show: showPage),
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
				new FlatStripItem("Refresh", () => CheckForChanges(false, true, true), image: ProjectImages.Tiny_Refresh),
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

				FlatStripItem.Empty,

				new FlatStripItem("Mark as " + (Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)) ? "Unwatched" : "Watched"), show: !Temporary, image: ProjectImages.Tiny_Ok, action: () =>
				{
					var set = !Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched));
					Seasons.ForEach(x => x.Episodes.ForEach(e =>
					{
						e.Watched = e.AirState == AirStateEnum.Aired ? set : e.Watched;
						if(e.Watched)
							e.WatchDate = DateTime.Now;
					}));
					new Action(()=>
					{
						LocalShowHandler.Refresh(this);
						ShowManager.Save(this);
					}).RunInBackground();
				}),

				FlatStripItem.Empty,

				new FlatStripItem("Delete", () => ShowManager.Remove(this), image: ProjectImages.Tiny_Trash)
			 );
		}

		public bool ZooqleLink()
		{
			var result = MessagePrompt.ShowInput(
					$"Enter a Zooqle TV Show link for {Name}",
					"Zooqle Link",
					ZooqleURL.IfEmpty("https://zooqle.com/tv/"),
					PromptButtons.OKCancel,
					PromptIcons.Input,
					s => Regex.IsMatch(s, @"(https?://)?zooqle.com/tv/[^/]+", RegexOptions.IgnoreCase),
					Data.Dashboard);

			if (result.DialogResult == DialogResult.OK)
			{
				ZooqleURL = Regex.Match(result.Input, @"(https?://)?zooqle.com/tv/[^/]+", RegexOptions.IgnoreCase).Value;
				ShowManager.Save(this);

				return true;
			}

			return false;
		}

		public override string ToString() => Name;

		#endregion Public Methods

		#region Internal Methods

		internal async Task<bool> CheckForChanges(bool wait = true, bool force = false, bool prompt = false)
		{
			if (wait)
			{
				LastEpisode = Seasons.ConvertEnumerable(x => x.Episodes).OrderBy(x => x.SN * 100 + x.EN).LastThat(x => x.AirState == AirStateEnum.Aired) ?? Episode.None;
				NextEpisode = Seasons.ConvertEnumerable(x => x.Episodes).OrderBy(x => x.SN * 100 + x.EN).FirstThat(x => x.AirState == AirStateEnum.ToBeAired) ?? Episode.None;
			}

			if (!wait && ConnectionHandler.State == ConnectionState.Connected)
				return await Reload();

			if (force || LastCheck.AddDays(Data.Options.ShowsRefreshDays) < DateTime.Now)
			{
				if (ConnectionHandler.State == ConnectionState.Connected)
					return await Reload();
				else if (wait || (prompt && DialogResult.Yes == MessagePrompt.Show("You are not connected to the Internet right now.\nDo you wish to Reload when the connection comes back up?", buttons: PromptButtons.YesNo, icon: PromptIcons.Hand, form: Data.Dashboard)))
					ConnectionHandler.Connected += async (c) => await Reload();
			}
			else
			{
				LastEpisode = Seasons.ConvertEnumerable(x => x.Episodes).OrderBy(x => x.SN * 100 + x.EN).LastThat(x => x.AirState == AirStateEnum.Aired) ?? Episode.None;
				NextEpisode = Seasons.ConvertEnumerable(x => x.Episodes).OrderBy(x => x.SN * 100 + x.EN).FirstThat(x => x.AirState == AirStateEnum.ToBeAired) ?? Episode.None;

				Loaded = true;
			}

			return false;
		}

		internal async Task<bool> Reload()
		{
			try
			{
				var data = await Data.TMDbHandler.GetTvShow(TMDbData.Id);

				TMDbData = await Data.TMDbHandler.GetTvShow(TMDbData.Id);
				ShowManager.Save(this);

				return true;
			}
			catch { return false; }
		}

		#endregion Internal Methods

		#region Private Methods

		private void FinishLoad()
		{
			if (Seasons.Count == tMDbData.Seasons.Count)
			{
				ShowManager.Add(this);
				Loaded = true;
				ShowLoaded?.Invoke(this);
				FirstLoad = false;
			}
		}

		private void LoadSeason(int sNum, IEnumerable<Episode> eps)
		{
			new Action(async () =>
			{
				var season = new Season(await Data.TMDbHandler.GetTvSeason(tMDbData.Id, sNum), eps, this);
				Seasons.Add(season);

				if (Seasons.Count == TMDbData.Seasons.Count)
				{
					Seasons = Seasons.OrderBy(x => x.SeasonNumber).ToList();

					LastEpisode = Seasons.ConvertEnumerable(x => x.Episodes).OrderBy(x => x.SN * 100 + x.EN).LastThat(x => x.AirState == AirStateEnum.Aired) ?? Episode.None;
					NextEpisode = Seasons.ConvertEnumerable(x => x.Episodes).OrderBy(x => x.SN * 100 + x.EN).FirstThat(x => x.AirState == AirStateEnum.ToBeAired) ?? Episode.None;

					FinishLoad();
				}
			}).RunInBackground(System.Threading.ThreadPriority.Lowest);
		}

		private void LoadSeasons()
		{
			if (tMDbData != null)
			{
				var eps = Episodes.ToArray();
				Seasons = new List<Season>();

				foreach (var season in tMDbData.Seasons)
					LoadSeason(season.SeasonNumber, eps.Where(x => x.SN == season.SeasonNumber));
			}
			else
			{
				LastEpisode = null;
				NextEpisode = null;
				Seasons = null;
			}
		}

		#endregion Private Methods
	}
}