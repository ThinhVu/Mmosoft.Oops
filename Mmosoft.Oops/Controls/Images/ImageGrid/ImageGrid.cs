using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls
{
    public abstract class ImageGrid : Control
    {
        // -- private members
        private Timer _timer;
        protected List<Img> imgs;
        // layout
        protected int column;
        protected int gutter;
        protected int colWidth;
        protected int virtualHeight;
        protected int offsetY;
        private SolidBrush backgroundBrush;
        private bool backgroundDrawRequired;

        //
        protected int selectedIndex;

        // -- properties
        [Browsable(true)]
        [Description("Get or set index of selected image")]
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            private set
            {
                if (0 <= value && value < imgs.Count)
                {
                    selectedIndex = value;
                }
            }
        }

        [Browsable(true)]
        [Description("Get total image number")]
        public int ImageCount 
        { 
            get 
            { 
                return imgs.Count; 
            }
        }

        [Browsable(true)]
        public int Column
        {
            get
            {
                return column;
            }
            set
            {
                if (value == 0)
                    throw new ArgumentException("Column must greater than 0.");
                if (column != value)
                {
                    column = value;
                    UpdateColumnWidth();
                    ResetGUI();
                }
            }
        }

        [Browsable(true)]
        public int Gutter 
        {
            get
            {
                return gutter;
            }
            set
            {
                if (gutter < 0)
                    throw new ArgumentException("Gutter must not nagative");
                if (gutter != value)
                {
                    gutter = value;
                    UpdateColumnWidth();
                    ResetGUI();
                }
            }
        }

        // events
        public event ImageGridItemClickedEventHandler OnImageClicked;

        // methods
        public ImageGrid()
        {
            column = 3;
            gutter = 0;
            imgs = new List<Img>();

            backgroundBrush = new SolidBrush(this.BackColor);

            _timer = new Timer();
            _timer.Interval = 50;
            
            _timer.Tick += (s, e) =>
            {
                using (Bitmap b = new Bitmap(this.Width, this.Height))
                {
                    Graphics g = Graphics.FromImage(b);
                    if (backgroundDrawRequired)
                    {
                        backgroundDrawRequired = false;
                        g.FillRectangle(backgroundBrush, this.ClientRectangle);
                    }
                    PaintImages(g, GetImagesInView());
                    g.Dispose();
                    
                    g = this.CreateGraphics();
                    g.DrawImage(b, Point.Empty);
                    g.Dispose();
                }
            };
            _timer.Start();
        }

        // public methods
        public void Clear()
        {
            DisposeImages();
            imgs = new List<Img>();
            offsetY = 0;
            backgroundDrawRequired = true;
        }

        // multi-threaded
        public void Add(Image image)
        {
            if (imgs == null) imgs = new List<Img>();
            imgs.Add(new Img(image));
            ComputePosition();
        }

        // calculate and drawing images
        public void ResetGUI()
        {
            virtualHeight = 0;
            if (imgs != null && imgs.Count != 0)
                ComputePosition();
        }
        // event handlers
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            // Allow the user scroll image grid view even though 
            // a typing cursor is currently focusing on another controls
            this.Focus();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.SelectedIndex = GetImageIndex(e.Location);
            // in case click, change selected index to invoke index changed event
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // perform click
                if (OnImageClicked != null)
                {
                    OnImageClicked(this, new ImageGridItemClickedEventArgs()
                    {
                        Index = this.SelectedIndex,
                        Image = imgs[this.SelectedIndex].Original
                    });
                }
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = GetImageIndex(e.Location) < 0 ? Cursors.Default : Cursors.Hand;
        }                
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (virtualHeight < this.Height) return;
            int newOffsetY = offsetY - e.Delta;
            if (newOffsetY < 0) newOffsetY = 0;
            if (newOffsetY > virtualHeight - this.Height)
                newOffsetY = virtualHeight - this.Height;
            int changed = newOffsetY - offsetY;
            offsetY = newOffsetY;
            if (changed != 0)
            {
                backgroundDrawRequired = true;
                foreach (Img iw in imgs)
                    iw.ClippingRegion = iw.ClippingRegion.AdjustY(-changed);
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            backgroundDrawRequired = true;
            UpdateColumnWidth();
            ResetGUI();
        }
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            this.backgroundBrush.Color = this.BackColor;
            backgroundDrawRequired = true;
        }
        private void DisposeImages()
        {
            if (imgs != null && imgs.Count > 0)
            {
                foreach (var imageWrapper in imgs)
                    imageWrapper.Dispose();
            }
        }
        //
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                DisposeImages();
            }
        }
        //
        protected abstract void ComputePosition();
        protected abstract void PaintImages(Graphics g, IEnumerable<Img> imageWrappers);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Bitmap b = new Bitmap(this.Width, this.Height))
            {
                Graphics g = Graphics.FromImage(b);
                if (backgroundDrawRequired)
                {
                    backgroundDrawRequired = false;
                    g.FillRectangle(backgroundBrush, this.ClientRectangle);
                }
                PaintImages(g, GetImagesInView(true));
                g.Dispose();

                e.Graphics.DrawImage(b, Point.Empty);
            }
        }
        // --- private methods
        private void UpdateColumnWidth()
        {
            colWidth = (int)((this.Width - 1 - (column + 1) * gutter * 1f) / column);
            if (column < 0)
                throw new Exception("Column width is <= 0, please check your setting again.");
        }
        private int GetImageIndex(Point location)
        {
            if (imgs == null) return -1;
            for (int i = 0; i < imgs.Count; i++)
            {
                if (imgs[i].ClippingRegion.Contains(location))
                    return i;
            }
            return -1;
        }
        private IEnumerable<Img> GetImagesInView(bool alwaysDraw = false)
        {
            foreach (var iw in imgs)
            {
                if ((alwaysDraw || iw.DrawRequired) && iw.ClippingRegion.IntersectsWith(this.ClientRectangle))
                {
                    iw.DrawRequired = false;
                    yield return iw;
                }
            }
        }
    }
}