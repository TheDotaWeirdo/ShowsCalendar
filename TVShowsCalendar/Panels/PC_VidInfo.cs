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
using TVShowsCalendar.Classes;
using Extensions;

namespace TVShowsCalendar.Panels
{
	public partial class PC_VidInfo : PanelContent
	{
		private PC_VidInfo(Vlc.DotNet.Forms.VlcControl vlcControl)
		{
			InitializeComponent();

			var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;

			L_Duration.Text = TimeSpan.FromMilliseconds(vlcControl.Length).ToReadableString();
			L_Resolution.Text = $"{vidInf.Width} x {vidInf.Height} pixels";
			L_Subtitles.Text = vlcControl.SubTitles.Count.If(x => x == 0, x => $"None", x => $"{x} SubTitles");

			DesignChanged(FormDesign.Design);
		}

		public PC_VidInfo(Episode ep, Vlc.DotNet.Forms.VlcControl vlcControl) : this(vlcControl)
		{
			L_Episode.Text = ep.ToString();
			L_Plot.Text = ep.TMDbData?.Overview;

			if (ep.AirState == AirStateEnum.Aired)
				L_AirDate.Text = $"Aired {ep.TMDbData.AirDate?.RelativeString()}";
			else if (ep.AirState == AirStateEnum.ToBeAired)
				L_AirDate.Text = $"Airing {ep.TMDbData.AirDate?.RelativeString()}";
			else
				L_AirDate.Visible = L_3.Visible = false;

			var tile = new EpisodeTile(ep, displayView: true) { Dock = DockStyle.Top };
			Controls.Add(tile);
		}

		public PC_VidInfo(Movie movie, Vlc.DotNet.Forms.VlcControl vlcControl) : this(vlcControl)
		{
			L_Episode.Text = movie.TMDbData.Tagline;
			L_2.Text = "Tag-line:";
			L_Plot.Text = movie.TMDbData?.Overview;

			if (movie.tMDbData.ReleaseDate != null)
			{
				if (movie.tMDbData.ReleaseDate < DateTime.Now)
					L_AirDate.Text = $"Aired {movie.tMDbData.ReleaseDate?.RelativeString()}";
				else
					L_AirDate.Text = $"Airing {movie.tMDbData.ReleaseDate?.RelativeString()}";
			}
			else
				L_AirDate.Visible = L_3.Visible = false;

			var tile = new MovieTile(movie, true, true) { Dock = DockStyle.Top };
			Controls.Add(tile);
		}

		protected override void DesignChanged(FormDesign design)
		{
			L_2.ForeColor = L_3.ForeColor =
				L_4.ForeColor = L_5.ForeColor = L_6.ForeColor =
				 L_7.ForeColor = design.LabelColor;
		}

		private void PC_VidInfo_Resize(object sender, EventArgs e)
		{
			tableLayoutPanel1.MaximumSize = new Size(panel1.Width, 9999);
			tableLayoutPanel1.MinimumSize = new Size(panel1.Width, 0);
		}
	}
}
