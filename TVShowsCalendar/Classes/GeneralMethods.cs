using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using Extensions;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace TVShowsCalendar.Classes
{
	public enum TaskbarLocation { Top, Left, Bottom, Right, None }

	public static class GeneralMethods
	{
		public delegate void action();

		public static Dictionary<int, string> QualityDicId2Txt = new Dictionary<int, string>()
		{
			{ 11  , "3D"      },
			{ 9   , "4K Ultra"},
			{ 7   , "1080p"   },
			{ 5   , "720p"    },
			{ 3   , "Low"     },
			{ 2   , "Low"     },
			{ 1   , "Low"     },
		};

		public static Dictionary<string, int> QualityDicTxt2Id = new Dictionary<string, int>()
		{
			{ "3D"   ,  11    },
			{ "Ultra",  9     },
			{ "1080p",  7     },
			{ "720p" ,  5     },
			{ "Low"  ,  1     },
		};
		
		public static void WhenClosed(this Process process, action action)
		{
			var T = new System.Timers.Timer(1000);
			T.Elapsed += (s, e) =>
			{
				if (IntPtr.Zero == GetWindow(process.MainWindowHandle, GW_HWNDPREV))
				{
					T.Dispose();
					action();
				}
			};
			T.Start();
		}

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr GetWindow(IntPtr hWnd, int nIndex);

		const int GW_HWNDPREV = 3;

		public static long GetFileDuration(this FileInfo file)
		{
			using (ShellObject shell = ShellObject.FromParsingName(file.FullName))
			{
				var prop = shell.Properties.System.Media.Duration;
				var durString = prop.FormatForDisplay(PropertyDescriptionFormatOptions.None).Split(':');

				return (durString[0].SmartParse() * TimeSpan.TicksPerHour) + (durString[1].SmartParse() * TimeSpan.TicksPerMinute) + (durString[2].SmartParse() * TimeSpan.TicksPerSecond);
			}
		}

		public static string RelativeString(this DateTime dateTime)
		{
			if (dateTime == DateTime.Today)
				return "Today";
			else if(dateTime < DateTime.Today)
			{
				if (dateTime == DateTime.Today.AddDays(-1))
					return "Yesterday";
				else if (DateTime.Today.Ticks - dateTime.Ticks <= 30 * TimeSpan.TicksPerDay)
					return $"{new TimeSpan(DateTime.Today.Ticks - dateTime.Date.Ticks).ToReadableBigString()} ago";
				else
					return $"{(dateTime.Year == DateTime.Today.Year ? "last" : "on")} {dateTime.ToReadableString(dateTime.Year != DateTime.Today.Year, ExtensionClass.DateFormat.MDY)}";
			}
			else
			{
				if (dateTime == DateTime.Today.AddDays(1))
					return "Tomorrow";
				else if (dateTime.Ticks - DateTime.Today.Ticks <= 30 * TimeSpan.TicksPerDay)
					return $"{(dateTime.Ticks - DateTime.Today.Ticks <= 7 * TimeSpan.TicksPerDay ? $"next {dateTime.DayOfWeek} " : "")}in {new TimeSpan(dateTime.Ticks - DateTime.Today.Ticks).ToReadableBigString()}";
				else
					return $"{(dateTime.Year == DateTime.Today.Year ? "next" : "on")} {dateTime.ToReadableString(dateTime.Year != DateTime.Today.Year, ExtensionClass.DateFormat.MDY)}";
			}
		}

		public static void LoadPicture(this SlickControls.Controls.RoundedPicture pictureBox, string url, Func<Bitmap> defaultImage, PictureBoxSizeMode pictureBoxSizeMode = PictureBoxSizeMode.CenterImage)
		{
			if (string.IsNullOrEmpty(url))
			{
				if (pictureBox.InvokeRequired)
					pictureBox.Invoke(new Action(() => { pictureBox.SizeMode = pictureBoxSizeMode; pictureBox.Image = defaultImage(); }));
				else
				{ pictureBox.SizeMode = pictureBoxSizeMode; pictureBox.Image = defaultImage(); }

				return;
			}

			pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
			pictureBox.Image = FormDesign.Loader;

			new Action(() =>
			{
				try
				{
					var picbox = new PictureBox();
					picbox.Load($"https://image.tmdb.org/t/p/w200{url}");

					if (pictureBox.InvokeRequired)
						pictureBox.Invoke(new Action(() =>
						{
							pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
							pictureBox.Image = picbox.Image;
						}));
					else
					{
						pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
						pictureBox.Image = picbox.Image;
					}
				}
				catch
				{
					if (pictureBox.InvokeRequired)
						pictureBox.Invoke(new Action(() => pictureBox.Image = defaultImage()));
					else
						pictureBox.Image = defaultImage();
				}
			}).RunInBackground();
		}

		private static bool? isadministrator;
		public static bool IsAdministrator
		{
			get
			{
				if (isadministrator == null)
				{
					var identity = WindowsIdentity.GetCurrent();
					var principal = new WindowsPrincipal(identity);
					isadministrator = principal.IsInRole(WindowsBuiltInRole.Administrator);
				}
				return (bool)(isadministrator);
			}
		}

		public static TaskbarLocation GetTaskbarLocation()
		{
			var sc = Screen.FromHandle(new WindowInteropHelper(new System.Windows.Window()).Handle);

			if (sc.WorkingArea.Top > 0)
				return TaskbarLocation.Top;
			else if (sc.WorkingArea.Left != sc.Bounds.X)
				return TaskbarLocation.Left;
			else if ((sc.Bounds.Height - sc.WorkingArea.Height) > 0)
				return TaskbarLocation.Bottom;
			else if (sc.WorkingArea.Right != 0)
				return TaskbarLocation.Right;

			return TaskbarLocation.None;
		}

		public static void CreateShortcut(string Shortcut, string TargetPath)
		{
			Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")); //Windows Script Host Shell Object
			dynamic shell = Activator.CreateInstance(t);
			try
			{
				var lnk = shell.CreateShortcut(Shortcut);
				try
				{
					lnk.TargetPath = TargetPath;
					lnk.Save();
				}
				finally
				{
					Marshal.FinalReleaseComObject(lnk);
				}
			}
			finally
			{
				Marshal.FinalReleaseComObject(shell);
			}
		}
	}
}
