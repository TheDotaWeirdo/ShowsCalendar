using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using SlickControls.Panels;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Panels;
using ProjectImages = TVShowsCalendar.Properties.Resources;
using TMDbMovie = TMDbLib.Objects.Movies.Movie;

namespace TVShowsCalendar.Classes
{
#pragma warning disable CS4014

	public class Movie : ISave
	{
		#region Public Events

		public event MovieManager.MovieChangeHandler MovieLoaded;

		public event EventHandler TMDbDataChanged;

		#endregion Public Events

		#region Public Fields

		public DateTime LastCheck { get; set; } = DateTime.MinValue;

		#endregion Public Fields

		#region Private Fields

		private OneWayTask MovieLoadingTask = new OneWayTask();
		private bool watched;
		private long watchTime = -1;
		private int _id;

		#endregion Private Fields

		#region Public Properties

		public ImagesWithId Images { get; internal set; }
		public new string Name => Title;
		public double Progress { get; set; }
		public LightMovie[] SimilarMovies { get; set; }
		internal string Title => (TMDbData?.Title ?? "Movie").RemoveDoubleSpaces();
		public TMDbMovie tMDbData { get; set; } = null;
		public DateTime WatchDate { get; set; } = DateTime.MinValue;

		public bool Watched
		{
			get => watched || WatchTime == 0;
			set => watched = value;
		}

		public long WatchTime
		{
			get => watchTime;
			set
			{
				watchTime = value;
				watched = value == 0 || watched;
			}
		}

		#endregion Public Properties

		#region Internal Properties

		internal List<Cast> Cast { get; set; }
		internal List<Crew> Crew { get; set; }
		internal bool FirstLoad { get; set; } = false;
		internal bool Loaded { get; private set; }

		internal bool Started => WatchTime > 0;
		internal bool Temporary { get; set; }

		internal TMDbMovie TMDbData
		{
			get => tMDbData;
			set
			{
				Loaded = false;

				tMDbData = value;

				if (Id == 0)
					Id = value.Id;

				TMDbDataChanged?.Invoke(this, new EventArgs());

				Cast = TMDbData.Credits?.Cast?.OrderBy(x => x.Order).ToList() ?? new List<Cast>();
				Crew = TMDbData.Credits?.Crew?.OrderBy(x => string.IsNullOrWhiteSpace(x.ProfilePath) ? 0 : 1).ThenBy(x => x.Name).ToList() ?? new List<Crew>();

				SimilarMovies = TMDbData.Similar?.Results?.Convert(LightMovie.Convert).ToArray() ?? new LightMovie[0];

				FinishLoad();
			}
		}

		internal FileInfo VidFile { get; set; }

		#endregion Internal Properties

		#region Public Constructors

		public Movie()
		{
		}

		public Movie(TMDbMovie movie)
			=> TMDbData = movie;

		public Movie(LightMovie searchMovie)
			=> new Action(async () =>
			{
				Loaded = false;
				tMDbData = new TMDbMovie() { Id = searchMovie.Id };
				await CheckForChanges();
			}).RunInBackground(System.Threading.ThreadPriority.Lowest);

		#endregion Public Constructors

		#region Public Methods

		public int Id { get => _id.If(0, TMDbData?.Id ?? 0); set => _id = value; }

		public AirStateEnum AirState
		{
			get
			{
				if (TMDbData?.ReleaseDate == null)
					return AirStateEnum.Unknown;
				if (TMDbData.ReleaseDate <= DateTime.Today)
					return AirStateEnum.Aired;
				return AirStateEnum.ToBeAired;
			}
		}

		public void Delete()
		{
			Delete($"MoviesData/{Name.RegexRemove("[^\\w\\ ]").RegexReplace(" {1,}", "-")}-{TMDbData.Id}.scm");
		}

		public override bool Equals(object obj)
		{
			return obj is Movie movie && movie.TMDbData?.Id == TMDbData?.Id;
		}

		public override int GetHashCode() => TMDbData?.Id ?? 0;

		public override void OnLoad()
		{
			if (TMDbData.Credits != null)
			{
				if (TMDbData.Credits.Cast != null)
					Cast = TMDbData.Credits.Cast.OrderBy(x => x.Order).ToList();

				if (TMDbData.Credits.Crew != null)
					Crew = TMDbData.Credits.Crew;
			}

			if (LastCheck.AddDays(Data.Options.MoviesRefreshDays) >= DateTime.Now)
			{
				Id = tMDbData.Id;

				Loaded = true;
			}
			else
				new Action(() => CheckForChanges()).RunInBackground(5000);
		}

		public void Play(FileInfo epFile = null)
		{
			epFile = epFile ?? VidFile;

			if (!File.Exists(epFile?.FullName ?? string.Empty))
				epFile = LocalMovieHandler.GetFile(this);

			PC_Player pc = null;
			var pushed = false;

			try
			{
				if (Data.Dashboard.CurrentPanel is PC_Player player)
					player.SetMovie(this, epFile);
				else
				{
					pc = new PC_Player(this, epFile);
					pushed = true;
					if (pc.Movie != null)
						Data.Dashboard.PushPanel(PanelItem.Empty, pc);
					else
						pc.Dispose();
				}
			}
			catch
			{
				pc?.Dispose();
				if (pushed)
					Data.Dashboard.PushBack();

				MessagePrompt.Show("Something went wrong while loading your movie.\n\nMake sure it fully downloaded, or try another file.", icon: PromptIcons.Error, form: Data.Dashboard);

				return;
			}

			Data.Dashboard.ShowUp();
		}

		public void Save()
		{
			if (Id.If(0, tMDbData.Id) != 0)
			{
				LastCheck = DateTime.Now;
				Save($"MoviesData/{Name.RegexRemove("[^\\w\\ ]").RegexReplace(" {1,}", "-")}-{TMDbData.Id}.scm", true);
				foreach (var item in Directory.GetFiles(Path.Combine(DocsFolder, "MoviesData"), $"*-{Id}.scm"))
					if (item != Path.Combine(DocsFolder, "MoviesData", $"{Name.RegexRemove("[^\\w\\ ]").RegexReplace(" {1,}", "-")}-{Id.If(0, tMDbData.Id)}.scm"))
						File.Delete(item);
			}
		}

		public void ShowStrip(bool showPage = true)
		{
			FlatToolStrip.Show(Data.Dashboard,
				new FlatStripItem("Play", () => Play(), image: ProjectImages.Tiny_Play, show: VidFile?.Exists ?? false),
				new FlatStripItem("Download", () => Data.Dashboard.PushPanel(null, new PC_Download(this)), image: ProjectImages.Tiny_Download),
				FlatStripItem.Empty,
				new FlatStripItem("Movie Page", () => Data.Dashboard.PushPanel(null, new PC_MoviePage(this)), image: ProjectImages.Tiny_Info, show: showPage),
				new FlatStripItem("Refresh", () => CheckForChanges(false, true, true), image: ProjectImages.Tiny_Refresh),
				FlatStripItem.Empty,
				new FlatStripItem("Mark as Complete", show: Progress > 0 || WatchTime > 0, image: ProjectImages.Tiny_Ok, action: () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					WatchDate = DateTime.Now;
					WatchTime = 0;
					Progress = 0;

					new Action(()=>
					{
						LocalMovieHandler.Refresh(this);
						MovieManager.Save(this);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}),
				new FlatStripItem("Mark as " + (Watched ? "Unwatched" : "Watched"), show: !Temporary, image: ProjectImages.Tiny_Ok, action: () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					WatchDate = DateTime.Now;
					Watched = !Watched;
					if(Watched)
					{
						WatchTime = 0;
						Progress = 0;
					}

					new Action(()=>
					{
						LocalMovieHandler.Refresh(this);
						MovieManager.Save(this);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}),
				FlatStripItem.Empty,
				new FlatStripItem("Delete", () => MovieManager.Remove(this), image: ProjectImages.Tiny_Trash)
			);
		}

		public override string ToString() => Title;

		#endregion Public Methods

		#region Internal Methods

		internal async Task<bool> CheckForChanges(bool wait = true, bool force = false, bool prompt = false)
		{
			if (!wait && ConnectionHandler.State == ConnectionState.Connected)
				return await Reload();

			if (force || LastCheck.AddDays(Data.Options.ShowsRefreshDays) < DateTime.Now)
			{
				if (ConnectionHandler.State == ConnectionState.Connected)
					return await Reload();
				else if (wait || (prompt && DialogResult.Yes == MessagePrompt.Show("You are not connected to the Internet right now.\nDo you wish to Reload when the connection comes back up?", buttons: PromptButtons.YesNo, icon: PromptIcons.Hand)))
					ConnectionHandler.Connected += (c) => Reload();
			}
			else
				Loaded = true;

			return false;
		}

		internal async Task<bool> Reload()
		{
			try
			{
				TMDbData = await Data.TMDbHandler.GetMovie(TMDbData.Id);
				LastCheck = DateTime.Now;

				return true;
			}
			catch { return false; }
		}

		#endregion Internal Methods

		#region Private Methods

		private void FinishLoad()
		{
			MovieManager.Add(this);
			Loaded = true;
			MovieLoaded?.Invoke(this);
			FirstLoad = false;
		}

		#endregion Private Methods
	}

#pragma warning restore CS4014
}