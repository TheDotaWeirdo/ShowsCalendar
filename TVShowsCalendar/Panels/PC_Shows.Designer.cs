namespace ShowsCalendar.Panels
{
	partial class PC_Shows
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.P_Tabs = new System.Windows.Forms.Panel();
			this.SP_Ended = new SlickControls.Controls.SlickSectionPanel();
			this.SP_Returning = new SlickControls.Controls.SlickSectionPanel();
			this.SP_Upcoming = new SlickControls.Controls.SlickSectionPanel();
			this.SP_Airing = new SlickControls.Controls.SlickSectionPanel();
			this.verticalScroll = new SlickControls.Controls.SlickScroll();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Search = new System.Windows.Forms.PictureBox();
			this.B_Add = new SlickControls.Controls.SlickButton();
			this.B_Discover = new SlickControls.Controls.SlickButton();
			this.TB_Search = new SlickControls.Controls.SlickTextBox();
			this.TLP_NoShows = new System.Windows.Forms.TableLayoutPanel();
			this.L_NoShows = new System.Windows.Forms.Label();
			this.L_NoShowsInfo = new System.Windows.Forms.Label();
			this.panel2.SuspendLayout();
			this.P_Tabs.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Search)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Add)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Discover)).BeginInit();
			this.TLP_NoShows.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.P_Tabs);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(5, 62);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(880, 431);
			this.panel2.TabIndex = 23;
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Controls.Add(this.SP_Ended);
			this.P_Tabs.Controls.Add(this.SP_Returning);
			this.P_Tabs.Controls.Add(this.SP_Upcoming);
			this.P_Tabs.Controls.Add(this.SP_Airing);
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.MinimumSize = new System.Drawing.Size(600, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(600, 216);
			this.P_Tabs.TabIndex = 3;
			// 
			// SP_Ended
			// 
			this.SP_Ended.Active = false;
			this.SP_Ended.AutoHide = true;
			this.SP_Ended.AutoSize = true;
			this.SP_Ended.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Ended.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Ended.Flavor = null;
			this.SP_Ended.Icon = global::ShowsCalendar.Properties.Resources.Big_RetroTv;
			this.SP_Ended.Info = "Ended TV Shows";
			this.SP_Ended.Location = new System.Drawing.Point(0, 162);
			this.SP_Ended.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Ended.Name = "SP_Ended";
			this.SP_Ended.Size = new System.Drawing.Size(600, 54);
			this.SP_Ended.TabIndex = 16;
			this.SP_Ended.Text = "Ended";
			// 
			// SP_Returning
			// 
			this.SP_Returning.Active = false;
			this.SP_Returning.AutoHide = true;
			this.SP_Returning.AutoSize = true;
			this.SP_Returning.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Returning.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Returning.Flavor = null;
			this.SP_Returning.Icon = global::ShowsCalendar.Properties.Resources.Big_TVEmpty;
			this.SP_Returning.Info = "Returning TV Shows with no release date yet";
			this.SP_Returning.Location = new System.Drawing.Point(0, 108);
			this.SP_Returning.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Returning.Name = "SP_Returning";
			this.SP_Returning.Size = new System.Drawing.Size(600, 54);
			this.SP_Returning.TabIndex = 15;
			this.SP_Returning.Text = "Returning";
			// 
			// SP_Upcoming
			// 
			this.SP_Upcoming.Active = false;
			this.SP_Upcoming.AutoHide = true;
			this.SP_Upcoming.AutoSize = true;
			this.SP_Upcoming.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Upcoming.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Upcoming.Flavor = null;
			this.SP_Upcoming.Icon = global::ShowsCalendar.Properties.Resources.Big_Schedule;
			this.SP_Upcoming.Info = "TV Shows with a new upcoming episode";
			this.SP_Upcoming.Location = new System.Drawing.Point(0, 54);
			this.SP_Upcoming.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Upcoming.Name = "SP_Upcoming";
			this.SP_Upcoming.Size = new System.Drawing.Size(600, 54);
			this.SP_Upcoming.TabIndex = 14;
			this.SP_Upcoming.Text = "Upcoming";
			// 
			// SP_Airing
			// 
			this.SP_Airing.Active = true;
			this.SP_Airing.AutoHide = true;
			this.SP_Airing.AutoSize = true;
			this.SP_Airing.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Airing.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Airing.Flavor = null;
			this.SP_Airing.Icon = global::ShowsCalendar.Properties.Resources.Big_Airing;
			this.SP_Airing.Info = "Currently airing TV Shows";
			this.SP_Airing.Location = new System.Drawing.Point(0, 0);
			this.SP_Airing.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Airing.Name = "SP_Airing";
			this.SP_Airing.Size = new System.Drawing.Size(600, 54);
			this.SP_Airing.TabIndex = 13;
			this.SP_Airing.Text = "Airing";
			// 
			// verticalScroll
			// 
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.P_Tabs;
			this.verticalScroll.Location = new System.Drawing.Point(873, 62);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(12, 427);
			this.verticalScroll.TabIndex = 24;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.PB_Search, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_Add, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_Discover, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.TB_Search, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(880, 32);
			this.tableLayoutPanel1.TabIndex = 25;
			// 
			// PB_Search
			// 
			this.PB_Search.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_Search.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PB_Search.Image = global::ShowsCalendar.Properties.Resources.Big_Search;
			this.PB_Search.Location = new System.Drawing.Point(15, 5);
			this.PB_Search.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
			this.PB_Search.Name = "PB_Search";
			this.PB_Search.Size = new System.Drawing.Size(22, 22);
			this.PB_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Search.TabIndex = 16;
			this.PB_Search.TabStop = false;
			this.PB_Search.Click += new System.EventHandler(this.PB_Search_Click);
			// 
			// B_Add
			// 
			this.B_Add.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_Add.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Add.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Add.ColorShade = null;
			this.B_Add.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Add.Font = new System.Drawing.Font("Nirmala UI", 9.75F);
			this.B_Add.IconSize = 16;
			this.B_Add.Image = global::ShowsCalendar.Properties.Resources.Tiny_Add;
			this.B_Add.Location = new System.Drawing.Point(745, 1);
			this.B_Add.Margin = new System.Windows.Forms.Padding(15, 0, 10, 0);
			this.B_Add.Name = "B_Add";
			this.B_Add.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_Add.Size = new System.Drawing.Size(125, 30);
			this.B_Add.TabIndex = 14;
			this.B_Add.Text = "ADD SHOW";
			this.B_Add.Click += new System.EventHandler(this.B_Add_Click);
			// 
			// B_Discover
			// 
			this.B_Discover.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_Discover.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.B_Discover.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Discover.ColorShade = null;
			this.B_Discover.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Discover.Font = new System.Drawing.Font("Nirmala UI", 9.75F);
			this.B_Discover.IconSize = 16;
			this.B_Discover.Image = global::ShowsCalendar.Properties.Resources.Tiny_Discover;
			this.B_Discover.Location = new System.Drawing.Point(595, 1);
			this.B_Discover.Margin = new System.Windows.Forms.Padding(15, 0, 10, 0);
			this.B_Discover.Name = "B_Discover";
			this.B_Discover.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.B_Discover.Size = new System.Drawing.Size(125, 30);
			this.B_Discover.TabIndex = 14;
			this.B_Discover.Text = "DISCOVER";
			this.B_Discover.Click += new System.EventHandler(this.B_Discover_Click);
			// 
			// TB_Search
			// 
			this.TB_Search.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.TB_Search.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TB_Search.Image = null;
			this.TB_Search.LabelText = "Search";
			this.TB_Search.Location = new System.Drawing.Point(50, 6);
			this.TB_Search.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.TB_Search.MaximumSize = new System.Drawing.Size(9999, 20);
			this.TB_Search.MaxLength = 32767;
			this.TB_Search.MinimumSize = new System.Drawing.Size(0, 20);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Placeholder = "Search Shows..";
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.ShowLabel = false;
			this.TB_Search.Size = new System.Drawing.Size(0, 20);
			this.TB_Search.TabIndex = 15;
			this.TB_Search.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.TB_Search.Validation = SlickControls.Enums.ValidationType.None;
			this.TB_Search.ValidationRegex = "";
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
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
			this.TLP_NoShows.Location = new System.Drawing.Point(302, 216);
			this.TLP_NoShows.Name = "TLP_NoShows";
			this.TLP_NoShows.RowCount = 2;
			this.TLP_NoShows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoShows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoShows.Size = new System.Drawing.Size(280, 60);
			this.TLP_NoShows.TabIndex = 28;
			// 
			// L_NoShows
			// 
			this.L_NoShows.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoShows.AutoSize = true;
			this.L_NoShows.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_NoShows.Location = new System.Drawing.Point(105, 3);
			this.L_NoShows.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
			this.L_NoShows.Name = "L_NoShows";
			this.L_NoShows.Size = new System.Drawing.Size(69, 17);
			this.L_NoShows.TabIndex = 1;
			this.L_NoShows.Text = "No Shows";
			// 
			// L_NoShowsInfo
			// 
			this.L_NoShowsInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoShowsInfo.AutoSize = true;
			this.L_NoShowsInfo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic);
			this.L_NoShowsInfo.Location = new System.Drawing.Point(0, 30);
			this.L_NoShowsInfo.Margin = new System.Windows.Forms.Padding(0);
			this.L_NoShowsInfo.Name = "L_NoShowsInfo";
			this.L_NoShowsInfo.Size = new System.Drawing.Size(280, 30);
			this.L_NoShowsInfo.TabIndex = 1;
			this.L_NoShowsInfo.Text = "You don\'t seem to have any Shows in your library.\r\nAdd Shows using the Add button" +
    " to start.";
			this.L_NoShowsInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PC_Shows
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TLP_NoShows);
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PC_Shows";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Size = new System.Drawing.Size(885, 493);
			this.Text = "Show Library";
			this.Resize += new System.EventHandler(this.PC_Show_Resize);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.panel2, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.Controls.SetChildIndex(this.TLP_NoShows, 0);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.P_Tabs.ResumeLayout(false);
			this.P_Tabs.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PB_Search)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Add)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Discover)).EndInit();
			this.TLP_NoShows.ResumeLayout(false);
			this.TLP_NoShows.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel panel2;
		private SlickControls.Controls.SlickScroll verticalScroll;
		private SlickControls.Controls.SlickButton B_Add;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel TLP_NoShows;
		private System.Windows.Forms.Label L_NoShows;
		private System.Windows.Forms.Label L_NoShowsInfo;
		private SlickControls.Controls.SlickButton B_Discover;
		private SlickControls.Controls.SlickSectionPanel SP_Airing;
		private SlickControls.Controls.SlickSectionPanel SP_Ended;
		private SlickControls.Controls.SlickSectionPanel SP_Upcoming;
		private SlickControls.Controls.SlickSectionPanel SP_Returning;
		private System.Windows.Forms.Panel P_Tabs;
		private SlickControls.Controls.SlickTextBox TB_Search;
		private System.Windows.Forms.PictureBox PB_Search;
	}
}
