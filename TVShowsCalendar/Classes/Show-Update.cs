using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace ShowsCalendar.Classes
{
	public partial class Show
	{
		public void Refresh()
		{
			ConnectionHandler.WhenConnected(() => new Action(startRefresh).RunInBackground(System.Threading.ThreadPriority.Lowest));
		}

		private async void startRefresh()
		{
			try
			{
				var dat = await Data.TMDbHandler.GetTvShow(Id);
				var seasons = new List<Season>();

				foreach (var sn in dat.Seasons.Select(x => x.SeasonNumber))
					seasons.Add(new Season(await Data.TMDbHandler.GetTvSeason(Id, sn), this));

				foreach (var s in Seasons)
					s.Update(seasons.FirstThat(y => s.SeasonNumber == y.SeasonNumber));

				Seasons.AddRange(seasons.Where(y => !Seasons.Any(x => x.SeasonNumber == y.SeasonNumber)));
				TMDbData = dat;
				ShowLoaded?.Invoke(this);
				ShowManager.Save(this);
			}
			catch { }
		}
	}
}
