namespace MobileTech.Windows.UI.Setup.Menu
{
    partial class SetupMenu
    {
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_mbAbout = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbExit = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbCustomerBalance = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbTCommSetup = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbRouteSetup = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbSystem = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbPen = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbSwitch = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_mbAbout
            // 
            this.m_mbAbout.BackColor = System.Drawing.Color.White;
            this.m_mbAbout.Enabled = false;
            this.m_mbAbout.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbAbout.Location = new System.Drawing.Point(4, 220);
            this.m_mbAbout.Name = "m_mbAbout";
            this.m_mbAbout.Picture = MobileTech.Windows.UI.ImageKeys.About;
            this.m_mbAbout.PictureDisabled = MobileTech.Windows.UI.ImageKeys.AboutDisabled;
            this.m_mbAbout.PictureFocus = MobileTech.Windows.UI.ImageKeys.AboutFocus;
            this.m_mbAbout.ShowBorder = false;
            this.m_mbAbout.ShowFocusBorder = true;
            this.m_mbAbout.Size = new System.Drawing.Size(114, 68);
            this.m_mbAbout.TabIndex = 7;
            this.m_mbAbout.Text = "About";
            // 
            // m_mbExit
            // 
            this.m_mbExit.BackColor = System.Drawing.Color.White;
            this.m_mbExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbExit.Location = new System.Drawing.Point(123, 220);
            this.m_mbExit.Name = "m_mbExit";
            this.m_mbExit.Picture = MobileTech.Windows.UI.ImageKeys.Done;
            this.m_mbExit.PictureDisabled = MobileTech.Windows.UI.ImageKeys.DoneDisabled;
            this.m_mbExit.PictureFocus = MobileTech.Windows.UI.ImageKeys.DoneFocus;
            this.m_mbExit.ShowBorder = false;
            this.m_mbExit.ShowFocusBorder = true;
            this.m_mbExit.Size = new System.Drawing.Size(114, 68);
            this.m_mbExit.TabIndex = 8;
            this.m_mbExit.Text = "Exit";
            this.m_mbExit.Click += new System.EventHandler(this.OnExitClick);
            // 
            // m_mbCustomerBalance
            // 
            this.m_mbCustomerBalance.BackColor = System.Drawing.Color.White;
            this.m_mbCustomerBalance.Enabled = false;
            this.m_mbCustomerBalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbCustomerBalance.Location = new System.Drawing.Point(4, 148);
            this.m_mbCustomerBalance.Name = "m_mbCustomerBalance";
            this.m_mbCustomerBalance.Picture = MobileTech.Windows.UI.ImageKeys.CustList;
            this.m_mbCustomerBalance.PictureDisabled = MobileTech.Windows.UI.ImageKeys.CustListDisabled;
            this.m_mbCustomerBalance.PictureFocus = MobileTech.Windows.UI.ImageKeys.CustListFocus;
            this.m_mbCustomerBalance.ShowBorder = false;
            this.m_mbCustomerBalance.ShowFocusBorder = true;
            this.m_mbCustomerBalance.Size = new System.Drawing.Size(114, 68);
            this.m_mbCustomerBalance.TabIndex = 5;
            this.m_mbCustomerBalance.Text = "Customer Bal.";
            // 
            // m_mbTCommSetup
            // 
            this.m_mbTCommSetup.BackColor = System.Drawing.Color.White;
            this.m_mbTCommSetup.Enabled = false;
            this.m_mbTCommSetup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbTCommSetup.Location = new System.Drawing.Point(123, 4);
            this.m_mbTCommSetup.Name = "m_mbTCommSetup";
            this.m_mbTCommSetup.Picture = MobileTech.Windows.UI.ImageKeys.TCommSetup;
            this.m_mbTCommSetup.PictureDisabled = MobileTech.Windows.UI.ImageKeys.TCommSetupDisabled;
            this.m_mbTCommSetup.PictureFocus = MobileTech.Windows.UI.ImageKeys.TCommSetupFocus;
            this.m_mbTCommSetup.ShowBorder = false;
            this.m_mbTCommSetup.ShowFocusBorder = true;
            this.m_mbTCommSetup.Size = new System.Drawing.Size(114, 68);
            this.m_mbTCommSetup.TabIndex = 2;
            this.m_mbTCommSetup.Text = "Telecom Setup";
            // 
            // m_mbRouteSetup
            // 
            this.m_mbRouteSetup.BackColor = System.Drawing.Color.White;
            this.m_mbRouteSetup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbRouteSetup.Location = new System.Drawing.Point(4, 4);
            this.m_mbRouteSetup.Name = "m_mbRouteSetup";
            this.m_mbRouteSetup.Picture = MobileTech.Windows.UI.ImageKeys.TrkDrvr;
            this.m_mbRouteSetup.PictureDisabled = MobileTech.Windows.UI.ImageKeys.TrkDrvrDisabled;
            this.m_mbRouteSetup.PictureFocus = MobileTech.Windows.UI.ImageKeys.TrkDrvrFocus;
            this.m_mbRouteSetup.ShowBorder = false;
            this.m_mbRouteSetup.ShowFocusBorder = true;
            this.m_mbRouteSetup.Size = new System.Drawing.Size(114, 68);
            this.m_mbRouteSetup.TabIndex = 1;
            this.m_mbRouteSetup.Text = "Route Setup";
            this.m_mbRouteSetup.Click += new System.EventHandler(this.m_mbRouteSetup_Click);
            // 
            // m_mbSystem
            // 
            this.m_mbSystem.BackColor = System.Drawing.Color.White;
            this.m_mbSystem.Enabled = false;
            this.m_mbSystem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbSystem.Location = new System.Drawing.Point(4, 76);
            this.m_mbSystem.Name = "m_mbSystem";
            this.m_mbSystem.Picture = MobileTech.Windows.UI.ImageKeys.SysInfo;
            this.m_mbSystem.PictureDisabled = MobileTech.Windows.UI.ImageKeys.SysInfoDisabled;
            this.m_mbSystem.PictureFocus = MobileTech.Windows.UI.ImageKeys.SysInfoFocus;
            this.m_mbSystem.ShowBorder = false;
            this.m_mbSystem.ShowFocusBorder = true;
            this.m_mbSystem.Size = new System.Drawing.Size(114, 68);
            this.m_mbSystem.TabIndex = 3;
            this.m_mbSystem.Text = "System Info";
            // 
            // m_mbPen
            // 
            this.m_mbPen.BackColor = System.Drawing.Color.White;
            this.m_mbPen.Enabled = false;
            this.m_mbPen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbPen.Location = new System.Drawing.Point(123, 76);
            this.m_mbPen.Name = "m_mbPen";
            this.m_mbPen.Picture = MobileTech.Windows.UI.ImageKeys.PenTarg;
            this.m_mbPen.PictureDisabled = MobileTech.Windows.UI.ImageKeys.PenTargDisabled;
            this.m_mbPen.PictureFocus = MobileTech.Windows.UI.ImageKeys.PenTargFocus;
            this.m_mbPen.ShowBorder = false;
            this.m_mbPen.ShowFocusBorder = true;
            this.m_mbPen.Size = new System.Drawing.Size(114, 68);
            this.m_mbPen.TabIndex = 4;
            this.m_mbPen.Text = "Pen Alignment";
            // 
            // m_mbSwitch
            // 
            this.m_mbSwitch.BackColor = System.Drawing.Color.White;
            this.m_mbSwitch.Enabled = false;
            this.m_mbSwitch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbSwitch.Location = new System.Drawing.Point(123, 150);
            this.m_mbSwitch.Name = "m_mbSwitch";
            this.m_mbSwitch.Picture = MobileTech.Windows.UI.ImageKeys.SwitchRoute;
            this.m_mbSwitch.PictureDisabled = MobileTech.Windows.UI.ImageKeys.SwitchRouteDisabled;
            this.m_mbSwitch.PictureFocus = MobileTech.Windows.UI.ImageKeys.SwitchRouteFocus;
            this.m_mbSwitch.ShowBorder = false;
            this.m_mbSwitch.ShowFocusBorder = true;
            this.m_mbSwitch.Size = new System.Drawing.Size(114, 68);
            this.m_mbSwitch.TabIndex = 9;
            this.m_mbSwitch.Text = "Switch Route";
            // 
            // SetupMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_mbSwitch);
            this.Controls.Add(this.m_mbAbout);
            this.Controls.Add(this.m_mbExit);
            this.Controls.Add(this.m_mbCustomerBalance);
            this.Controls.Add(this.m_mbTCommSetup);
            this.Controls.Add(this.m_mbRouteSetup);
            this.Controls.Add(this.m_mbSystem);
            this.Controls.Add(this.m_mbPen);
            this.Name = "SetupMenu";
            this.Text = "8000 - Setup Menu";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.MenuButton m_mbPen;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbSystem;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbRouteSetup;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbTCommSetup;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbCustomerBalance;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbExit;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbAbout;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbSwitch;
    }
}