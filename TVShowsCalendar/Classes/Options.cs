using Extensions;

namespace ShowsCalendar.Classes
{
	public class Options : ISave
	{
		public int PrefferedQuality { get; set; } = 7;
		public bool ShowAllDownloads { get; set; } = true;
		public bool StartupMode { get; set; } = true;
		public bool LaunchWithWindows { get; set; } = true;
		public bool StreamlineSearch { get; set; } = true;
		public bool DownloadBehavior { get; set; } = true;
		public bool EpisodeBehavior { get; set; } = true;
		public bool EpisodeNotification { get; set; } = true;
		public bool NotificationSound { get; set; } = true;
		public bool ShowUnwatchedOnThumb { get; set; } = true;
		public bool FinaleWarning { get; set; } = true;
		public bool FullScreenPlayer { get; set; } = true;
		public int ForwardTime { get; set; } = 15;
		public int BackwardTime { get; set; } = 5;
		public int MoviesRefreshDays { get; set; } = 10;
		public int ShowsRefreshDays { get; set; } = 2;
		public bool AutoCleaner { get; set; } = false;
	}
}
