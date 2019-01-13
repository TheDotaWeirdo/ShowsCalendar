using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Extensions;
using TVShowsCalendar.Classes;

namespace TVShowsCalendar.HandlerClasses
{
	public static class LocalShowHandler
	{
		public static List<ShowAssociation> Associations = new List<ShowAssociation>();

		public static List<DirectoryInfo> GeneralFolders = new List<DirectoryInfo>();

		private const int GENERAL_MIN_CHAR_LENGTH = 3;
		private const int SPELLCHECK_MAX_ERRORS = 1;
		private const int SPELLCHECK_MAX_SCORE = 10;

		public delegate void ShowLibraryEventHandler(Show show);

		public static event ShowLibraryEventHandler EpisodeWatchChanged;

		public static event ShowLibraryEventHandler FolderChanged;

		public static void AddGeneralFolder(string path)
		{
			try
			{
				if (!GeneralFolders.Any(x => x.FullName.Equals(path, StringComparison.CurrentCultureIgnoreCase)))
				{
					GeneralFolders.Add(new DirectoryInfo(path));

					Save();

					new Action(LoadFiles).RunInBackground();

					if (Directory.Exists(path))
					{
						var watcher = new FileSystemWatcher
						{
							Path = path,
							NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
							Filter = "*.*",
							EnableRaisingEvents = true
						};

						watcher.Changed += (s, e) =>
						{
							if (new DirectoryInfo(e.FullPath).GetFilesByExtensions(EpisodeFileHandler.VideoExtensions).Any())
								new Action(LoadFiles).RunInBackground();
						};
					}
				}
			}
			catch { }
		}

		public static void AddShowAssociation(Show show, DirectoryInfo path)
		{
			if (Associations.Any(x => x.ShowId == show.TMDbData.Id))
			{
				var showAssociation = Associations.FirstThat(x => x.ShowId == show.TMDbData.Id);

				if (!showAssociation.LinkedFolders.Contains(path))
				{
					showAssociation.LinkedFolders.Add(path);
					new Action(() => LoadFiles(show)).RunInBackground();

					Save();
				}
			}
			else
			{
				Associations.Add(new ShowAssociation(show.tMDbData.Id, new List<DirectoryInfo> { path }));
				new Action(() => LoadFiles(show)).RunInBackground();

				Save();
			}
		}

		public static Episode GetCurrentWatchEpisode(this Show show, int? seasonNumber = null)
		{
			if (seasonNumber == null)
				return show.Episodes.FirstThat(x => (x.VidFile?.Exists ?? false) && (!x.Watched))
					?? show.Episodes.FirstThat(x => (x.VidFile?.Exists ?? false));

			return show.Episodes.FirstThat(x => x.SN == seasonNumber && (x.VidFile?.Exists ?? false) && (!x.Watched))
				?? show.Episodes.FirstThat(x => x.SN == seasonNumber && (x.VidFile?.Exists ?? false));
		}

		public static DateTime GetDateOrder(this Episode ep)
		{
			if (ep.Started)
				return ep.WatchDate;

			var prev = ep.Previous;
			if (prev != null)
				return prev.WatchDate;

			return DateTime.MinValue;
		}

		public static DateTime GetDateOrder(this Movie mov)
		{
			if (mov.Started)
				return mov.WatchDate;

			return DateTime.MinValue;
		}

		public static FileInfo GetFile(this Episode episode)
		{
			try
			{
				if (Associations.Any(x => x.ShowId == episode.Show.TMDbData.Id))
				{
					foreach (var folder in Associations.FirstThat(x => x.ShowId == episode.Show.TMDbData.Id).LinkedFolders.Where(x => x.Exists))
					{
						foreach (var file in folder.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions))
						{
							if (EpisodeFileHandler.IsValid(file))
							{
								var ENs = EpisodeFileHandler.GetEpNumbers(file);
								if (ENs != null && ENs.Item1 == episode.SN && ENs.Item2 == episode.EN)
									return file;
							}
						}
					}
				}
				else
				{
					foreach (var folder in GeneralFolders.Where(x => x.Exists).ConvertEnumerable(x => x.EnumerateDirectories("*", SearchOption.AllDirectories)))
					{
							if (Match(episode.Show.Name, folder.Name))
							{
								foreach (var file in folder.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions))
								{
									if (EpisodeFileHandler.IsValid(file))
									{
										var ENs = EpisodeFileHandler.GetEpNumbers(file);
										if (ENs != null && ENs.Item1 == episode.SN && ENs.Item2 == episode.EN)
											return file;
									}
								}
							}
					}
				}
			}
			catch { }

			return null;
		}

		public static void GetFiles(Show show)
		{
			try
			{
				if (Associations.Any(x => x.ShowId == show.TMDbData.Id))
				{
					foreach (var folder in Associations.FirstThat(x => x.ShowId == show.TMDbData.Id).LinkedFolders.Where(x => x.Exists))
					{
						foreach (var file in folder.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions).Where(EpisodeFileHandler.IsValid))
						{
							foreach (var episode in show.Episodes.Where(x => !x.VidFile?.Exists ?? true))
							{
								var ENs = EpisodeFileHandler.GetEpNumbers(file);
								if (ENs != null && ENs.Item1 == episode.SN && ENs.Item2 == episode.EN)
									episode.VidFile = file;
							}
						}
					}
				}
				else
				{
					foreach (var item in GeneralFolders.Where(x => x.Exists).Convert(x => x.GetDirectories("*", SearchOption.AllDirectories)))
					{
						foreach (var folder in item)
						{
							if (Match(show.Name, folder.Name))
							{
								foreach (var file in folder.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions).Where(EpisodeFileHandler.IsValid))
								{
									foreach (var episode in show.Episodes.Where(x => !x.VidFile?.Exists ?? true))
									{
										var ENs = EpisodeFileHandler.GetEpNumbers(file);
										if (ENs != null && ENs.Item1 == episode.SN && ENs.Item2 == episode.EN)
										{
											episode.VidFile = file;
											break;
										}
									}
								}

								if (!show.Episodes.Any(x => x.AirState == AirStateEnum.Aired && (!x.VidFile?.Exists ?? true)))
									return;
							}
						}
					}
				}
			}
			catch { }
		}

		public static void Load()
		{
			EpisodeFileHandler.Load();

			ISave.Load(out GeneralFolders, "GeneralFolders.tf");
			ISave.Load(out Associations, "Associations.tf");

			foreach (var item in GeneralFolders)
			{
				if (Directory.Exists(item.FullName))
				{
					var watcher = new FileSystemWatcher
					{
						Path = item.FullName,
						NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
						Filter = "*.*",
						EnableRaisingEvents = true,
						IncludeSubdirectories = true
					};

					watcher.Changed += File_Changed;
					watcher.Created += File_Changed;
					watcher.Deleted += File_Changed;
				}
			}

			ShowManager.ShowLoaded += ShowManager_ShowLoaded;
			ShowManager.ShowAdded += ShowManager_ShowLoaded;

			LoadFiles();
		}

		public static void LoadFiles() => LoadFiles(null);

		public static void LoadFiles(Show show)
		{
			if (show != null)
			{
				GetFiles(show);
				FolderChanged?.Invoke(show);
			}
			else
			{
				foreach (var item in ShowManager.Shows)
					GetFiles(item);

				FolderChanged?.Invoke(null);
			}
		}

		public static void LoadLibrary(out List<Episode> onDeck, out List<Episode> continueWatching, out List<Episode> startWatching, out List<Episode> lastWatched, Show refShow = null)
		{
			onDeck = new List<Episode>();
			continueWatching = new List<Episode>();
			startWatching = new List<Episode>();
			lastWatched = new List<Episode>();

			foreach (var show in ShowManager.Shows.Where(x => refShow == null || refShow == x))
			{
				var firstEp = show.Episodes.FirstThat(x => x.VidFile != null && !x.Watched);
				var lastEp = show.Episodes.LastThat(x => x.Watched);

				if (firstEp != null)
				{
					if ((lastEp != null && lastEp.WatchDate > DateTime.Now.AddDays(-10)) || firstEp.Started)
						onDeck.Add(firstEp);
					else if (lastEp != null)
						continueWatching.Add(firstEp);

					if (lastEp == null)
						startWatching.Add(firstEp);
				}

				if (lastEp != null && lastEp.VidFile != null && lastEp.WatchDate > DateTime.Today.AddDays(-14))
					lastWatched.Add(lastEp);
			}
		}

		public static void LoadLibrary(out List<Episode> onDeck, Show refShow = null)
		{
			onDeck = new List<Episode>();

			foreach (var show in ShowManager.Shows.Where(x => refShow == null || refShow == x))
			{
				var firstEp = show.Episodes.FirstThat(x => x.VidFile != null && !x.Watched);
				var lastEp = show.Episodes.LastThat(x => x.Watched);

				if (firstEp != null)
				{
					if ((lastEp != null && lastEp.WatchDate > DateTime.Now.AddDays(-10)) || firstEp.Started)
						onDeck.Add(firstEp);
				}
			}
		}

		public static bool Match(string showName, string folder)
		{
			// Exact Match
			if (showName.Equals(folder, StringComparison.CurrentCultureIgnoreCase))
				return true;

			// Spell Check
			if (showName.SpellCheck(folder, SPELLCHECK_MAX_ERRORS, false))
				return true;

			return false;
		}

		public static void Refresh(Show show = null)
							=> EpisodeWatchChanged?.Invoke(show);

		public static void RemoveGeneralFolder(params string[] paths)
		{
			foreach (var item in paths)
			{
				GeneralFolders.Remove(GeneralFolders.FirstThat(x => x.FullName.Equals(item, StringComparison.CurrentCultureIgnoreCase)));
			}

			new Action(LoadFiles).RunInBackground();
			Save();
		}

		public static void Save()
		{
			ISave.Save(GeneralFolders, "GeneralFolders.tf");
			ISave.Save(Associations, "Associations.tf");
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

		private static void File_Changed(object sender, FileSystemEventArgs e)
		{
			new Action(LoadFiles).RunInBackground();
		}

		private static void ShowManager_ShowLoaded(Show show)
		{
			new Action(() => LoadFiles(show)).RunInBackground();
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