namespace Dalworth.Server.MainForm.ReportTechnicianProduction
{
    partial class ReportTechnicianProductionView
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
            Dalworth.Server.Domain.DateRange dateRange1 = new Dalworth.Server.Domain.DateRange();
            this.m_pnlReportContent = new DevExpress.XtraEditors.PanelControl();
            this.m_gridTechnicianProduction = new DevExpress.XtraGrid.GridControl();
            this.m_gridTechnicianProductionView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_columnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.m_linkGridVisit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_linkGridPendingPrint = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_gridShortcut = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbTechnician = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_ctlDateRange = new Dalworth.Server.MainForm.Components.DateRange();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).BeginInit();
            this.m_pnlReportContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianProduction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianProductionView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTechnician.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pnlReportContent
            // 
            this.m_pnlReportContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlReportContent.Controls.Add(this.m_gridTechnicianProduction);
            this.m_pnlReportContent.Controls.Add(this.m_gridShortcut);
            this.m_pnlReportContent.Controls.Add(this.panelControl1);
            this.m_pnlReportContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlReportContent.Location = new System.Drawing.Point(0, 0);
            this.m_pnlReportContent.Name = "m_pnlReportContent";
            this.m_pnlReportContent.Size = new System.Drawing.Size(916, 437);
            this.m_pnlReportContent.TabIndex = 6;
            // 
            // m_gridTechnicianProduction
            // 
            this.m_gridTechnicianProduction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridTechnicianProduction.EmbeddedNavigator.Name = "";
            this.m_gridTechnicianProduction.Location = new System.Drawing.Point(0, 32);
            this.m_gridTechnicianProduction.MainView = this.m_gridTechnicianProductionView;
            this.m_gridTechnicianProduction.Name = "m_gridTechnicianProduction";
            this.m_gridTechnicianProduction.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.m_linkGridVisit,
            this.m_linkGridPendingPrint});
            this.m_gridTechnicianProduction.ShowOnlyPredefinedDetails = true;
            this.m_gridTechnicianProduction.Size = new System.Drawing.Size(916, 405);
            this.m_gridTechnicianProduction.TabIndex = 2;
            this.m_gridTechnicianProduction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridTechnicianProductionView});
            // 
            // m_gridTechnicianProductionView
            // 
            this.m_gridTechnicianProductionView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_columnDate,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13});
            this.m_gridTechnicianProductionView.GridControl = this.m_gridTechnicianProduction;
            this.m_gridTechnicianProductionView.Name = "m_gridTechnicianProductionView";
            this.m_gridTechnicianProductionView.OptionsCustomization.AllowFilter = false;
            this.m_gridTechnicianProductionView.OptionsCustomization.AllowGroup = false;
            this.m_gridTechnicianProductionView.OptionsNavigation.UseTabKey = false;
            this.m_gridTechnicianProductionView.OptionsView.ShowFooter = true;
            this.m_gridTechnicianProductionView.OptionsView.ShowGroupPanel = false;
            // 
            // m_columnDate
            // 
            this.m_columnDate.Caption = "Date";
            this.m_columnDate.DisplayFormat.FormatString = "d";
            this.m_columnDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.m_columnDate.FieldName = "Date";
            this.m_columnDate.Name = "m_columnDate";
            this.m_columnDate.OptionsColumn.AllowEdit = false;
            this.m_columnDate.Visible = true;
            this.m_columnDate.VisibleIndex = 0;
            this.m_columnDate.Width = 55;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Floods";
            this.gridColumn1.FieldName = "FloodClosedQty";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 51;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Part 2";
            this.gridColumn2.FieldName = "HelpClosedQty";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 53;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Estimate";
            this.gridColumn3.FieldName = "FloodCancelledQty";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 57;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Relay/Clean Amt";
            this.gridColumn4.DisplayFormat.FormatString = "C";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "RestorationMiscAmount";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.SummaryItem.DisplayFormat = "{0:C2}";
            this.gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 91;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Monitor";
            this.gridColumn5.FieldName = "MonitoringClosedQty";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 60;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Rest Amt";
            this.gridColumn6.DisplayFormat.FormatString = "C";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "RestorationDepartmentAmount";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.SummaryItem.DisplayFormat = "{0:C2}";
            this.gridColumn6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 60;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Rug PU";
            this.gridColumn7.FieldName = "RugPickupQty";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 60;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Rug PU Est Amt";
            this.gridColumn8.DisplayFormat.FormatString = "C";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "RugPickupEstimatedAmount";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.SummaryItem.DisplayFormat = "{0:C2}";
            this.gridColumn8.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 88;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Oversales";
            this.gridColumn9.DisplayFormat.FormatString = "C";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "RugPickupOptionsAmount";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.SummaryItem.DisplayFormat = "{0:C2}";
            this.gridColumn9.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            this.gridColumn9.Width = 54;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Rug DEL";
            this.gridColumn10.FieldName = "RugDeliveryClosedQty";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 10;
            this.gridColumn10.Width = 54;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Rug Clean Amt";
            this.gridColumn11.DisplayFormat.FormatString = "C";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "RugCleaningDepartmentAmount";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.SummaryItem.DisplayFormat = "{0:C2}";
            this.gridColumn11.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 11;
            this.gridColumn11.Width = 77;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Total Amt";
            this.gridColumn12.DisplayFormat.FormatString = "C";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn12.FieldName = "TotalAmount";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.SummaryItem.DisplayFormat = "{0:C2}";
            this.gridColumn12.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 12;
            this.gridColumn12.Width = 82;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Hours";
            this.gridColumn13.FieldName = "WorkingHours";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 13;
            this.gridColumn13.Width = 53;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // m_linkGridVisit
            // 
            this.m_linkGridVisit.AutoHeight = false;
            this.m_linkGridVisit.Name = "m_linkGridVisit";
            // 
            // m_linkGridPendingPrint
            // 
            this.m_linkGridPendingPrint.AutoHeight = false;
            this.m_linkGridPendingPrint.Name = "m_linkGridPendingPrint";
            // 
            // m_gridShortcut
            // 
            this.m_gridShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_gridShortcut.Location = new System.Drawing.Point(3, 110);
            this.m_gridShortcut.Name = "m_gridShortcut";
            this.m_gridShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_gridShortcut.TabIndex = 1;
            this.m_gridShortcut.Text = "&B table schortcut";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_cmbTechnician);
            this.panelControl1.Controls.Add(this.m_ctlDateRange);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(916, 32);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(384, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "&Technician";
            // 
            // m_cmbTechnician
            // 
            this.m_cmbTechnician.Location = new System.Drawing.Point(440, 5);
            this.m_cmbTechnician.Name = "m_cmbTechnician";
            this.m_cmbTechnician.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTechnician.Size = new System.Drawing.Size(155, 20);
            this.m_cmbTechnician.TabIndex = 3;
            // 
            // m_ctlDateRange
            // 
            dateRange1.EndDate = null;
            dateRange1.StartDate = null;
            this.m_ctlDateRange.EditValue = dateRange1;
            this.m_ctlDateRange.Location = new System.Drawing.Point(66, 5);
            this.m_ctlDateRange.Name = "m_ctlDateRange";
            this.m_ctlDateRange.Size = new System.Drawing.Size(272, 20);
            this.m_ctlDateRange.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "&Dates";
            // 
            // ReportTechnicianProductionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_pnlReportContent);
            this.Name = "ReportTechnicianProductionView";
            this.Size = new System.Drawing.Size(916, 437);
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).EndInit();
            this.m_pnlReportContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianProduction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianProductionView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTechnician.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.PanelControl m_pnlReportContent;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl m_gridShortcut;
        internal Dalworth.Server.MainForm.Components.DateRange m_ctlDateRange;
        internal DevExpress.XtraGrid.GridControl m_gridTechnicianProduction;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridTechnicianProductionView;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridVisit;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridPendingPrint;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbTechnician;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;        
    }
}
