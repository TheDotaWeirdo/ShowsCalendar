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
using ShowsCalendar.Classes;
using System.IO;
using SlickControls.Forms;
using Extensions;
using SlickControls.Enums;
using System.Drawing.Drawing2D;
using SlickControls.Controls;

namespace ShowsCalendar.Panels
{
	public partial class PC_Settings : PanelContent
	{
		private bool valuesChanged = false;

		public PC_Settings()
		{
			InitializeComponent();

			PC_ShowsOrder.OptionList = PC_MoviesOrder.OptionList = typeof(MediaSortOptions).GetEnumDescs();

			if (!Data.FirstTimeSetup)
			{
				OC_Quality.SelectedOption = GeneralMethods.QualityDicId2Txt[Data.Options.PrefferedQuality.If(0, 7)];
				OC_DownloadOption.Checked = Data.Options.ShowAllDownloads;
				OC_StartMode.Checked = Data.Options.StartupMode;
				OC_DownloadBehavior.Checked = Data.Options.DownloadBehavior;
				OC_EpNotification.Checked = Data.Options.EpisodeNotification;
				OC_NotificationSound.Checked = Data.Options.NotificationSound;
				OC_UnwatchedBanner.Checked = Data.Options.ShowUnwatchedOnThumb;
				OC_FinaleWarning.Checked = Data.Options.FinaleWarning;
				OC_FullScreenPlayer.Checked = Data.Options.FullScreenPlayer;
				OC_EpBehavior.Checked = Data.Options.OpenAllPagesForEp;
				OC_AutoEpSwitch.Checked = Data.Options.AutomaticEpisodeSwitching;
				OC_AutoPauseScroll.Checked = Data.Options.AutoPauseOnInfo;
				OC_AutoEpPLay.Checked = Data.Options.AutomaticEpisodePlay;
                OC_DisableMini.Checked = Data.Options.DisabledMiniplayer;
				OC_DiscoverPages.SelectedOption = $"{Data.Options.DiscoverResults} results";
				OC_ForwardTime.SelectedOption = $"{Data.Options.ForwardTime} seconds";
				OC_BackwardTime.SelectedOption = $"{Data.Options.BackwardTime} seconds";
				PC_ShowsOrder.SelectedOption = Data.Options.ShowSorting.GetDescription();
				PC_MoviesOrder.SelectedOption = Data.Options.MovieSorting.GetDescription();

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
			Data.Options.PrefferedQuality = GeneralMethods.QualityDicId2Txt.Where(x => x.Value == OC_Quality.SelectedOption).FirstOrDefault().Key.If(0, 7);
			Data.Options.ShowAllDownloads = OC_DownloadOption.Checked;
			Data.Options.StartupMode = OC_StartMode.Checked;
			Data.Options.LaunchWithWindows = OC_LaunchWithWindows.Checked;
			Data.Options.DownloadBehavior = OC_DownloadBehavior.Checked;
			Data.Options.EpisodeNotification = OC_EpNotification.Checked;
			Data.Options.NotificationSound = OC_NotificationSound.Checked;
			Data.Options.ShowUnwatchedOnThumb = OC_UnwatchedBanner.Checked;
			Data.Options.FinaleWarning = OC_FinaleWarning.Checked;
			Data.Options.OpenAllPagesForEp = OC_EpBehavior.Checked;
			Data.Options.FullScreenPlayer = OC_FullScreenPlayer.Checked;
			Data.Options.AutomaticEpisodeSwitching = OC_AutoEpSwitch.Checked;
			Data.Options.AutomaticEpisodePlay = OC_AutoEpPLay.Checked;
			Data.Options.AutoPauseOnInfo = OC_AutoPauseScroll.Checked;
            Data.Options.DisabledMiniplayer = OC_DisableMini.Checked;
			Data.Options.ForwardTime = OC_ForwardTime.SelectedOption.SmartParse();
			Data.Options.BackwardTime = OC_BackwardTime.SelectedOption.SmartParse();
			Data.Options.DiscoverResults = OC_DiscoverPages.SelectedOption.SmartParse();
			Data.Options.ShowSorting = (MediaSortOptions)typeof(MediaSortOptions).GetEnumValueFromDescs(PC_ShowsOrder.SelectedOption);
			Data.Options.MovieSorting = (MediaSortOptions)typeof(MediaSortOptions).GetEnumValueFromDescs(PC_MoviesOrder.SelectedOption);

			Data.Options.Save();

			if (GeneralMethods.IsAdministrator)
			{
				if (OC_LaunchWithWindows.Checked)
					GeneralMethods.CreateShortcut(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk", Path.GetFullPath(".") + @"\ShowsCalendar.exe");
				else if (File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk"))
					File.Delete(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\Shows Calendar.lnk");
			}

			valuesChanged = false;

			if (Data.FirstTimeSetup)
			{
				Data.Mainform.Setup(1);
			}
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			P_OptionContainer.BackColor = B_Done.BackColor = verticalScroll1.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 3, -3));
		}

		public override bool CanExit(bool toBeDisposed)
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
			P_OptionContainer.ResumeDrawing();
		}

		private void OC_ValueChanged(object sender, EventArgs e) => valuesChanged = true;
	}
}
