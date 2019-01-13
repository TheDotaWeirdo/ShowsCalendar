using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Forms;
using SlickControls.Panels;
using TVShowsCalendar.Classes;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Panels;
using ProjectImages = TVShowsCalendar.Properties.Resources;

namespace TVShowsCalendar
{
	public partial class ShowTile : PictureBox
	{
		private bool ImageLoaded;

		public ShowTile(Show show)
		{
			InitializeComponent();

			LinkedShow = show;

			DoubleBuffered = true;

			LinkedShow.TMDbDataChanged += LinkedShow_TMDbDataChanged;
			LinkedShow_TMDbDataChanged(this, new EventArgs());

			ShowManager.ShowRemoved += ShowManager_ShowRemoved;

			FormDesign.DesignChanged += DesignChanged;
		}

		private void ShowManager_ShowRemoved(Show show)
		{
			if (show == LinkedShow)
				Dispose();
		}

		public Show LinkedShow { get; private set; }

		protected void DesignChanged(FormDesign design)
		{
			if (ImageLoaded && Image.Width < 100)
				Image = ProjectImages.Huge_TV.Color(FormDesign.Design.IconColor);
		}
		
		private void LinkedShow_TMDbDataChanged(object sender, EventArgs e)
		{
			this.TryInvoke(() =>
			{
				if (string.IsNullOrWhiteSpace(LinkedShow.TMDbData.BackdropPath))
				{
					Image = ProjectImages.Huge_TV.Color(FormDesign.Design.IconColor);
					ImageLoaded = true;
					Invalidate();
				}
				else
				{
					if (this.GetImage(LinkedShow.TMDbData.BackdropPath, 300))
						ShowTile_LoadCompleted(null, null);
				}
			});
		}

		private void ShowPage()
		{
			Data.Dashboard.PushPanel(null, new PC_ShowPage(LinkedShow));
		}

		private void ShowTile_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			ImageLoaded = true;
		}

		private void ShowTile_MouseClick(object sender, MouseEventArgs e)
		{
			if (new Rectangle(Width - 22, ((Width - 2) * 9 / 16) + 2, 20, 20).Contains(e.Location))
				LinkedShow.ShowStrip();
			else if(new Rectangle(1, 0, Width - 2, (Width - 2) * 9 / 16).Contains(e.Location))
			{
				if (e.Button == MouseButtons.Right)
					LinkedShow.ShowStrip();
				else if (e.Button == MouseButtons.Left)
				{
					if ((LinkedShow.GetCurrentWatchEpisode()?.VidFile?.Exists ?? false) && new Rectangle(1 + (Width - 2) / 2, 0, (Width - 2) / 2, (Width - 2) * 9 / 16).Contains(e.Location))
						LinkedShow.GetCurrentWatchEpisode().Play();
					else
						ShowPage();
				}
			}
		}

		private void ShowTile_MouseEnter(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void ShowTile_MouseLeave(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void ShowTile_MouseMove(object sender, MouseEventArgs e)
		{			
			if (new Rectangle(Width - 22, ((Width - 2) * 9 / 16) + 2, 20, 20).Contains(e.Location)
				|| new Rectangle(3, 3, Width - 5, (Width - 2) * 9 / 16).Contains(e.Location))
			{
				Cursor = Cursors.Hand;
			}
			else
			{
				Cursor = Cursors.Default;
			}

			Invalidate();
		}

		private void ShowTile_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			var imgRect = new Rectangle(1, 0, Width - 2, (Width - 2) * 9 / 16);

			if (Image != null)
			{
				if (!Image.IsAnimated() && Image.Width > 100)
					e.Graphics.DrawBorderedImage(Image, imgRect);
				else
					e.Graphics.DrawBorderedImage(Image, imgRect, ImageHandler.ImageSizeMode.Center);
			}
			else
				e.Graphics.DrawBorderedImage(ProjectImages.Huge_TV.Color(FormDesign.Design.IconColor), imgRect, ImageHandler.ImageSizeMode.Center);

			if (imgRect.Contains(PointToClient(MousePosition)))
			{
				if (LinkedShow.GetCurrentWatchEpisode()?.VidFile?.Exists ?? false)
					e.Graphics.DrawIconsOverImage(imgRect, PointToClient(MousePosition), ProjectImages.Icon_Info, ProjectImages.Icon_PlaySlick);
				else
					e.Graphics.DrawIconsOverImage(imgRect, PointToClient(MousePosition), ProjectImages.Icon_Info);

				var bnds = e.Graphics.MeasureString("TV SHOW", new Font("Nirmala UI", 6.75F, FontStyle.Bold));
				e.Graphics.DrawString("TV SHOW", new Font("Nirmala UI", 6.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), imgRect.Width - bnds.Width - 3, imgRect.Height - bnds.Height - 3);
			}

			var font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
			var h = imgRect.Height + 3F;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			e.Graphics.DrawString(LinkedShow.Name, font, new SolidBrush(FormDesign.Design.ForeColor), new RectangleF(3, h, Width - 25, e.Graphics.MeasureString(LinkedShow.Name, font).Height), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });

			h += e.Graphics.MeasureString(LinkedShow.Name, font).Height;
			font = new Font("Nirmala UI", 8.25F);

			e.Graphics.DrawString(LinkedShow.TMDbData.Genres.Convert(x => x.Name).Take(2).ListStrings(" • "), font, new SolidBrush(FormDesign.Design.LabelColor), 3, h);

			h += 2 + e.Graphics.MeasureString(LinkedShow.TMDbData.Genres.Convert(x => x.Name).Take(2).ListStrings(" • "), font).Height;

			if (!LinkedShow.Episodes?.Any() ?? true)
				e.Graphics.DrawString("Loading..", font, new SolidBrush(FormDesign.Design.InfoColor), 3, h);
			else
				e.Graphics.DrawString(LinkedShow.Years, font, new SolidBrush(FormDesign.Design.InfoColor), 3, h);

			// Dots
			if (new Rectangle(Width - 22, imgRect.Height + 2, 20, 20).Contains(PointToClient(MousePosition)))
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_H).Color(FormDesign.Design.ActiveColor), new PointF(Width - 18, imgRect.Height + 5));
			else
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_H).Color(FormDesign.Design.IconColor), new PointF(Width - 18, imgRect.Height + 5));

			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			if (Data.Options.ShowUnwatchedOnThumb && LinkedShow.Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired))
			{
				if (LinkedShow.LastEpisode?.TMDbData != null && !LinkedShow.LastEpisode.Watched && (LinkedShow.LastEpisode.TMDbData.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7))
					e.Graphics.DrawBannerOverImage(imgRect, "NEW", BannerStyle.Active);
				else
					e.Graphics.DrawBannerOverImage(imgRect, $"{LinkedShow.Episodes.Count(x => !x.Watched)} UNWATCHED", BannerStyle.Yellow);
			}
		}
	}
}