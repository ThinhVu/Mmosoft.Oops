using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mmosoft.Oops
{
    public static class RectangleHelper
    {
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

        public static Rectangle ChangeSizeToAbsolute(this Rectangle r, int w, int h)
        {
            return new Rectangle(r.X, r.Y, w, h);
        }

        public static Rectangle ChangeSizeRelative(this Rectangle r, int deltaW, int deltaH)
        {
            return new Rectangle(r.X, r.Y, r.Width + deltaW, r.Height + deltaH);
        }

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

        public static RectangleF ChangeSizeToAbsoluteF(this RectangleF r, float w, float h)
        {
            return new RectangleF(r.X, r.Y, w, h);
        }

        public static RectangleF ChangeSizeRelativeF(this RectangleF r, float deltaW, float deltaH)
        {
            return new RectangleF(r.X, r.Y, r.Width + deltaW, r.Height + deltaH);
        }
    }
}
