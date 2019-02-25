using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Table
{
    [Serializable]
    public class Table<TModel> : Control
    {
        private int HEADER_ROW_HEIGHT = 50;
        private int ROW_HEIGHT = 40;
        private int CELL_LEFT_PADDING = 10;

        private List<TModel> _models;

        // 
        private Font _headerFont;

        // viewport - vertical scrollable table move virtual viewport to the top.
        private int _offsetY;
        private RectangleF _virtualViewPort;
        private RectangleF _viewport;
        private RectangleF _verticalScrollThumb;

        private int _hoveredRowIndex;

        //
        private List<Column> _columns;

        // 
        private List<Cell> _headerRow;
        private List<List<Cell>> _dataRows;

        // Selected rows manipulation
        private List<int> _selectedRows;
        private bool _multipleRowSelected;
        [Browsable(true)]
        public bool MultipleRowsSelected
        {
            get { return _multipleRowSelected; }
            set { _multipleRowSelected = value; if (!_multipleRowSelected) _selectedRows.Clear(); Invalidate(); }
        }

        private TableColors _colors;
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

        public Table()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            Colors = new TableColors();

            _headerFont = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Bold);
            _headerRow = new List<Cell>();
            _dataRows = new List<List<Cell>>();

            _columns = new List<Column>();
            _selectedRows = new List<int>();
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
            if (_columns == null || _columns.Count == 0) return;

            Cell cell = null;
            Column column = null;
            Size txtSize;

            int offsetX = 0;
            int offsetY = 0;

            _headerRow = new List<Cell>();
            for (int colId = 0, colCount = _columns.Count; colId < colCount; colId++)
            {
                column = _columns[colId];
                txtSize = TextRenderer.MeasureText(column.Title, _headerFont);

                cell = new Cell()
                {
                    Text = column.Title,
                    CellBoundary = new Rectangle(offsetX, offsetY, column.Width, HEADER_ROW_HEIGHT),
                    TextBoundary = new Rectangle(offsetX + CELL_LEFT_PADDING, offsetY + (HEADER_ROW_HEIGHT - txtSize.Height) / 2, txtSize.Width, txtSize.Height),
                };
                _headerRow.Add(cell);
                offsetX += column.Width;
            }
        }
        private void BuildDataRows()
        {
            int x = 0;
            int y = HEADER_ROW_HEIGHT; // skip header
            var propertyReader = new PropertyDataReader(typeof(TModel));

            for (int rowId = 0, rowCount = _models.Count; rowId < rowCount; rowId++)
            {
                TModel record = _models[rowId];

                List<Cell> row = new List<Cell>();
                Column column = null;
                Cell cell = null;

                Size txtSize;
                for (int colId = 0, colCount = _columns.Count; colId < colCount; colId++)
                {
                    column = _columns[colId];
                    string content = string.Format(column.Format, propertyReader.GetData(record, column.MappingProperty));
                    txtSize = TextRenderer.MeasureText(content, this.Font);
                    cell = new Cell()
                    {
                        Text = content,
                        CellBoundary = new Rectangle(x, y - _offsetY, column.Width, ROW_HEIGHT),
                        TextBoundary = new Rectangle(x + CELL_LEFT_PADDING, y + (ROW_HEIGHT - txtSize.Height) / 2 - _offsetY, column.Width, ROW_HEIGHT)
                    };
                    row.Add(cell);
                    x += column.Width;
                }

                // add row
                _dataRows.Add(row);

                // Move to next row
                x = 0;
                y += ROW_HEIGHT;
            }

            // calc virtual view port
            _virtualViewPort = new RectangleF(0, ROW_HEIGHT, this.Width - 1, (_dataRows.Count) * ROW_HEIGHT);
        }
        private void CalculateRect()
        {
            if (_virtualViewPort.Height > _viewport.Height)
            {
                _verticalScrollThumb = new RectangleF(
                        this.Width - 1 - 5,
                        _viewport.Top + _offsetY * _viewport.Height / _virtualViewPort.Height,
                        5,
                        _viewport.Height * (_viewport.Height / _virtualViewPort.Height));
            }
            else
            {
                _verticalScrollThumb = RectangleF.Empty;
            }
        }
        public void RenderTable()
        {
            BuildHeaderRow();
            BuildDataRows();
            CalculateRect();
            Invalidate();
        }

        //protected override void OnMouseClick(MouseEventArgs e)
        //{
        //    base.OnMouseClick(e);

        //    if (dataCells != null)
        //    {
        //        // check if user click to header row                
        //        Tuple<bool, int> selectedRow;
        //        selectedRow = HitTestRow(headerCells, e.Location);

        //        if (selectedRow.Item1)
        //        {
        //            // reverse
        //            if (columns[selectedRow.Item2] == lastSortedBy)
        //            {
        //                isReverseSortRequested = !isReverseSortRequested;
        //            }
        //            else
        //            {
        //                isReverseSortRequested = false;
        //            }

        //            // sort by column
        //            records.Sort(columns[selectedRow.Item2]);
        //            // 
        //            if (isReverseSortRequested)
        //                records.Reverse();

        //            lastSortedBy = columns[selectedRow.Item2];

        //            CalculateRect();
        //        }

        //        for (int rowId = 0; rowId < dataRows.Count; rowId++)
        //        {
        //            selectedRow = HitTestRow(dataRows[rowId], e.Location);

        //            if (selectedRow.Item1) // hit test
        //            {
        //                if (multipleRowSelected)
        //                {
        //                    if (selectedRows.Contains(rowId))
        //                        selectedRows.Remove(rowId); // de-select row
        //                    else
        //                        selectedRows.Add(rowId); // select row
        //                }
        //                else
        //                {
        //                    if (selectedRows.Count > 0)
        //                    {
        //                        if (selectedRows[0] == rowId)
        //                            selectedRows.RemoveAt(0); // de select
        //                        else
        //                            selectedRows[0] = rowId; // select new row
        //                    }
        //                    else
        //                    {
        //                        selectedRows.Add(rowId); // add/select row
        //                    }
        //                }
        //            }                  
        //        }               

        //        Invalidate();
        //    }
        //}
        private Tuple<bool, int> HitTestRow(List<Cell> row, Point p)
        {
            for (int i = 0; i < row.Count; i++)
            {
                if (row[i].CellBoundary.Contains(p.X, p.Y))
                    return Tuple.Create(true, i);
            }
            return Tuple.Create(false, -1);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            _offsetY -= e.Delta;

            // cannot scroll up when the first item is being shown
            if (_offsetY < 0)
                _offsetY = 0;

            // cannot scroll down when the last item is being shown
            if (_virtualViewPort.Height - _viewport.Height > 0 && _offsetY > _virtualViewPort.Height - _viewport.Height)
                _offsetY = (int)(_virtualViewPort.Height - _viewport.Height);

            // recalculate row position
            CalculateRect();

            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Y < HEADER_ROW_HEIGHT)
                _hoveredRowIndex = -1;
            else
                _hoveredRowIndex = (e.Location.Y + _offsetY - HEADER_ROW_HEIGHT) / ROW_HEIGHT;
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
            _viewport = new Rectangle(0, ROW_HEIGHT, this.Width - 1, this.Height - 1 - ROW_HEIGHT);
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            _headerFont = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Bold);
            Invalidate();
        }
        // 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            // Draw data
            g.SetClip(_viewport);
            if (_dataRows != null)
            {
                List<Cell> row;
                Cell cell;

                // draw non-hover data row
                // index of the first record in view port
                int iMin = (_offsetY) / ROW_HEIGHT;
                // index of the last record in view port
                // TODO: Verify this check 
                int iMax = Math.Min((this.Height + _offsetY) / ROW_HEIGHT, _dataRows.Count);
                bool rowIsSelected;                
                for (int i = iMin; i < iMax; i++)
                {
                    row = _dataRows[i];
                    rowIsSelected = _selectedRows.Contains(i);
                    foreach (var dataCell in row)
                    {
                        if (rowIsSelected)
                        {
                            g.FillRectangle(
                                BrushCreator.CreateSolidBrush(Colors.DataBgSelected),
                                Adjust(dataCell.CellBoundary, 0, -iMin * ROW_HEIGHT)
                                );
                            g.DrawString(
                                dataCell.Text, this.Font, 
                                BrushCreator.CreateSolidBrush(Colors.DataTextSelected),
                                Adjust(dataCell.TextBoundary, 0, -iMin * ROW_HEIGHT)
                                );
                        }
                        else
                        {
                            g.DrawString(
                                dataCell.Text, this.Font, 
                                BrushCreator.CreateSolidBrush(Colors.DataText),
                                Adjust(dataCell.TextBoundary, 0, -iMin * ROW_HEIGHT));
                        }
                    }

                    g.DrawLine(PenCreator.Create(Colors.SeparatedDataLine),
                            new Point(10, row[0].CellBoundary.Bottom - iMin * ROW_HEIGHT),
                            new Point(this.Width - 10, row[0].CellBoundary.Bottom - iMin * ROW_HEIGHT));
                }

                // draw hovered row
                if (0 <= _hoveredRowIndex && _hoveredRowIndex < _dataRows.Count)
                {
                    row = _dataRows[_hoveredRowIndex];
                    for (int i = 0, cellCount = row.Count; i < cellCount; i++)
                    {
                        cell = row[i];
                        g.FillRectangle(BrushCreator.CreateSolidBrush(Colors.DataBgHover), 
                            Adjust(row[i].CellBoundary, 0, - iMin * ROW_HEIGHT));

                        g.DrawString(
                            cell.Text, this.Font, 
                            BrushCreator.CreateSolidBrush(Colors.DataTextHover),
                            Adjust(cell.TextBoundary, 0, -iMin * ROW_HEIGHT));
                    }
                }

                // draw vertical scroll bar if needed
                if (_verticalScrollThumb != Rectangle.Empty)
                {
                    g.FillRectangle(BrushCreator.CreateSolidBrush(Colors.VerticalScrollThumbBg), _verticalScrollThumb);
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
                g.FillRectangle(BrushCreator.CreateSolidBrush(Colors.HeaderBg), _headerRow[i].CellBoundary);
                g.DrawString(cell.Text, _headerFont, BrushCreator.CreateSolidBrush(Colors.HeaderText), cell.TextBoundary);
            }
        }

        // 
        private Rectangle Adjust(Rectangle original, int offsetX, int offsetY)
        {
            var rect = original;
            rect.Offset(offsetX, offsetY);
            return rect;
        }
    }
}