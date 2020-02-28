using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Gma.UserActivityMonitor;

namespace ScreenSnapper
{
    class Core
    {
        HotKey hk1;
        HotKey hk2;
        HotKey hk3;
        private int nbpress;
        private Rectangle ShotZone;
        private bool FirstClick;
        private Configuration SnapperConf;

        public event CoreEventHandler OnScreenShot;
        public event CoreEventHandler OnScreenShotFailed;
        public event CoreEventHandler OnTooltipWanted;
        public delegate void CoreEventHandler(object sender, CoreEventArgs e);

        public Core()
        {
            Init();
        }

        public void Init()
        {
            SnapperConf = new Configuration();

            hk1 = new HotKey();
            hk1.KeyCode = Keys.Snapshot;
            hk1.HotkeyPressed += new System.EventHandler(hk1_HotkeyPressed);
            hk1.Enabled = true;

            hk2 = new HotKey();
            hk2.KeyCode = Keys.Snapshot;
            hk2.Alt = true;
            hk2.HotkeyPressed += new System.EventHandler(hk2_HotkeyPressed);
            hk2.Enabled = true;

            hk3 = new HotKey();
            hk3.KeyCode = Keys.F9;
            hk3.HotkeyPressed += new System.EventHandler(hk3_HotkeyPressed);
            hk3.Enabled = true;

            nbpress = 0;
        }

        private string GetSavePath()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ScreenSnapper");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        private string GetFileName()
        {
            string name = "Screenshot_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            return name;
        }

        void hk1_HotkeyPressed(object sender, System.EventArgs e)
        {
            // Full primary screen
            Bitmap screenshot = ScreenShot.PrimaryScreenShot(false);
            if (screenshot != null)
            {
                string filepath = Path.Combine(GetSavePath(), GetFileName());
                screenshot.Save(filepath, ImageFormat.Png);
                SendShotEvent(filepath);
            }
            else SendShotFailedEvent();
        }

        void hk2_HotkeyPressed(object sender, System.EventArgs e)
        {
            // Active Window
            Bitmap screenshot = ScreenShot.ActiveWindowScreenShot();
            if (screenshot != null)
            {
                string filepath = Path.Combine(GetSavePath(), GetFileName());
                screenshot.Save(filepath, ImageFormat.Png);
                SendShotEvent(filepath);
            }
            else SendShotFailedEvent();
        }

        void hk3_HotkeyPressed(object sender, System.EventArgs e)
        {
            switch (nbpress)
            {
                case 0:
                    //affiche tooltip

                    if (OnTooltipWanted != null)
                    {
                        CoreEventArgs args = new CoreEventArgs();
                        args.Message = "Souris en TopLeft puis F9";
                        OnTooltipWanted(this, args);
                    }
                    nbpress++;
                    break;
                case 1:
                    ShotZone = new Rectangle();
                    ShotZone.Location = Cursor.Position;
                    nbpress++;

                    if (OnTooltipWanted != null)
                    {
                        CoreEventArgs args = new CoreEventArgs();
                        args.Message = "Souris en BottomRight puis F9";
                        OnTooltipWanted(this, args);
                    }
                    break;
                case 2:
                    ShotZone.Width = Cursor.Position.X - ShotZone.X;
                    ShotZone.Height = Cursor.Position.Y - ShotZone.Y;

                    Bitmap screenshot = ScreenShot.SpecificScreenShot(ShotZone);
                    if (screenshot != null)
                    {
                        string filepath = Path.Combine(GetSavePath(), GetFileName());
                        screenshot.Save(filepath, ImageFormat.Png);
                        SendShotEvent(filepath);
                    }
                    else SendShotFailedEvent();
                    nbpress = 0;
                    break;
            }
            // Screen Zone
            //FirstClick = true;
            //HookManager.MouseClick += new MouseEventHandler(HookManager_MouseClick);
        }

        private void SendShotEvent(string filePath)
        {
            if (OnScreenShot != null)
            {
                CoreEventArgs args = new CoreEventArgs();
                args.Message = "Capture effectuée...";
                args.ScreenshotPath = filePath;
                OnScreenShot(this, args);
            }
        }

        private void SendShotFailedEvent()
        {
            if (OnScreenShotFailed != null)
            {
                CoreEventArgs args = new CoreEventArgs();
                args.Message = "Capture impossible...";
                OnScreenShotFailed(this, args);
            }
        }

        void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (FirstClick)
                {
                    FirstClick = false;
                    ShotZone = new Rectangle();
                    ShotZone.Location = e.Location;
                }
                else
                {
                    ShotZone.Width = e.X - ShotZone.X;
                    ShotZone.Height = e.Y - ShotZone.Y;

                    HookManager.MouseClick -= new MouseEventHandler(HookManager_MouseClick);

                    Bitmap screenshot = ScreenShot.SpecificScreenShot(ShotZone);
                    string filepath = Path.Combine(GetSavePath(), GetFileName());
                    screenshot.Save(filepath, ImageFormat.Png);

                    //MessageBox.Show("Rect => X: " + ShotZone.X + ", Y: " + ShotZone.Y + ", Width: " + ShotZone.Width + ", Height: " + ShotZone.Height);
                }
            }
        }
    }

    public class CoreEventArgs : EventArgs
    {
        private string message = string.Empty;
        private string screenshotPath = string.Empty;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string ScreenshotPath
        {
            get { return screenshotPath; }
            set { screenshotPath = value; }
        }
    }
}
