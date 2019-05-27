using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Mmosoft.Oops.Controls.TitleBar
{
    public class TitleBar : Control
    {
        // Drag stuff
        private bool mouseIsDown; // mouse down state
        private Point mouseDownLocation; // where mouse down
        private Point mouseLocation; // last mouse down + hold position

        // event handlers
        public event EventHandler OnMouseDragCompleted;
        public event MouseDraggingEventHandler OnMouseDragging;
        public event EventHandler OnMinimizeClicked;
        public event EventHandler OnMaximizeClicked;
        public event EventHandler OnCloseClicked;

        // control buttons
        private TitleBarControlButton _minimizeButton;
        private TitleBarControlButton _maximizeButton;
        private TitleBarControlButton _closeButton;

        private bool _minimizeEnable;
        [Browsable(true)]
        public bool MinimizeEnable
        {
            get { return _minimizeEnable; }
            set { _minimizeEnable = value; CalculatePosition(); Invalidate(); }
        }

        private bool _maximizeEnable;
        [Browsable(true)]
        public bool MaximizeEnable
        {
            get { return _maximizeEnable; }
            set { _maximizeEnable = value; CalculatePosition(); Invalidate(); }
        }

        // UI stuff
        // UI size
        private int _controlButtonSize = 40;
        private int _controlImageSize = 16;
        private int _controlImagePadding = 12;        

        // UI resources
        private SolidBrush _textBrush;
        private SolidBrush _hoverBrush;
        private Pen _controlButtonPen;

        private Image _normalImg;
        private Image _maximizeImg;

        // 
        public TitleBar()
        {
            // control button setup
            _minimizeEnable = true;
            _maximizeEnable = true;

            // img setup
            _normalImg = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ControlButtons.Normal, 2, Brushes.Black, SmoothingMode.Default);
            _maximizeImg = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ControlButtons.Maximized, 2, Brushes.Black, SmoothingMode.Default);

            _minimizeButton = new TitleBarControlButton { Image = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ControlButtons.Minimize, 2, Brushes.Black, SmoothingMode.Default) };
            _maximizeButton = new TitleBarControlButton { Image = _normalImg };
            _closeButton = new TitleBarControlButton { Image = SvgPath8x8Mgr.Get(SvgPathBx8Constants.ControlButtons.Close, 2, Brushes.Black, SmoothingMode.Default) };
            
            _textBrush = BrushCreator.CreateSolidBrush();
            _hoverBrush = BrushCreator.CreateSolidBrush("#FFF0F0F0");
            _controlButtonPen = PenCreator.Create();

            DoubleBuffered = true;
        }        

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            var loc = e.Location;

            if (_minimizeButton.Contains(loc) && OnMinimizeClicked != null)
                OnMinimizeClicked(this, e);

            if (_maximizeButton.Contains(loc) && OnMaximizeClicked != null)
            {
                OnMaximizeClicked(this, e);
                var hostForm = this.FindForm();
                if (hostForm.WindowState == FormWindowState.Maximized)
                {
                    _maximizeButton.Image = _maximizeImg;
                }
                else
                {
                    _maximizeButton.Image = _normalImg;
                }

                Invalidate(_maximizeButton.Boundary);
            }

            if (_closeButton.Contains(loc) && OnCloseClicked != null)
                OnCloseClicked(this, e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalculatePosition();
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
            _minimizeButton.IsMouseHover = false;
            _maximizeButton.IsMouseHover = false;
            _closeButton.IsMouseHover = false;            
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (   _minimizeButton.Contains(e.Location)
                || _maximizeButton.Contains(e.Location)
                || _closeButton.Contains(e.Location))
            {
     
            }
            else
            {
                mouseIsDown = true;
                mouseLocation = e.Location;
                mouseDownLocation = PointToScreen(e.Location);
            }           
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (mouseIsDown)
            {
                if (PointToScreen(e.Location) != mouseDownLocation)
                {
                    if (OnMouseDragCompleted != null)
                        OnMouseDragCompleted(this, e);
                }
            }
            mouseIsDown = false;
        }

        private void RedrawIfButtonHoverStateChange(TitleBarControlButton btn, Point location)
        {
            var isMouseHover = btn.Contains(location);
            if (isMouseHover != btn.IsMouseHover)
            {
                btn.IsMouseHover = isMouseHover;
                Invalidate(btn.Boundary);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var loc = e.Location;
            RedrawIfButtonHoverStateChange(_minimizeButton, loc);
            RedrawIfButtonHoverStateChange(_maximizeButton, loc);
            RedrawIfButtonHoverStateChange(_closeButton, loc);            

            if (mouseIsDown)
            {
                if (OnMouseDragging != null)
                    OnMouseDragging(this, new MouseDraggingEventArgs() { 
                            OffsetX =  e.Location.X - mouseLocation.X, 
                            OffsetY = e.Location.Y - mouseLocation.Y});
            }
        }

        private void PaintControlButton(TitleBarControlButton button, Graphics g)
        {
            if (button.IsMouseHover)
                g.FillRectangle(_hoverBrush, button.Boundary);
            if (button.Image != null)
                g.DrawImage(button.Image, button.ImageBoundary);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            // Draw control button
            PaintControlButton(_minimizeButton, g);
            PaintControlButton(_maximizeButton, g);
            PaintControlButton(_closeButton, g);            

            // Draw title
            e.Graphics.DrawString(
                this.Text, 
                this.Font, 
                _textBrush, 
                this.ClientRectangle, 
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _textBrush.Dispose();
                _hoverBrush.Dispose();
                _controlButtonPen.Dispose();

                _normalImg.Dispose();
                _maximizeImg.Dispose();
            }
        }

        private void CalculatePosition()
        {
            _closeButton.Boundary = new Rectangle(this.Width - 1 - _controlButtonSize, 0, _controlButtonSize, _controlButtonSize);
            _closeButton.ImageBoundary = new Rectangle(_closeButton.Boundary.Left + _controlImagePadding, _controlImagePadding, _controlImageSize, _controlImageSize);

            if (MaximizeEnable)
            {
                _maximizeButton.Boundary = new Rectangle(_closeButton.Boundary.Left - _controlButtonSize, 0, _controlButtonSize, _controlButtonSize);
                _maximizeButton.ImageBoundary = new Rectangle(_maximizeButton.Boundary.Left + _controlImagePadding, _controlImagePadding, _controlImageSize, _controlImageSize);
            }
            else
            {
                _maximizeButton.Boundary = Rectangle.Empty;
                _maximizeButton.ImageBoundary = Rectangle.Empty;
            }

            if (MinimizeEnable)
            {
                if (MaximizeEnable)
                {
                    _minimizeButton.Boundary = new Rectangle(_maximizeButton.Boundary.Left - _controlButtonSize, 0, _controlButtonSize, _controlButtonSize);
                    _minimizeButton.ImageBoundary = new Rectangle(_minimizeButton.Boundary.Left + _controlImagePadding, _controlImagePadding, _controlImageSize, _controlImageSize);
                }
                else
                {
                    _minimizeButton.Boundary = new Rectangle(this.Width - 1 - 2 * _controlButtonSize, 0, _controlButtonSize, _controlButtonSize);
                    _minimizeButton.ImageBoundary = new Rectangle(_minimizeButton.Boundary.Left + _controlImagePadding, _controlImagePadding, _controlImageSize, _controlImageSize);
                }
            }
            else
            {
                _minimizeButton.Boundary = Rectangle.Empty;
                _minimizeButton.ImageBoundary = Rectangle.Empty;
            }
        }
    }

    public class MouseDraggingEventArgs : EventArgs
    {
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
    }

    public delegate void MouseDraggingEventHandler(object sender, MouseDraggingEventArgs e);
}