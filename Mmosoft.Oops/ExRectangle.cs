using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mmosoft.Oops
{
    public static class ExRectangle
    {
        // Moving
        public static Rectangle AdjustX(this Rectangle r, int x)
        {
            return AdjustXY(r, x, 0);
        }
        public static Rectangle AdjustY(this Rectangle r, int y)
        {
            return AdjustXY(r, 0, y);
        }
        public static Rectangle AdjustXY(this Rectangle r, int x, int y)
        {
            return new Rectangle(r.X + x, r.Y + y, r.Width, r.Height);
        }

        // Set size int
        public static Rectangle SetWidth(this Rectangle r, int w)
        {
            return new Rectangle(r.X, r.Width, w, r.Height); 
        }
        public static Rectangle SetHeight(this Rectangle r, int h)
        {
            return new Rectangle(r.X, r.Width, r.Width, h); 
        }
        public static Rectangle SetSize(this Rectangle r, int w, int h)
        {
            return new Rectangle(r.X, r.Y, w, h);
        }

        // Increase/decrease size
        public static Rectangle AdjustSize(this Rectangle r, int deltaW, int deltaH)
        {
            return new Rectangle(r.X, r.Y, r.Width + deltaW, r.Height + deltaH);
        }
        public static Rectangle AdjustSizeFromCenter(this Rectangle r, int deltaW, int deltaH)
        {
            return new Rectangle(r.X - deltaW/2, r.Y - deltaH/2, r.Width + deltaW, r.Height + deltaH);
        }

        // Float moving
        public static RectangleF AdjustXF(this RectangleF r, float x)
        {
            return AdjustXYF(r, x, 0f);
        }
        public static RectangleF AdjustYF(this RectangleF r, float y)
        {
            return AdjustXYF(r, 0f, y);
        }
        public static RectangleF AdjustXYF(this RectangleF r, float x, float y)
        {
            return new RectangleF(r.X + x, r.Y + y, r.Width, r.Height);
        }

        // Set size float
        public static RectangleF SetWidth(this RectangleF r, float w)
        {
            return new RectangleF(r.X, r.Width, w, r.Height);
        }
        public static RectangleF SetHeight(this RectangleF r, float h)
        {
            return new RectangleF(r.X, r.Width, r.Width, h);
        }
        public static RectangleF SetSize(this RectangleF r, float w, float h)
        {
            return new RectangleF(r.X, r.Y, w, h);
        }

        // Increase/decrease size f
        public static RectangleF AdjustSize(this RectangleF r, float deltaW, float deltaH)
        {
            return new RectangleF(r.X, r.Y, r.Width + deltaW, r.Height + deltaH);
        }
        public static RectangleF AdjustSizeFromCenter(this RectangleF r, float deltaW, float deltaH)
        {
            return new RectangleF(r.X - deltaW / 2, r.Y - deltaH/2, r.Width + deltaW, r.Height + deltaH);
        }
    }
}
