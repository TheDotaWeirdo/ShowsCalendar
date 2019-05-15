using Extensions;
using SlickControls.Classes;
using SlickControls.Controls;
using SlickControls.Enums;
using SlickControls.Panels;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ShowsCalendar.Panels
{
	public partial class PC_About : PanelContent
	{
		public PC_About()
		{
			InitializeComponent();

			L_Storage.Text += new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Thumbs")).GetFiles("*", SearchOption.AllDirectories).Sum(x => x.Length).SizeString();

			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ShowsCalendar.ChangeLog.json"))
			using (var reader = new StreamReader(stream))
				P_Changelog.Controls.Add(new ChangeLogVersion(Newtonsoft.Json.JsonConvert.DeserializeObject<VersionChangeLog[]>(reader.ReadToEnd()).FirstThat(x => x.VersionString == ProductVersion)));
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			P_OptionContainer.BackColor = verticalScroll1.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 3, -3));
		}

		private void PC_About_Resize(object sender, EventArgs e)
		{
			P_Stuff.MaximumSize = new Size(P_OptionContainer.Width, 9999);
			P_Stuff.MinimumSize = new Size(P_OptionContainer.Width, 0);
		}

		private void T_AppInfo_Click(object sender, EventArgs e)
		{
			P_Stuff.Controls.Cast<Control>().Foreach(x => x.Hide());
			P_AppInfo.Visible = true;
		}

		private void T_Tips_Click(object sender, EventArgs e)
		{
			P_Stuff.Controls.Cast<Control>().Foreach(x => x.Hide());
			P_Tips.Visible = true;
		}

		private void T_Licence_Click(object sender, EventArgs e)
		{
			P_Stuff.Controls.Cast<Control>().Foreach(x => x.Hide());
			P_Licence.Visible = true;
		}

		private void T_Changelog_Click(object sender, EventArgs e)
		{
			P_Stuff.Controls.Cast<Control>().Foreach(x => x.Hide());
			P_Changelog.Visible = true;
		}

		private void B_ChangeLog_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Form.PushPanel(null, new PC_Changelog(Assembly.GetExecutingAssembly(), "ShowsCalendar.ChangeLog.json", new Version(ProductVersion)));
			Cursor.Current = Cursors.Default;
		}

		private void B_TMDb_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { Process.Start("https://themoviedb.org/"); }
			catch (Exception) { Cursor.Current = Cursors.Default; ShowPrompt("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
			Cursor.Current = Cursors.Default;
			P_Stuff.Height = P_AppInfo.Height;
		}

		private void B_Icons8_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { Process.Start("https://icons8.com"); }
			catch (Exception) { Cursor.Current = Cursors.Default; ShowPrompt("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
			Cursor.Current = Cursors.Default;
		}

		private void B_Zooqle_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { Process.Start("https://zooqle.com"); }
			catch (Exception) { Cursor.Current = Cursors.Default; ShowPrompt("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
			Cursor.Current = Cursors.Default;
		}

		private void B_ClearAll_Click(object sender, EventArgs e)
		{
			EmptyFolder(new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Thumbs")));
			L_Storage.Text = "Shows Calendar stores the thumbnails and images locally so they don\'t have to be downloaded every time they are needed.\r\n\r\nCurrently used storage;  "
				+ new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Thumbs")).GetFiles("*", SearchOption.AllDirectories).Sum(x => x.Length).SizeString();
		}

		private void B_ClearRecent_Click(object sender, EventArgs e)
		{
			EmptyFolder(new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Thumbs")), x => x.LastAccessTime < DateTime.Now.AddMonths(-1));
			L_Storage.Text = "Shows Calendar stores the thumbnails and images locally so they don\'t have to be downloaded every time they are needed.\r\n\r\nCurrently used storage;  "
				+ new DirectoryInfo(Path.Combine(ISave.DocsFolder, "Thumbs")).GetFiles("*", SearchOption.AllDirectories).Sum(x => x.Length).SizeString();
		}

		private void EmptyFolder(DirectoryInfo directoryInfo, Func<FileInfo, bool> test = null)
		{
			if (!directoryInfo.Exists) return;

			foreach (var file in directoryInfo.GetFiles())
			{
				if (test == null || test(file))
				{ try { file.Delete(); } catch { } }
			}

			foreach (var subfolder in directoryInfo.GetDirectories())
			{
				EmptyFolder(subfolder, test);
			}

			try { directoryInfo.Delete(); } catch { }
		}
	}
}
