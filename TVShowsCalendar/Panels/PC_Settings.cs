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
using TVShowsCalendar.Classes;
using System.IO;
using SlickControls.Forms;
using Extensions;
using SlickControls.Enums;
using System.Drawing.Drawing2D;
using SlickControls.Controls;

namespace TVShowsCalendar.Panels
{
	public partial class PC_Settings : PanelContent
	{
		private bool valuesChanged = false;

		public PC_Settings()
		{
			InitializeComponent();

			if (!Data.FirstTimeSetup)
			{
				OC_Quality.SelectedOption = GeneralMethods.QualityDicId2Txt[Data.Options.PrefferedQuality];
				OC_DownloadOption.Checked = Data.Options.ShowAllDownloads;
				OC_StartMode.Checked = Data.Options.StartupMode;
				OC_DownloadBehavior.Checked = Data.Options.DownloadBehavior;
				OC_EpBehavior.Checked = Data.Options.EpisodeBehavior;
				OC_EpNotification.Checked = Data.Options.EpisodeNotification;
				OC_NotificationSound.Checked = Data.Options.NotificationSound;
				OC_UnwatchedBanner.Checked = Data.Options.ShowUnwatchedOnThumb;
				OC_FinaleWarning.Checked = Data.Options.FinaleWarning;
				OC_FullScreenPlayer.Checked = Data.Options.FullScreenPlayer;
				OC_ForwardTime.SelectedOption = $"{Data.Options.ForwardTime} seconds";
				OC_BackwardTime.SelectedOption = $"{Data.Options.BackwardTime} seconds";
				OC_MoviesRefreshDays.SelectedOption = $"{Data.Options.MoviesRefreshDays} days";
				OC_ShowsRefreshDays.SelectedOption = $"{Data.Options.ShowsRefreshDays} days";

				OC_LaunchWithWindows.Checked = File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk");

				if (!GeneralMethods.IsAdministrator)
					OC_LaunchWithWindows.Disable();
			}

			P_General.Tag = T_General.Tag = Setting.General;
			P_App.Tag = T_App.Tag = Setting.App;
			P_Download.Tag = T_Downloads.Tag = Setting.Download;
			P_Watch.Tag = T_Watch.Tag = Setting.Watch;

			valuesChanged = Data.FirstTimeSetup;

			SlickTip.SetTo(B_Done, "Applies all the chosen settings and closes this window");
		}

		private void B_Apply_Click(object sender, EventArgs e)
		{
			Data.Options.PrefferedQuality = GeneralMethods.QualityDicId2Txt.Where(x => x.Value == OC_Quality.SelectedOption).FirstOrDefault().Key;
			Data.Options.ShowAllDownloads = OC_DownloadOption.Checked;
			Data.Options.StartupMode = OC_StartMode.Checked;
			Data.Options.LaunchWithWindows = OC_LaunchWithWindows.Checked;
			Data.Options.DownloadBehavior = OC_DownloadBehavior.Checked;
			Data.Options.EpisodeBehavior = OC_EpBehavior.Checked;
			Data.Options.EpisodeNotification = OC_EpNotification.Checked;
			Data.Options.NotificationSound = OC_NotificationSound.Checked;
			Data.Options.ShowUnwatchedOnThumb = OC_UnwatchedBanner.Checked;
			Data.Options.FinaleWarning = OC_FinaleWarning.Checked;
			Data.Options.FullScreenPlayer = OC_FullScreenPlayer.Checked;
			Data.Options.ForwardTime = OC_ForwardTime.SelectedOption.SmartParse();
			Data.Options.BackwardTime = OC_BackwardTime.SelectedOption.SmartParse();
			Data.Options.MoviesRefreshDays = OC_MoviesRefreshDays.SelectedOption.SmartParse();
			Data.Options.ShowsRefreshDays = OC_ShowsRefreshDays.SelectedOption.SmartParse();

			Data.Options.Save();

			if (GeneralMethods.IsAdministrator)
			{
				if (OC_LaunchWithWindows.Checked)
					GeneralMethods.CreateShortcut(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk", Path.GetFullPath(".") + @"\TVShowsCalendar.exe");
				else if (File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk"))
					File.Delete(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk");
			}

			valuesChanged = false;

			if (Data.FirstTimeSetup)
			{
				Data.Dashboard.Setup(1);
			}
		}

		private void P_Spacer_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.BackColor);
			e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.AccentColor), 0, 0, 1, P_Spacer.Height - 150);

			e.Graphics.FillRectangle(new LinearGradientBrush(
				new RectangleF(0, P_Spacer.Height - 175, 1, 150),
				FormDesign.Design.AccentColor,
				FormDesign.Design.BackColor,
				90),
				new RectangleF(0, P_Spacer.Height - 175, 1, 150));
		}

		private void SettingsForm_Resize(object sender, EventArgs e)
		{
			P_Spacer.Refresh();
			P_Spacer_2.Refresh();
		}

		public override bool CanExit()
		{
			if (valuesChanged)
			{
				if (DialogResult.Yes == ShowPrompt("Do you want to save your changes before leaving?", "Save Changes?", PromptButtons.YesNo, PromptIcons.Question))
					B_Apply_Click(null, null);
			}

			return true;
		}

		private void P_OptionContainer_Resize(object sender, EventArgs e)
		{
			P_Options.MaximumSize = new Size(P_OptionContainer.Width, 9999);
			P_Options.MinimumSize = new Size(P_OptionContainer.Width, 0);
		}

		private void Tile_Click(object sender, EventArgs e)
		{
			P_OptionContainer.SuspendDrawing();
			foreach (var item in P_Options.Controls.ThatAre<Panel>())
				item.Visible = (sender as Control).Tag == item.Tag;
			foreach (var item in P_Tiles.Controls.ThatAre<SlickTile>())
				item.Selected = item == sender;
			P_OptionContainer.ResumeDrawing();
		}

		private void OC_ValueChanged(object sender, EventArgs e) => valuesChanged = true;

		private void P_Spacer_2_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.BackColor);
			e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.AccentColor), 0, 0, P_Spacer_2.Width - 150, 1);

			e.Graphics.FillRectangle(new LinearGradientBrush(
				new RectangleF(P_Spacer_2.Width - 175, 0, 150, 1),
				FormDesign.Design.AccentColor,
				FormDesign.Design.BackColor,
				0F),
				new RectangleF(P_Spacer_2.Width - 175, 0, 150, 1));
		}
	}
}
