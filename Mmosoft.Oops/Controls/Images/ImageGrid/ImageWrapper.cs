using System.Drawing;

namespace Mmosoft.Oops.Controls
{
    public class ImageWrapper
    {
        public Image OriginalImage;
        public Image ResizedImage;
        public Rectangle Boundary;

        public ImageWrapper(Image img)
        {
            OriginalImage = img;
            Boundary = new Rectangle { X = 0, Y = 0, Width = img.Width, Height = img.Height };
        }

        public void Dispose()
        {
            if (OriginalImage != null)
                OriginalImage.Dispose();

            if (ResizedImage != null)
                ResizedImage.Dispose();
        }
    }
}