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
using ShowsCalendar.Handlers;
using ShowsCalendar.Panels;
using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar.Classes
{
	public partial class Show : ISave
	{

		public event ShowManager.ShowChangeHandler ShowLoaded;

		public event EventHandler TMDbDataChanged;

		private OneWayTask ShowLoadingTask = new OneWayTask();
		private int _id;

		internal IEnumerable<Episode> Episodes => Seasons.ConvertEnumerable(x => x.Episodes);

		public int Id { get => _id.If(0, TMDbData?.Id ?? 0); set => _id = value; }

		public new string Name => (TMDbData?.Name ?? base.Name).RemoveDoubleSpaces();

		public List<Season> Seasons { get; set; } = new List<Season>();

		public LightShow[] SimilarShows { get; set; }

		public TvShow tMDbData { get; set; }

		public string ZooqleURL { get; set; }

		internal List<Cast> Cast { get; set; }

		internal List<Crew> Crew { get; set; }

		internal Episode LastEpisode => Seasons.ConvertEnumerable(x => x.Episodes).OrderBy(x => x.SN* 100 + x.EN).LastThat(x => x.AirState == AirStateEnum.Aired) ?? Episode.None;
				
		internal bool Loaded { get; set; }

		internal Episode NextEpisode => Seasons.ConvertEnumerable(x => x.Episodes).OrderBy(x => x.SN * 100 + x.EN).FirstThat(x => x.AirState == AirStateEnum.ToBeAired) ?? Episode.None;

		internal bool Temporary { get; set; }

		internal TvShow TMDbData
		{
			get => tMDbData;
			set
			{
				tMDbData = value;

				Cast = value?.Credits?.Cast?.OrderBy(x => x.Order).ToList() ?? new List<Cast>();
				Crew = value?.Credits?.Crew?.OrderBy(x => !string.IsNullOrWhiteSpace(x.ProfilePath) ? 0 : 1).ThenBy(x => x.Name).ToList() ?? new List<Crew>();
				SimilarShows = value?.Similar?.Results?.Convert(LightShow.Convert).ToArray() ?? new LightShow[0];

				TMDbDataChanged?.Invoke(this, new EventArgs());
			}
		}

		internal string Years
		{
			get
			{
				if (!Episodes?.Any() ?? true)
					return "";

				var first = Episodes.Min(x => x.TMDbData.AirDate ?? DateTime.MaxValue).Year;

				if (!TMDbData.Status.AnyOf("Ended", "Canceled"))
					return $"{first} - Present";

				var last = Episodes.Max(x => x.TMDbData.AirDate ?? DateTime.MinValue).Year;
				return $"{first} - {last}";
			}
		}
		
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

			TMDbData = TMDbData;
			Loaded = true;
			Refresh();
		}

		public void Save()
		{
			if (Id.If(0, tMDbData.Id) != 0)
			{
				Save($"ShowsData/{Name.RegexRemove("[^\\w\\ ]").RegexReplace(" {1,}", "-")}-{Id.If(0, tMDbData.Id)}.scs", true);
				foreach (var item in Directory.GetFiles(Path.Combine(DocsFolder, "ShowsData"), $"*-{Id}.scs"))
					if (item != Path.Combine(DocsFolder, "ShowsData", $"{Name.RegexRemove("[^\\w\\ ]").RegexReplace(" {1,}", "-")}-{Id.If(0, tMDbData.Id)}.scs"))
						File.Delete(item);
			}
		}

		public void ShowStrip(bool showPage = true)
		{
			FlatToolStrip.Show(Data.Mainform,
				new FlatStripItem("Play", () => this.GetCurrentWatchEpisode().Play(), image: ProjectImages.Tiny_Play, show: this.GetCurrentWatchEpisode()?.VidFiles.Any(x => x.Exists) ?? false),

				new FlatStripItem("Link", () =>
				{
					ZooqleLink();
				}, image: ProjectImages.Tiny_Link, show: string.IsNullOrWhiteSpace(ZooqleURL)),

				FlatStripItem.Empty,

				new FlatStripItem("Show Page", () => Data.Mainform.PushPanel(null, new PC_ShowPage(this)), image: ProjectImages.Tiny_TV, show: showPage),
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
				new FlatStripItem("Refresh", Refresh, image: ProjectImages.Tiny_Refresh),
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

				new FlatStripItem("Remove from library", () => ShowManager.Remove(this), image: ProjectImages.Tiny_Trash)
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
					Data.Mainform);

			if (result.DialogResult == DialogResult.OK)
			{
				ZooqleURL = Regex.Match(result.Input, @"(https?://)?zooqle.com/tv/[^/]+", RegexOptions.IgnoreCase).Value;
				ShowManager.Save(this);

				return true;
			}

			return false;
		}

		public override string ToString() => Name;

	}
}