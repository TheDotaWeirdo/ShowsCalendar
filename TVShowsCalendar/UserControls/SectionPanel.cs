using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Extensions;

namespace TVShowsCalendar.UserControls
{
	[Designer(typeof(SectionPanelControlDesigner))]
	public partial class SectionPanel : SlickControls.Controls.SlickControl
	{
		public SectionPanel()
		{
			InitializeComponent();
		}

		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Bindable(true)]
		public new string Text { get => base.Text; set => L_Label.Text = base.Text = value; }

		[Category("Appearance")]
		public string Info { get => L_Info.Text; set => L_Info.Text = value; }

		[Category("Appearance")]
		public Image Icon
		{
			get => PB_Icon.Image;
			set => PB_Icon.Image = value.If(!DesignMode, new Bitmap(value).Color(Active ? FormDesign.Design.ActiveColor : FormDesign.Design.IconColor));
		}

		[Category("Appearance")]
		public bool Active { get; set; }

		[Category("Appearance")]
		public string[] Flavor { get; set; }

		protected override void DesignChanged(FormDesign design)
		{
			L_Label.ForeColor = Active ? design.ActiveColor : design.LabelColor;
			L_Info.ForeColor = design.InfoColor;
			P_Line.BackColor = design.AccentColor;
			PB_Icon.Color(Active ? design.ActiveColor : design.IconColor);
		}

		[Category("Behavior")]
		public bool AutoHide { get; set; }

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			if (Flavor != null && Flavor.Any())
				Info = Flavor.Random();

			if (AutoHide && !DesignMode)
				Visible = Content.Controls.Count > 0;
		}

		private void SectionPanel_AutoSizeChanged(object sender, EventArgs e)
		{
			Content.AutoSize = TLP.AutoSize = AutoSize;

			Content.Dock = TLP.Dock = AutoSize ? DockStyle.Top : DockStyle.Fill;
		}

		public void Add(params Control[] controls)
		{
			Content.Controls.AddRange(controls);
		}

		public void Add(IEnumerable<Control> controls)
		{
			Content.Controls.AddRange(controls.ToArray());
		}

		public void Remove(params Control[] controls)
		{
			foreach (var item in controls)
				Content.Controls.Remove(item);
		}

		public void Remove(IEnumerable<Control> controls)
		{
			foreach (var item in controls)
				Content.Controls.Remove(item);
		}

		private void FLP_Content_Resize(object sender, EventArgs e)
		{
			MaximumSize = new Size(9999, Content.Height + Content.Top);
		}

		private void FLP_Content_ControlsChanged(object sender, ControlEventArgs e)
		{
			 if (AutoHide && !DesignMode)
				Visible = Content.Controls.Count > 0;
			MaximumSize = new Size(9999, Content.Height + Content.Top);
		}
	}

	public class SectionPanelControlDesigner : ParentControlDesigner
	{
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);

			if (Control is SectionPanel sp)
			{
				EnableDesignMode(sp.Content, "Content");
			}
		}
	}
}