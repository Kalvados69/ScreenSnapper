using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CaptureEcran.net2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cboListeEcrans.Items.Clear();
            for (int i = 0; i < Screen.AllScreens.Length; i++) {
                cboListeEcrans.Items.Add(i);
            }
            cboListeEcrans.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showCapture(ScreenShotHelper.Capture(ScreenShotHelper.ScreenShotType.PrimaryScreen));
        }

        private static void showCapture(Bitmap b)
        {

            using (FrmCapture f = new FrmCapture())
            {
                f.BackgroundImage = b;
                f.ClientSize = b.Size;

                f.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showCapture(ScreenShotHelper.Capture(ScreenShotHelper.ScreenShotType.WorkingArea));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showCapture(ScreenShotHelper.Capture(ScreenShotHelper.ScreenShotType.VirtualScreen));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showCapture(ScreenShotHelper.CaptureScreen((int)cboListeEcrans.SelectedItem));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showCapture(ScreenShotHelper.Capture(button5));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            showCapture(ScreenShotHelper.Capture(this, true));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            showCapture(ScreenShotHelper.Capture(this, false));
        }



    }
}