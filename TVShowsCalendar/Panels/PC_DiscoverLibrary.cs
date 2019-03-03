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
using ShowsCalendar.Handlers;
using ShowsCalendar.Controls;
using Extensions;
using SlickControls.Classes;

namespace ShowsCalendar.Panels
{
	public partial class PC_DiscoverLibrary : PanelContent
	{
		public PC_DiscoverLibrary() : base(true)
		{
			InitializeComponent();

			verticalScroll1.SizeSource = () => TLP_Shows.Visible.If(FLP_ShowResults.Height, FLP_MovieResults.Height);
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			PB_Loader.Image = FormDesign.Loader;
		}

		protected override bool LoadData()
		{
			this.TryInvoke(() =>
			{
				Notification.Create("Searching", "Now searching for TV Shows in your folders.\nIt might take some time..", SlickControls.Enums.PromptIcons.Info, null)
					.Show(Form, 15);
			});

			foreach (var show in MediaDiscoveryHandler.DiscoverShows())
			{
				if (FLP_ShowResults.Controls.ThatAre<MediaViewer>().Any(x => x.SearchData.Id == show.Id))
					continue;

				this.TryInvoke(() => FLP_ShowResults.Controls.Add(new MediaViewer(show) { Anchor = AnchorStyles.Top }));
			}

			this.TryInvoke(() =>
			{
				Notification.Create("Searching", "Now searching for Movies in your folders.\nIt might take some time..", SlickControls.Enums.PromptIcons.Info, null)
					.Show(Form, 15);
			});

			foreach (var movie in MediaDiscoveryHandler.DiscoverMovies())
			{
				if (FLP_MovieResults.Controls.ThatAre<MediaViewer>().Any(x => x.SearchData.Id == movie.Id))
					continue;

				this.TryInvoke(() => FLP_MovieResults.Controls.Add(new MediaViewer(movie) { Anchor = AnchorStyles.Top }));
			}

			this.TryInvoke(() =>
			{
				Notification.Create("Finished", "Finished searching your folders.\nLook around the results.", SlickControls.Enums.PromptIcons.Info, null)
					.Show(Form, 15);
				PB_Loader.Hide();
			});

			return true;
		}

		private void PC_DiscoverLibrary_Resize(object sender, EventArgs e)
		{
			P_Stuff.MaximumSize = new Size(Width, 9999);
			P_Stuff.MinimumSize = new Size(Width, 0);
		}

		private void T_Shows_TabSelected(object sender, EventArgs e)
		{
			TLP_Movies.Visible = false;
			TLP_Shows.Visible = true;
		}

		private void T_Movies_TabSelected(object sender, EventArgs e)
		{
			TLP_Shows.Visible = false;
			TLP_Movies.Visible = true;
		}
	}
}
