namespace ShowsCalendar
{
	partial class MovieTile
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
			// MovieTile
			// 
			this.Margin = new System.Windows.Forms.Padding(10);
			this.Size = new System.Drawing.Size(250, 200);
			this.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.MovieTile_LoadCompleted);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MovieTile_Paint);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MovieTile_MouseClick);
			this.MouseEnter += new System.EventHandler(this.MovieTile_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.MovieTile_MouseLeave);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MovieTile_MouseMove);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
	}
}
