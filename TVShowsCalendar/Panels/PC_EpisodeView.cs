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
using TVShowsCalendar.Classes;
using TVShowsCalendar.Handlers;
using TMDbLib.Objects.General;
using TVShowsCalendar.UserControls;
using TVShowsCalendar.HandlerClasses;
using Extensions;
using ProjectImages = TVShowsCalendar.Properties.Resources;
using SlickControls.Forms;

namespace TVShowsCalendar.Panels
{
	public partial class PC_EpisodeView : PanelContent
	{
		public PC_EpisodeView(Episode episode)
		{
			InitializeComponent();

			Episode = episode;

			Text = $"{episode.Show.Name} • {episode.Season.TMDbData.Name}";

			PB_EpisodeInfo.GetImage(episode.Season.TMDbData.PosterPath.IfEmpty(episode.Show.TMDbData.PosterPath), 200);

			SL_Backwards.Enabled = episode.Previous != null && episode.Previous.Season == episode.Season;
			SL_Forward.Enabled = episode.Next != null && episode.Next.Season == episode.Season;

			SlickTip.SetTo(SL_Backwards, "Previous Episode");
			SlickTip.SetTo(SL_Forward, "Next Episode");

			if (Episode.Images == null)
				StartDataLoad();
			else
				ST_Images_TabSelected(null, null);
		}

		protected override bool LoadData()
		{
			Episode.Images = Data.TMDbHandler.RunTask(x => x.GetTvEpisodeImagesAsync(Episode.Show.Id, Episode.SN, Episode.EN, "en")).Result;

			return true;
		}

		protected override void OnDataLoad()
		{
			if (ST_Images.Selected)
				ST_Images_TabSelected(null, null);
		}

		public Episode Episode { get; }

		private void PC_EpisodeView_Resize(object sender, EventArgs e)
		{
			FLP_Content.MaximumSize = new Size(panel1.Width, 9999);
			FLP_Content.MinimumSize = new Size(panel1.Width, 0);
		}

		private void PB_EpisodeInfo_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			if (PB_EpisodeInfo.Image != null)
				e.Graphics.DrawBorderedImage(PB_EpisodeInfo.Image, new Rectangle(8, 1, (130) * 2 / 3, 130), PB_EpisodeInfo.Image.IsAnimated() || PB_EpisodeInfo.Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage(ProjectImages.Big_TV.Color(FormDesign.Design.IconColor), new Rectangle(8, 1, (130) * 2 / 3, 130), ImageHandler.ImageSizeMode.Center);

			if (new Rectangle(Width - 45, 0, 32, 32).Contains(PB_EpisodeInfo.PointToClient(MousePosition)))
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_Dots_H).Color(FormDesign.Design.ActiveColor), Width - 45, 0);
			else
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_Dots_H).Color(FormDesign.Design.IconColor), Width - 45, 0);

			if (Episode.VidFile?.Exists ?? false)
			{
				if (new Rectangle(Width - 80, 0, 32, 32).Contains(PB_EpisodeInfo.PointToClient(MousePosition)))
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_PlaySlick).Color(FormDesign.Design.ActiveColor), Width - 80, 0);
				else
					e.Graphics.DrawImage(new Bitmap(ProjectImages.Icon_PlaySlick).Color(FormDesign.Design.IconColor), Width - 80, 0);
			}

			var w = 12 + ((130) * 2 / 3);
			var h = 2f;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			e.Graphics.DrawString(Episode.TMDbData.Name.RemoveDoubleSpaces(), new Font("Nirmala UI", 9.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), w, h);
			var bnds = e.Graphics.MeasureString(Episode.TMDbData.Name.RemoveDoubleSpaces(), new Font("Nirmala UI", 9.75F, FontStyle.Bold));

			h += bnds.Height + 2;

			e.Graphics.DrawString(Episode.TMDbData.AirDate?.ToReadableString() ?? "No air date", new Font("Nirmala UI", 9F), new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(Episode.TMDbData.AirDate?.ToReadableString() ?? "No air date", new Font("Nirmala UI", 9F));

			h += bnds.Height + 2;

			e.Graphics.DrawString($"Episode {Episode.EN}", new Font("Nirmala UI", 8.25F), new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString($"Episode {Episode.EN}", new Font("Nirmala UI", 8.25F));

			h += bnds.Height + 2;
			w += 2;

			e.Graphics.DrawString(Episode.TMDbData.Overview.IfEmpty("No overview"), new Font("Nirmala UI", 6.75F), new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, h, PB_EpisodeInfo.Width - w - 5, PB_EpisodeInfo.Height - h - 5), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}

		private void ST_Images_TabSelected(object sender, EventArgs e)
		{
			FLP_Content.Controls.Clear(true);
			foreach (var item in Episode.Images.Stills)
			{
				var width = 265;
				var height = width * item.Height / item.Width;

				var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
				pb.GetImage(item.FilePath, 300);

				FLP_Content.Controls.Add(pb);
			}
		}

		private void ST_Cast_TabSelected(object sender, EventArgs e)
		{
			FLP_Content.Controls.Clear(true);
			foreach (var item in Episode.TMDbData.GuestStars)
			{
				var pb = new CharacterControl(item)
				{
					Margin = new Padding(5),
					Tag = item
				};

				FLP_Content.Controls.Add(pb);
			}
		}

		private void ST_Crew_TabSelected(object sender, EventArgs e)
		{
			FLP_Content.Controls.Clear(true);
			foreach (var item in Episode.TMDbData.Crew)
			{
				var pb = new CharacterControl(item)
				{
					Margin = new Padding(5),
					Tag = item
				};

				FLP_Content.Controls.Add(pb);
			}
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			panel1.BackColor = verticalScroll1.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 3, -3));
		}

		private void PB_EpisodeInfo_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && new Rectangle(Width - 45, 0, 32, 32).Contains(e.Location))
				Episode.ShowStrip();

			if (e.Button == MouseButtons.Left && new Rectangle(Width - 80, 0, 32, 32).Contains(e.Location) && (Episode.VidFile?.Exists ?? false))
				Episode.Play();
		}

		private void PB_EpisodeInfo_MouseMove(object sender, MouseEventArgs e)
		{
			PB_EpisodeInfo.Invalidate(new Rectangle(Width - 80, 0, 80, 32));
			PB_EpisodeInfo.Cursor = new Rectangle(Width - 80, 0, 80, 32).Contains(e.Location) ? Cursors.Hand : Cursors.Default;
		}

		private void SL_Forward_Click(object sender, EventArgs e)
		{
			Form.SetPanel(null, new PC_EpisodeView(Episode.Next), clearHistory: false);
		}

		private void SL_Backwards_Click(object sender, EventArgs e)
		{
			Form.SetPanel(null, new PC_EpisodeView(Episode.Previous), clearHistory: false);
		}
	}
}
