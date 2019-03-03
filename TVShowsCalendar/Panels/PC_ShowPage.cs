using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlickControls.Panels;
using ShowsCalendar.Classes;
using ShowsCalendar.Handlers;
using Extensions;
using ProjectImages = ShowsCalendar.Properties.Resources;
using ShowsCalendar.Controls;
using SlickControls.Forms;
using SlickControls.Classes;
using System.Text.RegularExpressions;

namespace ShowsCalendar.Panels
{
	public partial class PC_ShowPage : PanelContent
	{
		public readonly Show LinkedShow;

		public PC_ShowPage(Show linkedShow) : base(true)
		{
			InitializeComponent();
			LinkedShow = linkedShow;
			Text = linkedShow.Name;

			PB_Top.GetImage(linkedShow.TMDbData.BackdropPath, 300);

			L_InfoNetwork.Text = linkedShow.tMDbData.Networks.Convert(x => x.Name).ListStrings(" • ").IfEmpty("Unknown");
			L_InfoStatus.Text = $"{linkedShow.tMDbData.Status}  •  ";
			L_InfoWebpage.Text = linkedShow.TMDbData.Homepage == null ? "Unknown" : Regex.Match(linkedShow.TMDbData.Homepage, @"([a-z]{1,2}tps?):\/\/((?:(?!(?:\/|#|\?|&)).)+)(?:(\/(?:(?:(?:(?!(?:#|\?|&)).)+\/))?))?(?:((?:(?!(?:\.|$|\?|#)).)+))?(?:(\.(?:(?!(?:\?|$|#)).)+))?(?:(\?(?:(?!(?:$|#)).)+))?(?:(#.+))?").Groups[2].Value.RegexRemove("www\\.");
			TLP_Info.Visible = linkedShow.tMDbData.CreatedBy.Any();

			if (linkedShow.TMDbData.Homepage != null)
				SlickTip.SetTo(L_InfoWebpage, linkedShow.TMDbData.Homepage);

			if (!linkedShow.NextEpisode?.Empty ?? false && linkedShow.NextEpisode.AirState == AirStateEnum.ToBeAired)
				L_InfoStatus.Text += $"Next episode '{linkedShow.NextEpisode}' will air {linkedShow.NextEpisode.TMDbData.AirDate?.RelativeString()}  •  ";

			L_InfoStatus.Text += $"{(linkedShow.TMDbData.FirstAirDate.HasValue ? $"Started in {linkedShow.TMDbData.FirstAirDate?.Year}" : "")} {(linkedShow.TMDbData.LastAirDate.HasValue ? $"{(linkedShow.TMDbData.FirstAirDate.HasValue ? "with its most recent release on " : "")}{linkedShow.TMDbData.LastAirDate?.ToReadableString()}" : "")}";

			if (!string.IsNullOrEmpty(linkedShow.ZooqleURL))
			{
				SL_ZooqleLink.Image = ProjectImages.Tiny_Ok;
				SL_ZooqleLink.Text = "Linked";
				SlickTip.SetTo(SL_ZooqleLink, linkedShow.ZooqleURL);
			}

			if (linkedShow.tMDbData.CreatedBy.Count > 1)
				L_Info.Text += 's';
			if (linkedShow.tMDbData.Networks.Count > 1)
				label2.Text += 's';

			verticalScroll.SizeSource = () => P_Content.Controls.If(x => x.Count == 0, x => 0, x => x.Cast<Control>().Max(X => X.Height + X.Top));

			foreach (var item in linkedShow.Seasons)
			{
				var pb = new PictureBox()
				{
					Size = new Size(110, 183),
					Cursor = Cursors.Hand,
					Margin = new Padding(5, 5, 2, 5),
					Tag = item
				};

				pb.GetImage(item.TMDbData.PosterPath.IfEmpty(linkedShow.TMDbData.PosterPath), 200);
				pb.Paint += Season_PB_Paint;
				pb.MouseClick += (s, e) =>
				{
					if (e.Button == MouseButtons.Right)
						item.ShowStrip();
					else
						SetSeason(item);
				};

				SlickTip.SetTo(pb, item.TMDbData.Name);

				FLP_Seasons.Controls.Add(pb);
			}

			foreach (var item in linkedShow.Cast)
				FLP_Cast.Controls.Add(new CharacterControl(item));

			foreach (var item in linkedShow.Crew)
				FLP_Crew.Controls.Add(new CharacterControl(item));

			foreach (var item in linkedShow.tMDbData.CreatedBy)
				FLP_Info.Controls.Add(new CharacterControl(item));

			if (linkedShow.SimilarShows != null)
				foreach (var item in linkedShow.SimilarShows)
				FLP_Similar.Controls.Add(new MediaViewer(item) { Margin = new Padding(15, 10, 15, 10) });
		}

		protected override bool LoadData()
		{
			if (LinkedShow.Images == null)
				LinkedShow.Images = Data.TMDbHandler.RunTask(c => c.GetTvShowImagesAsync(LinkedShow.tMDbData.Id, "en")).Result;

			return true;
		}

		protected override void OnDataLoad()
		{
			foreach (var item in LinkedShow.Images.Backdrops)
			{
				var width = 265;
				var height = width * item.Height / item.Width;

				var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
				pb.GetImage(item.FilePath, 300);

				FLP_Images.Controls.Add(pb);
			}

			if (FLP_Images.Controls.Count > 0)
				FLP_Images.SetFlowBreak(FLP_Images.Controls.Cast<Control>().Last(), true);

			foreach (var item in LinkedShow.Images.Posters)
			{
				var width = 195;
				var height = width * item.Height / item.Width;

				var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
				pb.GetImage(item.FilePath, 200);

				FLP_Images.Controls.Add(pb);
			}
		}

		private void Season_PB_Paint(object sender, PaintEventArgs e)
		{
			var pb = (PictureBox)sender;

			e.Graphics.Clear(pb.BackColor);

			if (pb.Image != null)
				e.Graphics.DrawBorderedImage(pb.Image, new Rectangle(1, 1, 103, 156), pb.Image.IsAnimated() || pb.Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage(ProjectImages.Big_TV.Color(FormDesign.Design.IconColor), new Rectangle(1, 1, 103, 156), ImageHandler.ImageSizeMode.Center);

			e.Graphics.DrawString((pb.Tag as Season).TMDbData.Name, new Font("Nirmala UI", 8.25F), new SolidBrush(FormDesign.Design.LabelColor), new Rectangle(3, 158, pb.Width - 3, 14), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });

			e.Graphics.DrawString((pb.Tag as Season).Episodes?.FirstOrDefault()?.TMDbData.AirDate?.Year.ToString() ?? "", new Font("Nirmala UI", 6.75F), new SolidBrush(FormDesign.Design.InfoColor), 4, 173);

			var font = new Font("Nirmala UI", 6.75F);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			// New // Unwatched
			if (Data.Options.ShowUnwatchedOnThumb && (pb.Tag as Season).Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired))
			{
				if ((pb.Tag as Season).Episodes.LastThat(x => x.AirState == AirStateEnum.Aired)?.TMDbData != null && !(pb.Tag as Season).Episodes.LastThat(x => x.AirState == AirStateEnum.Aired).Watched && ((pb.Tag as Season).Episodes.LastThat(x => x.AirState == AirStateEnum.Aired).TMDbData.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7))
				{
					var size = e.Graphics.MeasureString("NEW", font);
					var path = new System.Drawing.Drawing2D.GraphicsPath();
					path.AddLine(1, 7, size.Width + 16, 7);
					path.AddLine(size.Width + 23, 7, size.Width + 12, 7 + size.Height + 3);
					path.AddLine(size.Width + 12, 7 + size.Height + 3, 1, 7 + size.Height + 3);

					e.Graphics.FillPath(new SolidBrush(FormDesign.Design.ActiveForeColor), path);

					e.Graphics.DrawLine(new Pen(FormDesign.Design.ActiveColor, 2), size.Width + 23, 7, size.Width + 12, 7 + size.Height + 3);

					e.Graphics.DrawString("NEW", font, new SolidBrush(FormDesign.Design.ActiveColor), 10, 9);
				}
				else
				{
					var txt = $"{(pb.Tag as Season).Episodes.Count(x => !x.Watched)} UNWATCHED";
					var size = e.Graphics.MeasureString(txt, font);
					var path = new System.Drawing.Drawing2D.GraphicsPath();
					path.AddLine(1, 7, size.Width + 16, 7);
					path.AddLine(size.Width + 23, 7, size.Width + 12, 7 + size.Height + 3);
					path.AddLine(size.Width + 12, 7 + size.Height + 3, 1, 7 + size.Height + 3);

					e.Graphics.FillPath(new SolidBrush(FormDesign.Design.MenuColor), path);

					e.Graphics.DrawLine(new Pen(FormDesign.Design.YellowColor, 2), size.Width + 23, 7, size.Width + 12, 7 + size.Height + 3);

					e.Graphics.DrawString(txt, font, new SolidBrush(FormDesign.Design.YellowColor), 10, 9);
				}
			}
			// New // Unwatched
		}

		public PC_ShowPage(Episode episode) : this(episode.Show)
		{
			Load += (s, e) =>
			{
				SetSeason(episode.Season);
				Form.PushPanel(null, new PC_EpisodeView(episode));
			};
		}

		public PC_ShowPage(Season season) : this(season.Show)
		{
			Load += (s, e) =>
			{
				SetSeason(season);
			};
		}

		private void SetSeason(Season season)
		{
			Form.PushPanel(null, new PC_SeasonView(season));
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			P_Container.BackColor = verticalScroll.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 3, -3));
			P_Info.ForeColor = design.LabelColor;

			P_Line_0.BackColor = P_Line1.BackColor = P_Line_2.BackColor =
				 P_Line_3.BackColor = P_Line_4.BackColor = P_Line_5.BackColor = design.AccentColor;

			L_InfoNetwork.ForeColor = L_InfoStatus.ForeColor
				= L_InfoWebpage.ForeColor = design.InfoColor;

			PB_Info1.Color(design.IconColor);
			PB_Info2.Color(design.IconColor);
			PB_Info3.Color(design.IconColor);
			PB_Info4.Color(design.IconColor);
			PB_Info5.Color(design.IconColor);
			PB_Info6.Color(design.IconColor);
		}

		private void PB_Top_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			if (PB_Top.Image != null)
				e.Graphics.DrawBorderedImage(PB_Top.Image, new Rectangle(5, 5, 230, 130), PB_Top.Image.IsAnimated() || PB_Top.Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage(ProjectImages.Huge_TV.Color(FormDesign.Design.IconColor), new Rectangle(5, 5, 230, 130), ImageHandler.ImageSizeMode.Center);

			if (new Rectangle(Width - 45, 0, 32, 32).Contains(PB_Top.PointToClient(MousePosition)))
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_Dots_H).Color(FormDesign.Design.ActiveColor), Width - 45, 0);
			else
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_Dots_H).Color(FormDesign.Design.IconColor), Width - 45, 0);

			if (LinkedShow.GetCurrentWatchEpisode() != null)
			{
				if (new Rectangle(Width - 80, 0, 32, 32).Contains(PB_Top.PointToClient(MousePosition)))
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_PlaySlick).Color(FormDesign.Design.ActiveColor), Width - 80, 0);
				else
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_PlaySlick).Color(FormDesign.Design.IconColor), Width - 80, 0);
			}

			var strFrmt = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
			var font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
			var w = 240;
			var h = 5;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			var bnds = e.Graphics.MeasureString(LinkedShow.TMDbData.Genres.Convert(x => x.Name).ListStrings(" • ").IfEmpty("No Genres"), font);

			e.Graphics.DrawString(LinkedShow.TMDbData.Genres.Convert(x => x.Name).ListStrings(" • ").IfEmpty("No Genres"), font, new SolidBrush(FormDesign.Design.ForeColor), w, h);

			h += (int)bnds.Height + 1;
			w += 1;
			font = new Font("Nirmala UI", 8.25F);

			e.Graphics.DrawString(LinkedShow.Years, font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(LinkedShow.Years, font);
			
			h += (int)bnds.Height + 1;

			var txt = "Unknown Runtime";
			if (LinkedShow.TMDbData.EpisodeRunTime.Any())
				txt = $"{TimeSpan.FromMinutes(LinkedShow.TMDbData.EpisodeRunTime.Average()).ToReadableString()} average runtime";

			e.Graphics.DrawString(txt, font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(txt, font);

			h += (int)bnds.Height + 3;
			w++;

			font = new Font("Nirmala UI", 6.75F);

			e.Graphics.DrawString(LinkedShow.TMDbData.Overview, font, new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, h, PB_Top.Width - w - 20, PB_Top.Height - h - 5), strFrmt);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			font = new Font("Nirmala UI", 8.25F);
			// New // Unwatched
			if (Data.Options.ShowUnwatchedOnThumb && LinkedShow.Episodes.Any(x => !x.Watched && x.AirState == AirStateEnum.Aired))
			{
				if (LinkedShow.LastEpisode?.TMDbData != null && !LinkedShow.LastEpisode.Watched && (LinkedShow.LastEpisode.TMDbData.AirDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7))
				{
					var size = e.Graphics.MeasureString("NEW", font);
					var path = new System.Drawing.Drawing2D.GraphicsPath();
					path.AddLine(5, 12, size.Width + 27, 12);
					path.AddLine(size.Width + 27, 12, size.Width + 16, 12 + size.Height + 3);
					path.AddLine(size.Width + 16, 12 + size.Height + 3, 5, 12 + size.Height + 3);

					e.Graphics.FillPath(new SolidBrush(FormDesign.Design.ActiveForeColor), path);

					e.Graphics.DrawLine(new Pen(FormDesign.Design.ActiveColor, 2), size.Width + 27, 12, size.Width + 16, 12 + size.Height + 3);

					e.Graphics.DrawString("NEW", font, new SolidBrush(FormDesign.Design.ActiveColor), 14, 14);
				}
				else
				{
					txt = $"{LinkedShow.Episodes.Count(x => !x.Watched)} UNWATCHED";
					var size = e.Graphics.MeasureString(txt, font);
					var path = new System.Drawing.Drawing2D.GraphicsPath();
					path.AddLine(5, 12, size.Width + 27, 12);
					path.AddLine(size.Width + 27, 12, size.Width + 16, 12 + size.Height + 3);
					path.AddLine(size.Width + 16, 12 + size.Height + 3, 5, 12 + size.Height + 3);

					e.Graphics.FillPath(new SolidBrush(FormDesign.Design.MenuColor), path);

					e.Graphics.DrawLine(new Pen(FormDesign.Design.YellowColor, 2), size.Width + 27, 12, size.Width + 16, 12 + size.Height + 3);

					e.Graphics.DrawString(txt, font, new SolidBrush(FormDesign.Design.YellowColor), 14, 14);
				}
			}
			// New // Unwatched
		}

		private void PC_ShowPage_Resize(object sender, EventArgs e)
		{
			P_Info.MaximumSize = FLP_Similar.MaximumSize = FLP_Crew.MaximumSize = FLP_Cast.MaximumSize = FLP_Seasons.MaximumSize = P_Content.MaximumSize = new Size(Width, 99999);
			P_Info.MinimumSize = FLP_Similar.MinimumSize = FLP_Crew.MinimumSize = FLP_Cast.MinimumSize = FLP_Seasons.MinimumSize = P_Content.MinimumSize = new Size(Width, 0);
		}

		private void ST_Seasons_TabSelected(object sender, EventArgs e)
		{
			P_Content.Controls.RemoveAt(0);
			P_Content.Controls.Add(FLP_Seasons);
			verticalScroll.Reset();
		}

		private void ST_Cast_TabSelected(object sender, EventArgs e)
		{
			P_Content.Controls.RemoveAt(0);
			P_Content.Controls.Add(FLP_Cast);
			verticalScroll.Reset();
		}

		private void ST_Crew_TabSelected(object sender, EventArgs e)
		{
			P_Content.Controls.RemoveAt(0);
			P_Content.Controls.Add(FLP_Crew);
			verticalScroll.Reset();
		}

		private void ST_Similar_TabSelected(object sender, EventArgs e)
		{
			P_Content.Controls.RemoveAt(0);
			P_Content.Controls.Add(FLP_Similar);
			verticalScroll.Reset();
		}

		private void PB_Top_MouseMove(object sender, MouseEventArgs e)
		{
			PB_Top.Invalidate(new Rectangle(Width - 80, 0, 80, 32));
			PB_Top.Cursor = new Rectangle(Width - 80, 0, 80, 32).Contains(e.Location) ? Cursors.Hand : Cursors.Default;
		}

		private void PB_Top_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && new Rectangle(Width - 40, 0, 32, 32).Contains(e.Location))
				LinkedShow.ShowStrip(true);

			if (e.Button == MouseButtons.Left && new Rectangle(Width - 80, 0, 32, 32).Contains(e.Location) && LinkedShow.GetCurrentWatchEpisode() != null)
				LinkedShow.GetCurrentWatchEpisode().Play();
		}

		private void ST_Info_TabSelected(object sender, EventArgs e)
		{
			P_Content.Controls.RemoveAt(0);
			P_Content.Controls.Add(P_Info);
			verticalScroll.Reset();
		}

		private void SL_ZooqleLink_Click(object sender, EventArgs e)
		{
			if (LinkedShow.ZooqleLink())
			{
				SL_ZooqleLink.Image = ProjectImages.Tiny_Ok;
				SL_ZooqleLink.Text = "Linked";
			}
		}

		private void L_InfoWebpage_MouseEnter(object sender, EventArgs e)
		{
			L_InfoWebpage.Font = new Font(L_InfoWebpage.Font, FontStyle.Underline);
		}

		private void L_InfoWebpage_MouseLeave(object sender, EventArgs e)
		{
			L_InfoWebpage.Font = new Font(L_InfoWebpage.Font, FontStyle.Regular);
		}

		private void L_InfoWebpage_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(LinkedShow.TMDbData.Homepage))
					System.Diagnostics.Process.Start(LinkedShow.TMDbData.Homepage);
			}
			catch { ShowPrompt("Failed to open link.\n\nCheck that you have a default browser selected.", icon: SlickControls.Enums.PromptIcons.Error); }
		}

		private void PB_Stars_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			if (LinkedShow.tMDbData.VoteAverage < 10)
				e.Graphics.DrawImage(ProjectImages.PB_5_Stars.Color(FormDesign.Design.IconColor), 0, 0);

			e.Graphics.DrawImageUnscaledAndClipped(ProjectImages.PB_5_Stars_Filled.Color(LinkedShow.tMDbData.VoteAverage.If(10, FormDesign.Design.ActiveColor, FormDesign.Design.IconColor)), new Rectangle(0, 0, (int)(82 * LinkedShow.tMDbData.VoteAverage / 10), 16));

			e.Graphics.DrawString($"Rated '{Math.Round(LinkedShow.tMDbData.VoteAverage / 2, 2)}' over {LinkedShow.tMDbData.VoteCount} votes", new Font("Nirmala UI", 9F), new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(87, 1, PB_Stars.Width - 90, 13), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}

		private void ST_Images_TabSelected(object sender, EventArgs e)
		{
			P_Content.Controls.RemoveAt(0);
			P_Content.Controls.Add(FLP_Images);
			verticalScroll.Reset();
		}
	}
}
