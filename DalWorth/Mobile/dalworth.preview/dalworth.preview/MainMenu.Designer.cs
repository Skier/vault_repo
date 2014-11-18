using MobileTech.Windows.UI;

namespace dalworth.preview
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.m_btnStartDay = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_btnPage = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_timerIncome = new System.Windows.Forms.Timer();
            this.m_btnTools = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = " ";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Menu";
            // 
            // m_btnStartDay
            // 
            this.m_btnStartDay.Location = new System.Drawing.Point(3, 3);
            this.m_btnStartDay.Name = "m_btnStartDay";
            this.m_btnStartDay.ShowBorder = false;
            this.m_btnStartDay.Size = new System.Drawing.Size(114, 68);
            this.m_btnStartDay.TabIndex = 0;
            this.m_btnStartDay.Text = "Start Day";
            this.m_btnStartDay.Click += new System.EventHandler(this.OnStartDayClick);
            // 
            // m_btnPage
            // 
            this.m_btnPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPage.Location = new System.Drawing.Point(123, 3);
            this.m_btnPage.Name = "m_btnPage";
            this.m_btnPage.ShowBorder = false;
            this.m_btnPage.Size = new System.Drawing.Size(114, 68);
            this.m_btnPage.TabIndex = 1;
            this.m_btnPage.Text = "Job History";
            this.m_btnPage.Click += new System.EventHandler(this.OnPageClick);
            // 
            // m_timerIncome
            // 
            this.m_timerIncome.Interval = 10000;
            this.m_timerIncome.Tick += new System.EventHandler(this.OnTimerIncomeTick);
            // 
            // m_btnTools
            // 
            this.m_btnTools.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnTools.Location = new System.Drawing.Point(3, 197);
            this.m_btnTools.Name = "m_btnTools";
            this.m_btnTools.ShowBorder = false;
            this.m_btnTools.Size = new System.Drawing.Size(114, 68);
            this.m_btnTools.TabIndex = 3;
            this.m_btnTools.Text = "Tools";
            this.m_btnTools.Click += new System.EventHandler(this.OnToolsClick);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_btnTools);
            this.Controls.Add(this.m_btnPage);
            this.Controls.Add(this.m_btnStartDay);
            this.Menu = this.mainMenu1;
            this.Name = "MainMenu";
            this.Text = "0000 Main Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.MenuButton m_btnStartDay;
        private MobileTech.Windows.UI.Controls.MenuButton m_btnPage;
        private System.Windows.Forms.Timer m_timerIncome;
        private MobileTech.Windows.UI.Controls.MenuButton m_btnTools;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}

