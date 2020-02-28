using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace CaptureEcran.net2
{
    public static class ScreenShotHelper
    {
        /// <summary>
        /// Type de captures
        /// </summary>
        public enum ScreenShotType {
            VirtualScreen,
            PrimaryScreen,
            WorkingArea
        }

        /// <summary>
        /// Effectue une capture d'écran 
        /// </summary>
        /// <param name="type">Type de capture</param>
        /// <returns>Bitmap représentant la capture d'écran</returns>
        public static Bitmap Capture(ScreenShotType type) {
            Bitmap bitmap = null;
            Rectangle rect;

            try {
                switch (type) { 
                    case ScreenShotType.PrimaryScreen:
                        rect = Screen.PrimaryScreen.Bounds;
                        break;
                    default:
                    case ScreenShotType.VirtualScreen:
                        rect = SystemInformation.VirtualScreen;
                        break;
                    case ScreenShotType.WorkingArea:
                        rect = Screen.PrimaryScreen.WorkingArea;
                        break;
                }

                bitmap = capture(rect);
            }
            catch (Exception ex) {
                throw ex;
            }

            // retourne la capture
            return bitmap;
        }

        /// <summary>
        /// Capture l'affichage de l'écran dont l'identifiant est 
        /// passé en paramètre
        /// </summary>
        /// <param name="screen">Identifiant de l'écran</param>
        /// <returns>Bitmap représentant la capture d'écran</returns>
        public static Bitmap CaptureScreen(int screen) {
            if (screen > Screen.AllScreens.Length)
                throw new OverflowException("L'identifiant n'est pas dans la limite...");
            return capture(Screen.AllScreens[screen].Bounds);
        }

        /// <summary>
        /// Capture la réprésentation graphique du Control
        /// </summary>
        /// <param name="control">Control à capturer</param>
        /// <returns>Bitmap de la capture</returns>
        public static Bitmap Capture(Control control)
        {
            return capture(control.RectangleToScreen(control.ClientRectangle));
        }

        /// <summary>
        /// Capture la réprésentation graphique du formulaire
        /// </summary>
        /// <param name="form">Formulaire à capturer</param>
        /// <returns>Bitmap de la capture</returns>
        public static Bitmap Capture(Form form)
        {
            return Capture(form, false);
        }

        /// <summary>
        /// Capture la réprésentation graphique du formulaire<br />
        /// Si clientZoneOnly = true, seule la zone client sera capturée
        /// </summary>
        /// <param name="form">Formulaire à capturer</param>
        /// <param name="clientZoneOnly">Capturer que la zone cliente ?</param>
        /// <returns>Bitmap de la capture</returns>
        public static Bitmap Capture(Form form, bool clientZoneOnly) {
            Bitmap bitmap = null;
            if (clientZoneOnly)
            {
                bitmap = capture(form.RectangleToScreen(form.ClientRectangle));
            }
            else {
                bitmap = capture(form.Bounds);
            }
            return bitmap;
        }

        /// <summary>
        /// Capture la zone de l'écran spécifiée
        /// </summary>
        /// <param name="rect">Zone de l'écran à capturer</param>
        /// <returns>Bitmap représentant la capture</returns>
        private static Bitmap capture(Rectangle rect) {
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bitmap)) {
                g.CopyFromScreen(rect.Left, rect.Top, 0,0, rect.Size, CopyPixelOperation.SourceCopy);
            }
            return bitmap;
        }
    }
}
