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
using AudioSwitcher.AudioApi.CoreAudio;
using System.IO;
using TVShowsCalendar.Classes;
using Extensions;
using SlickControls.Forms;
using ProjectImages = TVShowsCalendar.Properties.Resources;
using TVShowsCalendar.HandlerClasses;
using SlickControls.Classes;
using TVShowsCalendar.Forms;

namespace TVShowsCalendar.Panels
{
	public partial class PC_Player : PanelContent
	{
		private AnimationHandler ControlsHideAnimation;
		private WaitIdentifier ControlsHideIdentifier = new WaitIdentifier();
		private bool ControlsOpening = false;
		public Episode Episode { get; private set; }
		public Movie Movie { get; private set; }
		private DateTime lastClick = DateTime.MinValue;
		private Point LastMousePoint = Point.Empty;
		private bool TimeLoop = true;
		private bool firstLoad;

		private PC_Player(FileInfo file)
		{
			InitializeComponent();

			P_VLC.Controls.Add((vlcControl = new Vlc.DotNet.Forms.VlcControl()));

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
			md.MouseMove += (s, e) =>
			{
				if (new Rectangle(vlcControl.PointToScreen(Point.Empty), vlcControl.Size).Contains(e)
					&& LastMousePoint != e)
				{
					ShowPlayControls();
				}
			};

			Disposed += (s, e) => md.Dispose();
		}

		public PC_Player(Episode ep, FileInfo file) : this(file)
		{
			SetEpisode(ep, file);
			SL_Next.Visible = SL_Previous.Visible = true;
		}

		public PC_Player(Movie movie, FileInfo file) : this(file)
		{
			SetMovie(movie, file);
		}

		private void VlcControl_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
		{
			this.TryInvoke(() =>
			{
				ToggleFullscreen(false);
				SL_Play.Image = ProjectImages.Tiny_PlayNoBorder;
				if (Episode != null && (Episode.Next?.VidFile?.Exists ?? false))
					SetEpisode(Episode.Next, play: false);
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
			//ToggleFullscreen(FullScreen);
			ShowPlayControls();
			var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;
			if (vidInf.Width != 0)
				P_VLC.Height = Math.Min(Height - P_BotSpacer.Height - P_Info.Height - Padding.Top, (int)(Width * vidInf.Height / vidInf.Width));
		}

		public void SetMovie(Movie movie, FileInfo epFile = null, bool play = true)
		{
			if (movie != null)
			{
				epFile = epFile != null && epFile.Exists ? epFile : LocalMovieHandler.GetFile(movie);
				if (epFile != null)
				{
					StartLoader();

					Movie = Movie ?? movie;

					this.TryInvoke(() =>
					{
						Text = movie.Title;
						P_BotSpacer.Enabled = false;
						P_Info.Controls.Clear(true);
						P_Info.Controls.Add(new MovieTile(movie, true, true) { Dock = DockStyle.Fill });
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
					this.TryInvoke(() =>
					{
						MessagePrompt.Show($"\"{movie.Title}\" does not seem to have a file associated with it.\n\nCheck that a file exists, or is named correctly", "File Missing", icon: SlickControls.Enums.PromptIcons.Error);
					});
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
				epFile = epFile != null && epFile.Exists ? epFile : LocalShowHandler.GetFile(ep);
				if (epFile != null)
				{
					StartLoader();

					Episode = Episode ?? ep;

					this.TryInvoke(() => 
					{
						Text = $"{ep.Show} • {ep}";
						P_BotSpacer.Enabled = false;
						P_Info.Controls.Clear(true);
						P_Info.Controls.Add(new EpisodeTile(ep, true, true) { Dock = DockStyle.Fill });

						var next = ep.Next;
						if (next != null && next.AirState == AirStateEnum.Aired && !(next.VidFile?.Exists ?? false))
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
							).Show(Data.Dashboard, 20);
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
					this.TryInvoke(() =>
					{
						MessagePrompt.Show($"Episode \"{ep}\" does not seem to have a file associated with it.\n\nCheck that a file exists, or is named correctly", "File Missing", icon: SlickControls.Enums.PromptIcons.Error);
						if (!string.IsNullOrWhiteSpace(ep.Show.ZooqleURL)
							&& DialogResult.Yes == MessagePrompt.Show($"Would you like to open the download window for \"{ep}\" ?", "Open Downloads", SlickControls.Enums.PromptButtons.YesNo, SlickControls.Enums.PromptIcons.Question))
							Data.Dashboard.PushPanel(null, new PC_Download(ep));
					});
			}
		}

		public override bool CanExit()
		{
			SaveWatchtime();

			if (!Paused)
			{
				vlcControl.Pause();
				System.Threading.Thread.Sleep(200);
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
			P_BotSpacer.BackColor = design.BackColor;
			base.DesignChanged(design);
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
						else
						{
							SL_Play_Click(null, null);
							lastClick = DateTime.Now;
						}
					}
				}
			}
			catch { }
		}

		private void ShowPlayControls()
		{
			if (!(FullScreen || (Form?.TopMost ?? false)) || ControlsOpening)
				return;

			if (P_BotSpacer.Height != 65)
			{
				ControlsOpening = true;
				ControlsHideAnimation?.Dispose();
				ControlsHideAnimation = new AnimationHandler(P_BotSpacer, new Size(P_BotSpacer.Width, 65)) { IgnoreWidth = true, SpeedModifier = 5 };
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
			List<FlatStripItem> items = new List<FlatStripItem>()
				{
					new FlatStripItem("More Info", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
						if (Form.TopMost)
							SL_MiniPlayer_Click(null,null);
						if (FullScreen)
							ToggleFullscreen(false);

						if (Episode != null)
							Form.PushPanel(null, new PC_VidInfo(Episode, vlcControl));
						else
							Form.PushPanel(null, new PC_VidInfo(Movie, vlcControl));
					}, image: ProjectImages.Tiny_Info),

					new FlatStripItem("Episode Page", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
						if (Form.TopMost)
							SL_MiniPlayer_Click(null,null);
						if (FullScreen)
							ToggleFullscreen(false);

						Data.Dashboard.PushPanel(null, new PC_ShowPage(Episode));
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

						System.Diagnostics.Process.Start((Episode?.VidFile ?? Movie.VidFile).Directory.FullName);
					}, image: ProjectImages.Tiny_Folder, show: !string.IsNullOrWhiteSpace(Episode.VidFile?.Directory?.FullName)),

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

			new FlatToolStrip(items, Data.Dashboard).ShowUp();
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
				Form.WindowState = lastState;

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
					P_BotSpacer.Parent = this;
					P_BotSpacer.BringToFront();
					ControlsHideAnimation?.StopAnimation();
					P_BotSpacer.Dock = DockStyle.Top;
					if (P_BotSpacer.Height != 65)
						BeginInvoke(new Action(() => { P_BotSpacer.Height = 65; PC_Player_Resize(null, null); }));
				}

				SL_FullScreen.Image = ProjectImages.Tiny_Fullscreen;
			}

			SS_Volume.Visible = SL_FullScreen.Visible = !Form.TopMost;
			SL_Previous.Visible = SL_Next.Visible = !Form.TopMost && Episode != null;
			TLP_Buttons.ColumnStyles[2].Width = TLP_Buttons.ColumnStyles[TLP_Buttons.ColumnCount - 2].Width = Form.TopMost ? 0 : 45;

			ShowPlayControls();
		}

		private void SL_Backwards_Click(object sender, EventArgs e)
		{
			vlcControl.Time -= Data.Options.BackwardTime * 1000;
		}

		private void SL_Forward_Click(object sender, EventArgs e)
		{
			vlcControl.Time += Data.Options.ForwardTime * 1000;
		}

		private void SL_Next_Click(object sender, EventArgs e)
		{
			SetEpisode(Episode.Next);
		}

		private void SL_Previous_Click(object sender, EventArgs e)
		{
			SetEpisode(Episode.Previous);
		}

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

		private void vlcControl_Paused(object sender, Vlc.DotNet.Core.VlcMediaPlayerPausedEventArgs e)
		{
			this.TryInvoke(() => SL_Play.Image = ProjectImages.Tiny_PlayNoBorder);
		}

		private void vlcControl_Playing(object sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
		{
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
				SS_TimeSlider.Value = vlcControl.Time;
				if (Episode != null)
					Episode.Progress = 100 * vlcControl.Time / vlcControl.Length;
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
				new FlatToolStrip(subs, Data.Dashboard).ShowUp();
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
				new FlatToolStrip(tracks, Data.Dashboard).ShowUp();
		}

		private void SS_Volume_ValuesChanged(object sender, EventArgs e)
		{
			vlcControl.Audio.Volume = (int)SS_Volume.Value;
		}

		private void SL_MiniPlayer_Click(object sender, EventArgs e)
		{
			var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;

			if (Form.TopMost)
			{
				Form.TopMost = false;
				ToggleFullscreen(false);
				SL_MiniPlayer.Image = ProjectImages.Tiny_MiniWindow;

				Form.Bounds = lastBounds;
				Form.MinimumSize = new Size(765, 445);
			}
			else
			{
				Form.TopMost = true;
				ToggleFullscreen(false);
				Form.WindowState = FormWindowState.Normal;
				SL_MiniPlayer.Image = ProjectImages.Tiny_RestoreWindow;

				var h = 280;
				Form.MinimumSize = new Size((int)(150 * vidInf.Width / vidInf.Height) + Form.Padding.Horizontal, 150);
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
				var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;
				if (vidInf.Width != 0)
					P_VLC.Height = Math.Min(Height - P_BotSpacer.Height - P_Info.Height - Padding.Top, (int)(Width * vidInf.Height / vidInf.Width));
				else
					P_VLC.Height = Height - P_BotSpacer.Height - P_Info.Height - Padding.Top;

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
	}
}
