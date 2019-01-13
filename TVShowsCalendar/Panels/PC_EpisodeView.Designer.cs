namespace TVShowsCalendar.Panels
{
	partial class PC_EpisodeView
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
			this.FLP_Content = new System.Windows.Forms.FlowLayoutPanel();
			this.verticalScroll1 = new SlickControls.Controls.VerticalScroll();
			this.PB_EpisodeInfo = new TVShowsCalendar.UserControls.SlickPB();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.ST_Images = new SlickControls.Controls.SlickTab();
			this.ST_Cast = new SlickControls.Controls.SlickTab();
			this.ST_Crew = new SlickControls.Controls.SlickTab();
			this.SL_Forward = new SlickControls.Controls.SlickLabel();
			this.SL_Backwards = new SlickControls.Controls.SlickLabel();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_EpisodeInfo)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.FLP_Content);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 197);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(783, 241);
			this.panel1.TabIndex = 13;
			// 
			// FLP_Content
			// 
			this.FLP_Content.AutoSize = true;
			this.FLP_Content.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Content.Location = new System.Drawing.Point(0, 0);
			this.FLP_Content.Name = "FLP_Content";
			this.FLP_Content.Size = new System.Drawing.Size(0, 0);
			this.FLP_Content.TabIndex = 0;
			// 
			// verticalScroll1
			// 
			this.verticalScroll1.BarColor = null;
			this.verticalScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll1.LinkedControl = this.FLP_Content;
			this.verticalScroll1.Location = new System.Drawing.Point(774, 30);
			this.verticalScroll1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll1.Name = "verticalScroll1";
			this.verticalScroll1.Size = new System.Drawing.Size(4, 246);
			this.verticalScroll1.TabIndex = 14;
			// 
			// PB_EpisodeInfo
			// 
			this.PB_EpisodeInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.PB_EpisodeInfo.Location = new System.Drawing.Point(0, 30);
			this.PB_EpisodeInfo.Name = "PB_EpisodeInfo";
			this.PB_EpisodeInfo.Size = new System.Drawing.Size(783, 135);
			this.PB_EpisodeInfo.TabIndex = 16;
			this.PB_EpisodeInfo.TabStop = false;
			this.PB_EpisodeInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_EpisodeInfo_Paint);
			this.PB_EpisodeInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PB_EpisodeInfo_MouseClick);
			this.PB_EpisodeInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_EpisodeInfo_MouseMove);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 6;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.SL_Backwards, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.ST_Images, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.ST_Cast, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.ST_Crew, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.SL_Forward, 5, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 165);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 32);
			this.tableLayoutPanel1.TabIndex = 17;
			// 
			// ST_Images
			// 
			this.ST_Images.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ST_Images.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ST_Images.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ST_Images.Hovered = false;
			this.ST_Images.Location = new System.Drawing.Point(0, 6);
			this.ST_Images.Margin = new System.Windows.Forms.Padding(0);
			this.ST_Images.Name = "ST_Images";
			this.ST_Images.Selected = true;
			this.ST_Images.Size = new System.Drawing.Size(175, 26);
			this.ST_Images.TabIndex = 0;
			this.ST_Images.Text = "Images";
			this.ST_Images.TabSelected += new System.EventHandler(this.ST_Images_TabSelected);
			// 
			// ST_Cast
			// 
			this.ST_Cast.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ST_Cast.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ST_Cast.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ST_Cast.Hovered = false;
			this.ST_Cast.Location = new System.Drawing.Point(175, 6);
			this.ST_Cast.Margin = new System.Windows.Forms.Padding(0);
			this.ST_Cast.Name = "ST_Cast";
			this.ST_Cast.Selected = false;
			this.ST_Cast.Size = new System.Drawing.Size(175, 26);
			this.ST_Cast.TabIndex = 0;
			this.ST_Cast.Text = "Guest Stars";
			this.ST_Cast.TabSelected += new System.EventHandler(this.ST_Cast_TabSelected);
			// 
			// ST_Crew
			// 
			this.ST_Crew.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ST_Crew.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ST_Crew.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ST_Crew.Hovered = false;
			this.ST_Crew.Location = new System.Drawing.Point(350, 6);
			this.ST_Crew.Margin = new System.Windows.Forms.Padding(0);
			this.ST_Crew.Name = "ST_Crew";
			this.ST_Crew.Selected = false;
			this.ST_Crew.Size = new System.Drawing.Size(175, 26);
			this.ST_Crew.TabIndex = 0;
			this.ST_Crew.Text = "Crew";
			this.ST_Crew.TabSelected += new System.EventHandler(this.ST_Crew_TabSelected);
			// 
			// SL_Forward
			// 
			this.SL_Forward.ActiveColor = null;
			this.SL_Forward.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.SL_Forward.AutoSize = true;
			this.SL_Forward.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_Forward.Center = true;
			this.SL_Forward.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Forward.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Forward.HideText = true;
			this.SL_Forward.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Forward.IconSize = 16;
			this.SL_Forward.Image = global::TVShowsCalendar.Properties.Resources.Tiny_JumpForward;
			this.SL_Forward.Location = new System.Drawing.Point(739, 7);
			this.SL_Forward.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.SL_Forward.Name = "SL_Forward";
			this.SL_Forward.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Forward.Size = new System.Drawing.Size(39, 24);
			this.SL_Forward.TabIndex = 4;
			this.SL_Forward.Click += new System.EventHandler(this.SL_Forward_Click);
			// 
			// SL_Backwards
			// 
			this.SL_Backwards.ActiveColor = null;
			this.SL_Backwards.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.SL_Backwards.AutoSize = true;
			this.SL_Backwards.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SL_Backwards.Center = true;
			this.SL_Backwards.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Backwards.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Backwards.HideText = true;
			this.SL_Backwards.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Backwards.IconSize = 16;
			this.SL_Backwards.Image = global::TVShowsCalendar.Properties.Resources.Tiny_JumpBack;
			this.SL_Backwards.Location = new System.Drawing.Point(700, 7);
			this.SL_Backwards.Margin = new System.Windows.Forms.Padding(0);
			this.SL_Backwards.Name = "SL_Backwards";
			this.SL_Backwards.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Backwards.Size = new System.Drawing.Size(39, 24);
			this.SL_Backwards.TabIndex = 5;
			this.SL_Backwards.Click += new System.EventHandler(this.SL_Backwards_Click);
			// 
			// PC_EpisodeView
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.verticalScroll1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.PB_EpisodeInfo);
			this.Name = "PC_EpisodeView";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.ShowBack = true;
			this.Resize += new System.EventHandler(this.PC_EpisodeView_Resize);
			this.Controls.SetChildIndex(this.PB_EpisodeInfo, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.verticalScroll1, 0);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_EpisodeInfo)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel FLP_Content;
		private SlickControls.Controls.VerticalScroll verticalScroll1;
		private UserControls.SlickPB PB_EpisodeInfo;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private SlickControls.Controls.SlickTab ST_Images;
		private SlickControls.Controls.SlickTab ST_Cast;
		private SlickControls.Controls.SlickTab ST_Crew;
		public SlickControls.Controls.SlickLabel SL_Backwards;
		public SlickControls.Controls.SlickLabel SL_Forward;
	}
}
