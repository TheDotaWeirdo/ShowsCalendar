using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.Forms;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Panels;
using ProjectImages = TVShowsCalendar.Properties.Resources;

namespace TVShowsCalendar
{
	public partial class MovieTile : PictureBox
	{
		public Movie LinkedMovie { get; private set; }
		public bool Horizontal { get; private set; }
		public bool DisplayView { get; private set; }

		private bool ImageLoaded;

		public MovieTile(Movie movie, bool horizontal = false, bool displayView = false)
		{
			InitializeComponent();

			LinkedMovie = movie;
			Horizontal = horizontal;
			DisplayView = displayView;
			DoubleBuffered = true;

			if (horizontal)
			{
				Size = new Size(400, 125);
				Margin = new Padding(5);
			}

			LinkedMovie.TMDbDataChanged += LinkedMovie_TMDbDataChanged;
			LinkedMovie_TMDbDataChanged(this, new EventArgs());

			MovieManager.MovieRemoved += MovieManager_MovieRemoved;

			FormDesign.DesignChanged += DesignChanged;
		}

		private void MovieManager_MovieRemoved(Movie show)
		{
			if (show == LinkedMovie)
				Dispose();
		}

		protected void DesignChanged(FormDesign design)
		{
			if (ImageLoaded && Image.Width < 100)
				Image = ProjectImages.Huge_Movie.Color(FormDesign.Design.IconColor);
		}
		
		private void LinkedMovie_TMDbDataChanged(object sender, EventArgs e)
		{
			this.TryInvoke(() =>
			{
				if (string.IsNullOrWhiteSpace(LinkedMovie.TMDbData.BackdropPath))
				{
					Image = ProjectImages.Huge_Movie.Color(FormDesign.Design.IconColor);
					ImageLoaded = true;
					Invalidate();
				}
				else
				{
					if (Horizontal)
					{
						if (this.GetImage(LinkedMovie.TMDbData.PosterPath, 200))
							MovieTile_LoadCompleted(null, null);
					}
					else
					{
						if (this.GetImage(LinkedMovie.TMDbData.BackdropPath, 300))
							MovieTile_LoadCompleted(null, null);
					}
				}
			});
		}
		
		private void MoviePage()
		{
			Data.Dashboard.PushPanel(null, new PC_MoviePage(LinkedMovie));
		}

		private void MovieTile_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			ImageLoaded = true;
		}

		private void MovieTile_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			if (Horizontal)
				PaintHorizontal(e);
			else
				PaintVertical(e);
		}

		private void PaintHorizontal(PaintEventArgs e)
		{
			var imgRect = new Rectangle(1, 1, 82, 123);
			e.Graphics.DrawBorderedImage(Image, imgRect, Image.IsAnimated() || Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);

			if (!DisplayView)
			{
				if (imgRect.Contains(PointToClient(MousePosition)))
				{
					e.Graphics.DrawIconsOverImage(imgRect, PointToClient(MousePosition), ProjectImages.Icon_Info);

					var _bnds = e.Graphics.MeasureString("MOVIE", new Font("Nirmala UI", 6.75F, FontStyle.Bold));
					e.Graphics.DrawString("MOVIE", new Font("Nirmala UI", 6.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), imgRect.Width - _bnds.Width - 3, imgRect.Height - _bnds.Height - 3);
				}

				if (new Rectangle(Width - 20, 6, 16, 16).Contains(PointToClient(MousePosition)))
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_V).Color(FormDesign.Design.ActiveColor), Width - 20, 6);
				else
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_V).Color(FormDesign.Design.IconColor), Width - 20, 6);

				if (LinkedMovie.AirState == AirStateEnum.Aired)
				{
					var vid = LinkedMovie.VidFile?.Exists ?? false;
					var img = vid ? ProjectImages.Tiny_Play : ProjectImages.Tiny_Download;
					var col = vid ? FormDesign.Design.ActiveColor : FormDesign.Design.GreenColor;

					if (new Rectangle(Width - 38, 6, 16, 16).Contains(PointToClient(MousePosition)))
						e.Graphics.DrawImage(new Bitmap(img).Color(col), Width - 38, 6);
					else
						e.Graphics.DrawImage(new Bitmap(img).Color(FormDesign.Design.IconColor), Width - 38, 6);
				}
			}

			var strFrmt = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
			var font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
			var w = 86;
			var h = 4;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			var bnds = e.Graphics.MeasureString(LinkedMovie.Title, font);

			e.Graphics.DrawString(LinkedMovie.Title, font, new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(w, h, Width - w - 40, (int)bnds.Height), strFrmt);

			h += (int)bnds.Height;

			font = new Font("Nirmala UI", 8.25F);

			e.Graphics.DrawString(LinkedMovie.TMDbData.Tagline, font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(LinkedMovie.TMDbData.Tagline, font);

			h += (int)bnds.Height + 1;
			w += 1;

			font = new Font("Nirmala UI", 6.75F);

			var txt = "No release date yet";
			if (LinkedMovie.tMDbData?.ReleaseDate != null)
				txt = (LinkedMovie.tMDbData?.ReleaseDate < DateTime.Today ? "Aired " : "Airing ") + LinkedMovie.tMDbData?.ReleaseDate?.RelativeString();

			e.Graphics.DrawString(txt, font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(txt, font);

			h += (int)bnds.Height + 6;

			e.Graphics.DrawString(LinkedMovie.TMDbData.Overview, font, new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, h, Width - 100, Height - h - 5), strFrmt);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			// New // Unwatched
			if (Data.Options.ShowUnwatchedOnThumb && (bool)(LinkedMovie.TMDbData?.ReleaseDate?.IfNull(false, LinkedMovie.TMDbData.ReleaseDate < DateTime.Today)))
			{
				if (!LinkedMovie.Watched && (LinkedMovie.TMDbData.ReleaseDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7))
					e.Graphics.DrawBannerOverImage(imgRect, "NEW", BannerStyle.Active);
				else if (!LinkedMovie.Watched)
					e.Graphics.DrawBannerOverImage(imgRect, "UNWATCHED", BannerStyle.Yellow);
			}
			// New // Unwatched
		}

		private void PaintVertical(PaintEventArgs e)
		{
			var imgRect = new Rectangle(1, 0, Width - 2, (Width - 2) * 9 / 16);

			if (Image != null)
			{
				if (!Image.IsAnimated() && Image.Width > 100)
					e.Graphics.DrawBorderedImage(Image, imgRect);
				else
					e.Graphics.DrawBorderedImage(Image, imgRect, ImageHandler.ImageSizeMode.Center);
			}
			else
				e.Graphics.DrawBorderedImage(ProjectImages.Huge_Movie.Color(FormDesign.Design.IconColor), imgRect, ImageHandler.ImageSizeMode.Center);

			if (imgRect.Contains(PointToClient(MousePosition)))
			{
				if (LinkedMovie.VidFile?.Exists ?? false)
					e.Graphics.DrawIconsOverImage(imgRect, PointToClient(MousePosition), ProjectImages.Icon_Info, ProjectImages.Icon_PlaySlick);
				else
					e.Graphics.DrawIconsOverImage(imgRect, PointToClient(MousePosition), ProjectImages.Icon_Info);

				var bnds = e.Graphics.MeasureString("MOVIE", new Font("Nirmala UI", 6.75F, FontStyle.Bold));
				e.Graphics.DrawString("MOVIE", new Font("Nirmala UI", 6.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), imgRect.Width - bnds.Width - 3, imgRect.Height - bnds.Height - 3);
			}

			var font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
			var h = imgRect.Height + 3F;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			e.Graphics.DrawString(LinkedMovie.Name, font, new SolidBrush(FormDesign.Design.ForeColor), new RectangleF(3, h, Width - 25, e.Graphics.MeasureString(LinkedMovie.Name, font).Height), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });

			h += e.Graphics.MeasureString(LinkedMovie.Name, font).Height;
			font = new Font("Nirmala UI", 8.25F);

			e.Graphics.DrawString(LinkedMovie.TMDbData.Genres.Convert(x => x.Name).Take(2).ListStrings(" • "), font, new SolidBrush(FormDesign.Design.LabelColor), 3, h);

			h += 2 + e.Graphics.MeasureString(LinkedMovie.TMDbData.Genres.Convert(x => x.Name).Take(2).ListStrings(" • "), font).Height;

			if (LinkedMovie.TMDbData == null)
				e.Graphics.DrawString("Loading..", font, new SolidBrush(FormDesign.Design.InfoColor), 3, h);
			else
				e.Graphics.DrawString($"{LinkedMovie.TMDbData.ReleaseDate.IfNull("No Release Date", LinkedMovie.TMDbData.ReleaseDate?.ToReadableString())}", font, new SolidBrush(FormDesign.Design.InfoColor), 3, h);

			// Dots
			if (new Rectangle(Width - 22, imgRect.Height + 2, 20, 20).Contains(PointToClient(MousePosition)))
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_H).Color(FormDesign.Design.ActiveColor), new PointF(Width - 18, imgRect.Height + 5));
			else
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_H).Color(FormDesign.Design.IconColor), new PointF(Width - 18, imgRect.Height + 5));


			font = new Font("Nirmala UI", 8.25F);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			// New // Unwatched
			if (Data.Options.ShowUnwatchedOnThumb && (bool)(LinkedMovie.TMDbData?.ReleaseDate?.IfNull(false, LinkedMovie.TMDbData.ReleaseDate < DateTime.Today)))
			{
				if (!LinkedMovie.Watched && (LinkedMovie.TMDbData.ReleaseDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7))
					e.Graphics.DrawBannerOverImage(imgRect, "NEW", BannerStyle.Active);
				else if (!LinkedMovie.Watched)
					e.Graphics.DrawBannerOverImage(imgRect, "UNWATCHED", BannerStyle.Yellow);
			}
			// New // Unwatched
		}

		private void MovieTile_MouseClick(object sender, MouseEventArgs e)
		{
			if (DisplayView)
				return;

			if (Horizontal)
			{
				if (new Rectangle(Width - 20, 6, 16, 16).Contains(e.Location))
					LinkedMovie.ShowStrip();
				else if (new Rectangle(1, 1, 82, 123).Contains(e.Location))
				{
					if (e.Button == MouseButtons.Left)
						MoviePage();
					else
						LinkedMovie.ShowStrip();
				}
				else if (new Rectangle(Width - 38, 6, 16, 16).Contains(e.Location) && e.Button == MouseButtons.Left && LinkedMovie.AirState == AirStateEnum.Aired)
				{
					if (LinkedMovie.VidFile?.Exists ?? false)
						LinkedMovie.Play();
					else
						Data.Dashboard.PushPanel(null, new PC_Download(LinkedMovie));
				}
			}
			else
			{
				if (new Rectangle(Width - 22, ((Width - 2) * 9 / 16) + 2, 20, 20).Contains(e.Location))
					LinkedMovie.ShowStrip();
				else if (new Rectangle(1, 0, Width - 2, (Width - 2) * 9 / 16).Contains(e.Location))
				{
					if (e.Button == MouseButtons.Right)
						LinkedMovie.ShowStrip();
					else if (e.Button == MouseButtons.Left)
					{
						if ((LinkedMovie.VidFile?.Exists ?? false) && new Rectangle(1 + (Width - 2) / 2, 0, (Width - 2) / 2, (Width - 2) * 9 / 16).Contains(e.Location))
							LinkedMovie.Play();
						else
							MoviePage();
					}
				}
			}
		}

		private void MovieTile_MouseMove(object sender, MouseEventArgs e)
		{
			if (DisplayView)
				return;

			if (Horizontal)
			{
				if (new Rectangle(Width - 20, 6, 16, 16).Contains(e.Location)
				 || (new Rectangle(Width - 38, 6, 16, 16).Contains(e.Location) && LinkedMovie.AirState == AirStateEnum.Aired)
				 || new Rectangle(1, 1, 82, 123).Contains(e.Location))
				{
					Cursor = Cursors.Hand;
				}
				else
					Cursor = Cursors.Default;
			}
			else
			{
				if (new Rectangle(Width - 22, ((Width - 2) * 9 / 16) + 2, 20, 20).Contains(e.Location)
				 || new Rectangle(3, 3, Width - 5, (Width - 2) * 9 / 16).Contains(e.Location))
				{
						Cursor = Cursors.Hand;
				}
				else
					Cursor = Cursors.Default;
			}

			Invalidate();
		}

		private void MovieTile_MouseEnter(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void MovieTile_MouseLeave(object sender, EventArgs e)
		{
			Invalidate();
		}
	}
}