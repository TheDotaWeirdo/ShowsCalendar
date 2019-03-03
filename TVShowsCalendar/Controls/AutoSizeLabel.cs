using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowsCalendar.Controls
{
	public class AutoSizeLabel : Control
	{
		[Browsable(true)]
		[Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Bindable(true)]
		public override string Text { get => base.Text; set => base.Text = value; }

		public AutoSizeLabel()
		{
			ResizeRedraw = true;
			DoubleBuffered = true;
			Paint += AutoSizeLabel_Paint;
			Resize += AutoSizeLabel_Resize;
			TextChanged += AutoSizeLabel_Resize;
		}

		private void AutoSizeLabel_Resize(object sender, EventArgs e)
		{
			Height = (int)CreateGraphics().MeasureString(Text, Font, Width).Height;
		}

		private void AutoSizeLabel_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new RectangleF(0, 0, Width, Height));
		}
	}
}
