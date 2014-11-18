namespace Dalworth.Server.MainForm.MainForm
{
    partial class MainFormView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormView));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_lblWelcome = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_navigationControl = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.m_navDashboard = new DevExpress.XtraNavBar.NavBarItem();
            this.m_navAccounting = new DevExpress.XtraNavBar.NavBarItem();
            this.m_navLeads = new DevExpress.XtraNavBar.NavBarItem();
            this.m_navFeedback = new DevExpress.XtraNavBar.NavBarItem();
            this.m_navProjects = new DevExpress.XtraNavBar.NavBarItem();
            this.m_navVisits = new DevExpress.XtraNavBar.NavBarItem();
            this.m_navWorks = new DevExpress.XtraNavBar.NavBarItem();
            this.m_navReports = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.m_pnlContent = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_navigationControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlContent)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 684);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(829, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // m_lblWelcome
            // 
            this.m_lblWelcome.Name = "m_lblWelcome";
            this.m_lblWelcome.Size = new System.Drawing.Size(109, 17);
            this.m_lblWelcome.Text = "toolStripStatusLabel1";
            // 
            // m_navigationControl
            // 
            this.m_navigationControl.ActiveGroup = this.navBarGroup1;
            this.m_navigationControl.AllowSelectedLink = true;
            this.m_navigationControl.Appearance.Item.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_navigationControl.Appearance.Item.Options.UseFont = true;
            this.m_navigationControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_navigationControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.m_navigationControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.m_navDashboard,
            this.m_navProjects,
            this.m_navVisits,
            this.m_navWorks,
            this.m_navReports,
            this.m_navLeads,
            this.m_navFeedback,
            this.navBarItem1,
            this.m_navAccounting});
            this.m_navigationControl.Location = new System.Drawing.Point(0, 0);
            this.m_navigationControl.Name = "m_navigationControl";
            this.m_navigationControl.OptionsNavPane.ExpandedWidth = 130;
            this.m_navigationControl.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.m_navigationControl.Size = new System.Drawing.Size(130, 684);
            this.m_navigationControl.StoreDefaultPaintStyleName = true;
            this.m_navigationControl.TabIndex = 1;
            this.m_navigationControl.Text = "m_navigationControl";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "General";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.m_navDashboard),
            new DevExpress.XtraNavBar.NavBarItemLink(this.m_navAccounting),
            new DevExpress.XtraNavBar.NavBarItemLink(this.m_navLeads),
            new DevExpress.XtraNavBar.NavBarItemLink(this.m_navFeedback),
            new DevExpress.XtraNavBar.NavBarItemLink(this.m_navProjects),
            new DevExpress.XtraNavBar.NavBarItemLink(this.m_navVisits),
            new DevExpress.XtraNavBar.NavBarItemLink(this.m_navWorks),
            new DevExpress.XtraNavBar.NavBarItemLink(this.m_navReports)});
            this.navBarGroup1.Name = "navBarGroup1";
            this.navBarGroup1.SelectedLinkIndex = 1;
            // 
            // m_navDashboard
            // 
            this.m_navDashboard.Caption = "1 - Dashboard";
            this.m_navDashboard.LargeImage = ((System.Drawing.Image)(resources.GetObject("m_navDashboard.LargeImage")));
            this.m_navDashboard.Name = "m_navDashboard";
            // 
            // m_navAccounting
            // 
            this.m_navAccounting.Caption = "2-Customers";
            this.m_navAccounting.LargeImage = global::Dalworth.Server.MainForm.Properties.Resources.customer;
            this.m_navAccounting.Name = "m_navAccounting";
            // 
            // m_navLeads
            // 
            this.m_navLeads.Caption = "3 - Leads";
            this.m_navLeads.LargeImage = global::Dalworth.Server.MainForm.Properties.Resources.lead;
            this.m_navLeads.Name = "m_navLeads";
            // 
            // m_navFeedback
            // 
            this.m_navFeedback.Caption = "4-Feedback";
            this.m_navFeedback.LargeImage = global::Dalworth.Server.MainForm.Properties.Resources.reply;
            this.m_navFeedback.Name = "m_navFeedback";
            // 
            // m_navProjects
            // 
            this.m_navProjects.Caption = "5 - Projects";
            this.m_navProjects.LargeImage = ((System.Drawing.Image)(resources.GetObject("m_navProjects.LargeImage")));
            this.m_navProjects.Name = "m_navProjects";
            // 
            // m_navVisits
            // 
            this.m_navVisits.Caption = "6 - Visits";
            this.m_navVisits.LargeImage = global::Dalworth.Server.MainForm.Properties.Resources.visits;
            this.m_navVisits.Name = "m_navVisits";
            // 
            // m_navWorks
            // 
            this.m_navWorks.Caption = "7 - Works";
            this.m_navWorks.LargeImage = global::Dalworth.Server.MainForm.Properties.Resources.works;
            this.m_navWorks.Name = "m_navWorks";
            // 
            // m_navReports
            // 
            this.m_navReports.Caption = "8 - Reports";
            this.m_navReports.LargeImage = global::Dalworth.Server.MainForm.Properties.Resources.reports;
            this.m_navReports.Name = "m_navReports";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "9-Accounting";
            this.navBarItem1.Name = "navBarItem1";
            // 
            // m_pnlContent
            // 
            this.m_pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlContent.Location = new System.Drawing.Point(130, 0);
            this.m_pnlContent.Name = "m_pnlContent";
            this.m_pnlContent.Size = new System.Drawing.Size(699, 684);
            this.m_pnlContent.TabIndex = 2;
            // 
            // MainFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 706);
            this.Controls.Add(this.m_pnlContent);
            this.Controls.Add(this.m_navigationControl);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFormView";
            this.Text = "Restoration.NET V1.06.01";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.m_navigationControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel m_lblWelcome;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        internal DevExpress.XtraEditors.PanelControl m_pnlContent;
        internal DevExpress.XtraNavBar.NavBarItem m_navDashboard;
        internal DevExpress.XtraNavBar.NavBarItem m_navProjects;
        internal DevExpress.XtraNavBar.NavBarItem m_navVisits;
        internal DevExpress.XtraNavBar.NavBarItem m_navWorks;
        internal DevExpress.XtraNavBar.NavBarControl m_navigationControl;
        internal DevExpress.XtraNavBar.NavBarItem m_navReports;
        internal DevExpress.XtraNavBar.NavBarItem m_navLeads;
        internal DevExpress.XtraNavBar.NavBarItem m_navFeedback;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        internal DevExpress.XtraNavBar.NavBarItem m_navAccounting;
    }
}
