using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Extensions;
using ShowsCalendar.Handlers;
using ShowsCalendar.Panels;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using SlickControls.Panels;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar.Classes
{
	public enum AirStateEnum { Unknown, Aired, ToBeAired }

	public class Episode
	{
		#region Public Events

		public event Action<double> EpisodeWatchTimeChanged;

		#endregion Public Events

		#region Private Fields

		private double progress;
		private bool watched;

		#endregion Private Fields

		#region Public Properties

		public static Episode None { get; } = new Episode(null) { Empty = true };
		internal string Name => TMDbData.Name;
		public double Progress { get => progress; set { progress = value; EpisodeWatchTimeChanged?.Invoke(value); } }
		internal Show Show => Season?.Show;
		public TvSeasonEpisode TMDbData { get; set; }
		public DateTime WatchDate { get; set; } = DateTime.MinValue;

		public bool Watched
		{
			get => watched || WatchTime == 0;
			set => watched = value;
		}

		public long WatchTime { get; set; } = -1;

		#endregion Public Properties

		#region Internal Properties

		internal AirStateEnum AirState => TMDbData?.AirDate == null ? AirStateEnum.Unknown : (TMDbData.AirDate < DateTime.Today ? AirStateEnum.Aired : AirStateEnum.ToBeAired);
		internal bool Empty { get; set; }
		internal int EN => TMDbData?.EpisodeNumber ?? 0;

		internal StillImages Images { get; set; }

		internal Episode Next
		{
			get
			{
				if (Season.Episodes.LastOrDefault() != this)
					return Season.Episodes[Season.Episodes.IndexOf(this) + 1];
				if (Show.Seasons.LastOrDefault() != Season)
					return Show.Seasons[Show.Seasons.IndexOf(Season) + 1].Episodes.FirstOrDefault();
				return null;
			}
		}

		internal Episode Previous
		{
			get
			{
				if (Season.Episodes.FirstOrDefault() != this)
					return Season.Episodes[Season.Episodes.IndexOf(this) - 1];
				if (Show.Seasons.FirstOrDefault() != Season)
					return Show.Seasons[Show.Seasons.IndexOf(Season) - 1].Episodes.LastOrDefault();
				return null;
			}
		}

		internal Season Season { get; set; }
		internal int SN => Season?.SeasonNumber ?? 0;
		internal bool Started => WatchTime > 0;
		internal List<FileInfo> VidFiles { get; set; } = new List<FileInfo>();
		internal string ZooqleURL => $"{Show.ZooqleURL}{(Show.ZooqleURL.Last() == '/' ? "" : "/")}{SN}x{EN}.html";

		#endregion Internal Properties

		#region Public Constructors

		public Episode()
		{
		}

		public Episode(TvSeasonEpisode ep, Season season = null, Show show = null)
		{
			TMDbData = ep;
			Season = season;
			Empty = false;
		}

		#endregion Public Constructors

		#region Public Methods

		public static bool operator !=(Episode episode1, Episode episode2) => !(episode1 == episode2);

		public static bool operator ==(Episode episode1, Episode episode2) => EqualityComparer<Episode>.Default.Equals(episode1, episode2);

		public override bool Equals(object obj)
			=> obj is Episode episode && TMDbData.Id == episode.TMDbData.Id;

		public override int GetHashCode() => 1912459738 + TMDbData.Id.GetHashCode();

		public void MarkAs(bool v)
		{
			if (Watched = v)
				WatchDate = DateTime.Now;
			WatchTime = -1;
			Progress = 0;
		}

		public void Play(FileInfo epFile = null)
		{
			if (Data.Options.FinaleWarning && Season.Episodes.Last() == this && !Watched
				&& MessagePrompt.Show($"You are about to watch the finale for {Season} of {Show}" +
					 $"\n\nClick cancel if you are not mentally ready.",
					 "Finale Warning",
					 PromptButtons.OKCancel,
					 PromptIcons.Info,
					 Data.Mainform
					) == DialogResult.Cancel)
				return;

			if (epFile == null && VidFiles.Any())
			{
				if (VidFiles.Count == 1)
					epFile = VidFiles.FirstOrDefault();
				else
				{
					if ((Data.Mainform.CurrentPanel is PC_EpisodeView psc) && psc.Episode == this)
					{
						psc.ST_Files.Selected = true;
					}
					else
					{
						var pce = new PC_EpisodeView(this);
						pce.ST_Files.Selected = true;
						Data.Mainform.PushPanel(Data.Mainform.PI_Watch, pce);
					}
					return;
				}
			}

			if (epFile == null || !File.Exists(epFile.FullName))
			{
				VidFiles = LocalShowHandler.GetFile(this).ToList();
				Play();
				return;
			}

			if (!epFile?.Exists ?? false)
			{
				MessagePrompt.Show($"Could not find the file associated with {Show} {this}\n\nCheck if the file exists, or if it was renamed into something Shows Calendar can't detect.", icon: PromptIcons.Hand, form: Data.Mainform);
				return;
			}

			PC_Player pc = null;
			var pushed = false;

			try
			{
				if (Data.Mainform.CurrentPanel is PC_Player player)
					player.SetEpisode(this, epFile);
				else
				{
					pc = new PC_Player(this, epFile);
					pushed = true;
					if (pc.Episode != null)
						Data.Mainform.PushPanel(PanelItem.Empty, pc);
					else
						pc.Dispose();
				}
			}
			catch
			{
				pc?.Dispose();
				if (pushed)
					Data.Mainform.PushBack();

				MessagePrompt.Show("Something went wrong while loading your episode.\n\nMake sure it fully downloaded, or try another file.", icon: PromptIcons.Error, form: Data.Mainform);

				return;
			}

			Data.Mainform.ShowUp();
		}

		public void ShowStrip()
		{
			FlatToolStrip.Show(Data.Mainform,
				new FlatStripItem("Play", () => Play()
				, image: ProjectImages.Tiny_Play
				, show: VidFiles != null && VidFiles.Any()
				, fade: !VidFiles.Any(x => x.Exists)),

				new FlatStripItem("Download", () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					Data.Mainform.PushPanel(null, new PC_Download(this));
					Cursor.Current = Cursors.Default;
				}, image: ProjectImages.Tiny_Download),

				new FlatStripItem("Link to Zooqle", () =>
				{
					Show.ZooqleLink();
				}, image: ProjectImages.Tiny_Link, show: string.IsNullOrWhiteSpace(Show.ZooqleURL)),

				new FlatStripItem("View on Zooqle", () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					Process.Start(ZooqleURL);
					Cursor.Current = Cursors.Default;
				}, image: ProjectImages.Tiny_Z, show: !string.IsNullOrWhiteSpace(Show.ZooqleURL)),

				new FlatStripItem("View File".Plural(VidFiles), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					if ((Data.Mainform.CurrentPanel is PC_EpisodeView psc) && psc.Episode == this)
					{
						psc.ST_Files.Selected = true;
					}
					else
					{
						var pce = new PC_EpisodeView(this);
						pce.ST_Files.Selected = true;
						Data.Mainform.PushPanel(Data.Mainform.PI_Watch, pce);
					}
					Cursor.Current = Cursors.Default;
				}, image: ProjectImages.Tiny_Folder, show: VidFiles.Any()),

				FlatStripItem.Empty,

				new FlatStripItem("More Info", () =>
				{
					if (Data.Mainform.CurrentPanel is PC_SeasonView seasonView && seasonView.Season == Season)
						Data.Mainform.PushPanel(null, new PC_EpisodeView(this));
					else if (!(Data.Mainform.CurrentPanel is PC_EpisodeView epView && epView.Episode == this))
						Data.Mainform.PushPanel(null, new PC_ShowPage(this));
				}, image: ProjectImages.Tiny_Info),

				new FlatStripItem("Season Info", () =>
				{
					Data.Mainform.PushPanel(null, new PC_ShowPage(Season));
				}, image: ProjectImages.Tiny_TVEmpty),

				new FlatStripItem("Show Page", () =>
				{
					Data.Mainform.PushPanel(null, new PC_ShowPage(Show));
				}, image: ProjectImages.Tiny_TV),

				FlatStripItem.Empty,

				new FlatStripItem("Mark", image: ProjectImages.Tiny_Ok, fade: true),

				new FlatStripItem("  Ep. as Complete", () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					WatchDate = DateTime.Now;
					WatchTime = 0;
					Progress = 0;
					new Action(() =>
					{
						LocalShowHandler.Refresh(Show);
						ShowManager.Save(this);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}, show: Progress > 0 || WatchTime > 0),

				new FlatStripItem("  Ep. as " + ((Watched || Progress > 0 || WatchTime > 0) ? "Unwatched" : "Watched"), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					MarkAs(!(Watched || Progress > 0 || WatchTime > 0));
					new Action(() =>
					{
						LocalShowHandler.Refresh(Show);
						ShowManager.Save(this);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}),

				new FlatStripItem("  Season as " + (Season.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched) ? "Unwatched" : "Watched"), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					var set = !Season.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched);
					Season.Episodes.ForEach(e =>
					{
						if (e.AirState == AirStateEnum.Aired)
							e.MarkAs(set);
					});
					new Action(() =>
					{
						LocalShowHandler.Refresh(Show);
						ShowManager.Save(Show);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}),

				new FlatStripItem("  Show as " + (Show.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)) ? "Unwatched" : "Watched"), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					var set = !Show.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched));
					Show.Seasons.ForEach(x => x.Episodes.ForEach(e =>
					{
						if (e.AirState == AirStateEnum.Aired)
							e.MarkAs(set);
					}));
					new Action(() =>
					{
						LocalShowHandler.Refresh(Show);
						ShowManager.Save(Show);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				})
			);
		}

		public override string ToString() => TMDbData == null ? $"{SN}x{EN}" : $"{SN}x{EN} • {TMDbData.Name}";

		#endregion Public Methods

		#region Internal Methods

		internal void Update(Episode episode)
		{
			if (episode != null)
			{
				TMDbData = episode.TMDbData;
			}
		}

		#endregion Internal Methods
	}
}