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
    public partial class PC_Movies : PanelContent
    {
        private readonly Dictionary<Movie, MovieTile> Tiles = new Dictionary<Movie, MovieTile>();
        private string lastSearch;

        public PC_Movies()
        {
            InitializeComponent();

            verticalScroll.SizeSource = () => P_Tabs.Controls.Cast<Control>().Sum(c => c.If(x => x.Visible, x => x.Height, 0));

            switch (Data.Options.MovieSorting)
            {
                case MediaSortOptions.Year:
                case MediaSortOptions.Name:
                    P_Tabs.Controls.Clear();
                    break;
                default:
                    break;
            }

            foreach (var movie in MovieManager.Movies.Where(x => x.Loaded))
                AddMovie(movie);

            TLP_NoMovies.Visible = ShowManager.Shows.Count == 0;
            SetLoading();

            SlickTip.SetTo(PB_Search, "Click to Search");

            MovieManager.MovieAdded += MovieManager_MovieAdded;
            MovieManager.MovieLoaded += MovieManager_MovieLoaded;
            MovieManager.MovieRemoved += MovieManager_MovieRemoved;

            Disposed += (s, e) =>
            {
                MovieManager.MovieAdded -= MovieManager_MovieAdded;
                MovieManager.MovieLoaded -= MovieManager_MovieLoaded;
                MovieManager.MovieRemoved -= MovieManager_MovieRemoved;
            };
        }

        private void SetLoading()
        {
            if (MovieManager.Movies.Any(x => !x.Loaded))
                StartLoader();
            else
                StopLoader();

            if (Data.Options.MovieSorting == MediaSortOptions.Default)
            {
                P_Tabs.SuspendDrawing();
                SP_Recent.BringToFront();
                SP_Upcoming.BringToFront();
                SP_Earlier.BringToFront();
                SP_Oldies.BringToFront();
                P_Tabs.ResumeDrawing();
            }
        }

        private void MovieManager_MovieRemoved(Movie movie) => this.TryInvoke(() =>
                                                               {
                                                                   SetLoading();
                                                                   TLP_NoMovies.Visible = ShowManager.Shows.Count == 0;
                                                               });

        private void MovieManager_MovieLoaded(Movie movie) => this.TryInvoke(() =>
                                                              {
                                                                  SetLoading();
                                                                  TLP_NoMovies.Visible = false;
                                                                  AddMovie(movie);
                                                              });
        private void MovieManager_MovieAdded(Movie movie) => this.TryInvoke(() =>
                                                             {
                                                                 SetLoading();
                                                                 TLP_NoMovies.Visible = false;
                                                             });

        private void AddMovie(Movie movie, MovieTile tile = null)
        {
            if (movie.TMDbData == null)
                return;

            var panel = SP_Upcoming;

            switch (Data.Options.MovieSorting)
            {
                case MediaSortOptions.Default:
                    if (movie.tMDbData.ReleaseDate == null || movie.tMDbData.ReleaseDate < DateTime.Today.AddMonths(-5))
                        panel = SP_Oldies;
                    else if (movie.tMDbData.ReleaseDate < DateTime.Today.AddMonths(-1))
                        panel = SP_Earlier;
                    else if (movie.tMDbData.ReleaseDate < DateTime.Today)
                        panel = SP_Recent;
                    break;

                case MediaSortOptions.Year:
                    var year = movie.TMDbData.ReleaseDate?.Year.ToString() ?? "Unknown";
                    panel = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstThat(x => x.Text == year) ?? createSection(year, Properties.Resources.Big_Calendar);
                    break;
                case MediaSortOptions.Name:
                    var name = movie.TMDbData.Title.Substring(0, 1).ToUpper(); ;
                    panel = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstThat(x => x.Text == name) ?? createSection(name, Properties.Resources.Big_Label);
                    break;
                case MediaSortOptions.Genre:
                    var genre = movie.TMDbData.Genres.FirstOrDefault()?.Name.Replace("&", "&&") ?? "Unknown";
                    panel = P_Tabs.Controls.OfType<SlickSectionPanel>().FirstThat(x => x.Text == genre) ?? createSection(genre, Properties.Resources.Big_Genre);
                    break;
                default:
                    break;
            }

            if (tile == null)
            {
                foreach (var item in P_Tabs.Controls.ThatAre<SlickSectionPanel>())
                    item.Content.Controls.Clear(true, x => (x is MovieTile st) && st.LinkedMovie == movie);

                tile = new MovieTile(movie);

                Tiles.TryAdd(movie, tile);

                if (CheckSearch(movie))
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

            switch (Data.Options.MovieSorting)
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

            if (Data.Options.MovieSorting == MediaSortOptions.Name)
                P_Tabs.OrderByDescending(x => x.Text);
            else
                P_Tabs.OrderBy(x => x.Text);

            if (Data.Options.MovieSorting == MediaSortOptions.Year)
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

            L_NoMoviesInfo.ForeColor = design.InfoColor;
            L_NoMovies.ForeColor = design.LabelColor;
            P_TopSpacer.BackColor = design.AccentColor;

            PB_Search.Color(design.IconColor);
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
                Form.PushPanel(null, new PC_AddMedia(true));
            }
            else
            {
                ShowPrompt("You can not add Movies without Internet connection.\n\nCheck your connectivity then try again.",
                    "No Connection",
                     PromptButtons.OK,
                     PromptIcons.Hand);
            }
        }

        private void B_Discover_Click(object sender, EventArgs e)
        {
            if (ConnectionHandler.State == Extensions.ConnectionState.Connected)
            {
                Form.PushPanel(null, new PC_Discover(true));
            }
            else
            {
                ShowPrompt("You can not discover Movies without Internet connection.\n\nCheck your connectivity then try again.",
                    "No Connection",
                     PromptButtons.OK,
                     PromptIcons.Hand);
            }
        }

        private void TB_Search_TextChanged(object sender, EventArgs e)
        {
            if (lastSearch == TB_Search.Text || Tiles.Count == 0)
                return;

            var results = false;

            P_Tabs.SuspendDrawing();
            foreach (var item in Tiles.Values)
            {
                if (CheckSearch(item.LinkedMovie))
                {
                    AddMovie(item.LinkedMovie, item);
                    results = true;
                }
                else
                {
                    item.Parent?.Controls.Remove(item);
                }
            }
            P_Tabs.ResumeDrawing();

            TLP_NoMovies.Visible = !results;

            lastSearch = TB_Search.Text;
        }

        private bool CheckSearch(Movie movie)
        {
            if (string.IsNullOrWhiteSpace(TB_Search.Text))
                return true;

            if (movie.Title.SearchCheck(TB_Search.Text))
                return true;

            if (movie.Title.GetAbbreviation().SearchCheck(TB_Search.Text))
                return true;

            if (movie.tMDbData.Genres.Any(x => TB_Search.Text.GetWords().Any(y => x.Name.SearchCheck(y))))
                return true;

            if (TB_Search.Text.Contains((movie.tMDbData.ReleaseDate?.Year ?? int.MaxValue).ToString()))
                return true;

            if (TB_Search.Text.ToLower().GetWords().Contains("unwatched") && movie.AirState == AirStateEnum.Aired && !movie.Watched)
                return true;

            if (TB_Search.Text.ToLower().GetWords().Contains("watched") && !(movie.AirState == AirStateEnum.Aired && !movie.Watched))
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
                searchOpened = TB_Search.ReadOnly = true;
                TB_Search.Text = string.Empty;
                TB_Search.Focus();
                PB_Search.Color(FormDesign.Design.ActiveColor);
                SlickTip.SetTo(PB_Search, string.Empty);

                (searchAnimation = new AnimationHandler(TB_Search, new Size(350, TB_Search.Height)) { IgnoreHeight = true, Lazy = true }).StartAnimation();
            }
            else
            {
                searchOpened = TB_Search.ReadOnly = false;
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
