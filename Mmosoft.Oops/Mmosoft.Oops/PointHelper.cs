using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mmosoft.Oops
{
    public static class PointHelper
    {
        /// <summary>
        /// Make new point structure with new position calculated by dx, dy
        /// </summary>
        /// <param name="point"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        public static Point ChangePosition(this Point point, int dx, int dy)
        {
            var p = point;
            p.Offset(dx, dy);
            return p;
        }
    }
}
