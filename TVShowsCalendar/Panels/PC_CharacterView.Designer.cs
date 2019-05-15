namespace ShowsCalendar.Panels
{
	partial class PC_CharacterView
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
			this.verticalScroll1 = new SlickControls.Controls.SlickScroll();
			this.PB_EpisodeInfo = new ShowsCalendar.Controls.SlickPB();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.ST_Images = new SlickControls.Controls.SlickTab();
			this.ST_Library = new SlickControls.Controls.SlickTab();
			this.ST_Career = new SlickControls.Controls.SlickTab();
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
			this.verticalScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll1.LinkedControl = this.FLP_Content;
			this.verticalScroll1.Location = new System.Drawing.Point(774, 30);
			this.verticalScroll1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll1.Name = "verticalScroll1";
			this.verticalScroll1.Size = new System.Drawing.Size(5, 246);
			this.verticalScroll1.SizeSource = null;
			this.verticalScroll1.Style = SlickControls.Controls.StyleType.Vertical;
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
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.ST_Images, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.ST_Library, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.ST_Career, 1, 0);
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
			this.ST_Images.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Image;
			this.ST_Images.Location = new System.Drawing.Point(300, 6);
			this.ST_Images.Margin = new System.Windows.Forms.Padding(0);
			this.ST_Images.Name = "ST_Images";
			this.ST_Images.Selected = false;
			this.ST_Images.Size = new System.Drawing.Size(150, 26);
			this.ST_Images.TabIndex = 0;
			this.ST_Images.Text = "Images";
			this.ST_Images.TabSelected += new System.EventHandler(this.ST_Images_TabSelected);
			// 
			// ST_Library
			// 
			this.ST_Library.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ST_Library.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ST_Library.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ST_Library.Hovered = false;
			this.ST_Library.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Library;
			this.ST_Library.Location = new System.Drawing.Point(0, 6);
			this.ST_Library.Margin = new System.Windows.Forms.Padding(0);
			this.ST_Library.Name = "ST_Library";
			this.ST_Library.Selected = true;
			this.ST_Library.Size = new System.Drawing.Size(150, 26);
			this.ST_Library.TabIndex = 0;
			this.ST_Library.Text = "In Library";
			this.ST_Library.TabSelected += new System.EventHandler(this.ST_Library_TabSelected);
			// 
			// ST_Career
			// 
			this.ST_Career.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ST_Career.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ST_Career.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ST_Career.Hovered = false;
			this.ST_Career.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Job;
			this.ST_Career.Location = new System.Drawing.Point(150, 6);
			this.ST_Career.Margin = new System.Windows.Forms.Padding(0);
			this.ST_Career.Name = "ST_Career";
			this.ST_Career.Selected = false;
			this.ST_Career.Size = new System.Drawing.Size(150, 26);
			this.ST_Career.TabIndex = 0;
			this.ST_Career.Text = "Career";
			this.ST_Career.TabSelected += new System.EventHandler(this.ST_Career_TabSelected);
			// 
			// PC_CharacterView
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.verticalScroll1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.PB_EpisodeInfo);
			this.Name = "PC_CharacterView";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Resize += new System.EventHandler(this.PC_EpisodeView_Resize);
			this.Controls.SetChildIndex(this.PB_EpisodeInfo, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.verticalScroll1, 0);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_EpisodeInfo)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel FLP_Content;
		private SlickControls.Controls.SlickScroll verticalScroll1;
		private ShowsCalendar.Controls.SlickPB PB_EpisodeInfo;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		internal SlickControls.Controls.SlickTab ST_Images;
		internal SlickControls.Controls.SlickTab ST_Library;
		internal SlickControls.Controls.SlickTab ST_Career;
	}
}
