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
using SlickControls.Forms;
using Extensions;
using TVShowsCalendar.Classes;
using TVShowsCalendar.UserControls;
using SlickControls.Enums;

namespace TVShowsCalendar.Panels
{
	public partial class PC_AddMedia : PanelContent
	{
		public Bitmap Poster { get; private set; }
		private bool Movie;

		public PC_AddMedia(bool movie = false)
		{
			InitializeComponent();
			TB_SeriesName.TextChanged += TB_FolderPath_TextChanged;
			verticalScroll.SizeSource = () => FLP_Results.Height;
			Movie = movie;

			if (movie)
			{
				Text = "Add Movies";
				TB_SeriesName.LabelText = "Movie Name:";
				TB_SeriesName.Placeholder = "Type the name of a Movie to search";
			}
			else
				Text = "Add Shows";

			SlickTip.SetTo(ML_Info, $"Type in the name of a {(Movie ? "movie" : "show")} then select one from the results");
		}

		private void Info_Click(object sender, EventArgs e)
		{
			ShowPrompt($"Type in the name of a {(Movie ? "movie" : "show")} then select one from the results.", "Info", PromptButtons.OK, PromptIcons.Info);
		}

		private void TB_FolderPath_TextChanged(object sender, EventArgs e)
		{
			FLP_Results.Controls.Clear(true);
			AbortLoad();

			if (TB_SeriesName.Text == string.Empty)
			{
				ML_Info.Image = Properties.Resources.Icon_Info;
				ML_Info.Enabled = true;
			}
			else
			{
				ML_Info.Image = FormDesign.Loader;
				ML_Info.Enabled = false;
				StartDataLoad();
			}
		}

		private TicketBooth ticketBooth = new TicketBooth();
		protected override bool LoadData()
		{
			if (string.IsNullOrWhiteSpace(TB_SeriesName.Text))
				return false;

			var ticket = ticketBooth.GetTicket();

			if (!Movie)
			{
				var shows = Data.TMDbHandler.SearchTvShow(TB_SeriesName.Text).Result.Convert(LightShow.Convert);

				if (ticketBooth.IsLast(ticket))
					this.TryInvoke(() =>
					{
						FLP_Results.SuspendLayout();
						foreach (var item in shows)
						{
							var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.Top };
							FLP_Results.Controls.Add(mediaViewer);
							mediaViewer.Show();
						}
						FLP_Results.ResumeLayout(true);

						if (shows.Count() == 0)
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

						ML_Info.Image = Properties.Resources.Icon_Info;
						ML_Info.Enabled = true;
					});
			}
			else
			{
				var movies = Data.TMDbHandler.SearchMovie(TB_SeriesName.Text, 0, true).Result.Convert(LightMovie.Convert);

				if (ticketBooth.IsLast(ticket))
					this.TryInvoke(() =>
					{
						foreach (var item in movies)
						{
							var mediaViewer = new MediaViewer(item) { Anchor = AnchorStyles.None };
							FLP_Results.Controls.Add(mediaViewer);
							mediaViewer.Show();
						}

						if (movies.Count() == 0)
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

						ML_Info.Image = Properties.Resources.Icon_Info;
						ML_Info.Enabled = true;
					});
			}

			return true;
		}

		private void TMDbForm_Resize(object sender, EventArgs e)
		{
			tableLayoutPanel1.MaximumSize = new Size(panel1.Width, 9999);
			tableLayoutPanel1.MinimumSize = new Size(panel1.Width, 0);
		}
	}
}
