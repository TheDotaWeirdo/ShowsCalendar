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
	public static class LocalShowHandler
	{
		public static List<ShowAssociation> Associations = new List<ShowAssociation>();

		private const int GENERAL_MIN_CHAR_LENGTH = 3;
		private const int SPELLCHECK_MAX_ERRORS = 1;
		private const int SPELLCHECK_MAX_SCORE = 10;

		public delegate void ShowLibraryEventHandler(Show show);

		public static event ShowLibraryEventHandler EpisodeWatchChanged;

		public static event ShowLibraryEventHandler FolderChanged;

		public static void AddShowAssociation(Show show, DirectoryInfo path)
		{
			if (Associations.Any(x => x.ShowId == show.TMDbData.Id))
			{
				var showAssociation = Associations.FirstThat(x => x.ShowId == show.TMDbData.Id);

				if (!showAssociation.LinkedFolders.Contains(path))
				{
					showAssociation.LinkedFolders.Add(path);
					new Action(() => LoadFiles(show)).RunInBackground();
				}
			}
			else
			{
				Associations.Add(new ShowAssociation(show.tMDbData.Id, new List<DirectoryInfo> { path }));
				new Action(() => LoadFiles(show)).RunInBackground();
			}

			ISave.Save(Associations, "Associations.tf");
		}

		public static Episode GetCurrentWatchEpisode(this Show show, int? seasonNumber = null)
		{
			if (seasonNumber == null)
				return show.Episodes.FirstThat(x => (x.VidFiles.Any(y => y.Exists)) && (!x.Watched))
					?? show.Episodes.FirstThat(x => (x.VidFiles.Any(y => y.Exists)));

			return show.Episodes.FirstThat(x => x.SN == seasonNumber && (x.VidFiles.Any(y => y.Exists)) && (!x.Watched))
				?? show.Episodes.FirstThat(x => x.SN == seasonNumber && (x.VidFiles.Any(y => y.Exists)));
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

		public static IEnumerable<FileInfo> GetFile(this Episode episode)
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
								yield return file;
						}
					}
				}
			}
			else
			{
				foreach (var folder in LocalFileHandler.GeneralFolders.Where(x => x.Exists).ConvertEnumerable(x => x.EnumerateDirectories("*", SearchOption.AllDirectories)))
				{
						if (Match(episode.Show.Name, folder.Name))
						{
							foreach (var file in folder.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions))
							{
								if (EpisodeFileHandler.IsValid(file))
								{
									var ENs = EpisodeFileHandler.GetEpNumbers(file);
									if (ENs != null && ENs.Item1 == episode.SN && ENs.Item2 == episode.EN)
										yield return file;
								}
							}
						}
				}
			}
		}

		public static string GetShowFolder(Show show)
		{
			if (Associations.Any(x => x.ShowId == show.TMDbData.Id))
			{
				return Associations.First(x => x.ShowId == show.TMDbData.Id).LinkedFolders.First().FullName;
			}
			else
			{
				foreach (var item in LocalFileHandler.GeneralFolders.Where(x => x.Exists).Convert(x => x.GetDirectories("*", SearchOption.AllDirectories)))
				{
					foreach (var folder in item)
					{
						if (Match(show.Name, folder.Name))
						{
							return folder.FullName;
						}
					}
				}
			}

			return string.Empty;
		}

		public static void GetFiles(Show show)
		{
			try
			{
				show.Episodes.Foreach(x => x.VidFiles.Clear());

				if (Associations.Any(x => x.ShowId == show.TMDbData.Id))
				{
					foreach (var folder in Associations.FirstThat(x => x.ShowId == show.TMDbData.Id).LinkedFolders.Where(x => x.Exists))
					{
						foreach (var file in folder.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions).Where(EpisodeFileHandler.IsValid))
						{
							foreach (var episode in show.Episodes.Where(x => !x.VidFiles.Any(y => y.Exists)))
							{
								var ENs = EpisodeFileHandler.GetEpNumbers(file);
								if (ENs != null
									&& ENs.Item1 == episode.SN
									&& ENs.Item2 == episode.EN
									&& !episode.VidFiles.Any(x => file.FullName.Equals(x.FullName, StringComparison.CurrentCultureIgnoreCase)))
								{ episode.VidFiles.Add(file); }
							}
						}
					}
				}
				else
				{
					foreach (var item in LocalFileHandler.GeneralFolders.Where(x => x.Exists).Convert(x => x.GetDirectories("*", SearchOption.AllDirectories)))
					{
						foreach (var folder in item)
						{
							if (Match(show.Name, folder.Name))
							{
								foreach (var file in folder.GetFilesByExtensions(EpisodeFileHandler.VideoExtensions).Where(EpisodeFileHandler.IsValid))
								{
									foreach (var episode in show.Episodes)
									{
										var ENs = EpisodeFileHandler.GetEpNumbers(file);
										if (ENs != null
											&& ENs.Item1 == episode.SN
											&& ENs.Item2 == episode.EN
											&& !episode.VidFiles.Any(x => file.FullName.Equals(x.FullName, StringComparison.CurrentCultureIgnoreCase)))
										{ episode.VidFiles.Add(file); }
									}
								}

								if (!show.Episodes.Any(x => x.AirState == AirStateEnum.Aired && (!x.VidFiles.Any(y => y.Exists))))
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
			ISave.Load(out Associations, "Associations.tf");

			foreach (var item in Associations.ConvertEnumerable(x => x.LinkedFolders).Distinct((x, y) => x.FullName == y.FullName))
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

			LocalFileHandler.FilesChanged += () => File_Changed(null, null);
		}

		public static void LoadFiles() => LoadFiles(null);

		public static void LoadFiles(Show show)
		{
			if (LocalFileHandler.Paused) return;

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
				var firstEp = show.Episodes.FirstThat(x => x.VidFiles.Any(y => y.Exists) && !x.Watched);
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

				if (lastEp != null && lastEp.VidFiles.Any(y => y.Exists) && lastEp.WatchDate > DateTime.Today.AddDays(-14))
					lastWatched.Add(lastEp);
			}
		}

		public static void LoadLibrary(out List<Episode> onDeck, Show refShow = null)
		{
			onDeck = new List<Episode>();

			foreach (var show in ShowManager.Shows.Where(x => refShow == null || refShow == x))
			{
				var firstEp = show.Episodes.FirstThat(x => x.VidFiles.Any(y => y.Exists) && !x.Watched);
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
			folder = NameExtractor.GetSeriesName(folder);

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
		
		private static void File_Changed(object sender, FileSystemEventArgs e)
		{
			if (!LocalFileHandler.Paused)
				new Action(LoadFiles).RunInBackground();
		}

		private static void ShowManager_ShowLoaded(Show show)
			=> new Action(() => LoadFiles(show)).RunInBackground();
	}
}