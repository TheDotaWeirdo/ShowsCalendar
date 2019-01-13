using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using SlickControls.Classes;
using SlickControls.Forms;
using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Panels;

namespace TVShowsCalendar.Classes
{
	public class Season
	{
		#region Public Properties

		public List<Episode> Episodes { get; set; } = new List<Episode>();
		public TvSeason TMDbData { get; set; }

		#endregion Public Properties

		#region Internal Properties

		internal string Name => TMDbData?.Name ?? $"Season {SeasonNumber}";

		internal int SeasonNumber => TMDbData.SeasonNumber;

		internal PosterImages Images { get; set; }

		internal Season Next
		{
			get
			{
				if (Show.Seasons.Last() != this)
					return Show.Seasons[Show.Seasons.IndexOf(this) + 1];

				return null;
			}
		}

		internal Season Previous
		{
			get
			{
				if (Show.Seasons.First() != this)
					return Show.Seasons[Show.Seasons.IndexOf(this) - 1];

				return null;
			}
		}

		internal Show Show { get; set; }

		#endregion Internal Properties

		#region Public Constructors

		public Season()
		{
		}

		public Season(TvSeason tvSeason, IEnumerable<Episode> watchTimes, Show show = null)
		{
			TMDbData = tvSeason;
			Show = show;

			if (tvSeason.Episodes != null)
				foreach (var ep in tvSeason.Episodes)
				{
					if (ep != null)
					{
						var watchdat = watchTimes?.FirstThat(x => x.EN == ep.EpisodeNumber);
						var episode = new Episode(ep, this, show)
						{
							WatchTime = watchdat?.WatchTime ?? -1,
							WatchDate = watchdat?.WatchDate ?? DateTime.MinValue,
							Progress = watchdat?.Progress ?? 0,
							Watched = watchdat?.Watched ?? false
						};

						Episodes.Add(episode);
					}
				}
		}

		#endregion Public Constructors

		#region Public Methods

		public override bool Equals(object obj)
		{
			return obj is Season season &&
					 EqualityComparer<Show>.Default.Equals(Show, season.Show) &&
					 SeasonNumber == season.SeasonNumber;
		}

		public override int GetHashCode()
		{
			var hashCode = -1562695867;
			hashCode = hashCode * -1521134295 + EqualityComparer<Show>.Default.GetHashCode(Show);
			hashCode = hashCode * -1521134295 + SeasonNumber.GetHashCode();
			return hashCode;
		}

		public void ShowStrip()
		{
			FlatToolStrip.Show(Data.Dashboard,
				new FlatStripItem("Play", () =>  Show.GetCurrentWatchEpisode(SeasonNumber).Play(), image: Properties.Resources.Tiny_Play, show: Show.GetCurrentWatchEpisode(SeasonNumber)?.VidFile?.Exists ?? false),
				new FlatStripItem("Download", () =>
				{
					Data.Dashboard.PushPanel(null, new PC_Download(this));
				}, image: Properties.Resources.Tiny_Download),
				FlatStripItem.Empty,
				new FlatStripItem("Mark as " + (Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched) ? "Unwatched" : "Watched"), show: !Show.Temporary, image: Properties.Resources.Tiny_Ok, action: () =>
				{
					var set = !Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched);
					Episodes.ForEach(e =>
					{
						e.Watched = e.AirState == AirStateEnum.Aired ? set : e.Watched;
						if(e.Watched)
							e.WatchDate = DateTime.Now;
					});
					new Action(()=>
					{
						LocalShowHandler.Refresh(Show);
						ShowManager.Save(Show);
					}).RunInBackground();
				})
			);
		}

		public override string ToString() => Name;

		#endregion Public Methods
	}
}