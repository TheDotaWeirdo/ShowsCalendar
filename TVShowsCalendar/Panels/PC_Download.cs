using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Extensions;
using SlickControls.Forms;
using SlickControls.Panels;
using TVShowsCalendar.Classes;
using TVShowsCalendar.HandlerClasses;

namespace TVShowsCalendar.Panels
{
	public partial class PC_Download : PanelContent
	{
		#region Private Fields

		private DisableIdentifier QualityChangeIdentifier = new DisableIdentifier();

		private bool Reversed;

		private PictureBox selectedLabel;

		private PictureBox selectedSort;

		#endregion Private Fields

		#region Public Properties

		public Episode Episode { get; }

		public Movie Movie { get; }

		public Season Season { get; }

		public PictureBox SelectedSort
		{
			get => selectedSort;
			set
			{
				selectedSort?.Color(FormDesign.Design.IconColor);
				selectedSort = value;
				value?.Color(FormDesign.Design.ActiveColor);
			}
		}

		#endregion Public Properties

		#region Public Constructors

		public PC_Download(Episode episode)
		{
			InitializeComponent();
			Episode = episode;
			Text = Episode.Show.Name;

			if (!string.IsNullOrWhiteSpace(episode.Show.ZooqleURL))
				InitializeQuality();
			else
			{
				LoadTorrentsFailover(new[]
				{
					$"https://zooqle.com/search?q={GetHttp(episode.Show.Name)}+S{episode.SN.ToString("00")}E{episode.EN.ToString("00")}+category%3ATV",
					$"https://zooqle.com/search?q={GetHttp(episode.Show.Name)}+{GetHttp(episode.Name)}+category%3ATV",
				});
				Select(selectedLabel = L_AllDownloads);
				L_Low.Cursor = L_720p.Cursor = L_1080p.Cursor = L_4K.Cursor = L_3D.Cursor = Cursors.Default;
			}

			PB_Image.GetImage(episode.TMDbData.StillPath.IfEmpty(episode.Show.TMDbData.BackdropPath), 200);

			verticalScroll.LinkedControl = TLP_Torrents;

			SlickTip.SetTo(L_Low, "Only show Low quality torrents");
			SlickTip.SetTo(L_720p, "Only show 720p quality torrents");
			SlickTip.SetTo(L_1080p, "Only show 1080p quality torrents");
			SlickTip.SetTo(L_4K, "Only show 4K UHD quality torrents");
			SlickTip.SetTo(L_3D, "Only show 3D quality torrents");
			SlickTip.SetTo(L_AllDownloads, "Show all torrents");

			SlickTip.SetTo(PB_Label, "Sort by Name");
			SlickTip.SetTo(PB_Res, "Sort by Quality");
			SlickTip.SetTo(PB_Subs, "Sort by Subtitles");
			SlickTip.SetTo(PB_Sound, "Sort by Sound Quality");
			SlickTip.SetTo(PB_Size, "Sort by Download Size");
		}

		public PC_Download(Season season)
		{
			InitializeComponent();
			Season = season;
			Text = Season.TMDbData.Name;

			if (!string.IsNullOrWhiteSpace(season.Show.ZooqleURL))
				InitializeQuality();
			else
			{
				LoadTorrents(new[]
				{
					$"https://zooqle.com/search?q={GetHttp(season.Show.Name)}+Season+{season.SeasonNumber.ToString("00")}+category%3ATV",
					$"https://zooqle.com/search?q={GetHttp(season.Show.Name)}+Season+{season.SeasonNumber}+category%3ATV",
				});
				Select(selectedLabel = L_AllDownloads);
				L_Low.Cursor = L_720p.Cursor = L_1080p.Cursor = L_4K.Cursor = L_3D.Cursor = Cursors.Default;
			}

			PB_Image.GetImage(season.TMDbData.PosterPath.IfEmpty(season.Show.TMDbData.PosterPath), 200);

			verticalScroll.LinkedControl = TLP_Torrents;

			SlickTip.SetTo(L_Low, "Only show Low quality torrents");
			SlickTip.SetTo(L_720p, "Only show 720p quality torrents");
			SlickTip.SetTo(L_1080p, "Only show 1080p quality torrents");
			SlickTip.SetTo(L_4K, "Only show 4K UHD quality torrents");
			SlickTip.SetTo(L_3D, "Only show 3D quality torrents");
			SlickTip.SetTo(L_AllDownloads, "Show all torrents");

			SlickTip.SetTo(PB_Label, "Sort by Name");
			SlickTip.SetTo(PB_Res, "Sort by Quality");
			SlickTip.SetTo(PB_Subs, "Sort by Subtitles");
			SlickTip.SetTo(PB_Sound, "Sort by Sound Quality");
			SlickTip.SetTo(PB_Size, "Sort by Download Size");
		}

		public PC_Download(Movie mov)
		{
			InitializeComponent();
			Movie = mov;
			Text = Movie.Name;

			var url = $"https://zooqle.com/search?q={GetHttp(mov.Name)}+category%3AMovies";
			LoadTorrentsFailover(new[] { url });
			Select(selectedLabel = L_AllDownloads);
			L_Low.Cursor = L_720p.Cursor = L_1080p.Cursor = L_4K.Cursor = L_3D.Cursor = Cursors.Default;

			PB_Image.GetImage(mov.TMDbData.BackdropPath, 200);

			verticalScroll.LinkedControl = TLP_Torrents;

			SlickTip.SetTo(L_Low, "Only show Low quality torrents");
			SlickTip.SetTo(L_720p, "Only show 720p quality torrents");
			SlickTip.SetTo(L_1080p, "Only show 1080p quality torrents");
			SlickTip.SetTo(L_4K, "Only show 4K UHD quality torrents");
			SlickTip.SetTo(L_3D, "Only show 3D quality torrents");
			SlickTip.SetTo(L_AllDownloads, "Show all torrents");

			SlickTip.SetTo(PB_Label, "Sort by Name");
			SlickTip.SetTo(PB_Res, "Sort by Quality");
			SlickTip.SetTo(PB_Subs, "Sort by Subtitles");
			SlickTip.SetTo(PB_Sound, "Sort by Sound Quality");
			SlickTip.SetTo(PB_Size, "Sort by Download Size");
		}

		#endregion Public Constructors

		#region Protected Methods

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			P_Spacer_1.BackColor = P_Spacer_2.BackColor = P_Spacer_3.BackColor = design.AccentColor;
			L_SelectedQuality.ForeColor = design.LabelColor;

			if (selectedLabel != null)
				Select(selectedLabel);

			L_NoResults.ForeColor = design.LabelColor;

			PB_Loader.Image = FormDesign.Loader;

			if (L_Low.Tag == null)
				L_Low.Color(design.ForeColor.Tint(Lum: design.Type.If(FormDesignType.Dark, -30, 30)));
			else
				L_Low.Color(design.IconColor);

			if (L_720p.Tag == null)
				L_720p.Color(design.ForeColor.Tint(Lum: design.Type.If(FormDesignType.Dark, -30, 30)));
			else
				L_720p.Color(design.IconColor);

			if (L_1080p.Tag == null)
				L_1080p.Color(design.ForeColor.Tint(Lum: design.Type.If(FormDesignType.Dark, -30, 30)));
			else
				L_1080p.Color(design.IconColor);

			if (L_4K.Tag == null)
				L_4K.Color(design.ForeColor.Tint(Lum: design.Type.If(FormDesignType.Dark, -30, 30)));
			else
				L_4K.Color(design.IconColor);

			if (L_3D.Tag == null)
				L_3D.Color(design.ForeColor.Tint(Lum: design.Type.If(FormDesignType.Dark, -30, 30)));
			else
				L_3D.Color(design.IconColor);

			PB_Label.Color(design.IconColor);
			PB_Subs.Color(design.IconColor);
			PB_Sound.Color(design.IconColor);
			PB_Size.Color(design.IconColor);
			PB_Res.Color(design.IconColor);
			PB_Download.Color(design.IconColor);
			if (SelectedSort != null)
				SelectedSort.Color(design.ActiveColor);
		}

		#endregion Protected Methods

		#region Private Methods

		private static string GetHttp(string s)
							=> System.Web.HttpUtility.UrlEncode(s.RemoveDoubleSpaces().Replace(' ', '+'));

		private void Deselect(PictureBox label)
		{
			label.Color(FormDesign.Design.IconColor);
			label.Invalidate();
		}

		private void InitializeQuality()
		{
			new Action(async () =>
			{
				try
				{
					var html = await ShowAnalyzer.GetHTML(Episode.ZooqleURL + "?tg=");
					html = Regex.Match(html, "Quality:.+?</ul>").Value;
					Invoke(new Action(() =>
					{
						L_Low.Tag = L_720p.Tag = L_1080p.Tag = L_4K.Tag = L_3D.Tag = null;

						foreach (var item in Regex.Matches(html, "<a.+?>(.+?)(?:&nbsp;)?(<span.+?>(\\d+)</span>)?</a>"))
						{
							if ((item as Match).Groups[2].Value != "")
							{
								switch ((item as Match).Groups[1].Value)
								{
									case "Low": L_Low.Tag = true; break;
									case "Std": L_Low.Tag = true; break;
									case "Med": L_Low.Tag = true; break;
									case "720p": L_720p.Tag = true; break;
									case "1080p": L_1080p.Tag = true; break;
									case "Ultra": L_4K.Tag = true; break;
									case "3D": L_3D.Tag = true; break;
									default:
										break;
								}
							}
						}

						if (L_Low.Tag != null)
							L_Low.Click += Quality_Click;
						else
							L_Low.Cursor = Cursors.Default;

						if (L_720p.Tag != null)
							L_720p.Click += Quality_Click;
						else
							L_720p.Cursor = Cursors.Default;

						if (L_1080p.Tag != null)
							L_1080p.Click += Quality_Click;
						else
							L_1080p.Cursor = Cursors.Default;

						if (L_4K.Tag != null)
							L_4K.Click += Quality_Click;
						else
							L_4K.Cursor = Cursors.Default;

						if (L_3D.Tag != null)
							L_3D.Click += Quality_Click;
						else
							L_3D.Cursor = Cursors.Default;

						var qual = (PictureBox)null;
						switch (Data.Options.PrefferedQuality)
						{
							case 1: qual = L_Low; break;
							case 5: qual = L_720p; break;
							case 7: qual = L_1080p; break;
							case 9: qual = L_4K; break;
							case 11: qual = L_3D; break;
							default:
								break;
						}

						if (!Data.Options.ShowAllDownloads && (qual?.Enabled ?? false))
						{
							LoadTorrents(new List<string> { Episode.ZooqleURL + "?tg=" + Data.Options.PrefferedQuality });

							Select(selectedLabel = qual);
						}
						else
						{
							LoadTorrents(new List<string> { Episode.ZooqleURL });
							Select(selectedLabel = L_AllDownloads);
						}

						DesignChanged(FormDesign.Design);
					}));
				}
				catch { }
			}).RunInBackground();
		}

		private void LoadTorrents(IEnumerable<string> urls)
		{
			PB_Loader.Show();
			L_NoResults.Hide();
			var SeedOrder = 0;
			var urlsDone = urls.Count();

			foreach (var URL in urls)
			{
				new Action(async () =>
				{
					var html = await ShowAnalyzer.GetHTML(URL);
					this.TryInvoke(() =>
					{
						urlsDone--;
						TLP_Torrents.SuspendDrawing();
						foreach (Match item in Regex.Matches(html, "<tr( \"=\"\")?>.+?22.+?\\d+?\\..+?</tr>"))
						{
							var tf = new TorrentTile(item, SeedOrder) { Dock = DockStyle.Fill };
							try
							{
								if (!tf.Canceled)
								{
									TLP_Torrents.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
									TLP_Torrents.Controls.Add(tf);
									tf.Show();
									SeedOrder++;
								}
							}
							catch (ObjectDisposedException) { }
						}
						TLP_Torrents.ResumeDrawing();

						if (TLP_Torrents.Controls.Count != 0)
						{
							PB_Loader.Hide();
							TLP_QualitySelection.Enabled = TLP_Icons.Enabled = true;
						}
						else if (urlsDone == 0)
						{
							L_NoResults.Show();
							PB_Loader.Hide();

							QualityChangeIdentifier.Enable();
						}
					});
				}).RunInBackground();
			}
		}

		private void LoadTorrentsFailover(IEnumerable<string> urls)
		{
			PB_Loader.Show();
			L_NoResults.Hide();
			var SeedOrder = 0;

			new Action(async () =>
			{
				var html = await ShowAnalyzer.GetHTML(urls.First());
				this.TryInvoke(() =>
				{
					TLP_Torrents.SuspendDrawing();
					foreach (Match item in Regex.Matches(html, "<tr( \"=\"\")?>.+?22.+?\\d+?\\..+?</tr>"))
					{
						var tf = new TorrentTile(item, SeedOrder) { Dock = DockStyle.Fill };
						try
						{
							if (!tf.Canceled)
							{
								TLP_Torrents.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
								TLP_Torrents.Controls.Add(tf);
								tf.Show();
								SeedOrder++;
							}
						}
						catch (ObjectDisposedException) { }
					}
					TLP_Torrents.ResumeDrawing();

					if (TLP_Torrents.Controls.Count != 0)
					{
						PB_Loader.Hide();
						TLP_QualitySelection.Enabled = TLP_Icons.Enabled = true;
					}
					else if (urls.Count() > 1)
					{
						LoadTorrentsFailover(urls.Skip(1));
					}
					else
					{
						L_NoResults.Show();
						PB_Loader.Hide();

						QualityChangeIdentifier.Enable();
					}
				});
			}).RunInBackground();
		}

		private void PB_Image_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(PB_Image.BackColor);

			e.Graphics.DrawBorderedImage(PB_Image.Image, new Rectangle(10, 5, 160, 90), PB_Image.Image.IsAnimated() ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Stretch);

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			var h = 5;
			var w = 175;

			e.Graphics.DrawString(Episode == null ? (Movie == null ? Season.TMDbData.Name : Movie.Name) : Episode.Name, new Font("Nirmala UI", 9.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), w, h);

			var bnds = e.Graphics.MeasureString(Episode == null ? (Movie == null ? Season.TMDbData.Name : Movie.Name) : Episode.Name, new Font("Nirmala UI", 9.75F, FontStyle.Bold));
			h += (int)bnds.Height + 2;

			e.Graphics.DrawString(Episode == null ? (Movie == null ? $"{Season.Episodes.Count} Episodes" : Movie.TMDbData.Tagline) : $"Season {Episode.TMDbData.SeasonNumber} • Episode {Episode.TMDbData.EpisodeNumber}", new Font("Nirmala UI", 9), new SolidBrush(FormDesign.Design.LabelColor), w, h);

			bnds = e.Graphics.MeasureString(Episode == null ? (Movie == null ? $"{Season.Episodes.Count} Episodes" : Movie.TMDbData.Tagline) : $"Season {Episode.TMDbData.SeasonNumber} • Episode {Episode.TMDbData.EpisodeNumber}", new Font("Nirmala UI", 9));
			h += (int)bnds.Height + 2;
			w++;

			e.Graphics.DrawString((Episode == null ? (Movie == null ? Season.TMDbData.Overview : Movie.tMDbData.Overview) : Episode.TMDbData.Overview).IfEmpty("No overview"), new Font("Nirmala UI", 6.75F), new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, h, PB_Image.Width - w - 5, PB_Image.Height - h - 5), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}

		private void PB_Label_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right;
			if (SelectedSort == PB_Label && r == Reversed)
			{
				SelectedSort = null;
				Reversed = false;
				SortBy(SortOption.None);
			}
			else
			{
				Reversed = r;
				SelectedSort = PB_Label;
				SortBy(SortOption.Name);
			}
		}

		private void PB_Label_MouseEnter(object sender, EventArgs e)
		{
			if (sender != SelectedSort)
				(sender as PictureBox).Image = ((sender as PictureBox).Image as Bitmap).Color(FormDesign.Design.LabelColor);
		}

		private void PB_Label_MouseLeave(object sender, EventArgs e)
		{
			if (sender != SelectedSort)
				(sender as PictureBox).Image = ((sender as PictureBox).Image as Bitmap).Color(FormDesign.Design.IconColor);
		}

		private void PB_Res_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right;
			if (SelectedSort == PB_Res && r == Reversed)
			{
				SelectedSort = null;
				Reversed = false;
				SortBy(SortOption.None);
			}
			else
			{
				Reversed = r;
				SelectedSort = PB_Res;
				SortBy(SortOption.Res);
			}
		}

		private void PB_Size_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right;
			if (SelectedSort == PB_Size && r == Reversed)
			{
				SelectedSort = null;
				Reversed = false;
				SortBy(SortOption.None);
			}
			else
			{
				Reversed = r;
				SelectedSort = PB_Size;
				SortBy(SortOption.Size);
			}
		}

		private void PB_Sound_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right;
			if (SelectedSort == PB_Sound && r == Reversed)
			{
				SelectedSort = null;
				Reversed = false;
				SortBy(SortOption.None);
			}
			else
			{
				Reversed = r;
				SelectedSort = PB_Sound;
				SortBy(SortOption.Sound);
			}
		}

		private void PB_Subs_Click(object sender, EventArgs e)
		{
			var r = (e as MouseEventArgs).Button == MouseButtons.Right;
			if (SelectedSort == PB_Subs && r == Reversed)
			{
				SelectedSort = null;
				Reversed = false;
				SortBy(SortOption.None);
			}
			else
			{
				Reversed = r;
				SelectedSort = PB_Subs;
				SortBy(SortOption.Subs);
			}
		}

		private void pictureBox1_MouseEnter(object sender, EventArgs e)
		{
			if (sender != selectedLabel && (sender as PictureBox).Tag != null)
				(sender as PictureBox).Image = ((sender as PictureBox).Image as Bitmap).Color(FormDesign.Design.LabelColor);
		}

		private void pictureBox1_MouseLeave(object sender, EventArgs e)
		{
			if (sender != selectedLabel && (sender as PictureBox).Tag != null)
				(sender as PictureBox).Image = ((sender as PictureBox).Image as Bitmap).Color(FormDesign.Design.IconColor);
		}

		private void Quality_Click(object sender, EventArgs e)
		{
			if (QualityChangeIdentifier.Disabled || selectedLabel == sender || string.IsNullOrEmpty(Episode?.ZooqleURL))
				return;

			Cursor = Cursors.WaitCursor;
			QualityChangeIdentifier.Disable();
			if (selectedLabel != null)
				Deselect(selectedLabel);
			selectedLabel = (PictureBox)sender;
			Select((PictureBox)sender);

			var links = new List<string>();
			if (sender == L_AllDownloads)
				links.Add(Episode.ZooqleURL);
			else if (sender == L_3D)
				links.Add(Episode.ZooqleURL + "?tg=11");
			else if (sender == L_4K)
				links.Add(Episode.ZooqleURL + "?tg=9");
			else if (sender == L_1080p)
				links.Add(Episode.ZooqleURL + "?tg=7");
			else if (sender == L_720p)
				links.Add(Episode.ZooqleURL + "?tg=5");
			else if (sender == L_Low)
				links.AddRange(new string[] { Episode.ZooqleURL + "?tg=3", Episode.ZooqleURL + "?tg=2", Episode.ZooqleURL + "?tg=1" });

			TLP_Torrents.SuspendDrawing();
			TLP_Torrents.Controls.Clear(true);
			TLP_Torrents.ResumeDrawing();
			SelectedSort = null;
			LoadTorrents(links);
			Cursor = Cursors.Default;
		}

		private void Select(PictureBox label)
		{
			label.Color(FormDesign.Design.ActiveColor);
		}

		private void SetTooltips()
		{
			SlickTip.SetTo(L_Low, "Show Low quality downloads only");
			SlickTip.SetTo(L_720p, "Show 720p quality downloads only");
			SlickTip.SetTo(L_1080p, "Show 1080p quality downloads only");
			SlickTip.SetTo(L_4K, "Show 4K Ultra quality downloads only");
			SlickTip.SetTo(L_3D, "Show 3D quality downloads only");
			SlickTip.SetTo(L_AllDownloads, "Show the Top downloads from all video qualities");
			SlickTip.SetTo(PB_Download, "Download");
			SlickTip.SetTo(PB_Size, "Sort by Size");
			SlickTip.SetTo(PB_Res, "Sort by Video Resolution");
			SlickTip.SetTo(PB_Sound, "Sort by Sound Channels");
			SlickTip.SetTo(PB_Subs, "Sort by Subtitles");
			SlickTip.SetTo(PB_Label, "Sort by Torrent Name");
		}

		private void SortBy(SortOption sortOption)
		{
			var torrentData = new List<SortingData>();
			foreach (TorrentTile item in TLP_Torrents.Controls)
				torrentData.Add(item.SortingData);

			if (sortOption == SortOption.Name)
				torrentData = torrentData.OrderByDescending(x => x.Name).ToList();
			else if (sortOption == SortOption.Subs)
				torrentData = torrentData.OrderBy(x => x.Subs).ToList();
			else if (sortOption == SortOption.Sound)
				torrentData = torrentData.OrderBy(x => x.Sound).ToList();
			else if (sortOption == SortOption.Res)
				torrentData = torrentData.OrderBy(x => x.Res).ToList();
			else if (sortOption == SortOption.Size)
				torrentData = torrentData.OrderBy(x => x.Size).ToList();
			else
				torrentData = torrentData.OrderByDescending(x => x.SeedOrder).ToList();

			if (Reversed)
				torrentData.Reverse();

			TLP_Torrents.SuspendDrawing();
			foreach (var item in torrentData)
				item.Tile.BringToFront();
			TLP_Torrents.ResumeDrawing();
		}

		private void TLP_Torrents_Resize(object sender, EventArgs e)
		{
			TLP_Torrents.MaximumSize = new Size(panel1.Width, 9999);
			TLP_Torrents.MinimumSize = new Size(panel1.Width, 0);
		}

		#endregion Private Methods
	}
}