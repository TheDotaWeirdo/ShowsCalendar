using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowsCalendar.Classes;
using Extensions;
using System.IO;
using System.Text.RegularExpressions;
using ShowsRenamer.Module.Handlers;

namespace ShowsCalendar.Handlers
{
	public static class MediaDiscoveryHandler
	{
		public static IEnumerable<LightShow> DiscoverShows()
		{
			foreach (var item in LocalFileHandler.GeneralFolders
				.Where(x => Directory.Exists(x.FullName))
				.ConvertEnumerable(x => x.EnumerateDirectories("*", SearchOption.AllDirectories))
				.Where(isValid))
			{
				var name = NameExtractor.GetSeriesName(item.Name);

				if (!string.IsNullOrWhiteSpace(name))
				{
					var shows = Data.TMDbHandler.SearchTvShow(name)?.Result?.Take(2);

					if (shows != null)
						foreach (var show in shows)
							yield return LightShow.Convert(show);
				}
			}
		}

		public static IEnumerable<LightMovie> DiscoverMovies()
		{
			foreach (var item in LocalFileHandler.GeneralFolders
				.Where(x => Directory.Exists(x.FullName))
				.ConvertEnumerable(x => x.EnumerateDirectories("*", SearchOption.AllDirectories))
				.Where(isValid))
			{
				var tuple = NameExtractor.GetMovieTitleYear(item.Name);

				if (!string.IsNullOrWhiteSpace(tuple.Item1))
				{
					var movies = Data.TMDbHandler.SearchMovie(tuple.Item1, year: tuple.Item2)?.Result?.Take(2);

					if (movies != null)
						foreach (var movie in movies)
							yield return LightMovie.Convert(movie);
				}
			}
		}

		private static bool isValid(DirectoryInfo directory)
		{
			if (Regex.IsMatch(directory.Name, "season([^\\d]+)?\\d+", RegexOptions.IgnoreCase))
				return false;

			if (!directory.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions).Any())
				return false;

			if (genericNames.Any(x => directory.Name.Equals(x, StringComparison.CurrentCultureIgnoreCase)))
				return false;

			return true;
		}

		private static string[] genericNames = new[]
		{
			"desktop",
			"shows",
			"series",
			"tv shows",
			"tv series",
			"videos",
			"movies",
			"anime",
			"captures"
		};
	}
}
