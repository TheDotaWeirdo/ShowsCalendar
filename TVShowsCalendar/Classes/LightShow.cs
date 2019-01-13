using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace TVShowsCalendar.Classes
{
	public class LightShow
	{
		public string Name { get; set; }
		public List<int> GenreIds { get; set; }
		public DateTime? FirstAirDate { get; set; }
		public string Overview { get; set; }
		public int Id { get; set; }
		public string PosterPath { get; set; }

		public static LightShow Convert(SearchTv searchTv)
		{
			return new LightShow()
			{
				Name = searchTv.Name,
				GenreIds = searchTv.GenreIds,
				FirstAirDate = searchTv.FirstAirDate,
				Overview = searchTv.Overview,
				Id = searchTv.Id,
				PosterPath = searchTv.PosterPath
			};
		}

		public static LightShow Convert(TvShow searchTv)
		{
			return new LightShow()
			{
				Name = searchTv.Name,
				GenreIds = searchTv.GenreIds,
				FirstAirDate = searchTv.FirstAirDate,
				Overview = searchTv.Overview,
				Id = searchTv.Id,
				PosterPath = searchTv.PosterPath
			};
		}
	}
}
