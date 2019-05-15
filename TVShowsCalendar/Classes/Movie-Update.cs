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
			ConnectionHandler.WhenConnected(() => new Action(startRefresh).RunInBackground(System.Threading.ThreadPriority.Lowest));
		}

		private async void startRefresh()
		{
			try
			{
				var dat = await Data.TMDbHandler.GetMovie(Id);

				TMDbData = dat;
				MovieLoaded?.Invoke(this);
				MovieManager.Save(this);
			}
			catch { }
		}
	}
}
