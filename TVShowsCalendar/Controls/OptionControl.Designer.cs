namespace ShowsCalendar
{
	partial class OptionControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionControl));
			this.L_Title = new System.Windows.Forms.Label();
			this.L_Info = new System.Windows.Forms.Label();
			this.P_Spacer_1 = new System.Windows.Forms.Panel();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.CB_OptionList = new SlickControls.Controls.SlickDropdown();
			this.ML_CheckBox = new SlickControls.Controls.SlickLabel();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ML_CheckBox)).BeginInit();
			this.SuspendLayout();
			// 
			// L_Title
			// 
			this.L_Title.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Title.AutoSize = true;
			this.L_Title.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold);
			this.L_Title.Location = new System.Drawing.Point(10, 5);
			this.L_Title.Margin = new System.Windows.Forms.Padding(10, 5, 3, 0);
			this.L_Title.Name = "L_Title";
			this.L_Title.Size = new System.Drawing.Size(36, 17);
			this.L_Title.TabIndex = 0;
			this.L_Title.Text = "Title";
			// 
			// L_Info
			// 
			this.L_Info.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Info.AutoEllipsis = true;
			this.L_Info.AutoSize = true;
			this.L_Info.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_Info.Location = new System.Drawing.Point(20, 35);
			this.L_Info.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.L_Info.Name = "L_Info";
			this.L_Info.Size = new System.Drawing.Size(28, 13);
			this.L_Info.TabIndex = 1;
			this.L_Info.Text = "Info\r\n";
			// 
			// P_Spacer_1
			// 
			this.P_Spacer_1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.tableLayoutPanel1.SetColumnSpan(this.P_Spacer_1, 2);
			this.P_Spacer_1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Spacer_1.Location = new System.Drawing.Point(30, 61);
			this.P_Spacer_1.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
			this.P_Spacer_1.Name = "P_Spacer_1";
			this.P_Spacer_1.Size = new System.Drawing.Size(440, 1);
			this.P_Spacer_1.TabIndex = 3;
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 20000;
			this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.toolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(65)))), ((int)(((byte)(77)))));
			this.toolTip.InitialDelay = 600;
			this.toolTip.ReshowDelay = 100;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.L_Info, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.L_Title, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.P_Spacer_1, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 62);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.CB_OptionList);
			this.panel1.Controls.Add(this.ML_CheckBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(273, 3);
			this.panel1.Name = "panel1";
			this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
			this.panel1.Size = new System.Drawing.Size(224, 55);
			this.panel1.TabIndex = 8;
			// 
			// CB_OptionList
			// 
			this.CB_OptionList.Conversion = null;
			this.CB_OptionList.Font = new System.Drawing.Font("Nirmala UI", 9.75F);
			this.CB_OptionList.Image = ((System.Drawing.Image)(resources.GetObject("CB_OptionList.Image")));
			this.CB_OptionList.Items = null;
			this.CB_OptionList.LabelText = "Dropdown";
			this.CB_OptionList.Location = new System.Drawing.Point(12, 17);
			this.CB_OptionList.MaximumSize = new System.Drawing.Size(900, 20);
			this.CB_OptionList.MaxLength = 32767;
			this.CB_OptionList.MinimumSize = new System.Drawing.Size(50, 20);
			this.CB_OptionList.Name = "CB_OptionList";
			this.CB_OptionList.Password = false;
			this.CB_OptionList.Placeholder = null;
			this.CB_OptionList.ReadOnly = false;
			this.CB_OptionList.Required = false;
			this.CB_OptionList.SelectAllOnFocus = false;
			this.CB_OptionList.SelectedItem = null;
			this.CB_OptionList.SelectedText = "";
			this.CB_OptionList.SelectionLength = 0;
			this.CB_OptionList.SelectionStart = 0;
			this.CB_OptionList.ShowLabel = false;
			this.CB_OptionList.Size = new System.Drawing.Size(164, 20);
			this.CB_OptionList.TabIndex = 5;
			this.CB_OptionList.Text = "slickDropdown1";
			this.CB_OptionList.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.CB_OptionList.Validation = SlickControls.Enums.ValidationType.None;
			this.CB_OptionList.ValidationRegex = "";
			this.CB_OptionList.TextChanged += new System.EventHandler(this.CB_OptionList_TextChanged);
			// 
			// ML_CheckBox
			// 
			this.ML_CheckBox.ActiveColor = null;
			this.ML_CheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.ML_CheckBox.Center = false;
			this.ML_CheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ML_CheckBox.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
			this.ML_CheckBox.HideText = false;
			this.ML_CheckBox.HoverState = SlickControls.Enums.HoverState.Normal;
			this.ML_CheckBox.IconSize = 16;
			this.ML_CheckBox.Image = global::ShowsCalendar.Properties.Resources.Tiny_ToggleOn;
			this.ML_CheckBox.Location = new System.Drawing.Point(6, 12);
			this.ML_CheckBox.Name = "ML_CheckBox";
			this.ML_CheckBox.Padding = new System.Windows.Forms.Padding(7, 5, 5, 5);
			this.ML_CheckBox.Size = new System.Drawing.Size(92, 26);
			this.ML_CheckBox.TabIndex = 4;
			this.ML_CheckBox.TabStop = false;
			this.ML_CheckBox.Text = "myLabel1";
			// 
			// OptionControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Nirmala UI", 12F);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "OptionControl";
			this.Size = new System.Drawing.Size(500, 62);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ML_CheckBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label L_Title;
		private System.Windows.Forms.Label L_Info;
		private System.Windows.Forms.Panel P_Spacer_1;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private SlickControls.Controls.SlickDropdown CB_OptionList;
		private SlickControls.Controls.SlickLabel ML_CheckBox;
	}
}
