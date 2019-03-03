using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;
using SlickControls.Enums;
using SlickControls.Forms;
using ShowsCalendar.Classes;
using ShowsCalendar.Forms;
using ShowsCalendar.Handlers;

namespace ShowsCalendar
{
	internal static class Program
	{
		private const int SW_HIDE = 0;
		private const int SW_SHOW = 5;

		[DllImport("User32")]
		private static extern int ShowWindow(int hwnd, int nCmdShow);

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				ISave.AppName = "Shows Calendar";
				UpdateHandler.Start();
				ConnectionHandler.Start();

				Data.TMDbHandler = new Handlers.TMDbHandler();

				Data.Preferences = ISave.Load<Preferences>("Preferences.tf");
				Data.Options = ISave.Load<Options>("Options.tf");
				Data.FirstTimeSetup = !Directory.Exists(ISave.DocsFolder);

#if !DEBUG
				if (Data.FirstTimeSetup && !GeneralMethods.IsAdministrator)
				{
					try
					{
						Process.Start(new ProcessStartInfo(AppDomain.CurrentDomain.FriendlyName)
						{
							UseShellExecute = true,
							Verb = "runas"
						});
						return;
					}
					catch (System.ComponentModel.Win32Exception) { }					
				}
#endif

				if (!Data.FirstTimeSetup)
				{
					Directory.CreateDirectory(ISave.DocsFolder);
					Directory.CreateDirectory(Path.Combine(ISave.DocsFolder, "ShowsData"));
					Directory.CreateDirectory(Path.Combine(ISave.DocsFolder, "MoviesData"));
					Directory.CreateDirectory(Path.Combine(ISave.DocsFolder, "Thumbs"));

					LocalFileHandler.Load();
					ShowManager.LoadAllShows();
					MovieManager.LoadAllMovies();
					LocalShowHandler.Load();
					LocalMovieHandler.Load();
				}

#if !DEBUG
				using (SingleProgramInstance spi = new SingleProgramInstance("x5k6yz"))
				{
					if (spi.IsSingleInstance || spi.RaiseOtherProcess())
					{
#endif
						Application.Run(Data.Mainform = new Dashboard());
#if !DEBUG
					}
				}
#endif
			}
			catch (Exception ex) { ExceptionHandler(ex); }
		}

		private static void ExceptionHandler(Exception ex)
		{
			if (ex.Message == "Cannot access a disposed object.\r\nObject name: 'Dashboard'.")
				return;

			if (Debugger.IsAttached)
				throw new Exception("Fatal Error", ex);

			var send = DialogResult.Yes == MessagePrompt.Show("Shows Calendar ran into a fatal error.\n\nWould you like to send the error report to the App Owner?", "Fatal Error", PromptButtons.YesNo, PromptIcons.Error);
			var restart = DialogResult.Yes == MessagePrompt.Show("Would you like to Restart the App?", "Restart App?", PromptButtons.YesNo, PromptIcons.Question);

			if (send && ConnectionHandler.State == ConnectionState.Connected)
			{
				try
				{
					using (SmtpClient client = new SmtpClient("smtp.live.com")
						{
							UseDefaultCredentials = false,
							Credentials = new NetworkCredential("ShowsCalendar@hotmail.com", "iPhone0X", "hotmail.com"),
							Port = 587,
							EnableSsl = true,
							DeliveryMethod = SmtpDeliveryMethod.Network
						}) 
					{
						MailMessage mailMessage = new MailMessage
						{
							From = new MailAddress("ShowsCalendar@hotmail.com"),
							IsBodyHtml = true,
							Body = GetBody(ex),
							Subject = "Error Report",
						};
						mailMessage.To.Add("dotca@hotmail.co.uk");
						client.Send(mailMessage);
					}
				}
				catch
				{
					MessagePrompt.Show("There was an error sending the message to the App Owner.\n\nThe Error was copied to your clipboard", "Message Failed", PromptButtons.OK, PromptIcons.Error);
					Clipboard.SetText(ex.ToString());
				}
			}
			else if (send)
			{
				MessagePrompt.Show("You aren't connected to the Internet right now.\n\nThe Error was copied to your clipboard", "Message Failed", PromptButtons.OK, PromptIcons.Error);
				Clipboard.SetText(ex.ToString());
			}

			if (restart)
				Application.Restart();
		}

		private static string GetBody(Exception ex)
		{
			var json = Regex.Matches(Newtonsoft.Json.JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented), "\"(.+?)\": \"?(.+)")
				.Cast<Match>().ConvertDictionary(x => new KeyValuePair<string, string>(x.Groups[1].Value.FormatWords(), x.Groups[2].Value.TrimEnd('"', ',', '\r').RegexReplace("(\\\\r)?\\\\n", "<br>")));

			if (json.ContainsKey("Stack Trace String"))
				json.Remove("Stack Trace String");

			if (json.ContainsKey("Watson Buckets"))
				json.Remove("Watson Buckets");

			return
				$"<h3>PC Info</h3>" +
				$"<table style=\"text-align: left; padding-left: 10pt;\">" +
				$"	<tbody>" +
				$"		<tr>" +
				$"			<th style=\"text-align: left;\">User:</th>" +
				$"			<td>{SystemInformation.UserName}</td>" +
				$"		</tr>" +
				$"		<tr>" +
				$"			<th style=\"text-align: left;\">Domain:</th>" +
				$"			<td>{SystemInformation.UserDomainName}</td>" +
				$"		</tr>" +
				$"		<tr>" +
				$"			<th style=\"text-align: left;\">OS:</th>" +
				$"			<td>{GetOSFriendlyName()}</td>" +
				$"		</tr>" +
				$"		<tr>" +
				$"			<th style=\"text-align: left;\">PC Name:</th>" +
				$"			<td>{SystemInformation.ComputerName}</td>" +
				$"		</tr>" +
				$"	</tbody>" +
				$"</table>" +
				$"<hr />" +
				$"<h3>Exception Trace</h3>" +
				$"<p>{ex.ToString().Replace("was thrown.", "was thrown.<br>").Replace("  at", "&emsp;&emsp;at").Replace("\r\n", "<br />").RegexRemove("(?<= in ).+solutions\\\\")}</p>" +
				$"<hr />" +
				$"<h3>Exception Info</h3>" +
				$"<table style=\"text-align: left; padding-left: 10pt;\">" +
				$"	<tbody>" +
					json.Where(x => x.Value != "null").ListStrings(x =>
						$"		<tr>" +
						$"			<th style=\"text-align: left;\">{x.Key}:</th>" +
						$"			<td>{x.Value}</td>" +
						$"		</tr>") +
				$"	</tbody>" +
				$"</table>";
		}

		public static string GetOSFriendlyName()
		{
			using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
				foreach (ManagementObject os in searcher.Get())
					return $"{os["Caption"]} v{os["Version"]}";

			return "Unknown";
		}
	}
}