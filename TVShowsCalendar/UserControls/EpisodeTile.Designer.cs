namespace TVShowsCalendar
{
	partial class EpisodeTile
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
			// EpisodeTile
			// 
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Size = new System.Drawing.Size(400, 125);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.EpisodeTile_Paint);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EpisodeTile_MouseClick);
			this.MouseEnter += new System.EventHandler(this.EpisodeTile_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.EpisodeTile_MouseLeave);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EpisodeTile_MouseMove);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
    }
}
