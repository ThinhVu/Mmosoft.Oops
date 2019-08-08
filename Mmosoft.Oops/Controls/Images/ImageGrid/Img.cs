using System.Drawing;

namespace Mmosoft.Oops.Controls
{
    public class Img
    {
        public bool RedrawRequired;
        private Image _original;
        private Image _resized;
        private Rectangle _clippingRegion;
        private Rectangle _drawingRegion;

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
                    RedrawRequired = true;
                }
            }
        }
        /// <summary>
        /// Image which has been resized to increase performance of painting
        /// </summary>
        public Image Resized
        {
            get
            {
                return _resized;
            }
            set
            {
                if (_resized != value)
                {
                    _resized = value;
                    RedrawRequired = true;
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

                    if (_drawingRegion == Rectangle.Empty)
                        _drawingRegion = _clippingRegion.AdjustSizeFromCenter(16, 16);
                    else
                        _drawingRegion = _clippingRegion;
                }
            }
        }
        /// <summary>
        /// Area which image will be draw in
        /// When animating, DrawingRegion area are contained in Clipping region
        /// When animating completed, DrawRegion is equal to ClippingRegion
        /// </summary>
        public Rectangle DrawingRegion
        {
            get
            {
                return _drawingRegion;
            }
            set
            {
                if (_drawingRegion != value)
                {
                    _drawingRegion = value;
                    RedrawRequired = true;
                }
            }
        }
        public Img(Image original)
        {
            Original = original;
        }
        public Img(Image original, Image resized)
        {
            Original = original;
            Resized = resized;
        }
        public void Dispose()
        {
            if (Original != null)
                Original.Dispose();
            if (Resized != null)
                Resized.Dispose();
        }
    }
}