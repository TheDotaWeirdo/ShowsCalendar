namespace ShowsCalendar.Panels
{
	partial class PC_SeasonView
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
			this.FLP_Episodes = new System.Windows.Forms.FlowLayoutPanel();
			this.verticalScroll = new SlickControls.Controls.SlickScroll();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.ST_Cast = new SlickControls.Controls.SlickTab();
			this.SL_Backwards = new SlickControls.Controls.SlickLabel();
			this.ST_Images = new SlickControls.Controls.SlickTab();
			this.SL_Forward = new SlickControls.Controls.SlickLabel();
			this.ST_Episodes = new SlickControls.Controls.SlickTab();
			this.ST_Crew = new SlickControls.Controls.SlickTab();
			this.PB_SeasonInfo = new ShowsCalendar.Controls.SlickPB();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SL_Backwards)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Forward)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_SeasonInfo)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.FLP_Episodes);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 197);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(783, 241);
			this.panel1.TabIndex = 13;
			// 
			// FLP_Episodes
			// 
			this.FLP_Episodes.AutoSize = true;
			this.FLP_Episodes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Episodes.Location = new System.Drawing.Point(0, 0);
			this.FLP_Episodes.Name = "FLP_Episodes";
			this.FLP_Episodes.Size = new System.Drawing.Size(0, 0);
			this.FLP_Episodes.TabIndex = 0;
			// 
			// verticalScroll
			// 
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.FLP_Episodes;
			this.verticalScroll.Location = new System.Drawing.Point(771, 30);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(5, 330);
			this.verticalScroll.SizeSource = null;
			this.verticalScroll.Style = SlickControls.Controls.StyleType.Vertical;
			this.verticalScroll.TabIndex = 14;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 7;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.ST_Cast, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.SL_Backwards, 5, 0);
			this.tableLayoutPanel1.Controls.Add(this.ST_Images, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.SL_Forward, 6, 0);
			this.tableLayoutPanel1.Controls.Add(this.ST_Episodes, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.ST_Crew, 3, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 165);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 32);
			this.tableLayoutPanel1.TabIndex = 18;
			// 
			// ST_Cast
			// 
			this.ST_Cast.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ST_Cast.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ST_Cast.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ST_Cast.Hovered = false;
			this.ST_Cast.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Cast;
			this.ST_Cast.Location = new System.Drawing.Point(300, 6);
			this.ST_Cast.Margin = new System.Windows.Forms.Padding(0);
			this.ST_Cast.Name = "ST_Cast";
			this.ST_Cast.Selected = false;
			this.ST_Cast.Size = new System.Drawing.Size(150, 26);
			this.ST_Cast.TabIndex = 21;
			this.ST_Cast.Text = "Cast";
			this.ST_Cast.TabSelected += new System.EventHandler(this.ST_Cast_TabSelected);
			// 
			// SL_Backwards
			// 
			this.SL_Backwards.ActiveColor = null;
			this.SL_Backwards.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.SL_Backwards.Center = true;
			this.SL_Backwards.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Backwards.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Backwards.HideText = true;
			this.SL_Backwards.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Backwards.IconSize = 16;
			this.SL_Backwards.Image = global::ShowsCalendar.Properties.Resources.Tiny_JumpBack;
			this.SL_Backwards.Location = new System.Drawing.Point(700, 7);
			this.SL_Backwards.Margin = new System.Windows.Forms.Padding(0);
			this.SL_Backwards.Name = "SL_Backwards";
			this.SL_Backwards.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Backwards.Size = new System.Drawing.Size(39, 24);
			this.SL_Backwards.TabIndex = 20;
			this.SL_Backwards.TabStop = false;
			this.SL_Backwards.Click += new System.EventHandler(this.SL_Backwards_Click);
			// 
			// ST_Images
			// 
			this.ST_Images.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ST_Images.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ST_Images.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ST_Images.Hovered = false;
			this.ST_Images.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Image;
			this.ST_Images.Location = new System.Drawing.Point(150, 6);
			this.ST_Images.Margin = new System.Windows.Forms.Padding(0);
			this.ST_Images.Name = "ST_Images";
			this.ST_Images.Selected = false;
			this.ST_Images.Size = new System.Drawing.Size(150, 26);
			this.ST_Images.TabIndex = 0;
			this.ST_Images.Text = "Images";
			this.ST_Images.TabSelected += new System.EventHandler(this.ST_Images_TabSelected);
			// 
			// SL_Forward
			// 
			this.SL_Forward.ActiveColor = null;
			this.SL_Forward.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.SL_Forward.Center = true;
			this.SL_Forward.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Forward.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Forward.HideText = true;
			this.SL_Forward.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Forward.IconSize = 16;
			this.SL_Forward.Image = global::ShowsCalendar.Properties.Resources.Tiny_JumpForward;
			this.SL_Forward.Location = new System.Drawing.Point(739, 7);
			this.SL_Forward.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.SL_Forward.Name = "SL_Forward";
			this.SL_Forward.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Forward.Size = new System.Drawing.Size(39, 24);
			this.SL_Forward.TabIndex = 19;
			this.SL_Forward.TabStop = false;
			this.SL_Forward.Click += new System.EventHandler(this.SL_Forward_Click);
			// 
			// ST_Episodes
			// 
			this.ST_Episodes.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ST_Episodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ST_Episodes.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ST_Episodes.Hovered = false;
			this.ST_Episodes.Icon = global::ShowsCalendar.Properties.Resources.Tiny_TVEmpty;
			this.ST_Episodes.Location = new System.Drawing.Point(0, 6);
			this.ST_Episodes.Margin = new System.Windows.Forms.Padding(0);
			this.ST_Episodes.Name = "ST_Episodes";
			this.ST_Episodes.Selected = true;
			this.ST_Episodes.Size = new System.Drawing.Size(150, 26);
			this.ST_Episodes.TabIndex = 0;
			this.ST_Episodes.Text = "Episodes";
			this.ST_Episodes.TabSelected += new System.EventHandler(this.ST_Episodes_TabSelected);
			// 
			// ST_Crew
			// 
			this.ST_Crew.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ST_Crew.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ST_Crew.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ST_Crew.Hovered = false;
			this.ST_Crew.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Crew;
			this.ST_Crew.Location = new System.Drawing.Point(450, 6);
			this.ST_Crew.Margin = new System.Windows.Forms.Padding(0);
			this.ST_Crew.Name = "ST_Crew";
			this.ST_Crew.Selected = false;
			this.ST_Crew.Size = new System.Drawing.Size(150, 26);
			this.ST_Crew.TabIndex = 0;
			this.ST_Crew.Text = "Crew";
			this.ST_Crew.TabSelected += new System.EventHandler(this.ST_Crew_TabSelected);
			// 
			// PB_SeasonInfo
			// 
			this.PB_SeasonInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.PB_SeasonInfo.Location = new System.Drawing.Point(0, 30);
			this.PB_SeasonInfo.Name = "PB_SeasonInfo";
			this.PB_SeasonInfo.Size = new System.Drawing.Size(783, 135);
			this.PB_SeasonInfo.TabIndex = 15;
			this.PB_SeasonInfo.TabStop = false;
			this.PB_SeasonInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_SeasonInfo_Paint);
			this.PB_SeasonInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PB_SeasonInfo_MouseClick);
			this.PB_SeasonInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_SeasonInfo_MouseMove);
			// 
			// PC_SeasonView
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.PB_SeasonInfo);
			this.Name = "PC_SeasonView";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Resize += new System.EventHandler(this.PC_SeasonView_Resize);
			this.Controls.SetChildIndex(this.PB_SeasonInfo, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.SL_Backwards)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Forward)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PB_SeasonInfo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private SlickControls.Controls.SlickScroll verticalScroll;
		private System.Windows.Forms.FlowLayoutPanel FLP_Episodes;
		private ShowsCalendar.Controls.SlickPB PB_SeasonInfo;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private SlickControls.Controls.SlickTab ST_Images;
		private SlickControls.Controls.SlickTab ST_Episodes;
		private SlickControls.Controls.SlickTab ST_Crew;
		public SlickControls.Controls.SlickLabel SL_Backwards;
		public SlickControls.Controls.SlickLabel SL_Forward;
		private SlickControls.Controls.SlickTab ST_Cast;
	}
}
