using ShowsCalendar.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using System;
using System.Windows.Forms;

#pragma warning disable CS4014, CS1998
namespace ShowsCalendar.Handlers
{
	public static class UpdateHandler
	{
		private static System.Timers.Timer Timer;

		public static void Start()
		{
			Timer = new System.Timers.Timer(TimeSpan.FromHours(12).TotalMilliseconds);
			Timer.Start();
			Timer.Elapsed += (s, e) => Timer_Elapsed();
		}

		private static async void Timer_Elapsed()
		{
			if (Extensions.ConnectionHandler.State == Extensions.ConnectionState.Connected)
			{
				try
				{
					var shows = ShowManager.Shows.ToArray();
					foreach (var show in shows)
					{
						try { show.Refresh(); }
						catch (Exception ex)
						{
							MessagePrompt.Show($"Error occurred while updating the show {show.Name}\n\n{ex.Message}", "Error", icon: PromptIcons.Error, form: Data.Mainform);
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
							MessagePrompt.Show($"Error occurred while updating the movie {movie.Title}\n\n{ex.Message}", "Error", icon: PromptIcons.Error, form: Data.Mainform);
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