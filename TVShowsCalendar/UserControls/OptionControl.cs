using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Extensions;
using TVShowsCalendar.Classes;

namespace TVShowsCalendar
{
	public enum OptionType { Checkbox, OptionList }

	public partial class OptionControl : UserControl
	{
		private bool _checked = true;
		private OptionType optionType = OptionType.Checkbox;
		private string text_CheckBox_Checked = "Checked";
		private string text_CheckBox_Unchecked = "Unchecked";

		public OptionControl()
		{
			InitializeComponent();

			ML_CheckBox.Click += (s, e) => ML_CheckBox_Click();
			ML_CheckBox.ActiveColor = () => FormDesign.Design.ActiveColor;

			FormDesign.DesignChanged += DesignChanged;
			DesignChanged(FormDesign.Design);
		}

		public event EventHandler ValueChanged;

		[Category("Data")]
		public bool Checked { get => _checked; set { _checked = value; CheckUpdate(); } }

		[Category("Data")]
		public string[] OptionList
		{
			get => CB_OptionList.Items as string[];
			set => CB_OptionList.Items = value;
		}

		[Category("Behavior")]
		public OptionType OptionType { get => optionType; set { optionType = value; SetOptionType(); } }

		[Category("Behavior")]
		public Setting SettingType { get; set; }

		[Category("Data")]
		public string SelectedOption { get => CB_OptionList.Text; set => CB_OptionList.Text = value; }

		[Category("Appearance")]
		public string Text_CheckBox_Checked { get => text_CheckBox_Checked; set { text_CheckBox_Checked = value; if (_checked) ML_CheckBox.Text = value; } }

		[Category("Appearance")]
		public string Text_CheckBox_Unchecked { get => text_CheckBox_Unchecked; set { text_CheckBox_Unchecked = value; if (!_checked) ML_CheckBox.Text = value; } }

		[Category("Appearance")]
		public string Text_Info { get => L_Info.Text; set => L_Info.Text = value; }

		[Category("Appearance")]
		public string Text_Title { get => L_Title.Text; set => L_Title.Text = value; }

		public void Disable()
		{
			ML_CheckBox.ForeColor = FormDesign.Design.Type.If(FormDesignType.Dark, FormDesign.Design.BackColor.Tint(Lum: 20), FormDesign.Design.ForeColor.Tint(Lum: 30));
			ML_CheckBox.Enabled = false;
			CB_OptionList.Enabled = false;
		}

		public void Enable()
		{
			ML_CheckBox.ForeColor = Color.Empty;
			ML_CheckBox.Enabled = true;
			CB_OptionList.Enabled = true;
		}

		private void CheckUpdate()
		{
			ML_CheckBox.Image = _checked ? Properties.Resources.Tiny_ToggleOn : Properties.Resources.Tiny_ToggleOff;
			ML_CheckBox.Text = _checked ? text_CheckBox_Checked : text_CheckBox_Unchecked;
			ValueChanged?.Invoke(this, new EventArgs());
		}

		private void DesignChanged(FormDesign design)
		{
			L_Title.ForeColor = design.LabelColor;
			L_Info.ForeColor = design.InfoColor;
			P_Spacer_1.BackColor = design.AccentColor;
			if (!CB_OptionList.Enabled)
				ML_CheckBox.ForeColor = design.Type.If(FormDesignType.Dark, design.BackColor.Tint(Lum:  20), design.ForeColor.Tint(Lum: 30));
		}

		private void ML_CheckBox_Click()
		{
			_checked = !_checked;
			CheckUpdate();
		}

		private void SetOptionType()
		{
			ML_CheckBox.Visible = optionType == OptionType.Checkbox;
			CB_OptionList.Visible = optionType == OptionType.OptionList;
		}

		private void CB_OptionList_TextChanged(object sender, EventArgs e)
		{
			ValueChanged?.Invoke(this, e);
		}
	}
}