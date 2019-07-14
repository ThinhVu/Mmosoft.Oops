using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls
{
    public class ImageWrapper
    {
        public bool RedrawRequired;
        // 
        private Image _orgImage;
        private Image _resizedImage;
        private Rectangle _clippingRegion;
        private Rectangle _drawingRegion;

        /// <summary>
        /// Original image src
        /// </summary>
        public Image OriginalImage
        {
            get
            {
                return _orgImage;
            }
            set
            {
                if (_orgImage != value)
                {
                    _orgImage = value;
                    RedrawRequired = true;
                }
            }
        }
        /// <summary>
        /// Image which has been resized to increase performance of painting
        /// </summary>
        public Image ResizedImage
        {
            get
            {
                return _resizedImage;
            }
            set
            {
                if (_resizedImage != value)
                {
                    _resizedImage = value;
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
                        _drawingRegion = _clippingRegion.AdjustY(-16);
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

        public ImageWrapper(Image img)
        {
            OriginalImage = img;
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