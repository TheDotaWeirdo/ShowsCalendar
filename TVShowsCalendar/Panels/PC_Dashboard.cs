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
using ShowsCalendar.Handlers;
using ShowsCalendar.Controls;
using ShowsCalendar.Classes;
using SlickControls.Classes;

namespace ShowsCalendar.Panels
{
	public partial class PC_Dashboard : PanelContent
	{
		public PC_Dashboard()
		{
			InitializeComponent();

			verticalScroll.SizeSource = () => P_Tabs.Controls.Cast<Control>().Sum(c => c.If(x => x.Visible, x => x.Height, 0));

			ManageShowChange(null);
			ManageMovieChange(null);

			foreach (var item in ShowManager.Shows)
				ManageShow(item);

			foreach (var item in MovieManager.Movies)
				ManageMovie(item);

			if (ShowManager.Shows.Any(x => !x.Loaded) || MovieManager.Movies.Any(x => !x.Loaded))
				StartLoader();

			LocalShowHandler.FolderChanged += ManageShowChange;
			LocalMovieHandler.FolderChanged += ManageMovieChange;
			LocalShowHandler.EpisodeWatchChanged += ManageShowChange;
			LocalMovieHandler.MovieWatchChanged += ManageMovieChange;
			ShowManager.ShowLoaded += ManageShow;
			MovieManager.MovieLoaded += ManageMovie;
			ShowManager.ShowRemoved += ManageShow;
			MovieManager.MovieRemoved += ManageMovie;

			Disposed += (s, e) =>
			{
				LocalShowHandler.FolderChanged -= ManageShowChange;
				LocalMovieHandler.FolderChanged -= ManageMovieChange;
				LocalShowHandler.EpisodeWatchChanged -= ManageShowChange;
				LocalMovieHandler.MovieWatchChanged -= ManageMovieChange;
				ShowManager.ShowLoaded -= ManageShow;
				MovieManager.MovieLoaded -= ManageMovie;
				ShowManager.ShowRemoved -= ManageShow;
				MovieManager.MovieRemoved -= ManageMovie;
			};
		}

		private void ManageMovieChange(Movie movie)
		{
			this.TryInvoke(() =>
			{
				var controls = new List<WatchControl>();
				LocalMovieHandler.LoadLibrary(out var onDeck, movie);

				foreach (var item in onDeck)
				{
					var current = SP_OnDeck.Content.Controls.ThatAre<WatchControl>().FirstThat(x => x.Movie == item);

					if (current == null)
					{
						var c = new WatchControl(item);

						controls.Add(c);
						SP_OnDeck.Content.Controls.Add(c);
					}
					else
					{
						current.Refresh();
						controls.Add(current);
					}
				}

				foreach (var item in SP_OnDeck.Content.Controls.ThatAre<WatchControl>().Where(x => x.Movie != null && !controls.Contains(x)))
				{
					if (movie == null || item.Movie == movie)
						item.Dispose();
				}

				P_Tabs.SuspendDrawing();

				SP_OnDeck.BringToFront();
				SP_ContinueWatching.BringToFront();
				SP_RecentEps.BringToFront();
				SP_RecentMovies.BringToFront();
				SP_UpcomingEps.BringToFront();
				SP_UpcomingMovies.BringToFront();

				P_Tabs.ResumeDrawing();
			});
		}

		private void ManageShowChange(Show show)
		{
			this.TryInvoke(() =>
			{
				var controls = new List<WatchControl>();
				LocalShowHandler.LoadLibrary(out var onDeck, show);

				foreach (var item in onDeck)
				{
					var current = SP_OnDeck.Content.Controls.ThatAre<WatchControl>().FirstThat(x => x.Episode == item);

					if (current == null)
					{
						var c = new WatchControl(item);

						controls.Add(c);
						SP_OnDeck.Content.Controls.Add(c);
					}
					else
					{
						current.Refresh();
						controls.Add(current);
					}
				}

				LocalShowHandler.LoadLibrary(out onDeck, show, true);

				foreach (var item in onDeck)
				{
					var current = SP_ContinueWatching.Content.Controls.ThatAre<WatchControl>().FirstThat(x => x.Episode == item);

					if (current == null)
					{
						var c = new WatchControl(item);

						controls.Add(c);
						SP_ContinueWatching.Content.Controls.Add(c);
					}
					else
					{
						current.Refresh();
						controls.Add(current);
					}
				}

				foreach (var item in SP_OnDeck.Content.Controls.ThatAre<WatchControl>().Where(x => x.Episode != null && !controls.Contains(x)))
				{
					if (show == null || item.Episode.Show == show)
						item.Dispose();
				}

				foreach (var item in SP_ContinueWatching.Content.Controls.ThatAre<WatchControl>().Where(x => x.Episode != null && !controls.Contains(x)))
				{
					if (show == null || item.Episode.Show == show)
						item.Dispose();
				}

				P_Tabs.SuspendDrawing();

				SP_OnDeck.BringToFront();
				SP_ContinueWatching.BringToFront();
				SP_RecentEps.BringToFront();
				SP_RecentMovies.BringToFront();
				SP_UpcomingEps.BringToFront();
				SP_UpcomingMovies.BringToFront();

				P_Tabs.ResumeDrawing();
			});
		}

		private void ManageMovie(Movie item)
		{
			if (ShowManager.Shows.Any(x => !x.Loaded) || MovieManager.Movies.Any(x => !x.Loaded))
				StartLoader();
			else
				StopLoader();

			this.TryInvoke(() =>
			{
				var addedNew = false;
				var addedOld = false;

				if ((item.TMDbData?.ReleaseDate ?? DateTime.MaxValue).If(x => x < DateTime.Today && x > DateTime.Today.AddMonths(-1), true, false))
				{
					var current = SP_RecentMovies.Content.Controls.ThatAre<MovieTile>().FirstThat(x => x.LinkedMovie == item);

					if (current == null)
						SP_RecentMovies.Content.Controls.Add(new MovieTile(item, true));
					else
						current.Refresh();

					addedNew = true;
				}
				else if ((item.TMDbData?.ReleaseDate ?? DateTime.MinValue).If(x => x > DateTime.Today && x < DateTime.Today.AddMonths(12), true, false))
				{
					var current = SP_UpcomingMovies.Content.Controls.ThatAre<MovieTile>().FirstThat(x => x.LinkedMovie == item);

					if (current == null)
						SP_UpcomingMovies.Content.Controls.Add(new MovieTile(item, true));
					else
						current.Refresh();

					addedOld = true;
				}

				if (!addedNew)
					SP_RecentMovies.Content.Controls.ThatAre<MovieTile>().Where(x => x.LinkedMovie == item).Foreach(x => x.Dispose());

				if (!addedOld)
					SP_UpcomingMovies.Content.Controls.ThatAre<MovieTile>().Where(x => x.LinkedMovie == item).Foreach(x => x.Dispose());

				P_Tabs.SuspendDrawing();

				SP_OnDeck.BringToFront();
				SP_ContinueWatching.BringToFront();
				SP_RecentEps.BringToFront();
				SP_RecentMovies.BringToFront();
				SP_UpcomingEps.BringToFront();
				SP_UpcomingMovies.BringToFront();

				P_Tabs.ResumeDrawing();
			});
		}

		private void ManageShow(Show show)
		{
			if (ShowManager.Shows.Any(x => !x.Loaded) || MovieManager.Movies.Any(x => !x.Loaded))
				StartLoader();
			else
				StopLoader();

			this.TryInvoke(() =>
			{
				var addedNew = false;
				var addedOld = false;

				if ((!show.LastEpisode?.Empty ?? false) && show.LastEpisode.TMDbData.AirDate > DateTime.Today.AddDays(-7))
				{
					var current = SP_RecentEps.Content.Controls.ThatAre<EpisodeTile>().FirstThat(x => x.Episode.Show == show);

					if (current == null)
						SP_RecentEps.Content.Controls.Add(new EpisodeTile(show.LastEpisode));
					else
					{
						current.Episode = show.LastEpisode;
						current.ReloadData();
						current.Refresh();
					}

					addedNew = true;
				}

				if ((!show.NextEpisode?.Empty ?? false) && show.NextEpisode.TMDbData.AirDate < DateTime.Today.AddDays(30))
				{
					var current = SP_UpcomingEps.Content.Controls.ThatAre<EpisodeTile>().FirstThat(x => x.Episode.Show == show);

					if (current == null)
						SP_UpcomingEps.Content.Controls.Add(new EpisodeTile(show.NextEpisode));
					else
					{
						current.Episode = show.NextEpisode;
						current.ReloadData();
						current.Refresh();
					}

					addedOld = true;
				}

				if (!addedNew)
					SP_RecentEps.Content.Controls.ThatAre<EpisodeTile>().Where(x => x.Episode.Show == show).Foreach(x => x.Dispose());

				if (!addedOld)
					SP_UpcomingEps.Content.Controls.ThatAre<EpisodeTile>().Where(x => x.Episode.Show == show).Foreach(x => x.Dispose());

				P_Tabs.SuspendDrawing();

				SP_OnDeck.BringToFront();
				SP_ContinueWatching.BringToFront();
				SP_RecentEps.BringToFront();
				SP_RecentMovies.BringToFront();
				SP_UpcomingEps.BringToFront();
				SP_UpcomingMovies.BringToFront();

				P_Tabs.ResumeDrawing();
			});
		}

		private void PC_Dashboard_Resize(object sender, EventArgs e)
		{
			P_Tabs.MaximumSize = new Size(panel1.Width, 9999);
			P_Tabs.MinimumSize = new Size(panel1.Width, 0);
		}
	}
}
