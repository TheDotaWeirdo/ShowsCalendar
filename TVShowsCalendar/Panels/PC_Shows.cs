using Extensions;
using ShowsCalendar.Classes;
using SlickControls.Controls;
using SlickControls.Enums;
using SlickControls.Forms;
using SlickControls.Panels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar.Panels
{
    public partial class PC_Shows : PanelContent
    {
        private readonly Dictionary<Show, ShowTile> Tiles = new Dictionary<Show, ShowTile>();
        private string lastSearch;

        public PC_Shows()
        {
            InitializeComponent();

            verticalScroll.SizeSource = () => P_Tabs.Controls.Cast<Control>().Sum(c => c.If(x => x.Visible, x => x.Height, 0));

            switch (Data.Options.ShowSorting)
            {
                case MediaSortOptions.Year:
                case MediaSortOptions.Name:
                    P_Tabs.Controls.Clear();
                    break;
                default:
                    break;
            }

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

            if (Data.Options.ShowSorting == MediaSortOptions.Default)
            {
                P_Tabs.SuspendDrawing();
                SP_Airing.BringToFront();
                SP_Upcoming.BringToFront();
                SP_Returning.BringToFront();
                SP_Ended.BringToFront();
                P_Tabs.ResumeDrawing();
            }
        }

        private void ShowManager_ShowRemoved(Show show) => this.TryInvoke(() =>
                                                           {
                                                               SetLoading();
                                                               TLP_NoShows.Visible = ShowManager.Shows.Count == 0;
                                                           });

        private void ShowManager_ShowLoaded(Show show) => this.TryInvoke(() =>
                                                          {
                                                              TLP_NoShows.Visible = false;
                                                              AddShow(show);
                                                              SetLoading();
                                                          });
        private void ShowManager_ShowAdded(Show show) => this.TryInvoke(() =>
                                                         {
                                                             SetLoading();
                                                             TLP_NoShows.Visible = false;
                                                         });

        private void AddShow(Show show, ShowTile tile = null)
        {
            if (show.TMDbData == null)
                return;

            var panel = SP_Returning;

            switch (Data.Options.ShowSorting)
            {
                case MediaSortOptions.Default:
                    if (show.TMDbData.Status == "Ended" || show.TMDbData.Status == "Canceled")
                        panel = SP_Ended;
                    else if (((!show.LastEpisode?.Empty ?? false) && show.LastEpisode.TMDbData.AirDate >= DateTime.Today.AddDays(-7))
                        || ((!show.NextEpisode?.Empty ?? false) && show.NextEpisode.TMDbData.AirDate <= DateTime.Today.AddDays(7)))
                    {
                        panel = SP_Airing;
                    }
                    else if ((!show.NextEpisode?.Empty ?? false) && show.NextEpisode.AirState == AirStateEnum.ToBeAired)
                        panel = SP_Upcoming;
                    break;

                case MediaSortOptions.Year:
                    var year = show.TMDbData.LastAirDate?.Year.ToString() ?? "Unknown";
                    panel = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstThat(x => x.Text == year) ?? createSection(year, Properties.Resources.Big_Calendar);
                    break;
                case MediaSortOptions.Name:
                    var name = show.TMDbData.Name.Substring(0, 1).ToUpper();
                    panel = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstThat(x => x.Text == name) ?? createSection(name, Properties.Resources.Big_Label);
                    break;
                case MediaSortOptions.Genre:
                    var genre = show.TMDbData.Genres.FirstOrDefault()?.Name.Replace("&", "&&") ?? "Unknown";
                    panel = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstThat(x => x.Text == genre) ?? createSection(genre, Properties.Resources.Big_Genre);
                    break;
                default:
                    break;
            }

            if (tile == null)
            {
                foreach (var item in P_Tabs.Controls.ThatAre<SlickSectionPanel>())
                    item.Content.Controls.Clear(true, x => (x is ShowTile st) && st.LinkedShow == show);

                tile = new ShowTile(show);

                Tiles.TryAdd(show, tile);

                if (CheckSearch(show))
                    panel.Content.Controls.Add(tile);
            }
            else
            {
                panel.Content.Controls.Add(tile);
            }
        }

        private SlickSectionPanel createSection(string text, Image icon)
        {
            var ssp = new SlickSectionPanel()
            {
                Text = text,
                AutoHide = true,
                Dock = DockStyle.Top,
                AutoSize = true,
                Icon = icon
            };

            P_Tabs.Controls.Add(ssp);

            switch (Data.Options.ShowSorting)
            {
                case MediaSortOptions.Year:
                    P_Tabs.OrderBy(x => x.Text);
                    break;
                case MediaSortOptions.Name:
                    P_Tabs.OrderByDescending(x => x.Text);
                    break;
                case MediaSortOptions.Genre:
                    P_Tabs.OrderBy(x => (x as SlickSectionPanel).Content.Controls.Count);
                    break;
                default:
                    break;
            }

            if (Data.Options.ShowSorting == MediaSortOptions.Year)
            {
                var first = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstThat(x => x.Text == DateTime.Now.Year.ToString());
                if (first != null)
                    first.Active = true;
            }

            return ssp;
        }

        protected override void DesignChanged(FormDesign design)
        {
            base.DesignChanged(design);

            L_NoShowsInfo.ForeColor = design.InfoColor;
            L_NoShows.ForeColor = design.LabelColor;
            P_TopSpacer.BackColor = design.AccentColor;

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
                {
                    item.Parent?.Controls.Remove(item);
                }
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

            if (TB_Search.Text.ToLower().GetWords().Contains("unwatched") && show.Episodes.Any(x => x.AirState == AirStateEnum.Aired && !x.Watched))
                return true;

            if (TB_Search.Text.ToLower().GetWords().Contains("watched") && !show.Episodes.Any(x => x.AirState == AirStateEnum.Aired && !x.Watched))
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
                TB_Search.ReadOnly = !(searchOpened = true);
                TB_Search.Text = string.Empty;
                TB_Search.Focus();
                PB_Search.Color(FormDesign.Design.ActiveColor);
                SlickTip.SetTo(PB_Search, string.Empty);

                (searchAnimation = new AnimationHandler(TB_Search, new Size(350, TB_Search.Height)) { IgnoreHeight = true, Lazy = true }).StartAnimation();
            }
            else
            {
                TB_Search.ReadOnly = !(searchOpened = false);
                TB_Search.Enabled = !(TB_Search.Enabled = false);
                TB_Search.Text = string.Empty;
                PB_Search.Color(FormDesign.Design.IconColor);
                SlickTip.SetTo(PB_Search, "Click to Search");

                (searchAnimation = new AnimationHandler(TB_Search, new Size(0, TB_Search.Height)) { IgnoreHeight = true, Lazy = true }).StartAnimation();
            }
        }

        public override bool KeyPressed(char keyChar)
        {
            if (char.IsLetterOrDigit(keyChar) && (!searchOpened || !TB_Search.Focused))
            {
                var set = !TB_Search.Focused;

                if (!searchOpened)
                    PB_Search_Click(null, null);

                if (set)
                {
                    TB_Search.Focus();
                    TB_Search.Text += keyChar.ToString().ToLower();
                }

                SendKeys.Send("{HOME}");
                SendKeys.Send("{END}");

                return true;
            }

            return false;
        }

        public override bool KeyPressed(ref Message msg, Keys keyData)
        {
            if (searchOpened && keyData == Keys.Escape)
            {
                PB_Search_Click(null, null);
                return true;
            }

            return base.KeyPressed(ref msg, keyData);
        }

        private void verticalScroll_Scroll(object sender, ScrollEventArgs e) => P_TopSpacer.Visible = e.NewValue > 0;
    }
}
