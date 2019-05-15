namespace ShowsCalendar.Panels
{
	partial class PC_Dashboard
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.P_Tabs = new System.Windows.Forms.Panel();
			this.SP_UpcomingMovies = new SlickControls.Controls.SlickSectionPanel();
			this.SP_UpcomingEps = new SlickControls.Controls.SlickSectionPanel();
			this.SP_RecentMovies = new SlickControls.Controls.SlickSectionPanel();
			this.SP_RecentEps = new SlickControls.Controls.SlickSectionPanel();
			this.SP_ContinueWatching = new SlickControls.Controls.SlickSectionPanel();
			this.SP_OnDeck = new SlickControls.Controls.SlickSectionPanel();
			this.verticalScroll = new SlickControls.Controls.SlickScroll();
			this.panel1.SuspendLayout();
			this.P_Tabs.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.P_Tabs);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(5, 30);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(775, 439);
			this.panel1.TabIndex = 15;
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Controls.Add(this.SP_UpcomingMovies);
			this.P_Tabs.Controls.Add(this.SP_UpcomingEps);
			this.P_Tabs.Controls.Add(this.SP_RecentMovies);
			this.P_Tabs.Controls.Add(this.SP_RecentEps);
			this.P_Tabs.Controls.Add(this.SP_ContinueWatching);
			this.P_Tabs.Controls.Add(this.SP_OnDeck);
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.MinimumSize = new System.Drawing.Size(500, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(500, 324);
			this.P_Tabs.TabIndex = 12;
			// 
			// SP_UpcomingMovies
			// 
			this.SP_UpcomingMovies.Active = false;
			this.SP_UpcomingMovies.AutoHide = true;
			this.SP_UpcomingMovies.AutoSize = true;
			this.SP_UpcomingMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_UpcomingMovies.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_UpcomingMovies.Flavor = null;
			this.SP_UpcomingMovies.Icon = global::ShowsCalendar.Properties.Resources.Big_Movie;
			this.SP_UpcomingMovies.Info = "Movies with a definitive release date";
			this.SP_UpcomingMovies.Location = new System.Drawing.Point(0, 270);
			this.SP_UpcomingMovies.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_UpcomingMovies.Name = "SP_UpcomingMovies";
			this.SP_UpcomingMovies.Size = new System.Drawing.Size(500, 54);
			this.SP_UpcomingMovies.TabIndex = 10;
			this.SP_UpcomingMovies.Text = "Upcoming Movies";
			// 
			// SP_UpcomingEps
			// 
			this.SP_UpcomingEps.Active = false;
			this.SP_UpcomingEps.AutoHide = true;
			this.SP_UpcomingEps.AutoSize = true;
			this.SP_UpcomingEps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_UpcomingEps.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_UpcomingEps.Flavor = null;
			this.SP_UpcomingEps.Icon = global::ShowsCalendar.Properties.Resources.Big_TV;
			this.SP_UpcomingEps.Info = "Episodes to look forward to within the next month";
			this.SP_UpcomingEps.Location = new System.Drawing.Point(0, 216);
			this.SP_UpcomingEps.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_UpcomingEps.Name = "SP_UpcomingEps";
			this.SP_UpcomingEps.Size = new System.Drawing.Size(500, 54);
			this.SP_UpcomingEps.TabIndex = 9;
			this.SP_UpcomingEps.Text = "Upcoming Episodes";
			// 
			// SP_RecentMovies
			// 
			this.SP_RecentMovies.Active = false;
			this.SP_RecentMovies.AutoHide = true;
			this.SP_RecentMovies.AutoSize = true;
			this.SP_RecentMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_RecentMovies.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_RecentMovies.Flavor = null;
			this.SP_RecentMovies.Icon = global::ShowsCalendar.Properties.Resources.Big_Popcorn;
			this.SP_RecentMovies.Info = "Movies that were released not so long ago";
			this.SP_RecentMovies.Location = new System.Drawing.Point(0, 162);
			this.SP_RecentMovies.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_RecentMovies.Name = "SP_RecentMovies";
			this.SP_RecentMovies.Size = new System.Drawing.Size(500, 54);
			this.SP_RecentMovies.TabIndex = 8;
			this.SP_RecentMovies.Text = "Recently Premiered";
			// 
			// SP_RecentEps
			// 
			this.SP_RecentEps.Active = false;
			this.SP_RecentEps.AutoHide = true;
			this.SP_RecentEps.AutoSize = true;
			this.SP_RecentEps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_RecentEps.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_RecentEps.Flavor = new string[] {
        "Episodes that came out in the past week",
        "Episodes straight out of last week",
        "Freshest episodes around"};
			this.SP_RecentEps.Icon = global::ShowsCalendar.Properties.Resources.Big_Airing;
			this.SP_RecentEps.Info = "Episodes straight out of last week";
			this.SP_RecentEps.Location = new System.Drawing.Point(0, 108);
			this.SP_RecentEps.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_RecentEps.Name = "SP_RecentEps";
			this.SP_RecentEps.Size = new System.Drawing.Size(500, 54);
			this.SP_RecentEps.TabIndex = 7;
			this.SP_RecentEps.Text = "Recently Aired";
			// 
			// SP_ContinueWatching
			// 
			this.SP_ContinueWatching.Active = false;
			this.SP_ContinueWatching.AutoHide = true;
			this.SP_ContinueWatching.AutoSize = true;
			this.SP_ContinueWatching.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_ContinueWatching.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_ContinueWatching.Flavor = new string[] {
        "Episodes you should download",
        "Download the episodes and continue the binge!",
        "Ready to put on the Deck"};
			this.SP_ContinueWatching.Icon = global::ShowsCalendar.Properties.Resources.Big_ViewPlay;
			this.SP_ContinueWatching.Info = "Ready to put on the Deck";
			this.SP_ContinueWatching.Location = new System.Drawing.Point(0, 54);
			this.SP_ContinueWatching.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_ContinueWatching.Name = "SP_ContinueWatching";
			this.SP_ContinueWatching.Size = new System.Drawing.Size(500, 54);
			this.SP_ContinueWatching.TabIndex = 11;
			this.SP_ContinueWatching.Text = "Continue Watching";
			// 
			// SP_OnDeck
			// 
			this.SP_OnDeck.Active = true;
			this.SP_OnDeck.AutoHide = true;
			this.SP_OnDeck.AutoSize = true;
			this.SP_OnDeck.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_OnDeck.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_OnDeck.Flavor = new string[] {
        "Where were we?",
        "Must. Binge. More.",
        "Yes, those are the things you were watching.",
        "Gotcha covered, buddy!",
        "The temptation is small, but your will is weak, press on one."};
			this.SP_OnDeck.Icon = global::ShowsCalendar.Properties.Resources.Big_Play;
			this.SP_OnDeck.Info = "The temptation is small, but your will is weak, press on one.";
			this.SP_OnDeck.Location = new System.Drawing.Point(0, 0);
			this.SP_OnDeck.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_OnDeck.Name = "SP_OnDeck";
			this.SP_OnDeck.Size = new System.Drawing.Size(500, 54);
			this.SP_OnDeck.TabIndex = 6;
			this.SP_OnDeck.Text = "On-Deck";
			// 
			// verticalScroll
			// 
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.P_Tabs;
			this.verticalScroll.Location = new System.Drawing.Point(776, 30);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(5, 385);
			this.verticalScroll.SizeSource = null;
			this.verticalScroll.Style = SlickControls.Controls.StyleType.Vertical;
			this.verticalScroll.TabIndex = 16;
			// 
			// PC_Dashboard
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.panel1);
			this.Name = "PC_Dashboard";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Size = new System.Drawing.Size(780, 469);
			this.Text = "Dashboard";
			this.Resize += new System.EventHandler(this.PC_Dashboard_Resize);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.P_Tabs.ResumeLayout(false);
			this.P_Tabs.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel P_Tabs;
		private SlickControls.Controls.SlickScroll verticalScroll;
		private SlickControls.Controls.SlickSectionPanel SP_UpcomingMovies;
		private SlickControls.Controls.SlickSectionPanel SP_UpcomingEps;
		private SlickControls.Controls.SlickSectionPanel SP_RecentMovies;
		private SlickControls.Controls.SlickSectionPanel SP_RecentEps;
		private SlickControls.Controls.SlickSectionPanel SP_OnDeck;
		private SlickControls.Controls.SlickSectionPanel SP_ContinueWatching;
	}
}
