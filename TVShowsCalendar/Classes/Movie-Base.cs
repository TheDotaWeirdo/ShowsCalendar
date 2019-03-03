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
using ShowsCalendar.Handlers;
using ShowsCalendar.Panels;
using ProjectImages = ShowsCalendar.Properties.Resources;
using TMDbMovie = TMDbLib.Objects.Movies.Movie;

namespace ShowsCalendar.Classes
{
#pragma warning disable CS4014

	public partial class Movie : ISave
	{

		public event MovieManager.MovieChangeHandler MovieLoaded;

		public event EventHandler TMDbDataChanged;

		private OneWayTask MovieLoadingTask = new OneWayTask();
		private bool watched;
		private long watchTime = -1;
		private int _id;

		public ImagesWithId Images { get; internal set; }
		public new string Name => Title;
		public double Progress { get; set; }
		public LightMovie[] SimilarMovies { get; set; }
		internal string Title => (TMDbData?.Title ?? base.Name).RemoveDoubleSpaces();
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

		internal List<Cast> Cast { get; set; }
		internal List<Crew> Crew { get; set; }
		internal bool FirstLoad { get; set; } = false;
		internal bool Loaded { get; private set; }

		internal bool Started => WatchTime > 0;

		internal TMDbMovie TMDbData
		{
			get => tMDbData;
			set
			{
				tMDbData = value;

				Cast = value?.Credits?.Cast?.OrderBy(x => x.Order).ToList() ?? new List<Cast>();
				Crew = value?.Credits?.Crew?.OrderBy(x => !string.IsNullOrWhiteSpace(x.ProfilePath) ? 0 : 1).ThenBy(x => x.Name).ToList() ?? new List<Crew>();
				SimilarMovies = value?.Similar?.Results?.Convert(LightMovie.Convert).ToArray() ?? new LightMovie[0];

				TMDbDataChanged?.Invoke(this, new EventArgs());
			}
		}

		internal List<FileInfo> VidFiles { get; set; } = new List<FileInfo>();

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
			TMDbData = TMDbData;
			Loaded = true;
			Refresh();
		}

		public void Play(FileInfo epFile = null)
		{
			if (epFile == null && VidFiles.Any())
			{
				if (VidFiles.Count == 1)
					epFile = VidFiles.FirstOrDefault();
				else
				{
					if ((Data.Mainform.CurrentPanel is PC_MoviePage psc) && psc.LinkedMovie == this)
					{
						psc.ST_Files.Selected = true;
					}
					else
					{
						var pce = new PC_MoviePage(this);
						pce.ST_Files.Selected = true;
						Data.Mainform.PushPanel(Data.Mainform.PI_Watch, pce);
					}
					return;
				}
			}

			if (epFile == null || !File.Exists(epFile.FullName))
			{
				VidFiles = LocalMovieHandler.GetFile(this).ToList();
				Play();
				return;
			}

			PC_Player pc = null;
			var pushed = false;

			try
			{
				if (Data.Mainform.CurrentPanel is PC_Player player)
					player.SetMovie(this, epFile);
				else
				{
					pc = new PC_Player(this, epFile);
					pushed = true;
					if (pc.Movie != null)
						Data.Mainform.PushPanel(PanelItem.Empty, pc);
					else
						pc.Dispose();
				}
			}
			catch
			{
				pc?.Dispose();
				if (pushed)
					Data.Mainform.PushBack();

				MessagePrompt.Show("Something went wrong while loading your movie.\n\nMake sure it fully downloaded, or try another file.", icon: PromptIcons.Error, form: Data.Mainform);

				return;
			}

			Data.Mainform.ShowUp();
		}

		public void Save()
		{
			if (Id.If(0, tMDbData.Id) != 0)
			{
				Save($"MoviesData/{Name.RegexRemove("[^\\w\\ ]").RegexReplace(" {1,}", "-")}-{TMDbData.Id}.scm", true);
				foreach (var item in Directory.GetFiles(Path.Combine(DocsFolder, "MoviesData"), $"*-{Id}.scm"))
					if (item != Path.Combine(DocsFolder, "MoviesData", $"{Name.RegexRemove("[^\\w\\ ]").RegexReplace(" {1,}", "-")}-{Id.If(0, tMDbData.Id)}.scm"))
						File.Delete(item);
			}
		}

		public void ShowStrip(bool showPage = true)
		{
			FlatToolStrip.Show(Data.Mainform,
				new FlatStripItem("Play", () => Play(), image: ProjectImages.Tiny_Play, show: VidFiles.Any(x => x.Exists)),
				new FlatStripItem("Download", () => Data.Mainform.PushPanel(null, new PC_Download(this)), image: ProjectImages.Tiny_Download),
				new FlatStripItem("View File".Plural(VidFiles), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					if ((Data.Mainform.CurrentPanel is PC_MoviePage psc) && psc.LinkedMovie == this)
					{
						psc.ST_Files.Selected = true;
					}
					else
					{
						var pce = new PC_MoviePage(this);
						pce.ST_Files.Selected = true;
						Data.Mainform.PushPanel(Data.Mainform.PI_Watch, pce);
					}
					Cursor.Current = Cursors.Default;
				}, image: ProjectImages.Tiny_Folder, show: VidFiles.Any()),
				FlatStripItem.Empty,
				new FlatStripItem("Movie Page", () => Data.Mainform.PushPanel(null, new PC_MoviePage(this)), image: ProjectImages.Tiny_Info, show: showPage),
				new FlatStripItem("Refresh", () => Refresh(), image: ProjectImages.Tiny_Refresh),
				FlatStripItem.Empty,

				new FlatStripItem("Mark", image: ProjectImages.Tiny_Ok, fade: true, show: Progress > 0 || WatchTime > 0),
				new FlatStripItem("as Complete", tab: 1, show: Progress > 0 || WatchTime > 0, action: () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					MarkAs(true);

					new Action(()=>
					{
						LocalMovieHandler.Refresh(this);
						MovieManager.Save(this);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}),
				new FlatStripItem("as " + ((Watched || Progress > 0 || WatchTime > 0) ? "Unwatched" : "Watched"), tab: 1, show: (Progress > 0 || WatchTime > 0), action: () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					MarkAs(!(Watched || Progress > 0 || WatchTime > 0));

					new Action(() =>
					{
						LocalMovieHandler.Refresh(this);
						MovieManager.Save(this);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}),
				new FlatStripItem("Mark as " + ((Watched || Progress > 0 || WatchTime > 0) ? "Unwatched" : "Watched"), show: !(Progress > 0 || WatchTime > 0), image: ProjectImages.Tiny_Ok, action: () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					MarkAs(!(Watched || Progress > 0 || WatchTime > 0));

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

		public void MarkAs(bool v)
		{
			if (Watched = v)
				WatchDate = DateTime.Now;
			WatchTime = -1;
			Progress = 0;

		}
	}

#pragma warning restore CS4014
}