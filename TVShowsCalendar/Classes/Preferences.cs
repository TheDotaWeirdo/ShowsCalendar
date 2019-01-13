using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Extensions;

namespace TVShowsCalendar.Classes
{
	public class Preferences : ISave
	{
		public bool DashMax { get; set; } = false;

		public Rectangle DashBounds { get; set; } = Rectangle.Empty;

		public DateTime LastMovieCheck { get; set; } = DateTime.MinValue;
		public DateTime LastShowCheck { get; set; } = DateTime.MinValue;
	}
}
