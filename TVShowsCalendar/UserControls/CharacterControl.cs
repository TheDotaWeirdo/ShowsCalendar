using System.Drawing;
using System.Windows.Forms;
using Extensions;
using ProjectImages = TVShowsCalendar.Properties.Resources;
using TVShowsCalendar.HandlerClasses;
using TMDbLib.Objects.General;
using SlickControls.Forms;
using TMDbLib.Objects.TvShows;

namespace TVShowsCalendar.UserControls
{
	public partial class CharacterControl : PictureBox
	{
		private string name;
		private string character;
		private Bitmap defaultImage;

		private CharacterControl(string name, string character, string image, Bitmap defaultImage)
		{
			InitializeComponent();

			this.name = name;
			this.character = character;
			this.defaultImage = defaultImage;

			SlickTip.SetTo(this, character);

			this.GetImage(image, 200);
		}

		public CharacterControl(Cast cast)
			: this(cast.Name, cast.Character, cast.ProfilePath, ProjectImages.Huge_Cast) { }

		public CharacterControl(TMDbLib.Objects.Movies.Cast cast)
			: this(cast.Name, cast.Character, cast.ProfilePath, ProjectImages.Huge_Cast) { }

		public CharacterControl(Crew crew)
			: this(crew.Name, crew.Job, crew.ProfilePath, ProjectImages.Huge_Crew) { }

		public CharacterControl(CreatedBy item)
			: this(item.Name, string.Empty, item.ProfilePath, ProjectImages.Huge_Quill) { Height -= 15; }

		private void CastControl_Paint(object sender, PaintEventArgs e)
		{
			var pb = (PictureBox)sender;

			e.Graphics.Clear(pb.BackColor);

			if (pb.Image != null)
				e.Graphics.DrawBorderedImage(pb.Image, new Rectangle(1, 1, 108, 163), pb.Image.IsAnimated() || pb.Image.Width < 100 ? ImageHandler.ImageSizeMode.Center : ImageHandler.ImageSizeMode.Fill);
			else
				e.Graphics.DrawBorderedImage(defaultImage.Color(FormDesign.Design.IconColor), new Rectangle(1, 1, 108, 163), ImageHandler.ImageSizeMode.Center);

			e.Graphics.DrawString(name, new Font("Nirmala UI", 8.25F), new SolidBrush(FormDesign.Design.LabelColor), new Rectangle(2, 166, pb.Width - 3, 14), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });

			e.Graphics.DrawString(character, new Font("Nirmala UI", 6.75F), new SolidBrush(FormDesign.Design.InfoColor), new Rectangle(3, 181, pb.Width - 3, 14), new StringFormat() { Trimming = StringTrimming.EllipsisCharacter });
		}
	}
}
