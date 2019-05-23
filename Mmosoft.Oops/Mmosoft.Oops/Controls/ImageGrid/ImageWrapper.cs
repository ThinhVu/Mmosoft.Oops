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
        }
    }
}