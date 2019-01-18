using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Panels;

namespace TVShowsCalendar
{
	public static class MovieManager
	{
		public delegate void MovieChangeHandler(Movie show);

		public static List<Movie> Movies = new List<Movie>();

		public static event MovieChangeHandler MovieLoaded;

		public static event MovieChangeHandler MovieAdded;

		public static event MovieChangeHandler MovieRemoved;

		public static void Add(Movie movie, bool save = true)
		{
			if (!Movies.Contains(movie) && movie.Id != 0)
			{
				Movies.Add(movie);
				MovieAdded?.Invoke(movie);
				movie.MovieLoaded += MovieLoadedMethod;
			}

			if (save && movie.Id != 0)
				Save(movie);
		}

		public static void Remove(Movie movie)
		{
			if (DialogResult.No == MessagePrompt.Show($"Are you sure you want to remove {movie.Title}?", "Remove Movie?", PromptButtons.YesNo, PromptIcons.Hand, Data.Dashboard))
				return;

			start: try
			{
				movie.Delete();
				Movies.Remove(movie);
				movie.MovieLoaded -= MovieLoadedMethod;
				MovieRemoved?.Invoke(movie);
			}
			catch
			{
				if (DialogResult.Retry == MessagePrompt.Show($"Failed to remove {movie.Title}, would you like to try again?", "Failed to Remove Movie", PromptButtons.RetryCancel, PromptIcons.Warning, Data.Dashboard))
					goto start;
			}
		}

		private static WaitIdentifier RemindLogIdentifier = new WaitIdentifier();

		private static void MovieLoadedMethod(Movie movie)
		{
			MovieLoaded?.Invoke(movie);

			if (RemindLogIdentifier.Enabled && Movies.All(x => x.Loaded))
			{
				RemindLogIdentifier.Wait(() =>
				{
					ISave.Load(out Dictionary<string, DateTime> remindLog, "RemindLog.tf");

					var movs = Movies
						.Where(x => x != null
							&& x.TMDbData.ReleaseDate > DateTime.Today.AddDays(-15)
							&& x.TMDbData.ReleaseDate < DateTime.Today.AddDays(15)
							&& !remindLog.ContainsKey($"M{x.TMDbData.Id}")
							&& Data.Options.EpisodeNotification
							&& !x.VidFile?.Exists == null);

					foreach (var mov in movs)
					{
						Data.Dashboard.TryInvoke(() =>
						{
							Notification.Create(
								(f, x) => PaintEpNotification(f, mov, x)
								, () => { Data.Dashboard.ShowUp(); Data.Dashboard.PushPanel(null, new PC_Download(mov)); }
								, true
								, new Size(400, 80))
								.Show(Data.Dashboard)
								.PictureBox.GetImage(mov.TMDbData.BackdropPath, 200);
						});
						remindLog.Add($"M{mov.TMDbData.Id}", DateTime.Now);
					}

					ISave.Save(remindLog.Where(x => x.Value > DateTime.Now.AddDays(-60)).AsDictionary(), "RemindLog.tf");
				}, 5000);
			}
		}

		private static void PaintEpNotification(PictureBox pb, Movie movie, Graphics g)
		{
			var Width = pb.Width;
			var Height = pb.Height;

			if (pb.Image != null)
				g.DrawBorderedImage(pb.Image, new Rectangle(3, 2, (Height - 5) * 16 / 9, Height - 5), pb.Image.IsAnimated() || pb.Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				g.DrawBorderedImage(Properties.Resources.Big_TV.Color(FormDesign.Design.IconColor), new Rectangle(3, 2, (Height - 5) * 16 / 9, Height - 5), ImageHandler.ImageSizeMode.Center);

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			g.FillEllipse(new SolidBrush(Color.FromArgb(150, FormDesign.Design.ActiveColor)), new RectangleF(7, 6, 22, 22));
			g.DrawImage(Properties.Resources.Tiny_CloudDownload.Color(Color.FromArgb(175, FormDesign.Design.ActiveForeColor)), 10, 8);

			var strFrmt = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
			var font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
			var w = ((Height - 5) * 16 / 9) + 5;
			var h = 3;

			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			var bnds = g.MeasureString(movie.Name, font);

			g.DrawString(movie.Name, font, new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(w, h, Width - w - 20, (int)bnds.Height), strFrmt);

			h += (int)bnds.Height + 2;

			font = new Font("Nirmala UI", 9F);
			g.DrawString(movie.Name, font, new SolidBrush(FormDesign.Design.LabelColor), new Rectangle(w, h, Width - w - 20, (int)bnds.Height), strFrmt);
			bnds = g.MeasureString(movie.Name, font);
			h += (int)bnds.Height + 2;

			font = new Font("Nirmala UI", 8.25F);

			g.DrawString(movie.TMDbData.Tagline, font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = g.MeasureString(movie.TMDbData.Tagline, font);
			h += (int)bnds.Height + 2;

			var txt = "No air date yet";
			if (movie.AirState == AirStateEnum.Aired)
				txt = $"Aired {movie.TMDbData.ReleaseDate?.RelativeString()}";
			else if (movie.AirState == AirStateEnum.ToBeAired)
				txt = $"Airing {movie.TMDbData.ReleaseDate?.RelativeString()}";

			g.DrawString(txt, font, new SolidBrush(FormDesign.Design.InfoColor), w, h);

			font = new Font("Nirmala UI", 6.75F);

			bnds = g.MeasureString("Click to download", font);
			g.DrawString("Click to download", font, new SolidBrush(FormDesign.Design.AccentColor), Width - bnds.Width - 2, Height - bnds.Height - 2);
		}

		public static void Save(Movie movie)
		{
			if (movie.TMDbData != null && movie.TMDbData.Id != 0)
				movie.Save();
		}

		public static void LoadAllMovies()
		{
			foreach (var file in new DirectoryInfo(Path.Combine(ISave.DocsFolder, "MoviesData"))
				.EnumerateFiles("*.scm").Where(x => x.Length > 0))
			{
				var movie = ISave.Load<Movie>($"MoviesData/{file.Name}");

				Add(movie, false);

				if (movie.Loaded)
					MovieLoaded?.Invoke(movie);
			}

			if (Movies.All(x => x.Loaded))
				MovieLoadedMethod(null);

			LocalMovieHandler.Load();

			Data.Preferences.LastMovieCheck = DateTime.Now;
			Data.Preferences.Save();
		}
	}
}