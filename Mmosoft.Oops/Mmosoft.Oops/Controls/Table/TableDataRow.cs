using System.Collections.Generic;

namespace Mmosoft.Oops.Controls.Table
{
    public class TableDataRow
    {
        public int Index { get; set; }
        public List<object> RowData { get; private set; }

        public TableDataRow()
        {
            RowData = new List<object>();
        }
    }
}
