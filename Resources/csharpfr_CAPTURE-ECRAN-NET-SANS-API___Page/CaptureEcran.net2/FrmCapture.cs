using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CaptureEcran.net2
{
    public partial class FrmCapture : Form
    {
        public FrmCapture()
        {
            InitializeComponent();
        }

        private void enregistrerLimageSousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                this.BackgroundImage.Save(saveFileDialog1.FileName);
            }
        }
    }
}