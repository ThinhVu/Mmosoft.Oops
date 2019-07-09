using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Table
{
    [Serializable]
    public class Table<TModel> : Control
    {
        private int HEADER_ROW_HEIGHT = 30;
        private int ROW_HEIGHT = 25;
        private int SEPARATE_LINE_HEIGHT = 1;
        private int VERTICAL_SCROLL_BAR_WIDTH = 5;

        private List<TModel> _models;

        // viewport - vertical scrollable table move virtual viewport to the top.
        private int _offsetY;
        private RectangleF _virtualViewPortRect;
        private RectangleF _viewportRect;
        private RectangleF _verticalScrollThumbRect;

        private int _hoveredRowIndex;

        //
        private List<Column> _columns;

        // 
        private List<Cell> _headerRow;
        private List<List<Cell>> _dataRows;

        // Selected rows manipulation
        private List<int> _selectedRows;
        private bool _multipleRowSelected;
        private TableColors _colors;

        // resources
        private SolidBrush _dataBgSelectedBr;
        private SolidBrush _dataTextSelectedBr;
        private SolidBrush _dataBgBr;
        private SolidBrush _dataTextBr;
        private SolidBrush _dataBgHoverBr;
        private SolidBrush _dataTextHoverBr;
        private SolidBrush _headerBgBr;
        private SolidBrush _headerTextBr;
        private SolidBrush _verticalScrollThumbBgBr;
        private Pen _separateLinePen;
        private StringFormat _stringFormat;
        private Font _headerFont;

        //
        [Browsable(true)]
        public bool MultipleRowsSelected
        {
            get { return _multipleRowSelected; }
            set { _multipleRowSelected = value; if (!_multipleRowSelected) _selectedRows.Clear(); Invalidate(); }
        }
        
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableColors Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        public Font HeaderFont { get { return _headerFont; } set { _headerFont = value; Invalidate(); } }

        //
        public Table()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;

            //
            Colors = new TableColors();
            //
            _headerRow = new List<Cell>();
            _dataRows = new List<List<Cell>>();
            _columns = new List<Column>();
            //
            _selectedRows = new List<int>();            
            //
            _headerFont = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Bold);
            _stringFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            //
            _dataBgSelectedBr = BrushCreator.CreateSolidBrush(Colors.DataBgSelected);
            _dataTextSelectedBr = BrushCreator.CreateSolidBrush(Colors.DataTextSelected);            
            _dataBgBr = BrushCreator.CreateSolidBrush(Colors.DataBg);
            _dataTextBr = BrushCreator.CreateSolidBrush(Colors.DataText);
            _dataBgHoverBr = BrushCreator.CreateSolidBrush(Colors.DataBgHover);
            _dataTextHoverBr = BrushCreator.CreateSolidBrush(Colors.DataTextHover);
            _headerBgBr = BrushCreator.CreateSolidBrush(Colors.HeaderBg);
            _headerTextBr = BrushCreator.CreateSolidBrush(Colors.HeaderText);
            _verticalScrollThumbBgBr = BrushCreator.CreateSolidBrush(Colors.VerticalScrollThumbBg);
            _separateLinePen = PenCreator.Create(Colors.SeparatedDataLine);
        }

        public void AddColumns(List<Column> columns)
        {
            this._columns = columns;
        }
        public void AddModels(List<TModel> models)
        {
            this._models = models;
        }

        private void BuildHeaderRow()
        {
            if (_columns == null || _columns.Count == 0)
                return;

            _headerRow = new List<Cell>();

            Cell cell = null;
            Column column = null;
            Size txtSize;

            int offsetX = 0;
            for (int colId = 0, colCount = _columns.Count; colId < colCount; colId++)
            {
                column = _columns[colId];
                txtSize = TextRenderer.MeasureText(column.Title, _headerFont);
                cell = new Cell(column.Title, new Rectangle(offsetX, 0, column.Width, HEADER_ROW_HEIGHT));
                _headerRow.Add(cell);
                offsetX += column.Width;
            }
        }
        private void BuildDataRows()
        {
            _dataRows = new List<List<Cell>>();

            int x = 0;
            int y = HEADER_ROW_HEIGHT; // skip header
            var propReader = new PropertyDataReader(typeof(TModel));

            for (int rowId = 0, rowCount = _models.Count; rowId < rowCount; rowId++)
            {
                TModel record = _models[rowId];

                var row = new List<Cell>();
                var column = default(Column);
                var cell = default(Cell);

                Size txtSize;
                for (int colId = 0, colCount = _columns.Count; colId < colCount; colId++)
                {
                    column = _columns[colId];
                    string content = string.Format(column.Format, propReader.GetData(record, column.MappingProperty));
                    txtSize = TextRenderer.MeasureText(content, this.Font);
                    cell = new Cell(content, new Rectangle(x, y - _offsetY, column.Width, ROW_HEIGHT));
                    row.Add(cell);
                    x += column.Width;
                }

                // add row
                _dataRows.Add(row);

                // Move to next row
                x = 0;
                y += ROW_HEIGHT + SEPARATE_LINE_HEIGHT;
            }
            // calc virtual view port
            _virtualViewPortRect = new RectangleF(
                0, 
                HEADER_ROW_HEIGHT, 
                this.Width,
                (_dataRows.Count) * (ROW_HEIGHT + SEPARATE_LINE_HEIGHT));
        }
        private void UpdateVerticleScrollBar()
        {
            if (_virtualViewPortRect.Height > _viewportRect.Height)
            {
                _verticalScrollThumbRect = new RectangleF(
                        this.Width - VERTICAL_SCROLL_BAR_WIDTH,
                        _viewportRect.Top + _offsetY * _viewportRect.Height / _virtualViewPortRect.Height,
                        VERTICAL_SCROLL_BAR_WIDTH,
                        _viewportRect.Height * (_viewportRect.Height / _virtualViewPortRect.Height));
            }
            else
            {
                _verticalScrollThumbRect = RectangleF.Empty;
            }
        }
        public void RenderTable()
        {
            BuildHeaderRow();
            BuildDataRows();
            UpdateVerticleScrollBar();
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Y <= HEADER_ROW_HEIGHT)
                return;

            if (_dataRows != null)
            {
                for (int rowId = 0; rowId < _dataRows.Count; rowId++)
                {
                    var selectedRow = HitTestRow(_dataRows[rowId], e.Location);

                    if (selectedRow.Item1) // hit test
                    {
                        if (_multipleRowSelected)
                        {
                            if (_selectedRows.Contains(rowId))
                                _selectedRows.Remove(rowId); // de-select row
                            else
                                _selectedRows.Add(rowId); // select row
                        }
                        else
                        {
                            if (_selectedRows.Count > 0)
                            {
                                if (_selectedRows[0] == rowId)
                                    _selectedRows.RemoveAt(0); // de select
                                else
                                    _selectedRows[0] = rowId; // select new row
                            }
                            else
                            {
                                _selectedRows.Add(rowId); // add/select row
                            }
                        }
                    }
                }

                Invalidate();
            }
        }
        private Tuple<bool, int> HitTestRow(List<Cell> row, Point p)
        {
            for (int i = 0; i < row.Count; i++)
            {
                if (row[i].Bounds.Contains(p))
                    return Tuple.Create(true, i);
            }
            return Tuple.Create(false, -1);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_virtualViewPortRect.Height < _viewportRect.Height)
                return;

            var offsetY = _offsetY - e.Delta;

            // cannot scroll up when the first item is being shown
            if (offsetY < 0)
                offsetY = 0;

            // cannot scroll down when the last item is being shown
            if (offsetY > _virtualViewPortRect.Height - _viewportRect.Height)
                offsetY = (int)(_virtualViewPortRect.Height - _viewportRect.Height);

            _offsetY = offsetY;

            BuildDataRows();
            UpdateVerticleScrollBar();
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Y < HEADER_ROW_HEIGHT)
            {
                _hoveredRowIndex = -1;
            }
            else
            {
                for (int i = 0; i < _dataRows.Count; i++)
                {
                    var hitTest = HitTestRow(_dataRows[i], e.Location);
                    if (hitTest.Item1)
                    {
                        _hoveredRowIndex = i;
                        break;
                    }
                }
            }
            Invalidate();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Cursor = Cursors.Hand;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Cursor = Cursors.Default;
            _hoveredRowIndex = -1;
            Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _viewportRect = new Rectangle(0, HEADER_ROW_HEIGHT, this.Width, this.Height - HEADER_ROW_HEIGHT);
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            _headerFont = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Bold);
            Invalidate();
        }

        private IEnumerable<Tuple<int, List<Cell>>> GetRowsInViewport()
        {
            for (int i = 0; i < _dataRows.Count; i++)
            {
                if (_viewportRect.IntersectsWith(_dataRows[i][0].Bounds))
                    yield return Tuple.Create(i, _dataRows[i]);
            }
        }
        // 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            // Draw data
            g.SetClip(_viewportRect);
            if (_dataRows != null)
            {
                bool rowIsSelected;
                List<Cell> row;
                Cell cell;

                foreach (var item in GetRowsInViewport())
                {
                    row = item.Item2;
                    rowIsSelected = _selectedRows.Contains(item.Item1);
                    foreach (var dataCell in row)
                    {
                        g.FillRectangle(rowIsSelected ? _dataBgSelectedBr : _dataBgBr, dataCell.Bounds);
                        g.DrawString(dataCell.Text, this.Font, rowIsSelected? _dataTextSelectedBr : _dataTextBr, dataCell.Bounds, _stringFormat);
                    }
                    // draw separate line
                    g.DrawLine(_separateLinePen, new Point(0, row[0].Bounds.Bottom), new Point(this.Width, row[0].Bounds.Bottom));
                }
               

                // draw hovered row
                if (0 <= _hoveredRowIndex && _hoveredRowIndex < _dataRows.Count)
                {
                    row = _dataRows[_hoveredRowIndex];
                    for (int i = 0, cellCount = row.Count; i < cellCount; i++)
                    {
                        cell = row[i];
                        g.FillRectangle(_dataBgHoverBr, row[i].Bounds);
                        g.DrawString(cell.Text, this.Font, _dataTextHoverBr, cell.Bounds, _stringFormat);
                    }
                }

                // draw vertical scroll bar if needed
                if (_verticalScrollThumbRect != Rectangle.Empty)
                {
                    g.FillRectangle(_verticalScrollThumbBgBr, _verticalScrollThumbRect);
                }
            }

            // draw header
            g.SetClip(this.ClientRectangle);
            if (_headerRow == null || _headerRow.Count == 0)
                return;
            for (int i = 0, cellCount = _headerRow.Count; i < cellCount; i++)
            {
                Cell cell = _headerRow[i];
                //
                g.FillRectangle(_headerBgBr, _headerRow[i].Bounds);
                g.DrawString(cell.Text, _headerFont, _headerTextBr, cell.Bounds, _stringFormat);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _dataBgSelectedBr.Dispose();
                _dataTextSelectedBr.Dispose();
                _dataBgBr.Dispose();
                _dataTextBr.Dispose();
                _dataBgHoverBr.Dispose();
                _dataTextHoverBr.Dispose();
                _headerBgBr.Dispose();
                _headerTextBr.Dispose();
                //
                _separateLinePen.Dispose();

                _stringFormat.Dispose();
                _headerFont.Dispose();
            }
        }
    }
}