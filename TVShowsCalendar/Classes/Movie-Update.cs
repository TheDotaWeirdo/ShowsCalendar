using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace ShowsCalendar.Classes
{
	public partial class Movie
	{
		public void Refresh()
		{
			new Action(startRefresh).RunInBackground(System.Threading.ThreadPriority.Lowest);
		}

		private async void startRefresh()
		{
			var dat = await Data.TMDbHandler.GetMovie(TMDbData.Id);

			TMDbData = dat;
			MovieLoaded?.Invoke(this);
			MovieManager.Save(this);
		}
	}
}
