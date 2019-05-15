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
using Extensions;
using ShowsCalendar.Handlers;
using ProjectImages = ShowsCalendar.Properties.Resources;
using ShowsCalendar.Controls;
using SlickControls.Forms;
using SlickControls.Classes;

namespace ShowsCalendar.Panels
{
	public partial class PC_SeasonView : PanelContent
	{
		public Season Season { get; }

		public PC_SeasonView(Season season, Episode episode = null)
		{
			InitializeComponent();

			Season = season;

			Text = season.Show.Name;
			PB_SeasonInfo.GetImage(season.TMDbData.PosterPath.IfEmpty(season.Show.TMDbData.PosterPath), 200);

			SL_Backwards.Enabled = season.Previous != null;
			SL_Forward.Enabled = season.Next != null;

			SlickTip.SetTo(SL_Backwards, "Previous Season");
			SlickTip.SetTo(SL_Forward, "Next Season");

			foreach (var item in season.Episodes)
				FLP_Episodes.Controls.Add(new EpisodeTile(item, false));
			
			if (season.Images == null)
				StartDataLoad();

			if(episode != null)
			{
				Load += (s, e) =>
				{
					var tile = FLP_Episodes.Controls.OfType<EpisodeTile>().FirstThat(x => x.Episode == episode);
					if (tile != null)
						verticalScroll.SetPercentage(tile, true);
				};
			}
		}

		protected override bool LoadData()
		{
			if (Season.Images == null && ConnectionHandler.IsConnected)
				Season.Images = Data.TMDbHandler.RunTask(x => x.GetTvSeasonImagesAsync(Season.Show.Id, Season.SeasonNumber, "en")).Result;

			return ConnectionHandler.IsConnected;
		}

		protected override void OnDataLoad()
		{
			if (ST_Images.Selected)
				ST_Images_TabSelected(null, null);
		}

		private void PC_SeasonView_Resize(object sender, EventArgs e)
		{
			FLP_Episodes.MaximumSize = new Size(Width, 99999);
			FLP_Episodes.MinimumSize = new Size(Width, 0);
		}

		private void PB_SeasonInfo_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			if (PB_SeasonInfo.Image != null)
				e.Graphics.DrawBorderedImage(PB_SeasonInfo.Image, new Rectangle(8, 1, (130) * 2 / 3, 130), PB_SeasonInfo.Image.IsAnimated() || PB_SeasonInfo.Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage(ProjectImages.Big_TV.Color(FormDesign.Design.IconColor), new Rectangle(8, 1, (130) * 2 / 3, 130), ImageHandler.ImageSizeMode.Center);
			
			if (new Rectangle(Width - 45, 0, 32, 32).Contains(PB_SeasonInfo.PointToClient(MousePosition)))
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_Dots_H).Color(FormDesign.Design.ActiveColor), Width - 45, 0);
			else
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_Dots_H).Color(FormDesign.Design.IconColor), Width - 45, 0);

			if (Season.Show.GetCurrentWatchEpisode(Season.SeasonNumber) != null)
			{
				if (new Rectangle(Width - 80, 0, 32, 32).Contains(PB_SeasonInfo.PointToClient(MousePosition)))
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_PlaySlick).Color(FormDesign.Design.ActiveColor), Width - 80, 0);
				else
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_PlaySlick).Color(FormDesign.Design.IconColor), Width - 80, 0);
			}

			var w = 12 + ((130) * 2 / 3);
			var h = 2f;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			e.Graphics.DrawString(Season.TMDbData.Name.RemoveDoubleSpaces(), new Font("Nirmala UI", 9.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), w, h);
			var bnds = e.Graphics.MeasureString(Season.TMDbData.Name.RemoveDoubleSpaces(), new Font("Nirmala UI", 9.75F, FontStyle.Bold));

			h += bnds.Height + 2;

			e.Graphics.DrawString(Season.TMDbData.AirDate?.ToReadableString() ?? "No air date", new Font("Nirmala UI", 9F), new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(Season.TMDbData.AirDate?.ToReadableString() ?? "No air date", new Font("Nirmala UI", 9F));

			h += bnds.Height + 2;

			e.Graphics.DrawString($"{Season.Episodes.Count} Episodes", new Font("Nirmala UI", 8.25F), new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString($"{Season.Episodes.Count} Episodes", new Font("Nirmala UI", 8.25F));

			h += bnds.Height + 2;
			w += 2;

			e.Graphics.DrawString(Season.TMDbData.Overview.IfEmpty("No overview"), new Font("Nirmala UI", 6.75F), new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, h, PB_SeasonInfo.Width - w - 5, PB_SeasonInfo.Height - h - 5), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}

		private void ST_Episodes_TabSelected(object sender, EventArgs e)
		{
			FLP_Episodes.Controls.Clear(true);	

			foreach (var item in Season.Episodes)
				FLP_Episodes.Controls.Add(new EpisodeTile(item, false));
		}

		private void ST_Images_TabSelected(object sender, EventArgs e)
		{
			FLP_Episodes.Controls.Clear(true);

			if (Season.Images != null)
				foreach (var item in Season.Images.Posters)
				{
					var width = 200;
					var height = width * item.Height / item.Width;

					var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
					pb.GetImage(item.FilePath, 200);

					FLP_Episodes.Controls.Add(pb);
				}
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			panel1.BackColor = verticalScroll.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 3, -3));
		}

		private void PB_SeasonInfo_MouseMove(object sender, MouseEventArgs e)
		{
			PB_SeasonInfo.Invalidate(new Rectangle(Width - 80, 0, 80, 32));
			PB_SeasonInfo.Cursor = new Rectangle(Width - 80, 0, 80, 32).Contains(e.Location) ? Cursors.Hand : Cursors.Default;
		}
		
		private void PB_SeasonInfo_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && new Rectangle(Width - 45, 0, 32, 32).Contains(e.Location))
				Season.ShowStrip();

			if (e.Button == MouseButtons.Left && new Rectangle(Width - 80, 0, 32, 32).Contains(e.Location) && Season.Show.GetCurrentWatchEpisode(Season.SeasonNumber) != null)
				Season.Show.GetCurrentWatchEpisode(Season.SeasonNumber).Play();
		}

		private void SL_Forward_Click(object sender, EventArgs e)
		{
			var sePan = new PC_SeasonView(Season.Next);

			sePan.ST_Cast.Selected = ST_Cast.Selected;
			sePan.ST_Crew.Selected = ST_Crew.Selected;
			sePan.ST_Images.Selected = ST_Images.Selected;

			Form.SetPanel(null, sePan, clearHistory: false);
		}

		private void SL_Backwards_Click(object sender, EventArgs e)
		{
			var sePan = new PC_SeasonView(Season.Previous);

			sePan.ST_Cast.Selected = ST_Cast.Selected;
			sePan.ST_Crew.Selected = ST_Crew.Selected;
			sePan.ST_Images.Selected = ST_Images.Selected;

			Form.SetPanel(null, sePan, clearHistory: false);
		}

		private void ST_Cast_TabSelected(object sender, EventArgs e)
		{
			FLP_Episodes.Controls.Clear(true);

			if (Season.TMDbData.Credits?.Cast != null)
				foreach (var item in Season.TMDbData.Credits.Cast)
					FLP_Episodes.Controls.Add(new CharacterControl(item));
		}

		private void ST_Crew_TabSelected(object sender, EventArgs e)
		{
			FLP_Episodes.Controls.Clear(true);

			if (Season.TMDbData.Credits?.Crew != null)
				foreach (var item in Season.TMDbData.Credits.Crew)
					FLP_Episodes.Controls.Add(new CharacterControl(item));
		}

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Left && SL_Backwards.Enabled)
			{
				SL_Backwards_Click(null, null);
				return true;
			}

			if (keyData == Keys.Right && SL_Forward.Enabled)
			{
				SL_Forward_Click(null, null);
				return true;
			}

			return base.KeyPressed(ref msg, keyData);
		}
	}
}
