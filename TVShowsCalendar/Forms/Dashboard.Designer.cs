namespace TVShowsCalendar.Forms
{
	partial class Dashboard
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
			this.PI_Settings = new SlickControls.Panels.PanelItem();
			this.PI_LibraryFolders = new SlickControls.Panels.PanelItem();
			this.PI_Shows = new SlickControls.Panels.PanelItem();
			this.PI_Movies = new SlickControls.Panels.PanelItem();
			this.PI_Library = new SlickControls.Panels.PanelItem();
			this.PI_Watch = new SlickControls.Panels.PanelItem();
			this.PI_Dashboard = new SlickControls.Panels.PanelItem();
			this.L_Icons8 = new System.Windows.Forms.Label();
			this.L_Version = new System.Windows.Forms.Label();
			this.PB_TMDb = new System.Windows.Forms.PictureBox();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.PI_About = new SlickControls.Panels.PanelItem();
			this.base_P_SideControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_TMDb)).BeginInit();
			this.SuspendLayout();
			// 
			// base_P_Content
			// 
			this.base_P_Content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(58)))));
			this.base_P_Content.Size = new System.Drawing.Size(1046, 546);
			// 
			// base_P_SideControls
			// 
			this.base_P_SideControls.Controls.Add(this.PB_TMDb);
			this.base_P_SideControls.Controls.Add(this.L_Version);
			this.base_P_SideControls.Controls.Add(this.L_Icons8);
			this.base_P_SideControls.Location = new System.Drawing.Point(0, 423);
			this.base_P_SideControls.Size = new System.Drawing.Size(165, 51);
			// 
			// PI_Settings
			// 
			this.PI_Settings.ForceReopen = false;
			this.PI_Settings.Group = "Manage";
			this.PI_Settings.Icon = global::TVShowsCalendar.Properties.Resources.Tiny_Settings;
			this.PI_Settings.Selected = false;
			this.PI_Settings.Text = "Settings";
			this.PI_Settings.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Settings_OnClick);
			// 
			// PI_LibraryFolders
			// 
			this.PI_LibraryFolders.ForceReopen = false;
			this.PI_LibraryFolders.Group = "Manage";
			this.PI_LibraryFolders.Icon = global::TVShowsCalendar.Properties.Resources.Tiny_FolderPlay;
			this.PI_LibraryFolders.Selected = false;
			this.PI_LibraryFolders.Text = "Library";
			this.PI_LibraryFolders.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_LibraryFolders_OnClick);
			// 
			// PI_Shows
			// 
			this.PI_Shows.ForceReopen = false;
			this.PI_Shows.Group = "Content";
			this.PI_Shows.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Shows.Icon")));
			this.PI_Shows.Selected = false;
			this.PI_Shows.Text = "TV Shows";
			this.PI_Shows.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Shows_OnClick);
			// 
			// PI_Movies
			// 
			this.PI_Movies.ForceReopen = false;
			this.PI_Movies.Group = "Content";
			this.PI_Movies.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Movies.Icon")));
			this.PI_Movies.Selected = false;
			this.PI_Movies.Text = "Movies";
			this.PI_Movies.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Movies_OnClick);
			// 
			// PI_Library
			// 
			this.PI_Library.ForceReopen = false;
			this.PI_Library.Group = "Watch";
			this.PI_Library.Icon = global::TVShowsCalendar.Properties.Resources.Tiny_Library;
			this.PI_Library.Selected = false;
			this.PI_Library.Text = "Library";
			this.PI_Library.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Library_OnClick);
			// 
			// PI_Watch
			// 
			this.PI_Watch.ForceReopen = false;
			this.PI_Watch.Group = "Watch";
			this.PI_Watch.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Watch.Icon")));
			this.PI_Watch.Selected = false;
			this.PI_Watch.Text = "Watch";
			this.PI_Watch.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Watch_OnClick);
			// 
			// PI_Dashboard
			// 
			this.PI_Dashboard.ForceReopen = false;
			this.PI_Dashboard.Group = null;
			this.PI_Dashboard.Icon = ((System.Drawing.Bitmap)(resources.GetObject("PI_Dashboard.Icon")));
			this.PI_Dashboard.Selected = false;
			this.PI_Dashboard.Text = "Dashboard";
			this.PI_Dashboard.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_Dashboard_OnClick);
			// 
			// L_Icons8
			// 
			this.L_Icons8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.L_Icons8.AutoSize = true;
			this.L_Icons8.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Icons8.Font = new System.Drawing.Font("Nirmala UI", 6.75F);
			this.L_Icons8.Location = new System.Drawing.Point(89, 35);
			this.L_Icons8.Name = "L_Icons8";
			this.L_Icons8.Padding = new System.Windows.Forms.Padding(2);
			this.L_Icons8.Size = new System.Drawing.Size(77, 16);
			this.L_Icons8.TabIndex = 29;
			this.L_Icons8.Text = "Icons from Icons8";
			this.L_Icons8.Click += new System.EventHandler(this.L_Icons8_Click);
			// 
			// L_Version
			// 
			this.L_Version.AutoSize = true;
			this.L_Version.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Version.Font = new System.Drawing.Font("Nirmala UI", 6.75F);
			this.L_Version.Location = new System.Drawing.Point(0, 35);
			this.L_Version.Name = "L_Version";
			this.L_Version.Padding = new System.Windows.Forms.Padding(2);
			this.L_Version.Size = new System.Drawing.Size(38, 16);
			this.L_Version.TabIndex = 27;
			this.L_Version.Text = "Version";
			this.L_Version.Click += new System.EventHandler(this.L_Version_Click);
			// 
			// PB_TMDb
			// 
			this.PB_TMDb.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.PB_TMDb.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_TMDb.Image = global::TVShowsCalendar.Properties.Resources.PoweredByTMDb;
			this.PB_TMDb.Location = new System.Drawing.Point(37, 0);
			this.PB_TMDb.Name = "PB_TMDb";
			this.PB_TMDb.Size = new System.Drawing.Size(90, 36);
			this.PB_TMDb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_TMDb.TabIndex = 31;
			this.PB_TMDb.TabStop = false;
			this.PB_TMDb.Click += new System.EventHandler(this.PB_TMDb_Click);
			this.PB_TMDb.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_TMDb_Paint);
			// 
			// notifyIcon
			// 
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Shows Calendar";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
			// 
			// PI_About
			// 
			this.PI_About.ForceReopen = false;
			this.PI_About.Group = "Other";
			this.PI_About.Icon = global::TVShowsCalendar.Properties.Resources.Tiny_Info;
			this.PI_About.Selected = false;
			this.PI_About.Text = "About";
			this.PI_About.OnClick += new System.Windows.Forms.MouseEventHandler(this.PI_About_OnClick);
			// 
			// Dashboard
			// 
			this.ClientSize = new System.Drawing.Size(1065, 565);
			this.CloseForm = false;
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.FormIcon = global::TVShowsCalendar.Properties.Resources.Icon_32;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = true;
			this.MinimizeBox = true;
			this.MinimumSize = new System.Drawing.Size(765, 445);
			this.Name = "Dashboard";
			this.SidebarItems = new SlickControls.Panels.PanelItem[] {
        this.PI_Dashboard,
        this.PI_Shows,
        this.PI_Movies,
        this.PI_Watch,
        this.PI_Library,
        this.PI_Settings,
        this.PI_LibraryFolders,
        this.PI_About};
			this.Text = "Shows Calendar";
			this.Load += new System.EventHandler(this.Dashboard_Load);
			this.base_P_SideControls.ResumeLayout(false);
			this.base_P_SideControls.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_TMDb)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private SlickControls.Panels.PanelItem PI_Settings;
		private SlickControls.Panels.PanelItem PI_LibraryFolders;
		private SlickControls.Panels.PanelItem PI_Shows;
		private SlickControls.Panels.PanelItem PI_Movies;
		private SlickControls.Panels.PanelItem PI_Library;
		private SlickControls.Panels.PanelItem PI_Watch;
		private SlickControls.Panels.PanelItem PI_Dashboard;
		private System.Windows.Forms.Label L_Icons8;
		private System.Windows.Forms.Label L_Version;
		private System.Windows.Forms.PictureBox PB_TMDb;
		public System.Windows.Forms.NotifyIcon notifyIcon;
		private SlickControls.Panels.PanelItem PI_About;
	}
}