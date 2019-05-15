namespace ShowsCalendar.Panels
{
	partial class PC_Player
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private Vlc.DotNet.Forms.VlcControl vlcControl;

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
			this.TLP_Controls = new System.Windows.Forms.TableLayoutPanel();
			this.L_CurrentTime = new System.Windows.Forms.Label();
			this.TLP_Buttons = new System.Windows.Forms.TableLayoutPanel();
			this.SL_Play = new SlickControls.Controls.SlickLabel();
			this.SL_Forward = new SlickControls.Controls.SlickLabel();
			this.SL_Next = new SlickControls.Controls.SlickLabel();
			this.SL_Backwards = new SlickControls.Controls.SlickLabel();
			this.SL_Previous = new SlickControls.Controls.SlickLabel();
			this.SL_Subs = new SlickControls.Controls.SlickLabel();
			this.SS_Volume = new SlickControls.Controls.SlickSlider();
			this.SL_Audio = new SlickControls.Controls.SlickLabel();
			this.SL_MiniPlayer = new SlickControls.Controls.SlickLabel();
			this.SL_More = new SlickControls.Controls.SlickLabel();
			this.SL_FullScreen = new SlickControls.Controls.SlickLabel();
			this.SS_TimeSlider = new SlickControls.Controls.SlickSlider();
			this.L_Time = new System.Windows.Forms.Label();
			this.P_Progress = new System.Windows.Forms.Panel();
			this.P_BotSpacer = new System.Windows.Forms.Panel();
			this.P_VLC = new System.Windows.Forms.Panel();
			this.P_Info = new System.Windows.Forms.Panel();
			this.TLP_SimilarContent = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Suggestions = new System.Windows.Forms.TableLayoutPanel();
			this.SP_Crew = new SlickControls.Controls.SlickSectionPanel();
			this.SP_Similar = new SlickControls.Controls.SlickSectionPanel();
			this.SP_Cast = new SlickControls.Controls.SlickSectionPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.L_3 = new System.Windows.Forms.Label();
			this.L_AirDate = new System.Windows.Forms.Label();
			this.L_4 = new System.Windows.Forms.Label();
			this.L_Plot = new System.Windows.Forms.Label();
			this.L_6 = new System.Windows.Forms.Label();
			this.L_Resolution = new System.Windows.Forms.Label();
			this.L_7 = new System.Windows.Forms.Label();
			this.L_Subtitles = new System.Windows.Forms.Label();
			this.slickSectionPanel1 = new SlickControls.Controls.SlickSectionPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.TLP_MoreInfo = new System.Windows.Forms.TableLayoutPanel();
			this.L_MoreInfo = new System.Windows.Forms.Label();
			this.I_MoreInfo = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.slickScroll = new SlickControls.Controls.SlickScroll();
			this.TLP_Controls.SuspendLayout();
			this.TLP_Buttons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SL_Play)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Forward)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Next)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Backwards)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Previous)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Subs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Audio)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_MiniPlayer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_More)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_FullScreen)).BeginInit();
			this.P_BotSpacer.SuspendLayout();
			this.P_Info.SuspendLayout();
			this.TLP_Suggestions.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.TLP_MoreInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.I_MoreInfo)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// TLP_Controls
			// 
			this.TLP_Controls.AutoSize = true;
			this.TLP_Controls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Controls.ColumnCount = 3;
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Controls.Controls.Add(this.L_CurrentTime, 0, 1);
			this.TLP_Controls.Controls.Add(this.TLP_Buttons, 0, 2);
			this.TLP_Controls.Controls.Add(this.SS_TimeSlider, 1, 1);
			this.TLP_Controls.Controls.Add(this.L_Time, 2, 1);
			this.TLP_Controls.Controls.Add(this.P_Progress, 0, 0);
			this.TLP_Controls.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Controls.Location = new System.Drawing.Point(0, 2);
			this.TLP_Controls.Name = "TLP_Controls";
			this.TLP_Controls.RowCount = 3;
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Controls.Size = new System.Drawing.Size(696, 66);
			this.TLP_Controls.TabIndex = 2;
			// 
			// L_CurrentTime
			// 
			this.L_CurrentTime.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_CurrentTime.AutoSize = true;
			this.L_CurrentTime.Location = new System.Drawing.Point(9, 11);
			this.L_CurrentTime.Margin = new System.Windows.Forms.Padding(9, 0, 0, 0);
			this.L_CurrentTime.Name = "L_CurrentTime";
			this.L_CurrentTime.Size = new System.Drawing.Size(0, 13);
			this.L_CurrentTime.TabIndex = 4;
			// 
			// TLP_Buttons
			// 
			this.TLP_Buttons.ColumnCount = 13;
			this.TLP_Controls.SetColumnSpan(this.TLP_Buttons, 3);
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.Controls.Add(this.SL_Play, 6, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Forward, 7, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Next, 8, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Backwards, 5, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Previous, 4, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Subs, 0, 0);
			this.TLP_Buttons.Controls.Add(this.SS_Volume, 2, 0);
			this.TLP_Buttons.Controls.Add(this.SL_Audio, 1, 0);
			this.TLP_Buttons.Controls.Add(this.SL_MiniPlayer, 10, 0);
			this.TLP_Buttons.Controls.Add(this.SL_More, 12, 0);
			this.TLP_Buttons.Controls.Add(this.SL_FullScreen, 11, 0);
			this.TLP_Buttons.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Buttons.Location = new System.Drawing.Point(0, 32);
			this.TLP_Buttons.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Buttons.Name = "TLP_Buttons";
			this.TLP_Buttons.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.TLP_Buttons.RowCount = 1;
			this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Buttons.Size = new System.Drawing.Size(696, 34);
			this.TLP_Buttons.TabIndex = 0;
			// 
			// SL_Play
			// 
			this.SL_Play.ActiveColor = null;
			this.SL_Play.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Play.Center = true;
			this.SL_Play.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Play.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Play.HideText = true;
			this.SL_Play.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Play.IconSize = 16;
			this.SL_Play.Image = global::ShowsCalendar.Properties.Resources.Tiny_PlayNoBorder;
			this.SL_Play.Location = new System.Drawing.Point(331, 3);
			this.SL_Play.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Play.Name = "SL_Play";
			this.SL_Play.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Play.Size = new System.Drawing.Size(39, 24);
			this.SL_Play.TabIndex = 3;
			this.SL_Play.TabStop = false;
			this.SL_Play.Click += new System.EventHandler(this.SL_Play_Click);
			// 
			// SL_Forward
			// 
			this.SL_Forward.ActiveColor = null;
			this.SL_Forward.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Forward.Center = true;
			this.SL_Forward.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Forward.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Forward.HideText = true;
			this.SL_Forward.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Forward.IconSize = 16;
			this.SL_Forward.Image = global::ShowsCalendar.Properties.Resources.Tiny_JumpForward;
			this.SL_Forward.Location = new System.Drawing.Point(376, 3);
			this.SL_Forward.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Forward.Name = "SL_Forward";
			this.SL_Forward.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Forward.Size = new System.Drawing.Size(39, 24);
			this.SL_Forward.TabIndex = 3;
			this.SL_Forward.TabStop = false;
			this.SL_Forward.Click += new System.EventHandler(this.SL_Forward_Click);
			// 
			// SL_Next
			// 
			this.SL_Next.ActiveColor = null;
			this.SL_Next.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Next.Center = true;
			this.SL_Next.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Next.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Next.HideText = true;
			this.SL_Next.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Next.IconSize = 16;
			this.SL_Next.Image = global::ShowsCalendar.Properties.Resources.Tiny_Next;
			this.SL_Next.Location = new System.Drawing.Point(421, 3);
			this.SL_Next.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Next.Name = "SL_Next";
			this.SL_Next.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Next.Size = new System.Drawing.Size(39, 24);
			this.SL_Next.TabIndex = 3;
			this.SL_Next.TabStop = false;
			this.SL_Next.Visible = false;
			this.SL_Next.Click += new System.EventHandler(this.SL_Next_Click);
			// 
			// SL_Backwards
			// 
			this.SL_Backwards.ActiveColor = null;
			this.SL_Backwards.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Backwards.Center = true;
			this.SL_Backwards.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Backwards.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Backwards.HideText = true;
			this.SL_Backwards.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Backwards.IconSize = 16;
			this.SL_Backwards.Image = global::ShowsCalendar.Properties.Resources.Tiny_JumpBack;
			this.SL_Backwards.Location = new System.Drawing.Point(286, 3);
			this.SL_Backwards.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Backwards.Name = "SL_Backwards";
			this.SL_Backwards.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Backwards.Size = new System.Drawing.Size(39, 24);
			this.SL_Backwards.TabIndex = 3;
			this.SL_Backwards.TabStop = false;
			this.SL_Backwards.Click += new System.EventHandler(this.SL_Backwards_Click);
			// 
			// SL_Previous
			// 
			this.SL_Previous.ActiveColor = null;
			this.SL_Previous.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Previous.Center = true;
			this.SL_Previous.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Previous.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Previous.HideText = true;
			this.SL_Previous.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Previous.IconSize = 16;
			this.SL_Previous.Image = global::ShowsCalendar.Properties.Resources.Tiny_Previous;
			this.SL_Previous.Location = new System.Drawing.Point(241, 3);
			this.SL_Previous.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Previous.Name = "SL_Previous";
			this.SL_Previous.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Previous.Size = new System.Drawing.Size(39, 24);
			this.SL_Previous.TabIndex = 3;
			this.SL_Previous.TabStop = false;
			this.SL_Previous.Visible = false;
			this.SL_Previous.Click += new System.EventHandler(this.SL_Previous_Click);
			// 
			// SL_Subs
			// 
			this.SL_Subs.ActiveColor = null;
			this.SL_Subs.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Subs.Center = true;
			this.SL_Subs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Subs.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Subs.HideText = true;
			this.SL_Subs.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Subs.IconSize = 16;
			this.SL_Subs.Image = global::ShowsCalendar.Properties.Resources.Tiny_CC;
			this.SL_Subs.Location = new System.Drawing.Point(3, 3);
			this.SL_Subs.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Subs.Name = "SL_Subs";
			this.SL_Subs.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Subs.Size = new System.Drawing.Size(39, 24);
			this.SL_Subs.TabIndex = 3;
			this.SL_Subs.TabStop = false;
			this.SL_Subs.Click += new System.EventHandler(this.SL_Subs_Click);
			// 
			// SS_Volume
			// 
			this.TLP_Buttons.SetColumnSpan(this.SS_Volume, 2);
			this.SS_Volume.Dock = System.Windows.Forms.DockStyle.Left;
			this.SS_Volume.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
			this.SS_Volume.FromValue = 0D;
			this.SS_Volume.Location = new System.Drawing.Point(90, 3);
			this.SS_Volume.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.SS_Volume.MaxValue = 200D;
			this.SS_Volume.MinValue = 0D;
			this.SS_Volume.Name = "SS_Volume";
			this.SS_Volume.Padding = new System.Windows.Forms.Padding(7, 11, 7, 7);
			this.SS_Volume.Percentage = 0.5D;
			this.SS_Volume.PercFrom = 0D;
			this.SS_Volume.PercTo = 0.5D;
			this.SS_Volume.ShowValues = false;
			this.SS_Volume.Size = new System.Drawing.Size(100, 28);
			this.SS_Volume.SliderStyle = SlickControls.Controls.SliderStyle.SingleHorizontal;
			this.SS_Volume.TabIndex = 5;
			this.SS_Volume.ToValue = 100D;
			this.SS_Volume.Value = 100D;
			this.SS_Volume.ValueOutput = null;
			this.SS_Volume.ValuesChanged += new System.EventHandler(this.SS_Volume_ValuesChanged);
			this.SS_Volume.Click += new System.EventHandler(this.SS_Volume_ValuesChanged);
			// 
			// SL_Audio
			// 
			this.SL_Audio.ActiveColor = null;
			this.SL_Audio.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_Audio.Center = true;
			this.SL_Audio.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_Audio.Enabled = false;
			this.SL_Audio.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_Audio.HideText = true;
			this.SL_Audio.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_Audio.IconSize = 16;
			this.SL_Audio.Image = global::ShowsCalendar.Properties.Resources.Tiny_Sound;
			this.SL_Audio.Location = new System.Drawing.Point(48, 3);
			this.SL_Audio.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_Audio.Name = "SL_Audio";
			this.SL_Audio.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_Audio.Size = new System.Drawing.Size(39, 24);
			this.SL_Audio.TabIndex = 3;
			this.SL_Audio.TabStop = false;
			this.SL_Audio.Click += new System.EventHandler(this.SL_Audio_Click);
			// 
			// SL_MiniPlayer
			// 
			this.SL_MiniPlayer.ActiveColor = null;
			this.SL_MiniPlayer.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_MiniPlayer.Center = true;
			this.SL_MiniPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_MiniPlayer.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_MiniPlayer.HideText = true;
			this.SL_MiniPlayer.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_MiniPlayer.IconSize = 16;
			this.SL_MiniPlayer.Image = global::ShowsCalendar.Properties.Resources.Tiny_MiniWindow;
			this.SL_MiniPlayer.Location = new System.Drawing.Point(564, 3);
			this.SL_MiniPlayer.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_MiniPlayer.Name = "SL_MiniPlayer";
			this.SL_MiniPlayer.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_MiniPlayer.Size = new System.Drawing.Size(39, 24);
			this.SL_MiniPlayer.TabIndex = 3;
			this.SL_MiniPlayer.TabStop = false;
			this.SL_MiniPlayer.Click += new System.EventHandler(this.SL_MiniPlayer_Click);
			// 
			// SL_More
			// 
			this.SL_More.ActiveColor = null;
			this.SL_More.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_More.Center = true;
			this.SL_More.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_More.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_More.HideText = true;
			this.SL_More.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_More.IconSize = 16;
			this.SL_More.Image = global::ShowsCalendar.Properties.Resources.Tiny_Dots_V;
			this.SL_More.Location = new System.Drawing.Point(654, 3);
			this.SL_More.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_More.Name = "SL_More";
			this.SL_More.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_More.Size = new System.Drawing.Size(39, 24);
			this.SL_More.TabIndex = 6;
			this.SL_More.TabStop = false;
			this.SL_More.Click += new System.EventHandler(this.SI_More_Click);
			// 
			// SL_FullScreen
			// 
			this.SL_FullScreen.ActiveColor = null;
			this.SL_FullScreen.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.SL_FullScreen.Center = true;
			this.SL_FullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SL_FullScreen.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SL_FullScreen.HideText = true;
			this.SL_FullScreen.HoverState = SlickControls.Enums.HoverState.Normal;
			this.SL_FullScreen.IconSize = 16;
			this.SL_FullScreen.Image = global::ShowsCalendar.Properties.Resources.Tiny_Fullscreen;
			this.SL_FullScreen.Location = new System.Drawing.Point(609, 3);
			this.SL_FullScreen.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.SL_FullScreen.Name = "SL_FullScreen";
			this.SL_FullScreen.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.SL_FullScreen.Size = new System.Drawing.Size(39, 24);
			this.SL_FullScreen.TabIndex = 6;
			this.SL_FullScreen.TabStop = false;
			this.SL_FullScreen.Click += new System.EventHandler(this.SL_FullScreen_Click);
			// 
			// SS_TimeSlider
			// 
			this.SS_TimeSlider.Dock = System.Windows.Forms.DockStyle.Top;
			this.SS_TimeSlider.DrawValues = false;
			this.SS_TimeSlider.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
			this.SS_TimeSlider.FromValue = 0D;
			this.SS_TimeSlider.Location = new System.Drawing.Point(9, 4);
			this.SS_TimeSlider.Margin = new System.Windows.Forms.Padding(0);
			this.SS_TimeSlider.MaxValue = 100D;
			this.SS_TimeSlider.MinValue = 0D;
			this.SS_TimeSlider.Name = "SS_TimeSlider";
			this.SS_TimeSlider.Padding = new System.Windows.Forms.Padding(14, 15, 14, 0);
			this.SS_TimeSlider.Percentage = 0D;
			this.SS_TimeSlider.PercFrom = 0D;
			this.SS_TimeSlider.PercTo = 0D;
			this.SS_TimeSlider.Size = new System.Drawing.Size(678, 28);
			this.SS_TimeSlider.SliderStyle = SlickControls.Controls.SliderStyle.SingleHorizontal;
			this.SS_TimeSlider.TabIndex = 1;
			this.SS_TimeSlider.ToValue = 0D;
			this.SS_TimeSlider.Value = 0D;
			this.SS_TimeSlider.ValueOutput = null;
			this.SS_TimeSlider.ValuesChanged += new System.EventHandler(this.SS_TimeSlider_ValuesChanged);
			this.SS_TimeSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SS_TimeSlider_MouseDown);
			this.SS_TimeSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SS_TimeSlider_MouseUp);
			// 
			// L_Time
			// 
			this.L_Time.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Time.AutoSize = true;
			this.L_Time.Location = new System.Drawing.Point(687, 11);
			this.L_Time.Margin = new System.Windows.Forms.Padding(0, 0, 9, 0);
			this.L_Time.Name = "L_Time";
			this.L_Time.Size = new System.Drawing.Size(0, 13);
			this.L_Time.TabIndex = 2;
			// 
			// P_Progress
			// 
			this.TLP_Controls.SetColumnSpan(this.P_Progress, 3);
			this.P_Progress.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Progress.Location = new System.Drawing.Point(0, 0);
			this.P_Progress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.P_Progress.Name = "P_Progress";
			this.P_Progress.Size = new System.Drawing.Size(696, 2);
			this.P_Progress.TabIndex = 5;
			this.P_Progress.Visible = false;
			this.P_Progress.Paint += new System.Windows.Forms.PaintEventHandler(this.P_Progress_Paint);
			// 
			// P_BotSpacer
			// 
			this.P_BotSpacer.Controls.Add(this.TLP_Controls);
			this.P_BotSpacer.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_BotSpacer.Location = new System.Drawing.Point(0, 275);
			this.P_BotSpacer.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.P_BotSpacer.Name = "P_BotSpacer";
			this.P_BotSpacer.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.P_BotSpacer.Size = new System.Drawing.Size(696, 64);
			this.P_BotSpacer.TabIndex = 15;
			// 
			// P_VLC
			// 
			this.P_VLC.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_VLC.Location = new System.Drawing.Point(0, 0);
			this.P_VLC.Name = "P_VLC";
			this.P_VLC.Size = new System.Drawing.Size(696, 275);
			this.P_VLC.TabIndex = 14;
			// 
			// P_Info
			// 
			this.P_Info.AutoSize = true;
			this.P_Info.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Info.Controls.Add(this.TLP_SimilarContent);
			this.P_Info.Controls.Add(this.TLP_Suggestions);
			this.P_Info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Info.Location = new System.Drawing.Point(0, 339);
			this.P_Info.Name = "P_Info";
			this.P_Info.Size = new System.Drawing.Size(696, 460);
			this.P_Info.TabIndex = 16;
			// 
			// TLP_SimilarContent
			// 
			this.TLP_SimilarContent.AutoSize = true;
			this.TLP_SimilarContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_SimilarContent.ColumnCount = 2;
			this.TLP_SimilarContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_SimilarContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_SimilarContent.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_SimilarContent.Location = new System.Drawing.Point(0, 460);
			this.TLP_SimilarContent.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_SimilarContent.Name = "TLP_SimilarContent";
			this.TLP_SimilarContent.Padding = new System.Windows.Forms.Padding(43, 0, 0, 0);
			this.TLP_SimilarContent.RowCount = 1;
			this.TLP_SimilarContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_SimilarContent.Size = new System.Drawing.Size(696, 0);
			this.TLP_SimilarContent.TabIndex = 22;
			// 
			// TLP_Suggestions
			// 
			this.TLP_Suggestions.AutoSize = true;
			this.TLP_Suggestions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Suggestions.ColumnCount = 4;
			this.TLP_Suggestions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
			this.TLP_Suggestions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 275F));
			this.TLP_Suggestions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Suggestions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 275F));
			this.TLP_Suggestions.Controls.Add(this.SP_Crew, 0, 3);
			this.TLP_Suggestions.Controls.Add(this.SP_Similar, 0, 4);
			this.TLP_Suggestions.Controls.Add(this.SP_Cast, 0, 2);
			this.TLP_Suggestions.Controls.Add(this.tableLayoutPanel1, 2, 1);
			this.TLP_Suggestions.Controls.Add(this.slickSectionPanel1, 0, 0);
			this.TLP_Suggestions.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Suggestions.Location = new System.Drawing.Point(0, 0);
			this.TLP_Suggestions.Name = "TLP_Suggestions";
			this.TLP_Suggestions.RowCount = 5;
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Suggestions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Suggestions.Size = new System.Drawing.Size(696, 460);
			this.TLP_Suggestions.TabIndex = 21;
			// 
			// SP_Crew
			// 
			this.SP_Crew.Active = false;
			this.SP_Crew.AutoHide = true;
			this.SP_Crew.AutoSize = true;
			this.SP_Crew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Suggestions.SetColumnSpan(this.SP_Crew, 4);
			this.SP_Crew.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Crew.Flavor = null;
			this.SP_Crew.Icon = global::ShowsCalendar.Properties.Resources.Big_Crew;
			this.SP_Crew.Info = "Know the people behind the works of this episode";
			this.SP_Crew.Location = new System.Drawing.Point(3, 343);
			this.SP_Crew.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Crew.Name = "SP_Crew";
			this.SP_Crew.Size = new System.Drawing.Size(690, 54);
			this.SP_Crew.TabIndex = 6;
			this.SP_Crew.Text = "Who\'s behind this?";
			// 
			// SP_Similar
			// 
			this.SP_Similar.Active = false;
			this.SP_Similar.AutoHide = false;
			this.SP_Similar.AutoSize = true;
			this.SP_Similar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Suggestions.SetColumnSpan(this.SP_Similar, 4);
			this.SP_Similar.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Similar.Flavor = null;
			this.SP_Similar.Icon = global::ShowsCalendar.Properties.Resources.Big_Rating;
			this.SP_Similar.Info = "Recognize someone? You probably saw them here";
			this.SP_Similar.Location = new System.Drawing.Point(3, 403);
			this.SP_Similar.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Similar.Name = "SP_Similar";
			this.SP_Similar.Size = new System.Drawing.Size(690, 54);
			this.SP_Similar.TabIndex = 5;
			this.SP_Similar.Text = "Where have I seen them?";
			// 
			// SP_Cast
			// 
			this.SP_Cast.Active = false;
			this.SP_Cast.AutoHide = true;
			this.SP_Cast.AutoSize = true;
			this.SP_Cast.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Suggestions.SetColumnSpan(this.SP_Cast, 4);
			this.SP_Cast.Dock = System.Windows.Forms.DockStyle.Top;
			this.SP_Cast.Flavor = null;
			this.SP_Cast.Icon = global::ShowsCalendar.Properties.Resources.Big_Cast;
			this.SP_Cast.Info = "From your beloved cast to this episode\'s notable guest stars";
			this.SP_Cast.Location = new System.Drawing.Point(3, 283);
			this.SP_Cast.MaximumSize = new System.Drawing.Size(9999, 54);
			this.SP_Cast.Name = "SP_Cast";
			this.SP_Cast.Size = new System.Drawing.Size(690, 54);
			this.SP_Cast.TabIndex = 4;
			this.SP_Cast.Text = "Who\'s in this?";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.TLP_Suggestions.SetColumnSpan(this.tableLayoutPanel1, 2);
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Controls.Add(this.L_3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.L_AirDate, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.L_4, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.L_Plot, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.L_6, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.L_Resolution, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.L_7, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.L_Subtitles, 2, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(321, 63);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(372, 214);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// L_3
			// 
			this.L_3.AutoSize = true;
			this.L_3.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_3.Location = new System.Drawing.Point(13, 13);
			this.L_3.Margin = new System.Windows.Forms.Padding(3);
			this.L_3.Name = "L_3";
			this.L_3.Size = new System.Drawing.Size(56, 15);
			this.L_3.TabIndex = 0;
			this.L_3.Text = "Air Date:";
			// 
			// L_AirDate
			// 
			this.L_AirDate.AutoSize = true;
			this.L_AirDate.Location = new System.Drawing.Point(13, 31);
			this.L_AirDate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 7);
			this.L_AirDate.Name = "L_AirDate";
			this.L_AirDate.Size = new System.Drawing.Size(38, 13);
			this.L_AirDate.TabIndex = 1;
			this.L_AirDate.Text = "label2";
			// 
			// L_4
			// 
			this.L_4.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.L_4, 2);
			this.L_4.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_4.Location = new System.Drawing.Point(13, 61);
			this.L_4.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.L_4.Name = "L_4";
			this.L_4.Size = new System.Drawing.Size(32, 15);
			this.L_4.TabIndex = 0;
			this.L_4.Text = "Plot:";
			// 
			// L_Plot
			// 
			this.L_Plot.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.L_Plot, 3);
			this.L_Plot.Location = new System.Drawing.Point(13, 79);
			this.L_Plot.Margin = new System.Windows.Forms.Padding(3, 0, 3, 7);
			this.L_Plot.Name = "L_Plot";
			this.L_Plot.Size = new System.Drawing.Size(38, 13);
			this.L_Plot.TabIndex = 1;
			this.L_Plot.Text = "label2";
			// 
			// L_6
			// 
			this.L_6.AutoSize = true;
			this.L_6.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_6.Location = new System.Drawing.Point(189, 13);
			this.L_6.Margin = new System.Windows.Forms.Padding(3);
			this.L_6.Name = "L_6";
			this.L_6.Size = new System.Drawing.Size(69, 15);
			this.L_6.TabIndex = 0;
			this.L_6.Text = "Resolution:";
			// 
			// L_Resolution
			// 
			this.L_Resolution.AutoSize = true;
			this.L_Resolution.Location = new System.Drawing.Point(189, 31);
			this.L_Resolution.Margin = new System.Windows.Forms.Padding(3, 0, 3, 7);
			this.L_Resolution.Name = "L_Resolution";
			this.L_Resolution.Size = new System.Drawing.Size(38, 13);
			this.L_Resolution.TabIndex = 1;
			this.L_Resolution.Text = "label2";
			// 
			// L_7
			// 
			this.L_7.AutoSize = true;
			this.L_7.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_7.Location = new System.Drawing.Point(277, 13);
			this.L_7.Margin = new System.Windows.Forms.Padding(3);
			this.L_7.Name = "L_7";
			this.L_7.Size = new System.Drawing.Size(61, 15);
			this.L_7.TabIndex = 0;
			this.L_7.Text = "SubTitles:";
			// 
			// L_Subtitles
			// 
			this.L_Subtitles.AutoSize = true;
			this.L_Subtitles.Location = new System.Drawing.Point(277, 31);
			this.L_Subtitles.Margin = new System.Windows.Forms.Padding(3, 0, 3, 7);
			this.L_Subtitles.Name = "L_Subtitles";
			this.L_Subtitles.Size = new System.Drawing.Size(38, 13);
			this.L_Subtitles.TabIndex = 1;
			this.L_Subtitles.Text = "label2";
			// 
			// slickSectionPanel1
			// 
			this.slickSectionPanel1.Active = false;
			this.slickSectionPanel1.AutoHide = false;
			this.slickSectionPanel1.AutoSize = true;
			this.slickSectionPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Suggestions.SetColumnSpan(this.slickSectionPanel1, 4);
			this.slickSectionPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSectionPanel1.Flavor = null;
			this.slickSectionPanel1.Icon = global::ShowsCalendar.Properties.Resources.Big_Play;
			this.slickSectionPanel1.Info = "More about what\'s up there";
			this.slickSectionPanel1.Location = new System.Drawing.Point(3, 3);
			this.slickSectionPanel1.MaximumSize = new System.Drawing.Size(9999, 54);
			this.slickSectionPanel1.Name = "slickSectionPanel1";
			this.slickSectionPanel1.Size = new System.Drawing.Size(690, 54);
			this.slickSectionPanel1.TabIndex = 3;
			this.slickSectionPanel1.Text = "What you\'re watching";
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Controls.Add(this.TLP_MoreInfo);
			this.panel1.Controls.Add(this.P_Info);
			this.panel1.Controls.Add(this.P_BotSpacer);
			this.panel1.Controls.Add(this.P_VLC);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.MinimumSize = new System.Drawing.Size(696, 465);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(696, 799);
			this.panel1.TabIndex = 18;
			// 
			// TLP_MoreInfo
			// 
			this.TLP_MoreInfo.ColumnCount = 4;
			this.TLP_MoreInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_MoreInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MoreInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MoreInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_MoreInfo.Controls.Add(this.L_MoreInfo, 2, 0);
			this.TLP_MoreInfo.Controls.Add(this.I_MoreInfo, 1, 0);
			this.TLP_MoreInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.TLP_MoreInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_MoreInfo.Location = new System.Drawing.Point(0, 339);
			this.TLP_MoreInfo.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_MoreInfo.Name = "TLP_MoreInfo";
			this.TLP_MoreInfo.RowCount = 1;
			this.TLP_MoreInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_MoreInfo.Size = new System.Drawing.Size(696, 22);
			this.TLP_MoreInfo.TabIndex = 17;
			this.TLP_MoreInfo.Tag = "NoMouseDown";
			this.TLP_MoreInfo.Click += new System.EventHandler(this.TLP_MoreInfo_Click);
			this.TLP_MoreInfo.MouseEnter += new System.EventHandler(this.TLP_MoreInfo_MouseEnter);
			this.TLP_MoreInfo.MouseLeave += new System.EventHandler(this.TLP_MoreInfo_MouseLeave);
			// 
			// L_MoreInfo
			// 
			this.L_MoreInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_MoreInfo.AutoSize = true;
			this.L_MoreInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_MoreInfo.Location = new System.Drawing.Point(327, 4);
			this.L_MoreInfo.Name = "L_MoreInfo";
			this.L_MoreInfo.Size = new System.Drawing.Size(58, 13);
			this.L_MoreInfo.TabIndex = 0;
			this.L_MoreInfo.Tag = "NoMouseDown";
			this.L_MoreInfo.Text = "More Info";
			this.L_MoreInfo.Click += new System.EventHandler(this.TLP_MoreInfo_Click);
			this.L_MoreInfo.MouseEnter += new System.EventHandler(this.TLP_MoreInfo_MouseEnter);
			this.L_MoreInfo.MouseLeave += new System.EventHandler(this.TLP_MoreInfo_MouseLeave);
			// 
			// I_MoreInfo
			// 
			this.I_MoreInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.I_MoreInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_MoreInfo.Image = global::ShowsCalendar.Properties.Resources.ArrowDown;
			this.I_MoreInfo.Location = new System.Drawing.Point(308, 3);
			this.I_MoreInfo.Margin = new System.Windows.Forms.Padding(0);
			this.I_MoreInfo.Name = "I_MoreInfo";
			this.I_MoreInfo.Size = new System.Drawing.Size(16, 16);
			this.I_MoreInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.I_MoreInfo.TabIndex = 1;
			this.I_MoreInfo.TabStop = false;
			this.I_MoreInfo.Click += new System.EventHandler(this.TLP_MoreInfo_Click);
			this.I_MoreInfo.MouseEnter += new System.EventHandler(this.TLP_MoreInfo_MouseEnter);
			this.I_MoreInfo.MouseLeave += new System.EventHandler(this.TLP_MoreInfo_MouseLeave);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 30);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(696, 965);
			this.panel2.TabIndex = 19;
			// 
			// slickScroll
			// 
			this.slickScroll.LinkedControl = this.panel1;
			this.slickScroll.Location = new System.Drawing.Point(346, 298);
			this.slickScroll.Name = "slickScroll";
			this.slickScroll.ShowHandle = false;
			this.slickScroll.Size = new System.Drawing.Size(5, 99);
			this.slickScroll.SizeSource = null;
			this.slickScroll.Style = SlickControls.Controls.StyleType.Vertical;
			this.slickScroll.TabIndex = 20;
			this.slickScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.slickScroll_Scroll);
			// 
			// PC_Player
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.slickScroll);
			this.LabelBounds = new System.Drawing.Point(3, 1);
			this.Name = "PC_Player";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(696, 995);
			this.Load += new System.EventHandler(this.PC_Player_Load);
			this.Resize += new System.EventHandler(this.PC_Player_Resize);
			this.Controls.SetChildIndex(this.slickScroll, 0);
			this.Controls.SetChildIndex(this.panel2, 0);
			this.TLP_Controls.ResumeLayout(false);
			this.TLP_Controls.PerformLayout();
			this.TLP_Buttons.ResumeLayout(false);
			this.TLP_Buttons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.SL_Play)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Forward)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Next)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Backwards)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Previous)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Subs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_Audio)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_MiniPlayer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_More)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SL_FullScreen)).EndInit();
			this.P_BotSpacer.ResumeLayout(false);
			this.P_BotSpacer.PerformLayout();
			this.P_Info.ResumeLayout(false);
			this.P_Info.PerformLayout();
			this.TLP_Suggestions.ResumeLayout(false);
			this.TLP_Suggestions.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.TLP_MoreInfo.ResumeLayout(false);
			this.TLP_MoreInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.I_MoreInfo)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Controls;
		private System.Windows.Forms.Label L_CurrentTime;
		private System.Windows.Forms.TableLayoutPanel TLP_Buttons;
		public SlickControls.Controls.SlickLabel SL_Play;
		public SlickControls.Controls.SlickLabel SL_Forward;
		public SlickControls.Controls.SlickLabel SL_Next;
		public SlickControls.Controls.SlickLabel SL_Backwards;
		public SlickControls.Controls.SlickLabel SL_Previous;
		public SlickControls.Controls.SlickLabel SL_Subs;
		private SlickControls.Controls.SlickSlider SS_Volume;
		public SlickControls.Controls.SlickLabel SL_Audio;
		public SlickControls.Controls.SlickLabel SL_FullScreen;
		public SlickControls.Controls.SlickLabel SL_MiniPlayer;
		public SlickControls.Controls.SlickLabel SL_More;
		private SlickControls.Controls.SlickSlider SS_TimeSlider;
		private System.Windows.Forms.Label L_Time;
		private System.Windows.Forms.Panel P_BotSpacer;
		private System.Windows.Forms.Panel P_VLC;
		private System.Windows.Forms.Panel P_Info;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private SlickControls.Controls.SlickScroll slickScroll;
		private System.Windows.Forms.TableLayoutPanel TLP_Suggestions;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label L_3;
		private System.Windows.Forms.Label L_AirDate;
		private System.Windows.Forms.Label L_4;
		private System.Windows.Forms.Label L_Plot;
		private System.Windows.Forms.Label L_6;
		private System.Windows.Forms.Label L_Resolution;
		private System.Windows.Forms.Label L_7;
		private System.Windows.Forms.Label L_Subtitles;
		private SlickControls.Controls.SlickSectionPanel SP_Cast;
		private SlickControls.Controls.SlickSectionPanel slickSectionPanel1;
		private System.Windows.Forms.TableLayoutPanel TLP_MoreInfo;
		private System.Windows.Forms.Label L_MoreInfo;
		private System.Windows.Forms.PictureBox I_MoreInfo;
		private SlickControls.Controls.SlickSectionPanel SP_Similar;
		private System.Windows.Forms.TableLayoutPanel TLP_SimilarContent;
        private System.Windows.Forms.Panel P_Progress;
		private SlickControls.Controls.SlickSectionPanel SP_Crew;
	}
}
