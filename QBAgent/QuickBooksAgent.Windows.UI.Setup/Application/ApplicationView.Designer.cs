namespace QuickBooksAgent.Windows.UI.Setup.Application
{
    partial class ApplicationView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_pnlTrace = new System.Windows.Forms.Panel();
            this.m_chbTrace = new System.Windows.Forms.CheckBox();
            this.m_lblTransactionHistory = new System.Windows.Forms.Label();
            this.m_cmbTransactionHistory = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chkUseUserIdentification = new System.Windows.Forms.CheckBox();
            this.m_cmbUser = new System.Windows.Forms.ComboBox();
            this.m_cmbUserType = new System.Windows.Forms.ComboBox();
            this.m_lblUserIdentification = new System.Windows.Forms.Label();
            this.m_tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_txtEmailFrom = new System.Windows.Forms.TextBox();
            this.m_lblEmailFrom = new System.Windows.Forms.Label();
            this.m_txtSmtpPort = new System.Windows.Forms.TextBox();
            this.m_lblSmtpPort = new System.Windows.Forms.Label();
            this.m_txtSmtpServer = new System.Windows.Forms.TextBox();
            this.m_lblSmtpServer = new System.Windows.Forms.Label();
            this.m_cmbOutlookAccount = new System.Windows.Forms.ComboBox();
            this.m_lblOutlookAccount = new System.Windows.Forms.Label();
            this.m_cmbSettingsType = new System.Windows.Forms.ComboBox();
            this.m_lblSettingsType = new System.Windows.Forms.Label();
            this.m_pnlTrace.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlTrace
            // 
            this.m_pnlTrace.Controls.Add(this.m_chbTrace);
            this.m_pnlTrace.Location = new System.Drawing.Point(3, 140);
            this.m_pnlTrace.Name = "m_pnlTrace";
            this.m_pnlTrace.Size = new System.Drawing.Size(203, 29);
            // 
            // m_chbTrace
            // 
            this.m_chbTrace.Location = new System.Drawing.Point(0, 0);
            this.m_chbTrace.Name = "m_chbTrace";
            this.m_chbTrace.Size = new System.Drawing.Size(86, 20);
            this.m_chbTrace.TabIndex = 1;
            this.m_chbTrace.Text = "Use Trace";
            // 
            // m_lblTransactionHistory
            // 
            this.m_lblTransactionHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTransactionHistory.Location = new System.Drawing.Point(3, 81);
            this.m_lblTransactionHistory.Name = "m_lblTransactionHistory";
            this.m_lblTransactionHistory.Size = new System.Drawing.Size(234, 31);
            this.m_lblTransactionHistory.Text = "How many transactions do you want to keep on your handheld?";
            // 
            // m_cmbTransactionHistory
            // 
            this.m_cmbTransactionHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTransactionHistory.DisplayMember = "Description";
            this.m_cmbTransactionHistory.Location = new System.Drawing.Point(3, 115);
            this.m_cmbTransactionHistory.Name = "m_cmbTransactionHistory";
            this.m_cmbTransactionHistory.Size = new System.Drawing.Size(153, 22);
            this.m_cmbTransactionHistory.TabIndex = 6;
            this.m_cmbTransactionHistory.ValueMember = "DaysCount";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkUseUserIdentification);
            this.panel1.Location = new System.Drawing.Point(3, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 23);
            // 
            // m_chkUseUserIdentification
            // 
            this.m_chkUseUserIdentification.Location = new System.Drawing.Point(0, 0);
            this.m_chkUseUserIdentification.Name = "m_chkUseUserIdentification";
            this.m_chkUseUserIdentification.Size = new System.Drawing.Size(192, 20);
            this.m_chkUseUserIdentification.TabIndex = 1;
            this.m_chkUseUserIdentification.Text = "Use User Identification";
            // 
            // m_cmbUser
            // 
            this.m_cmbUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbUser.Location = new System.Drawing.Point(86, 24);
            this.m_cmbUser.Name = "m_cmbUser";
            this.m_cmbUser.Size = new System.Drawing.Size(151, 22);
            this.m_cmbUser.TabIndex = 3;
            // 
            // m_cmbUserType
            // 
            this.m_cmbUserType.Items.Add("Employee");
            this.m_cmbUserType.Items.Add("Vendor");
            this.m_cmbUserType.Location = new System.Drawing.Point(3, 24);
            this.m_cmbUserType.Name = "m_cmbUserType";
            this.m_cmbUserType.Size = new System.Drawing.Size(81, 22);
            this.m_cmbUserType.TabIndex = 2;
            // 
            // m_lblUserIdentification
            // 
            this.m_lblUserIdentification.Location = new System.Drawing.Point(3, 3);
            this.m_lblUserIdentification.Name = "m_lblUserIdentification";
            this.m_lblUserIdentification.Size = new System.Drawing.Size(108, 21);
            this.m_lblUserIdentification.Text = "User Identification";
            // 
            // m_tabs
            // 
            this.m_tabs.Controls.Add(this.tabPage1);
            this.m_tabs.Controls.Add(this.tabPage2);
            this.m_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabs.Location = new System.Drawing.Point(0, 0);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedIndex = 0;
            this.m_tabs.Size = new System.Drawing.Size(240, 294);
            this.m_tabs.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_lblUserIdentification);
            this.tabPage1.Controls.Add(this.m_pnlTrace);
            this.tabPage1.Controls.Add(this.m_cmbUser);
            this.tabPage1.Controls.Add(this.m_lblTransactionHistory);
            this.tabPage1.Controls.Add(this.m_cmbUserType);
            this.tabPage1.Controls.Add(this.m_cmbTransactionHistory);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(240, 271);
            this.tabPage1.Text = "General";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_txtEmailFrom);
            this.tabPage2.Controls.Add(this.m_lblEmailFrom);
            this.tabPage2.Controls.Add(this.m_txtSmtpPort);
            this.tabPage2.Controls.Add(this.m_lblSmtpPort);
            this.tabPage2.Controls.Add(this.m_txtSmtpServer);
            this.tabPage2.Controls.Add(this.m_lblSmtpServer);
            this.tabPage2.Controls.Add(this.m_cmbOutlookAccount);
            this.tabPage2.Controls.Add(this.m_lblOutlookAccount);
            this.tabPage2.Controls.Add(this.m_cmbSettingsType);
            this.tabPage2.Controls.Add(this.m_lblSettingsType);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(240, 271);
            this.tabPage2.Text = "E-Mail";
            // 
            // m_txtEmailFrom
            // 
            this.m_txtEmailFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtEmailFrom.Location = new System.Drawing.Point(81, 101);
            this.m_txtEmailFrom.MaxLength = 500;
            this.m_txtEmailFrom.Name = "m_txtEmailFrom";
            this.m_txtEmailFrom.Size = new System.Drawing.Size(156, 21);
            this.m_txtEmailFrom.TabIndex = 12;
            // 
            // m_lblEmailFrom
            // 
            this.m_lblEmailFrom.Location = new System.Drawing.Point(3, 102);
            this.m_lblEmailFrom.Name = "m_lblEmailFrom";
            this.m_lblEmailFrom.Size = new System.Drawing.Size(69, 20);
            this.m_lblEmailFrom.Text = "Email From";
            // 
            // m_txtSmtpPort
            // 
            this.m_txtSmtpPort.Location = new System.Drawing.Point(81, 77);
            this.m_txtSmtpPort.MaxLength = 5;
            this.m_txtSmtpPort.Name = "m_txtSmtpPort";
            this.m_txtSmtpPort.Size = new System.Drawing.Size(58, 21);
            this.m_txtSmtpPort.TabIndex = 10;
            this.m_txtSmtpPort.Text = "25";
            // 
            // m_lblSmtpPort
            // 
            this.m_lblSmtpPort.Location = new System.Drawing.Point(3, 78);
            this.m_lblSmtpPort.Name = "m_lblSmtpPort";
            this.m_lblSmtpPort.Size = new System.Drawing.Size(69, 20);
            this.m_lblSmtpPort.Text = "SMTP Port";
            // 
            // m_txtSmtpServer
            // 
            this.m_txtSmtpServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSmtpServer.Location = new System.Drawing.Point(81, 53);
            this.m_txtSmtpServer.MaxLength = 500;
            this.m_txtSmtpServer.Name = "m_txtSmtpServer";
            this.m_txtSmtpServer.Size = new System.Drawing.Size(156, 21);
            this.m_txtSmtpServer.TabIndex = 7;
            // 
            // m_lblSmtpServer
            // 
            this.m_lblSmtpServer.Location = new System.Drawing.Point(3, 54);
            this.m_lblSmtpServer.Name = "m_lblSmtpServer";
            this.m_lblSmtpServer.Size = new System.Drawing.Size(76, 20);
            this.m_lblSmtpServer.Text = "SMTP Server";
            // 
            // m_cmbOutlookAccount
            // 
            this.m_cmbOutlookAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbOutlookAccount.Location = new System.Drawing.Point(109, 28);
            this.m_cmbOutlookAccount.Name = "m_cmbOutlookAccount";
            this.m_cmbOutlookAccount.Size = new System.Drawing.Size(128, 22);
            this.m_cmbOutlookAccount.TabIndex = 4;
            // 
            // m_lblOutlookAccount
            // 
            this.m_lblOutlookAccount.Location = new System.Drawing.Point(3, 29);
            this.m_lblOutlookAccount.Name = "m_lblOutlookAccount";
            this.m_lblOutlookAccount.Size = new System.Drawing.Size(100, 20);
            this.m_lblOutlookAccount.Text = "Outlook Account";
            // 
            // m_cmbSettingsType
            // 
            this.m_cmbSettingsType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbSettingsType.Items.Add("(None)");
            this.m_cmbSettingsType.Items.Add("Outlook Account");
            this.m_cmbSettingsType.Items.Add("Custom");
            this.m_cmbSettingsType.Location = new System.Drawing.Point(109, 3);
            this.m_cmbSettingsType.Name = "m_cmbSettingsType";
            this.m_cmbSettingsType.Size = new System.Drawing.Size(128, 22);
            this.m_cmbSettingsType.TabIndex = 1;
            // 
            // m_lblSettingsType
            // 
            this.m_lblSettingsType.Location = new System.Drawing.Point(3, 5);
            this.m_lblSettingsType.Name = "m_lblSettingsType";
            this.m_lblSettingsType.Size = new System.Drawing.Size(100, 20);
            this.m_lblSettingsType.Text = "Settings Type";
            // 
            // ApplicationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tabs);
            this.Name = "ApplicationView";
            this.Size = new System.Drawing.Size(240, 294);
            this.m_pnlTrace.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnlTrace;
        internal System.Windows.Forms.CheckBox m_chbTrace;
        private System.Windows.Forms.Label m_lblUserIdentification;
        internal System.Windows.Forms.ComboBox m_cmbUser;
        internal System.Windows.Forms.ComboBox m_cmbUserType;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.CheckBox m_chkUseUserIdentification;
        internal System.Windows.Forms.Label m_lblTransactionHistory;
        internal System.Windows.Forms.ComboBox m_cmbTransactionHistory;
        internal System.Windows.Forms.TabControl m_tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label m_lblSettingsType;
        internal System.Windows.Forms.ComboBox m_cmbOutlookAccount;
        internal System.Windows.Forms.ComboBox m_cmbSettingsType;
        internal System.Windows.Forms.TextBox m_txtEmailFrom;
        internal System.Windows.Forms.TextBox m_txtSmtpPort;
        internal System.Windows.Forms.TextBox m_txtSmtpServer;
        internal System.Windows.Forms.Label m_lblOutlookAccount;
        internal System.Windows.Forms.Label m_lblSmtpServer;
        internal System.Windows.Forms.Label m_lblEmailFrom;
        internal System.Windows.Forms.Label m_lblSmtpPort;
    }
}
