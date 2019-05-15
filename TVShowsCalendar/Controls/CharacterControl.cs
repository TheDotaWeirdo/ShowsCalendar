using Extensions;
using ShowsCalendar.Handlers;
using ShowsCalendar.Panels;
using SlickControls.Forms;
using System.Drawing;
using System.Windows.Forms;
using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;
using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar.Controls
{
    public partial class CharacterControl : PictureBox
    {
        private readonly string name;
        private readonly string character;
        private readonly Bitmap defaultImage;
        private readonly dynamic Data;

        private CharacterControl(dynamic data, string name, string character, string image, Bitmap defaultImage)
        {
            InitializeComponent();

            this.name = name;
            this.character = character;
            this.defaultImage = defaultImage;
            Data = data;

            SlickTip.SetTo(this, character);

            this.GetImage(image, 200);

            MouseMove += (s, e) => Invalidate();
            MouseLeave += (s, e) => Invalidate();
        }

        public CharacterControl(Cast cast)
            : this(cast, cast.Name, cast.Character, cast.ProfilePath, ProjectImages.Huge_Cast) { }

        public CharacterControl(TMDbLib.Objects.Movies.Cast cast)
            : this(cast, cast.Name, cast.Character, cast.ProfilePath, ProjectImages.Huge_Cast) { }

        public CharacterControl(Crew crew)
            : this(crew, crew.Name, crew.Job, crew.ProfilePath, ProjectImages.Huge_Crew) { }

        public CharacterControl(CreatedBy item)
            : this(item, item.Name, string.Empty, item.ProfilePath, ProjectImages.Huge_Quill) => Height -= 15;

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

            if (new Rectangle(1, 1, 108, 163).Contains(PointToClient(MousePosition)))
            {
                e.Graphics.DrawIconsOverImage(new Rectangle(1, 1, 108, 163), PointToClient(MousePosition), ProjectImages.Big_Info);
            }
        }

        private void CharacterControl_Click(object sender, System.EventArgs e) => Classes.Data.Mainform.PushPanel(null, new PC_CharacterView(Data));
    }
}
