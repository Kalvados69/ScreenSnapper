using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ScreenSnapper
{
    public partial class MainForm : Form
    {
        private NotifyIcon trayIcon;
        private string lastScreenshotPath = string.Empty;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            Core Snapper = new Core();
            trayIcon = new NotifyIcon();
            trayIcon.Icon = this.Icon;
            trayIcon.Text = "ScreenSnapper - Click to restore";
            trayIcon.Click += new EventHandler(trayIcon_Click);
            trayIcon.DoubleClick += new EventHandler(trayIcon_DoubleClick);
            trayIcon.BalloonTipClicked += new EventHandler(trayIcon_BalloonTipClicked);
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            this.Resize += new EventHandler(MainForm_Resize);
            Snapper.OnTooltipWanted += new Core.CoreEventHandler(Snapper_OnTooltipWanted);
            Snapper.OnScreenShot += new Core.CoreEventHandler(Snapper_OnScreenShot);
            Snapper.OnScreenShotFailed += new Core.CoreEventHandler(Snapper_OnScreenShotFailed);
            trayIcon.Visible = true;

            //comboBox1.DataSource = System.Enum.GetValues(typeof(Keys));
        }

        void trayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            OpenScreenshotsFolder();
        }

        void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            OpenScreenshotsFolder();
        }

        void Snapper_OnScreenShotFailed(object sender, CoreEventArgs e)
        {
            trayIcon.BalloonTipTitle = "ScreenSnapper";
            trayIcon.BalloonTipText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " > " + e.Message;
            trayIcon.ShowBalloonTip(1000);
        }

        void Snapper_OnScreenShot(object sender, CoreEventArgs e)
        {
            trayIcon.BalloonTipTitle = "ScreenSnapper";
            trayIcon.BalloonTipText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " > " + e.Message;
            lastScreenshotPath = e.ScreenshotPath;
            trayIcon.ShowBalloonTip(1000);
        }

        void Snapper_OnTooltipWanted(object sender, CoreEventArgs e)
        {
            trayIcon.BalloonTipTitle = "ScreenSnapper";
            trayIcon.BalloonTipText = e.Message;
            trayIcon.ShowBalloonTip(1000);
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            trayIcon.Dispose();
        }

        void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                SwitchToTray();
            }
        }

        void trayIcon_Click(object sender, EventArgs e)
        {
            SwitchBackFromTray();
        }

        private void SwitchToTray()
        {
            this.ShowInTaskbar = false;
            //trayIcon.Visible = true;
        }

        private void SwitchBackFromTray()
        {
            //trayIcon.Visible = false;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void OpenScreenshotsFolder()
        {
            if (!string.IsNullOrEmpty(lastScreenshotPath))
            {
                string launchArgs = string.Format("/n, /select, {0}", lastScreenshotPath);
                Process.Start(new ProcessStartInfo("explorer.exe", launchArgs));
            }
        }
    }
}
