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
		public Movie() { }

		public Movie(LightMovie searchTv)
		{
			Id = searchTv.Id;
			base.Name = searchTv.Title;
			Loaded = false;

			new Action(startFirstLoad).RunInBackground(System.Threading.ThreadPriority.Lowest);
		}

		private async void startFirstLoad()
		{
			var dat = await Data.TMDbHandler.GetMovie(Id);

			Loaded = true;
			TMDbData = dat;
			MovieLoaded?.Invoke(this);
			MovieManager.Save(this);
		}
	}
}
