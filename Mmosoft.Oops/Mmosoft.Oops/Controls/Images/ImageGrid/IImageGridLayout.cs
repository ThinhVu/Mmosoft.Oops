using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mmosoft.Oops.Controls
{
    public abstract class ImageGridLayoutBase
    {
        public int Column { get; private set; }
        public int Gutter { get; private set; }

        public ImageGridLayoutBase(int column, int gutter)
        {
            Column = column;
            Gutter = gutter;
        }
    }

    public class FillToTopLayout : ImageGridLayoutBase
    {
        public FillToTopLayout(int column, int gutter) : base (column, gutter)
        {

        }
    }

    public class TableLayout : ImageGridLayoutBase
    {
        public int RowHeight { get; private set; }
        public bool MergeColumn { get; private set; }
        public ImageGridDisplayMode DisplayMode { get; private set; }

        public TableLayout(int column, int gutter, int rowHeight, bool mergeColumn, ImageGridDisplayMode displayMode) 
            : base (column, gutter)
        {
            RowHeight = rowHeight;
            MergeColumn = mergeColumn;
            DisplayMode = displayMode;
        }
    }
}
