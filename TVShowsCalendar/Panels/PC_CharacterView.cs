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
using ProjectImages = ShowsCalendar.Properties.Resources;
using TMDbLib.Objects.TvShows;
using ShowsCalendar.Handlers;
using TMDbLib.Objects.General;
using SlickControls.Forms;
using Extensions;
using ShowsCalendar.Classes;
using TMDbLib.Objects.People;
using ShowsCalendar.Controls;
using SlickControls.Controls;

namespace ShowsCalendar.Panels
{
	public partial class PC_CharacterView : PanelContent
	{
		public Person Person { get; private set; }
		private Bitmap defaultImage;

		private PC_CharacterView(int id, string name, string profile, Bitmap img) : base(true)
		{
			InitializeComponent();

			Text = name;
			defaultImage = img;
			Person = new Person() { Id = id };

			PB_EpisodeInfo.GetImage(profile, 200);

			ST_Library_TabSelected(null, null);
		}

		public PC_CharacterView(Cast cast)
			: this(cast.Id, cast.Name, cast.ProfilePath, ProjectImages.Huge_Cast) { }

		public PC_CharacterView(TMDbLib.Objects.Movies.Cast cast)
			: this(cast.Id, cast.Name, cast.ProfilePath, ProjectImages.Huge_Cast) { }

		public PC_CharacterView(Crew crew)
			: this(crew.Id, crew.Name, crew.ProfilePath, ProjectImages.Huge_Crew) { }

		public PC_CharacterView(CreatedBy item)
			: this(item.Id, item.Name, item.ProfilePath, ProjectImages.Huge_Quill) { }

		protected override bool LoadData()
		{
			Person = Data.TMDbHandler.RunTask(x => x.GetPersonAsync(Person.Id, PersonMethods.Images | PersonMethods.MovieCredits | PersonMethods.TvCredits)).Result;

			return true;
		}

		private string knownFor()
		{
			if (DataLoaded)
			{
				var dic = new Dictionary<string, double>() { { "Known for Acting", (Person.MovieCredits.Cast.Count * 4) + Person.TvCredits.Cast.Sum(x => x.EpisodeCount / 2) } };

				foreach (var item in Person.MovieCredits.Crew.Where(x => !string.IsNullOrWhiteSpace(x.Department)))
				{
					var dep = "Known for " + item.Department;
					if (dic.ContainsKey(dep))
						dic[dep] += 6;
					else
						dic.Add(dep, 6);
				}

				foreach (var item in Person.TvCredits.Crew.Where(x => !string.IsNullOrWhiteSpace(x.Department)))
				{
					var dep = "Known for " + item.Department;
					if (dic.ContainsKey(dep))
						dic[dep] += 6;
					else
						dic.Add(dep, 6);
				}

				var res = dic.OrderBy(x => x.Value).Last();

				return res.Key;
			}

			return "Loading info..";
		}

		protected override void OnDataLoad()
		{
			PB_EpisodeInfo.Refresh();

			if (ST_Images.Selected)
				ST_Images_TabSelected(null, null);
			else if (ST_Career.Selected)
				ST_Career_TabSelected(null, null);
		}

		private void PC_EpisodeView_Resize(object sender, EventArgs e)
		{
			FLP_Content.MaximumSize = new Size(panel1.Width, 9999);
			FLP_Content.MinimumSize = new Size(panel1.Width, 0);
		}

		private void PB_EpisodeInfo_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);

			if (PB_EpisodeInfo.Image != null)
				e.Graphics.DrawBorderedImage(PB_EpisodeInfo.Image, new Rectangle(8, 1, (130) * 2 / 3, 130), PB_EpisodeInfo.Image.IsAnimated() || PB_EpisodeInfo.Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage(defaultImage.Color(FormDesign.Design.IconColor), new Rectangle(8, 1, (130) * 2 / 3, 130), ImageHandler.ImageSizeMode.Center);

			var w = 12 + ((130) * 2 / 3);
			var h = 2f;

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			e.Graphics.DrawString(knownFor(), new Font("Nirmala UI", 9.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), w, h);
			var bnds = e.Graphics.MeasureString(knownFor(), new Font("Nirmala UI", 9.75F, FontStyle.Bold));

			if (!DataLoaded)
				return;

			h += bnds.Height + 2;

			if (Person.AlsoKnownAs.Any())
			{
				e.Graphics.DrawString($"Also known as {Person.AlsoKnownAs.First()}", new Font("Nirmala UI", 9F), new SolidBrush(FormDesign.Design.LabelColor), w, h);
				bnds = e.Graphics.MeasureString($"Also known as {Person.AlsoKnownAs.First()}", new Font("Nirmala UI", 9F));

				h += bnds.Height + 2;
			}

			if (Person.Birthday != null)
			{
				e.Graphics.DrawString($"Born on {Person.Birthday?.ToReadableString(format: ExtensionClass.DateFormat.TDMY)}" + (Person.Deathday == null ? "" : $" and passed away on {Person.Deathday?.ToReadableString(format: ExtensionClass.DateFormat.TDMY)}"), new Font("Nirmala UI", 8.25F), new SolidBrush(FormDesign.Design.LabelColor), w, h);
				bnds = e.Graphics.MeasureString($"Born on {Person.Birthday?.ToReadableString(format: ExtensionClass.DateFormat.TDMY)}" + (Person.Deathday == null ? "" : $" and passed away on {Person.Deathday?.ToReadableString(format: ExtensionClass.DateFormat.TDMY)}"), new Font("Nirmala UI", 8.25F));

				h += bnds.Height + 2;
			}

			w += 2;

			e.Graphics.DrawString(Person.Biography.IfEmpty("No Biography"), new Font("Nirmala UI", 6.75F), new SolidBrush(FormDesign.Design.InfoColor), new RectangleF(w, h, PB_EpisodeInfo.Width - w - 5, PB_EpisodeInfo.Height - h - 5), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}

		private void ST_Images_TabSelected(object sender, EventArgs e)
		{
			FLP_Content.Controls.Clear(true);
			if (Person.Images != null)
				foreach (var item in Person.Images.Profiles)
				{
					var width = 235;
					var height = width * item.Height / item.Width;

					var pb = new BorderedImage() { Size = new Size(width, height), Margin = new Padding(7) };
					pb.GetImage(item.FilePath, 300);

					FLP_Content.Controls.Add(pb);
				}
		}

		private void ST_Library_TabSelected(object sender, EventArgs e)
		{
			FLP_Content.Controls.Clear(true);
			var showsDic = new List<Show>();
			var epsDic = new List<Episode>();
			var moviesDic = new List<Movie>();

			foreach (var item in ShowManager.Shows)
			{
				if (item.Cast.Any(z => z.Id == Person.Id) 
					|| item.Crew.Any(z => z.Id == Person.Id) 
					|| item.Seasons.Any(y => y.TMDbData.Credits.Cast.Any(z => z.Id == Person.Id) || y.TMDbData.Credits.Crew.Any(z => z.Id == Person.Id)))
					showsDic.Add(item);

				epsDic.AddRange(item.Episodes.Where(y => y.TMDbData.GuestStars.Any(z => z.Id == Person.Id) || y.TMDbData.Crew.Any(z => z.Id == Person.Id)));
			}

			foreach (var item in MovieManager.Movies)
			{
				if (item.Cast.Any(z => z.Id == Person.Id)
					|| item.Crew.Any(z => z.Id == Person.Id))
					moviesDic.Add(item);
			}

			foreach (var item in showsDic)
				FLP_Content.Controls.Add(new ShowTile(item));

			foreach (var item in epsDic)
				FLP_Content.Controls.Add(new EpisodeTile(item, false));

			foreach (var item in moviesDic)
				FLP_Content.Controls.Add(new MovieTile(item));
		}

		private void ST_Career_TabSelected(object sender, EventArgs e)
		{
			FLP_Content.Controls.Clear(true);
			if (!DataLoaded)
				return;

			var imgs = new List<ContextedImage>();

			foreach (var item in Person.TvCredits.Cast)
			{
				var pb = new ContextedImage()
				{
					Size = new Size(130, 230),
					HeaderText = item.Name,
					InfoText = item.Character.IfEmpty("", $"As {item.Character}"),
					HoverText = "TV SERIES",
					HoverImages = new[] { ProjectImages.Tiny_Add },
					ErrorImage = ProjectImages.Huge_TV,
					Tag = item.FirstAirDate?.Year ?? int.MinValue
				};
				pb.GetImage(item.PosterPath, 200);
				pb.Click += (s, te) => ShowManager.Add(new Show(new LightShow() { Id = item.Id, Name = item.Name }), true);

				imgs.Add(pb);
			}

			foreach (var item in Person.TvCredits.Crew)
			{
				var pb = new ContextedImage()
				{
					Size = new Size(130, 230),
					HeaderText = item.Name,
					InfoText = item.Job,
					HoverText = "TV SERIES",
					HoverImages = new[] { ProjectImages.Tiny_Add },
					ErrorImage = ProjectImages.Huge_TV,
					Tag = item.FirstAirDate?.Year ?? int.MinValue
				};
				pb.GetImage(item.PosterPath, 200);
				pb.Click += (s, te) => ShowManager.Add(new Show(new LightShow() { Id = item.Id, Name = item.Name }), true);

				imgs.Add(pb);
			}

			foreach (var item in Person.MovieCredits.Cast)
			{
				var pb = new ContextedImage()
				{
					Size = new Size(130, 230),
					HeaderText = item.Title,
					InfoText = item.Character.IfEmpty("", $"As {item.Character}"),
					HoverText = "MOVIE",
					HoverImages = new[] { ProjectImages.Tiny_Add },
					ErrorImage = ProjectImages.Huge_Movie,
					Tag = item.ReleaseDate?.Year ?? int.MinValue
				};
				pb.GetImage(item.PosterPath, 200);
				pb.Click += (s, te) => MovieManager.Add(new Movie(new LightMovie() { Id = item.Id, Title = item.Title }), true);

				imgs.Add(pb);
			}

			foreach (var item in Person.MovieCredits.Crew)
			{
				var pb = new ContextedImage()
				{
					Size = new Size(130, 230),
					HeaderText = item.Title,
					InfoText = item.Job,
					HoverText = "MOVIE",
					HoverImages = new[] { ProjectImages.Tiny_Add },
					ErrorImage = ProjectImages.Huge_Movie,
					Tag = item.ReleaseDate?.Year ?? int.MinValue
				};
				pb.GetImage(item.PosterPath, 200);
				pb.Click += (s, te) => MovieManager.Add(new Movie(new LightMovie() { Id = item.Id, Title = item.Title }), true);

				imgs.Add(pb);
			}

			var p = new Panel() { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };

			foreach (var grp in imgs.GroupBy(x => (int)x.Tag).OrderByDescending(x => x.Key))
			{
				var ssp = new SlickSectionPanel()
				{
					Size = new Size(130, 250),
					Text = grp.Key.If(int.MinValue, "Unknown", grp.Key.ToString()),
					AutoHide = true,
					Dock = DockStyle.Top,
					AutoSize = true,
					Icon = ProjectImages.Big_Calendar,
					MinimumSize = new Size(FLP_Content.Width, 0)
				};

				ssp.Add(grp.OrderBy(x => x.HeaderText));

				FLP_Content.Controls.Add(ssp);
			}
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			panel1.BackColor = verticalScroll1.BackColor = design.BackColor.Tint(Lum: design.Type.If(FormDesignType.Dark, 3, -3));
		}
	}
}
