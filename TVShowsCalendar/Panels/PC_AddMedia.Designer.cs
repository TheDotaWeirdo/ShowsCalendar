namespace TVShowsCalendar.Panels
{
	partial class PC_AddMedia
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
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.ML_Info = new SlickControls.Controls.SlickIcon();
			this.TB_SeriesName = new SlickControls.Controls.SlickTextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_Results = new System.Windows.Forms.FlowLayoutPanel();
			this.verticalScroll = new SlickControls.Controls.VerticalScroll();
			this.TLP_Main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ML_Info)).BeginInit();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Controls.Add(this.ML_Info, 1, 0);
			this.TLP_Main.Controls.Add(this.TB_SeriesName, 0, 0);
			this.TLP_Main.Controls.Add(this.panel1, 0, 1);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(5, 30);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 2;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Size = new System.Drawing.Size(778, 408);
			this.TLP_Main.TabIndex = 0;
			// 
			// ML_Info
			// 
			this.ML_Info.ActiveColor = null;
			this.ML_Info.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.ML_Info.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ML_Info.HoverState = SlickControls.Enums.HoverState.Normal;
			this.ML_Info.Image = global::TVShowsCalendar.Properties.Resources.Icon_Info;
			this.ML_Info.Location = new System.Drawing.Point(741, 9);
			this.ML_Info.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
			this.ML_Info.MinimumIconSize = 20;
			this.ML_Info.Name = "ML_Info";
			this.ML_Info.Size = new System.Drawing.Size(22, 22);
			this.ML_Info.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.ML_Info.TabIndex = 14;
			this.ML_Info.TabStop = false;
			this.ML_Info.Click += new System.EventHandler(this.Info_Click);
			// 
			// TB_SeriesName
			// 
			this.TB_SeriesName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.TB_SeriesName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TB_SeriesName.Image = null;
			this.TB_SeriesName.LabelText = "Series Name";
			this.TB_SeriesName.Location = new System.Drawing.Point(30, 3);
			this.TB_SeriesName.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
			this.TB_SeriesName.MaximumSize = new System.Drawing.Size(9999, 34);
			this.TB_SeriesName.MaxLength = 32767;
			this.TB_SeriesName.MinimumSize = new System.Drawing.Size(50, 35);
			this.TB_SeriesName.Name = "TB_SeriesName";
			this.TB_SeriesName.Placeholder = "Type the name of a TV Show to search";
			this.TB_SeriesName.SelectedText = "";
			this.TB_SeriesName.SelectionLength = 0;
			this.TB_SeriesName.SelectionStart = 0;
			this.TB_SeriesName.Size = new System.Drawing.Size(437, 35);
			this.TB_SeriesName.TabIndex = 0;
			this.TB_SeriesName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.TB_SeriesName.Validation = SlickControls.Enums.ValidationType.None;
			this.TB_SeriesName.ValidationRegex = "";
			this.TB_SeriesName.TextChanged += new System.EventHandler(this.TB_FolderPath_TextChanged);
			// 
			// panel1
			// 
			this.TLP_Main.SetColumnSpan(this.panel1, 2);
			this.panel1.Controls.Add(this.tableLayoutPanel1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(15, 44);
			this.panel1.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(748, 361);
			this.panel1.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.FLP_Results, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(6, 6);
			this.tableLayoutPanel1.TabIndex = 12;
			// 
			// FLP_Results
			// 
			this.FLP_Results.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.FLP_Results.AutoSize = true;
			this.FLP_Results.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Results.Location = new System.Drawing.Point(3, 3);
			this.FLP_Results.Name = "FLP_Results";
			this.FLP_Results.Size = new System.Drawing.Size(0, 0);
			this.FLP_Results.TabIndex = 13;
			// 
			// verticalScroll
			// 
			this.verticalScroll.BarColor = null;
			this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll.LinkedControl = this.tableLayoutPanel1;
			this.verticalScroll.Location = new System.Drawing.Point(385, 29);
			this.verticalScroll.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll.Name = "verticalScroll";
			this.verticalScroll.Size = new System.Drawing.Size(12, 381);
			this.verticalScroll.TabIndex = 13;
			// 
			// PC_AddMedia
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.verticalScroll);
			this.Controls.Add(this.TLP_Main);
			this.Name = "PC_AddMedia";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.ShowBack = true;
			this.Resize += new System.EventHandler(this.TMDbForm_Resize);
			this.Controls.SetChildIndex(this.TLP_Main, 0);
			this.Controls.SetChildIndex(this.verticalScroll, 0);
			this.TLP_Main.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ML_Info)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel FLP_Results;
		private SlickControls.Controls.VerticalScroll verticalScroll;
		private SlickControls.Controls.SlickTextBox TB_SeriesName;
		private SlickControls.Controls.SlickIcon ML_Info;
	}
}
