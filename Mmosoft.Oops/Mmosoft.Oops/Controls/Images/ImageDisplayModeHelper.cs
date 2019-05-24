using System;
using System.Drawing;

namespace Mmosoft.Oops.Controls
{
    public static class ImageDisplayModeHelper
    {
        public static Rectangle GetImageRect(ref Rectangle clippingRegion, Rectangle imgRect, DisplayMode mode)
        {
            Rectangle output = Rectangle.Empty;
            switch (mode)
            {
                case DisplayMode.Normal:
                    output = Normal(ref clippingRegion, imgRect);
                    break;
                case DisplayMode.StretchImage:
                    output = StretchImage(ref clippingRegion, imgRect);
                    break;
                case DisplayMode.AutoSize:
                    output = AutoSize(ref clippingRegion, imgRect);
                    break;
                case DisplayMode.CenterImage:
                    output = CenterImage(ref clippingRegion, imgRect);
                    break;
                case DisplayMode.ScaleLoss:
                    output = Scale(ref clippingRegion, imgRect, scaleLoss: true);
                    break;
                case DisplayMode.ScaleLossLess:
                    output = Scale(ref clippingRegion, imgRect, scaleLoss: false);
                    break;
                case DisplayMode.ScaleLossCenter:
                    output = ScaleCenter(ref clippingRegion, imgRect, scaleLoss: true);
                    break;
                case DisplayMode.ScaleLossLessCenter:
                    output = ScaleCenter(ref clippingRegion, imgRect, scaleLoss: false);
                    break;
                case DisplayMode.ChangeHeightFixedWidthRatio:
                    output = ChangeHeightFixedWidthRatio(ref clippingRegion, imgRect);
                    break;
                case DisplayMode.ChangeWidthFixedHeightRatio:
                    output = ChangeWidthFixedHeightRatio(ref clippingRegion, imgRect);
                    break;
                default:
                    break;
            }

            return output;
        }

        private static Rectangle Normal(ref Rectangle clippingRegion, Rectangle imgRect)
        {
            return imgRect;
        }

        private static Rectangle StretchImage(ref Rectangle clippingRegion, Rectangle imgRect)
        {
            return clippingRegion;
        }

        private static Rectangle AutoSize(ref Rectangle clippingRegion, Rectangle imgRect)
        {
            clippingRegion = imgRect;
            return imgRect;
        }

        private static Rectangle CenterImage(ref Rectangle clippingRegion, Rectangle imgRect)
        {
            return new Rectangle()
            {
                X = clippingRegion.X - (clippingRegion.Width - imgRect.Width) / 2,
                Y = clippingRegion.Y - (clippingRegion.Height - imgRect.Height) / 2,
                Width = imgRect.Width,
                Height = imgRect.Height
            };
        }

        private static Rectangle Scale(ref Rectangle clippingRegion, Rectangle imgRect, bool scaleLoss)
        {
            float ratio = 1.0f;
            if (!scaleLoss)
                ratio = Math.Max(clippingRegion.Width * 1f / imgRect.Width, clippingRegion.Height * 1f / imgRect.Height);
            else
                ratio = Math.Min(clippingRegion.Width * 1f / imgRect.Width, clippingRegion.Height * 1f / imgRect.Height);

            return new Rectangle
            {
                X = clippingRegion.X,
                Y = clippingRegion.Y,
                Width = (int)(clippingRegion.Width / ratio),
                Height = (int)(clippingRegion.Height / ratio)
            };
        }

        private static Rectangle ScaleCenter(ref Rectangle clippingRegion, Rectangle imgRect, bool scaleLoss)
        {
            Rectangle scaled = Scale(ref clippingRegion, imgRect, scaleLoss);
            return CenterImage(ref scaled, imgRect);
        }

        private static Rectangle ChangeHeightFixedWidthRatio(ref Rectangle clippingRegion, Rectangle imgRect)
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

        private static Rectangle ChangeWidthFixedHeightRatio(ref Rectangle clippingRegion, Rectangle imgRect)
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
