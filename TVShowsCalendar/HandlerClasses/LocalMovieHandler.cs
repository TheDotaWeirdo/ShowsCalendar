using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Extensions;
using TVShowsCalendar.Classes;

namespace TVShowsCalendar.HandlerClasses
{
	public static class LocalMovieHandler
	{
		public delegate void MovieLibraryEventHandler(Movie show);

		private const int GENERAL_MIN_CHAR_LENGTH = 3;
		private const int SPELLCHECK_MAX_ERRORS = 1;
		private const int SPELLCHECK_MAX_SCORE = 10;

		public static event MovieLibraryEventHandler FolderChanged;

		public static event MovieLibraryEventHandler MovieWatchChanged;

		public static List<DirectoryInfo> GeneralFolders => LocalShowHandler.GeneralFolders;

		public static void AddGeneralFolder(string path) => LocalShowHandler.AddGeneralFolder(path);

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
			var movies = mov == null ? MovieManager.Movies : new List<Movie> { mov };

			foreach (var item in GeneralFolders.Where(x => x.Exists).ConvertEnumerable(x => x.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions)))
			{
				foreach (var movie in movies.Where(x => !x.VidFile?.Exists ?? true))
				{
					if (Match(movie.Title, item.FileName()))
						movie.VidFile = item;
				}
			}

			FolderChanged?.Invoke(mov);
		}

		public static void Refresh(Movie movie = null)
			=> MovieWatchChanged?.Invoke(movie);

		public static FileInfo GetFile(this Movie movie)
		{
			try
			{
				foreach (var item in GeneralFolders.Where(x => x.Exists).ConvertEnumerable(x => x.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions)))
				{
					if (Match(movie.Title, item.FileName()))
						return item;
				}
			}
			catch { }

			return null;
		}

		public static void Load()
		{
			if (!(GeneralFolders?.Any() ?? false))
				ISave.Load(out LocalShowHandler.GeneralFolders, "GeneralFolders.tf");
			
			foreach (var item in GeneralFolders)
			{
				if (Directory.Exists(item.FullName))
				{
					var watcher = new FileSystemWatcher
					{
						Path = item.FullName,
						NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
						Filter = "*.*",
						EnableRaisingEvents = true
					};

					watcher.Changed += File_Changed;
					watcher.Created += File_Changed;
					watcher.Deleted += File_Changed;
				}
			}

			new Action(ReloadMovieFiles).RunInBackground();
		}

		private static void File_Changed(object sender, FileSystemEventArgs e)
		{
			if (Path.GetExtension(e.Name).ToLower().AnyOf(EpisodeFileHandler.VideoExtensions))
				new Action(ReloadMovieFiles).RunInBackground();
		}

		public static void LoadLibrary(out List<Movie> onDeck, out List<Movie> continueWatching, out List<Movie> startWatching, Movie refMovie = null)
		{
			onDeck = new List<Movie>();
			continueWatching = new List<Movie>();
			startWatching = new List<Movie>();

			foreach (var movie in MovieManager.Movies.Where(x => refMovie == null || refMovie == x))
			{
				if (movie.VidFile?.Exists ?? false)
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
				if (movie.VidFile?.Exists ?? false)
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
			folder = folder.RegexRemove(".\\d{4}.$").Trim();

			// Exact Match
			if (showName.Equals(folder, StringComparison.CurrentCultureIgnoreCase))
				return true;

			// Spell Check
			if (showName.SpellCheck(folder, 1, false))
				return true;

			return false;
		}

		public static void SpellCheck(string s1, string s2, out int errors)
		{
			// Levenshtein Algorithm
			var n = s1.Length;
			var m = s2.Length;
			var d = new int[n + 1, m + 1];

			if (n == 0)
			{ errors = m; return; }

			if (m == 0)
			{ errors = n; return; }

			for (int i = 0; i <= n; d[i, 0] = i++) { }

			for (int j = 0; j <= m; d[0, j] = j++) { }

			for (int i = 1; i <= n; i++)
			{
				for (int j = 1; j <= m; j++)
				{
					int cost = (s2[j - 1] == s1[i - 1]) ? 0 : 1;

					d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
						 d[i - 1, j - 1] + cost);
				}
			}

			errors = d[n, m];
		}

		private static int SimilarCheck(string file, string test)
		{
			var s1 = file.ToLower();
			var s2 = test.ToLower();

			if (s1.Length > s2.Length)
				s1.Substring(0, s2.Length);
			else
				s2.Substring(0, s1.Length);

			if (s1.Length <= GENERAL_MIN_CHAR_LENGTH)
				return 0;

			return SPELLCHECK_MAX_ERRORS - SpellCheck(s1, s2);
		}

		private static int SpellCheck(string format1, string format2)
		{
			var bestScore = 0;
			format1 = format1.ToLower();
			format2 = format2.ToLower();

			if (format1.Length == 0)
				return 0;

			if (format2.Length == 0)
				return 0;

			if (Math.Abs(format1.Length - format2.Length) > SPELLCHECK_MAX_ERRORS || format1[0] != format2[0])
				return 0;

			if (format1.Length <= GENERAL_MIN_CHAR_LENGTH || format2.Length <= GENERAL_MIN_CHAR_LENGTH)
				return 0;

			SpellCheck(format1, format2, out var totErrors);

			if (totErrors > SPELLCHECK_MAX_ERRORS)
				return 0;

			bestScore = bestScore > SPELLCHECK_MAX_SCORE - totErrors ? bestScore : SPELLCHECK_MAX_SCORE - totErrors;

			return bestScore;
		}
	}
}