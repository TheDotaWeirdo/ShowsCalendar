namespace ShowsCalendar
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
			this.components = new System.ComponentModel.Container();
			this.I_Dots = new SlickControls.Controls.SlickIconComponent(this.components);
			this.I_Action = new SlickControls.Controls.SlickIconComponent(this.components);
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// I_Dots
			// 
			this.I_Dots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.I_Dots.Bounds = new System.Drawing.Rectangle(4, 6, 16, 16);
			this.I_Dots.Enabled = true;
			this.I_Dots.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Dots_V;
			this.I_Dots.Location = new System.Drawing.Point(4, 6);
			this.I_Dots.Parent = this;
			this.I_Dots.Size = new System.Drawing.Size(16, 16);
			this.I_Dots.MouseHoverChanged += new System.Windows.Forms.MouseEventHandler(this.EpisodeTile_MouseMove);
			this.I_Dots.Click += new System.Windows.Forms.MouseEventHandler(this.I_Dots_Click);
			// 
			// I_Action
			// 
			this.I_Action.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.I_Action.Bounds = new System.Drawing.Rectangle(24, 6, 16, 16);
			this.I_Action.Enabled = true;
			this.I_Action.Icon = global::ShowsCalendar.Properties.Resources.Tiny_Info;
			this.I_Action.Location = new System.Drawing.Point(24, 6);
			this.I_Action.Parent = this;
			this.I_Action.Size = new System.Drawing.Size(16, 16);
			this.I_Action.MouseHoverChanged += new System.Windows.Forms.MouseEventHandler(this.EpisodeTile_MouseMove);
			this.I_Action.Click += new System.Windows.Forms.MouseEventHandler(this.I_Action_Click);
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

		private SlickControls.Controls.SlickIconComponent I_Dots;
		private SlickControls.Controls.SlickIconComponent I_Action;
	}
}
