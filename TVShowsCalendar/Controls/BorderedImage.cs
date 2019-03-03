using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Forms;
using ShowsCalendar.Classes;
using ShowsCalendar.Handlers;

namespace ShowsCalendar.Controls
{
	public partial class BorderedImage : PictureBox
	{
		public string ImageUrl { get; set; }

		public BorderedImage()
		{
			InitializeComponent();
		}

		private void BorderedImage_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			e.Graphics.DrawBorderedImage(
				(Image ?? new Bitmap(Properties.Resources.Icon_ErrorImage).Color(FormDesign.Modern.IconColor))
				, 1
				, 1
				, Width - 2
				, Height - 2
				, (Image == null || SizeMode == PictureBoxSizeMode.CenterImage || Image.IsAnimated())
					? ImageHandler.ImageSizeMode.Center
					: ImageHandler.ImageSizeMode.Fill
			);
		}

		private void BorderedImage_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				FlatToolStrip.Show(Data.Mainform,
					new FlatStripItem("Save Image"
					, () =>
					{
						var path = SaveImage();

						if (path != null)
							Image.Save(path);
					}
					, Properties.Resources.Tiny_Download),

					new FlatStripItem("", show: !string.IsNullOrWhiteSpace(ImageUrl)),

					new FlatStripItem("Download Original"
					, () =>
					{
						var path = SaveImage();

						if (path != null)
						{
							var frm = Notification.Create("Downloading Image", "Getting the full resolution image you requested.\nThis should take a couple seconds..", SlickControls.Enums.PromptIcons.Info, null)
											.Show(Data.Mainform);
							var pb = frm.PictureBox;

							pb.GetImage(ImageUrl, 0);
							pb.LoadCompleted += (s, re)=>
							{
								if (re.Error == null && !re.Cancelled)
									pb.Image.Save(path);
								frm.TryInvoke(frm.Dispose);
							};	
						}
					}
					, Properties.Resources.Tiny_CloudDownload
					, show: !string.IsNullOrWhiteSpace(ImageUrl))
				);
			}
		}

		private string SaveImage()
		{
			var sd = new SaveFileDialog() { Filter = "Images|*.jpeg;*.jpg", InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) };

			if (sd.ShowDialog(this) == DialogResult.OK)
				return sd.FileName;

			return null;
		}
	}
}
