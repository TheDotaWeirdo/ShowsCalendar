using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowsCalendar.Controls
{
	public class SlickPB : PictureBox
	{
		public SlickPB()
		{
			DoubleBuffered = true;
			ResizeRedraw = true;
		}
	}
}
