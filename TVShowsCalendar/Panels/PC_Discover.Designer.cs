namespace TVShowsCalendar.Panels
{
	partial class PC_Discover
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
			this.TLP_4 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.FLP_Results = new System.Windows.Forms.FlowLayoutPanel();
			this.verticalScroll1 = new SlickControls.Controls.VerticalScroll();
			this.PB_Load = new System.Windows.Forms.PictureBox();
			this.TLP_4.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Load)).BeginInit();
			this.SuspendLayout();
			// 
			// TLP_4
			// 
			this.TLP_4.ColumnCount = 1;
			this.TLP_4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_4.Controls.Add(this.panel1, 0, 0);
			this.TLP_4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_4.Location = new System.Drawing.Point(5, 30);
			this.TLP_4.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_4.Name = "TLP_4";
			this.TLP_4.RowCount = 1;
			this.TLP_4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_4.Size = new System.Drawing.Size(778, 408);
			this.TLP_4.TabIndex = 12;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.FLP_Results);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(10, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(10, 0, 15, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(753, 408);
			this.panel1.TabIndex = 8;
			// 
			// FLP_Results
			// 
			this.FLP_Results.AutoSize = true;
			this.FLP_Results.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Results.Location = new System.Drawing.Point(0, 0);
			this.FLP_Results.Name = "FLP_Results";
			this.FLP_Results.Size = new System.Drawing.Size(0, 0);
			this.FLP_Results.TabIndex = 13;
			// 
			// verticalScroll1
			// 
			this.verticalScroll1.BarColor = null;
			this.verticalScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.verticalScroll1.LinkedControl = null;
			this.verticalScroll1.Location = new System.Drawing.Point(779, 30);
			this.verticalScroll1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.verticalScroll1.Name = "verticalScroll1";
			this.verticalScroll1.Size = new System.Drawing.Size(4, 408);
			this.verticalScroll1.TabIndex = 14;
			// 
			// PB_Load
			// 
			this.PB_Load.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.PB_Load.Location = new System.Drawing.Point(375, 203);
			this.PB_Load.Name = "PB_Load";
			this.PB_Load.Size = new System.Drawing.Size(32, 32);
			this.PB_Load.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Load.TabIndex = 16;
			this.PB_Load.TabStop = false;
			// 
			// PC_Discover
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.PB_Load);
			this.Controls.Add(this.verticalScroll1);
			this.Controls.Add(this.TLP_4);
			this.Name = "PC_Discover";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.ShowBack = true;
			this.Text = "Discover";
			this.Resize += new System.EventHandler(this.PC_Discover_Resize);
			this.Controls.SetChildIndex(this.TLP_4, 0);
			this.Controls.SetChildIndex(this.verticalScroll1, 0);
			this.Controls.SetChildIndex(this.PB_Load, 0);
			this.TLP_4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Load)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_4;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel FLP_Results;
		private SlickControls.Controls.VerticalScroll verticalScroll1;
		private System.Windows.Forms.PictureBox PB_Load;
	}
}
