namespace Dalworth.Windows
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.m_mainMenu = new System.Windows.Forms.MainMenu();
            this.m_leftAction = new System.Windows.Forms.MenuItem();
            this.m_rightAction = new System.Windows.Forms.MenuItem();
            this.m_pnlContent = new System.Windows.Forms.Panel();
            this.m_pnlTop = new System.Windows.Forms.Panel();
            this.m_pictureConnectionLost = new System.Windows.Forms.PictureBox();
            this.m_pictureConnectionFound = new System.Windows.Forms.PictureBox();
            this.m_pnlExit = new System.Windows.Forms.Panel();
            this.m_pictureGpsLost = new System.Windows.Forms.PictureBox();
            this.m_pictureGpsFound = new System.Windows.Forms.PictureBox();
            this.m_lblTime = new System.Windows.Forms.Label();
            this.m_batteryLife = new OpenNETCF.Windows.Forms.BatteryLife();
            this.m_lblTitle = new System.Windows.Forms.Label();
            this.m_timerClock = new System.Windows.Forms.Timer();
            this.m_timerBatteryUpdate = new System.Windows.Forms.Timer();
            this.m_timerStayAlive = new System.Windows.Forms.Timer();
            this.m_pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_mainMenu
            // 
            this.m_mainMenu.MenuItems.Add(this.m_leftAction);
            this.m_mainMenu.MenuItems.Add(this.m_rightAction);
            // 
            // m_leftAction
            // 
            this.m_leftAction.Text = "Default Action";
            // 
            // m_rightAction
            // 
            this.m_rightAction.Text = "Menu";
            // 
            // m_pnlContent
            // 
            this.m_pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlContent.Location = new System.Drawing.Point(0, 32);
            this.m_pnlContent.Name = "m_pnlContent";
            this.m_pnlContent.Size = new System.Drawing.Size(240, 262);
            // 
            // m_pnlTop
            // 
            this.m_pnlTop.Controls.Add(this.m_pictureConnectionLost);
            this.m_pnlTop.Controls.Add(this.m_pictureConnectionFound);
            this.m_pnlTop.Controls.Add(this.m_pnlExit);
            this.m_pnlTop.Controls.Add(this.m_pictureGpsLost);
            this.m_pnlTop.Controls.Add(this.m_pictureGpsFound);
            this.m_pnlTop.Controls.Add(this.m_lblTime);
            this.m_pnlTop.Controls.Add(this.m_batteryLife);
            this.m_pnlTop.Controls.Add(this.m_lblTitle);
            this.m_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTop.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTop.Name = "m_pnlTop";
            this.m_pnlTop.Size = new System.Drawing.Size(240, 32);
            // 
            // m_pictureConnectionLost
            // 
            this.m_pictureConnectionLost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pictureConnectionLost.Image = ((System.Drawing.Image)(resources.GetObject("m_pictureConnectionLost.Image")));
            this.m_pictureConnectionLost.Location = new System.Drawing.Point(109, 15);
            this.m_pictureConnectionLost.Name = "m_pictureConnectionLost";
            this.m_pictureConnectionLost.Size = new System.Drawing.Size(16, 15);
            // 
            // m_pictureConnectionFound
            // 
            this.m_pictureConnectionFound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pictureConnectionFound.Image = ((System.Drawing.Image)(resources.GetObject("m_pictureConnectionFound.Image")));
            this.m_pictureConnectionFound.Location = new System.Drawing.Point(109, 15);
            this.m_pictureConnectionFound.Name = "m_pictureConnectionFound";
            this.m_pictureConnectionFound.Size = new System.Drawing.Size(16, 15);
            // 
            // m_pnlExit
            // 
            this.m_pnlExit.BackColor = System.Drawing.Color.Black;
            this.m_pnlExit.Location = new System.Drawing.Point(3, 18);
            this.m_pnlExit.Name = "m_pnlExit";
            this.m_pnlExit.Size = new System.Drawing.Size(5, 5);
            // 
            // m_pictureGpsLost
            // 
            this.m_pictureGpsLost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pictureGpsLost.Image = ((System.Drawing.Image)(resources.GetObject("m_pictureGpsLost.Image")));
            this.m_pictureGpsLost.Location = new System.Drawing.Point(127, 15);
            this.m_pictureGpsLost.Name = "m_pictureGpsLost";
            this.m_pictureGpsLost.Size = new System.Drawing.Size(15, 15);
            // 
            // m_pictureGpsFound
            // 
            this.m_pictureGpsFound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pictureGpsFound.Image = ((System.Drawing.Image)(resources.GetObject("m_pictureGpsFound.Image")));
            this.m_pictureGpsFound.Location = new System.Drawing.Point(127, 15);
            this.m_pictureGpsFound.Name = "m_pictureGpsFound";
            this.m_pictureGpsFound.Size = new System.Drawing.Size(15, 15);
            // 
            // m_lblTime
            // 
            this.m_lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.m_lblTime.Location = new System.Drawing.Point(187, 15);
            this.m_lblTime.Name = "m_lblTime";
            this.m_lblTime.Size = new System.Drawing.Size(50, 15);
            this.m_lblTime.Text = "12:59 PM";
            // 
            // m_batteryLife
            // 
            this.m_batteryLife.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_batteryLife.BorderColor = System.Drawing.Color.Green;
            this.m_batteryLife.Location = new System.Drawing.Point(144, 15);
            this.m_batteryLife.Name = "m_batteryLife";
            this.m_batteryLife.PercentageBarColor = System.Drawing.Color.Green;
            this.m_batteryLife.Size = new System.Drawing.Size(40, 15);
            this.m_batteryLife.TabIndex = 0;
            // 
            // m_lblTitle
            // 
            this.m_lblTitle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.m_lblTitle.Location = new System.Drawing.Point(2, 0);
            this.m_lblTitle.Name = "m_lblTitle";
            this.m_lblTitle.Size = new System.Drawing.Size(235, 15);
            this.m_lblTitle.Text = "Some title";
            // 
            // m_timerClock
            // 
            this.m_timerClock.Enabled = true;
            this.m_timerClock.Interval = 1000;
            // 
            // m_timerBatteryUpdate
            // 
            this.m_timerBatteryUpdate.Enabled = true;
            this.m_timerBatteryUpdate.Interval = 30000;
            // 
            // m_timerStayAlive
            // 
            this.m_timerStayAlive.Enabled = true;
            this.m_timerStayAlive.Interval = 15000;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.ControlBox = false;
            this.Controls.Add(this.m_pnlContent);
            this.Controls.Add(this.m_pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.m_mainMenu;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.m_pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.MenuItem m_rightAction;
        internal System.Windows.Forms.MenuItem m_leftAction;
        internal System.Windows.Forms.MainMenu m_mainMenu;
        internal System.Windows.Forms.Panel m_pnlContent;
        internal System.Windows.Forms.Panel m_pnlTop;
        internal System.Windows.Forms.Label m_lblTitle;
        internal OpenNETCF.Windows.Forms.BatteryLife m_batteryLife;
        internal System.Windows.Forms.Label m_lblTime;
        internal System.Windows.Forms.Timer m_timerClock;
        internal System.Windows.Forms.Timer m_timerBatteryUpdate;
        internal System.Windows.Forms.PictureBox m_pictureGpsLost;
        internal System.Windows.Forms.PictureBox m_pictureGpsFound;
        internal System.Windows.Forms.Panel m_pnlExit;
        internal System.Windows.Forms.Timer m_timerStayAlive;
        internal System.Windows.Forms.PictureBox m_pictureConnectionLost;
        internal System.Windows.Forms.PictureBox m_pictureConnectionFound;
    }
}