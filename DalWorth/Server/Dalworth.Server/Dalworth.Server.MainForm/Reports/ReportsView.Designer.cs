namespace Dalworth.Server.MainForm.Reports
{
    partial class ReportsView
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnExportXls = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.m_cmbReport = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_pnlReportContent = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.m_btnExportXls);
            this.panelControl2.Controls.Add(this.m_btnPrint);
            this.panelControl2.Controls.Add(this.m_btnPreview);
            this.panelControl2.Controls.Add(this.m_cmbReport);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(996, 35);
            this.panelControl2.TabIndex = 5;
            // 
            // m_btnExportXls
            // 
            this.m_btnExportXls.Location = new System.Drawing.Point(400, 6);
            this.m_btnExportXls.Name = "m_btnExportXls";
            this.m_btnExportXls.Size = new System.Drawing.Size(75, 23);
            this.m_btnExportXls.TabIndex = 5;
            this.m_btnExportXls.Text = "E&xport XLS";
            // 
            // m_btnPrint
            // 
            this.m_btnPrint.Location = new System.Drawing.Point(319, 6);
            this.m_btnPrint.Name = "m_btnPrint";
            this.m_btnPrint.Size = new System.Drawing.Size(75, 23);
            this.m_btnPrint.TabIndex = 4;
            this.m_btnPrint.Text = "P&rint";
            // 
            // m_btnPreview
            // 
            this.m_btnPreview.Location = new System.Drawing.Point(238, 6);
            this.m_btnPreview.Name = "m_btnPreview";
            this.m_btnPreview.Size = new System.Drawing.Size(75, 23);
            this.m_btnPreview.TabIndex = 3;
            this.m_btnPreview.Text = "&Preview";
            // 
            // m_cmbReport
            // 
            this.m_cmbReport.EditValue = "Equipment Summary";
            this.m_cmbReport.Location = new System.Drawing.Point(66, 7);
            this.m_cmbReport.Name = "m_cmbReport";
            this.m_cmbReport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbReport.Properties.Items.AddRange(new object[] {
            "Equipment Summary",
            "Daily Flood Production",
            "Daily Rug Production",
            "Daily Technician Production",
            "Pending Rugs",
            "Ready Rug Order Aging",
            "Construction Timeline",
            "Construction Manager",
            "Construction Lead",
            "Booking",
            "Construction Summary",
            "Content Summary",
            "Ad source by Year",
            "Revenue",
            "Invoices"});
            this.m_cmbReport.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_cmbReport.Size = new System.Drawing.Size(166, 20);
            this.m_cmbReport.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(33, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "R&eport";
            // 
            // m_pnlReportContent
            // 
            this.m_pnlReportContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlReportContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlReportContent.Location = new System.Drawing.Point(0, 35);
            this.m_pnlReportContent.Name = "m_pnlReportContent";
            this.m_pnlReportContent.Size = new System.Drawing.Size(996, 540);
            this.m_pnlReportContent.TabIndex = 6;
            // 
            // ReportsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_pnlReportContent);
            this.Controls.Add(this.panelControl2);
            this.Name = "ReportsView";
            this.Size = new System.Drawing.Size(996, 575);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        internal DevExpress.XtraEditors.PanelControl m_pnlReportContent;
        internal DevExpress.XtraEditors.ComboBoxEdit m_cmbReport;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnPrint;
        internal DevExpress.XtraEditors.SimpleButton m_btnPreview;
        internal DevExpress.XtraEditors.SimpleButton m_btnExportXls;
    }
}
