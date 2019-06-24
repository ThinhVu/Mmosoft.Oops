using Mmosoft.ImageProcessing;
using Mmosoft.Oops.Extensions.Syntax;
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Mmosoft.Oops.Controls.Images
{
    public class ImageFrame : Control
    {
        private const string DEFAULT_FRAME_STATE = "";

        private Image _source;
        private Size _frameSize;
        public List<Image> _frames;

        private bool _loopEnable;
        private Dictionary<string, int[]> _frameState;
        private string _currentFrameState;
        private int _currentFrameStateIndex;
        private bool _run;
        private Timer _runTimer;

        /// <summary>
        /// Image source
        /// </summary>
        public Image Source 
        {
            get { return _source; }
            set { if (_source != value) { _source = value; InitFrames(); } }
        }

        /// <summary>
        /// Size of each frame
        /// </summary>
        public Size FrameSize
        {
            get { return _frameSize; }
            set { if (_frameSize != value) { _frameSize = value; InitFrames(); } }
        }

        /// <summary>
        /// Loop or not
        /// </summary>
        public bool LoopEnable 
        {
            get { return _loopEnable; }
            set { if (_loopEnable != value) { _loopEnable = value; Invalidate(); } }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Run
        {
            get
            {
                return _run;
            }
            set
            {
                if (_run != value)
                {
                    _run = value;
                    if (_run)
                    {
                        _runTimer.Start();
                    }
                    else
                    {
                        _runTimer.Stop();
                    }
                }
            }
        }

        public ImageFrame()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;

            _frameState = new Dictionary<string, int[]>();

            InitRunner();
        }

        public void AddFrameState(NotAllowNull<string> stateName, NotAllowNull<int[]> frameIndexes)
        {
            _frameState[stateName] = frameIndexes;
        }

        public void SetFrameState(NotAllowNull<string> stateName)
        {
            if (_frameState.ContainsKey(stateName))
            {
                _currentFrameState = stateName;
                _currentFrameStateIndex = 0;
            }
        }

        private void InitFrames()
        {
            if (_source == null || _frameSize == Size.Empty) return;
            if (_source.Width % _frameSize.Width != 0 || _source.Height % _frameSize.Height != 0)
                throw new ArgumentException("Invalid frame dimensions");

            _frames = new List<Image>();

            // get pixel
            Pixel[,] imagePixels = GetPixel(_source);
            Pixel[,] framePixels;
            int pxRow = 0;
            int pxColumn = 0;
            // reading by row
            while (pxRow < _source.Height)
            {
                while (pxColumn < _source.Width)
                {
                    framePixels = new Pixel[_frameSize.Width, _frameSize.Height];

                    for (int frameRow = 0; frameRow < _frameSize.Height; frameRow++)
                    {
                        for (int frameColumn = 0; frameColumn < _frameSize.Width; frameColumn++)
                        {
                            // it's really ugly when we access column first ò_ó
                            framePixels[frameColumn, frameRow] = imagePixels[pxColumn + frameColumn, pxRow + frameRow];
                        }
                    }

                    // Add frame into frame collection
                    _frames.Add(CreateImage(framePixels));

                    // move next frame in the same frame line
                    pxColumn += _frameSize.Width;
                }
                // end of line => move to next row (Carry Return)
                pxColumn = 0;
                pxRow += _frameSize.Height;
            }

            // Init default index
            var defaultIndexes = new int[_frames.Count];
            for (int i = 0; i < _frames.Count; i++)
			    defaultIndexes[i] = i;

            AddFrameState(DEFAULT_FRAME_STATE, defaultIndexes);

            _currentFrameState = DEFAULT_FRAME_STATE;
            _currentFrameStateIndex = 0;
        }
        private void InitRunner()
        {
            _runTimer = new Timer { Interval = 500 };
            _runTimer.Tick += _runTimer_Tick;
        }

        void _runTimer_Tick(object sender, EventArgs e)
        {
            if (_frames == null || _frames.Count == 0)
                return;

            _currentFrameStateIndex++;

            if (_currentFrameStateIndex == _frameState[_currentFrameState].Length)
            {
                // reset current index
                _currentFrameStateIndex = 0;
                
                // if not allow loop then stop
                if (!LoopEnable)
                {
                    _runTimer.Stop();
                    return;
                }
            }

            Invalidate();
        }

        // should belong to Image process stuff
        unsafe private static Pixel[,] GetPixel(Image src)
        {
            var b = (Bitmap)src;
            BitmapData bitmapData = b.LockBits(new Rectangle(Point.Empty, b.Size), 
                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            Pixel[,] pxs = null;
            bitmapData.CopyDataTo(out pxs);
            pxs.CopyDataTo(bitmapData);
            b.UnlockBits(bitmapData);
            return pxs;
        }
        unsafe private static Image CreateImage(Pixel[,] pxs)
        {
            int width = pxs.GetLength(0);
            int height = pxs.GetLength(1);
            Bitmap frame = new Bitmap(width, height);
            BitmapData bitmapData = frame.LockBits(
                new Rectangle(0, 0, width, height), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format32bppArgb);
            pxs.CopyDataTo(bitmapData);
            frame.UnlockBits(bitmapData);
            return frame;
        }

        //
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_frames == null || _frames.Count == 0)
                return;
            var g = e.Graphics;
            // get selected frame index
            g.DrawImage(_frames[_frameState[_currentFrameState][_currentFrameStateIndex]], this.ClientRectangle);
        }
    }
}
