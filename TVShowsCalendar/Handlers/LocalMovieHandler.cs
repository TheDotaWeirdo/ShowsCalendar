using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using Extensions;
using ShowsRenamer.Module.Handlers;
using ShowsCalendar.Classes;

namespace ShowsCalendar.Handlers
{
	public static class LocalMovieHandler
	{
		public delegate void MovieLibraryEventHandler(Movie show);

		private const int GENERAL_MIN_CHAR_LENGTH = 3;
		private const int SPELLCHECK_MAX_ERRORS = 1;
		private const int SPELLCHECK_MAX_SCORE = 10;

		public static event MovieLibraryEventHandler FolderChanged;

		public static event MovieLibraryEventHandler MovieWatchChanged;

		public static void ReloadMovieFiles() => ReloadMovieFiles(null);

		private static OneWayTask OneWayTask = new OneWayTask();

		public static void ReloadMovieFiles(Movie movie)
		{
			if (movie == null)
				OneWayTask.Run(() => LoadFiles(), false);
			else
				LoadFiles(movie);
		}

		private static void LoadFiles(Movie mov = null)
		{
			if (LocalFileHandler.Paused) return;

			var movies = mov == null ? MovieManager.Movies : new List<Movie> { mov };
			movies.ForEach(x => x.VidFiles.Clear());

			foreach (var item in LocalFileHandler.GeneralFolders.Where(x => x.Exists).ConvertEnumerable(x => x.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions)))
			{
				foreach (var movie in movies)
				{
					if (Match(movie.Title, item.FileName()) && !movie.VidFiles.Any(x => item.FullName.Equals(x.FullName, StringComparison.CurrentCultureIgnoreCase)))
						movie.VidFiles.Add(item); 
				}
			}

			FolderChanged?.Invoke(mov);
		}

		public static void Refresh(Movie movie = null)
			=> MovieWatchChanged?.Invoke(movie);

		public static IEnumerable<FileInfo> GetFile(this Movie movie)
		{
			foreach (var item in LocalFileHandler.GeneralFolders.Where(x => x.Exists).ConvertEnumerable(x => x.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions)))
			{
				if (Match(movie.Title, item.FileName()))
					yield return item;
			}
		}

		public static void Load()
		{			
			new Action(ReloadMovieFiles).RunInBackground();
		}

		private static void File_Changed(object sender, FileSystemEventArgs e)
		{
			if (!LocalFileHandler.Paused && Path.GetExtension(e.Name).ToLower().AnyOf(EpisodeFileHandler.VideoExtensions))
				new Action(ReloadMovieFiles).RunInBackground();
		}

		public static void LoadLibrary(out List<Movie> onDeck, out List<Movie> continueWatching, out List<Movie> startWatching, Movie refMovie = null)
		{
			onDeck = new List<Movie>();
			continueWatching = new List<Movie>();
			startWatching = new List<Movie>();

			foreach (var movie in MovieManager.Movies.Where(x => refMovie == null || refMovie == x))
			{
				if (movie.VidFiles.Any(x => x.Exists))
				{
					if (movie.Started)
					{
						if (movie.WatchDate > DateTime.Now.AddDays(-10) && !movie.Watched)
							onDeck.Add(movie);
						else
							continueWatching.Add(movie);
					}
					else if (!movie.Watched)
						startWatching.Add(movie);
				}
			}
		}

		public static void LoadLibrary(out List<Movie> onDeck, Movie refMovie = null)
		{
			onDeck = new List<Movie>();

			foreach (var movie in MovieManager.Movies.Where(x => refMovie == null || refMovie == x))
			{
				if (movie.VidFiles.Any(x => x.Exists))
				{
					if (movie.Started && !movie.Watched)
					{
						if (movie.WatchDate > DateTime.Now.AddDays(-10))
							onDeck.Add(movie);
					}
				}
			}
		}

		public static bool Match(string showName, string folder)
		{
			if (Check(showName, folder))
				return true;

			folder = NameExtractor.GetMovieTitleYear(folder).Item1.RegexRemove(".\\d{4}.$").Trim();

			if (Check(showName, folder))
				return true;

			showName = NameExtractor.GetMovieTitleYear(showName).Item1.RegexRemove(".\\d{4}.$").Trim();

			if (Check(showName, folder))
				return true;

			return false;
		}

		private static bool Check(string s1, string s2)
		{
			// Exact Match
			if (s1.Equals(s2, StringComparison.CurrentCultureIgnoreCase))
				return true;

			// Spell Check
			if (s1.SpellCheck(s2, SPELLCHECK_MAX_ERRORS, false))
				return true;

			return false;
		}
	}
}