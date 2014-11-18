namespace Dalworth.PpcTesterWin
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
            this.components = new System.ComponentModel.Container();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.m_cmbCompaigns = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_tabImport = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.m_dtStatsStart = new DevExpress.XtraEditors.DateEdit();
            this.m_btnUpdateCompanyStats = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnUpdateCampaignStats = new DevExpress.XtraEditors.SimpleButton();
            this.m_cmbCompany = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnUpdateCompaign = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnUpdateAllCampaigns = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.m_btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.m_spinMaxImpressions = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbCampaignsTesting = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_cmbCompanyTesting = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCompaigns.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabImport)).BeginInit();
            this.m_tabImport.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtStatsStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtStatsStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCompany.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinMaxImpressions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCampaignsTesting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCompanyTesting.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(35, 129);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 19);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Campaign";
            // 
            // m_cmbCompaigns
            // 
            this.m_cmbCompaigns.Location = new System.Drawing.Point(128, 131);
            this.m_cmbCompaigns.Name = "m_cmbCompaigns";
            this.m_cmbCompaigns.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbCompaigns.Size = new System.Drawing.Size(209, 20);
            this.m_cmbCompaigns.TabIndex = 5;
            // 
            // m_tabImport
            // 
            this.m_tabImport.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_tabImport.Location = new System.Drawing.Point(12, 12);
            this.m_tabImport.Name = "m_tabImport";
            this.m_tabImport.SelectedTabPage = this.xtraTabPage1;
            this.m_tabImport.Size = new System.Drawing.Size(895, 595);
            this.m_tabImport.TabIndex = 6;
            this.m_tabImport.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.labelControl4);
            this.xtraTabPage1.Controls.Add(this.m_dtStatsStart);
            this.xtraTabPage1.Controls.Add(this.m_btnUpdateCompanyStats);
            this.xtraTabPage1.Controls.Add(this.m_btnUpdateCampaignStats);
            this.xtraTabPage1.Controls.Add(this.m_cmbCompany);
            this.xtraTabPage1.Controls.Add(this.labelControl5);
            this.xtraTabPage1.Controls.Add(this.m_btnUpdateCompaign);
            this.xtraTabPage1.Controls.Add(this.labelControl3);
            this.xtraTabPage1.Controls.Add(this.m_btnUpdateAllCampaigns);
            this.xtraTabPage1.Controls.Add(this.labelControl1);
            this.xtraTabPage1.Controls.Add(this.m_cmbCompaigns);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(886, 564);
            this.xtraTabPage1.Text = "Google Data Import";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(35, 27);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 19);
            this.labelControl4.TabIndex = 19;
            this.labelControl4.Text = "Stats Start";
            // 
            // m_dtStatsStart
            // 
            this.m_dtStatsStart.EditValue = null;
            this.m_dtStatsStart.Location = new System.Drawing.Point(128, 29);
            this.m_dtStatsStart.Name = "m_dtStatsStart";
            this.m_dtStatsStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtStatsStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtStatsStart.Size = new System.Drawing.Size(100, 20);
            this.m_dtStatsStart.TabIndex = 18;
            // 
            // m_btnUpdateCompanyStats
            // 
            this.m_btnUpdateCompanyStats.Location = new System.Drawing.Point(654, 63);
            this.m_btnUpdateCompanyStats.Name = "m_btnUpdateCompanyStats";
            this.m_btnUpdateCompanyStats.Size = new System.Drawing.Size(209, 23);
            this.m_btnUpdateCompanyStats.TabIndex = 17;
            this.m_btnUpdateCompanyStats.Text = "Update Company Stats";
            // 
            // m_btnUpdateCampaignStats
            // 
            this.m_btnUpdateCampaignStats.Location = new System.Drawing.Point(654, 134);
            this.m_btnUpdateCampaignStats.Name = "m_btnUpdateCampaignStats";
            this.m_btnUpdateCampaignStats.Size = new System.Drawing.Size(209, 23);
            this.m_btnUpdateCampaignStats.TabIndex = 16;
            this.m_btnUpdateCampaignStats.Text = "Update Keywords And Stats";
            this.m_btnUpdateCampaignStats.Click += new System.EventHandler(this.m_btnUpdateCampaignStats_Click);
            // 
            // m_cmbCompany
            // 
            this.m_cmbCompany.Location = new System.Drawing.Point(128, 66);
            this.m_cmbCompany.Name = "m_cmbCompany";
            this.m_cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbCompany.Size = new System.Drawing.Size(209, 20);
            this.m_cmbCompany.TabIndex = 15;
            this.m_cmbCompany.SelectedIndexChanged += new System.EventHandler(this.m_cmbCompany_SelectedIndexChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(35, 69);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(67, 19);
            this.labelControl5.TabIndex = 14;
            this.labelControl5.Text = "Company";
            // 
            // m_btnUpdateCompaign
            // 
            this.m_btnUpdateCompaign.Location = new System.Drawing.Point(394, 134);
            this.m_btnUpdateCompaign.Name = "m_btnUpdateCompaign";
            this.m_btnUpdateCompaign.Size = new System.Drawing.Size(209, 23);
            this.m_btnUpdateCompaign.TabIndex = 9;
            this.m_btnUpdateCompaign.Text = "Update Adgroups";
            this.m_btnUpdateCompaign.Click += new System.EventHandler(this.m_btnUpdateCompaign_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(23, 109);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(0, 19);
            this.labelControl3.TabIndex = 8;
            // 
            // m_btnUpdateAllCampaigns
            // 
            this.m_btnUpdateAllCampaigns.Location = new System.Drawing.Point(394, 65);
            this.m_btnUpdateAllCampaigns.Name = "m_btnUpdateAllCampaigns";
            this.m_btnUpdateAllCampaigns.Size = new System.Drawing.Size(220, 23);
            this.m_btnUpdateAllCampaigns.TabIndex = 6;
            this.m_btnUpdateAllCampaigns.Text = "Update All Campaigns";
            this.m_btnUpdateAllCampaigns.Click += new System.EventHandler(this.m_btnUpdateAllCampaigns_Click);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.m_btnTest);
            this.xtraTabPage2.Controls.Add(this.labelControl7);
            this.xtraTabPage2.Controls.Add(this.m_spinMaxImpressions);
            this.xtraTabPage2.Controls.Add(this.labelControl2);
            this.xtraTabPage2.Controls.Add(this.m_cmbCampaignsTesting);
            this.xtraTabPage2.Controls.Add(this.m_cmbCompanyTesting);
            this.xtraTabPage2.Controls.Add(this.labelControl6);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(886, 564);
            this.xtraTabPage2.Text = "Testing";
            // 
            // m_btnTest
            // 
            this.m_btnTest.Location = new System.Drawing.Point(191, 126);
            this.m_btnTest.Name = "m_btnTest";
            this.m_btnTest.Size = new System.Drawing.Size(75, 23);
            this.m_btnTest.TabIndex = 23;
            this.m_btnTest.Text = "Start Test";
            this.m_btnTest.Click += new System.EventHandler(this.m_btnTest_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(23, 91);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(119, 19);
            this.labelControl7.TabIndex = 22;
            this.labelControl7.Text = "Max Impressions";
            // 
            // m_spinMaxImpressions
            // 
            this.m_spinMaxImpressions.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_spinMaxImpressions.Location = new System.Drawing.Point(166, 90);
            this.m_spinMaxImpressions.Name = "m_spinMaxImpressions";
            this.m_spinMaxImpressions.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_spinMaxImpressions.Size = new System.Drawing.Size(100, 20);
            this.m_spinMaxImpressions.TabIndex = 21;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(23, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 19);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "Campaign";
            // 
            // m_cmbCampaignsTesting
            // 
            this.m_cmbCampaignsTesting.Location = new System.Drawing.Point(166, 64);
            this.m_cmbCampaignsTesting.Name = "m_cmbCampaignsTesting";
            this.m_cmbCampaignsTesting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbCampaignsTesting.Size = new System.Drawing.Size(209, 20);
            this.m_cmbCampaignsTesting.TabIndex = 19;
            // 
            // m_cmbCompanyTesting
            // 
            this.m_cmbCompanyTesting.Location = new System.Drawing.Point(166, 36);
            this.m_cmbCompanyTesting.Name = "m_cmbCompanyTesting";
            this.m_cmbCompanyTesting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbCompanyTesting.Size = new System.Drawing.Size(209, 20);
            this.m_cmbCompanyTesting.TabIndex = 17;
            this.m_cmbCompanyTesting.SelectedIndexChanged += new System.EventHandler(this.m_cmbCompanyTesting_SelectedIndexChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(23, 37);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(67, 19);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "Company";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 633);
            this.Controls.Add(this.m_tabImport);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCompaigns.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabImport)).EndInit();
            this.m_tabImport.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtStatsStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtStatsStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCompany.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinMaxImpressions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCampaignsTesting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCompanyTesting.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.ComboBoxEdit m_cmbCompaigns;
        private DevExpress.XtraTab.XtraTabControl m_tabImport;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton m_btnUpdateAllCampaigns;
        private DevExpress.XtraEditors.SimpleButton m_btnUpdateCompaign;
        private DevExpress.XtraEditors.ComboBoxEdit m_cmbCompany;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton m_btnUpdateCompanyStats;
        private DevExpress.XtraEditors.SimpleButton m_btnUpdateCampaignStats;
        private DevExpress.XtraEditors.DateEdit m_dtStatsStart;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit m_cmbCompanyTesting;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit m_cmbCampaignsTesting;
        private DevExpress.XtraEditors.SpinEdit m_spinMaxImpressions;
        private DevExpress.XtraEditors.SimpleButton m_btnTest;
        private DevExpress.XtraEditors.LabelControl labelControl7;
    }
}

