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
using Extensions;
using TVShowsCalendar.Classes;
using TVShowsCalendar.UserControls;

namespace TVShowsCalendar.Panels
{
	public partial class PC_Discover : PanelContent
	{
		private bool IsMovie;

		public PC_Discover(bool isMovie = false)
		{
			InitializeComponent();
			IsMovie = isMovie;
			Text += isMovie.If(" Movies", " Shows");
			verticalScroll1.LinkedControl = FLP_Results;
			PB_Load.Image = FormDesign.Loader;

			StartDataLoad();
		}

		protected override bool LoadData()
		{
			var index = 0;
			do
			{
				if (IsMovie)
				{
					var media = Data.TMDbHandler.DiscoverMovies(index).Result.Convert(LightMovie.Convert);

					this.TryInvoke(() =>
					{
						FLP_Results.SuspendDrawing();
						foreach (var item in media)
						{
							if (FLP_Results.Controls.ThatAre<MediaViewer>().Any(x => x.SearchData.Id == item.Id))
								continue;

							var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.Top };
							FLP_Results.Controls.Add(mediaViewer);
							mediaViewer.Show();
						}
						FLP_Results.ResumeDrawing();

						if (media.Count() == 0)
						{
							System.Media.SystemSounds.Exclamation.Play();
							var label1 = new Label
							{
								Anchor = AnchorStyles.None,
								Size = new Size(145, 150),
								TextAlign = ContentAlignment.BottomCenter,
								Font = new Font("Century Gothic", 12F, (FontStyle.Bold | FontStyle.Italic), GraphicsUnit.Point, 0),
								Text = "No Results Found",
								ForeColor = FormDesign.Design.RedColor
							};

							FLP_Results.Controls.Add(label1);
						}

						PC_Discover_Resize(null, null);
						PB_Load.Dispose();
					});
				}
				else
				{
					var media = Data.TMDbHandler.DiscoverTvShow(index).Result.Convert(LightShow.Convert);

					this.TryInvoke(() =>
					{
						FLP_Results.SuspendDrawing();
						foreach (var item in media)
						{
							if (FLP_Results.Controls.ThatAre<MediaViewer>().Any(x => x.SearchData.Id == item.Id))
								continue;

							var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.Top };
							FLP_Results.Controls.Add(mediaViewer);
							mediaViewer.Show();
						}
						FLP_Results.ResumeDrawing();

						if (media.Count() == 0)
						{
							System.Media.SystemSounds.Exclamation.Play();
							var label1 = new Label
							{
								Anchor = AnchorStyles.None,
								Size = new Size(145, 150),
								TextAlign = ContentAlignment.BottomCenter,
								Font = new Font("Century Gothic", 12F, (FontStyle.Bold | FontStyle.Italic), GraphicsUnit.Point, 0),
								Text = "No Results Found",
								ForeColor = FormDesign.Design.RedColor
							};

							FLP_Results.Controls.Add(label1);
						}

						PC_Discover_Resize(null, null);
						PB_Load.Dispose();
					});
				}
			}
			while (index++ < 3);

			return true;
		}

		private void PC_Discover_Resize(object sender, EventArgs e)
		{
			FLP_Results.MaximumSize = new Size(panel1.Width, 9999);
			FLP_Results.Left = (panel1.Width - FLP_Results.Width) / 2;
		}
	}
}