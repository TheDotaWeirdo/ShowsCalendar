using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace ShowsCalendar.Handlers
{
	public class LocalFileHandler
	{
		public static List<DirectoryInfo> GeneralFolders = new List<DirectoryInfo>();
		public static Dictionary<string, FileSystemWatcher> watchers = new Dictionary<string, FileSystemWatcher>();

		public static bool Paused { get; private set; }

		public static event Action FilesChanged;

		public static void Load()
		{
			ISave.Load(out GeneralFolders, "GeneralFolders.tf");

			foreach (var item in GeneralFolders)
			{
				addWatcher(item);
			}

			var driveWatcher = new ManagementEventWatcher();
			driveWatcher.EventArrived += (s, e) => file_Changed(null, null);
			driveWatcher.Query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
			driveWatcher.Start();
		}

		public static void Pause() => Paused = true;

		public static void Resume() { Paused = false; FilesChanged?.Invoke(); }

		private static void addWatcher(DirectoryInfo item)
		{
			if (item.Exists)
			{
				var watcher = new FileSystemWatcher
				{
					Path = item.FullName,
					NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size,
					Filter = "*.*",
					EnableRaisingEvents = true,
					IncludeSubdirectories = true
				};

				watcher.Changed += file_Changed;
				watcher.Created += file_Changed;
				watcher.Deleted += file_Changed;

				watchers.TryAdd(item.FullName.ToLower(), watcher);
			}
		}

		private static void removeWatcher(DirectoryInfo item)
		{
			watchers.TryGetValue(item.FullName.ToLower(), out var watcher);

			if (watcher != null)
			{
				watcher.Dispose();
				watchers.Remove(item.FullName.ToLower());
			}
		}

		private static void file_Changed(object sender, FileSystemEventArgs e)
		{
			if (!Paused)
				FilesChanged?.Invoke();
		}

		public static void AddGeneralFolder(string path)
		{
			try
			{
				if (!GeneralFolders.Any(x => x.FullName.Equals(path, StringComparison.CurrentCultureIgnoreCase)))
				{
					GeneralFolders.Add(new DirectoryInfo(path));

					ISave.Save(GeneralFolders, "GeneralFolders.tf");

					if (Directory.Exists(path))
						addWatcher(new DirectoryInfo(path));

					if (!Paused)
						FilesChanged?.Invoke();
				}
			}
			catch { }
		}

		public static void RemoveGeneralFolder(string path)
		{
			GeneralFolders.Remove(GeneralFolders.FirstThat(x => x.FullName.Equals(path, StringComparison.CurrentCultureIgnoreCase)));

			ISave.Save(GeneralFolders, "GeneralFolders.tf");

			removeWatcher(new DirectoryInfo(path));

			if (!Paused)
				FilesChanged?.Invoke();
		}
	}
}
