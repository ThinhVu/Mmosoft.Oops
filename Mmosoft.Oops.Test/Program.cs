﻿using Mmosoft.Oops;
using Mmosoft.Oops.Test;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mmosoft.OopsTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ScaleLoss();
            //ScaleLossLess();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmImageSlide());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            
        }



        static void ScaleLoss()
        {
            // case 1
            var clipping = new Rectangle(0, 0, 200, 300);
            var actualSize = new Rectangle(0, 0, 400, 700);
            var output = Mmosoft.Oops.Controls.ImageDisplayModeHelper.GetImageRect(clipping, actualSize, Oops.Controls.DisplayMode.ScaleLoss);
            MessageBox.Show(
                "Test 1" + Environment.NewLine +
                "Actual_____: " + actualSize.ToString() + "  W/H ratio:" + (actualSize.Width * 1f / actualSize.Height) + Environment.NewLine +
                "Clipping___: " + clipping.ToString() + Environment.NewLine +
                "ScaleLoss__: " + output.ToString() + "  W/H ratio:" + (output.Width * 1f / output.Height));

            //
            clipping = new Rectangle(0, 0, 300, 400);
            actualSize = new Rectangle(0, 0, 400, 600);
            output = Mmosoft.Oops.Controls.ImageDisplayModeHelper.GetImageRect(clipping, actualSize, Oops.Controls.DisplayMode.ScaleLoss);
            MessageBox.Show(
                "Test 2" + Environment.NewLine +
                "Actual_____: " + actualSize.ToString() + "  W/H ratio:" + (actualSize.Width * 1f / actualSize.Height) + Environment.NewLine +
                "Clipping___: " + clipping.ToString() + Environment.NewLine +
                "ScaleLoss__: " + output.ToString() + "  W/H ratio:" + (output.Width * 1f / output.Height));

            clipping = new Rectangle(0, 0, 400, 500);
            actualSize = new Rectangle(0, 0, 400, 500);
            output = Mmosoft.Oops.Controls.ImageDisplayModeHelper.GetImageRect(clipping, actualSize, Oops.Controls.DisplayMode.ScaleLoss);
            MessageBox.Show(
                "Test 3" + Environment.NewLine +
                "Actual_____: " + actualSize.ToString() + "  W/H ratio:" + (actualSize.Width * 1f / actualSize.Height) + Environment.NewLine +
                "Clipping___: " + clipping.ToString() + Environment.NewLine +
                "ScaleLoss__: " + output.ToString() + "  W/H ratio:" + (output.Width * 1f / output.Height));

            clipping = new Rectangle(0, 0, 500, 600);
            actualSize = new Rectangle(0, 0, 400, 400);
            output = Mmosoft.Oops.Controls.ImageDisplayModeHelper.GetImageRect(clipping, actualSize, Oops.Controls.DisplayMode.ScaleLoss);
            MessageBox.Show(
                "Test 4" + Environment.NewLine +
               "Actual_____: " + actualSize.ToString() + "  W/H ratio:" + (actualSize.Width * 1f / actualSize.Height) + Environment.NewLine +
                "Clipping___: " + clipping.ToString() + Environment.NewLine +
                "ScaleLoss__: " + output.ToString() + "  W/H ratio:" + (output.Width * 1f / output.Height));

            clipping = new Rectangle(0, 0, 600, 700);
            actualSize = new Rectangle(0, 0, 400, 300);
            output = Mmosoft.Oops.Controls.ImageDisplayModeHelper.GetImageRect(clipping, actualSize, Oops.Controls.DisplayMode.ScaleLoss);
            MessageBox.Show(
                "Test 5" + Environment.NewLine +
               "Actual_____: " + actualSize.ToString() + "  W/H ratio:" + (actualSize.Width * 1f / actualSize.Height) + Environment.NewLine +
                "Clipping___: " + clipping.ToString() + Environment.NewLine +
                "ScaleLoss__: " + output.ToString() + "  W/H ratio:" + (output.Width * 1f / output.Height));
        }

        static void ScaleLossLess()
        {
            // case 1
            var clipping = new Rectangle(0, 0, 200, 300);
            var actualSize = new Rectangle(0, 0, 400, 700);
            var output = Mmosoft.Oops.Controls.ImageDisplayModeHelper.GetImageRect(clipping, actualSize, Oops.Controls.DisplayMode.ScaleLossLess);
            MessageBox.Show(
                "Test 1" + Environment.NewLine +
                "Actual_____: " + actualSize.ToString() + "  W/H ratio:" + (actualSize.Width * 1f / actualSize.Height) + Environment.NewLine +
                "Clipping___: " + clipping.ToString() + Environment.NewLine +
                "ScaleLossLess__: " + output.ToString() + "  W/H ratio:" + (output.Width * 1f / output.Height));

            //
            clipping = new Rectangle(0, 0, 300, 400);
            actualSize = new Rectangle(0, 0, 400, 600);
            output = Mmosoft.Oops.Controls.ImageDisplayModeHelper.GetImageRect(clipping, actualSize, Oops.Controls.DisplayMode.ScaleLossLess);
            MessageBox.Show(
                "Test 2" + Environment.NewLine +
                "Actual_____: " + actualSize.ToString() + "  W/H ratio:" + (actualSize.Width * 1f / actualSize.Height) + Environment.NewLine +
                "Clipping___: " + clipping.ToString() + Environment.NewLine +
                "ScaleLossLess__: " + output.ToString() + "  W/H ratio:" + (output.Width * 1f / output.Height));

            clipping = new Rectangle(0, 0, 400, 500);
            actualSize = new Rectangle(0, 0, 400, 500);
            output = Mmosoft.Oops.Controls.ImageDisplayModeHelper.GetImageRect(clipping, actualSize, Oops.Controls.DisplayMode.ScaleLossLess);
            MessageBox.Show(
                "Test 3" + Environment.NewLine +
                "Actual_____: " + actualSize.ToString() + "  W/H ratio:" + (actualSize.Width * 1f / actualSize.Height) + Environment.NewLine +
                "Clipping___: " + clipping.ToString() + Environment.NewLine +
                "ScaleLossLess__: " + output.ToString() + "  W/H ratio:" + (output.Width * 1f / output.Height));

            clipping = new Rectangle(0, 0, 500, 600);
            actualSize = new Rectangle(0, 0, 400, 400);
            output = Mmosoft.Oops.Controls.ImageDisplayModeHelper.GetImageRect(clipping, actualSize, Oops.Controls.DisplayMode.ScaleLossLess);
            MessageBox.Show(
                "Test 4" + Environment.NewLine +
               "Actual_____: " + actualSize.ToString() + "  W/H ratio:" + (actualSize.Width * 1f / actualSize.Height) + Environment.NewLine +
                "Clipping___: " + clipping.ToString() + Environment.NewLine +
                "ScaleLossLess__: " + output.ToString() + "  W/H ratio:" + (output.Width * 1f / output.Height));

            clipping = new Rectangle(0, 0, 600, 700);
            actualSize = new Rectangle(0, 0, 400, 300);
            output = Mmosoft.Oops.Controls.ImageDisplayModeHelper.GetImageRect(clipping, actualSize, Oops.Controls.DisplayMode.ScaleLossLess);
            MessageBox.Show(
                "Test 4" + Environment.NewLine +
               "Actual_____: " + actualSize.ToString() + "  W/H ratio:" + (actualSize.Width * 1f / actualSize.Height) + Environment.NewLine +
                "Clipping___: " + clipping.ToString() + Environment.NewLine +
                "ScaleLossLess__: " + output.ToString() + "  W/H ratio:" + (output.Width * 1f / output.Height));
        }
    }
}
