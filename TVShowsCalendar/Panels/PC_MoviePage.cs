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
	public partial class PC_MoviePage : PanelContent
	{
		public readonly Movie LinkedMovie;

		public PC_MoviePage(Movie linkedMovie) : base(true)
		{
			InitializeComponent();
			LinkedMovie = linkedMovie;
			Text = linkedMovie.Name;

			PB_Top.GetImage(linkedMovie.TMDbData.BackdropPath, 300);

			L_InfoNetwork.Text = linkedMovie.tMDbData.ProductionCompanies.Convert(x => x.Name).ListStrings(" • ").IfEmpty("Unknown");
			L_InfoStatus.Text = linkedMovie.tMDbData.Status;
			L_InfoWebpage.Text = linkedMovie.TMDbData.Homepage == null ? "Unknown" : Regex.Match(linkedMovie.TMDbData.Homepage, @"([a-z]{1,2}tps?):\/\/((?:(?!(?:\/|#|\?|&)).)+)(?:(\/(?:(?:(?:(?!(?:#|\?|&)).)+\/))?))?(?:((?:(?!(?:\.|$|\?|#)).)+))?(?:(\.(?:(?!(?:\?|$|#)).)+))?(?:(\?(?:(?!(?:$|#)).)+))?(?:(#.+))?").Groups[2].Value.RegexRemove("www\\.");
			L_Info_Countries.Text = linkedMovie.tMDbData.ProductionCountries.Convert(x => x.Name).ListStrings(" • ").IfEmpty("Unknown");
			L_InfoLanguage.Text = linkedMovie.tMDbData.SpokenLanguages.Convert(x => x.Name).ListStrings(" • ").IfEmpty("Unknown");

			if (linkedMovie.TMDbData.Homepage != null)
				SlickTip.SetTo(L_InfoWebpage, linkedMovie.TMDbData.Homepage);

			if (linkedMovie.tMDbData.Status != "Canceled")
			{
				switch (linkedMovie.AirState)
				{
					case AirStateEnum.Aired:
						L_InfoStatus.Text += $"  •  Premiered {linkedMovie.tMDbData.ReleaseDate?.RelativeString()}";
						break;
					case AirStateEnum.ToBeAired:
						L_InfoStatus.Text += $"  •  Premiere date set {linkedMovie.tMDbData.ReleaseDate?.RelativeString()}";
						break;
					case AirStateEnum.Unknown:
						L_InfoStatus.Text += $"  •  Unknown premiere date";
						break;
					default:
						break;
				}
			}

			if (linkedMovie.tMDbData.Revenue > 0)
			{
				LabelBudget.Text += " && Revenue";
				L_InfoBudget.Text = $"{linkedMovie.tMDbData.Budget.ToString("C0")} with {linkedMovie.tMDbData.Revenue.ToString("C0")} in revenue";
			}
			else
				L_InfoBudget.Text = linkedMovie.tMDbData.Budget.If(x => x == 0, x => "Unknown", x => x.ToString("C0"));

			verticalScroll.SizeSource = () => P_Content.Controls.If(x => x.Count == 0, x => 0, x => x.Cast<Control>().Max(X => X.Height + X.Top));

			foreach (var item in linkedMovie.Cast)
				FLP_Cast.Controls.Add(new CharacterControl(item));

			foreach (var item in linkedMovie.Crew)
				FLP_Crew.Controls.Add(new CharacterControl(item));

			if (linkedMovie.SimilarMovies != null)
				foreach (var item in linkedMovie.SimilarMovies)
					FLP_Similar.Controls.Add(new MediaViewer(item) { Margin = new Padding(15, 10, 15, 10) });

			foreach (var item in linkedMovie.VidFiles)
				FLP_Files.Controls.Add(new VideoFileControl(item, linkedMovie));
		}

		protected override bool LoadData()
		{
			if (LinkedMovie.Images == null)
				LinkedMovie.Images = Data.TMDbHandler.RunTask(c => c.GetMovieImagesAsync(LinkedMovie.tMDbData.Id, "en")).Result;

			return true;
		}

		protected override void OnDataLoad()
		{
			foreach (var item in LinkedMovie.Images.Backdrops)
			{
				var width = 265;
				var height = width * item.Height / item.Width;

				var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
				pb.GetImage(item.FilePath, 300);

				FLP_Images.Controls.Add(pb);
			}

			if (FLP_Images.Controls.Count > 0)
				FLP_Images.SetFlowBreak(FLP_Images.Controls.Cast<Control>().Last(), true);

			foreach (var item in LinkedMovie.Images.Posters)
			{
				var width = 195;
				var height = width * item.Height / item.Width;

				var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
				pb.GetImage(item.FilePath, 200);

				FLP_Images.Controls.Add(pb);
			}
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			P_Container.BackColor = verticalScroll.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 3, -3));
			P_Info.ForeColor = design.LabelColor;

			P_Line_0.BackColor = P_Line_2.BackColor = P_Line1.BackColor = P_Line_7.BackColor =
				 P_Line_3.BackColor = P_Line_4.BackColor = P_Line_5.BackColor = design.AccentColor;

			L_InfoNetwork.ForeColor = L_InfoStatus.ForeColor
				= L_InfoWebpage.ForeColor = L_InfoBudget.ForeColor = L_Info_Countries.ForeColor = design.InfoColor;

			PB_Info1.Color(design.IconColor);
			PB_Info2.Color(design.IconColor);
			PB_Info3.Color(design.IconColor);
			PB_Info4.Color(design.IconColor);
			PB_Info5.Color(design.IconColor);
			PB_Info6.Color(design.IconColor);
			PB_Info7.Color(design.IconColor);
		}

		private void PB_Top_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			if (PB_Top.Image != null)
				e.Graphics.DrawBorderedImage(PB_Top.Image, new Rectangle(5, 5, 230, 130), PB_Top.Image.IsAnimated() || PB_Top.Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage(ProjectImages.Huge_Movie.Color(FormDesign.Design.IconColor), new Rectangle(5, 5, 230, 130), ImageHandler.ImageSizeMode.Center);

			if (new Rectangle(Width - 40, 0, 32, 32).Contains(PB_Top.PointToClient(MousePosition)))
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_Dots_H).Color(FormDesign.Design.ActiveColor), Width - 40, 0);
			else
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_Dots_H).Color(FormDesign.Design.IconColor), Width - 40, 0);

			var strFrmt = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
			var font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
			var w = 240;
			var h = 5;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			var bnds = e.Graphics.MeasureString(LinkedMovie.TMDbData.Genres.Convert(x => x.Name).ListStrings(" • ").IfEmpty("No Genres"), font);

			e.Graphics.DrawString(LinkedMovie.TMDbData.Genres.Convert(x => x.Name).ListStrings(" • ").IfEmpty("No Genres"), font, new SolidBrush(FormDesign.Design.ForeColor), w, h);

			h += (int)bnds.Height + 1;
			w += 1;
			font = new Font("Nirmala UI", 8.25F);

			e.Graphics.DrawString(LinkedMovie.tMDbData.Tagline, font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(LinkedMovie.tMDbData.Tagline, font);
			
			h += (int)bnds.Height + 1;

			var txt = "Unknown Runtime";
			if (LinkedMovie.TMDbData.Runtime != null)
				txt = $"{TimeSpan.FromMinutes((int)LinkedMovie.TMDbData.Runtime).ToReadableString()} average runtime";

			e.Graphics.DrawString(txt, font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(txt, font);

			h += (int)bnds.Height + 3;
			w++;

			font = new Font("Nirmala UI", 6.75F);

			e.Graphics.DrawString(LinkedMovie.TMDbData.Overview, font, new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, h, PB_Top.Width - w - 20, PB_Top.Height - h - 5), strFrmt);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			font = new Font("Nirmala UI", 8.25F);
			// New // Unwatched
			if (Data.Options.ShowUnwatchedOnThumb && (bool)(LinkedMovie.TMDbData?.ReleaseDate?.IfNull(false, LinkedMovie.TMDbData.ReleaseDate < DateTime.Today)))
			{
				if (!LinkedMovie.Watched && (LinkedMovie.TMDbData.ReleaseDate ?? DateTime.MinValue) > DateTime.Today.AddDays(-7))
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
					txt = $"UNWATCHED";
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

		private void PC_MoviePage_Resize(object sender, EventArgs e)
		{
			FLP_Images.MaximumSize = P_Info.MaximumSize = FLP_Similar.MaximumSize = FLP_Crew.MaximumSize = FLP_Cast.MaximumSize = P_Content.MaximumSize = new Size(Width, 99999);
			FLP_Images.MinimumSize = P_Info.MinimumSize = FLP_Similar.MinimumSize = FLP_Crew.MinimumSize = FLP_Cast.MinimumSize = P_Content.MinimumSize = new Size(Width, 0);
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
			PB_Top.Invalidate(new Rectangle(Width - 40, 0, 32, 32));
			PB_Top.Cursor = new Rectangle(Width - 40, 0, 32, 32).Contains(e.Location) ? Cursors.Hand : Cursors.Default;
		}

		private void PB_Top_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && new Rectangle(Width - 40, 0, 32, 32).Contains(e.Location))
				LinkedMovie.ShowStrip();
		}

		private void ST_Info_TabSelected(object sender, EventArgs e)
		{
			P_Content.Controls.RemoveAt(0);
			P_Content.Controls.Add(P_Info);
			verticalScroll.Reset();
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
				if (!string.IsNullOrWhiteSpace(LinkedMovie.TMDbData.Homepage))
					System.Diagnostics.Process.Start(LinkedMovie.TMDbData.Homepage);
			}
			catch { ShowPrompt("Failed to open link.\n\nCheck that you have a default browser selected.", icon: SlickControls.Enums.PromptIcons.Error); }
		}

		private void PB_Stars_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			if (LinkedMovie.tMDbData.VoteAverage < 10)
				e.Graphics.DrawImage(ProjectImages.PB_5_Stars.Color(FormDesign.Design.IconColor), 0, 0);

			e.Graphics.DrawImageUnscaledAndClipped(ProjectImages.PB_5_Stars_Filled.Color(LinkedMovie.tMDbData.VoteAverage.If(10, FormDesign.Design.ActiveColor, FormDesign.Design.IconColor)), new Rectangle(0, 0, (int)(82 * LinkedMovie.tMDbData.VoteAverage / 10), 16));

			e.Graphics.DrawString($"Rated '{Math.Round(LinkedMovie.tMDbData.VoteAverage / 2, 2)}' over {LinkedMovie.tMDbData.VoteCount} votes", new Font("Nirmala UI", 9F), new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(87, 1, PB_Stars.Width - 90, 13), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}

		private void ST_Images_TabSelected(object sender, EventArgs e)
		{
			P_Content.Controls.RemoveAt(0);
			P_Content.Controls.Add(FLP_Images);
			verticalScroll.Reset();
		}

		private void ST_Files_TabSelected(object sender, EventArgs e)
		{
			P_Content.Controls.RemoveAt(0);
			P_Content.Controls.Add(FLP_Files);
			verticalScroll.Reset();
		}
	}
}
