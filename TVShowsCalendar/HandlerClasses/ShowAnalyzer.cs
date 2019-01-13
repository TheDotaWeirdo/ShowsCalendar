using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Extensions;
using TVShowsCalendar.Classes;

namespace TVShowsCalendar
{
	public static class ShowAnalyzer
	{
		public static async Task<string> GetHTML(string URL, int? Season = null, int? Episode = null)
		{
			try
			{
				string HTML;
				using (var wp = new WebProcessor())
				{
					load: HTML = wp.GetGeneratedHTML(URL + (Season != null ? (URL.Last() == '/' ? "" : "/") + Season + (Episode != null ? "x" + Episode : "") + ".html" : ""));

					if (HTML.Contains("Please try again in a few minutes."))
					{
						await Task.Delay(30000);
						goto load;
					}
				}

				return HTML;
			}
			catch { return string.Empty; }
		}
	}
}