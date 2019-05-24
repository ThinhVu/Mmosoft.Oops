using System;
using System.Drawing;

namespace Mmosoft.Oops.Controls
{
    public static class ImageDisplayModeHelper
    {
        //
        public static bool IsLandscape(Rectangle rect)
        {
            return rect.Width > rect.Height;
        }
        public static bool IsPortrait(Rectangle rect)
        {
            return !IsLandscape(rect);
        }
        public static bool IsSquare(Rectangle rect)
        {
            return !IsLandscape(rect) && !IsPortrait(rect);
        }
        //
        public static Rectangle GetImageRect(Rectangle clippingRegion, Rectangle imgRect, DisplayMode mode)
        {
            Rectangle output = Rectangle.Empty;
            switch (mode)
            {
                case DisplayMode.Normal:
                    output = Normal(clippingRegion, imgRect);
                    break;
                case DisplayMode.StretchImage:
                    output = StretchImage(clippingRegion, imgRect);
                    break;
                case DisplayMode.CenterImage:
                    output = CenterImage(clippingRegion, imgRect);
                    break;
                case DisplayMode.ScaleLoss:
                    output = Scale(clippingRegion, imgRect, scaleLoss: true);
                    break;
                case DisplayMode.ScaleLossLess:
                    output = Scale(clippingRegion, imgRect, scaleLoss: false);
                    break;
                case DisplayMode.ScaleLossCenter:
                    output = ScaleCenter(clippingRegion, imgRect, scaleLoss: true);
                    break;
                case DisplayMode.ScaleLossLessCenter:
                    output = ScaleCenter(clippingRegion, imgRect, scaleLoss: false);
                    break;
                case DisplayMode.ChangeHeightFixedWidthRatio:
                    output = ChangeHeightFixedWidthRatio(clippingRegion, imgRect);
                    break;
                case DisplayMode.ChangeWidthFixedHeightRatio:
                    output = ChangeWidthFixedHeightRatio(clippingRegion, imgRect);
                    break;
                default:
                    break;
            }

            return output;
        }
        private static Rectangle Normal(Rectangle clippingRegion, Rectangle imgRect)
        {
            return new Rectangle 
            { 
                X = clippingRegion.X,
                Y = clippingRegion.Y,
                Width = imgRect.Width,
                Height = imgRect.Height
            };
        }
        private static Rectangle StretchImage(Rectangle clippingRegion, Rectangle imgRect)
        {
            return new Rectangle
            {
                X = clippingRegion.X,
                Y = clippingRegion.Y,
                Width = clippingRegion.Width,
                Height = clippingRegion.Height
            };
        }        
        private static Rectangle CenterImage(Rectangle clippingRegion, Rectangle imgRect)
        {
            return new Rectangle()
            {
                X = clippingRegion.X - (imgRect.Width - clippingRegion.Width) / 2,
                Y = clippingRegion.Y - (imgRect.Height - clippingRegion.Height) / 2,
                Width = imgRect.Width,
                Height = imgRect.Height
            };
        }
        private static Rectangle Scale(Rectangle clippingRegion, Rectangle imgRect, bool scaleLoss)
        {
            var r = new Rectangle { X = clippingRegion.X, Y = clippingRegion.Y };
            var ratioWidth = imgRect.Width * 1f/ clippingRegion.Width;
            var ratioHeight = imgRect.Height * 1f / clippingRegion.Height;
            if (scaleLoss)
            {
                float minRatio = Math.Min(ratioWidth, ratioHeight);
                r.Width = (int)(imgRect.Width / minRatio);
                r.Height = (int)(imgRect.Height / minRatio);
            }
            else
            {
                float maxRatio = Math.Max(ratioWidth, ratioHeight);
                r.Width = (int)(imgRect.Width / maxRatio);
                r.Height = (int)(imgRect.Height / maxRatio);
            }
            return r;
        }
        private static Rectangle ScaleCenter(Rectangle clippingRegion, Rectangle imgRect, bool scaleLoss)
        {
            return CenterImage(clippingRegion, Scale(clippingRegion, imgRect, scaleLoss));
        }
        private static Rectangle ChangeHeightFixedWidthRatio(Rectangle clippingRegion, Rectangle imgRect)
        {
            float widthRatio = imgRect.Width * 1f / clippingRegion.Width;
            return new Rectangle
            {
                X = clippingRegion.X,
                Y = clippingRegion.Y,
                Width = clippingRegion.Width,
                Height = (int)widthRatio * clippingRegion.Height
            };
        }
        private static Rectangle ChangeWidthFixedHeightRatio(Rectangle clippingRegion, Rectangle imgRect)
        {
            float heightRatio = imgRect.Height * 1f / clippingRegion.Height;
            return new Rectangle
            {
                X = clippingRegion.X,
                Y = clippingRegion.Y,
                Width = (int)heightRatio * clippingRegion.Width,
                Height = clippingRegion.Height
            };
        }
    }
}
