﻿namespace ShowsCalendar
{
	partial class ShowTile
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
			// ShowTile
			// 
			this.Font = new System.Drawing.Font("Century Gothic", 8.25F);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.Margin = new System.Windows.Forms.Padding(10);
			this.Size = new System.Drawing.Size(250, 200);
			this.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.ShowTile_LoadCompleted);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ShowTile_Paint);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ShowTile_MouseClick);
			this.MouseEnter += new System.EventHandler(this.ShowTile_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.ShowTile_MouseLeave);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ShowTile_MouseMove);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
	}
}
