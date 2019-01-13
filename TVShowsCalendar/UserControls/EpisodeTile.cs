using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Panels;
using ProjectImages = TVShowsCalendar.Properties.Resources;

namespace TVShowsCalendar
{
	public partial class EpisodeTile : PictureBox
	{
		#region Public Fields

		public Episode Episode;

		#endregion Public Fields

		#region Public Properties

		public bool Horizontal { get; private set; }
		public bool DisplayView { get; private set; }
		public bool Hovered { get; private set; }

		#endregion Public Properties

		#region Public Constructors

		public EpisodeTile(Episode episode, bool horizontal = true, bool displayView = false)
		{
			InitializeComponent();

			Horizontal = horizontal;
			DisplayView = displayView;

			if (!horizontal)
			{
				Size = new Size(275, 220);
				Margin = new Padding(7);
			}

			Episode = episode;

			ReloadData();

			ResizeRedraw = true;
			DoubleBuffered = true;

			LocalShowHandler.FolderChanged += LocalShowHandler_FolderChanged;
			Disposed += (s, e) => LocalShowHandler.FolderChanged -= LocalShowHandler_FolderChanged;
		}

		#endregion Public Constructors

		#region Public Methods

		public void Action_Click()
		{
			if (Episode.VidFile?.Exists ?? false)
			{
				Episode.Play(Episode.VidFile);
			}
			else
			{
				Data.Dashboard.PushPanel(null, new PC_Download(Episode));
			}
		}

		public void ReloadData()
		{
			if (!DisplayView)
			{
				if (Horizontal)
					SlickTip.SetTo(this, Episode.Show.Name);
				else
					SlickTip.SetTo(this, Episode.Name);
			}
			this.TryInvoke(() =>
			{
				if (Horizontal)
					this.GetImage(Episode.Season.TMDbData.PosterPath.IfEmpty(Episode.Show.TMDbData.PosterPath), 200);
				else
					this.GetImage(Episode.TMDbData.StillPath.IfEmpty(Episode.Show.TMDbData.BackdropPath), 300);

				LocalShowHandler_FolderChanged(null);
			});
		}

		#endregion Public Methods

		#region Private Methods

		private void EpisodeTile_MouseClick(object sender, MouseEventArgs e)
		{
			if (DisplayView)
				return;

			if (Horizontal)
			{
				if (new Rectangle(Width - 20, 6, 16, 16).Contains(e.Location))
					Episode.ShowStrip();
				else if (new Rectangle(1, 1, 82, 123).Contains(e.Location))
				{
					if (e.Button == MouseButtons.Left)
					{
						Data.Dashboard.PushPanel(null, new PC_ShowPage(Episode));
					}
					else
						Episode.ShowStrip();
				}
				else if (new Rectangle(Width - 38, 6, 16, 16).Contains(e.Location) && e.Button == MouseButtons.Left && Episode.AirState == AirStateEnum.Aired)
					Action_Click();
			}
			else
			{
				if (new Rectangle(Width - 22, ((Width - 2) * 9 / 16) + 2, 20, 20).Contains(e.Location))
					Episode.ShowStrip();
				else if (new Rectangle(3, 3, Width - 5, (Width - 2) * 9 / 16).Contains(e.Location))
				{
					if(e.Button == MouseButtons.Right)
						Episode.ShowStrip();
					else if (e.Button == MouseButtons.Left)
					{
						if(new Rectangle(3 + (Width - 5) / 2, 3, (Width - 5) / 2, (Width - 2) * 9 / 16).Contains(e.Location)
							&& (Episode.VidFile?.Exists ?? false))
							Episode.Play();
						else
							ShowPage();
					}
				}
			}
		}

		private void ShowPage()
		{
			if (Data.Dashboard.CurrentPanel is PC_SeasonView seasonView && seasonView.Season == Episode.Season)
				Data.Dashboard.PushPanel(null, new PC_EpisodeView(Episode));
			else if (!(Data.Dashboard.CurrentPanel is PC_EpisodeView epView && epView.Episode == Episode))
				Data.Dashboard.PushPanel(null, new PC_ShowPage(Episode));
		}

		private void EpisodeTile_MouseMove(object sender, MouseEventArgs e)
		{
			if (DisplayView)
				return;

			Invalidate();

			if (Horizontal)
			{
				if (new Rectangle(Width - 20, 6, 16, 16).Contains(e.Location)
				 || (new Rectangle(Width - 38, 6, 16, 16).Contains(e.Location) && Episode.AirState == AirStateEnum.Aired)
				 || new Rectangle(1, 1, 82, 123).Contains(e.Location))
					Cursor = Cursors.Hand;
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
		}

		private void EpisodeTile_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			if (Horizontal)
				PaintHorizontal(e);
			else
				PaintVertical(e);
		}

		private void LocalShowHandler_FolderChanged(Show show)
		{
			if (show == null || show == Episode.Show)
				this.TryInvoke(Invalidate);
		}

		private void PaintHorizontal(PaintEventArgs e)
		{
			var imgRect = new Rectangle(1, 1, 82, 123);
			if (Image != null)
				e.Graphics.DrawBorderedImage(Image, imgRect, Image.IsAnimated() || Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage(ProjectImages.Big_TV.Color(FormDesign.Design.IconColor), imgRect, ImageHandler.ImageSizeMode.Center);

			if (!DisplayView)
			{
				if (imgRect.Contains(PointToClient(MousePosition)))
				{
					e.Graphics.DrawIconsOverImage(imgRect, PointToClient(MousePosition), ProjectImages.Icon_Info);

					var _bnds = e.Graphics.MeasureString("EPISODE", new Font("Nirmala UI", 6.75F, FontStyle.Bold));
					e.Graphics.DrawString("EPISODE", new Font("Nirmala UI", 6.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), imgRect.Width - _bnds.Width - 3, imgRect.Height - _bnds.Height - 3);
				}

				if (new Rectangle(Width - 20, 6, 16, 16).Contains(PointToClient(MousePosition)))
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_V).Color(FormDesign.Design.ActiveColor), Width - 20, 6);
				else
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_V).Color(FormDesign.Design.IconColor), Width - 20, 6);

				if (Episode.AirState == AirStateEnum.Aired)
				{
					var vid = Episode.VidFile?.Exists ?? false;
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
			var bnds = e.Graphics.MeasureString(Episode.Name, font);

			e.Graphics.DrawString(Episode.Name, font, new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(w, h, Width - w - 40, (int)bnds.Height), strFrmt);

			h += (int)bnds.Height;

			font = new Font("Nirmala UI", 8.25F);

			e.Graphics.DrawString($"Season {Episode.TMDbData.SeasonNumber} • Episode {Episode.TMDbData.EpisodeNumber}", font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString($"Season {Episode.TMDbData.SeasonNumber} • Episode {Episode.TMDbData.EpisodeNumber}", font);

			h += (int)bnds.Height + 1;
			w += 1;

			font = new Font("Nirmala UI", 6.75F);

			var txt = "No air date yet";
			if (Episode.AirState == AirStateEnum.Aired)
				txt = $"Aired {Episode.TMDbData.AirDate?.RelativeString()}";
			else if (Episode.AirState == AirStateEnum.ToBeAired)
				txt = $"Airing {Episode.TMDbData.AirDate?.RelativeString()}";

			e.Graphics.DrawString(txt, font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(txt, font);

			h += (int)bnds.Height + 6;

			e.Graphics.DrawString(Episode.TMDbData.Overview, font, new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, h, Width - 100, Height - h - 5), strFrmt);

			if (Data.Options.ShowUnwatchedOnThumb && !DisplayView && Episode?.TMDbData != null && Episode.AirState == AirStateEnum.Aired)
			{
				if (!Episode.Watched && (Episode.TMDbData.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7))
					e.Graphics.DrawBannerOverImage(imgRect, "NEW", BannerStyle.Active);
				else if(!Episode.Watched && Episode.AirState == AirStateEnum.Aired)
					e.Graphics.DrawBannerOverImage(imgRect, "UNWATCHED", BannerStyle.Yellow);
			}
		}

		private void PaintVertical(PaintEventArgs e)
		{
			var imgRect = new Rectangle(1, 0, Width - 2, (Width - 2) * 9 / 16);

			if (Image != null)
			{
				if (!Image.IsAnimated())
					e.Graphics.DrawBorderedImage(Image, imgRect);
				else
					e.Graphics.DrawBorderedImage(Image, imgRect, ImageHandler.ImageSizeMode.Center);
			}
			else
				e.Graphics.DrawBorderedImage(ProjectImages.Big_TV.Color(FormDesign.Design.IconColor), imgRect, ImageHandler.ImageSizeMode.Center);

			if (imgRect.Contains(PointToClient(MousePosition)))
			{
				if (Episode.VidFile?.Exists ?? false)
					e.Graphics.DrawIconsOverImage(imgRect, PointToClient(MousePosition), ProjectImages.Icon_Info, ProjectImages.Icon_PlaySlick);
				else
					e.Graphics.DrawIconsOverImage(imgRect, PointToClient(MousePosition), ProjectImages.Icon_Info);

				var bnds = e.Graphics.MeasureString("EPISODE", new Font("Nirmala UI", 6.75F, FontStyle.Bold));
				e.Graphics.DrawString("EPISODE", new Font("Nirmala UI", 6.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), imgRect.Width - bnds.Width - 3, imgRect.Height - bnds.Height - 3);
			}

			var font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
			var h = imgRect.Height + 3F;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			e.Graphics.DrawString(Episode.Name, font, new SolidBrush(FormDesign.Design.ForeColor), new RectangleF(3, h, Width - 25, e.Graphics.MeasureString(Episode.Name, font).Height), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });

			h += e.Graphics.MeasureString(Episode.Name, font).Height;
			font = new Font("Nirmala UI", 8.25F);

			e.Graphics.DrawString($"Season {Episode.TMDbData.SeasonNumber} • Episode {Episode.TMDbData.EpisodeNumber}", font, new SolidBrush(FormDesign.Design.LabelColor), 3, h);

			h += 2 + e.Graphics.MeasureString($"Season {Episode.TMDbData.SeasonNumber} • Episode {Episode.TMDbData.EpisodeNumber}", font).Height;

			var txt = "No air date yet";
			if (Episode.AirState == AirStateEnum.Aired)
				txt = $"Aired {Episode.TMDbData.AirDate?.RelativeString()}";
			else if (Episode.AirState == AirStateEnum.ToBeAired)
				txt = $"Airing {Episode.TMDbData.AirDate?.RelativeString()}";

			e.Graphics.DrawString(txt, font, new SolidBrush(FormDesign.Design.InfoColor), 3, h);

			// Dots
			if (new Rectangle(Width - 22, imgRect.Height + 2, 20, 20).Contains(PointToClient(MousePosition)))
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_H).Color(FormDesign.Design.ActiveColor), new PointF(Width - 18, imgRect.Height + 5));
			else
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_H).Color(FormDesign.Design.IconColor), new PointF(Width - 18, imgRect.Height + 5));


			font = new Font("Nirmala UI", 8.25F);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			// New // Unwatched
			if (Data.Options.ShowUnwatchedOnThumb && ((Episode.TMDbData?.AirDate?.IfNull(false, Episode.TMDbData.AirDate < DateTime.Today)) ?? false) && Episode.AirState == AirStateEnum.Aired)
			{
				if (!Episode.Watched && (Episode.TMDbData.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7))
					e.Graphics.DrawBannerOverImage(imgRect, "NEW", BannerStyle.Active);
				else if (!Episode.Watched)
					e.Graphics.DrawBannerOverImage(imgRect, "UNWATCHED", BannerStyle.Yellow);
			}
			// New // Unwatched
		}

		#endregion Private Methods

		private void EpisodeTile_MouseEnter(object sender, EventArgs e)
		{
			Hovered = true;
			Invalidate();
		}

		private void EpisodeTile_MouseLeave(object sender, EventArgs e)
		{
			Hovered = false;
			Invalidate();
		}
	}
}