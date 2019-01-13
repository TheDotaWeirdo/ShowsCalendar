namespace TVShowsCalendar.UserControls
{
	partial class SectionPanel
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
			this.TLP = new System.Windows.Forms.TableLayoutPanel();
			this.Content = new System.Windows.Forms.FlowLayoutPanel();
			this.L_Label = new System.Windows.Forms.Label();
			this.P_Line = new System.Windows.Forms.Panel();
			this.PB_Icon = new System.Windows.Forms.PictureBox();
			this.L_Info = new System.Windows.Forms.Label();
			this.TLP.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Icon)).BeginInit();
			this.SuspendLayout();
			// 
			// TLP
			// 
			this.TLP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP.ColumnCount = 3;
			this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP.Controls.Add(this.Content, 1, 2);
			this.TLP.Controls.Add(this.L_Label, 1, 0);
			this.TLP.Controls.Add(this.P_Line, 1, 1);
			this.TLP.Controls.Add(this.PB_Icon, 0, 0);
			this.TLP.Controls.Add(this.L_Info, 2, 0);
			this.TLP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP.Location = new System.Drawing.Point(0, 0);
			this.TLP.Name = "TLP";
			this.TLP.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
			this.TLP.RowCount = 3;
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.Size = new System.Drawing.Size(621, 167);
			this.TLP.TabIndex = 1;
			// 
			// FLP_Content
			// 
			this.Content.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP.SetColumnSpan(this.Content, 2);
			this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Content.Location = new System.Drawing.Point(43, 54);
			this.Content.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Content.Name = "FLP_Content";
			this.Content.Size = new System.Drawing.Size(578, 113);
			this.Content.TabIndex = 0;
			this.Content.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.FLP_Content_ControlsChanged);
			this.Content.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.FLP_Content_ControlsChanged);
			this.Content.Resize += new System.EventHandler(this.FLP_Content_Resize);
			// 
			// L_Label
			// 
			this.L_Label.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Label.AutoSize = true;
			this.L_Label.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_Label.Location = new System.Drawing.Point(50, 24);
			this.L_Label.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			this.L_Label.Name = "L_Label";
			this.L_Label.Size = new System.Drawing.Size(41, 17);
			this.L_Label.TabIndex = 1;
			this.L_Label.Text = "Label";
			// 
			// P_Line
			// 
			this.P_Line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(192)))), ((int)(((byte)(212)))));
			this.TLP.SetColumnSpan(this.P_Line, 2);
			this.P_Line.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Line.Location = new System.Drawing.Point(70, 50);
			this.P_Line.Margin = new System.Windows.Forms.Padding(30, 0, 100, 0);
			this.P_Line.Name = "P_Line";
			this.P_Line.Size = new System.Drawing.Size(451, 1);
			this.P_Line.TabIndex = 2;
			// 
			// PB_Icon
			// 
			this.PB_Icon.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.PB_Icon.Location = new System.Drawing.Point(15, 22);
			this.PB_Icon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.PB_Icon.Name = "PB_Icon";
			this.PB_Icon.Size = new System.Drawing.Size(22, 22);
			this.PB_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_Icon.TabIndex = 3;
			this.PB_Icon.TabStop = false;
			// 
			// L_Info
			// 
			this.L_Info.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Info.AutoSize = true;
			this.L_Info.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic);
			this.L_Info.Location = new System.Drawing.Point(104, 26);
			this.L_Info.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
			this.L_Info.Name = "L_Info";
			this.L_Info.Size = new System.Drawing.Size(0, 15);
			this.L_Info.TabIndex = 1;
			// 
			// SectionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.TLP);
			this.Name = "SectionPanel";
			this.Size = new System.Drawing.Size(621, 167);
			this.AutoSizeChanged += new System.EventHandler(this.SectionPanel_AutoSizeChanged);
			this.TLP.ResumeLayout(false);
			this.TLP.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Icon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TLP;
		public System.Windows.Forms.FlowLayoutPanel Content;
		private System.Windows.Forms.Label L_Label;
		private System.Windows.Forms.Panel P_Line;
		private System.Windows.Forms.PictureBox PB_Icon;
		private System.Windows.Forms.Label L_Info;
	}
}
