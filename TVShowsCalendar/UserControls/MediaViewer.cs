using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TMDbLib.Objects.Search;
using TVShowsCalendar.Classes;
using Extensions;
using ProjectImages = TVShowsCalendar.Properties.Resources;
using TVShowsCalendar.HandlerClasses;
using TVShowsCalendar.Panels;

namespace TVShowsCalendar.UserControls
{
	public partial class MediaViewer : PictureBox
	{
		public dynamic SearchData { get; private set; }
		private bool IsMovie;
		private bool added;

		private string[] infoStrings;

		public bool PictureLoaded = false;

		public MediaViewer(LightMovie movie) : this (movie.PosterPath)
		{
			SearchData = movie;
			IsMovie = true;
			added = MovieManager.Movies.Any(x => (x.TMDbData?.Id ?? 0) == movie.Id);

			infoStrings = new[]
			{
				movie.Title,
				movie.GenreIds.Convert(x => Data.TMDbHandler.GetMovieGenre(x).Name).Where(x => !string.IsNullOrWhiteSpace(x)).Take(2).ListStrings(" • "),
				movie.ReleaseDate != null ? movie.ReleaseDate?.ToReadableString() : "No release date",
				movie.Overview.IfEmpty("No overview")
			};
		}

		public MediaViewer(LightShow show) : this(show.PosterPath)
		{
			SearchData = show;
			IsMovie = false;
			added = ShowManager.Shows.Any(x => (x.TMDbData?.Id ?? 0) == show.Id);

			infoStrings = new[]
			{
				show.Name,
				show.GenreIds.Convert(x => Data.TMDbHandler.GetTvGenre(x).Name).Where(x => !string.IsNullOrWhiteSpace(x)).Take(2).ListStrings(" • "),
				show.FirstAirDate != null ? show.FirstAirDate?.ToReadableString() : "No air date",
				show.Overview.IfEmpty("No overview")
			};
		}

		public MediaViewer(string data)
		{
			InitializeComponent();

			this.GetImage(data, 200);
		}

		private void MediaViewer_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && (new Rectangle(Width - 20, 6, 16, 16).Contains(e.Location) || new Rectangle(1, 1, 82, 123).Contains(e.Location)))
			{
				if (!added)
				{
					if (IsMovie)
						MovieManager.Add(new Movie((LightMovie)SearchData), true);
					else
						ShowManager.Add(new Show((LightShow)SearchData), true);

					added = true;
					Invalidate();
				}
				else if(new Rectangle(1, 1, 82, 123).Contains(e.Location))
				{
					if (IsMovie)
						Data.Dashboard.PushPanel(null, new PC_MoviePage(MovieManager.Movies.FirstThat(x => x.Id == SearchData.Id)));
					else
						Data.Dashboard.PushPanel(null, new PC_ShowPage(ShowManager.Shows.FirstThat(x => x.Id == SearchData.Id)));
				}
			}
		}

		private void MediaViewer_MouseMove(object sender, MouseEventArgs e)
		{
			if (new Rectangle(1, 1, 82, 123).Contains(e.Location) ||
				(!added && new Rectangle(Width - 20, 6, 16, 16).Contains(e.Location)))
				Cursor = Cursors.Hand;
			else
				Cursor = Cursors.Default;

			Invalidate();
		}

		private void MediaViewer_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			if(Image != null)
				e.Graphics.DrawBorderedImage(Image, new Rectangle(1, 1, 82, 123), Image.IsAnimated() || Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage((IsMovie ? ProjectImages.Big_Movie : ProjectImages.Big_TV).Color(FormDesign.Design.IconColor), new Rectangle(1, 1, 82, 123), ImageHandler.ImageSizeMode.Center);

			if (new Rectangle(1, 1, 82, 123).Contains(PointToClient(MousePosition)))
			{
				e.Graphics.DrawIconsOverImage(new Rectangle(1, 1, 82, 123), PointToClient(MousePosition), added ? ProjectImages.Icon_Info : ProjectImages.Tiny_Add);

				var _bnds = e.Graphics.MeasureString(IsMovie ? "MOVIE" : "TV SHOW", new Font("Nirmala UI", 6.75F, FontStyle.Bold));
				e.Graphics.DrawString(IsMovie ? "MOVIE" : "TV SHOW", new Font("Nirmala UI", 6.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), 81 - _bnds.Width, 121 - _bnds.Height);
			}

			if (added || new Rectangle(Width - 20, 6, 16, 16).Contains(PointToClient(MousePosition)))
				e.Graphics.DrawImage(new Bitmap(added ? ProjectImages.Tiny_Ok : ProjectImages.Tiny_Add).Color(FormDesign.Design.ActiveColor), Width - 20, 6);
			else
				e.Graphics.DrawImage(new Bitmap(added ? ProjectImages.Tiny_Ok : ProjectImages.Tiny_Add).Color(FormDesign.Design.IconColor), Width - 20, 6);

			var strFrmt = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
			var font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
			var w = 86;
			var h = 4;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			var bnds = e.Graphics.MeasureString(infoStrings[0], font);

			e.Graphics.DrawString(infoStrings[0], font, new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(w, h, Width - w - 20, (int)bnds.Height), strFrmt);

			h += (int)bnds.Height;

			font = new Font("Nirmala UI", 8.25F);

			e.Graphics.DrawString(infoStrings[1], font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(infoStrings[1], font);

			font = new Font("Nirmala UI", 6.75F);
			h += (int)bnds.Height + 1;
			w++;

			e.Graphics.DrawString(infoStrings[2], font, new SolidBrush(FormDesign.Design.LabelColor), w, h);
			bnds = e.Graphics.MeasureString(infoStrings[2], font);

			h += (int)bnds.Height + 6;

			e.Graphics.DrawString(infoStrings[3], font, new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, h, Width - 100, Height - h - 8), strFrmt);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		}

		private void MediaViewer_MouseLeave(object sender, System.EventArgs e)
		{
			Invalidate();
		}
	}
}
