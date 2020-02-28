namespace CaptureEcran.net2
{
    partial class FrmCapture
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enregistrerLimageSousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enregistrerLimageSousToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(200, 26);
            // 
            // enregistrerLimageSousToolStripMenuItem
            // 
            this.enregistrerLimageSousToolStripMenuItem.Name = "enregistrerLimageSousToolStripMenuItem";
            this.enregistrerLimageSousToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.enregistrerLimageSousToolStripMenuItem.Text = "Enregistrer l\'image sous...";
            this.enregistrerLimageSousToolStripMenuItem.Click += new System.EventHandler(this.enregistrerLimageSousToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "*.jpg";
            this.saveFileDialog1.Filter = "Fichier JPEG (*.jpg)|*.jpg|Fichier PNG (*.png)|*.png";
            // 
            // FrmCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "FrmCapture";
            this.Text = "Votre Capture :)";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem enregistrerLimageSousToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}