using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TMDbLib.Objects.Search;
using ShowsCalendar.Classes;
using Extensions;
using ProjectImages = ShowsCalendar.Properties.Resources;
using ShowsCalendar.Handlers;
using ShowsCalendar.Panels;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShowsCalendar.Controls
{
	public partial class VideoFileControl : PictureBox
	{
		public dynamic ItemData { get; private set; }
		private bool IsMovie;
		private FileInfo VidFile;

		private string[] infoStrings;

		public bool PictureLoaded = false;

		public VideoFileControl(FileInfo fileInfo, Movie movie) : this (fileInfo, movie.TMDbData.PosterPath)
		{
			ItemData = movie;
			IsMovie = true;
		}

		public VideoFileControl(FileInfo fileInfo, Episode episode) : this(fileInfo, episode.Season.TMDbData.PosterPath.IfEmpty(episode.Show.TMDbData.PosterPath))
		{
			ItemData = episode;
			IsMovie = false;
		}

		private VideoFileControl(FileInfo fileInfo, string data)
		{
			InitializeComponent();

			VidFile = fileInfo;
			this.GetImage(data, 200);

			var shell = new Shell32.Shell();
			var objFolder = shell.NameSpace(fileInfo.DirectoryName);
			var item = objFolder.ParseName(fileInfo.Name);
			var vidprops = 0;
			var audprops = (NReco.VideoInfo.MediaInfo.StreamInfo)null;
			var subs = new NReco.VideoInfo.MediaInfo.StreamInfo[0];
			var dur = "File Read Failed";

			try
			{
				var fileprops = new NReco.VideoInfo.FFProbe().GetMediaInfo(fileInfo.FullName);
				vidprops = fileprops.Streams.FirstThat(x => x.CodecType == "video")?.Height ?? 0;
				audprops = fileprops.Streams.FirstThat(x => x.CodecType == "audio");
				subs = fileprops.Streams.Where(x => x.CodecType == "subtitle").ToArray();
				dur = fileprops.Duration.ToReadableString();
			}
			catch { }

			var vidQ = "Low Quality";

			if (vidprops == 0)
				vidQ = string.Empty;
			else if (vidprops > 2250)
				vidQ = "8K";
			else if (vidprops > 1700)
				vidQ = "4K UHD";
			else if (vidprops > 775)
				vidQ = "1080p HD";
			else if (vidprops > 550)
				vidQ = "720p HQ";

			infoStrings = new[]
			{
				fileInfo.FileName(),
				vidQ,
				fileInfo.Length.SizeString(),
				dur,
				$"Created on {fileInfo.CreationTime.ToReadableString(true, ExtensionClass.DateFormat.TDMY)}",
				fileInfo.AbreviatedPath(true).Replace("\\..\\", "\\ ..\\"),
				audprops?.CodecLongName ?? string.Empty,
				subs.Any() ? $"{subs.Length} Subtitle".Plural(subs) : string.Empty
			};
		}

		private void MediaViewer_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && (new Rectangle(Width - 20, 6, 16, 16).Contains(e.Location) || new Rectangle(1, 1, 82, 123).Contains(e.Location)))
			{
				ItemData.Play(VidFile);
			}
			if (e.Button == MouseButtons.Left && new Rectangle(Width - 40, 6, 16, 16).Contains(e.Location))
			{
				Process.Start(VidFile.DirectoryName);
			}
		}

		private void MediaViewer_MouseMove(object sender, MouseEventArgs e)
		{
			if (new Rectangle(1, 1, 82, 123).Contains(e.Location) || new Rectangle(Width - 20, 6, 16, 16).Contains(e.Location) || new Rectangle(Width - 40, 6, 16, 16).Contains(e.Location))
				Cursor = Cursors.Hand;
			else
				Cursor = Cursors.Default;

			Invalidate();
		}

		private void MediaViewer_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			if(Image != null)
				e.Graphics.DrawBorderedImage(Image, new Rectangle(1, 1, 82, 123), Image.IsAnimated() || Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage((IsMovie ? ProjectImages.Big_Movie : ProjectImages.Big_TV).Color(FormDesign.Design.IconColor), new Rectangle(1, 1, 82, 123), ImageHandler.ImageSizeMode.Center);

			if (new Rectangle(1, 1, 82, 123).Contains(PointToClient(MousePosition)))
			{
				e.Graphics.DrawIconsOverImage(new Rectangle(1, 1, 82, 123), PointToClient(MousePosition), ProjectImages.Big_Play);

				var _bnds = e.Graphics.MeasureString("PLAY THIS", new Font("Nirmala UI", 6.75F, FontStyle.Bold));
				e.Graphics.DrawString("PLAY THIS", new Font("Nirmala UI", 6.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), 81 - _bnds.Width, 123 - _bnds.Height);
			}

			if (new Rectangle(Width - 20, 6, 16, 16).Contains(PointToClient(MousePosition)))
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Play).Color(FormDesign.Design.ActiveColor), Width - 20, 6);
			else
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Play).Color(FormDesign.Design.IconColor), Width - 20, 6);

			if (new Rectangle(Width - 40, 6, 16, 16).Contains(PointToClient(MousePosition)))
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Folder).Color(FormDesign.Design.ActiveColor), Width - 40, 6);
			else
				e.Graphics.DrawImage(new Bitmap(ProjectImages.Tiny_Folder).Color(FormDesign.Design.IconColor), Width - 40, 6);

			var strFrmt = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
			var font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
			var w = 86;
			var h = 4;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			var bnds = e.Graphics.MeasureString(infoStrings[0], font);

			e.Graphics.DrawString(infoStrings[0], font, new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(w, h, Width - w - 40, (int)bnds.Height), strFrmt);

			h += (int)bnds.Height + 1;

			font = new Font("Nirmala UI", 8.25F);

			e.Graphics.DrawString(infoStrings[2], font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(infoStrings[2], font);

			font = new Font("Nirmala UI", 6.75F);
			h += (int)bnds.Height + 2;
			w++;

			e.Graphics.DrawString(infoStrings[3], font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(infoStrings[3], font);

			h += (int)bnds.Height + 1;

			if (!string.IsNullOrWhiteSpace(infoStrings[6]))
			{
				e.Graphics.DrawString(infoStrings[6], font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
				bnds = e.Graphics.MeasureString(infoStrings[6], font);

				h += (int)bnds.Height + 1;
			}

			e.Graphics.DrawString(infoStrings[4], font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(infoStrings[4], font);

			h += (int)bnds.Height + 6;

			bnds = e.Graphics.MeasureString(infoStrings[5], font, Width - w);
			e.Graphics.DrawString(infoStrings[5], font, new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, System.Math.Max(h, Height - bnds.Height - 5), Width - 100, Height - System.Math.Max(h, Height - bnds.Height - 5)), strFrmt);

			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			if (!string.IsNullOrWhiteSpace(infoStrings[1]))
				e.Graphics.DrawBannerOverImage(new Rectangle(1, 1, 82, 123), infoStrings[1], BannerStyle.Active);

			if (!string.IsNullOrWhiteSpace(infoStrings[7]))
				e.Graphics.DrawBannerOverImage(new Rectangle(1, 1, 82, 123), infoStrings[7], BannerStyle.Green, tab: string.IsNullOrWhiteSpace(infoStrings[1]).If(0, 1));
		}

		private void MediaViewer_MouseLeave(object sender, System.EventArgs e)
		{
			Invalidate();
		}
	}
}
