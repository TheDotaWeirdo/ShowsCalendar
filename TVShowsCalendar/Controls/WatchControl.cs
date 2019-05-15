using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using ShowsCalendar.Classes;
using ShowsCalendar.Handlers;
using ProjectImages = ShowsCalendar.Properties.Resources;
using ShowsCalendar.Panels;

namespace ShowsCalendar.Controls
{
	public partial class WatchControl : PictureBox
	{
		private bool OnDeck;

		public Episode Episode { get; private set; }
		public string BannerName { get; set; }
		public Movie Movie { get; private set; }

		public WatchControl(Episode ep, bool onDeck = true, string bannerName = null)
		{
			InitializeComponent();
			Episode = ep;
			BannerName = bannerName;
			OnDeck = onDeck;

			if (!onDeck)
			{
				SlickTip.SetTo(this, ep.Name);

				Size = new Size(135, 250);
				this.GetImage(ep.Season.TMDbData.PosterPath.IfEmpty(ep.Show.TMDbData.PosterPath), 200, ProjectImages.Huge_Play.Color(FormDesign.Design.IconColor));

			}
			else
				this.GetImage(ep.TMDbData.StillPath.IfEmpty(ep.Show.TMDbData.BackdropPath), 300, ProjectImages.Huge_Play.Color(FormDesign.Design.IconColor));

			ep.EpisodeWatchTimeChanged += Ep_EpisodeWatchTimeChanged;
			Disposed += (s, e) => ep.EpisodeWatchTimeChanged -= Ep_EpisodeWatchTimeChanged;
		}

		private void Ep_EpisodeWatchTimeChanged(double obj)
			=> this.TryInvoke(() =>
			{
				Invalidate();
				Parent.Controls.SetChildIndex(this, 0);
			});

		public WatchControl(Movie mov, bool onDeck = true, string bannerName = null)
		{
			InitializeComponent();
			Movie = mov;
			BannerName = bannerName;
			OnDeck = onDeck;

			if (!onDeck)
			{
				SlickTip.SetTo(this, mov.Title);

				Size = new Size(135, 250);
				this.GetImage(mov.TMDbData.PosterPath, 200, ProjectImages.Big_Play.Color(FormDesign.Design.IconColor));

			}
			else
				this.GetImage(mov.TMDbData.BackdropPath, 300, ProjectImages.Big_Play.Color(FormDesign.Design.IconColor));
		}

		private void Master_Click(object sender, MouseEventArgs e)
		{
			if (OnDeck)
			{
				if (new Rectangle(3, 3, Width - 5, (Width - 2) * 9 / 16).Contains(e.Location))
				{
					if (e.Button == MouseButtons.Left)
						Play();
					else
						ShowStrip();
				}
				else if (new Rectangle(Width - 22, ((Width - 2) * 9 / 16) + 2, 20, 20).Contains(e.Location))
					ShowStrip();
			}
			else
			{
				if (new Rectangle(3, 3, Width - 5, (Width - 2) * 3 / 2).Contains(e.Location))
				{
					if (e.Button == MouseButtons.Left)
						Play();
					else
						ShowStrip();
				}
				else if (new Rectangle(Width - 22, ((Width - 2) * 3 / 2) + 2, 20, 20).Contains(e.Location))
					ShowStrip();
			}
		}

		private void Play()
		{
			Cursor.Current = Cursors.WaitCursor;
			if (Episode != null)
			{
				try
				{
					if (Episode.VidFiles.Any(y => y.Exists))
						Episode.Play();
					else
						Data.Mainform.PushPanel(null, new PC_Download(Episode));
				}
				catch (Exception) { Cursor.Current = Cursors.Default; MessagePrompt.Show($"Could not play the file.\nCheck that you have a default app selected for video files.", "No Supported Player", PromptButtons.OK, PromptIcons.Error); }
			}
			else
			{
				try
				{
					if (Movie.VidFiles.Any(y => y.Exists))
						Movie.Play();
					else
						Data.Mainform.PushPanel(null, new PC_Download(Movie));
				}
				catch (Exception) { Cursor.Current = Cursors.Default; MessagePrompt.Show($"Could not play the file.\nCheck that you have a default app selected for video files.", "No Supported Player", PromptButtons.OK, PromptIcons.Error); }
			}
			Cursor.Current = Cursors.Default;
		}

		private void ShowStrip()
		{
			if (Episode != null)
				Episode.ShowStrip();
			else
				Movie.ShowStrip();
		}

		private void WatchControl_Paint(object sender, PaintEventArgs e)
		{
			var playable = (Episode?.VidFiles.Any(y => y.Exists) ?? false) || (Movie?.VidFiles.Any(y => y.Exists) ?? false);
			var text = Episode != null ? Episode.Name : Movie.Name;
			var subText = Episode != null ? $"Season {Episode.TMDbData.SeasonNumber} • Episode {Episode.TMDbData.EpisodeNumber}" : Movie.TMDbData.Tagline;
			var infoText = Episode != null ? Episode.TMDbData.AirDate?.ToReadableString() : Movie.TMDbData.ReleaseDate?.ToReadableString();

			e.Graphics.Clear(BackColor);
			var imgRect = OnDeck ? new Rectangle(1, 0, Width - 2, (Width - 2) * 9 / 16)
										: new Rectangle(1, 0, Width - 2, (Width - 2) * 3 / 2);

			if (Image != null)
			{
				if (Image.Width >= Width)
					e.Graphics.DrawBorderedImage(Image, imgRect);
				else
					e.Graphics.DrawBorderedImage(Image, imgRect, ImageHandler.ImageSizeMode.Center);
			}
			else
				e.Graphics.DrawBorderedImage(ProjectImages.Huge_Play.Color(FormDesign.Design.IconColor), imgRect, ImageHandler.ImageSizeMode.Center);

			if (Enabled && imgRect.Contains(PointToClient(MousePosition)))
				e.Graphics.DrawIconsOverImage(imgRect, PointToClient(MousePosition), playable ? ProjectImages.Icon_PlaySlick : ProjectImages.Huge_Download);

			var h = 0F;
			if (Episode != null)
			{
				if (Episode.Started && Enabled)
				{
					var msecs = Episode.WatchTime * (100 - Episode.Progress) / Episode.Progress;
					if (msecs != 0 && !double.IsInfinity(msecs))
					{
						infoText = TimeSpan.FromMilliseconds(msecs).ToReadableString() + " left";
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(175, FormDesign.Design.AccentColor))
							, new Rectangle(4, imgRect.Height - 7, imgRect.Width - 5, 5));
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, FormDesign.Design.ActiveColor))
							, new RectangleF(4, imgRect.Height - 7, (float)((imgRect.Width - 5) * Episode.Progress / 100), 5));

						h = 5;
					}
				}
			}
			else
			{
				if (Movie.Started && Enabled)
				{
					var msecs = Movie.WatchTime * (100 - Movie.Progress) / Movie.Progress;
					if (msecs != 0 && !double.IsInfinity(msecs))
					{
						infoText = TimeSpan.FromMilliseconds(msecs).ToReadableString() + " left";
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(175, FormDesign.Design.AccentColor))
							, new Rectangle(4, imgRect.Height - 7, imgRect.Width - 5, 5));
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, FormDesign.Design.ActiveColor))
							, new RectangleF(4, imgRect.Height - 7, (float)((imgRect.Width - 5) * Movie.Progress / 100), 5));

						h = 5;
					}
				}
			}

			var bnds = e.Graphics.MeasureString($"{playable.If("PLAY", "DOWNLOAD")} {(Movie == null ? "EPISODE" : "MOVIE")}", new Font("Nirmala UI", 6.75F, FontStyle.Bold));
			if (Enabled && imgRect.Contains(PointToClient(MousePosition)))
			{
				if (OnDeck && Episode != null)
					e.Graphics.DrawString(Episode.Show.Name.ToUpper(), new Font("Nirmala UI", 6.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), imgRect.X + 6, imgRect.Height - bnds.Height - 3 - h);
				e.Graphics.DrawString($"{playable.If("PLAY", "DOWNLOAD")} {(Movie == null ? "EPISODE" : "MOVIE")}", new Font("Nirmala UI", 6.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), imgRect.Width - bnds.Width - 6, imgRect.Height - bnds.Height - 3 - h);
			}
			var strFormat = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
			var font = OnDeck ? new Font("Nirmala UI", 9.75F, FontStyle.Bold) : new Font("Nirmala UI", 8.25F, FontStyle.Bold);
			h = imgRect.Height + 3F;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			bnds = e.Graphics.MeasureString(text, font);
			e.Graphics.DrawString(text, font, new SolidBrush(FormDesign.Design.ForeColor), new RectangleF(3, h, Width - 25, bnds.Height), strFormat);

			h += bnds.Height - 1;
			font = OnDeck ? new Font("Nirmala UI", 8.25F) : new Font("Nirmala UI", 6.75F);

			bnds = e.Graphics.MeasureString(subText, font);
			e.Graphics.DrawString(subText, font, new SolidBrush(FormDesign.Design.LabelColor), new RectangleF(3, h, Width - 6, bnds.Height), strFormat);

			h += bnds.Height;

			e.Graphics.DrawString(infoText, font, new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(3, h, Width - 6, bnds.Height), strFormat);

			// Dots
			if (Enabled)
			{
				if (new Rectangle(Width - 22, imgRect.Height + 2, 20, 20).Contains(PointToClient(MousePosition)))
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_H).Color(FormDesign.Design.ActiveColor), new PointF(Width - 18, imgRect.Height + 4));
				else
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Dots_H).Color(FormDesign.Design.IconColor), new PointF(Width - 18, imgRect.Height + 4));
			}

			font = new Font("Nirmala UI", 9F);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			var rating = Episode != null ? Episode.TMDbData.VoteAverage : Movie.TMDbData.VoteAverage;
			var votes = Episode != null ? Episode.TMDbData.VoteCount : Movie.TMDbData.VoteCount;

			var tab = 0;

			if (!string.IsNullOrWhiteSpace(BannerName))
				e.Graphics.DrawBannerOverImage(imgRect, BannerName, BannerStyle.Text, 0, tab++);

			// New // Unwatched
			if (Episode != null && Episode.Season.Episodes.Last() == Episode)
				e.Graphics.DrawBannerOverImage(imgRect, "SEASON FINALE", BannerStyle.Active, 0, tab++);

			if (votes > 0 && Data.Options.ShowUnwatchedOnThumb)
			{
				var style = rating.If(x => x >= 7.5, BannerStyle.Green, rating.If(y => y < 2.5, BannerStyle.Red, BannerStyle.Yellow));
				var rect = e.Graphics.DrawBannerOverImage(imgRect, rating.ToString("0.##"), style, 16, tab);
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Star).Color(style.ForeColor()), rect.X + rect.Width - 9 - 16, rect.Center(new Size(16,16)).Y+1);
			}
		}

		private void WatchControl_MouseEnter(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void WatchControl_MouseLeave(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void WatchControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (OnDeck)
			{
				if (new Rectangle(Width - 22, ((Width - 2) * 9 / 16) + 2, 20, 20).Contains(e.Location)
				 || new Rectangle(3, 3, Width - 5, (Width - 2) * 9 / 16).Contains(e.Location))
				{
					Cursor = Cursors.Hand;
				}
				else
					Cursor = Cursors.Default;
			}
			else
			{
				if (new Rectangle(Width - 22, ((Width - 2) * 3 / 2) + 2, 20, 20).Contains(e.Location)
				 || new Rectangle(3, 3, Width - 5, (Width - 2) * 3 / 2).Contains(e.Location))
				{
					Cursor = Cursors.Hand;
				}
				else
					Cursor = Cursors.Default;
			}

			Invalidate();
		}
	}
}