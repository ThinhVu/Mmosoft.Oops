using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Mmosoft.Oops.Controls
{
    public class TableImageGrid : ImageGrid
    {
        private int _rowHeight;
        private bool _mergeColumn;
        private bool _squareSized;
        private ImageGridDisplayMode _displayMode;

        //
        [Browsable(true)]
        public int RowHeight
        {
            get
            {
                return _rowHeight;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Row height must positive");
                if (_rowHeight != value)
                {
                    _rowHeight = value;
                    base.ReDraw();
                }
            }
        }

        [Browsable(true)]
        public bool MergeColumn
        {
            get
            {
                return _mergeColumn;
            }
            set
            {
                if (_mergeColumn != value)
                {
                    _mergeColumn = value;
                    base.ReDraw();
                }
            }
        }

        public bool SquareSized
        {
            get
            {
                return _squareSized;
            }
            set
            {
                if (_squareSized != value)
                {
                    _squareSized = value;
                    base.ReDraw();
                }
            }
        }

        [Browsable(true)]
        public ImageGridDisplayMode DisplayMode
        {
            get
            {
                return _displayMode;
            }
            set
            {
                if (_displayMode != value)
                {
                    _displayMode = value;
                    base.ReDraw();
                }
            } 
        }

        //
        public TableImageGrid()
        {
            _rowHeight = 300;
            _mergeColumn = false;
            _displayMode = ImageGridDisplayMode.StretchImage;
        }

        //
        protected override void ComputePosition(int currentReDrawRequestId)
        {
            var availableWidth = _colWidth;
            var rowHeight = _squareSized ? availableWidth: _rowHeight;
            var slotMgr = new BlockMgr(_column);
            var takenSlots = new List<Block>();
            for (int i = 0; i < _imgs.Count; i++)
            {
                if (currentReDrawRequestId < _redrawRequestId)
                    return;

                if (ImageDisplayModeHelper.IsPortrait(_imgs[i].ClippingRegion))
                {
                    takenSlots.Add(slotMgr.FindAvailableSlot(1));
                }
                else // square or landscape
                {
                    // check if we need more slots for landspace image
                    float imageRatio = 1f * _imgs[i].Original.Width / _imgs[i].Original.Height;
                    int slotNeeded = MergeColumn ? (int)Math.Round(imageRatio, MidpointRounding.AwayFromZero) : 1;
                    takenSlots.Add(slotMgr.FindAvailableSlot(slotNeeded));
                }
            }

            // converting slots to specific position in grid
            for (int i = 0; i < takenSlots.Count; i++)
            {
                if (currentReDrawRequestId < _redrawRequestId)
                    return;

                Img iw = _imgs[i];
                Block slot = takenSlots[i];
                iw.ClippingRegion = new Rectangle
                {
                    X = (slot.SlotIndex + 1) * _gutter + slot.SlotIndex * availableWidth,
                    Y = (slot.LaneIndex + 1) * _gutter + slot.LaneIndex * rowHeight - _offsetY,
                    Width = (slot.SlotNeeded - 1) * _gutter + slot.SlotNeeded * availableWidth,
                    Height = rowHeight
                };
            }

            _virtualHeight = (slotMgr.LaneCount * rowHeight) + (slotMgr.LaneCount - 1) * _gutter;
        }
        protected override void PaintImages(Graphics g, IEnumerable<Img> images)
        {
            var pickedDrawRegion = Rectangle.Empty;
            var drawRegion = Rectangle.Empty;
            foreach (Img image in images)
            {
                if (_dragItem == null || _dragItem.ItemRef != image)
                {
                    // translate draw region
                    switch (DisplayMode)
                    {
                        case ImageGridDisplayMode.StretchImage:
                            drawRegion = ImageDisplayModeHelper.GetImageRect(
                                image.DrawingRegion,
                                new Rectangle(0, 0, image.Original.Width, image.Original.Height),
                                Mmosoft.Oops.Controls.DisplayMode.StretchImage);
                            break;
                        case ImageGridDisplayMode.ScaleLossCenter:
                            drawRegion = ImageDisplayModeHelper.GetImageRect(
                                image.DrawingRegion,
                                new Rectangle(0, 0, image.Original.Width, image.Original.Height),
                                Mmosoft.Oops.Controls.DisplayMode.ScaleLossCenter);
                            break;
                    }

                    // set clip to image boundary to clipped outside edges
                    g.SetClip(image.ClippingRegion);
                    g.DrawImage(image.Resized, drawRegion);
                }
            }

            // draw floating picked item
            if (_dragItem != null)
            {
                switch (DisplayMode)
                {
                    case ImageGridDisplayMode.StretchImage:
                        drawRegion = ImageDisplayModeHelper.GetImageRect(
                            _dragItem.Boundary,
                            new Rectangle(0, 0, _dragItem.Image.Width, _dragItem.Image.Height),
                            Mmosoft.Oops.Controls.DisplayMode.StretchImage);
                        break;
                    case ImageGridDisplayMode.ScaleLossCenter:
                        drawRegion = ImageDisplayModeHelper.GetImageRect(
                            _dragItem.Boundary,
                            new Rectangle(0, 0, _dragItem.Image.Width, _dragItem.Image.Height),
                            Mmosoft.Oops.Controls.DisplayMode.ScaleLossCenter);
                        break;
                }

                g.SetClip(_dragItem.Boundary);
                g.DrawImage(_dragItem.Image, drawRegion);
            }
        }

        // 
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
            public int LaneIndex;
            public int SlotIndex;
            public int SlotNeeded;
        }
    }
}
