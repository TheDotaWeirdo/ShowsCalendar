using Extensions;
using ShowsCalendar.Handlers;
using System.Drawing;
using System.Windows.Forms;

namespace ShowsCalendar.Controls
{
    public partial class ContextedImage : PictureBox
    {
        public string HeaderText { get; set; }
        public string InfoText { get; set; }
        public string HoverText { get; set; }
        public Bitmap[] HoverImages { get; set; }

        public ContextedImage()
        {
            Size = new Size(110, 183);
            Cursor = Cursors.Hand;

            MouseMove += (s, e) => Invalidate();
            MouseLeave += (s, e) => Invalidate();
			Paint += ContextedImage_Paint;
        }

		private void ContextedImage_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			var imgRec = new Rectangle(1, 1, Width - 2, 3 * (Width - 2) / 2);

			if (Image != null)
				e.Graphics.DrawBorderedImage(Image, imgRec, Image.IsAnimated() || Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage(ErrorImage.Color(FormDesign.Design.IconColor), imgRec, ImageHandler.ImageSizeMode.Center);

			e.Graphics.DrawString(HeaderText, new Font("Nirmala UI", 8.25F), new SolidBrush(FormDesign.Design.LabelColor), new Rectangle(3, imgRec.Height + 4, Width - 3, 14), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });

			e.Graphics.DrawString(InfoText, new Font("Nirmala UI", 6.75F), new SolidBrush(FormDesign.Design.InfoColor), 4, imgRec.Height + 20);

			if (imgRec.Contains(PointToClient(MousePosition)))
			{
				e.Graphics.DrawIconsOverImage(imgRec, PointToClient(MousePosition), HoverImages);

				var _bnds = e.Graphics.MeasureString(HoverText, new Font("Nirmala UI", 6.75F, FontStyle.Bold));
				e.Graphics.DrawString(HoverText, new Font("Nirmala UI", 6.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), imgRec.Width - 3 - _bnds.Width, imgRec.Height - 2 - _bnds.Height);
			}
		}
    }
}
