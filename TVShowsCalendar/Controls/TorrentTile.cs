using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using SlickControls.Panels;
using ShowsCalendar.Classes;

namespace ShowsCalendar
{
	public partial class TorrentTile : SlickControls.Controls.SlickControl
	{
		#region Public Fields

		public bool Canceled = false;

		#endregion Public Fields

		#region Private Fields

		private string magnet;
		private int SeedOrder;
		private string URL;

		#endregion Private Fields

		#region Public Properties

		public SortingData SortingData { get; }

		#endregion Public Properties

		#region Public Constructors

		public TorrentTile(Match match, int seedOrder)
		{
			InitializeComponent();
			SeedOrder = seedOrder;
			URL = "https://zooqle.com" + Regex.Match(match.Value, "href ?=\"(.+?)\"").Groups[1].Value;
			L_Size.Text = Regex.Match(match.Value, @"[\d\.]+ ?[TGMK]B").Value;
			L_Sound.Text = Regex.Match(match.Value, @"<span.+?Audio.+?</i>(.+?)</span>").Groups[1].Value;
			L_Subs.Text = Regex.Match(match.Value, @"<span.+?language.+?>(.+?)</span>").Groups[1].Value.ToUpper().RegexReplace(",", ", ");
			L_Name.Text = Regex.Match(match.Value, @"<a.+?>(.+?)</a>").Groups[1].Value.RegexRemove("</?hl>");
			magnet = Regex.Match(match.Value, "<a title=\"Magnet link\" (?:rel=\"nofollow\" )?href=\"(.+?)\"").Groups[1].Value;
			var seeders =  Regex.Match(match.Value, "title=\"seeders.+?(\\d+).+?(\\d+)\"", RegexOptions.IgnoreCase);
			var health = (30 * Math.Log(((seeders.Groups[1].Value.SmartParseD() - seeders.Groups[2].Value.SmartParseD() + 4) / 4) + (seeders.Groups[1].Value.SmartParseD() / 2), Math.E)).Between(0, 100);

			PB_Signal.Image = health.If(x => x < 25, Properties.Resources.Tiny_Signal_0, health.If(x => x < 50, Properties.Resources.Tiny_Signal_1, health.If(x => x < 75, Properties.Resources.Tiny_Signal_2, Properties.Resources.Tiny_Signal_3)));
			SlickTip.SetTo(PB_Signal, $"{seeders.Groups[1].Value} Seeders | {seeders.Groups[2].Value} Leechers");

			switch (Regex.Match(match.Value, "<span class=\"text-nowrap smallest trans90 text-muted hidden-md hidden-xs\">.+? (\\w+?)</span>").Groups[1].Value)
			{
				case "Std": L_Quality.Text = "Low"; break;
				case "Med": L_Quality.Text = "Low"; break;
				case "Ultra": L_Quality.Text = "4K Ultra"; break;
				default:
					L_Quality.Text = Regex.Match(match.Value, "<span class=\"text-nowrap smallest trans90 text-muted hidden-md hidden-xs\">.+? (\\w+?)</span>").Groups[1].Value;
					break;
			}

			if (L_Quality.Text == "")
			{
				L_Quality.Text = FindQuality(match.Value);
				if (L_Name.Text.Length < 5)
				{ Canceled = true; Dispose(); return; }
			}

			SortingData = GetSort(health);

			foreach (var item in TableLayout.Controls.ThatAre<Label>())
				SlickTip.SetTo(item, item.Text);
			SlickTip.SetTo(B_Download, "Download this torrent using a torrent app on your computer");
		}

		#endregion Public Constructors

		#region Protected Methods

		protected override void DesignChanged(FormDesign design)
		{
			L_Name.ForeColor = design.LabelColor;
			ForeColor = design.InfoColor;
			B_Download.ColorShade = design.GreenColor;
			PB_Signal.Color(SortingData.Health.If(x => x < 25, design.IconColor, SortingData.Health.If(x => x < 50, design.RedColor, SortingData.Health.If(x => x < 75, design.YellowColor, design.GreenColor))));
			if (GeneralMethods.QualityDicTxt2Id.ContainsKey(L_Quality.Text) && GeneralMethods.QualityDicTxt2Id[L_Quality.Text] == Data.Options.PrefferedQuality)
			{ L_Quality.ForeColor = design.ActiveColor; }
		}

		#endregion Protected Methods

		#region Private Methods

		private void B_Download_Click(object sender, EventArgs e)
		{
			var notif = Notification.Create(PaintNotification, null)
								.Show(Data.Mainform.Visible.If(Data.Mainform, null));

			notif.PictureBox.Image = FormDesign.Loader;
			notif.PictureBox.Text = L_Name.Text;

			new Action(async () =>
			{
				try
				{
					if (string.IsNullOrEmpty(magnet))
					{
						var html = await ShowAnalyzer.GetHTML(URL);
						magnet = Regex.Match(html, "text-nowrap.{1,10}href=\"(magnet:\\?xt=urn:btih:.+?)\"").Groups[1].Value;
					}

					System.Diagnostics.Process.Start(magnet.IfEmpty(URL));

					notif.TryInvoke(notif.Dispose);
				}
				catch
				{
					var frm = (ParentForm is BasePanelForm form) ? form : null;
					if (magnet == "")
						MessagePrompt.Show("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error, frm);
					else
						MessagePrompt.Show("Something went wrong while trying to open the magnet link.\n\nCheck that you have a default browser set up and that you have an application to download torrents with.", "Something Went Wrong", PromptButtons.OK, PromptIcons.Error, frm);
				}
			}).RunInBackground();

			if (Data.Options.DownloadBehavior)
			{
				if (ParentForm is BasePanelForm form)
					form.PushBack();
				else
					ParentForm.Close();
			}
			else
			{
				B_Download.Image = Properties.Resources.Tiny_Download;
				B_Download.Click += B_Download_Click;
				B_Download.Cursor = Cursors.Hand;
			}
		}

		private string FindQuality(string match)
		{
			if (match.Contains("3D"))
				return "3D";
			if (match.Contains("4K") || match.Contains("UHD"))
				return "4K Ultra";
			if (match.Contains("1080p"))
				return "1080p";
			if (match.Contains("720p"))
				return "720p";
			return "Low";
		}

		private SortingData GetSort(double health)
		{
			var data = new SortingData
			{
				Tile = this,
				SeedOrder = SeedOrder,
				Name = L_Name.Text,
				Subs = L_Subs.Text,
				Sound = (int)(L_Sound.Text.SmartParseD() * 10),
				Health = health
			};

			switch (L_Quality.Text)
			{
				case "3D": data.Res = 6; break;
				case "4K Ultra": data.Res = 5; break;
				case "Ultra": data.Res = 5; break;
				case "1080p": data.Res = 4; break;
				case "720p": data.Res = 3; break;
				case "Std": data.Res = 2; break;
				case "Med": data.Res = 1; break;
				default: data.Res = 0; break;
			}

			var s = L_Size.Text.SmartParseD();
			switch (Regex.Match(L_Size.Text, "[TKMG]B").Value)
			{
				case "TB": data.Size = (int)(s * Math.Pow(1024, 3)); break;
				case "GB": data.Size = (int)(s * Math.Pow(1024, 2)); break;
				case "MB": data.Size = (int)(s * Math.Pow(1024, 1)); break;
				case "KB": data.Size = (int)(s * Math.Pow(1024, 0)); break;
				default: data.Size = 0; break;
			}

			return data;
		}

		private void L_Name_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			try { System.Diagnostics.Process.Start(URL); }
			catch (Exception) { Cursor.Current = Cursors.Default; MessagePrompt.Show("Could not open link because you do not have a default Web Browser Selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
			Cursor.Current = Cursors.Default;
		}

		private void PaintNotification(PictureBox pb, Graphics g)
		{
			g.DrawImage(pb.Image, 8, (pb.Height - 22) / 2, 22, 22);

			g.DrawString("Loading Download", new Font("Nirmala UI", 9.75F), new SolidBrush(FormDesign.Design.ForeColor), 36, 4);

			g.DrawString($"Getting the download information for `{pb.Text}`", new Font("Nirmala UI", 8.25F), new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(39, 26, pb.Width - 50, pb.Height - 29), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}

		#endregion Private Methods
	}
}