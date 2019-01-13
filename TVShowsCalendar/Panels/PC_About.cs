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

namespace TVShowsCalendar.Panels
{
	public partial class PC_About : PanelContent
	{
		public PC_About()
		{
			InitializeComponent();

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TVShowsCalendar.ChangeLog.txt"))
				using (StreamReader reader = new StreamReader(stream))
					P_Changelog.Controls.Add(new ChangeLogVersion(VersionInfo.GenerateInfo(reader.ReadToEnd().Split('\r', '\n')).FirstThat(x => x.Version.ToString() == ProductVersion)));
		}

		private void P_Spacer_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.BackColor);
			e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.AccentColor), 0, 0, 1, P_Spacer.Height - 150);

			e.Graphics.FillRectangle(new LinearGradientBrush(
				new RectangleF(0, P_Spacer.Height - 175, 1, 150),
				FormDesign.Design.AccentColor,
				FormDesign.Design.BackColor,
				90),
				new RectangleF(0, P_Spacer.Height - 175, 1, 150));
		}

		private void P_Spacer_2_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.BackColor);
			e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.AccentColor), 0, 0, P_Spacer_2.Width - 150, 1);

			e.Graphics.FillRectangle(new LinearGradientBrush(
				new RectangleF(P_Spacer_2.Width - 175, 0, 150, 1),
				FormDesign.Design.AccentColor,
				FormDesign.Design.BackColor,
				0F),
				new RectangleF(P_Spacer_2.Width - 175, 0, 150, 1));
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
			foreach (var item in P_Tiles.Controls.ThatAre<SlickTile>())
				item.Selected = item == sender;
		}

		private void T_Tips_Click(object sender, EventArgs e)
		{
			P_Stuff.Controls.Cast<Control>().Foreach(x => x.Hide());
			P_Tips.Visible = true;
			foreach (var item in P_Tiles.Controls.ThatAre<SlickTile>())
				item.Selected = item == sender;
		}

		private void T_Licence_Click(object sender, EventArgs e)
		{
			P_Stuff.Controls.Cast<Control>().Foreach(x => x.Hide());
			P_Licence.Visible = true;
			foreach (var item in P_Tiles.Controls.ThatAre<SlickTile>())
				item.Selected = item == sender;
		}

		private void T_Changelog_Click(object sender, EventArgs e)
		{
			P_Stuff.Controls.Cast<Control>().Foreach(x => x.Hide());
			P_Changelog.Visible = true;
			foreach (var item in P_Tiles.Controls.ThatAre<SlickTile>())
				item.Selected = item == sender;
		}

		private void B_ChangeLog_Click(object sender, EventArgs e)
		{
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TVShowsCalendar.ChangeLog.txt"))
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
	}
}
