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
using Extensions;
using TVShowsCalendar.Classes;
using TVShowsCalendar.Forms;
using SlickControls.Enums;
using TVShowsCalendar.UserControls;
using SlickControls.Forms;

namespace TVShowsCalendar.Panels
{
	public partial class PC_Shows : PanelContent
	{
		private Dictionary<Show, ShowTile> Tiles = new Dictionary<Show, ShowTile>();
		private string lastSearch;

		public PC_Shows()
		{
			InitializeComponent();

			verticalScroll.SizeSource = () => P_Tabs.Controls.Cast<Control>().Sum(c => c.If(x => x.Visible, x => x.Height, 0));

			foreach (var show in ShowManager.Shows.Where(x => x.Loaded))
				AddShow(show);

			TLP_NoShows.Visible = ShowManager.Shows.Count == 0;
			SetLoading();

			SlickTip.SetTo(PB_Search, "Click to Search");

			ShowManager.ShowAdded += ShowManager_ShowAdded;
			ShowManager.ShowLoaded += ShowManager_ShowLoaded;
			ShowManager.ShowRemoved += ShowManager_ShowRemoved;

			Disposed += (s, e) =>
			{
				ShowManager.ShowAdded -= ShowManager_ShowAdded;
				ShowManager.ShowLoaded -= ShowManager_ShowLoaded;
				ShowManager.ShowRemoved -= ShowManager_ShowRemoved;
			};
		}

		private void SetLoading()
		{
			if (ShowManager.Shows.Any(x => !x.Loaded))
				StartLoader();
			else
				StopLoader();

			P_Tabs.SuspendDrawing();
			SP_Airing.BringToFront();
			SP_Upcoming.BringToFront();
			SP_Returning.BringToFront();
			SP_Ended.BringToFront();
			P_Tabs.ResumeDrawing();
		}

		private void ShowManager_ShowRemoved(Show show)
		{
			this.TryInvoke(() =>
			{
				SetLoading();
				TLP_NoShows.Visible = ShowManager.Shows.Count == 0;
			});
		}

		private void ShowManager_ShowLoaded(Show show)
		{
			this.TryInvoke(() =>
			{
				TLP_NoShows.Visible = false;
				AddShow(show);
				SetLoading();
			});
		}
		private void ShowManager_ShowAdded(Show show)
		{
			this.TryInvoke(() =>
			{
				SetLoading();
				TLP_NoShows.Visible = false;
			});
		}

		private void AddShow(Show show, ShowTile tile = null)
		{
			if (show.TMDbData == null)
				return;

			var panel = SP_Returning;

			if (show.TMDbData.Status == "Ended" || show.TMDbData.Status == "Canceled")
				panel = SP_Ended;
			else if (((!show.LastEpisode?.Empty ?? false) && show.LastEpisode.TMDbData.AirDate >= DateTime.Today.AddDays(-7))
				|| ((!show.NextEpisode?.Empty ?? false) && show.NextEpisode.TMDbData.AirDate <= DateTime.Today.AddDays(7)))
				panel = SP_Airing;
			else if ((!show.NextEpisode?.Empty ?? false) && show.NextEpisode.AirState == AirStateEnum.ToBeAired)
				panel = SP_Upcoming;

			if (tile == null)
			{
				foreach (var item in P_Tabs.Controls.ThatAre<SectionPanel>())
					item.Content.Controls.Clear(true, x => (x is ShowTile st) && st.LinkedShow == show);

				tile = new ShowTile(show);

				Tiles.TryAdd(show, tile);

				if (CheckSearch(show))
					panel.Content.Controls.Add(tile);
			}
			else
				panel.Content.Controls.Add(tile);
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_NoShowsInfo.ForeColor = design.InfoColor;

			L_NoShows.ForeColor = design.LabelColor;

			PB_Search.Color(searchOpened ? design.ActiveColor : design.IconColor);
		}

		private void PC_Show_Resize(object sender, EventArgs e)
		{
			P_Tabs.MaximumSize = new Size(Width - 12, 9999);
			P_Tabs.MinimumSize = new Size(Width - 12, 0);
		}

		private void B_Add_Click(object sender, EventArgs e)
		{
			if (ConnectionHandler.State == Extensions.ConnectionState.Connected)
			{
				Form.PushPanel(null, new PC_AddMedia(false));
			}
			else
			{
				ShowPrompt("You can not add Shows without Internet connection.\n\nCheck your connectivity then try again.",
					"No Connection",
					 PromptButtons.OK,
					 PromptIcons.Hand);
			}
		}

		private void B_Discover_Click(object sender, EventArgs e)
		{
			if (ConnectionHandler.State == Extensions.ConnectionState.Connected)
			{
				Form.PushPanel(null, new PC_Discover(false));
			}
			else
			{
				ShowPrompt("You can not discover Shows without Internet connection.\n\nCheck your connectivity then try again.",
					"No Connection",
					 PromptButtons.OK,
					 PromptIcons.Hand);
			}
		}

		private void TB_Search_TextChanged(object sender, EventArgs e)
		{
			if (searchOpened && (lastSearch == TB_Search.Text || Tiles.Count == 0))
				return;

			var results = false;

			P_Tabs.SuspendDrawing();
			foreach (var item in Tiles.Values)
			{
				if (CheckSearch(item.LinkedShow))
				{
					AddShow(item.LinkedShow, item);
					results = true;
				}
				else
					item.Parent?.Controls.Remove(item);
			}
			P_Tabs.ResumeDrawing();

			TLP_NoShows.Visible = !results;

			lastSearch = TB_Search.Text;
		}

		private bool CheckSearch(Show show)
		{
			if (string.IsNullOrWhiteSpace(TB_Search.Text))
				return true;

			if (show.Name.SearchCheck(TB_Search.Text))
				return true;

			if (show.Name.GetAbbreviation().SearchCheck(TB_Search.Text))
				return true;

			if (show.tMDbData.Genres.Any(x => TB_Search.Text.GetWords().Any(y => x.Name.SearchCheck(y))))
				return true;

			if (show.Episodes.Any(x => (x.TMDbData.AirDate?.Year ?? int.MinValue) == TB_Search.Text.SmartParse()))
				return true;

			return false;
		}

		private bool searchOpened = false;
		private AnimationHandler searchAnimation;
		private void PB_Search_Click(object sender, EventArgs e)
		{
			searchAnimation?.Dispose();

			if (!searchOpened)
			{
				searchOpened = TB_Search.Enabled = true;
				TB_Search.Text = string.Empty;
				TB_Search.Focus();
				PB_Search.Color(FormDesign.Design.ActiveColor);
				SlickTip.SetTo(PB_Search, string.Empty);

				(searchAnimation = new AnimationHandler(TB_Search, new Size(350, 21))).StartAnimation();
			}
			else
			{
				searchOpened = TB_Search.Enabled = false;
				TB_Search.Text = string.Empty;
				PB_Search.Color(FormDesign.Design.IconColor);
				SlickTip.SetTo(PB_Search, "Click to Search");

				(searchAnimation = new AnimationHandler(TB_Search, new Size(0, 21))).StartAnimation();
			}
		}

		public override bool KeyPressed(char keyChar)
		{
			if (!TB_Search.Focused && char.IsLetterOrDigit(keyChar))
			{
				if (!searchOpened)
					PB_Search_Click(null, null);
				else
					TB_Search.Focus();

				TB_Search.Text += keyChar.ToString().ToLower();
				SendKeys.Send("{HOME}");
				SendKeys.Send("{END}");
			}

			return false;
		}

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (searchOpened && TB_Search.Focused && keyData == Keys.Escape)
			{
				PB_Search_Click(null, null);
				return true;
			}

			return base.KeyPressed(ref msg, keyData);
		}
	}
}
