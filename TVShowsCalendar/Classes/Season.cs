using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using SlickControls.Classes;
using SlickControls.Forms;
using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;
using ShowsCalendar.Handlers;
using ShowsCalendar.Panels;

namespace ShowsCalendar.Classes
{
	public class Season
	{

		public List<Episode> Episodes { get; set; } = new List<Episode>();
		public TvSeason TMDbData { get; set; }

		internal string Name => TMDbData?.Name ?? $"Season {SeasonNumber}";

		internal int SeasonNumber => TMDbData.SeasonNumber;

		internal PosterImages Images { get; set; }

		internal Season Next
		{
			get
			{
				if (Show.Seasons.LastOrDefault() != this)
					return Show.Seasons[Show.Seasons.IndexOf(this) + 1];
				return null;
			}
		}

		internal Season Previous
		{
			get
			{
				if (Show.Seasons.FirstOrDefault() != this)
					return Show.Seasons[Show.Seasons.IndexOf(this) - 1];
				return null;
			}
		}

		internal Show Show { get; set; }

		public Season()
		{
		}

		public Season(TvSeason tvSeason, Show show = null)
		{
			TMDbData = tvSeason;
			Show = show;

			foreach (var ep in tvSeason.Episodes)
				Episodes.Add(new Episode(ep, this, show));
		}

		internal void Update(Season season)
		{
			if (season != null)
			{
				TMDbData = season.TMDbData;

				foreach (var e in Episodes)
					e.Update(season.Episodes.FirstThat(y => e.EN == y.EN));

				Episodes.AddRange(season.Episodes.Where(y => !Episodes.Any(x => x.EN == y.EN)));
			}
		}

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
			FlatToolStrip.Show(Data.Mainform,
				new FlatStripItem("Play", () =>  Show.GetCurrentWatchEpisode(SeasonNumber).Play(), image: Properties.Resources.Tiny_Play, show: Show.GetCurrentWatchEpisode(SeasonNumber)?.VidFiles.Any(x => x.Exists) ?? false),
				new FlatStripItem("Download", () =>
				{
					Data.Mainform.PushPanel(null, new PC_Download(this));
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

	}
}