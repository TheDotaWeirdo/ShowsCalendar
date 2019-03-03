using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlickControls.Enums;
using SlickControls.Forms;
using ShowsCalendar.Forms;

#pragma warning disable CS4014, CS1998
namespace ShowsCalendar.Handlers
{
	public static class UpdateHandler
	{
		private static System.Timers.Timer Timer = new System.Timers.Timer(3600000);

		public static void Start()
		{
			Timer.Start();
			Timer.Elapsed += (s, e) => Timer_Elapsed();
		}

		private static async void Timer_Elapsed()
		{
			if (DateTime.Now.Hour % 12 == 0 && Extensions.ConnectionHandler.State == Extensions.ConnectionState.Connected)
			{
				try
				{
					foreach (var show in ShowManager.Shows)
					{
						try { show.Refresh(); }
						catch(Exception ex)
						{
							MessagePrompt.Show($"Error occurred while updating the show {show.Name}\n\n{ex.Message}", "Error", icon: PromptIcons.Error);
							Clipboard.SetText(ex.ToString());
						}
					}
				}
				catch { }

				try
				{
					var movies = MovieManager.Movies.ToArray();
					foreach (var movie in movies)
					{
						try { movie.Refresh(); }
						catch (Exception ex)
						{
							MessagePrompt.Show($"Error occurred while updating the movie {movie.Title}\n\n{ex.Message}", "Error", icon: PromptIcons.Error);
							Clipboard.SetText(ex.ToString());
						}
					}
				}
				catch { }
			}
		}
	}
}
#pragma warning restore CS4014, CS1998