namespace TVShowsCalendar
{
	partial class TorrentTile
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
			this.L_Size = new System.Windows.Forms.Label();
			this.L_Quality = new System.Windows.Forms.Label();
			this.L_Name = new System.Windows.Forms.Label();
			this.L_Sound = new System.Windows.Forms.Label();
			this.L_Subs = new System.Windows.Forms.Label();
			this.TableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.B_Download = new SlickControls.Controls.SlickButton();
			this.TableLayout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_Download)).BeginInit();
			this.SuspendLayout();
			// 
			// L_Size
			// 
			this.L_Size.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_Size.Location = new System.Drawing.Point(438, 0);
			this.L_Size.Name = "L_Size";
			this.L_Size.Size = new System.Drawing.Size(59, 35);
			this.L_Size.TabIndex = 0;
			this.L_Size.Text = "0 MB";
			this.L_Size.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_Quality
			// 
			this.L_Quality.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_Quality.Location = new System.Drawing.Point(373, 0);
			this.L_Quality.Name = "L_Quality";
			this.L_Quality.Size = new System.Drawing.Size(59, 35);
			this.L_Quality.TabIndex = 1;
			this.L_Quality.Text = "4K Ultra";
			this.L_Quality.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_Name
			// 
			this.L_Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Name.AutoEllipsis = true;
			this.L_Name.AutoSize = true;
			this.L_Name.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Name.Location = new System.Drawing.Point(10, 11);
			this.L_Name.Margin = new System.Windows.Forms.Padding(10, 0, 5, 0);
			this.L_Name.MaximumSize = new System.Drawing.Size(9999, 18);
			this.L_Name.Name = "L_Name";
			this.L_Name.Size = new System.Drawing.Size(26, 13);
			this.L_Name.TabIndex = 2;
			this.L_Name.Text = "Text";
			this.L_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.L_Name.Click += new System.EventHandler(this.L_Name_Click);
			// 
			// L_Sound
			// 
			this.L_Sound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_Sound.Location = new System.Drawing.Point(308, 0);
			this.L_Sound.Name = "L_Sound";
			this.L_Sound.Size = new System.Drawing.Size(59, 35);
			this.L_Sound.TabIndex = 1;
			this.L_Sound.Text = "5.1";
			this.L_Sound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_Subs
			// 
			this.L_Subs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_Subs.Location = new System.Drawing.Point(243, 0);
			this.L_Subs.Name = "L_Subs";
			this.L_Subs.Size = new System.Drawing.Size(59, 35);
			this.L_Subs.TabIndex = 1;
			this.L_Subs.Text = "EN, RU";
			this.L_Subs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TableLayout
			// 
			this.TableLayout.ColumnCount = 6;
			this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
			this.TableLayout.Controls.Add(this.L_Size, 4, 0);
			this.TableLayout.Controls.Add(this.L_Quality, 3, 0);
			this.TableLayout.Controls.Add(this.L_Sound, 2, 0);
			this.TableLayout.Controls.Add(this.L_Subs, 1, 0);
			this.TableLayout.Controls.Add(this.L_Name, 0, 0);
			this.TableLayout.Controls.Add(this.B_Download, 5, 0);
			this.TableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TableLayout.Location = new System.Drawing.Point(0, 0);
			this.TableLayout.Margin = new System.Windows.Forms.Padding(0);
			this.TableLayout.Name = "TableLayout";
			this.TableLayout.RowCount = 1;
			this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TableLayout.Size = new System.Drawing.Size(630, 35);
			this.TableLayout.TabIndex = 3;
			// 
			// B_Download
			// 
			this.B_Download.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Download.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.B_Download.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.B_Download.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Download.Font = new System.Drawing.Font("Nirmala UI", 8.25F);
			this.B_Download.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.B_Download.ColorShade = null;
			this.B_Download.IconSize = 16;
			this.B_Download.Image = global::TVShowsCalendar.Properties.Resources.Tiny_Download;
			this.B_Download.Location = new System.Drawing.Point(512, 3);
			this.B_Download.Margin = new System.Windows.Forms.Padding(0);
			this.B_Download.Name = "B_Download";
			this.B_Download.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.B_Download.Size = new System.Drawing.Size(105, 28);
			this.B_Download.TabIndex = 7;
			this.B_Download.Text = "DOWNLOAD";
			this.B_Download.Click += new System.EventHandler(this.B_Download_Click);
			// 
			// TorrentTile
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.TableLayout);
			this.Font = new System.Drawing.Font("Nirmala UI", 8.25F);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "TorrentTile";
			this.Size = new System.Drawing.Size(630, 35);
			this.TableLayout.ResumeLayout(false);
			this.TableLayout.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.B_Download)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private SlickControls.Controls.SlickButton B_Download;
		private System.Windows.Forms.Label L_Size;
		private System.Windows.Forms.Label L_Quality;
		private System.Windows.Forms.Label L_Name;
		private System.Windows.Forms.Label L_Sound;
		private System.Windows.Forms.Label L_Subs;
		private System.Windows.Forms.TableLayoutPanel TableLayout;
	}
}
