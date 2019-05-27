using Mmosoft.Oops.Controls.ColorTemplate;

namespace Mmosoft.Oops.Controls.Table
{
    [System.Serializable]
    public class TableColors
    {
        // Header
        public string HeaderText { get; set; }
        public string HeaderBg { get; set; }
        public string SeparatedHeaderVerticalLine { get; set; }
        // Data
        public string DataBg { get; set; }
        public string DataText { get; set; }
        public string SeparatedDataLine { get; set; }
        // Data Hover
        public string DataBgHover { get; set; }
        public string DataTextHover { get; set; }
        // Data Selected
        public string DataBgSelected { get; set; }
        public string DataTextSelected { get; set; }
        // Vertical scroll bar        
        public string VerticalScrollThumbBg { get; set; }

        public TableColors()
        {
            HeaderText = TableColor.HeaderText;
            HeaderBg = TableColor.HeaderBg;
            SeparatedHeaderVerticalLine = TableColor.SeparatedHeaderVerticalLine;
            DataBg = TableColor.DataBg;
            DataText = TableColor.DataText;
            SeparatedDataLine = TableColor.SeparatedDataLine;
            DataBgHover = TableColor.DataBgHover;
            DataTextHover = TableColor.DataTextHover;
            DataBgSelected = TableColor.DataBgSelected;
            DataTextSelected = TableColor.DataTextSelected;
            VerticalScrollThumbBg = TableColor.VerticalScrollThumbBg;
        }
    }
}
