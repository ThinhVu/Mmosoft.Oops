using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Mmosoft.Oops.Animation;

namespace Mmosoft.Oops.Controls
{
    public class ImageGrid : Control
    {
        private List<ImageWrapper> _imgWrappers;
        private ImageGridLayoutBase _gridLayout;
        private int _virtualHeight;
        private int _offsetY;
        // private data member <which has corresponding public property>
        private int _selectedIndex;

        [Browsable(true)]
        [Description("Get or set index of selected image")]
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (0 <= value && value < _imgWrappers.Count)
                {
                    _selectedIndex = value;
                    // perform click
                    if (OnItemClicked != null)
                    {
                        OnItemClicked(this, new ImageGridItemClickedEventArgs()
                        {
                            Index = value,
                            Image = _imgWrappers[value].Image
                        });
                    }
                }
            }
        }

        [Browsable(true)]
        [Description("Get total image number")]
        public int Count 
        { 
            get 
            { 
                return _imgWrappers.Count; 
            }
        }

        [Browsable(false)]
        [Description("Get or set grid layout")]
        public ImageGridLayoutBase GridLayout 
        { 
            get 
            { 
                return _gridLayout; 
            } 
            set 
            { 
                _gridLayout = value;
                ReDraw(); 
            } 
        }

        // events
        public event ImageGridItemClickedEventHandler OnItemClicked;

        // methods
        public ImageGrid()
        {
            _imgWrappers = new List<ImageWrapper>();
            //
            DoubleBuffered = true;
        }

        //
        public void Clear()
        {
            foreach (var imageWrapper in _imgWrappers)
                imageWrapper.Image.Dispose();
            _imgWrappers = new List<ImageWrapper>();
            _offsetY = 0;
            ReDraw();
        }
        public void Add(Image image)
        {
            _imgWrappers.Add(new ImageWrapper(image));
            ReDraw();
        }
        public void ReDraw()
        {
            _virtualHeight = 0;
            if (_imgWrappers == null || _imgWrappers.Count == 0)
                return;

            if (_gridLayout is FillToTopLayout)
            {
                ApplyFillToTopLayout();
            }
            else
            {
                ApplyTableLayout();
            }
    
            Invalidate();
        }

        private void ApplyFillToTopLayout()
        {
            var availableWidth = GetColumnWidth();

            var columnTops = new int[_gridLayout.Column];
            for (int i = 0; i < columnTops.Length; i++)
                columnTops[i] = _gridLayout.Gutter;

            int left, top;
            List<int> colId;
            for (int i = 0; i < _imgWrappers.Count; i++)
            {
                GetMinTopAndColumnIndex(columnTops, out top, out colId);
                left = _gridLayout.Gutter * (1 + colId[0]) + availableWidth * colId[0];

                ImageWrapper iw = _imgWrappers[i];
                int actualImageWidth = iw.Image.Width;
                int actualImageHeight = iw.Image.Height;
                int availableHeight = (int)((availableWidth * 1f / actualImageWidth) * actualImageHeight);
                iw.Boundary = new Rectangle(left, top - _offsetY, availableWidth, availableHeight);

                // next image in the same column will be drawned at "columnTops[colId] + availableHeight + MGutter" position
                columnTops[colId[0]] += availableHeight + _gridLayout.Gutter;

                // increase virtual height
                if (_virtualHeight < columnTops[colId[0]])
                    _virtualHeight = columnTops[colId[0]];
            }
        }

        private void ApplyTableLayout()
        {
            var availableWidth = GetColumnWidth();
            var layout = (TableLayout)_gridLayout;
            var slotMgr = new SlotMgr(_gridLayout.Column);
            var takenSlots = new List<Slot>();
            for (int i = 0; i < _imgWrappers.Count; i++)
            {
                if (ImageDisplayModeHelper.IsPortrait(_imgWrappers[i].Boundary))
                {
                    takenSlots.Add(slotMgr.FindAvailableSlot(1));
                }
                else // square or landscape
                {
                    // check if we need more slots for landspace image
                    float imageRatio = 1f * _imgWrappers[i].Image.Width / _imgWrappers[i].Image.Height;
                    int slotNeeded = layout.MergeColumn ? (int)Math.Round(imageRatio, MidpointRounding.AwayFromZero) : 1;
                    takenSlots.Add(slotMgr.FindAvailableSlot(slotNeeded));
                }
            }

            // converting slots to specific position in grid
            for (int i = 0; i < takenSlots.Count; i++)
            {
                ImageWrapper iw = _imgWrappers[i];
                Slot slot = takenSlots[i];
                iw.Boundary = new Rectangle
                {
                    X = (slot.SlotIndex + 1) * layout.Gutter + slot.SlotIndex * availableWidth,
                    Y = (slot.LaneIndex + 1) * layout.Gutter + slot.LaneIndex * layout.RowHeight - _offsetY,
                    Width = (slot.SlotNeeded - 1) * layout.Gutter + slot.SlotNeeded * availableWidth,
                    Height = layout.RowHeight
                };
            }

            _virtualHeight = (slotMgr.LaneCount * layout.RowHeight) + (slotMgr.LaneCount - 1) * layout.Gutter;
        }

        private int GetColumnWidth()
        {
            return (int)((this.Width - 1 - (_gridLayout.Column + 1) * _gridLayout.Gutter * 1f) / _gridLayout.Column);
        }
        //
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ReDraw();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = GetHotItemIndex(e.Location) < 0 ? Cursors.Default : Cursors.Hand;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.SelectedIndex = GetHotItemIndex(e.Location);
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            _offsetY -= e.Delta;
            //
            if (_offsetY < 0)
                _offsetY = 0;

            // 
            if (_offsetY > _virtualHeight - this.Height)
                _offsetY = _virtualHeight - this.Height;

            ReDraw();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            SolidBrush br = new SolidBrush(this.BackColor);
            g.FillRectangle(br, this.ClientRectangle);
            if (DesignMode)
            {
                g.DrawString(this.Name + " control doesn't provide design time support", this.Font, Brushes.Black, new Point(0, 0));
            }
            else
            {
                if (_gridLayout is FillToTopLayout)
                {
                    foreach (ImageWrapper i in ImagesInView())
                    {                    
                        g.DrawImage(i.Image, i.Boundary);
                    }                 
                }
                else
                {
                    var layout = (TableLayout) _gridLayout;
                    var r = Rectangle.Empty;
                    foreach (ImageWrapper i in ImagesInView())
                    {
                        switch (layout.DisplayMode)
                        {
                            case ImageGridDisplayMode.StretchImage:
                                r = ImageDisplayModeHelper.GetImageRect(
                                       i.Boundary,
                                       new Rectangle(0, 0, i.Image.Width, i.Image.Height),
                                       DisplayMode.StretchImage);
                                break;
                            case ImageGridDisplayMode.ScaleLossCenter:
                                r = ImageDisplayModeHelper.GetImageRect(
                                        i.Boundary,
                                        new Rectangle(0, 0, i.Image.Width, i.Image.Height),
                                        DisplayMode.ScaleLossCenter);
                                break;
                        }

                        g.SetClip(i.Boundary);
                        g.DrawImage(i.Image, r);
                    }

                    
                }
            }

            br.Dispose();
        }
        
        private void GetMinTopAndColumnIndex(int[] num, out int value, out List<int> indexes)
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
        private int GetHotItemIndex(Point location)
        {
            for (int i = 0; i < _imgWrappers.Count; i++)
            {
                if (_imgWrappers[i].Boundary.Contains(location))
                    return i;
            }
            return -1;
        }
        private IEnumerable<ImageWrapper> ImagesInView()
        {
            foreach (var iw in _imgWrappers)
            {
                if (iw.Boundary.IntersectsWith(this.ClientRectangle))
                    yield return iw;
            }
        }
    }

    public class SlotMgr
    {
        private List<bool[]> _lanes;
        private int _slotPerLane;

        /// <summary>
        /// Return number of lane
        /// </summary>
        public int LaneCount
        { 
            get 
            { 
                return _lanes.Count; 
            } 
        }

        public SlotMgr(int slotPerLane)
        {
            _lanes = new List<bool[]>();
            _slotPerLane = slotPerLane;
        }

        public Slot FindAvailableSlot(int slotsNeeded)
        {
            // if column needed is large than available col
            // then reduce column needed to _col
            if (slotsNeeded > _slotPerLane)
                slotsNeeded = _slotPerLane;

            // find in existed slots
            for (int laneIndex = 0; laneIndex < _lanes.Count; laneIndex++)
            {
                for (int slotIndex = 0; slotIndex < _slotPerLane; slotIndex++)
                {
                    if (IsSlotAvailable(laneIndex, slotIndex, slotsNeeded))
                    {
                        TakeSlot(laneIndex, slotIndex, slotsNeeded);
                        // then return taken slots
                        return new Slot { LaneIndex = laneIndex, SlotIndex = slotIndex, SlotNeeded = slotsNeeded };
                    }
                }
            }

            // if there are no available slots
            // add more row then find again
            AddNewLane();
            return FindAvailableSlot(slotsNeeded);
        }

        // Add new lane, each lane contains _col slots
        private void AddNewLane()
        {
            _lanes.Add(new bool[_slotPerLane]);
        }

        private bool IsSlotAvailable(int laneIndex, int slotIndex, int slotsNeeded)
        {
            bool[] lane = _lanes[laneIndex];
            int availColumn = 0;
            for (int i = slotIndex; i < lane.Length && availColumn < slotsNeeded; i++)
            {
                if (lane[i] == false)
                    availColumn++;
                else
                    break;
            }
            return availColumn == slotsNeeded;
        }

        private void TakeSlot(int laneIndex, int slotIndex, int slotsNeeded)
        {
            bool[] lane = _lanes[laneIndex];
            int slotTaken = 0;
            for (int i = slotIndex; i < lane.Length && slotTaken < slotsNeeded; i++)
            {
                lane[i] = true;
                slotTaken++;
            }
        }

        public override string ToString()
        {
            List<string> matrix = new List<string>();
            for (int i = 0; i < _lanes.Count; i++)
            {
                matrix.Add(string.Join(" ", _lanes[i].ToArray()));
            }

            return string.Join(Environment.NewLine, matrix);
        }
    }

    public class Slot
    {
        public int LaneIndex { get; set; }
        public int SlotIndex { get; set; }
        public int SlotNeeded { get; set; }
    }
}