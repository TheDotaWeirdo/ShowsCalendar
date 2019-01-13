using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SlickControls.Panels;
using Extensions;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Classes;
using TVShowsCalendar.UserControls;

namespace TVShowsCalendar.Panels
{
	public partial class PC_Library : PanelContent
	{
		public PC_Library()
		{
			InitializeComponent();

			verticalScroll.LinkedControl = P_Tabs;
			verticalScroll.SizeSource = () => P_Tabs.Controls.Cast<Control>().Sum(c => c.If(x => x.Visible, x => x.Height, 0));

			LoadShowLibrary();
			LoadMovieLibrary();

			LocalShowHandler.FolderChanged += LocalShowHandler_FolderChanged;
			LocalShowHandler.EpisodeWatchChanged += LocalShowHandler_FolderChanged;

			LocalMovieHandler.FolderChanged += LocalMovieHandler_FolderChanged;
			LocalMovieHandler.MovieWatchChanged += LocalMovieHandler_FolderChanged;

			Disposed += (s, e) =>
			{
				LocalShowHandler.FolderChanged -= LocalShowHandler_FolderChanged;
				LocalShowHandler.EpisodeWatchChanged -= LocalShowHandler_FolderChanged;

				LocalMovieHandler.FolderChanged -= LocalMovieHandler_FolderChanged;
				LocalMovieHandler.MovieWatchChanged -= LocalMovieHandler_FolderChanged;
			};
		}

		private void LocalMovieHandler_FolderChanged(Movie s)
			=> new Action(() => LoadMovieLibrary(s)).RunInBackground();

		private void LocalShowHandler_FolderChanged(Show s)
			=> new Action(() => LoadShowLibrary(s)).RunInBackground();

		private void LoadShowLibrary(Show show = null)
		{
			this.TryInvoke(() =>
			{
				var shows = show.IfNull(ShowManager.Shows, new List<Show> { show });
				SP_Shows.Content.SuspendDrawing();

				SP_Shows.Content.Controls.Clear(true, x => show == null || (x as WatchControl).Episode.Show == show);

				foreach (var item in shows)
				{
					var ep = item.GetCurrentWatchEpisode();
					if (ep != null)
						SP_Shows.Content.Controls.Add(new WatchControl(ep, false));
				}
				SP_Shows.Content.OrderByDescending(x => (x as WatchControl).Episode.GetDateOrder());

				TLP_NoShows.Visible = !P_Tabs.Controls.ThatAre<SectionPanel>().Any(x => x.Content.Controls.Count > 0);

				SP_Shows.Content.ResumeDrawing();
			});
		}

		private void LoadMovieLibrary(Movie movie = null)
		{
			this.TryInvoke(() =>
			{
				var movies = movie.IfNull(MovieManager.Movies, new List<Movie> { movie });
				SP_Movies.Content.SuspendDrawing();

				SP_Movies.Content.Controls.Clear(true, x => movie == null || (x as WatchControl).Movie == movie);

				foreach (var item in movies.Where(x => (x.VidFile?.Exists ?? false)))
					SP_Movies.Content.Controls.Add(new WatchControl(item, false));
				SP_Movies.Content.OrderByDescending(x => (x as WatchControl).Movie.GetDateOrder());

				TLP_NoShows.Visible = !P_Tabs.Controls.ThatAre<SectionPanel>().Any(x => x.Content.Controls.Count > 0);

				SP_Movies.Content.ResumeDrawing();

				StopLoader();
			});
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_NoShowsInfo.ForeColor = design.InfoColor;

			L_NoShows.ForeColor = design.LabelColor;
		}

		DisableIdentifier RefreshIdentifier = new DisableIdentifier();
		private void B_Refresh_Click(object sender, EventArgs e)
		{
			if (!RefreshIdentifier.Disable(2000))
				new Action(() =>
				{
					StartLoader();
					LocalShowHandler.LoadFiles();
					LocalMovieHandler.ReloadMovieFiles();
				}).RunInBackground();
		}

		private void PC_Watch_Resize(object sender, EventArgs e)
		{
			P_Tabs.MaximumSize = new Size(Width - 12, 9999);
			P_Tabs.MinimumSize = new Size(Width - 12, 0);
		}
	}
}
