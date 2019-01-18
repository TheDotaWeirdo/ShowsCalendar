using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using SlickControls.Panels;
using TVShowsCalendar.Classes;
using TVShowsCalendar.Panels;

namespace TVShowsCalendar.Forms
{
	public partial class Dashboard : BasePanelForm
	{
		#region Public Constructors

		public Dashboard()
		{
			InitializeComponent();

			SetPanel<PC_Dashboard>(PI_Dashboard);
			L_Version.Text = "v " + ProductVersion;

			FormDesign.Initialize(this);

			Visible = Data.Options.StartupMode;

			if (Data.Preferences.DashBounds != Rectangle.Empty)
				Bounds = Data.Preferences.DashBounds;

			WindowState = Data.Preferences.DashMax ? FormWindowState.Maximized : FormWindowState.Normal;
		}

		#endregion Public Constructors

		#region Internal Methods

		internal void Setup(int v)
		{
			switch (v)
			{
				case 1:
					SetPanel<PC_LibraryFolders>(PI_LibraryFolders);

					Notification.Clear();
					Notification.Create("First Time Setup • Library Folders", "This is your library folders screen, add in the folders that contain your Series and Movies so the app knows where to look.", PromptIcons.Info, null, false, new Size(350, 70))
						.Show(this, 30);
					System.Media.SystemSounds.Beep.Play();
					break;

				case 2:
					SetPanel<PC_Shows>(PI_Shows);

					Notification.Clear();
					Notification.Create("First Time Setup • Shows", "Finally, this is your Shows screen, add or discover TV Shows to your library to finish.\n\n" +
						"You can always go to the About section for more help and other tips about the App.", PromptIcons.Info, null, false, new Size(350, 115))
						.Show(this, 50);
					System.Media.SystemSounds.Beep.Play();
					EnableSideBar();
					Data.FirstTimeSetup = false;
					break;

				default:
					break;
			}
		}

		#endregion Internal Methods

		#region Protected Methods

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			PB_TMDb.Color(design.LabelColor);
			L_Version.ForeColor = L_Icons8.ForeColor = design.LabelColor;
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (!Data.FirstTimeSetup && e.CloseReason == CloseReason.UserClosing && e.CloseReason != CloseReason.ApplicationExitCall)
				e.Cancel = MessagePrompt.Show("Are you sure you want to close Shows Calendar?", "Confirm Action", PromptButtons.YesNo, PromptIcons.Question, this) == DialogResult.No;

			if (!e.Cancel)
				notifyIcon.Visible = false;

			base.OnFormClosing(e);
		}

		#endregion Protected Methods

		#region Private Methods

		private void Dashboard_Load(object sender, EventArgs e)
		{
			if (Data.FirstTimeSetup)
			{
				DisableSideBar();

				SetPanel<PC_Settings>(PI_Settings);

				BeginInvoke((MethodInvoker)(() =>
				{
					MessagePrompt.Show("Welcome to Shows Calendar!\nLet's set you up nice and breezy.", "First Time Setup", icon: PromptIcons.Info, form: this);

					Notification.Create("First Time Setup • Settings", "This is your settings screen, go through them to cherry-pick your experience. You can come here anytime.", PromptIcons.Info, null, false, new Size(350, 70))
						.Show(this, 30);
				}));
			}
		}

		private void L_Icons8_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { Process.Start("https://icons8.com"); }
			catch (Exception) { Cursor.Current = Cursors.Default; MessagePrompt.Show("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error, this); }
			Cursor.Current = Cursors.Default;
		}

		private void L_Version_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "TVShowsCalendar.ChangeLog.txt";
			var result = new string[0];

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
				result = reader.ReadToEnd().Split('\r', '\n');

			PushPanel(null, new PC_Changelog(result, ProductVersion));
			Cursor.Current = Cursors.Default;
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				new FlatToolStrip(new List<FlatStripItem>()
				{
					new FlatStripItem("Open", this.ShowUp, image: Properties.Resources.Icon_16),
					new FlatStripItem("TV Shows", () => { SetPanel<PC_Shows>(PI_Shows); this.ShowUp(); }, image: Properties.Resources.Tiny_TV),
					new FlatStripItem("Movies", () => { SetPanel<PC_Movies>(PI_Movies); this.ShowUp(); }, image: Properties.Resources.Tiny_Movie),
					new FlatStripItem("Watch", () => { SetPanel<PC_Watch>(PI_Watch); this.ShowUp(); }, image: Properties.Resources.Tiny_Play),
					new FlatStripItem("Settings", () => { SetPanel<PC_Settings>(PI_Settings); this.ShowUp(); }, image: Properties.Resources.Tiny_Settings),
					new FlatStripItem("Exit App", Close, image: Properties.Resources.Tiny_Cancel)
				}).ShowUp();
			}
			else
				this.ShowUp();
		}

		private void PB_TMDb_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { Process.Start("https://themoviedb.org/"); }
			catch (Exception) { Cursor.Current = Cursors.Default; MessagePrompt.Show("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error, this); }
			Cursor.Current = Cursors.Default;
		}

		private void PB_TMDb_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			e.Graphics.DrawString("powered by", new Font("Nirmala UI", 6.75F), new SolidBrush(FormDesign.Design.LabelColor), 7, 3);

			e.Graphics.DrawString("THE MOVIE DB", new Font("Nirmala UI", 8.25F, FontStyle.Bold), new SolidBrush(FormDesign.Design.LabelColor), 5, 14);
		}

		private void PI_About_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_About>((PanelItem)sender);

		private void PI_Dashboard_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Dashboard>((PanelItem)sender);

		private void PI_Library_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Library>((PanelItem)sender);

		private void PI_LibraryFolders_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_LibraryFolders>((PanelItem)sender);

		private void PI_Movies_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Movies>((PanelItem)sender);

		private void PI_Settings_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Settings>((PanelItem)sender);

		private void PI_Shows_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Shows>((PanelItem)sender);

		private void PI_Watch_OnClick(object sender, MouseEventArgs e) => SetPanel<PC_Watch>((PanelItem)sender);

		#endregion Private Methods
	}
}