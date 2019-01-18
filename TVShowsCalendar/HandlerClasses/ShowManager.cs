using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions;
using SlickControls.Classes;
using SlickControls.Enums;
using SlickControls.Forms;
using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;
using TVShowsCalendar.Classes;
using TVShowsCalendar.Forms;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Panels;

namespace TVShowsCalendar
{
	public static class ShowManager
	{
		public delegate void ShowChangeHandler(Show show);

		public static List<Show> Shows = new List<Show>();
		public static event ShowChangeHandler ShowLoaded;
		public static event ShowChangeHandler ShowAdded;
		public static event ShowChangeHandler ShowRemoved;

		public static Episode GetEpisode(int id)
			=> Shows.ConvertEnumerable(x => x.Seasons).ConvertEnumerable(x => x.Episodes).FirstThat(x => x.TMDbData.Id == id);

		public static Episode GetNextEpisode(Episode episode)
		{
			var found = false;

			foreach (var item in episode.Show.Seasons.ConvertEnumerable(x => x.Episodes))
			{
				if (found)
					return item;

				if (item == episode)
					found = true;
			}

			return null;
		}

		public static void Add(Show show, bool save = true)
		{
			if (!Shows.Contains(show) && show.Id != 0)
			{
				Shows.Add(show);
				ShowAdded?.Invoke(show);
				show.ShowLoaded += ShowLoadedMethod;
			}

			if (save && show.Id != 0)
				Save(show);
		}

		public static void Remove(Show show)
		{
			if (DialogResult.No == MessagePrompt.Show($"Are you sure you want to remove {show.Name}?", "Remove Show?", PromptButtons.YesNo, PromptIcons.Hand, Data.Dashboard))
				return;

			start: try
			{
				show.Delete();
				Shows.Remove(show);
				show.ShowLoaded -= ShowLoadedMethod;
				ShowRemoved?.Invoke(show);
			}
			catch
			{
				if (DialogResult.Retry == MessagePrompt.Show($"Failed to delete {show.Name}, would you like to try again?", "Failed to Delete Show", PromptButtons.RetryCancel, PromptIcons.Warning, Data.Dashboard))
					goto start;
			}
		}

		private static WaitIdentifier RemindLogIdentifier = new WaitIdentifier();

		private static void ShowLoadedMethod(Show show)
		{
			ShowLoaded?.Invoke(show);

			if (RemindLogIdentifier.Enabled && Shows.All(x => x.Loaded))
			{
				RemindLogIdentifier.Wait(() =>
				{
					ISave.Load(out Dictionary<string, DateTime> remindLog, "RemindLog.tf");

					var eps = Shows.Convert(x => x.LastEpisode)
						.Where(x => x != null
							&& !x.Empty
							&& x.TMDbData.AirDate > DateTime.Today.AddDays(-7)
							&& !remindLog.ContainsKey($"E{x.TMDbData.Id}")
							&& Data.Options.EpisodeNotification
							&& !x.VidFile?.Exists == null);

					foreach (var ep in eps)
					{
						Data.Dashboard.TryInvoke(() =>
						{
							Notification.Create(
								(f, x) => PaintEpNotification(f, ep, x)
								, () => { Data.Dashboard.ShowUp(); Data.Dashboard.PushPanel(null, new PC_Download(ep)); }
								, true
								, new Size(400, 80))
								.Show(Data.Dashboard)
								.PictureBox.GetImage(ep.TMDbData.StillPath.IfEmpty(ep.Show.TMDbData.BackdropPath), 200);
						});
						remindLog.Add($"E{ep.TMDbData.Id}", DateTime.Now);
					}

					ISave.Save(remindLog.Where(x => x.Value > DateTime.Now.AddDays(-60)).AsDictionary(), "RemindLog.tf");
				}, 5000);
			}
		}

		private static void PaintEpNotification(PictureBox pb, Episode ep, Graphics g)
		{
			var Width = pb.Width;
			var Height = pb.Height;

			if (pb.Image != null)
				g.DrawBorderedImage(pb.Image, new Rectangle(3, 2, (Height - 5) * 16 / 9, Height - 5), pb.Image.IsAnimated() || pb.Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				g.DrawBorderedImage(Properties.Resources.Big_TV.Color(FormDesign.Design.IconColor), new Rectangle(3, 2, (Height - 5) * 16 / 9, Height - 5), ImageHandler.ImageSizeMode.Center);

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			g.FillEllipse(new SolidBrush(Color.FromArgb(150, FormDesign.Design.ActiveColor)), new RectangleF(7, 6, 22, 22));
			g.DrawImage(Properties.Resources.Tiny_CloudDownload.Color(Color.FromArgb(175, FormDesign.Design.ActiveForeColor)), 10, 8);

			var strFrmt = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
			var font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
			var w = ((Height - 5) * 16 / 9) + 5;
			var h = 3;

			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			var bnds = g.MeasureString(ep.Name, font);

			g.DrawString(ep.Name, font, new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(w, h, Width - w - 20, (int)bnds.Height), strFrmt);

			h += (int)bnds.Height+2;

			font = new Font("Nirmala UI", 9F);
			g.DrawString(ep.Show.Name, font, new SolidBrush(FormDesign.Design.LabelColor), new Rectangle(w, h, Width - w - 20, (int)bnds.Height), strFrmt);
			bnds = g.MeasureString(ep.Show.Name, font);
			h += (int)bnds.Height+2;

			font = new Font("Nirmala UI", 8.25F);

			g.DrawString($"Season {ep.SN} • Episode {ep.EN}", font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = g.MeasureString($"Season {ep.SN} • Episode {ep.EN}", font);
			h += (int)bnds.Height+2;

			var txt = "No air date yet";
			if (ep.AirState == AirStateEnum.Aired)
				txt = $"Aired {ep.TMDbData.AirDate?.RelativeString()}";
			else if (ep.AirState == AirStateEnum.ToBeAired)
				txt = $"Airing {ep.TMDbData.AirDate?.RelativeString()}";

			g.DrawString(txt, font, new SolidBrush(FormDesign.Design.InfoColor), w, h);

			font = new Font("Nirmala UI", 6.75F);

			bnds = g.MeasureString("Click to download", font);
			g.DrawString("Click to download", font, new SolidBrush(FormDesign.Design.AccentColor), Width - bnds.Width - 2, Height - bnds.Height - 2);
		}

		public static void Save(Show show)
		{
			if (show.TMDbData != null && show.TMDbData.Id != 0)
				show.Save();
		}

		public static void Save(Episode episode)
			=> Save(episode.Show);

		public static void LoadAllShows()
		{
			foreach (var file in new DirectoryInfo(Path.Combine(ISave.DocsFolder, "ShowsData"))
				.EnumerateFiles("*.scs").Where(x => x.Length > 0))
			{
				var show = ISave.Load<Show>($"ShowsData/{file.Name}");
				Add(show, false);
			}

			if (Shows.All(x => x.Loaded))
				ShowLoadedMethod(null);

			Data.Preferences.LastShowCheck = DateTime.Now;
			Data.Preferences.Save();

			LocalShowHandler.Load();
		}
	}
}
