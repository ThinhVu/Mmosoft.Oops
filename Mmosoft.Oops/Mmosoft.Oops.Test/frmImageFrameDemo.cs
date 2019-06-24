using Mmosoft.Oops.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mmosoft.Oops;

namespace Mmosoft.Oops.Test
{
    public partial class frmImageFrameDemo : Form
    {
        public enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public frmImageFrameDemo()
        {
            DoubleBuffered = true;

            InitializeComponent();
        }

        private void frmImageFrameDemo_Load(object sender, EventArgs e)
        {
            imageFrame1.AddFrameState(Direction.DOWN.ToString(), new int[] { 0, 1, 2, 3 });
            imageFrame1.AddFrameState(Direction.UP.ToString(), new int[] { 4, 5, 6, 7 });
            imageFrame1.AddFrameState(Direction.LEFT.ToString(), new int[] { 8, 9, 10, 11 });
            imageFrame1.AddFrameState(Direction.RIGHT.ToString(), new int[] { 12, 13, 14, 15 });

            imageFrame1.SetFrameState(Direction.DOWN.ToString());
            imageFrame1.Run = true;
            imageFrame1.LoopEnable = true;
        }

        private void frmImageFrameDemo_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void frmImageFrameDemo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // W , A , S , D

            base.ProcessCmdKey(ref msg, keyData);
            switch (keyData)
            {
                case Keys.S:
                    KeyHandle(Direction.DOWN);
                    break;
                case Keys.W:
                    KeyHandle(Direction.UP);
                    break;
                case Keys.A:
                    KeyHandle(Direction.LEFT);
                    break;
                case Keys.D:
                    KeyHandle(Direction.RIGHT);
                    break;
            }

            return true;
        }

        private void KeyHandle(Direction direction)
        {
            imageFrame1.SetFrameState(direction.ToString());
            Moving(direction);
        }

        private void Moving(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    imageFrame1.Location = imageFrame1.Location.ChangePosition(0, -1);
                    break;

                case Direction.DOWN:
                    imageFrame1.Location = imageFrame1.Location.ChangePosition(0, 1);
                    break;

                case Direction.LEFT:
                    imageFrame1.Location = imageFrame1.Location.ChangePosition(-1, 0);
                    break;

                case Direction.RIGHT:
                    imageFrame1.Location = imageFrame1.Location.ChangePosition(1, 0);
                    break;
            }
        }
    }
}
