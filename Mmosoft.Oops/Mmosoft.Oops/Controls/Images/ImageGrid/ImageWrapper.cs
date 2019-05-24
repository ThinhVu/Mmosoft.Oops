using System.Drawing;

namespace Mmosoft.Oops.Controls
{
    class ImageWrapper
    {
        /// <summary>
        /// Contain image object
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// Contain image boundary
        /// </summary>
        public Rectangle Boundary { get; set; }

        public ImageWrapper(Image img)
        {
            Image = img;
            Boundary = new Rectangle { X = 0, Y = 0, Width = img.Width, Height = img.Height };
        }
    }
}