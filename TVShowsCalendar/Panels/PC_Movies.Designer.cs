namespace ShowsCalendar.Panels
{
	partial class PC_Movies
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
			this.SP_Earlier = new SlickControls.Controls.SlickSectionPanel();
			this.SP_Upcoming = new SlickControls.Controls.SlickSectionPanel();
			this.SP_Recent = new SlickControls.Controls.SlickSectionPanel();
			this.verticalScroll = new SlickControls.Controls.SlickScroll();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Search = new SlickControls.Controls.SlickTextBox();
			this.B_Discover = new SlickControls.Controls.SlickButton();
			this.B_Add = new SlickControls.Controls.SlickButton();
			this.PB_Search = new System.Windows.Forms.PictureBox();
			this.TLP_NoMovies = new System.Windows.Forms.TableLayoutPanel();
			this.L_NoMovies = new System.Windows.Forms.Label();
			this.L_NoMoviesInfo = new System.Windows.Forms.Label();
			this.SP_Oldies = new SlickControls.Controls.SlickSectionPanel();
			this.panel2.SuspendLayout();
			this.P_Tabs.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_Discover)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Add)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Search)).BeginInit();
			this.TLP_NoMovies.SuspendLayout();
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
			this.P_Tabs.Controls.Add(this.SP_Oldies);
			this.P_Tabs.Controls.Add(this.SP_Earlier);
			this.P_Tabs.Controls.Add(this.SP_Upcoming);
			this.P_Tabs.Controls.Add(this.SP_Recent);
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.MinimumSize = new System.Drawing.Size(500, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(500, 216);
			this.P_Tabs.TabIndex = 12;
			// 
			// SP_Earlier
			// 
			this.SP_Earlier.Active = false;
			this.SP_Earlier.AutoHide = true;
			this.SP_Earlier.AutoSize = true;
			this.SP_Earlier.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Earlier.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Earlier.Flavor = null;
			this.SP_Earlier.Icon = global::ShowsCalendar.Properties.Resources.Big_Movie;
			this.SP_Earlier.Info = "Movies released a while back";
			this.SP_Earlier.Location = new System.Drawing.Point(0, 108);
			this.SP_Earlier.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Earlier.Name = "SP_Earlier";
			this.SP_Earlier.Size = new System.Drawing.Size(500, 54);
			this.SP_Earlier.TabIndex = 7;
			this.SP_Earlier.Text = "Earlier";
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
			this.SP_Upcoming.Info = "Recently aired Movies";
			this.SP_Upcoming.Location = new System.Drawing.Point(0, 54);
			this.SP_Upcoming.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Upcoming.Name = "SP_Upcoming";
			this.SP_Upcoming.Size = new System.Drawing.Size(500, 54);
			this.SP_Upcoming.TabIndex = 6;
			this.SP_Upcoming.Text = "Upcoming";
			// 
			// SP_Recent
			// 
			this.SP_Recent.Active = true;
			this.SP_Recent.AutoHide = true;
			this.SP_Recent.AutoSize = true;
			this.SP_Recent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Recent.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Recent.Flavor = null;
			this.SP_Recent.Icon = global::ShowsCalendar.Properties.Resources.Big_Popcorn;
			this.SP_Recent.Info = "Recently aired Movies";
			this.SP_Recent.Location = new System.Drawing.Point(0, 0);
			this.SP_Recent.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Recent.Name = "SP_Recent";
			this.SP_Recent.Size = new System.Drawing.Size(500, 54);
			this.SP_Recent.TabIndex = 5;
			this.SP_Recent.Text = "Recently Premiered";
			// 
			// verticalScroll
			// 
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.P_Tabs;
			this.verticalScroll.Location = new System.Drawing.Point(881, 62);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(12, 431);
			this.verticalScroll.TabIndex = 24;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.TB_Search, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_Discover, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_Add, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.PB_Search, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(880, 32);
			this.tableLayoutPanel1.TabIndex = 25;
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
			this.TB_Search.Placeholder = "Search Movies..";
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.ShowLabel = false;
			this.TB_Search.Size = new System.Drawing.Size(0, 20);
			this.TB_Search.TabIndex = 27;
			this.TB_Search.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.TB_Search.Validation = SlickControls.Enums.ValidationType.None;
			this.TB_Search.ValidationRegex = "";
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
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
			this.B_Discover.TabIndex = 26;
			this.B_Discover.Text = "DISCOVER";
			this.B_Discover.Click += new System.EventHandler(this.B_Discover_Click);
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
			this.B_Add.Text = "ADD MOVIE";
			this.B_Add.Click += new System.EventHandler(this.B_Add_Click);
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
			this.PB_Search.TabIndex = 15;
			this.PB_Search.TabStop = false;
			this.PB_Search.Click += new System.EventHandler(this.PB_Search_Click);
			// 
			// TLP_NoMovies
			// 
			this.TLP_NoMovies.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.TLP_NoMovies.AutoSize = true;
			this.TLP_NoMovies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_NoMovies.ColumnCount = 1;
			this.TLP_NoMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoMovies.Controls.Add(this.L_NoMovies, 0, 0);
			this.TLP_NoMovies.Controls.Add(this.L_NoMoviesInfo, 0, 1);
			this.TLP_NoMovies.Location = new System.Drawing.Point(300, 216);
			this.TLP_NoMovies.Name = "TLP_NoMovies";
			this.TLP_NoMovies.RowCount = 2;
			this.TLP_NoMovies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoMovies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_NoMovies.Size = new System.Drawing.Size(284, 60);
			this.TLP_NoMovies.TabIndex = 30;
			// 
			// L_NoMovies
			// 
			this.L_NoMovies.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoMovies.AutoSize = true;
			this.L_NoMovies.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_NoMovies.Location = new System.Drawing.Point(105, 3);
			this.L_NoMovies.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
			this.L_NoMovies.Name = "L_NoMovies";
			this.L_NoMovies.Size = new System.Drawing.Size(74, 17);
			this.L_NoMovies.TabIndex = 1;
			this.L_NoMovies.Text = "No Movies";
			// 
			// L_NoMoviesInfo
			// 
			this.L_NoMoviesInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoMoviesInfo.AutoSize = true;
			this.L_NoMoviesInfo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic);
			this.L_NoMoviesInfo.Location = new System.Drawing.Point(0, 30);
			this.L_NoMoviesInfo.Margin = new System.Windows.Forms.Padding(0);
			this.L_NoMoviesInfo.Name = "L_NoMoviesInfo";
			this.L_NoMoviesInfo.Size = new System.Drawing.Size(284, 30);
			this.L_NoMoviesInfo.TabIndex = 1;
			this.L_NoMoviesInfo.Text = "You don\'t seem to have any Movies in your library.\r\nAdd Movies using the Add butt" +
    "on to start.";
			this.L_NoMoviesInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SP_Oldies
			// 
			this.SP_Oldies.Active = false;
			this.SP_Oldies.AutoHide = true;
			this.SP_Oldies.AutoSize = true;
			this.SP_Oldies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Oldies.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Oldies.Flavor = null;
			this.SP_Oldies.Icon = global::ShowsCalendar.Properties.Resources.Big_OldMovie;
			this.SP_Oldies.Info = "Movies released a while back";
			this.SP_Oldies.Location = new System.Drawing.Point(0, 162);
			this.SP_Oldies.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Oldies.Name = "SP_Oldies";
			this.SP_Oldies.Size = new System.Drawing.Size(500, 54);
			this.SP_Oldies.TabIndex = 8;
			this.SP_Oldies.Text = "Oldies\'";
			// 
			// PC_Movies
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TLP_NoMovies);
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PC_Movies";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Size = new System.Drawing.Size(885, 493);
			this.Text = "Movie Library";
			this.Resize += new System.EventHandler(this.PC_Show_Resize);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.panel2, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.Controls.SetChildIndex(this.TLP_NoMovies, 0);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.P_Tabs.ResumeLayout(false);
			this.P_Tabs.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.B_Discover)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.B_Add)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_Search)).EndInit();
			this.TLP_NoMovies.ResumeLayout(false);
			this.TLP_NoMovies.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel P_Tabs;
		private SlickControls.Controls.SlickScroll verticalScroll;
		private SlickControls.Controls.SlickButton B_Add;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.PictureBox PB_Search;
		private SlickControls.Controls.SlickButton B_Discover;
		private System.Windows.Forms.TableLayoutPanel TLP_NoMovies;
		private System.Windows.Forms.Label L_NoMovies;
		private System.Windows.Forms.Label L_NoMoviesInfo;
		private SlickControls.Controls.SlickSectionPanel SP_Recent;
		private SlickControls.Controls.SlickSectionPanel SP_Earlier;
		private SlickControls.Controls.SlickSectionPanel SP_Upcoming;
		private SlickControls.Controls.SlickTextBox TB_Search;
		private SlickControls.Controls.SlickSectionPanel SP_Oldies;
	}
}
