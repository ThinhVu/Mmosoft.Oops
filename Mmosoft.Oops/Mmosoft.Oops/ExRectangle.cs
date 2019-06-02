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
        public static Rectangle MoveX(this Rectangle r, int x)
        {
            return MoveXY(r, x, 0);
        }
        public static Rectangle MoveY(this Rectangle r, int y)
        {
            return MoveXY(r, 0, y);
        }
        public static Rectangle MoveXY(this Rectangle r, int x, int y)
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
        public static Rectangle IncreaseSize(this Rectangle r, int deltaW, int deltaH)
        {
            return new Rectangle(r.X, r.Y, r.Width + deltaW, r.Height + deltaH);
        }
        public static Rectangle DecreaseSize(this Rectangle r, int deltaW, int deltaH)
        {
            return new Rectangle(r.X, r.Y, r.Width - deltaW, r.Height - deltaH);
        }
        public static Rectangle IncreaseSizeFromCenter(this Rectangle r, int deltaW, int deltaH)
        {
            return new Rectangle(r.X - deltaW/2, r.Y - deltaH/2, r.Width + deltaW, r.Height + deltaH);
        }
        public static Rectangle DecreaseSizeFromCenter(this Rectangle r, int deltaW, int deltaH)
        {
            return new Rectangle(r.X + deltaW / 2, r.Y + deltaH/2, r.Width - deltaW, r.Height - deltaH);
        }

        // Float moving
        public static RectangleF MoveXF(this RectangleF r, float x)
        {
            return MoveXYF(r, x, 0f);
        }
        public static RectangleF MoveYF(this RectangleF r, float y)
        {
            return MoveXYF(r, 0f, y);
        }
        public static RectangleF MoveXYF(this RectangleF r, float x, float y)
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
        public static RectangleF IncreaseSize(this RectangleF r, float deltaW, float deltaH)
        {
            return new RectangleF(r.X, r.Y, r.Width + deltaW, r.Height + deltaH);
        }
        public static RectangleF DecreaseSize(this RectangleF r, float deltaW, float deltaH)
        {
            return new RectangleF(r.X, r.Y, r.Width - deltaW, r.Height - deltaH);
        }
        public static RectangleF IncreaseSizeFromCenter(this RectangleF r, float deltaW, float deltaH)
        {
            return new RectangleF(r.X - deltaW / 2, r.Y - deltaH/2, r.Width + deltaW, r.Height + deltaH);
        }
        public static RectangleF DecreaseSizeFromCenter(this RectangleF r, float deltaW, float deltaH)
        {
            return new RectangleF(r.X + deltaW / 2, r.Y + deltaH/2, r.Width - deltaW, r.Height - deltaH);
        }
    }
}
