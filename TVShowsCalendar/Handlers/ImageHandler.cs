using Extensions;
using ShowsCalendar.Classes;
using ShowsCalendar.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ShowsCalendar.Handlers
{
    public static class ImageHandler
    {
        public enum ImageSizeMode
        {
            Fill,
            Stretch,
            Center
        }

        public static bool GetImage(this PictureBox pb, string path, int size, Bitmap errorImage = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                if (errorImage != null)
                    pb.Image = errorImage;
                return false;
            }

            if (pb is BorderedImage bpb)
                bpb.ImageUrl = path;

            var filepath = Path.Combine(ISave.DocsFolder, "Thumbs", size.ToString(), path.TrimStart('/'));
            var imgPath = new FileInfo(filepath);
            if (imgPath.Exists && imgPath.LastWriteTime > DateTime.Now.AddMonths(-2))
            {
                pb.Image = Image.FromFile(imgPath.FullName);
                return true;
            }

			pb.InitialImage = FormDesign.Loader;
            if (errorImage != null)
                pb.ErrorImage = errorImage;

            if (ConnectionHandler.State == ConnectionState.Connected)
            {
                if (size == 0)
                {
                    pb.LoadAsync($"https://image.tmdb.org/t/p/original{path}");
                }
                else
                {
                    pb.LoadAsync($"https://image.tmdb.org/t/p/w{size}{path}");
                    pb.LoadCompleted += (s, e) =>
                    {
                        if (e.Error == null && !e.Cancelled)
                        {
                            try
                            {
                                Directory.GetParent(filepath).Create();
                                pb.Image.Save(filepath);
                            }
                            catch { }
                        }
                    };
                }
            }
            else
            {
                ConnectionHandler.Connected += (c) =>
                {
                    pb.LoadAsync($"https://image.tmdb.org/t/p/w{size}{path}");
                    pb.LoadCompleted += (s, e) =>
                    {
                        if (e.Error == null && !e.Cancelled)
                        {
                            try
                            {
                                Directory.GetParent(filepath).Create();
                                pb.Image.Save(filepath);
                            }
                            catch { }
                        }
                    };
                };
            }

            return false;
        }

        public static void DrawIconsOverImage(this Graphics g, Rectangle imgRect, Point mousePosition, params Bitmap[] bitmaps)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(150, FormDesign.Design.BackColor)), new Rectangle(imgRect.X + 1, imgRect.Y + 1, imgRect.Width - 1, imgRect.Height - 1));

            var baseX = 5 + imgRect.X + ((imgRect.Width - bitmaps.Sum(x => x.Width + 10)) / 2);

            for (var i = 0; i < bitmaps.Length; i++)
            {
                var hovered = new Rectangle(baseX - 5, (imgRect.Height - bitmaps[i].Height) / 2 - 5, bitmaps[i].Width + 10, bitmaps[i].Height + 10).Contains(mousePosition);
                g.DrawImage(new Bitmap(bitmaps[i]).Color(hovered ? FormDesign.Design.ActiveColor : FormDesign.Design.ForeColor), baseX, (imgRect.Height - bitmaps[i].Height) / 2);
                baseX += bitmaps[i].Width + 10;
            }
        }

        public static Rectangle DrawBannerOverImage(this Graphics g, Rectangle imgRect, string text, BannerStyle style, int padding = 0, int tab = 0)
        {
            if (!Data.Options.ShowUnwatchedOnThumb)
                return Rectangle.Empty;

            var sm = g.SmoothingMode;

            try
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                if (imgRect.Width > 100)
                {
                    var font = new Font("Nirmala UI", 8.25F);
                    var size = g.MeasureString(text, font);
                    var path = new System.Drawing.Drawing2D.GraphicsPath();
                    var tabSize = tab * (size.Height + 10);

                    path.AddLine(imgRect.X, imgRect.Y + 7 + tabSize,
                        size.Width + 22 + imgRect.X + padding, imgRect.Y + 7 + tabSize);

                    path.AddLine(size.Width + 22 + imgRect.X + padding, imgRect.Y + 7 + tabSize,
                        size.Width + 12 + imgRect.X + padding, imgRect.Y + 7 + size.Height + 3 + tabSize);

                    path.AddLine(size.Width + 12 + imgRect.X + padding, imgRect.Y + 7 + size.Height + 3 + tabSize,
                        imgRect.X, imgRect.Y + 7 + size.Height + 3 + tabSize);

                    g.FillPath(new SolidBrush(style.BackColor()), path);

                    g.DrawLine(new Pen(style.ForeColor(), 2), size.Width + 22 + imgRect.X + padding, imgRect.Y + 7 + tabSize, size.Width + 12 + imgRect.X + padding, imgRect.Y + 7 + size.Height + 3 + tabSize);

                    g.DrawString(text, font, new SolidBrush(style.ForeColor()), 10, 9 + tabSize);

                    return new Rectangle(imgRect.X, imgRect.Y + 7 + (int)tabSize, (int)size.Width + 22 + padding, (int)size.Height + 3);
                }
                else
                {
                    var font = new Font("Nirmala UI", 6.75F);
                    var size = g.MeasureString(text, font);
                    var path = new System.Drawing.Drawing2D.GraphicsPath();
                    var tabSize = tab * (size.Height + 10);

                    path.AddLine(imgRect.X, imgRect.Y + 7 + tabSize,
                        imgRect.X + size.Width + 15 + padding, imgRect.Y + 7 + tabSize);

                    path.AddLine(imgRect.X + size.Width + 15 + padding, imgRect.Y + 7 + tabSize,
                        imgRect.X + size.Width + 7 + padding, imgRect.Y + 7 + size.Height + 3 + tabSize);

                    path.AddLine(imgRect.X + size.Width + 7 + padding, imgRect.Y + 7 + size.Height + 3 + tabSize,
                        imgRect.X, imgRect.Y + 7 + size.Height + 3 + tabSize);

                    g.FillPath(new SolidBrush(style.BackColor()), path);

                    g.DrawLine(new Pen(style.ForeColor(), 2), imgRect.X + size.Width + 15 + padding, imgRect.Y + 7 + tabSize, imgRect.X + size.Width + 7 + padding, imgRect.Y + 7 + size.Height + 3 + tabSize);

                    g.DrawString(text, font, new SolidBrush(style.ForeColor()), 6, 10 + tabSize);

                    return new Rectangle(imgRect.X, imgRect.Y + 7 + (int)tabSize, (int)size.Width + 15 + padding, (int)size.Height + 3);
                }
            }
            finally
            {
                g.SmoothingMode = sm;
            }
        }

        public static void DrawBorderedImage(this Graphics g, Bitmap bitmap, Rectangle rectangle, ImageSizeMode sizeMode = ImageSizeMode.Fill)
        {
            if (bitmap == null)
            { bitmap = Properties.Resources.Icon_ErrorImage; sizeMode = ImageSizeMode.Center; }

            var bitRect = new Rectangle(rectangle.X + 3, rectangle.Y + 3, rectangle.Width - 5, rectangle.Height - 5);

            switch (sizeMode)
            {
                case ImageSizeMode.Fill:
                    if (bitmap.Height * bitRect.Width / bitmap.Width >= bitRect.Height)
                        bitmap = new Bitmap(bitmap, bitRect.Width, bitmap.Height * bitRect.Width / bitmap.Width);
                    else
                        bitmap = new Bitmap(bitmap, bitmap.Width * bitRect.Height / bitmap.Height, bitRect.Height);
                    break;
                case ImageSizeMode.Stretch:
                    if (bitmap.Size != bitRect.Size)
                        bitmap = new Bitmap(bitmap, bitRect.Size);
                    break;
                default:
                    break;
            }

            g.DrawRectangle(new Pen(FormDesign.Design.AccentColor
                , 1.5F)
                , rectangle);

            if (sizeMode != ImageSizeMode.Center)
                g.DrawImageUnscaledAndClipped(bitmap, bitRect);
            else
                g.DrawImage(bitmap, bitRect.Center(bitmap.Size));
        }

        public static void DrawBorderedImage(this Graphics g, Bitmap bitmap, int x, int y, int width, int height, ImageSizeMode sizeMode = ImageSizeMode.Fill)
            => g.DrawBorderedImage(bitmap, new Rectangle(x, y, width, height), sizeMode);

        public static void DrawBorderedImage(this Graphics g, Image bitmap, int x, int y, int width, int height, ImageSizeMode sizeMode = ImageSizeMode.Fill)
            => g.DrawBorderedImage((Bitmap)bitmap, new Rectangle(x, y, width, height), sizeMode);

        public static void DrawBorderedImage(this Graphics g, Image bitmap, Rectangle rectangle, ImageSizeMode sizeMode = ImageSizeMode.Fill)
            => g.DrawBorderedImage((Bitmap)bitmap, rectangle, sizeMode);
    }
}
