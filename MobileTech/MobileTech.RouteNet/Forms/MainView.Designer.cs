namespace MobileTech.Windows.UI.Forms
{
    partial class MainView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.m_linkCreateDatabase = new System.Windows.Forms.LinkLabel();
            this.m_lbCopyright = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_lbLanguage = new System.Windows.Forms.Label();
            this.m_cbLanguage = new System.Windows.Forms.ComboBox();
            this.m_mbEvents = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbClose = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbMainMenu = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_linkCreateDatabase
            // 
            this.m_linkCreateDatabase.Enabled = false;
            this.m_linkCreateDatabase.Location = new System.Drawing.Point(12, 212);
            this.m_linkCreateDatabase.Name = "m_linkCreateDatabase";
            this.m_linkCreateDatabase.Size = new System.Drawing.Size(120, 20);
            this.m_linkCreateDatabase.TabIndex = 14;
            this.m_linkCreateDatabase.Text = "Database manager";
            this.m_linkCreateDatabase.Visible = false;
            this.m_linkCreateDatabase.Click += new System.EventHandler(this.OnLinkCreateDatabaseClick);
            // 
            // m_lbCopyright
            // 
            this.m_lbCopyright.Location = new System.Drawing.Point(3, 244);
            this.m_lbCopyright.Name = "m_lbCopyright";
            this.m_lbCopyright.Size = new System.Drawing.Size(146, 47);
            this.m_lbCopyright.Text = "© Copyright 2005\r\nMobileTech Solutions, Inc.\r\nAll rights reserved.";
            this.m_lbCopyright.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 84);
            // 
            // m_lbLanguage
            // 
            this.m_lbLanguage.Location = new System.Drawing.Point(12, 178);
            this.m_lbLanguage.Name = "m_lbLanguage";
            this.m_lbLanguage.Size = new System.Drawing.Size(71, 16);
            this.m_lbLanguage.Text = "Language";
            // 
            // m_cbLanguage
            // 
            this.m_cbLanguage.Location = new System.Drawing.Point(89, 172);
            this.m_cbLanguage.Name = "m_cbLanguage";
            this.m_cbLanguage.Size = new System.Drawing.Size(148, 22);
            this.m_cbLanguage.TabIndex = 21;
            this.m_cbLanguage.SelectedIndexChanged += new System.EventHandler(this.OnLanguageSelectedIndexChanged);
            // 
            // m_mbEvents
            // 
            this.m_mbEvents.BackColor = System.Drawing.Color.White;
            this.m_mbEvents.BackDownColor = System.Drawing.Color.Black;
            this.m_mbEvents.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbEvents.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbEvents.ForeDownColor = System.Drawing.Color.White;
            this.m_mbEvents.IconMargin = 3;
            this.m_mbEvents.IconShift = false;
            this.m_mbEvents.IconTextSpace = 3;
            this.m_mbEvents.Location = new System.Drawing.Point(123, 99);
            this.m_mbEvents.Name = "m_mbEvents";
            this.m_mbEvents.Picture = MobileTech.Windows.UI.ImageKeys.Events;
            this.m_mbEvents.PictureDisabled = MobileTech.Windows.UI.ImageKeys.EventsDisabled;
            this.m_mbEvents.PictureFocus = MobileTech.Windows.UI.ImageKeys.EventsFocus;
            this.m_mbEvents.ShowBorder = false;
            this.m_mbEvents.ShowFocusBorder = true;
            this.m_mbEvents.Size = new System.Drawing.Size(98, 61);
            this.m_mbEvents.TabIndex = 20;
            this.m_mbEvents.Text = "Events";
            this.m_mbEvents.TextShift = false;
            this.m_mbEvents.TransparentIcon = true;
            this.m_mbEvents.TransparentImage = true;
            this.m_mbEvents.Click += new System.EventHandler(this.OnEventsClick);
            // 
            // m_mbClose
            // 
            this.m_mbClose.BackColor = System.Drawing.Color.White;
            this.m_mbClose.BackDownColor = System.Drawing.Color.Black;
            this.m_mbClose.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbClose.ForeDownColor = System.Drawing.Color.White;
            this.m_mbClose.IconMargin = 3;
            this.m_mbClose.IconShift = false;
            this.m_mbClose.IconTextSpace = 3;
            this.m_mbClose.Location = new System.Drawing.Point(192, 230);
            this.m_mbClose.Name = "m_mbClose";
            this.m_mbClose.Picture = MobileTech.Windows.UI.ImageKeys.Exit;
            this.m_mbClose.PictureDisabled = MobileTech.Windows.UI.ImageKeys.ExitDisabled;
            this.m_mbClose.PictureFocus = MobileTech.Windows.UI.ImageKeys.ExitFocus;
            this.m_mbClose.ShowBorder = true;
            this.m_mbClose.ShowFocusBorder = true;
            this.m_mbClose.Size = new System.Drawing.Size(48, 64);
            this.m_mbClose.TabIndex = 18;
            this.m_mbClose.Text = "Exit";
            this.m_mbClose.TextShift = false;
            this.m_mbClose.TransparentIcon = true;
            this.m_mbClose.TransparentImage = true;
            this.m_mbClose.Click += new System.EventHandler(this.OnCloseClick);
            // 
            // m_mbMainMenu
            // 
            this.m_mbMainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.m_mbMainMenu.BackDownColor = System.Drawing.Color.Black;
            this.m_mbMainMenu.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbMainMenu.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbMainMenu.ForeDownColor = System.Drawing.Color.White;
            this.m_mbMainMenu.IconMargin = 3;
            this.m_mbMainMenu.IconShift = false;
            this.m_mbMainMenu.IconTextSpace = 3;
            this.m_mbMainMenu.Location = new System.Drawing.Point(3, 90);
            this.m_mbMainMenu.Name = "m_mbMainMenu";
            this.m_mbMainMenu.Picture = MobileTech.Windows.UI.ImageKeys.House;
            this.m_mbMainMenu.PictureDisabled = MobileTech.Windows.UI.ImageKeys.HouseDisabled;
            this.m_mbMainMenu.PictureFocus = MobileTech.Windows.UI.ImageKeys.HouseFocus;
            this.m_mbMainMenu.ShowBorder = false;
            this.m_mbMainMenu.ShowFocusBorder = true;
            this.m_mbMainMenu.Size = new System.Drawing.Size(114, 70);
            this.m_mbMainMenu.TabIndex = 16;
            this.m_mbMainMenu.Text = "Main Menu";
            this.m_mbMainMenu.TextShift = false;
            this.m_mbMainMenu.TransparentIcon = true;
            this.m_mbMainMenu.TransparentImage = true;
            this.m_mbMainMenu.Click += new System.EventHandler(this.OnMainMenuClick);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_linkCreateDatabase);
            this.Controls.Add(this.m_lbCopyright);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.m_lbLanguage);
            this.Controls.Add(this.m_cbLanguage);
            this.Controls.Add(this.m_mbEvents);
            this.Controls.Add(this.m_mbClose);
            this.Controls.Add(this.m_mbMainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainView";
            this.Text = "Mobile Tech";
            this.Activated += new System.EventHandler(this.OnActivated);
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel m_linkCreateDatabase;
        private System.Windows.Forms.Label m_lbCopyright;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label m_lbLanguage;
        private System.Windows.Forms.ComboBox m_cbLanguage;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbEvents;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbClose;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbMainMenu;


    }
}