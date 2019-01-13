using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Extensions;
using TVShowsCalendar.Classes;

namespace TVShowsCalendar.HandlerClasses
{
	public static class EpisodeFileHandler
	{
		private static string DocumentsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TV Re-Namer 3");

		private static Regex[] testPresets = new Regex[]
		{
			new Regex(@"s(\d+)(?:.+)?e(\d+)", RegexOptions.IgnoreCase),
			new Regex(@"(\d+)x(\d+)", RegexOptions.IgnoreCase),
			new Regex(@"season\s+?(\d+)(?:.+)?episode\s+?(\d+)", RegexOptions.IgnoreCase),
			new Regex(@"season\s+?(\d+)(?:.+)?e(\d+)", RegexOptions.IgnoreCase),
			new Regex(@"s(\d+)(?:.+)?episode\s+?(\d+)", RegexOptions.IgnoreCase),
			new Regex(@"\b(\d)\s?(\d\d)\b", RegexOptions.IgnoreCase)
		};

		public static string[] VideoExtensions =
		{
			".mkv"  , ".webm" , ".flv"  , "vob"   , ".ogv",
			".ogg"  , ".drc"  , ".mng"  , ".avi"  , ".mov",
			".wmv"  , ".amv"  , ".mp4"  , ".m4p"  , ".m4v",
			".mpg"  , ".mpeg" , ".3gp"  , ".f4v"  , ".amv"
		};

		public static List<Regex> Tests;

		public static void Load()
		{
			try
			{
				ISave.Load(out List<Regex> data, "NamingStyles.tf", "TV Re-Namer 3");

				Tests = data.Concat(testPresets).Distinct().ToList();
			}
			catch
			{
				Tests = testPresets.ToList();
			}
		}

		public static bool IsValid(FileInfo file)
			=> VideoExtensions.Any(x => file.Extension.Equals(x, StringComparison.CurrentCultureIgnoreCase))
            && Tests.Any(x => x.IsMatch(file.FileName()));

		public static Tuple<int,int> GetEpNumbers(FileInfo file)
		{
			foreach (var test in Tests)
			{
				if (test.IsMatch(file.FileName()))
				{
					var grps = test.Match(file.FileName()).Groups;
                    if (!string.IsNullOrWhiteSpace(grps[1].Value) && !string.IsNullOrWhiteSpace(grps[2].Value))
                        return new Tuple<int, int>(grps[1].Value.SmartParse(), grps[2].Value.SmartParse());
				}
			}

			return null;
		}
	}
}
