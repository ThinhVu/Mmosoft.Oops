using System.Drawing;

namespace Mmosoft.Oops.Controls
{
    public class Img
    {
        private Image _original;
        private Rectangle _clippingRegion;

        public bool DrawRequired;
        /// <summary>
        /// Original image src
        /// </summary>
        public Image Original
        {
            get
            {
                return _original;
            }
            set
            {
                if (_original != value)
                {
                    _original = value;
                    DrawRequired = true;
                }
            }
        }
        /// <summary>
        /// An area which will be used to draw
        /// </summary>
        public Rectangle ClippingRegion
        {
            get
            {
                return _clippingRegion;
            }
            set
            {
                if (_clippingRegion != value)
                {
                    _clippingRegion = value;
                    DrawRequired = true;
                }
            }
        }


        public Img(Image original)
        {
            Original = original;
        }
        public void Dispose()
        {
            if (Original != null)
                Original.Dispose();
        }
    }
}