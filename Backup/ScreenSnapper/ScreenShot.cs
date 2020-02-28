using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenSnapper
{
    static class ScreenShot
    {
        private enum ShotType
        {
            VirtualScreen,
            PrimaryScreen,
            WorkingArea,
            ActiveWindow
        }

        static public Bitmap PrimaryScreenShot(bool workingAreaOnly)
        {
            if (workingAreaOnly)
            {
                return Shot(ShotType.WorkingArea);
            }
            else
            {
                return Shot(ShotType.PrimaryScreen);
            }
        }

        static public Bitmap SpecificScreenShot(Rectangle rect)
        {
            return Shot(rect);
        }

        static public Bitmap FullScreenShot()
        {
            return Shot(ShotType.VirtualScreen);
        }

        static public Bitmap ActiveWindowScreenShot()
        {
            return Shot(ShotType.ActiveWindow);
        }

        static public Bitmap SpecificWindowScreenShot()
        {
            throw new NotImplementedException();
        }

        static private Bitmap Shot(ShotType shotType)
        {
            Rectangle rect = new Rectangle();

            switch (shotType)
            {
                case ShotType.PrimaryScreen:
                    rect = Screen.PrimaryScreen.Bounds;
                    break;
                case ShotType.VirtualScreen:
                    rect = SystemInformation.VirtualScreen;
                    break;
                case ShotType.WorkingArea:
                    rect = Screen.PrimaryScreen.WorkingArea;
                    break;
                case ShotType.ActiveWindow:
                    rect = ForegroundWindow.GetForegroundWindowRect();
                    break;
            }

            return Shot(rect);
        }

        static private Bitmap Shot(Rectangle rect)
        {
            if (rect.Width > 0 && rect.Height > 0)
            {
                // Set the bitmap object to the size of the screen
                Bitmap bmpScreenshot = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
                // Create a graphics object from the bitmap
                using (Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot))
                {
                    // Take the screenshot from the upper left corner to the right bottom corner
                    gfxScreenshot.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
                }
                return bmpScreenshot;
            }
            else return null;
        }
    }

    static class ForegroundWindow
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width
            {
                get { return Right - Left; }
            }

            public int Height
            {
                get { return Bottom - Top; }
            }
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowRect(IntPtr hWnd, out RECT rect);

        static public Rectangle GetForegroundWindowRect()
        {
            IntPtr hWnd = GetForegroundWindow();
            RECT rect;
            GetWindowRect(hWnd, out rect);
            return new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        //[StructLayout(LayoutKind.Sequential)]
        //public struct POINT
        //{
        //    public int X;
        //    public int Y;

        //    public POINT(int x, int y)
        //    {
        //        this.X = x;
        //        this.Y = y;
        //    }

        //    public static implicit operator System.Drawing.Point(POINT p)
        //    {
        //        return new System.Drawing.Point(p.X, p.Y);
        //    }

        //    public static implicit operator POINT(System.Drawing.Point p)
        //    {
        //        return new POINT(p.X, p.Y);
        //    }
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //public struct SIZE
        //{
        //    public int cx;
        //    public int cy;

        //    public SIZE(int cx, int cy)
        //    {
        //        this.cx = cx;
        //        this.cy = cy;
        //    }
        //}

        //[DllImport("gdi32.dll")]
        //static extern bool GetWindowOrgEx(IntPtr hdc, out POINT lpPoint);

        //[DllImport("gdi32.dll")]
        //static extern bool GetWindowExtEx(IntPtr hdc, out SIZE lpSize);

        //static public Rectangle GetForegroundWindowRect2()
        //{
        //    bool rlt = false;
        //    IntPtr hWnd = GetForegroundWindow();
        //    POINT point;
        //    SIZE size;
        //    rlt = GetWindowOrgEx(hWnd, out point);
        //    rlt = GetWindowExtEx(hWnd, out size);
        //    return new Rectangle(point.X, point.Y, size.cx, size.cy);
        //}
    }
}
