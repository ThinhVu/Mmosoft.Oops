namespace Mmosoft.Oops.Controls
{
    public abstract class LayoutSettingBase
    {
        public int Column { get; private set; }
        public int Gutter { get; private set; }

        public LayoutSettingBase(int column, int gutter)
        {
            Column = column;
            Gutter = gutter;
        }
    }

    public class FillToTop : LayoutSettingBase
    {
        public FillToTop(int column, int gutter) : base (column, gutter)
        {

        }
    }

    public class FillToBlock : LayoutSettingBase
    {
        public int RowHeight { get; private set; }
        public bool MergeColumn { get; private set; }
        public ImageGridDisplayMode DisplayMode { get; private set; }

        public FillToBlock(int column, int gutter, int rowHeight, bool mergeColumn, ImageGridDisplayMode displayMode) 
            : base (column, gutter)
        {
            RowHeight = rowHeight;
            MergeColumn = mergeColumn;
            DisplayMode = displayMode;
        }
    }
}
