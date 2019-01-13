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
	public partial class PC_Movies : PanelContent
	{
		private Dictionary<Movie, MovieTile> Tiles = new Dictionary<Movie, MovieTile>();
		private string lastSearch;

		public PC_Movies()
		{
			InitializeComponent();

			verticalScroll.SizeSource = () => P_Tabs.Controls.Cast<Control>().Sum(c => c.If(x => x.Visible, x => x.Height, 0));

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

			P_Tabs.SuspendDrawing();
			SP_Recent.BringToFront();
			SP_Upcoming.BringToFront();
			SP_Earlier.BringToFront();
			SP_Oldies.BringToFront();
			P_Tabs.ResumeDrawing();
		}

		private void MovieManager_MovieRemoved(Movie movie)
		{
			this.TryInvoke(() =>
			{
				SetLoading();
				TLP_NoMovies.Visible = ShowManager.Shows.Count == 0;
			});
		}

		private void MovieManager_MovieLoaded(Movie movie)
		{
			this.TryInvoke(() =>
			{
				SetLoading();
				TLP_NoMovies.Visible = false;
				AddMovie(movie);
			});
		}
		private void MovieManager_MovieAdded(Movie movie)
		{
			this.TryInvoke(() =>
			{
				SetLoading();
				TLP_NoMovies.Visible = false;
			});
		}

		private void AddMovie(Movie movie, MovieTile tile = null)
		{
			if (movie.TMDbData == null)
				return;

			var panel = SP_Upcoming;

			if (movie.tMDbData.ReleaseDate == null || movie.tMDbData.ReleaseDate < DateTime.Today.AddMonths(-5))
				panel = SP_Oldies;
			else if (movie.tMDbData.ReleaseDate < DateTime.Today.AddMonths(-1))
				panel = SP_Earlier;
			else if (movie.tMDbData.ReleaseDate < DateTime.Today)
				panel = SP_Recent;

			if (tile == null)
			{
				foreach (var item in P_Tabs.Controls.ThatAre<SectionPanel>())
					item.Content.Controls.Clear(true, x => (x is MovieTile st) && st.LinkedMovie == movie);

				tile = new MovieTile(movie);

				Tiles.TryAdd(movie, tile);

				if (CheckSearch(movie))
					panel.Content.Controls.Add(tile);
			}
			else
				panel.Content.Controls.Add(tile);
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_NoMoviesInfo.ForeColor = design.InfoColor;

			L_NoMovies.ForeColor = design.LabelColor;

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
					item.Parent?.Controls.Remove(item);
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
			if(searchOpened && TB_Search.Focused && keyData == Keys.Escape)
			{
				PB_Search_Click(null, null);
				return true;
			}

			return base.KeyPressed(ref msg, keyData);
		}
	}
}
