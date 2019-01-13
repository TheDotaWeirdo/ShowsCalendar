using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using TVShowsCalendar.Handlers;
using Extensions;
using TVShowsCalendar.Forms;
using SlickControls.Forms;

namespace TVShowsCalendar.Classes
{
	public static class Data
	{
		public static Dashboard Dashboard { get; set; }

		public static TMDbHandler TMDbHandler { get; set; }

		public static bool FirstTimeSetup { get; set; } = false;

		public static Options Options { get; set; } = new Options();
		public static Preferences Preferences { get; set; } = new Preferences();

		public static NotifyIcon TaskIcon { get; internal set; }
	}
}