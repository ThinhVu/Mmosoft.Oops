using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Mmosoft.Oops.Animation;

namespace Mmosoft.Oops.Controls
{
    // TODO: Separate layout engine with ImageGrid
    public class ImageGrid : Control
    {
        // -- private members
        // data
        private List<ImageWrapper> _items;
        // layout
        private LayoutSettingBase _layoutSetting;        
        private int _virtualHeight;        
        private int _offsetY;
        // drag drop
        private PickedItem _pickedItem;

        // 
        private int _selectedIndex;

        // -- properties
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
                if (0 <= value && value < _items.Count)
                {
                    _selectedIndex = value;
                    // perform click
                    if (OnItemClicked != null)
                    {
                        OnItemClicked(this, new ImageGridItemClickedEventArgs()
                        {
                            Index = value,
                            Image = _items[value].Image
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
                return _items.Count; 
            }
        }

        [Browsable(false)]
        [Description("Get or set grid layout")]
        public LayoutSettingBase LayoutSettings 
        { 
            get 
            { 
                return _layoutSetting; 
            } 
            set 
            { 
                _layoutSetting = value;
                _offsetY = 0;
                ReDraw();
            } 
        }

        // events
        public event ImageGridItemClickedEventHandler OnItemClicked;

        // methods
        public ImageGrid()
        {
            _items = new List<ImageWrapper>();            
            //
            DoubleBuffered = true;
        }

        // public methods
        public void Clear()
        {
            foreach (var imageWrapper in _items)
                imageWrapper.Image.Dispose();
            _items = new List<ImageWrapper>();
            _offsetY = 0;
            ReDraw();
        }
        public void Add(Image image)
        {
            _items.Add(new ImageWrapper(image));
            ReDraw();
        }

        // calculate and drawing images
        public void ReDraw()
        {
            _virtualHeight = 0;
            if (_items == null || _items.Count == 0)
                return;

            if (_layoutSetting is FillToTop)
            {
                ApplyFillToTop();
            }
            else
            {
                ApplyFillToBlock();
            }
    
            Invalidate();
        }

        // event handlers
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int pickedIndex = GetHotItemIndex(e.Location);
            if (pickedIndex > -1)
            {
                _pickedItem = new PickedItem(_items[pickedIndex], e.Location);
                _pickedItem.Index = pickedIndex;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Location != _pickedItem.PickedLocation) // dragging
            {
                int droppedIndex = GetHotItemIndex(e.Location);
                if (droppedIndex == -1 && this.Bounds.Contains(e.Location))
                    droppedIndex = _items.Count - 1;
                if (droppedIndex != -1)
                {
                    // swap if picked != dropped
                    if (droppedIndex != _pickedItem.Index)
                    {
                        _items[_pickedItem.Index] = _items[droppedIndex];
                        _items[droppedIndex] = _pickedItem.ItemRef;
                    }
                }
                ReDraw();
            }
            else // click
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    this.SelectedIndex = GetHotItemIndex(e.Location);
                }
            }

            _pickedItem = null;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = GetHotItemIndex(e.Location) < 0 ? Cursors.Default : Cursors.Hand;
            if (_pickedItem != null)
            {
                _pickedItem.Move(e.Location);
                ReDraw();
            }
        }                
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_virtualHeight < this.Height)
                return;

            var newOffsetY = _offsetY - e.Delta;
            //
            if (newOffsetY < 0)
                newOffsetY = 0;

            // 
            if (newOffsetY > _virtualHeight - this.Height)
                newOffsetY = _virtualHeight - this.Height;

            _offsetY = newOffsetY;            
            ReDraw();
        }
        //
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ReDraw();
        }
        //
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            SolidBrush br = new SolidBrush(this.BackColor);
            g.FillRectangle(br, this.ClientRectangle);
            if (DesignMode)
            {
                g.DrawString(this.Name + " control doesn't provide design time support", this.Font, Brushes.Black, new Point(0, 0));
                g.DrawRectangle(Pens.Black, this.ClientRectangle.ChangeSizeRelative(-1, -1));
            }
            else
            {                
                if (_layoutSetting is FillToTop)
                {
                    foreach (ImageWrapper item in GetImagesInView())
                    {
                        if (_pickedItem == null || _pickedItem.ItemRef != item)
                            g.DrawImage(item.Image, item.Boundary);
                    }

                    // draw floating picked item
                    if (_pickedItem != null)
                    {
                        g.DrawImage(_pickedItem.Image, _pickedItem.Boundary);
                    }
                }
                else
                {
                    var layout = (FillToBlock) _layoutSetting;
                    var pickedDrawRegion = Rectangle.Empty;
                    var drawRegion = Rectangle.Empty;
                    foreach (ImageWrapper item in GetImagesInView())
                    {
                        if (_pickedItem == null || _pickedItem.ItemRef != item)
                        {
                            // translate draw region
                            switch (layout.DisplayMode)
                            {
                                case ImageGridDisplayMode.StretchImage:
                                    drawRegion = ImageDisplayModeHelper.GetImageRect(item.Boundary, new Rectangle(0, 0, item.Image.Width, item.Image.Height), DisplayMode.StretchImage);
                                    break;
                                case ImageGridDisplayMode.ScaleLossCenter:
                                    drawRegion = ImageDisplayModeHelper.GetImageRect(item.Boundary, new Rectangle(0, 0, item.Image.Width, item.Image.Height), DisplayMode.ScaleLossCenter);
                                    break;
                            }

                            // set clip to image boundary to clipped outside edges
                            g.SetClip(item.Boundary);
                            g.DrawImage(item.Image, drawRegion);
                        }
                    }

                    // draw floating picked item
                    if (_pickedItem != null)
                    {
                        g.SetClip(this.ClientRectangle);
                        g.DrawImage(_pickedItem.Image, _pickedItem.Boundary);
                    }
                }
            }

            br.Dispose();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Clear();
            }
        }
        
        // --- private methods
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
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Boundary.Contains(location))
                    return i;
            }
            return -1;
        }
        private IEnumerable<ImageWrapper> GetImagesInView()
        {
            foreach (var iw in _items)
            {
                if (iw.Boundary.IntersectsWith(this.ClientRectangle))
                    yield return iw;
            }
        }

        // styling
        private void ApplyFillToTop()
        {
            var availableWidth = GetColumnWidth();

            var columnTops = new int[_layoutSetting.Column];
            for (int i = 0; i < columnTops.Length; i++)
                columnTops[i] = _layoutSetting.Gutter;

            int left, top;
            List<int> colId;
            for (int i = 0; i < _items.Count; i++)
            {
                GetMinTopAndColumnIndex(columnTops, out top, out colId);
                left = _layoutSetting.Gutter * (1 + colId[0]) + availableWidth * colId[0];

                ImageWrapper iw = _items[i];
                int actualImageWidth = iw.Image.Width;
                int actualImageHeight = iw.Image.Height;
                int availableHeight = (int)((availableWidth * 1f / actualImageWidth) * actualImageHeight);
                iw.Boundary = new Rectangle(left, top - _offsetY, availableWidth, availableHeight);

                // next image in the same column will be drawned at "columnTops[colId] + availableHeight + MGutter" position
                columnTops[colId[0]] += availableHeight + _layoutSetting.Gutter;

                // increase virtual height
                if (_virtualHeight < columnTops[colId[0]])
                    _virtualHeight = columnTops[colId[0]];
            }
        }
        private void ApplyFillToBlock()
        {
            var availableWidth = GetColumnWidth();
            var layout = (FillToBlock)_layoutSetting;
            var slotMgr = new BlockMgr(_layoutSetting.Column);
            var takenSlots = new List<Block>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (ImageDisplayModeHelper.IsPortrait(_items[i].Boundary))
                {
                    takenSlots.Add(slotMgr.FindAvailableSlot(1));
                }
                else // square or landscape
                {
                    // check if we need more slots for landspace image
                    float imageRatio = 1f * _items[i].Image.Width / _items[i].Image.Height;
                    int slotNeeded = layout.MergeColumn ? (int)Math.Round(imageRatio, MidpointRounding.AwayFromZero) : 1;
                    takenSlots.Add(slotMgr.FindAvailableSlot(slotNeeded));
                }
            }

            // converting slots to specific position in grid
            for (int i = 0; i < takenSlots.Count; i++)
            {
                ImageWrapper iw = _items[i];
                Block slot = takenSlots[i];
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
            return (int)((this.Width - 1 - (_layoutSetting.Column + 1) * _layoutSetting.Gutter * 1f) / _layoutSetting.Column);
        }

        // Helper classes
        class BlockMgr
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

            public BlockMgr(int slotPerLane)
            {
                _lanes = new List<bool[]>();
                _slotPerLane = slotPerLane;
            }

            public Block FindAvailableSlot(int slotsNeeded)
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
                            return new Block { LaneIndex = laneIndex, SlotIndex = slotIndex, SlotNeeded = slotsNeeded };
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
        class Block
        {
            public int LaneIndex { get; set; }
            public int SlotIndex { get; set; }
            public int SlotNeeded { get; set; }
        }
        class PickedItem
        {
            private Rectangle _originBoundary;
            //
            public Point PickedLocation;
            public ImageWrapper ItemRef;
            public Rectangle Boundary;
            public Image Image;
            public int Index;
            
            public PickedItem(ImageWrapper itemRef, Point pickedPosition)
            {
                ItemRef = itemRef;
                Image = itemRef.Image;
                Boundary = itemRef.Boundary;

                //
                PickedLocation = pickedPosition;
                _originBoundary = itemRef.Boundary;
            }

            public void Move(Point p)
            {
                Boundary.X = _originBoundary.X + (p.X - PickedLocation.X);
                Boundary.Y = _originBoundary.Y + (p.Y - PickedLocation.Y);
            }
        }
    }
}