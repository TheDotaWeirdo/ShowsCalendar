﻿namespace ShowsCalendar.Controls
{
	partial class WatchControl
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
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// WatchControl
			// 
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Font = new System.Drawing.Font("Century Gothic", 11.25F);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
			this.Margin = new System.Windows.Forms.Padding(10);
			this.Size = new System.Drawing.Size(300, 220);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.WatchControl_Paint);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Master_Click);
			this.MouseEnter += new System.EventHandler(this.WatchControl_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.WatchControl_MouseLeave);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WatchControl_MouseMove);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
    }
}
