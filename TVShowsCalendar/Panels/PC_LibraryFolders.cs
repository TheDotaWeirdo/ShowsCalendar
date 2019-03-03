using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlickControls.Panels;
using SlickControls.Enums;
using Extensions;
using ShowsCalendar.Handlers;
using System.IO;
using ShowsCalendar.Classes;

namespace ShowsCalendar.Panels
{
	public partial class PC_LibraryFolders : PanelContent
	{
		public PC_LibraryFolders()
		{
			InitializeComponent();
			LoadFolders();

			B_Done.Visible = Data.FirstTimeSetup;
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);
			label2.ForeColor = design.LabelColor;
		}

		private void B_Add_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TB_Path.Text) && (Directory.Exists(TB_Path.Text) || ShowPrompt("The selected folder does not currently exist.\n\nWould you like to add it anyway?",
				"Directory Unavailable", PromptButtons.YesNo, PromptIcons.Question) == DialogResult.Yes))
			{
				LocalFileHandler.AddGeneralFolder(TB_Path.Text);
				LoadFolders();
				TB_Path.Text = string.Empty;
			}
		}

		private void LoadFolders()
		{
			FLP_Folders.SuspendDrawing();
			FLP_Folders.Controls.Clear(true);

			foreach (var item in LocalFileHandler.GeneralFolders)
			{
				var myL = new SlickControls.Controls.SlickLabel()
				{
					Text = item.FullName,
					Image = Properties.Resources.Tiny_Folder,
					IconSize = 16,
					Cursor = Cursors.Hand,
					Font = new Font("Century Gothic", 9),
					Padding = new Padding(7, 6, 7, 6),
					ActiveColor = () => FormDesign.Design.RedColor
				};
				myL.HoverStateChanged += (s) =>
				{
					switch (s)
					{
						case HoverState.Normal:
							myL.Image = Properties.Resources.Tiny_Folder;
							break;
						default:
							myL.Image = Properties.Resources.Tiny_Trash;
							break;
					}
				};
				myL.Click += (s, e) =>
				{
					if (ShowPrompt("Are you sure you want to remove this folder?", "Confirm Action", PromptButtons.OKCancel, PromptIcons.Hand) == DialogResult.OK)
					{
						LocalFileHandler.RemoveGeneralFolder(item.FullName);
						myL.Dispose();
					}
				};

				FLP_Folders.Controls.Add(myL);
			}

			FLP_Folders.ResumeDrawing();
		}

		public override bool CanExit()
		{
			if (!string.IsNullOrWhiteSpace(TB_Path.Text))
			{
				if (DialogResult.Yes == ShowPrompt($"You haven't added the folder:\n'{TB_Path.Text}'\n\nWould you like to add it?", "Add Folder?", PromptButtons.YesNo, PromptIcons.Question))
					B_Add_Click(null, null);
			}

			return true;
		}

		private void B_Done_Click(object sender, EventArgs e)
		{
			Data.Mainform.Setup(2);
		}

		private void B_SearchMedia_Click(object sender, EventArgs e)
		{
			Form.PushPanel<PC_DiscoverLibrary>(null);
		}
	}
}
