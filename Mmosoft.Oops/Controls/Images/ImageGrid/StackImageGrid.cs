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
            for (int i = 0; i < _imageWrappers.Count; i++)
            {
                GetMinStackHeightAndIndex(stackTops, out top, out colId);
                left = Gutter * (1 + colId[0]) + _columnWidth  * colId[0];

                ImageWrapper iw = _imageWrappers[i];
                int actualImageWidth = iw.OriginalImage.Width;
                int actualImageHeight = iw.OriginalImage.Height;
                int availableHeight = (int)((_columnWidth * 1f / actualImageWidth) * actualImageHeight);
                iw.ClippingRegion = new Rectangle(left, top - _offsetY, _columnWidth, availableHeight);

                // next image in the same column will be drawned at "columnTops[colId] + availableHeight + MGutter" position
                stackTops[colId[0]] += availableHeight + Gutter;

                // increase virtual height
                if (_virtualHeight < stackTops[colId[0]])
                    _virtualHeight = stackTops[colId[0]];
            }
        }
        protected override void PaintImages(Graphics g, IEnumerable<ImageWrapper> images)
        {
            foreach (ImageWrapper image in images)
            {
                if (_dragItem == null || _dragItem.ItemRef != image)
                {
                    g.SetClip(image.ClippingRegion);
                    g.DrawImage(image.OriginalImage, image.DrawingRegion);
                }

                if (_hoverItem != image)
                    g.FillRectangle(_overlayBrush, image.DrawingRegion);
            }

            // draw floating picked item
            g.SetClip(this.ClientRectangle);
            if (_dragItem != null)
            {
                g.DrawImage(_dragItem.Image, _dragItem.Boundary);
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
