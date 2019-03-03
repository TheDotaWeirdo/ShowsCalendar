namespace ShowsCalendar.Panels
{
	partial class PC_Watch
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
			this.P_Tabs = new System.Windows.Forms.Panel();
			this.verticalScroll = new SlickControls.Controls.SlickScroll();
			this.panel1 = new System.Windows.Forms.Panel();
			this.TLP_NoShows = new System.Windows.Forms.TableLayoutPanel();
			this.L_NoShows = new System.Windows.Forms.Label();
			this.L_NoShowsInfo = new System.Windows.Forms.Label();
			this.SP_OnDeck = new SlickControls.Controls.SlickSectionPanel();
			this.SP_ContinueEps = new SlickControls.Controls.SlickSectionPanel();
			this.SP_ContinueMovies = new SlickControls.Controls.SlickSectionPanel();
			this.SP_StartShows = new SlickControls.Controls.SlickSectionPanel();
			this.SP_StartMovies = new SlickControls.Controls.SlickSectionPanel();
			this.SP_RewatchEps = new SlickControls.Controls.SlickSectionPanel();
			this.P_Tabs.SuspendLayout();
			this.panel1.SuspendLayout();
			this.TLP_NoShows.SuspendLayout();
			this.SuspendLayout();
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Controls.Add(this.SP_RewatchEps);
			this.P_Tabs.Controls.Add(this.SP_StartMovies);
			this.P_Tabs.Controls.Add(this.SP_StartShows);
			this.P_Tabs.Controls.Add(this.SP_ContinueMovies);
			this.P_Tabs.Controls.Add(this.SP_ContinueEps);
			this.P_Tabs.Controls.Add(this.SP_OnDeck);
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.MinimumSize = new System.Drawing.Size(500, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(500, 324);
			this.P_Tabs.TabIndex = 12;
			// 
			// verticalScroll
			// 
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.P_Tabs;
			this.verticalScroll.Location = new System.Drawing.Point(779, 30);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(4, 408);
			this.verticalScroll.TabIndex = 13;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.P_Tabs);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(5, 30);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(778, 408);
			this.panel1.TabIndex = 14;
			// 
			// TLP_NoShows
			// 
			this.TLP_NoShows.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.TLP_NoShows.AutoSize = true;
			this.TLP_NoShows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_NoShows.ColumnCount = 1;
			this.TLP_NoShows.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoShows.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoShows.Controls.Add(this.L_NoShows, 0, 0);
			this.TLP_NoShows.Controls.Add(this.L_NoShowsInfo, 0, 1);
			this.TLP_NoShows.Location = new System.Drawing.Point(237, 159);
			this.TLP_NoShows.Name = "TLP_NoShows";
			this.TLP_NoShows.RowCount = 2;
			this.TLP_NoShows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoShows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoShows.Size = new System.Drawing.Size(309, 120);
			this.TLP_NoShows.TabIndex = 30;
			// 
			// L_NoShows
			// 
			this.L_NoShows.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoShows.AutoSize = true;
			this.L_NoShows.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_NoShows.Location = new System.Drawing.Point(95, 18);
			this.L_NoShows.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
			this.L_NoShows.Name = "L_NoShows";
			this.L_NoShows.Size = new System.Drawing.Size(119, 17);
			this.L_NoShows.TabIndex = 1;
			this.L_NoShows.Text = "Nothing to Watch";
			// 
			// L_NoShowsInfo
			// 
			this.L_NoShowsInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoShowsInfo.AutoSize = true;
			this.L_NoShowsInfo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic);
			this.L_NoShowsInfo.Location = new System.Drawing.Point(0, 60);
			this.L_NoShowsInfo.Margin = new System.Windows.Forms.Padding(0);
			this.L_NoShowsInfo.Name = "L_NoShowsInfo";
			this.L_NoShowsInfo.Size = new System.Drawing.Size(309, 60);
			this.L_NoShowsInfo.TabIndex = 1;
			this.L_NoShowsInfo.Text = "You don\'t seem to have anything to watch right now.\r\n\r\nTry adding more Shows/Movi" +
    "es, download episodes or \r\ncheck that your library is set up.";
			this.L_NoShowsInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SP_OnDeck
			// 
			this.SP_OnDeck.Active = true;
			this.SP_OnDeck.AutoHide = true;
			this.SP_OnDeck.AutoSize = true;
			this.SP_OnDeck.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_OnDeck.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_OnDeck.Flavor = null;
			this.SP_OnDeck.Icon = global::ShowsCalendar.Properties.Resources.Big_Play;
			this.SP_OnDeck.Info = "";
			this.SP_OnDeck.Location = new System.Drawing.Point(0, 0);
			this.SP_OnDeck.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_OnDeck.Name = "SP_OnDeck";
			this.SP_OnDeck.Size = new System.Drawing.Size(500, 54);
			this.SP_OnDeck.TabIndex = 14;
			this.SP_OnDeck.Text = "On-Deck";
			// 
			// SP_ContinueEps
			// 
			this.SP_ContinueEps.Active = false;
			this.SP_ContinueEps.AutoHide = true;
			this.SP_ContinueEps.AutoSize = true;
			this.SP_ContinueEps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_ContinueEps.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_ContinueEps.Flavor = null;
			this.SP_ContinueEps.Icon = global::ShowsCalendar.Properties.Resources.Big_TV;
			this.SP_ContinueEps.Info = "";
			this.SP_ContinueEps.Location = new System.Drawing.Point(0, 54);
			this.SP_ContinueEps.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_ContinueEps.Name = "SP_ContinueEps";
			this.SP_ContinueEps.Size = new System.Drawing.Size(500, 54);
			this.SP_ContinueEps.TabIndex = 15;
			this.SP_ContinueEps.Text = "Continue Episodes";
			// 
			// SP_ContinueMovies
			// 
			this.SP_ContinueMovies.Active = false;
			this.SP_ContinueMovies.AutoHide = true;
			this.SP_ContinueMovies.AutoSize = true;
			this.SP_ContinueMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_ContinueMovies.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_ContinueMovies.Flavor = null;
			this.SP_ContinueMovies.Icon = global::ShowsCalendar.Properties.Resources.Big_Movie;
			this.SP_ContinueMovies.Info = "";
			this.SP_ContinueMovies.Location = new System.Drawing.Point(0, 108);
			this.SP_ContinueMovies.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_ContinueMovies.Name = "SP_ContinueMovies";
			this.SP_ContinueMovies.Size = new System.Drawing.Size(500, 54);
			this.SP_ContinueMovies.TabIndex = 16;
			this.SP_ContinueMovies.Text = "Continue Movies";
			// 
			// SP_StartShows
			// 
			this.SP_StartShows.Active = false;
			this.SP_StartShows.AutoHide = true;
			this.SP_StartShows.AutoSize = true;
			this.SP_StartShows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_StartShows.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_StartShows.Flavor = null;
			this.SP_StartShows.Icon = global::ShowsCalendar.Properties.Resources.Big_TV;
			this.SP_StartShows.Info = "";
			this.SP_StartShows.Location = new System.Drawing.Point(0, 162);
			this.SP_StartShows.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_StartShows.Name = "SP_StartShows";
			this.SP_StartShows.Size = new System.Drawing.Size(500, 54);
			this.SP_StartShows.TabIndex = 17;
			this.SP_StartShows.Text = "Start Shows";
			// 
			// SP_StartMovies
			// 
			this.SP_StartMovies.Active = false;
			this.SP_StartMovies.AutoHide = true;
			this.SP_StartMovies.AutoSize = true;
			this.SP_StartMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_StartMovies.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_StartMovies.Flavor = null;
			this.SP_StartMovies.Icon = global::ShowsCalendar.Properties.Resources.Big_Movie;
			this.SP_StartMovies.Info = "";
			this.SP_StartMovies.Location = new System.Drawing.Point(0, 216);
			this.SP_StartMovies.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_StartMovies.Name = "SP_StartMovies";
			this.SP_StartMovies.Size = new System.Drawing.Size(500, 54);
			this.SP_StartMovies.TabIndex = 18;
			this.SP_StartMovies.Text = "Start Movies";
			// 
			// SP_RewatchEps
			// 
			this.SP_RewatchEps.Active = false;
			this.SP_RewatchEps.AutoHide = true;
			this.SP_RewatchEps.AutoSize = true;
			this.SP_RewatchEps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_RewatchEps.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_RewatchEps.Flavor = null;
			this.SP_RewatchEps.Icon = global::ShowsCalendar.Properties.Resources.Big_TV;
			this.SP_RewatchEps.Info = "";
			this.SP_RewatchEps.Location = new System.Drawing.Point(0, 270);
			this.SP_RewatchEps.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_RewatchEps.Name = "SP_RewatchEps";
			this.SP_RewatchEps.Size = new System.Drawing.Size(500, 54);
			this.SP_RewatchEps.TabIndex = 19;
			this.SP_RewatchEps.Text = "Re-Watch Episodes";
			// 
			// PC_Watch
			// 
			SP_OnDeck.Flavor = new string[]
			{
				"Where were we?",
				"Must. Binge. More.",
				"Yes, those are the things you were watching.",
				"Gotcha covered, buddy!",
				"The temptation is small, but your will is weak, press on one."
			};

			SP_ContinueEps.Flavor = new string[]
			{
				"Oh yeah.. Someone forgot about those..",
				"My miiiind is telling me noooo, but my body, my boooody is telling me YESS!",
				"Pick up that episode you started!",
				"Pick up that episode you started god damn-it!",
				"Remember.. mee?",
				"Oh shoot, someone fell off his schedule.."
			};

			SP_ContinueMovies.Flavor = new string[]
			{
				"Oh yeah.. Someone forgot about those..",
				"How does someone forget a movie?",
				"Pick up that movie you started!",
				"Pick up that movie you started god damn-it!",
				"Movies. Right. Gotta continue those."
			};

			SP_StartShows.Flavor = new string[]
			{
				"Oh boy, oh boy!",
				"Shiny!",
				"Start something new.",
				"You know those episodes above ain't got what it takes."
			};

			SP_StartMovies.Flavor = new string[]
			{
				"Laiche Vitrine.",
				"Start something new.",
			};

			SP_RewatchEps.Flavor = new string[]
			{
				"Be sure you haven't missed anything"
			};

			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TLP_NoShows);
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.panel1);
			this.Name = "PC_Watch";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Text = "Watch";
			this.Resize += new System.EventHandler(this.PC_Watch_Resize);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.Controls.SetChildIndex(this.TLP_NoShows, 0);
			this.P_Tabs.ResumeLayout(false);
			this.P_Tabs.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.TLP_NoShows.ResumeLayout(false);
			this.TLP_NoShows.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel P_Tabs;
		private SlickControls.Controls.SlickScroll verticalScroll;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel TLP_NoShows;
		private System.Windows.Forms.Label L_NoShows;
		private System.Windows.Forms.Label L_NoShowsInfo;
		private SlickControls.Controls.SlickSectionPanel SP_RewatchEps;
		private SlickControls.Controls.SlickSectionPanel SP_StartMovies;
		private SlickControls.Controls.SlickSectionPanel SP_StartShows;
		private SlickControls.Controls.SlickSectionPanel SP_ContinueMovies;
		private SlickControls.Controls.SlickSectionPanel SP_ContinueEps;
		private SlickControls.Controls.SlickSectionPanel SP_OnDeck;
	}
}
