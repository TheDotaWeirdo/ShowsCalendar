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
using ShowsRenamer.Module.Classes;
using Extensions;
using SlickControls.Classes;
using ShowsCalendar.Handlers;
using ShowsRenamer.Module.Interfaces;
using SlickControls.Controls;
using System.IO;
using ShowsRenamer.Module.Handlers;

namespace ShowsCalendar.Panels
{
	public partial class PC_LibraryRenamer : PanelContent
	{
		private EpisodeFile sampleEp = new EpisodeFile(null) { SeasonNumber = 11, EpisodeNumber = 5, Name = "Ep Name", Show = new ShowFiles() { Name = "Show" } };

		public PC_LibraryRenamer()
		{
			InitializeComponent();

			RB_Style1.Data = new NamingStyle(" - S", "E", " - ");
			RB_Style2.Data = new NamingStyle(" ", "x", " ");
			RB_Style3.Data = new NamingStyle(" Season ", " - E", " - ");
			RB_Style4.Data = new NamingStyle(" S", " - Episode ", " - ");
			RB_Custom.Data = new NamingStyle(" • ", "x", " • ");

			CB_ShowSeries.Checked = Options.Current.ShowSeries;
			CB_AddZero.Checked = Options.Current.AddZero;
			CB_IncSubs.Checked = Options.Current.IncSubs;
			CB_SyncOnline.Checked = Options.Current.SubsInSeperateFolder;
			CB_CleanFolders.Checked = Options.Current.CleanFolders;
			CB_Auto.Checked = Classes.Data.Options.AutoCleaner;

			if (RB_Style1.RadioGroup.Any(x => Options.Current.NamingStyle.Equals(x.Data)))
			{
				RB_Style1.RadioGroup.FirstThat(x => Options.Current.NamingStyle.Equals(x.Data)).Checked = true;
			}
			else if (Options.Current.NamingStyle != null)
			{
				TB_CN_1.Text = Options.Current.NamingStyle.PreSeason;
				TB_CN_2.Text = Options.Current.NamingStyle.PreEpisode;
				TB_CN_3.Text = Options.Current.NamingStyle.PreName;
			}

			foreach (var radio in RB_Style1.RadioGroup)
				radio.Text = sampleEp.GetName(radio.Data as NamingStyle);

			if (Classes.Data.RenameHandler != null)
				SetEnabled(false);
		}
		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_AutoIInfo.ForeColor = design.InfoColor;
		}

		private void PC_LibraryRenamer_Resize(object sender, EventArgs e)
		{
			P_Tabs.MaximumSize = new Size(panel2.Width, 9999);
			P_Tabs.MinimumSize = new Size(panel2.Width, 0);
		}
		
		private void CB_AddZero_CheckChanged(object sender, EventArgs e)
		{
			Options.Current.AddZero = CB_AddZero.Checked;

			foreach (var radio in RB_Style1.RadioGroup)
				radio.Text = sampleEp.GetName(radio.Data as NamingStyle);
		}

		public override bool CanExit()
		{
			Options.Current.ShowSeries = CB_ShowSeries.Checked;
			Options.Current.AddZero = CB_AddZero.Checked;
			Options.Current.IncSubs = CB_IncSubs.Checked;
			Options.Current.SubsInSeperateFolder = CB_SyncOnline.Checked;
			Options.Current.CleanFolders = CB_CleanFolders.Checked;
			Options.Current.NamingStyle = (NamingStyle)RB_Style1.RadioGroup.GetSelectedData();

			Options.Current.Save();

			Classes.Data.Options.AutoCleaner = CB_Auto.Checked;
			Classes.Data.Options.Save();

			return true;
		}

		private void TB_CN_TextChanged(object sender, EventArgs e)
		{
			RB_Custom.Data = new NamingStyle(TB_CN_1.Text, TB_CN_2.Text, TB_CN_3.Text);
			RB_Custom.Text = sampleEp.GetName(RB_Custom.Data as NamingStyle);
			RB_Custom.Checked = true;
		}

		private void CB_ShowSeries_CheckChanged(object sender, EventArgs e)
		{
			Options.Current.ShowSeries = CB_ShowSeries.Checked;

			foreach (var radio in RB_Style1.RadioGroup)
				radio.Text = sampleEp.GetName(radio.Data as NamingStyle);
		}

		private void B_Run_Click(object sender, EventArgs e)
		{
			SetEnabled(false);
			CanExit();

			var notif = Notification.Create(PaintNotification, null).Show(Form);

			notif.PictureBox.Image = FormDesign.Loader;
			notif.PictureBox.Text = "Finding & matching video files in your library folders";

			Classes.Data.RenameHandler = new RenameHandler(new CleaningSessionInfo()
			{
				Paths = LocalFileHandler.GeneralFolders.Convert(x => x.FullName),
				RunningForm = Form
			});

			Classes.Data.RenameHandler.Log += (p, m) =>
			{
				notif.PictureBox.TryInvoke(() =>
				{
					notif.PictureBox.Text = m;
					notif.PictureBox.Refresh();
				});
			};

			new Action(() =>
			{
				RunCleaner();
				notif.Close();
				SetEnabled(true);
			}).RunInBackground(System.Threading.ThreadPriority.BelowNormal);
	}

		public static void RunCleaner()
		{
			LocalShowHandler.LoadFiles();
			LocalMovieHandler.ReloadMovieFiles();

			var files = new List<IRenameFile>();

			foreach (var show in ShowManager.Shows)
			{
				var showfiles = new ShowFiles()
				{
					Name = show.Name
				};

				var eps = show.Episodes.Where(x => x.VidFiles.Any(y => y.Exists)).ConvertEnumerable(
					x => x.VidFiles.Convert(item => new EpisodeFile(item)
					{
						SeasonNumber = x.SN,
						EpisodeNumber = x.EN,
						Name = x.Name,
						Show = showfiles
					})).ToArray();

				var path = LocalShowHandler.GetShowFolder(show);

				if (Directory.Exists(path))
				{
					foreach (var ep in eps)
						ep.GenerateNewFile(Directory.GetParent(path).FullName, Options.Current.NamingStyle);

					showfiles.Episodes = eps;

					files.AddRange(eps);

					foreach (var item in eps)
						files.AddRange(item.GetSubtitleFiles(path, Options.Current.NamingStyle));

					foreach (var file in new DirectoryInfo(path).EnumerateFiles("*.*", SearchOption.AllDirectories)
						.Where(x => !files.Any(y => y.CurrentFile.FullName == x.FullName) && JunkFilesHandler.Validate(x)))
					{
						files.Add(new JunkFile(file));
					}
				}
			}

			var movies = MovieManager.Movies.Where(x => x.VidFiles.Any(y => y.Exists)).ConvertEnumerable(
				x => x.VidFiles.Convert(item => new MovieFile(item)
				{
					Title = x.Title,
					Year = x.TMDbData.ReleaseDate?.Year ?? 0
				})).ToArray();

			files.AddRange(movies);

			foreach (var movie in movies)
			{
				movie.GenerateNewFile(movie.CurrentFile.Directory.Parent.FullName, Options.Current.NamingStyle);

				files.AddRange(movie.GetSubtitleFiles(Options.Current.NamingStyle));

				foreach (var file in new DirectoryInfo(movie.CurrentFile.Directory.Parent.FullName).EnumerateFiles("*.*", SearchOption.AllDirectories)
					.Where(x => !files.Any(y => y.CurrentFile.FullName == x.FullName) && JunkFilesHandler.Validate(x)))
				{
					files.Add(new JunkFile(file));
				}
			}

			Classes.Data.RenameHandler.Session.Files = files;

			try
			{
				LocalFileHandler.Pause();

				Classes.Data.RenameHandler.RunCleaner();
			}
			catch { }
			finally
			{
				Classes.Data.RenameHandler = null;
				LocalFileHandler.Resume();
			}
		}

		private void PaintNotification(PictureBox pb, Graphics g)
		{
			g.DrawImage(pb.Image, 8, (pb.Height - 22) / 2, 22, 22);

			g.DrawString("Library Cleaner", new Font("Nirmala UI", 9.75F), new SolidBrush(FormDesign.Design.ForeColor), 36, 4);

			g.DrawString(pb.Text, new Font("Nirmala UI", 8.25F), new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(39, 26, pb.Width - 50, pb.Height - 29), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}

		private void SetEnabled(bool val)
		{
			this.TryInvoke(() =>
			{
				TLP_Options.Enabled =
					RB_Style1.Enabled =
					RB_Style2.Enabled =
					RB_Style3.Enabled =
					RB_Style4.Enabled =
					RB_Custom.Enabled =
					TB_CN_1.Enabled =
					TB_CN_2.Enabled =
					TB_CN_3.Enabled =
					B_Run.Enabled = val;
			});
		}
	}
}
