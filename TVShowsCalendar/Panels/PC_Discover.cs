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
using ShowsCalendar.Classes;
using ShowsCalendar.Controls;

namespace ShowsCalendar.Panels
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
			var totRes = 0;

			do
			{
				if (IsMovie)
				{
					var media = Data.TMDbHandler.DiscoverMovies(index).Result.Convert(LightMovie.Convert);

					if (!media.Any())
						break;

					this.TryInvoke(() =>
					{
						FLP_Results.SuspendDrawing();
						foreach (var item in media)
						{
							if (FLP_Results.Controls.ThatAre<MediaViewer>().Any(x => x.SearchData.Id == item.Id))
								continue;

							if (totRes++ >= Data.Options.DiscoverResults)
								break;

							var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.Top };
							FLP_Results.Controls.Add(mediaViewer);
							mediaViewer.Show();
						}
						FLP_Results.ResumeDrawing();

						PC_Discover_Resize(null, null);
						PB_Load.Dispose();
					});
				}
				else
				{
					var media = Data.TMDbHandler.DiscoverTvShow(index).Result.Convert(LightShow.Convert);

					if (!media.Any())
						break;

					this.TryInvoke(() =>
					{
						FLP_Results.SuspendDrawing();
						foreach (var item in media)
						{
							if (FLP_Results.Controls.ThatAre<MediaViewer>().Any(x => x.SearchData.Id == item.Id))
								continue;

							if (totRes++ >= Data.Options.DiscoverResults)
								break;

							var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.Top };
							FLP_Results.Controls.Add(mediaViewer);
							mediaViewer.Show();
						}
						FLP_Results.ResumeDrawing();

						PC_Discover_Resize(null, null);
						PB_Load.Dispose();
					});
				}

				index++;
			}
			while (totRes < Data.Options.DiscoverResults);

			return true;
		}

		private void PC_Discover_Resize(object sender, EventArgs e)
		{
			FLP_Results.MaximumSize = new Size(panel1.Width, 9999);
			FLP_Results.Left = (panel1.Width - FLP_Results.Width) / 2;
		}
	}
}