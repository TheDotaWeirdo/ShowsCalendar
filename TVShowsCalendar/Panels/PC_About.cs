using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlickControls.Panels;
using Extensions;
using System.Drawing.Drawing2D;
using SlickControls.Controls;
using System.Reflection;
using System.IO;
using SlickControls.Classes;
using System.Diagnostics;
using SlickControls.Enums;

namespace ShowsCalendar.Panels
{
	public partial class PC_About : PanelContent
	{
		public PC_About()
		{
			InitializeComponent();

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ShowsCalendar.ChangeLog.txt"))
				using (StreamReader reader = new StreamReader(stream))
					P_Changelog.Controls.Add(new ChangeLogVersion(VersionInfo.GenerateInfo(reader.ReadToEnd().Split('\r', '\n')).FirstThat(x => x.Version.ToString() == ProductVersion)));
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
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ShowsCalendar.ChangeLog.txt"))
				using (StreamReader reader = new StreamReader(stream))
					Form.PushPanel(null, new PC_Changelog(reader.ReadToEnd().Split('\r', '\n'), ProductVersion));
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
	}
}
