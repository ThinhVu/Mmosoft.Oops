using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mmosoft.Oops.Controls
{
    public class StackImageGrid : ImageGrid
    {
        protected override void ComputePosition()
        {
            var stackTops = new int[Column];
            for (int i = 0; i < stackTops.Length; i++)
                stackTops[i] = Gutter;

            int left, top;
            List<int> colId;
            for (int i = 0; i < imgs.Count; i++)
            {
                GetMinStackHeightAndIndex(stackTops, out top, out colId);
                left = Gutter * (1 + colId[0]) + colWidth  * colId[0];

                Img iw = imgs[i];
                int actualImageWidth = iw.Original.Width;
                int actualImageHeight = iw.Original.Height;
                int availableHeight = (int)((colWidth * 1f / actualImageWidth) * actualImageHeight);
                iw.ClippingRegion = new Rectangle(left, top - offsetY, colWidth, availableHeight);

                // next image in the same column will be drawned at "columnTops[colId] + availableHeight + MGutter" position
                stackTops[colId[0]] += availableHeight + Gutter;

                // increase virtual height
                if (virtualHeight < stackTops[colId[0]])
                    virtualHeight = stackTops[colId[0]];
            }
        }
        protected override void PaintImages(Graphics g, IEnumerable<Img> images)
        {
            foreach (Img image in images)
            {
                g.SetClip(image.ClippingRegion);
                g.DrawImage(image.Original, image.ClippingRegion);
            }
        }
        // 
        private void GetMinStackHeightAndIndex(int[] num, out int value, out List<int> indexes)
        {
            value = int.MaxValue;
            indexes = new List<int>();
            // looping from right to left
            // so left side will have higher priority
            for (int i = 0; i < num.Length; i++)
            {
                if (value > num[i])
                {
                    indexes.Clear();
                    value = num[i];
                    indexes.Add(i);
                }
                else if (value == num[i])
                {
                    indexes.Add(i);
                }
            }
        }
    }
}
