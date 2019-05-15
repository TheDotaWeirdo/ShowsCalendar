using Extensions;
using ShowsCalendar.Classes;
using ShowsCalendar.Controls;
using ShowsCalendar.Handlers;
using SlickControls.Classes;
using SlickControls.Forms;
using SlickControls.Panels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar.Panels
{
	public partial class PC_Player : PanelContent
	{
		private int CONTROLS_SIZE => Form.TopMost && Data.Options.DisabledMiniplayer ? 36 : 64;
		private AnimationHandler ControlsHideAnimation;
		private readonly WaitIdentifier ControlsHideIdentifier = new WaitIdentifier();
		private bool ControlsOpening = false;
		public Episode Episode { get; private set; }
		public FileInfo VidFile { get; private set; }
		public Movie Movie { get; private set; }
		private DateTime lastClick = DateTime.MinValue;
		private Point LastMousePoint = Point.Empty;
		private bool TimeLoop = true;
		private bool firstLoad;

		private PC_Player()
		{
			InitializeComponent();

			P_VLC.Controls.Add(vlcControl = new Vlc.DotNet.Forms.VlcControl());

			vlcControl.BeginInit();
			vlcControl.Dock = DockStyle.Fill;
			vlcControl.Spu = -1;
			vlcControl.TabIndex = 0;
			vlcControl.VlcLibDirectory = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "vlc"));
			vlcControl.VlcMediaplayerOptions = new string[] { "-vv" };
			vlcControl.Paused += vlcControl_Paused;
			vlcControl.Playing += vlcControl_Playing;
			vlcControl.TimeChanged += vlcControl_TimeChanged;
			vlcControl.EndReached += VlcControl_EndReached;
			vlcControl.EndInit();

			SlickTip.SetTo(SL_Backwards, $"Go Back {Data.Options.BackwardTime} Seconds");
			SlickTip.SetTo(SL_Forward, $"Go Forward {Data.Options.ForwardTime} Seconds");
			SlickTip.SetTo(SL_Previous, "Switch to Previous Episode");
			SlickTip.SetTo(SL_Next, "Switch to Next Episode");
			SlickTip.SetTo(SL_Play, "Play / Pause");
			SlickTip.SetTo(SL_More, "More Options");
			SlickTip.SetTo(SL_Subs, "Select Subtitles");
			SlickTip.SetTo(SL_Audio, "Select Sound Track");
			SlickTip.SetTo(SL_MiniPlayer, "Toggle Mini-Player");
			SlickTip.SetTo(SL_FullScreen, "Toggle Full-screen");

			var md = new MouseDetector();
			md.MouseMove += Md_MouseMove;
			Disposed += (s, e) => md.Dispose();

			slickScroll.SizeSource = () =>
			{
				var c = TLP_SimilarContent.Visible ? (Control)TLP_SimilarContent : SP_Cast;

				return c.Height
					+ c.Top
					+ TLP_Suggestions.Top
					+ P_Info.Top;
			};
		}

		private bool lazyMiniPlayerWaiting;

		private void Md_MouseMove(object sender, Point p)
		{
			if (new Rectangle(vlcControl.PointToScreen(Point.Empty), vlcControl.Size).Contains(p)
				&& LastMousePoint != p)
			{
				if (Form.TopMost && Data.Options.DisabledMiniplayer)
				{
					lazyMiniPlayerWaiting = true;
					new Action(() =>
					{
						if (lazyMiniPlayerWaiting)
						{
							lazyMiniPlayerWaiting = false;
							this.TryInvoke(() =>
							{
								if (new Rectangle(vlcControl.PointToScreen(Point.Empty), vlcControl.Size).Contains(MousePosition))
									ShowPlayControls();
							});
						}
					}).RunInBackground(750);
				}
				else
					ShowPlayControls();
			}
		}

		public PC_Player(Episode ep, FileInfo file) : this()
		{
			SetEpisode(ep, file);
			SL_Next.Visible = SL_Previous.Visible = true;
		}

		public PC_Player(Movie movie, FileInfo file) : this() => SetMovie(movie, file);

		private void VlcControl_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
		{
			if (Episode != null)
				Episode.MarkAs(true);
			else
				Movie.MarkAs(true);

			this.TryInvoke(() =>
			{
				SL_Play.Image = ProjectImages.Tiny_PlayNoBorder;
				if (Data.Options.AutomaticEpisodeSwitching && Episode != null && (Episode.Next?.VidFiles.Any(y => y.Exists) ?? false))
					SetEpisode(Episode.Next, play: Data.Options.AutomaticEpisodePlay);
				else
					ToggleFullscreen(false);
			});
		}

		public bool FullScreen { get; private set; } = false;
		public bool Paused { get; private set; } = false;

		private void FileLoaded()
		{
			StopLoader();
			P_BotSpacer.Enabled = true;
			if (SL_Subs.Enabled = vlcControl.SubTitles.All.Any(x => x.ID != -1))
				vlcControl.SubTitles.Current = vlcControl.SubTitles.All.FirstThat(x => x.ID != -1);

			ShowPlayControls();
			var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;
			if (vidInf.Width != 0)
				P_VLC.Height = Math.Min(Height - P_BotSpacer.Height - Padding.Top - TLP_MoreInfo.Height, 2 + (int)(P_VLC.Width * vidInf.Height / vidInf.Width));
			L_Resolution.Text = $"{vidInf.Width} x {vidInf.Height} pixels";
			L_Subtitles.Text = vlcControl.SubTitles.Count.If(x => x == 0, x => $"None", x => $"{x} SubTitles");
		}

		public void SetMovie(Movie movie, FileInfo epFile = null, bool play = true)
		{
			if (movie != null)
			{
				epFile = epFile != null && epFile.Exists ? epFile : LocalMovieHandler.GetFile(movie).FirstOrDefault();
				if (epFile != null)
				{
					StartLoader();

					Movie = Movie ?? movie;
					VidFile = epFile;

					this.TryInvoke(() =>
					{
						slickScroll.SetPercentage(0);
						P_BotSpacer.Enabled = false;

						Text = movie.Title;

						if (movie.tMDbData.ReleaseDate != null)
						{
							if (movie.tMDbData.ReleaseDate < DateTime.Now)
								L_AirDate.Text = $"Aired {movie.tMDbData.ReleaseDate?.RelativeString()}";
							else
								L_AirDate.Text = $"Airing {movie.tMDbData.ReleaseDate?.RelativeString()}";
						}
						else
						{
							L_AirDate.Visible = L_3.Visible = false;
						}

						L_Resolution.Text = L_Subtitles.Text = "Loading..";
						L_Plot.Text = string.IsNullOrWhiteSpace(Movie.TMDbData?.Overview) ? "No Overview" : Movie.TMDbData?.Overview;

						SP_Cast.Info = "Meet the actors behind the show.";
						SP_Cast.Content.Controls.Clear(true);
						foreach (var cast in Movie.Cast)
							SP_Cast.Add(new CharacterControl(cast));

						SP_Crew.Content.Controls.Clear(true);
						foreach (var crew in Movie.Crew)
							SP_Crew.Add(new CharacterControl(crew));

						setSimilarCharacters(Movie.Cast);

						TLP_Suggestions.Controls.Clear(true, x => x is WatchControl);
						TLP_Suggestions.Controls.Add(new WatchControl(movie, true, "NOW PLAYING") { Enabled = false, Anchor = AnchorStyles.Left }, 1, 1);

						TLP_Suggestions.SetColumnSpan(tableLayoutPanel1, 2);
					});

					new Action(() =>
					{
						if (vlcControl.IsDisposed)
							return;

						firstLoad = true;
						SaveWatchtime();

						Movie = movie;

						vlcControl.SetMedia(epFile, GetSubs(epFile, true).ToArray());
						if (play)
							vlcControl.Play();
						else if (vlcControl.IsPlaying)
							vlcControl.Pause();

						ShowPlayControls();

						TimeLoop = false;
						vlcControl.Time = movie.WatchTime - 5000;
						TimeLoop = true;

						if (!play)
						{
							this.TryInvoke(() => { SS_TimeSlider.Value = 0; P_BotSpacer.Enabled = true; });
							StopLoader();
						}
					}).RunInBackground();
				}
				else
				{
					this.TryInvoke(() =>
					{
						MessagePrompt.Show($"\"{movie.Title}\" does not seem to have a file associated with it.\n\nCheck that a file exists, or is named correctly", "File Missing", icon: SlickControls.Enums.PromptIcons.Error);
					});
				}
			}
		}

		private IEnumerable<string> GetSubs(FileInfo epFile, bool movie)
		{
			foreach (var item in Directory.GetParent(epFile.FullName).GetFilesByExtensions(".srt"))
			{
				if (movie || item.FileName().SpellCheck(epFile.FileName(), false) <= 2)
					yield return $"-sub-file=file:///{item.FullName.Replace('\\', '/')}";
			}
		}

		public void SetEpisode(Episode ep, FileInfo epFile = null, bool play = true)
		{
			if (ep != null)
			{
				epFile = epFile != null && epFile.Exists ? epFile : LocalShowHandler.GetFile(ep).FirstOrDefault();
				if (epFile != null)
				{
					StartLoader();

					Episode = Episode ?? ep;
					VidFile = epFile;

					this.TryInvoke(() =>
					{
						slickScroll.SetPercentage(0);
						P_BotSpacer.Enabled = false;

						Text = $"{ep.Show} • {ep}";

						if (ep.AirState == AirStateEnum.Aired)
							L_AirDate.Text = $"Aired {ep.TMDbData.AirDate?.RelativeString()}";
						else if (ep.AirState == AirStateEnum.ToBeAired)
							L_AirDate.Text = $"Airing {ep.TMDbData.AirDate?.RelativeString()}";
						else
							L_AirDate.Visible = L_3.Visible = false;

						L_Resolution.Text = L_Subtitles.Text = "Loading..";
						L_Plot.Text = string.IsNullOrWhiteSpace(ep.TMDbData?.Overview) ? "No Overview" : ep.TMDbData?.Overview;

						SP_Cast.Info = "From your beloved cast to this episode's notable guest stars";
						SP_Cast.Content.Controls.Clear(true);
						foreach (var cast in Episode.Season.TMDbData.Credits.Cast.Concat(Episode.TMDbData.GuestStars).Distinct(x => x.Id))
							SP_Cast.Add(new CharacterControl(cast));

						SP_Crew.Content.Controls.Clear(true);
						foreach (var crew in Episode.TMDbData.Crew)
							SP_Crew.Add(new CharacterControl(crew));

						setSimilarCharacters(Episode.Season.TMDbData.Credits.Cast.Concat(Episode.TMDbData.GuestStars));

						TLP_Suggestions.Controls.Clear(true, x => x is WatchControl);
						TLP_Suggestions.Controls.Add(new WatchControl(ep, true, "NOW PLAYING") { Enabled = false, Anchor = AnchorStyles.Left }, 1, 1);

						var next = ep.Next;

						TLP_Suggestions.SetColumnSpan(tableLayoutPanel1, next != null ? 1 : 2);

						if (next != null)
							TLP_Suggestions.Controls.Add(new WatchControl(next, true, "UP NEXT") { Anchor = AnchorStyles.Right }, 3, 1);

						if (next != null && next.AirState == AirStateEnum.Aired && !(next.VidFiles.Any(y => y.Exists)))
						{
							Notification.Create("Download Next Episode?"
								, $"Episode {next} isn't downloaded yet.\nClick to download it while you watch."
								, SlickControls.Enums.PromptIcons.Question
								, () =>
								{
									ToggleFullscreen(false);
									vlcControl.Pause();
									Paused = true;
									Form.PushPanel(null, new PC_Download(next));
								}
							).Show(Data.Mainform, 20);
						}
					});

					new Action(() =>
					{
						if (vlcControl.IsDisposed)
							return;

						firstLoad = true;
						SaveWatchtime();

						Episode = ep;

						vlcControl.SetMedia(epFile, GetSubs(epFile, true).ToArray());
						if (play)
							vlcControl.Play();
						else if (vlcControl.IsPlaying)
							vlcControl.Pause();

						ShowPlayControls();

						TimeLoop = false;
						vlcControl.Time = ep.WatchTime - 5000;
						TimeLoop = true;

						if (!play)
						{
							this.TryInvoke(() => { SS_TimeSlider.Value = 0; P_BotSpacer.Enabled = true; });
							StopLoader();
						}
					}).RunInBackground();
				}
				else
				{
					this.TryInvoke(() =>
					{
						MessagePrompt.Show($"Episode \"{ep}\" does not seem to have a file associated with it.\n\nCheck that a file exists, or is named correctly", "File Missing", icon: SlickControls.Enums.PromptIcons.Error);
						if (!string.IsNullOrWhiteSpace(ep.Show.ZooqleURL)
							&& DialogResult.Yes == MessagePrompt.Show($"Would you like to open the download window for \"{ep}\" ?", "Open Downloads", SlickControls.Enums.PromptButtons.YesNo, SlickControls.Enums.PromptIcons.Question))
						{
							Data.Mainform.PushPanel(null, new PC_Download(ep));
						}
					});
				}
			}
		}

		public override bool CanExit(bool toBeDisposed)
		{
			SaveWatchtime();

			if (!toBeDisposed && vlcControl.IsPlaying)
			{
				vlcControl.Pause();
			}

			return true;
		}

		private void SaveWatchtime()
		{
			if (Episode != null)
			{
				if (vlcControl.Length > 0 && vlcControl.Time > 10000)
				{
					var ended = vlcControl.Time > vlcControl.Length * 90 / 100;
					Episode.WatchTime = ended ? 0 : vlcControl.Time;
					Episode.Watched = Episode.Watched || ended;
					Episode.WatchDate = DateTime.Now;
					Episode.Progress = Episode.Started ? 100 * vlcControl.Time / vlcControl.Length : 0;

					new Action(() =>
					{
						ShowManager.Save(Episode);
						LocalShowHandler.Refresh(Episode.Show); ;
					}).RunInBackground();
				}
			}
			else if (Movie != null)
			{
				if (vlcControl.Length > 0 && vlcControl.Time > 10000)
				{
					var ended = vlcControl.Time > vlcControl.Length * 90 / 100;
					Movie.WatchTime = ended ? 0 : vlcControl.Time;
					Movie.WatchDate = DateTime.Now;
					Movie.Watched = Movie.Watched || ended;
					Movie.Progress = Movie.Started ? 100 * vlcControl.Time / vlcControl.Length : 0;

					new Action(() =>
					{
						MovieManager.Save(Movie);
						LocalMovieHandler.Refresh(Movie); ;
					}).RunInBackground();
				}
			}
		}

		internal void SL_Play_Click(object sender, EventArgs e)
		{
			if (vlcControl.IsPlaying)
			{
				vlcControl.Pause();
				Paused = true;
				SaveWatchtime();
			}
			else
			{
				vlcControl.Play();
				Paused = false;
			}
			ShowPlayControls();
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			L_3.ForeColor = L_4.ForeColor =
				L_6.ForeColor = L_7.ForeColor = design.LabelColor;
			P_BotSpacer.BackColor = design.BackColor;
			TLP_Suggestions.BackColor = P_Info.BackColor = design.AccentBackColor;
			TLP_MoreInfo.BackColor = slickScroll.Percentage.If(0, design.BackColor, design.AccentBackColor);
			I_MoreInfo.Color(FormDesign.Design.ForeColor);
		}

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Space)
			{
				SL_Play_Click(null, null);
				return true;
			}

			if (keyData == Keys.Left)
			{
				SL_Backwards_Click(null, null);
				return true;
			}

			if (keyData == Keys.Right)
			{
				SL_Forward_Click(null, null);
				return true;
			}

			if (keyData == Keys.Up)
			{
				SS_Volume.Value += 5;
				return true;
			}

			if (keyData == Keys.Down)
			{
				SS_Volume.Value -= 5;
				return true;
			}

			if (keyData == Keys.Escape && FullScreen)
			{
				ToggleFullscreen(false);
				return true;
			}

			if (keyData == Keys.Escape && Form.TopMost)
			{
				SL_MiniPlayer_Click(null, null);
				return true;
			}

			if (keyData == (Keys.Alt | Keys.Enter))
			{
				ToggleFullscreen(!FullScreen);
				return true;
			}

			return false;
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			Form.OnWndProc += Form_OnWndProc;
			Form.VisibleChanged += Form_VisibleChanged;
			Disposed += (s, e) =>
			{
				Form.OnWndProc -= Form_OnWndProc;
				Form.VisibleChanged -= Form_VisibleChanged;
			};
		}

		private void Form_VisibleChanged(object sender, EventArgs e)
		{
			if (!Form.Visible)
				vlcControl.Pause();
		}

		private void Form_OnWndProc(Message m)
		{
			try
			{
				if (Form.CurrentPanel == this && m.Msg == 0x210 && m.WParam.ToInt32() == 513) //513 is WM_LBUTTONCLICK
				{
					if (new Rectangle(vlcControl.PointToScreen(Point.Empty), vlcControl.Size).Contains(Cursor.Position)
						&& !new Rectangle(P_BotSpacer.PointToScreen(Point.Empty), P_BotSpacer.Size).Contains(Cursor.Position))
					{
						if (!Form.TopMost && DateTime.Now.Ticks - lastClick.Ticks < 500 * TimeSpan.TicksPerMillisecond)
						{
							SL_Play_Click(null, null);

							ToggleFullscreen(!FullScreen);

							lastClick = DateTime.MinValue;
						}
						else if (!Form.TopMost || !Data.Options.DisabledMiniplayer)
						{
							SL_Play_Click(null, null);
							lastClick = DateTime.Now;
						}
					}
				}
				else if (Form.TopMost && Form.CurrentPanel == this && m.Msg == 0x214)
				{   // WM_SIZING
					var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;
					var ratio = (double)vidInf.Width / vidInf.Height;
					var rc = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));
					var w = rc.Right - rc.Left; // get width
					var h = rc.Bottom - rc.Top; // get height
					switch ((int)m.WParam)
					{
						case 1: //left
							rc.Bottom = rc.Top + (int)(w / ratio) + (Form.Padding.Horizontal / 2);
							break;
						case 2: //right
							rc.Bottom = rc.Top + (int)(w / ratio) + (Form.Padding.Horizontal / 2);
							break;
						case 3: //top
							rc.Right = rc.Left + (int)(h * ratio) - (Form.Padding.Vertical / 1);
							break;
						case 4:
							if (w / ratio < h)
								rc.Left = rc.Right - (int)(h * ratio) + (Form.Padding.Vertical / 1);
							else
								rc.Top = rc.Bottom - (int)(w / ratio) - (Form.Padding.Horizontal / 2);
							break;
						case 5:
							if (w / ratio < h)
								rc.Right = rc.Left + (int)(h * ratio) - (Form.Padding.Vertical / 1);
							else
								rc.Top = rc.Bottom - (int)(w / ratio) - (Form.Padding.Horizontal / 2);
							break;
						case 6:
							rc.Right = rc.Left + (int)(h * ratio) - (Form.Padding.Vertical / 1);
							break;
						case 7:
							if (w / ratio < h)
								rc.Left = rc.Right - (int)(h * ratio) + (Form.Padding.Vertical / 1);
							else
								rc.Bottom = rc.Top + (int)(w / ratio) + (Form.Padding.Horizontal / 2);
							break;
						case 8:
							if (w / ratio < h)
								rc.Right = rc.Left + (int)(h * ratio) - (Form.Padding.Vertical / 1);
							else
								rc.Bottom = rc.Top + (int)(w / ratio) + (Form.Padding.Horizontal / 2);
							break;
						default:
							break;
					}
					Marshal.StructureToPtr(rc, m.LParam, false);
					m.Result = (IntPtr)1;
				}
			}
			catch { }
		}

		private void ShowPlayControls()
		{
			if (!(FullScreen || (Form?.TopMost ?? false)) || ControlsOpening)
				return;

			if (P_BotSpacer.Height != CONTROLS_SIZE)
			{
				ControlsOpening = true;
				ControlsHideAnimation?.Dispose();
				ControlsHideAnimation = new AnimationHandler(P_BotSpacer, new Size(P_BotSpacer.Width, CONTROLS_SIZE)) { IgnoreWidth = true, SpeedModifier = 5 };
				ControlsHideAnimation.StartAnimation();
				ControlsHideAnimation.OnEnd += (s, e, t) => ControlsOpening = false;
			}

			LastMousePoint = Cursor.Position;

			if (!Paused)
			{
				ControlsHideIdentifier.Enable();

				ControlsHideIdentifier.Wait(HideControls, 750);
			}
			else
			{
				ControlsHideIdentifier.Disable();
			}
		}

		private void HideControls() => this.TryInvoke(() =>
		{
			if ((FullScreen || Form.TopMost) && !Paused && !firstLoad && !new Rectangle(P_BotSpacer.PointToScreen(new Point(10, 10)), new Size(P_BotSpacer.Width - 20, P_BotSpacer.Height - 20)).Contains(Cursor.Position))
			{
				LastMousePoint = Cursor.Position;
				ControlsHideAnimation?.Dispose();
				ControlsHideAnimation = new AnimationHandler(P_BotSpacer, new Size(P_BotSpacer.Width, 0)) { IgnoreWidth = true, Interval = 10, SpeedModifier = 20 };
				ControlsHideAnimation.StartAnimation();
			}
		});

		private void SI_More_Click(object sender, EventArgs et)
		{
			var items = new List<FlatStripItem>()
				{
					new FlatStripItem("More Info", () =>
					{
						 slickScroll.ScrollTo(slickScroll.Percentage.If(0, TLP_MoreInfo, P_VLC), 6);
					}, image: ProjectImages.Tiny_Info),

					new FlatStripItem("Episode Page", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
						if (Form.TopMost)
							SL_MiniPlayer_Click(null,null);
						if (FullScreen)
							ToggleFullscreen(false);

						Data.Mainform.PushPanel(null, new PC_ShowPage(Episode));
					}, image: ProjectImages.Tiny_TV, show: Episode != null),

					new FlatStripItem("Movie Page", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
						if (Form.TopMost)
							SL_MiniPlayer_Click(null,null);
						if (FullScreen)
							ToggleFullscreen(false);

						Form.PushPanel(null, new PC_MoviePage(Movie));
					}, image: ProjectImages.Tiny_Movie, show: Movie != null),

					FlatStripItem.Empty,

					new FlatStripItem("View File", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
						if (Form.TopMost)
							SL_MiniPlayer_Click(null,null);
						if (FullScreen)
							ToggleFullscreen(false);

						System.Diagnostics.Process.Start(VidFile.Directory.FullName);
					}, image: ProjectImages.Tiny_Folder, show: VidFile != null && VidFile.Exists),

					FlatStripItem.Empty
				};

			if (Episode != null)
			{
				items.AddRange(new[]
				{

				new FlatStripItem("Mark", image: ProjectImages.Tiny_Ok, fade: true),

				new FlatStripItem("  Ep. as Complete", () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					Episode.WatchDate = DateTime.Now;
					Episode.WatchTime = 0;
					Episode.Progress = 0;
					new Action(()=>
					{
						LocalShowHandler.Refresh(Episode.Show);
						ShowManager.Save(Episode);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}, show: !Episode.Watched && Episode.Progress > 0),

				new FlatStripItem("  Ep. as " + (Episode.Watched ? "Unwatched" : "Watched"), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					Episode.WatchDate = DateTime.Now;
					Episode.Watched = !Episode.Watched;
					if(Episode.Watched)
					{
						Episode.WatchTime = 0;
						Episode.Progress = 0;
					}
					new Action(()=>
					{
						LocalShowHandler.Refresh(Episode.Show);
						ShowManager.Save(Episode);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}),

				new FlatStripItem("  Season as " + (Episode.Season.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched) ? "Unwatched" : "Watched"), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					var set = !Episode.Season.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched);
					Episode.Season.Episodes.ForEach(e =>
						{
							e.Watched = e.AirState == AirStateEnum.Aired ? set : e.Watched;
							if(e.Watched)
								e.WatchDate = DateTime.Now;
						});
					new Action(()=>
					{
						LocalShowHandler.Refresh(Episode.Show);
						ShowManager.Save(Episode.Show);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}),

				new FlatStripItem("  Show as " + (Episode.Show.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)) ? "Unwatched" : "Watched"), () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					var set = !Episode.Show.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched));
					Episode.Show.Seasons.ForEach(x => x.Episodes.ForEach(e =>
						{
							e.Watched = e.AirState == AirStateEnum.Aired ? set : e.Watched;
							if(e.Watched)
								e.WatchDate = DateTime.Now;
						}));
					new Action(()=>
					{
						LocalShowHandler.Refresh(Episode.Show);
						ShowManager.Save(Episode.Show);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				})
				});
			}
			else
			{
				items.Add(new FlatStripItem("Mark as " + (Movie.Watched ? "Unwatched" : "Watched"), image: ProjectImages.Tiny_Ok, action: () =>
				{
					Cursor.Current = Cursors.WaitCursor;
					Movie.WatchDate = DateTime.Now;
					Movie.Watched = !Movie.Watched;
					if (Movie.Watched)
					{
						Movie.WatchTime = 0;
						Movie.Progress = 0;
					}

					new Action(() =>
					{
						LocalMovieHandler.Refresh(Movie);
						MovieManager.Save(Movie);
					}).RunInBackground();
					Cursor.Current = Cursors.Default;
				}));
			}

			new FlatToolStrip(items, Data.Mainform).ShowUp();
		}

		private FormWindowState lastState;
		private Rectangle lastBounds;

		private void ToggleFullscreen(bool value = true)
		{
			FullScreen = value;

			if (FullScreen)
			{
				lastState = Form.WindowState;
				Form.WindowState = FormWindowState.Maximized;
			}
			else
			{
				Form.WindowState = lastState;
			}

			if (FullScreen)
			{
				vlcControl.Parent = Form;
				P_BotSpacer.Parent = Form;
				P_BotSpacer.Dock = DockStyle.Bottom;
				vlcControl.BringToFront();
				P_BotSpacer.BringToFront();
				SL_FullScreen.Image = ProjectImages.Tiny_NormalScreen;
			}
			else
			{
				if (Form.TopMost)
				{
					vlcControl.Parent = Form;
					P_BotSpacer.Parent = Form;
					P_BotSpacer.Dock = DockStyle.Bottom;
					vlcControl.BringToFront();
					P_BotSpacer.BringToFront();
				}
				else
				{
					vlcControl.Parent = P_VLC;
					P_BotSpacer.Parent = panel1;
					P_BotSpacer.BringToFront();
					P_Info.BringToFront();
					TLP_MoreInfo.BringToFront();
					ControlsHideAnimation?.StopAnimation();
					P_BotSpacer.Dock = DockStyle.Top;
					if (P_BotSpacer.Height != CONTROLS_SIZE)
						BeginInvoke(new Action(() => { P_BotSpacer.Height = CONTROLS_SIZE; PC_Player_Resize(null, null); }));
				}

				SL_FullScreen.Image = ProjectImages.Tiny_Fullscreen;
			}

			SL_Subs.Visible = SL_FullScreen.Visible = !Form.TopMost;
			SL_Previous.Visible = SL_Next.Visible = !Form.TopMost && Episode != null;

			ShowPlayControls();
		}

		private void SL_Backwards_Click(object sender, EventArgs e) => vlcControl.Time -= Data.Options.BackwardTime * 1000;

		private void SL_Forward_Click(object sender, EventArgs e) => vlcControl.Time += Data.Options.ForwardTime * 1000;

		private void SL_Next_Click(object sender, EventArgs e) => SetEpisode(Episode.Next);

		private void SL_Previous_Click(object sender, EventArgs e) => SetEpisode(Episode.Previous);

		private void SS_TimeSlider_MouseDown(object sender, MouseEventArgs e)
		{
			if (!Paused && vlcControl.IsPlaying)
				vlcControl.Pause();
		}

		private void SS_TimeSlider_MouseUp(object sender, MouseEventArgs e)
		{
			if (!Paused)
				vlcControl.Play();
		}

		private void SS_TimeSlider_ValuesChanged(object sender, EventArgs e)
		{
			if (TimeLoop)
			{
				TimeLoop = false;
				vlcControl.Time = (long)SS_TimeSlider.Value;
				if (Episode != null)
					Episode.Progress = 100 * vlcControl.Time / vlcControl.Length;
				var ts = TimeSpan.FromMilliseconds(vlcControl.Length - vlcControl.Time);
				L_Time.TryInvoke(() => L_Time.Text = (ts.Hours > 0 ? $"-{ts.Hours.ToString("00")}:" : "-") + $"{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}");

				ts = TimeSpan.FromMilliseconds(SS_TimeSlider.Value.If(double.NaN, 0));
				L_CurrentTime.TryInvoke(() => L_CurrentTime.Text = (ts.Hours > 0 ? $"{ts.Hours.ToString("00")}:" : "") + $"{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}");
				TimeLoop = true;
			}
		}

		private void vlcControl_Paused(object sender, Vlc.DotNet.Core.VlcMediaPlayerPausedEventArgs e) => this.TryInvoke(() => SL_Play.Image = ProjectImages.Tiny_PlayNoBorder);

		private void vlcControl_Playing(object sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
		{
			pausedFromScroll = false;
			SS_TimeSlider.MaxValue = vlcControl.Length;
			this.TryInvoke(() => SL_Play.Image = ProjectImages.Tiny_Pause);

			if (firstLoad)
			{
				firstLoad = false;
				this.TryInvoke(FileLoaded);
			}
		}

		private void vlcControl_TimeChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs e)
		{
			if (TimeLoop)
			{
				TimeLoop = false;

				if (vlcControl.Audio.Volume != (int)SS_Volume.Value)
					SS_Volume.Value = vlcControl.Audio.Volume;

				SS_TimeSlider.Value = vlcControl.Time;

				if (Episode != null)
					Episode.Progress = 100 * vlcControl.Time / vlcControl.Length;
				else
					Movie.Progress = 100 * vlcControl.Time / vlcControl.Length;

				var ts = TimeSpan.FromMilliseconds(vlcControl.Length - vlcControl.Time);
				L_Time.TryInvoke(() => L_Time.Text = (ts.Hours > 0 ? $"-{ts.Hours.ToString("00")}:" : "-") + $"{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}");

				ts = TimeSpan.FromMilliseconds(SS_TimeSlider.Value.If(double.NaN, 0));
				L_CurrentTime.TryInvoke(() => L_CurrentTime.Text = (ts.Hours > 0 ? $"{ts.Hours.ToString("00")}:" : "") + $"{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}");
				TimeLoop = true;
			}
		}

		private void SL_Subs_Click(object sender, EventArgs e)
		{
			var subs = new List<FlatStripItem>();

			subs.AddRange(vlcControl.SubTitles.All, x =>
				new FlatStripItem(x.Name, () =>
				{
					vlcControl.SubTitles.Current = x;
				}, (vlcControl.SubTitles.Current.ID == x.ID).If(ProjectImages.ArrowRight, null)
				, fade: vlcControl.SubTitles.Current.ID == x.ID));

			if (subs.Count > 0)
				new FlatToolStrip(subs, Data.Mainform).ShowUp();
		}

		private void SL_Audio_Click(object sender, EventArgs e)
		{
			var tracks = new List<FlatStripItem>();

			tracks.AddRange(vlcControl.Audio.Tracks.All, x =>
				new FlatStripItem(x.Name, () =>
				{
					vlcControl.Audio.Tracks.Current = x;
				}, (vlcControl.Audio.Tracks.Current.ID == x.ID).If(ProjectImages.ArrowRight, null)
				, fade: vlcControl.Audio.Tracks.Current.ID == x.ID));

			if (tracks.Count > 0)
				new FlatToolStrip(tracks, Data.Mainform).ShowUp();
		}

		private void SS_Volume_ValuesChanged(object sender, EventArgs e) => vlcControl.Audio.Volume = (int)SS_Volume.Value;

		private void SL_MiniPlayer_Click(object sender, EventArgs e)
		{
			var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;

			if (Form.TopMost)
			{
				Form.TopMost = false;
				ToggleFullscreen(false);
				SL_MiniPlayer.Image = ProjectImages.Tiny_MiniWindow;

				if (Data.Options.DisabledMiniplayer)
				{
					SS_TimeSlider.Visible = L_Time.Visible = L_CurrentTime.Visible = true;
					P_BotSpacer.Padding = new Padding(0, 2, 0, 0);
					P_Progress.Visible = false;
				}

				Form.Bounds = lastBounds;
				Form.MinimumSize = new Size(765, 445);
			}
			else
			{
				Form.TopMost = true;
				ToggleFullscreen(false);
				Form.WindowState = FormWindowState.Normal;
				SL_MiniPlayer.Image = ProjectImages.Tiny_RestoreWindow;

				if (Data.Options.DisabledMiniplayer)
				{
					SS_TimeSlider.Visible = L_Time.Visible = L_CurrentTime.Visible = false;
					P_Progress.Visible = true;
					P_BotSpacer.Padding = new Padding(0);
					P_BotSpacer.Height = CONTROLS_SIZE;
				}

				var h = 280;
				Form.MinimumSize = new Size((int)(150 * vidInf.Width / vidInf.Height) - Form.Padding.Horizontal, 150);
				Form.Bounds = new Rectangle(22, SystemInformation.VirtualScreen.Height - 325, (int)(h * vidInf.Width / vidInf.Height) - Form.Padding.Horizontal, h);
			}
		}

		private void SL_FullScreen_Click(object sender, EventArgs e)
		{
			if (Form.TopMost)
				SL_MiniPlayer_Click(null, null);
			ToggleFullscreen(!FullScreen);
		}

		private void PC_Player_Resize(object sender, EventArgs e)
		{
			try
			{
				panel1.MaximumSize = new Size(panel2.Width, 9999);
				panel1.MinimumSize = new Size(panel2.Width, 0);

				var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;
				if (vidInf.Width != 0)
					P_VLC.Height = Math.Min(Height - P_BotSpacer.Height - Padding.Top - TLP_MoreInfo.Height, 2 + (int)(P_VLC.Width * vidInf.Height / vidInf.Width));
				else
					P_VLC.Height = Height - P_BotSpacer.Height - Padding.Top - TLP_MoreInfo.Height;

				if (Form.WindowState == FormWindowState.Normal && !Form.TopMost)
					lastBounds = Form.Bounds;
			}
			catch { }
		}

		private void PC_Player_Load(object sender, EventArgs e)
		{
			if (Form.WindowState == FormWindowState.Normal)
				lastBounds = Form.Bounds;

			if (Data.Options.FullScreenPlayer)
				ToggleFullscreen(true);
			else
				PC_Player_Resize(null, null);
		}

		private void TLP_MoreInfo_Click(object sender, EventArgs e)
			=> slickScroll.ScrollTo(slickScroll.Percentage.If(0, TLP_MoreInfo, P_VLC), 6);

		private bool pausedFromScroll = false;
		private void slickScroll_Scroll(object sender, ScrollEventArgs e)
		{
			if (Data.Options.AutoPauseOnInfo)
			{
				if (slickScroll.Percentage == 0)
				{
					if (pausedFromScroll && !vlcControl.IsPlaying)
					{
						vlcControl.Play();
						pausedFromScroll = false;
					}
				}
				else
				{
					if (vlcControl.IsPlaying)
					{
						vlcControl.Pause();
						pausedFromScroll = true;
					}
				}
			}

			TLP_MoreInfo.BackColor = slickScroll.Percentage.If(0, FormDesign.Design.BackColor, FormDesign.Design.AccentBackColor);
			L_MoreInfo.Text = slickScroll.Percentage.If(0, "More Info", "Less Info");
			I_MoreInfo.Image = slickScroll.Percentage.If(0, ProjectImages.ArrowDown, ProjectImages.ArrowUp);
		}

		private void setSimilarCharacters(IEnumerable<dynamic> cast)
		{
			TLP_SimilarContent.Controls.Clear();
			TLP_SimilarContent.RowStyles.Clear();

			var showsDic = new Dictionary<Show, List<dynamic>>();
			var moviesDic = new Dictionary<Movie, List<dynamic>>();

			var ind = 0;

			foreach (var c in cast)
			{
				foreach (var item in ShowManager.Shows)
				{
					if (item != Episode?.Show)
					{
						var ct = item.Seasons.FirstThat(y => y.TMDbData.Credits.Cast.Any(z => z.Id == c.Id))?.TMDbData.Credits.Cast.FirstThat(z => z.Id == c.Id)
							?? item.Episodes.FirstThat(y => y.TMDbData.GuestStars.Any(z => z.Id == c.Id))?.TMDbData.GuestStars.FirstThat(z => z.Id == c.Id);

						if (ct != null)
						{
							if (showsDic.ContainsKey(item))
								showsDic[item].Add(ct);
							else
								showsDic.Add(item, new List<dynamic>() { ct });
						}
					}
				}

				foreach (var item in MovieManager.Movies)
				{
					if (item != Movie)
					{
						var ct = item.TMDbData.Credits.Cast.FirstThat(z => z.Id == c.Id);

						if (ct != null)
						{
							if (moviesDic.ContainsKey(item))
								moviesDic[item].Add(ct);
							else
								moviesDic.Add(item, new List<dynamic>() { ct });
						}
					}
				}
			}

			if (showsDic.Count + moviesDic.Count > 0)
			{
				foreach (var item in showsDic)
				{
					TLP_SimilarContent.RowStyles.Add(new RowStyle());

					var flp = new FlowLayoutPanel()
					{
						Dock = DockStyle.Top,
						AutoSize = true,
						AutoSizeMode = AutoSizeMode.GrowOnly
					};

					foreach (var c in item.Value)
						flp.Controls.Add(new CharacterControl(c));

					TLP_SimilarContent.Controls.Add(new ShowTile(item.Key), 0, ind);
					TLP_SimilarContent.Controls.Add(flp, 1, ind);

					ind++;
				}

				foreach (var item in moviesDic)
				{
					TLP_SimilarContent.RowStyles.Add(new RowStyle());

					var flp = new FlowLayoutPanel()
					{
						Dock = DockStyle.Top,
						AutoSize = true,
						AutoSizeMode = AutoSizeMode.GrowOnly
					};

					foreach (var c in item.Value)
						flp.Controls.Add(new CharacterControl(c));

					TLP_SimilarContent.Controls.Add(new MovieTile(item.Key), 0, ind);
					TLP_SimilarContent.Controls.Add(flp, 1, ind);

					ind++;
				}
			}

			TLP_SimilarContent.Visible = SP_Similar.Visible = TLP_SimilarContent.Controls.Count > 0;
		}

		private void P_Progress_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.BackColor);
			e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.ActiveColor), new Rectangle(0, 0, (int)(P_Progress.Width * vlcControl.Time / vlcControl.Length.If(0, int.MaxValue)), P_BotSpacer.Height));
		}

		private void TLP_MoreInfo_MouseEnter(object sender, EventArgs e)
		{
			L_MoreInfo.ForeColor = FormDesign.Design.ActiveColor;
			I_MoreInfo.Color(FormDesign.Design.ActiveColor);
		}

		private void TLP_MoreInfo_MouseLeave(object sender, EventArgs e)
		{
			L_MoreInfo.ForeColor = Color.Empty;
			I_MoreInfo.Color(FormDesign.Design.ForeColor);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}
}
