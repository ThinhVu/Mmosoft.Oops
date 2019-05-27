namespace Mmosoft.Oops.Controls.ColorTemplate
{
    public static class ButtonColor
    {
        // Default
        public static string Bg = "255, 204, 204, 204";
        public static string Border = "255, 204, 204, 204";
        public static string Text = "255, 0, 0, 0";

        // Disabled
        public static string BgDisabled = "255, 204, 204, 204";
        public static string BorderDisabled = "255, 204, 204, 204";
        public static string TextDisabled = "255, 145, 145, 145";

        // Hovered
        public static string BgHovered = "255, 204, 204, 204";
        public static string BorderHovered = "255, 122, 122, 122";
        public static string TextHovered = "255, 0, 0, 0";

        // Focused
        public static string BgFocused = "255, 153, 153, 153";
        public static string BorderFocused = "255, 195, 195, 195";
        public static string TextFocused = "255, 0, 0, 0";
    }

    public static class ToogleButtonColors
    {
        // background color
        public static string Bg = "#B";
        public static string BgDisabled = "#0";
        public static string BgHovered = "#C";
        public static string BgChecked = "#C";

        // border color
        public static string Border = "#8";
        public static string BorderDisabled = "#0";
        public static string BorderHovered = "#9";
        public static string BorderChecked = "#8";

        // text color
        public static string Dot = "#FF";
        public static string DotDisabled = "#0";
        public static string DotHovered = "#FF";
        public static string DotChecked = "#FF";
    }

    public static class LineColors
    {
        public static string LineColor = "#0";
    }

    public static class TableColor
    {
        // Header
        public static string HeaderText = "#0";
        public static string HeaderBg = "#F";
        public static string SeparatedHeaderVerticalLine = "#F4";
        // Data
        public static string DataBg = "#AF";
        public static string DataText = "#0";
        // Data Hover
        public static string DataBgHover = "#AF";
        public static string DataTextHover = "#0";
        // Data Selected
        public static string DataBgSelected = "#DF";
        public static string DataTextSelected = "#0";
        // Vertical scroll bar        
        public static string VerticalScrollThumbBg = "#80";
        public static string SeparatedDataLine = "#FA";
    }

    public static class ProgressBarColors
    {
        public static string BarBackgound = "#A";
        public static string BarBackgoundDisabled = "#0";
        public static string BarBackgoundHovered = "#B";

        public static string ProgressBackgound = "#8";
        public static string ProgressBackgoundDisabled = "#0";
        public static string ProgressBackgoundHovered = "#9";
    }
}