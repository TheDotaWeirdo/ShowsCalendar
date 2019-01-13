namespace TVShowsCalendar.Panels
{
	partial class PC_Library
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
			this.verticalScroll = new SlickControls.Controls.VerticalScroll();
			this.panel1 = new System.Windows.Forms.Panel();
			this.TLP_NoShows = new System.Windows.Forms.TableLayoutPanel();
			this.L_NoShows = new System.Windows.Forms.Label();
			this.L_NoShowsInfo = new System.Windows.Forms.Label();
			this.SP_Shows = new TVShowsCalendar.UserControls.SectionPanel();
			this.Content = new System.Windows.Forms.FlowLayoutPanel();
			this.SP_Movies = new TVShowsCalendar.UserControls.SectionPanel();
			this.P_Tabs.SuspendLayout();
			this.panel1.SuspendLayout();
			this.TLP_NoShows.SuspendLayout();
			this.SuspendLayout();
			// 
			// P_Tabs
			// 
			this.P_Tabs.AutoSize = true;
			this.P_Tabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tabs.Controls.Add(this.SP_Movies);
			this.P_Tabs.Controls.Add(this.SP_Shows);
			this.P_Tabs.Location = new System.Drawing.Point(0, 0);
			this.P_Tabs.MinimumSize = new System.Drawing.Size(500, 0);
			this.P_Tabs.Name = "P_Tabs";
			this.P_Tabs.Size = new System.Drawing.Size(500, 108);
			this.P_Tabs.TabIndex = 12;
			// 
			// verticalScroll
			// 
			this.verticalScroll.BarColor = null;
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.P_Tabs;
			this.verticalScroll.Location = new System.Drawing.Point(774, 30);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(12, 396);
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
			// SP_Shows
			// 
			this.SP_Shows.Active = false;
			this.SP_Shows.AutoHide = true;
			this.SP_Shows.AutoSize = true;
			this.SP_Shows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Shows.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Shows.Flavor = null;
			this.SP_Shows.Icon = global::TVShowsCalendar.Properties.Resources.Big_TV;
			this.SP_Shows.Info = "TV Shows available to watch";
			this.SP_Shows.Location = new System.Drawing.Point(0, 0);
			this.SP_Shows.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Shows.Name = "SP_Shows";
			this.SP_Shows.Size = new System.Drawing.Size(500, 54);
			this.SP_Shows.TabIndex = 5;
			this.SP_Shows.Text = "TV Shows";
			// 
			// Content
			// 
			this.Content.AutoSize = true;
			this.Content.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Content.Dock = System.Windows.Forms.DockStyle.Top;
			this.Content.Location = new System.Drawing.Point(43, 54);
			this.Content.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Content.Name = "Content";
			this.Content.Size = new System.Drawing.Size(457, 0);
			this.Content.TabIndex = 0;
			// 
			// SP_Movies
			// 
			this.SP_Movies.Active = false;
			this.SP_Movies.AutoHide = true;
			this.SP_Movies.AutoSize = true;
			this.SP_Movies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SP_Movies.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Movies.Flavor = null;
			this.SP_Movies.Icon = global::TVShowsCalendar.Properties.Resources.Big_Movie;
			this.SP_Movies.Info = "Movies available to watch";
			this.SP_Movies.Location = new System.Drawing.Point(0, 54);
			this.SP_Movies.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Movies.Name = "SP_Movies";
			this.SP_Movies.Size = new System.Drawing.Size(500, 54);
			this.SP_Movies.TabIndex = 6;
			this.SP_Movies.Text = "Movies";
			// 
			// PC_Library
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.TLP_NoShows);
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.panel1);
			this.Name = "PC_Library";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Text = "Library";
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
		private SlickControls.Controls.VerticalScroll verticalScroll;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel TLP_NoShows;
		private System.Windows.Forms.Label L_NoShows;
		private System.Windows.Forms.Label L_NoShowsInfo;
		private UserControls.SectionPanel SP_Movies;
		private UserControls.SectionPanel SP_Shows;
		private System.Windows.Forms.FlowLayoutPanel Content;
	}
}
