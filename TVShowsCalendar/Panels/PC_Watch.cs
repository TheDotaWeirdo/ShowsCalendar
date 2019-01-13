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
	public partial class PC_Watch : PanelContent
	{
		public PC_Watch()
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
			LocalShowHandler.LoadLibrary(out var onDeck, out var continueWatching, out var startWatching, out var lastWatched, show);

			if (InvokeRequired)
				Invoke(new Action(() => GenerateEpisodes(onDeck, continueWatching, startWatching, lastWatched, show)));
			else
				GenerateEpisodes(onDeck, continueWatching, startWatching, lastWatched, show);
		}

		private void LoadMovieLibrary(Movie movie = null)
		{
			LocalMovieHandler.LoadLibrary(out var onDeck, out var continueWatching, out var startWatching, movie);

			if (InvokeRequired)
				Invoke(new Action(() => GenerateMovies(onDeck, continueWatching, startWatching, movie)));
			else
				GenerateMovies(onDeck, continueWatching, startWatching, movie);
		}

		private void GenerateMovies(List<Movie> onDeck, List<Movie> continueWatching, List<Movie> startWatching, Movie movie)
		{
			P_Tabs.SuspendDrawing();

			SP_OnDeck.Content.Controls.Clear(true, x => (x as WatchControl).Movie != null && (movie == null || (x as WatchControl).Movie == movie));
			SP_ContinueMovies.Content.Controls.Clear(true, x => movie == null || (x as WatchControl).Movie == movie);
			SP_StartMovies.Content.Controls.Clear(true, x => movie == null || (x as WatchControl).Movie == movie);

			foreach (var item in onDeck)
				SP_OnDeck.Content.Controls.Add(new WatchControl(item));
			SP_OnDeck.Content.OrderByDescending(c => (c as WatchControl).If(x => x.Episode != null, x => x.Episode.GetDateOrder(), x => x.Movie.WatchDate));

			foreach (var item in continueWatching)
				SP_ContinueMovies.Content.Controls.Add(new WatchControl(item, false));
			SP_ContinueMovies.Content.OrderByDescending(x => (x as WatchControl).Movie.WatchDate);

			foreach (var item in startWatching)
				SP_StartMovies.Content.Controls.Add(new WatchControl(item, false));
			SP_StartMovies.Content.OrderByDescending(x => (x as WatchControl).Movie.WatchDate);

			P_Tabs.ResumeDrawing();

			TLP_NoShows.Visible = !P_Tabs.Controls.ThatAre<SectionPanel>().Any(x => x.Content.Controls.Count > 0);

			StopLoader();
		}

		private void GenerateEpisodes(List<Episode> onDeck, List<Episode> continueWatching, List<Episode> startWatching, List<Episode> rewatch, Show refShow)
		{
			P_Tabs.SuspendDrawing();

			SP_OnDeck.Content.Controls.Clear(true, x => (x as WatchControl).Episode != null && (refShow == null || (x as WatchControl).Episode.Show == refShow));
			SP_ContinueEps.Content.Controls.Clear(true, x => refShow == null || (x as WatchControl).Episode.Show == refShow);
			SP_StartShows.Content.Controls.Clear(true, x => refShow == null || (x as WatchControl).Episode.Show == refShow);
			SP_RewatchEps.Content.Controls.Clear(true, x => refShow == null || (x as WatchControl).Episode.Show == refShow);

			foreach (var item in onDeck)
				SP_OnDeck.Content.Controls.Add(new WatchControl(item));
			SP_OnDeck.Content.OrderByDescending(c => (c as WatchControl).If(x => x.Episode != null, x => x.Episode.GetDateOrder(), x => x.Movie.WatchDate));

			foreach (var item in continueWatching)
				SP_ContinueEps.Content.Controls.Add(new WatchControl(item, false));
			SP_ContinueEps.Content.OrderByDescending(x => (x as WatchControl).Episode.GetDateOrder());

			foreach (var item in startWatching)
				SP_StartShows.Content.Controls.Add(new WatchControl(item, false));
			SP_StartShows.Content.OrderByDescending(x => (x as WatchControl).Episode.GetDateOrder());

			foreach (var item in rewatch)
				SP_RewatchEps.Content.Controls.Add(new WatchControl(item, false));
			SP_RewatchEps.Content.OrderByDescending(x => (x as WatchControl).Episode.GetDateOrder());

			TLP_NoShows.Visible = !P_Tabs.Controls.ThatAre<SectionPanel>().Any(x => x.Content.Controls.Count > 0);

			P_Tabs.ResumeDrawing();
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
