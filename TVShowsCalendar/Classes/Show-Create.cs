using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using TMDbLib.Objects.General;

namespace ShowsCalendar.Classes
{
	public partial class Show
	{
		public Show() { }

		public Show(LightShow searchTv)
		{
			Id = searchTv.Id;
			base.Name = searchTv.Name;
			Loaded = false;

			new Action(startFirstLoad).RunInBackground(System.Threading.ThreadPriority.Lowest);
		}

		public ImagesWithId Images { get; internal set; }

		private async void startFirstLoad()
		{
			var dat = await Data.TMDbHandler.GetTvShow(Id);
			var seasons = new List<Season>();

			foreach (var sn in dat.Seasons.Select(x => x.SeasonNumber))
				seasons.Add(new Season(await Data.TMDbHandler.GetTvSeason(Id, sn), this));

			Loaded = true;
			Seasons = seasons;
			TMDbData = dat;
			ShowLoaded?.Invoke(this);
			ShowManager.Save(this);
		}
	}
}
