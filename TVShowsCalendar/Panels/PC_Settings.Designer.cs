namespace ShowsCalendar.Panels
{
	partial class PC_Settings
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
            this.P_OptionContainer = new System.Windows.Forms.Panel();
            this.verticalScroll1 = new SlickControls.Controls.SlickScroll();
            this.P_Options = new System.Windows.Forms.Panel();
            this.P_Watch = new System.Windows.Forms.Panel();
            this.P_Download = new System.Windows.Forms.Panel();
            this.P_App = new System.Windows.Forms.Panel();
            this.P_General = new System.Windows.Forms.Panel();
            this.TLP_Tabs = new System.Windows.Forms.TableLayoutPanel();
            this.T_App = new SlickControls.Controls.SlickTab();
            this.T_Watch = new SlickControls.Controls.SlickTab();
            this.T_Downloads = new SlickControls.Controls.SlickTab();
            this.T_General = new SlickControls.Controls.SlickTab();
            this.B_Done = new SlickControls.Controls.SlickButton();
            this.OC_DisableMini = new ShowsCalendar.OptionControl();
            this.OC_AutoPauseScroll = new ShowsCalendar.OptionControl();
            this.OC_AutoEpPLay = new ShowsCalendar.OptionControl();
            this.OC_AutoEpSwitch = new ShowsCalendar.OptionControl();
            this.OC_BackwardTime = new ShowsCalendar.OptionControl();
            this.OC_ForwardTime = new ShowsCalendar.OptionControl();
            this.OC_FullScreenPlayer = new ShowsCalendar.OptionControl();
            this.OC_Quality = new ShowsCalendar.OptionControl();
            this.OC_DownloadOption = new ShowsCalendar.OptionControl();
            this.OC_DownloadBehavior = new ShowsCalendar.OptionControl();
            this.OC_NotificationSound = new ShowsCalendar.OptionControl();
            this.OC_EpNotification = new ShowsCalendar.OptionControl();
            this.OC_StartMode = new ShowsCalendar.OptionControl();
            this.OC_LaunchWithWindows = new ShowsCalendar.OptionControl();
            this.OC_EpBehavior = new ShowsCalendar.OptionControl();
            this.OC_FinaleWarning = new ShowsCalendar.OptionControl();
            this.OC_UnwatchedBanner = new ShowsCalendar.OptionControl();
            this.OC_DiscoverPages = new ShowsCalendar.OptionControl();
            this.PC_MoviesOrder = new ShowsCalendar.OptionControl();
            this.PC_ShowsOrder = new ShowsCalendar.OptionControl();
            this.P_OptionContainer.SuspendLayout();
            this.P_Options.SuspendLayout();
            this.P_Watch.SuspendLayout();
            this.P_Download.SuspendLayout();
            this.P_App.SuspendLayout();
            this.P_General.SuspendLayout();
            this.TLP_Tabs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.B_Done)).BeginInit();
            this.SuspendLayout();
            // 
            // P_OptionContainer
            // 
            this.P_OptionContainer.Controls.Add(this.verticalScroll1);
            this.P_OptionContainer.Controls.Add(this.P_Options);
            this.P_OptionContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.P_OptionContainer.Location = new System.Drawing.Point(0, 32);
            this.P_OptionContainer.Margin = new System.Windows.Forms.Padding(0);
            this.P_OptionContainer.Name = "P_OptionContainer";
            this.P_OptionContainer.Size = new System.Drawing.Size(1082, 1542);
            this.P_OptionContainer.TabIndex = 34;
            this.P_OptionContainer.Resize += new System.EventHandler(this.P_OptionContainer_Resize);
            // 
            // verticalScroll1
            // 
            this.verticalScroll1.Dock = System.Windows.Forms.DockStyle.Right;
            this.verticalScroll1.LinkedControl = this.P_Options;
            this.verticalScroll1.Location = new System.Drawing.Point(1077, 0);
            this.verticalScroll1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.verticalScroll1.Name = "verticalScroll1";
            this.verticalScroll1.Size = new System.Drawing.Size(5, 1542);
            this.verticalScroll1.SizeSource = null;
            this.verticalScroll1.Style = SlickControls.Controls.StyleType.Vertical;
            this.verticalScroll1.TabIndex = 35;
            // 
            // P_Options
            // 
            this.P_Options.AutoSize = true;
            this.P_Options.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.P_Options.Controls.Add(this.P_Watch);
            this.P_Options.Controls.Add(this.P_Download);
            this.P_Options.Controls.Add(this.P_App);
            this.P_Options.Controls.Add(this.P_General);
            this.P_Options.Location = new System.Drawing.Point(0, 0);
            this.P_Options.MinimumSize = new System.Drawing.Size(600, 0);
            this.P_Options.Name = "P_Options";
            this.P_Options.Size = new System.Drawing.Size(600, 1240);
            this.P_Options.TabIndex = 34;
            // 
            // P_Watch
            // 
            this.P_Watch.AutoSize = true;
            this.P_Watch.Controls.Add(this.OC_DisableMini);
            this.P_Watch.Controls.Add(this.OC_AutoPauseScroll);
            this.P_Watch.Controls.Add(this.OC_AutoEpPLay);
            this.P_Watch.Controls.Add(this.OC_AutoEpSwitch);
            this.P_Watch.Controls.Add(this.OC_BackwardTime);
            this.P_Watch.Controls.Add(this.OC_ForwardTime);
            this.P_Watch.Controls.Add(this.OC_FullScreenPlayer);
            this.P_Watch.Dock = System.Windows.Forms.DockStyle.Top;
            this.P_Watch.Location = new System.Drawing.Point(0, 806);
            this.P_Watch.Name = "P_Watch";
            this.P_Watch.Size = new System.Drawing.Size(600, 434);
            this.P_Watch.TabIndex = 37;
            this.P_Watch.Visible = false;
            // 
            // P_Download
            // 
            this.P_Download.AutoSize = true;
            this.P_Download.Controls.Add(this.OC_Quality);
            this.P_Download.Controls.Add(this.OC_DownloadOption);
            this.P_Download.Controls.Add(this.OC_DownloadBehavior);
            this.P_Download.Dock = System.Windows.Forms.DockStyle.Top;
            this.P_Download.Location = new System.Drawing.Point(0, 620);
            this.P_Download.Name = "P_Download";
            this.P_Download.Size = new System.Drawing.Size(600, 186);
            this.P_Download.TabIndex = 23;
            this.P_Download.Visible = false;
            // 
            // P_App
            // 
            this.P_App.AutoSize = true;
            this.P_App.Controls.Add(this.OC_NotificationSound);
            this.P_App.Controls.Add(this.OC_EpNotification);
            this.P_App.Controls.Add(this.OC_StartMode);
            this.P_App.Controls.Add(this.OC_LaunchWithWindows);
            this.P_App.Dock = System.Windows.Forms.DockStyle.Top;
            this.P_App.Location = new System.Drawing.Point(0, 372);
            this.P_App.Name = "P_App";
            this.P_App.Size = new System.Drawing.Size(600, 248);
            this.P_App.TabIndex = 22;
            this.P_App.Visible = false;
            // 
            // P_General
            // 
            this.P_General.AutoSize = true;
            this.P_General.Controls.Add(this.OC_EpBehavior);
            this.P_General.Controls.Add(this.OC_FinaleWarning);
            this.P_General.Controls.Add(this.OC_UnwatchedBanner);
            this.P_General.Controls.Add(this.OC_DiscoverPages);
            this.P_General.Controls.Add(this.PC_MoviesOrder);
            this.P_General.Controls.Add(this.PC_ShowsOrder);
            this.P_General.Dock = System.Windows.Forms.DockStyle.Top;
            this.P_General.Location = new System.Drawing.Point(0, 0);
            this.P_General.Name = "P_General";
            this.P_General.Size = new System.Drawing.Size(600, 372);
            this.P_General.TabIndex = 21;
            // 
            // TLP_Tabs
            // 
            this.TLP_Tabs.ColumnCount = 6;
            this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Tabs.Controls.Add(this.T_App, 2, 0);
            this.TLP_Tabs.Controls.Add(this.T_Watch, 4, 0);
            this.TLP_Tabs.Controls.Add(this.T_Downloads, 3, 0);
            this.TLP_Tabs.Controls.Add(this.T_General, 1, 0);
            this.TLP_Tabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.TLP_Tabs.Location = new System.Drawing.Point(0, 0);
            this.TLP_Tabs.Name = "TLP_Tabs";
            this.TLP_Tabs.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.TLP_Tabs.RowCount = 1;
            this.TLP_Tabs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Tabs.Size = new System.Drawing.Size(1082, 32);
            this.TLP_Tabs.TabIndex = 18;
            // 
            // T_App
            // 
            this.T_App.Cursor = System.Windows.Forms.Cursors.Hand;
            this.T_App.Dock = System.Windows.Forms.DockStyle.Fill;
            this.T_App.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T_App.Hovered = false;
            this.T_App.Icon = global::ShowsCalendar.Properties.Resources.Tiny_App;
            this.T_App.Location = new System.Drawing.Point(391, 6);
            this.T_App.Margin = new System.Windows.Forms.Padding(0);
            this.T_App.Name = "T_App";
            this.T_App.Selected = false;
            this.T_App.Size = new System.Drawing.Size(150, 26);
            this.T_App.TabIndex = 4;
            this.T_App.Text = "Application";
            this.T_App.TabSelected += new System.EventHandler(this.Tile_Click);
            // 
            // T_Watch
            // 
            this.T_Watch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.T_Watch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.T_Watch.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T_Watch.Hovered = false;
            this.T_Watch.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Play;
            this.T_Watch.Location = new System.Drawing.Point(691, 6);
            this.T_Watch.Margin = new System.Windows.Forms.Padding(0);
            this.T_Watch.Name = "T_Watch";
            this.T_Watch.Selected = false;
            this.T_Watch.Size = new System.Drawing.Size(150, 26);
            this.T_Watch.TabIndex = 3;
            this.T_Watch.Text = "Video Player";
            this.T_Watch.TabSelected += new System.EventHandler(this.Tile_Click);
            // 
            // T_Downloads
            // 
            this.T_Downloads.Cursor = System.Windows.Forms.Cursors.Hand;
            this.T_Downloads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.T_Downloads.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T_Downloads.Hovered = false;
            this.T_Downloads.Icon = global::ShowsCalendar.Properties.Resources.Tiny_CloudDownload;
            this.T_Downloads.Location = new System.Drawing.Point(541, 6);
            this.T_Downloads.Margin = new System.Windows.Forms.Padding(0);
            this.T_Downloads.Name = "T_Downloads";
            this.T_Downloads.Selected = false;
            this.T_Downloads.Size = new System.Drawing.Size(150, 26);
            this.T_Downloads.TabIndex = 1;
            this.T_Downloads.Text = "Downloads";
            this.T_Downloads.TabSelected += new System.EventHandler(this.Tile_Click);
            // 
            // T_General
            // 
            this.T_General.Cursor = System.Windows.Forms.Cursors.Hand;
            this.T_General.Dock = System.Windows.Forms.DockStyle.Fill;
            this.T_General.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T_General.Hovered = false;
            this.T_General.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Settings;
            this.T_General.Location = new System.Drawing.Point(241, 6);
            this.T_General.Margin = new System.Windows.Forms.Padding(0);
            this.T_General.Name = "T_General";
            this.T_General.Selected = true;
            this.T_General.Size = new System.Drawing.Size(150, 26);
            this.T_General.TabIndex = 0;
            this.T_General.Text = "General";
            this.T_General.TabSelected += new System.EventHandler(this.Tile_Click);
            // 
            // B_Done
            // 
            this.B_Done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Done.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.B_Done.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.B_Done.ColorShade = null;
            this.B_Done.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B_Done.IconSize = 16;
            this.B_Done.Image = global::ShowsCalendar.Properties.Resources.Tiny_Ok;
            this.B_Done.Location = new System.Drawing.Point(932, 1531);
            this.B_Done.Margin = new System.Windows.Forms.Padding(0, 0, 15, 15);
            this.B_Done.Name = "B_Done";
            this.B_Done.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.B_Done.Size = new System.Drawing.Size(135, 28);
            this.B_Done.TabIndex = 107;
            this.B_Done.Text = "APPLY CHANGES";
            this.B_Done.Click += new System.EventHandler(this.B_Apply_Click);
            // 
            // OC_DisableMini
            // 
            this.OC_DisableMini.Checked = true;
            this.OC_DisableMini.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_DisableMini.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_DisableMini.Location = new System.Drawing.Point(0, 372);
            this.OC_DisableMini.Margin = new System.Windows.Forms.Padding(0);
            this.OC_DisableMini.Name = "OC_DisableMini";
            this.OC_DisableMini.OptionList = new string[0];
            this.OC_DisableMini.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_DisableMini.SelectedOption = "";
            this.OC_DisableMini.SettingType = ShowsCalendar.Classes.Setting.Watch;
            this.OC_DisableMini.Size = new System.Drawing.Size(600, 62);
            this.OC_DisableMini.TabIndex = 8;
            this.OC_DisableMini.Text_CheckBox_Checked = "Enabled";
            this.OC_DisableMini.Text_CheckBox_Unchecked = "Disabled";
            this.OC_DisableMini.Text_Info = "When in the mini-player, disables pausing by clicking on the video, hides the tim" +
    "e slider and delays the controls poping up.";
            this.OC_DisableMini.Text_Title = "Lazy Mini-Player";
            // 
            // OC_AutoPauseScroll
            // 
            this.OC_AutoPauseScroll.Checked = true;
            this.OC_AutoPauseScroll.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_AutoPauseScroll.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_AutoPauseScroll.Location = new System.Drawing.Point(0, 310);
            this.OC_AutoPauseScroll.Margin = new System.Windows.Forms.Padding(0);
            this.OC_AutoPauseScroll.Name = "OC_AutoPauseScroll";
            this.OC_AutoPauseScroll.OptionList = new string[0];
            this.OC_AutoPauseScroll.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_AutoPauseScroll.SelectedOption = "";
            this.OC_AutoPauseScroll.SettingType = ShowsCalendar.Classes.Setting.Watch;
            this.OC_AutoPauseScroll.Size = new System.Drawing.Size(600, 62);
            this.OC_AutoPauseScroll.TabIndex = 7;
            this.OC_AutoPauseScroll.Text_CheckBox_Checked = "Auto-Pause";
            this.OC_AutoPauseScroll.Text_CheckBox_Unchecked = "Do Nothing";
            this.OC_AutoPauseScroll.Text_Info = "Automatically pause/play when scrolling down to see more info about the episode/m" +
    "ovie.";
            this.OC_AutoPauseScroll.Text_Title = "Auto-Pause on Scroll";
            // 
            // OC_AutoEpPLay
            // 
            this.OC_AutoEpPLay.Checked = true;
            this.OC_AutoEpPLay.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_AutoEpPLay.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_AutoEpPLay.Location = new System.Drawing.Point(0, 248);
            this.OC_AutoEpPLay.Margin = new System.Windows.Forms.Padding(0);
            this.OC_AutoEpPLay.Name = "OC_AutoEpPLay";
            this.OC_AutoEpPLay.OptionList = new string[0];
            this.OC_AutoEpPLay.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_AutoEpPLay.SelectedOption = "";
            this.OC_AutoEpPLay.SettingType = ShowsCalendar.Classes.Setting.Watch;
            this.OC_AutoEpPLay.Size = new System.Drawing.Size(600, 62);
            this.OC_AutoEpPLay.TabIndex = 6;
            this.OC_AutoEpPLay.Text_CheckBox_Checked = "Play Episode";
            this.OC_AutoEpPLay.Text_CheckBox_Unchecked = "Pause Episode";
            this.OC_AutoEpPLay.Text_Info = "Automatically starts playing the episode when it\'s switched to.";
            this.OC_AutoEpPLay.Text_Title = "Start Playing on Switch";
            this.OC_AutoEpPLay.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_AutoEpSwitch
            // 
            this.OC_AutoEpSwitch.Checked = true;
            this.OC_AutoEpSwitch.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_AutoEpSwitch.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_AutoEpSwitch.Location = new System.Drawing.Point(0, 186);
            this.OC_AutoEpSwitch.Margin = new System.Windows.Forms.Padding(0);
            this.OC_AutoEpSwitch.Name = "OC_AutoEpSwitch";
            this.OC_AutoEpSwitch.OptionList = new string[0];
            this.OC_AutoEpSwitch.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_AutoEpSwitch.SelectedOption = "";
            this.OC_AutoEpSwitch.SettingType = ShowsCalendar.Classes.Setting.Watch;
            this.OC_AutoEpSwitch.Size = new System.Drawing.Size(600, 62);
            this.OC_AutoEpSwitch.TabIndex = 5;
            this.OC_AutoEpSwitch.Text_CheckBox_Checked = "Switch";
            this.OC_AutoEpSwitch.Text_CheckBox_Unchecked = "Do not Switch";
            this.OC_AutoEpSwitch.Text_Info = "Switches to the next episode when the one you\'re currently watching ends.";
            this.OC_AutoEpSwitch.Text_Title = "Automatic Episode Switching";
            this.OC_AutoEpSwitch.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_BackwardTime
            // 
            this.OC_BackwardTime.Checked = true;
            this.OC_BackwardTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_BackwardTime.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_BackwardTime.Location = new System.Drawing.Point(0, 124);
            this.OC_BackwardTime.Margin = new System.Windows.Forms.Padding(0);
            this.OC_BackwardTime.Name = "OC_BackwardTime";
            this.OC_BackwardTime.OptionList = new string[] {
        "60 seconds",
        "45 seconds",
        "30 seconds",
        "15 seconds",
        "10 seconds",
        "5 seconds"};
            this.OC_BackwardTime.OptionType = ShowsCalendar.OptionType.OptionList;
            this.OC_BackwardTime.SelectedOption = "5 seconds";
            this.OC_BackwardTime.SettingType = ShowsCalendar.Classes.Setting.Watch;
            this.OC_BackwardTime.Size = new System.Drawing.Size(600, 62);
            this.OC_BackwardTime.TabIndex = 3;
            this.OC_BackwardTime.Text_CheckBox_Checked = "Show All Downloads";
            this.OC_BackwardTime.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
            this.OC_BackwardTime.Text_Info = "Choose how much time to jump backward at once.";
            this.OC_BackwardTime.Text_Title = "Backward Time";
            this.OC_BackwardTime.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_ForwardTime
            // 
            this.OC_ForwardTime.Checked = true;
            this.OC_ForwardTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_ForwardTime.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_ForwardTime.Location = new System.Drawing.Point(0, 62);
            this.OC_ForwardTime.Margin = new System.Windows.Forms.Padding(0);
            this.OC_ForwardTime.Name = "OC_ForwardTime";
            this.OC_ForwardTime.OptionList = new string[] {
        "60 seconds",
        "45 seconds",
        "30 seconds",
        "15 seconds",
        "10 seconds",
        "5 seconds"};
            this.OC_ForwardTime.OptionType = ShowsCalendar.OptionType.OptionList;
            this.OC_ForwardTime.SelectedOption = "15 seconds";
            this.OC_ForwardTime.SettingType = ShowsCalendar.Classes.Setting.Watch;
            this.OC_ForwardTime.Size = new System.Drawing.Size(600, 62);
            this.OC_ForwardTime.TabIndex = 2;
            this.OC_ForwardTime.Text_CheckBox_Checked = "Show All Downloads";
            this.OC_ForwardTime.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
            this.OC_ForwardTime.Text_Info = "Choose how much time to jump forward at once.";
            this.OC_ForwardTime.Text_Title = "Forward Time";
            this.OC_ForwardTime.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_FullScreenPlayer
            // 
            this.OC_FullScreenPlayer.Checked = true;
            this.OC_FullScreenPlayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_FullScreenPlayer.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_FullScreenPlayer.Location = new System.Drawing.Point(0, 0);
            this.OC_FullScreenPlayer.Margin = new System.Windows.Forms.Padding(0);
            this.OC_FullScreenPlayer.Name = "OC_FullScreenPlayer";
            this.OC_FullScreenPlayer.OptionList = new string[0];
            this.OC_FullScreenPlayer.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_FullScreenPlayer.SelectedOption = "";
            this.OC_FullScreenPlayer.SettingType = ShowsCalendar.Classes.Setting.Watch;
            this.OC_FullScreenPlayer.Size = new System.Drawing.Size(600, 62);
            this.OC_FullScreenPlayer.TabIndex = 4;
            this.OC_FullScreenPlayer.Text_CheckBox_Checked = "Fullscreen Window";
            this.OC_FullScreenPlayer.Text_CheckBox_Unchecked = "Normal Window";
            this.OC_FullScreenPlayer.Text_Info = "Choose to start the video player in full-screen or in a normal window.";
            this.OC_FullScreenPlayer.Text_Title = "Download Options";
            this.OC_FullScreenPlayer.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_Quality
            // 
            this.OC_Quality.Checked = true;
            this.OC_Quality.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_Quality.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_Quality.Location = new System.Drawing.Point(0, 124);
            this.OC_Quality.Margin = new System.Windows.Forms.Padding(0);
            this.OC_Quality.Name = "OC_Quality";
            this.OC_Quality.OptionList = new string[] {
        "3D",
        "4K Ultra",
        "1080p",
        "720p",
        "Low"};
            this.OC_Quality.OptionType = ShowsCalendar.OptionType.OptionList;
            this.OC_Quality.SelectedOption = "1080p";
            this.OC_Quality.SettingType = ShowsCalendar.Classes.Setting.Download;
            this.OC_Quality.Size = new System.Drawing.Size(600, 62);
            this.OC_Quality.TabIndex = 1;
            this.OC_Quality.Text_CheckBox_Checked = "Show All Downloads";
            this.OC_Quality.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
            this.OC_Quality.Text_Info = "Specifies which Video Quality you\'d like highlighted while downloading Episodes.";
            this.OC_Quality.Text_Title = "Preferred Quality";
            this.OC_Quality.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_DownloadOption
            // 
            this.OC_DownloadOption.Checked = true;
            this.OC_DownloadOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_DownloadOption.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_DownloadOption.Location = new System.Drawing.Point(0, 62);
            this.OC_DownloadOption.Margin = new System.Windows.Forms.Padding(0);
            this.OC_DownloadOption.Name = "OC_DownloadOption";
            this.OC_DownloadOption.OptionList = new string[0];
            this.OC_DownloadOption.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_DownloadOption.SelectedOption = "";
            this.OC_DownloadOption.SettingType = ShowsCalendar.Classes.Setting.Download;
            this.OC_DownloadOption.Size = new System.Drawing.Size(600, 62);
            this.OC_DownloadOption.TabIndex = 2;
            this.OC_DownloadOption.Text_CheckBox_Checked = "Show All Downloads";
            this.OC_DownloadOption.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
            this.OC_DownloadOption.Text_Info = "Changes the default download view between \'All\' qualities or \'Preferred\' quality." +
    "";
            this.OC_DownloadOption.Text_Title = "Download Options";
            this.OC_DownloadOption.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_DownloadBehavior
            // 
            this.OC_DownloadBehavior.Checked = true;
            this.OC_DownloadBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_DownloadBehavior.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_DownloadBehavior.Location = new System.Drawing.Point(0, 0);
            this.OC_DownloadBehavior.Margin = new System.Windows.Forms.Padding(0);
            this.OC_DownloadBehavior.Name = "OC_DownloadBehavior";
            this.OC_DownloadBehavior.OptionList = new string[0];
            this.OC_DownloadBehavior.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_DownloadBehavior.SelectedOption = "";
            this.OC_DownloadBehavior.SettingType = ShowsCalendar.Classes.Setting.Download;
            this.OC_DownloadBehavior.Size = new System.Drawing.Size(600, 62);
            this.OC_DownloadBehavior.TabIndex = 3;
            this.OC_DownloadBehavior.Text_CheckBox_Checked = "Close After Download";
            this.OC_DownloadBehavior.Text_CheckBox_Unchecked = "Keep Opened";
            this.OC_DownloadBehavior.Text_Info = "When enabled, the Download window will automatically close after you click on a t" +
    "orrent\'s download button.";
            this.OC_DownloadBehavior.Text_Title = "Download Behavior";
            this.OC_DownloadBehavior.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_NotificationSound
            // 
            this.OC_NotificationSound.Checked = true;
            this.OC_NotificationSound.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_NotificationSound.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_NotificationSound.Location = new System.Drawing.Point(0, 186);
            this.OC_NotificationSound.Margin = new System.Windows.Forms.Padding(0);
            this.OC_NotificationSound.Name = "OC_NotificationSound";
            this.OC_NotificationSound.OptionList = new string[0];
            this.OC_NotificationSound.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_NotificationSound.SelectedOption = "";
            this.OC_NotificationSound.SettingType = ShowsCalendar.Classes.Setting.App;
            this.OC_NotificationSound.Size = new System.Drawing.Size(600, 62);
            this.OC_NotificationSound.TabIndex = 18;
            this.OC_NotificationSound.Text_CheckBox_Checked = "Play Sound";
            this.OC_NotificationSound.Text_CheckBox_Unchecked = "Do not play Sound";
            this.OC_NotificationSound.Text_Info = "Choose to turn the notification sound off if you want.";
            this.OC_NotificationSound.Text_Title = "Notification Sound";
            this.OC_NotificationSound.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_EpNotification
            // 
            this.OC_EpNotification.Checked = true;
            this.OC_EpNotification.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_EpNotification.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_EpNotification.Location = new System.Drawing.Point(0, 124);
            this.OC_EpNotification.Margin = new System.Windows.Forms.Padding(0);
            this.OC_EpNotification.Name = "OC_EpNotification";
            this.OC_EpNotification.OptionList = new string[0];
            this.OC_EpNotification.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_EpNotification.SelectedOption = "";
            this.OC_EpNotification.SettingType = ShowsCalendar.Classes.Setting.App;
            this.OC_EpNotification.Size = new System.Drawing.Size(600, 62);
            this.OC_EpNotification.TabIndex = 17;
            this.OC_EpNotification.Text_CheckBox_Checked = "Show Notification";
            this.OC_EpNotification.Text_CheckBox_Unchecked = "Do Nothing";
            this.OC_EpNotification.Text_Info = "Pops up a notification you when episodes air.";
            this.OC_EpNotification.Text_Title = "Episode Notification";
            this.OC_EpNotification.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_StartMode
            // 
            this.OC_StartMode.Checked = true;
            this.OC_StartMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_StartMode.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_StartMode.Location = new System.Drawing.Point(0, 62);
            this.OC_StartMode.Margin = new System.Windows.Forms.Padding(0);
            this.OC_StartMode.Name = "OC_StartMode";
            this.OC_StartMode.OptionList = new string[0];
            this.OC_StartMode.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_StartMode.SelectedOption = "";
            this.OC_StartMode.SettingType = ShowsCalendar.Classes.Setting.App;
            this.OC_StartMode.Size = new System.Drawing.Size(600, 62);
            this.OC_StartMode.TabIndex = 8;
            this.OC_StartMode.Text_CheckBox_Checked = "Start Normally";
            this.OC_StartMode.Text_CheckBox_Unchecked = "Start Hidden in Taskbar";
            this.OC_StartMode.Text_Info = "Choose to start the App normally or hidden in the TaskBar.";
            this.OC_StartMode.Text_Title = "Startup Mode";
            this.OC_StartMode.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_LaunchWithWindows
            // 
            this.OC_LaunchWithWindows.Checked = true;
            this.OC_LaunchWithWindows.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_LaunchWithWindows.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_LaunchWithWindows.Location = new System.Drawing.Point(0, 0);
            this.OC_LaunchWithWindows.Margin = new System.Windows.Forms.Padding(0);
            this.OC_LaunchWithWindows.Name = "OC_LaunchWithWindows";
            this.OC_LaunchWithWindows.OptionList = new string[0];
            this.OC_LaunchWithWindows.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_LaunchWithWindows.SelectedOption = "";
            this.OC_LaunchWithWindows.SettingType = ShowsCalendar.Classes.Setting.App;
            this.OC_LaunchWithWindows.Size = new System.Drawing.Size(600, 62);
            this.OC_LaunchWithWindows.TabIndex = 9;
            this.OC_LaunchWithWindows.Text_CheckBox_Checked = "Launch Automatically";
            this.OC_LaunchWithWindows.Text_CheckBox_Unchecked = "Launch Manually";
            this.OC_LaunchWithWindows.Text_Info = "Toggles whether to start the App when turning on the computer or not. (App must b" +
    "e run in Administrattor Mode)";
            this.OC_LaunchWithWindows.Text_Title = "Boot Launch";
            this.OC_LaunchWithWindows.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_EpBehavior
            // 
            this.OC_EpBehavior.Checked = true;
            this.OC_EpBehavior.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_EpBehavior.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_EpBehavior.Location = new System.Drawing.Point(0, 310);
            this.OC_EpBehavior.Margin = new System.Windows.Forms.Padding(0);
            this.OC_EpBehavior.Name = "OC_EpBehavior";
            this.OC_EpBehavior.OptionList = new string[0];
            this.OC_EpBehavior.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_EpBehavior.SelectedOption = "";
            this.OC_EpBehavior.SettingType = ShowsCalendar.Classes.Setting.General;
            this.OC_EpBehavior.Size = new System.Drawing.Size(600, 62);
            this.OC_EpBehavior.TabIndex = 22;
            this.OC_EpBehavior.Text_CheckBox_Checked = "Open All";
            this.OC_EpBehavior.Text_CheckBox_Unchecked = "Open only episode";
            this.OC_EpBehavior.Text_Info = "Choose to open the Show && Season Pages when opening the Episode Page.";
            this.OC_EpBehavior.Text_Title = "Episode Info Behavior";
            this.OC_EpBehavior.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_FinaleWarning
            // 
            this.OC_FinaleWarning.Checked = true;
            this.OC_FinaleWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_FinaleWarning.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_FinaleWarning.Location = new System.Drawing.Point(0, 248);
            this.OC_FinaleWarning.Margin = new System.Windows.Forms.Padding(0);
            this.OC_FinaleWarning.Name = "OC_FinaleWarning";
            this.OC_FinaleWarning.OptionList = new string[0];
            this.OC_FinaleWarning.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_FinaleWarning.SelectedOption = "";
            this.OC_FinaleWarning.SettingType = ShowsCalendar.Classes.Setting.General;
            this.OC_FinaleWarning.Size = new System.Drawing.Size(600, 62);
            this.OC_FinaleWarning.TabIndex = 20;
            this.OC_FinaleWarning.Text_CheckBox_Checked = "Warn Me";
            this.OC_FinaleWarning.Text_CheckBox_Unchecked = "Do not Warn";
            this.OC_FinaleWarning.Text_Info = "Get warned when you start up the finale episode of a season.";
            this.OC_FinaleWarning.Text_Title = "Finale Warning";
            this.OC_FinaleWarning.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_UnwatchedBanner
            // 
            this.OC_UnwatchedBanner.Checked = true;
            this.OC_UnwatchedBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_UnwatchedBanner.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OC_UnwatchedBanner.Location = new System.Drawing.Point(0, 186);
            this.OC_UnwatchedBanner.Margin = new System.Windows.Forms.Padding(0);
            this.OC_UnwatchedBanner.Name = "OC_UnwatchedBanner";
            this.OC_UnwatchedBanner.OptionList = new string[0];
            this.OC_UnwatchedBanner.OptionType = ShowsCalendar.OptionType.Checkbox;
            this.OC_UnwatchedBanner.SelectedOption = "";
            this.OC_UnwatchedBanner.SettingType = ShowsCalendar.Classes.Setting.General;
            this.OC_UnwatchedBanner.Size = new System.Drawing.Size(600, 62);
            this.OC_UnwatchedBanner.TabIndex = 19;
            this.OC_UnwatchedBanner.Text_CheckBox_Checked = "Show Banner";
            this.OC_UnwatchedBanner.Text_CheckBox_Unchecked = "Hide Banner";
            this.OC_UnwatchedBanner.Text_Info = "Choose to show a banner on episodes when they haven\'t been watched yet.";
            this.OC_UnwatchedBanner.Text_Title = "Un-Watched Banner";
            this.OC_UnwatchedBanner.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // OC_DiscoverPages
            // 
            this.OC_DiscoverPages.Checked = true;
            this.OC_DiscoverPages.Dock = System.Windows.Forms.DockStyle.Top;
            this.OC_DiscoverPages.Font = new System.Drawing.Font("Nirmala UI", 12F);
            this.OC_DiscoverPages.Location = new System.Drawing.Point(0, 124);
            this.OC_DiscoverPages.Margin = new System.Windows.Forms.Padding(0);
            this.OC_DiscoverPages.Name = "OC_DiscoverPages";
            this.OC_DiscoverPages.OptionList = new string[] {
        "200 results",
        "150 results",
        "100 results",
        "50 results",
        "25 results",
        "10 results"};
            this.OC_DiscoverPages.OptionType = ShowsCalendar.OptionType.OptionList;
            this.OC_DiscoverPages.SelectedOption = "50 Results";
            this.OC_DiscoverPages.SettingType = ShowsCalendar.Classes.Setting.General;
            this.OC_DiscoverPages.Size = new System.Drawing.Size(600, 62);
            this.OC_DiscoverPages.TabIndex = 21;
            this.OC_DiscoverPages.Text_CheckBox_Checked = "Show All Downloads";
            this.OC_DiscoverPages.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
            this.OC_DiscoverPages.Text_Info = "This determines how many pages are loaded in the \'Discover\' screens.";
            this.OC_DiscoverPages.Text_Title = "Discover Pages";
            this.OC_DiscoverPages.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // PC_MoviesOrder
            // 
            this.PC_MoviesOrder.Checked = true;
            this.PC_MoviesOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.PC_MoviesOrder.Font = new System.Drawing.Font("Nirmala UI", 12F);
            this.PC_MoviesOrder.Location = new System.Drawing.Point(0, 62);
            this.PC_MoviesOrder.Margin = new System.Windows.Forms.Padding(0);
            this.PC_MoviesOrder.Name = "PC_MoviesOrder";
            this.PC_MoviesOrder.OptionList = new string[] {
        "3D",
        "4K Ultra",
        "1080p",
        "720p",
        "Low"};
            this.PC_MoviesOrder.OptionType = ShowsCalendar.OptionType.OptionList;
            this.PC_MoviesOrder.SelectedOption = "1080p";
            this.PC_MoviesOrder.SettingType = ShowsCalendar.Classes.Setting.General;
            this.PC_MoviesOrder.Size = new System.Drawing.Size(600, 62);
            this.PC_MoviesOrder.TabIndex = 11;
            this.PC_MoviesOrder.Text_CheckBox_Checked = "Show All Downloads";
            this.PC_MoviesOrder.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
            this.PC_MoviesOrder.Text_Info = "Choose how your Movies library is ordered.";
            this.PC_MoviesOrder.Text_Title = "Movies Order";
            this.PC_MoviesOrder.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // PC_ShowsOrder
            // 
            this.PC_ShowsOrder.Checked = true;
            this.PC_ShowsOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.PC_ShowsOrder.Font = new System.Drawing.Font("Nirmala UI", 12F);
            this.PC_ShowsOrder.Location = new System.Drawing.Point(0, 0);
            this.PC_ShowsOrder.Margin = new System.Windows.Forms.Padding(0);
            this.PC_ShowsOrder.Name = "PC_ShowsOrder";
            this.PC_ShowsOrder.OptionList = new string[] {
        "3D",
        "4K Ultra",
        "1080p",
        "720p",
        "Low"};
            this.PC_ShowsOrder.OptionType = ShowsCalendar.OptionType.OptionList;
            this.PC_ShowsOrder.SelectedOption = "1080p";
            this.PC_ShowsOrder.SettingType = ShowsCalendar.Classes.Setting.General;
            this.PC_ShowsOrder.Size = new System.Drawing.Size(600, 62);
            this.PC_ShowsOrder.TabIndex = 10;
            this.PC_ShowsOrder.Text_CheckBox_Checked = "Show All Downloads";
            this.PC_ShowsOrder.Text_CheckBox_Unchecked = "Pre-Select Preferred Quality";
            this.PC_ShowsOrder.Text_Info = "Choose how your TV Shows library is ordered.";
            this.PC_ShowsOrder.Text_Title = "TV Shows Order";
            this.PC_ShowsOrder.ValueChanged += new System.EventHandler(this.OC_ValueChanged);
            // 
            // PC_Settings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.B_Done);
            this.Controls.Add(this.P_OptionContainer);
            this.Controls.Add(this.TLP_Tabs);
            this.Name = "PC_Settings";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Size = new System.Drawing.Size(1082, 1574);
            this.Text = "Settings";
            this.Controls.SetChildIndex(this.TLP_Tabs, 0);
            this.Controls.SetChildIndex(this.P_OptionContainer, 0);
            this.Controls.SetChildIndex(this.B_Done, 0);
            this.P_OptionContainer.ResumeLayout(false);
            this.P_OptionContainer.PerformLayout();
            this.P_Options.ResumeLayout(false);
            this.P_Options.PerformLayout();
            this.P_Watch.ResumeLayout(false);
            this.P_Download.ResumeLayout(false);
            this.P_App.ResumeLayout(false);
            this.P_General.ResumeLayout(false);
            this.TLP_Tabs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.B_Done)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel P_OptionContainer;
		private SlickControls.Controls.SlickScroll verticalScroll1;
		private System.Windows.Forms.Panel P_Options;
		private System.Windows.Forms.Panel P_Watch;
		private OptionControl OC_BackwardTime;
		private OptionControl OC_ForwardTime;
		private OptionControl OC_FullScreenPlayer;
		private System.Windows.Forms.Panel P_Download;
		private OptionControl OC_Quality;
		private OptionControl OC_DownloadOption;
		private OptionControl OC_DownloadBehavior;
		private System.Windows.Forms.Panel P_App;
		private OptionControl OC_StartMode;
		private OptionControl OC_LaunchWithWindows;
		private System.Windows.Forms.Panel P_General;
		private OptionControl OC_NotificationSound;
		private OptionControl OC_UnwatchedBanner;
		private OptionControl OC_FinaleWarning;
		private OptionControl OC_EpNotification;
		private System.Windows.Forms.TableLayoutPanel TLP_Tabs;
		private SlickControls.Controls.SlickTab T_App;
		private SlickControls.Controls.SlickTab T_Watch;
		private SlickControls.Controls.SlickTab T_Downloads;
		private SlickControls.Controls.SlickTab T_General;
		private SlickControls.Controls.SlickButton B_Done;
		private OptionControl PC_ShowsOrder;
		private OptionControl PC_MoviesOrder;
		private OptionControl OC_DiscoverPages;
		private OptionControl OC_EpBehavior;
		private OptionControl OC_AutoEpPLay;
		private OptionControl OC_AutoEpSwitch;
		private OptionControl OC_AutoPauseScroll;
        private OptionControl OC_DisableMini;
    }
}
