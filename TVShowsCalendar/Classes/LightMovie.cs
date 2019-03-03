using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Search;

namespace ShowsCalendar.Classes
{
	public class LightMovie
	{
		public string Overview { get; set; }
		public DateTime? ReleaseDate { get; set; }
		public string Title { get; set; }
		public List<int> GenreIds { get; set; }
		public int Id { get; set; }
		public string PosterPath { get; set; }

		public static LightMovie Convert(SearchMovie searchMovie)
		{
			return new LightMovie()
			{
				Title = searchMovie.Title,
				GenreIds = searchMovie.GenreIds,
				ReleaseDate = searchMovie.ReleaseDate,
				Overview = searchMovie.Overview,
				Id = searchMovie.Id,
				PosterPath = searchMovie.PosterPath
			};
		}
	}
}
