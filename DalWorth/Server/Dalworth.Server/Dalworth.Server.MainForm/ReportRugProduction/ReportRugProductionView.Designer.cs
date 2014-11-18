namespace Dalworth.Server.MainForm.ReportRugProduction
{
    partial class ReportRugProductionView
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
            this.m_gridRugProduction = new DevExpress.XtraGrid.GridControl();
            this.m_gridRugProductionView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_columnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnPickupScheduledQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnPickupCompleted = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnPickupCompletedPct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnPickupRugsNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnPickupEstimatedAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnOrderBookedAvg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnDeliveryCompletedQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnDeliveryRugsNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnCleaningClosedAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnOrderCompletedAvg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.m_linkGridVisit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_linkGridPendingPrint = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_gridShortcut = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_ctlDateRange = new Dalworth.Server.MainForm.Components.DateRange();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).BeginInit();
            this.m_pnlReportContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRugProduction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRugProductionView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlReportContent
            // 
            this.m_pnlReportContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlReportContent.Controls.Add(this.m_gridRugProduction);
            this.m_pnlReportContent.Controls.Add(this.m_gridShortcut);
            this.m_pnlReportContent.Controls.Add(this.panelControl1);
            this.m_pnlReportContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlReportContent.Location = new System.Drawing.Point(0, 0);
            this.m_pnlReportContent.Name = "m_pnlReportContent";
            this.m_pnlReportContent.Size = new System.Drawing.Size(886, 437);
            this.m_pnlReportContent.TabIndex = 6;
            // 
            // m_gridRugProduction
            // 
            this.m_gridRugProduction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridRugProduction.EmbeddedNavigator.Name = "";
            this.m_gridRugProduction.Location = new System.Drawing.Point(0, 32);
            this.m_gridRugProduction.MainView = this.m_gridRugProductionView;
            this.m_gridRugProduction.Name = "m_gridRugProduction";
            this.m_gridRugProduction.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.m_linkGridVisit,
            this.m_linkGridPendingPrint});
            this.m_gridRugProduction.ShowOnlyPredefinedDetails = true;
            this.m_gridRugProduction.Size = new System.Drawing.Size(886, 405);
            this.m_gridRugProduction.TabIndex = 13;
            this.m_gridRugProduction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridRugProductionView});
            // 
            // m_gridRugProductionView
            // 
            this.m_gridRugProductionView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_columnDate,
            this.m_columnPickupScheduledQty,
            this.m_columnPickupCompleted,
            this.m_columnPickupCompletedPct,
            this.m_columnPickupRugsNumber,
            this.m_columnPickupEstimatedAmt,
            this.m_columnOrderBookedAvg,
            this.m_columnDeliveryCompletedQty,
            this.m_columnDeliveryRugsNumber,
            this.m_columnCleaningClosedAmt,
            this.m_columnOrderCompletedAvg});
            this.m_gridRugProductionView.GridControl = this.m_gridRugProduction;
            this.m_gridRugProductionView.Name = "m_gridRugProductionView";
            this.m_gridRugProductionView.OptionsCustomization.AllowFilter = false;
            this.m_gridRugProductionView.OptionsCustomization.AllowGroup = false;
            this.m_gridRugProductionView.OptionsNavigation.UseTabKey = false;
            this.m_gridRugProductionView.OptionsView.ShowFooter = true;
            this.m_gridRugProductionView.OptionsView.ShowGroupPanel = false;
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
            this.m_columnDate.Width = 71;
            // 
            // m_columnPickupScheduledQty
            // 
            this.m_columnPickupScheduledQty.Caption = "PU Scheduled";
            this.m_columnPickupScheduledQty.FieldName = "PickupScheduledQty";
            this.m_columnPickupScheduledQty.Name = "m_columnPickupScheduledQty";
            this.m_columnPickupScheduledQty.OptionsColumn.AllowEdit = false;
            this.m_columnPickupScheduledQty.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnPickupScheduledQty.Visible = true;
            this.m_columnPickupScheduledQty.VisibleIndex = 1;
            this.m_columnPickupScheduledQty.Width = 52;
            // 
            // m_columnPickupCompleted
            // 
            this.m_columnPickupCompleted.Caption = "PU Completed";
            this.m_columnPickupCompleted.FieldName = "PickupCompletedQty";
            this.m_columnPickupCompleted.Name = "m_columnPickupCompleted";
            this.m_columnPickupCompleted.OptionsColumn.AllowEdit = false;
            this.m_columnPickupCompleted.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnPickupCompleted.Visible = true;
            this.m_columnPickupCompleted.VisibleIndex = 2;
            this.m_columnPickupCompleted.Width = 83;
            // 
            // m_columnPickupCompletedPct
            // 
            this.m_columnPickupCompletedPct.Caption = "PU Completed %";
            this.m_columnPickupCompletedPct.DisplayFormat.FormatString = "p";
            this.m_columnPickupCompletedPct.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnPickupCompletedPct.FieldName = "PickupCompletedPct";
            this.m_columnPickupCompletedPct.Name = "m_columnPickupCompletedPct";
            this.m_columnPickupCompletedPct.OptionsColumn.AllowEdit = false;
            this.m_columnPickupCompletedPct.SummaryItem.DisplayFormat = "{0:P2}";
            this.m_columnPickupCompletedPct.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            this.m_columnPickupCompletedPct.Visible = true;
            this.m_columnPickupCompletedPct.VisibleIndex = 3;
            // 
            // m_columnPickupRugsNumber
            // 
            this.m_columnPickupRugsNumber.Caption = "Number of Rugs Pickup";
            this.m_columnPickupRugsNumber.FieldName = "PickupRugsNumber";
            this.m_columnPickupRugsNumber.Name = "m_columnPickupRugsNumber";
            this.m_columnPickupRugsNumber.OptionsColumn.AllowEdit = false;
            this.m_columnPickupRugsNumber.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnPickupRugsNumber.Visible = true;
            this.m_columnPickupRugsNumber.VisibleIndex = 4;
            // 
            // m_columnPickupEstimatedAmt
            // 
            this.m_columnPickupEstimatedAmt.Caption = "PU Estimated Amt";
            this.m_columnPickupEstimatedAmt.DisplayFormat.FormatString = "C";
            this.m_columnPickupEstimatedAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnPickupEstimatedAmt.FieldName = "PickupEstimatedAmt";
            this.m_columnPickupEstimatedAmt.Name = "m_columnPickupEstimatedAmt";
            this.m_columnPickupEstimatedAmt.OptionsColumn.AllowEdit = false;
            this.m_columnPickupEstimatedAmt.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnPickupEstimatedAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnPickupEstimatedAmt.Visible = true;
            this.m_columnPickupEstimatedAmt.VisibleIndex = 5;
            this.m_columnPickupEstimatedAmt.Width = 83;
            // 
            // m_columnOrderBookedAvg
            // 
            this.m_columnOrderBookedAvg.Caption = "Avg Order Booked";
            this.m_columnOrderBookedAvg.DisplayFormat.FormatString = "C";
            this.m_columnOrderBookedAvg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnOrderBookedAvg.FieldName = "OrderBookedAvg";
            this.m_columnOrderBookedAvg.Name = "m_columnOrderBookedAvg";
            this.m_columnOrderBookedAvg.OptionsColumn.AllowEdit = false;
            this.m_columnOrderBookedAvg.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnOrderBookedAvg.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            this.m_columnOrderBookedAvg.Visible = true;
            this.m_columnOrderBookedAvg.VisibleIndex = 6;
            this.m_columnOrderBookedAvg.Width = 110;
            // 
            // m_columnDeliveryCompletedQty
            // 
            this.m_columnDeliveryCompletedQty.Caption = "DEL Completed";
            this.m_columnDeliveryCompletedQty.FieldName = "DeliveryCompletedQty";
            this.m_columnDeliveryCompletedQty.Name = "m_columnDeliveryCompletedQty";
            this.m_columnDeliveryCompletedQty.OptionsColumn.AllowEdit = false;
            this.m_columnDeliveryCompletedQty.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnDeliveryCompletedQty.Visible = true;
            this.m_columnDeliveryCompletedQty.VisibleIndex = 7;
            this.m_columnDeliveryCompletedQty.Width = 76;
            // 
            // m_columnDeliveryRugsNumber
            // 
            this.m_columnDeliveryRugsNumber.Caption = "DEL Rugs Number";
            this.m_columnDeliveryRugsNumber.FieldName = "DeliveryRugsNumber";
            this.m_columnDeliveryRugsNumber.Name = "m_columnDeliveryRugsNumber";
            this.m_columnDeliveryRugsNumber.OptionsColumn.AllowEdit = false;
            this.m_columnDeliveryRugsNumber.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
            this.m_columnDeliveryRugsNumber.Visible = true;
            this.m_columnDeliveryRugsNumber.VisibleIndex = 8;
            this.m_columnDeliveryRugsNumber.Width = 76;
            // 
            // m_columnCleaningClosedAmt
            // 
            this.m_columnCleaningClosedAmt.Caption = "Closed Amt";
            this.m_columnCleaningClosedAmt.DisplayFormat.FormatString = "C";
            this.m_columnCleaningClosedAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnCleaningClosedAmt.FieldName = "RugCleaningClosedAmt";
            this.m_columnCleaningClosedAmt.Name = "m_columnCleaningClosedAmt";
            this.m_columnCleaningClosedAmt.OptionsColumn.AllowEdit = false;
            this.m_columnCleaningClosedAmt.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnCleaningClosedAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnCleaningClosedAmt.Visible = true;
            this.m_columnCleaningClosedAmt.VisibleIndex = 9;
            this.m_columnCleaningClosedAmt.Width = 76;
            // 
            // m_columnOrderCompletedAvg
            // 
            this.m_columnOrderCompletedAvg.Caption = "Avg Order Completed";
            this.m_columnOrderCompletedAvg.DisplayFormat.FormatString = "C";
            this.m_columnOrderCompletedAvg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnOrderCompletedAvg.FieldName = "OrderCompletedAvg";
            this.m_columnOrderCompletedAvg.Name = "m_columnOrderCompletedAvg";
            this.m_columnOrderCompletedAvg.OptionsColumn.AllowEdit = false;
            this.m_columnOrderCompletedAvg.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnOrderCompletedAvg.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            this.m_columnOrderCompletedAvg.Visible = true;
            this.m_columnOrderCompletedAvg.VisibleIndex = 10;
            this.m_columnOrderCompletedAvg.Width = 104;
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
            this.m_gridShortcut.TabIndex = 2;
            this.m_gridShortcut.Text = "&B table schortcut";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_ctlDateRange);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(886, 32);
            this.panelControl1.TabIndex = 0;
            // 
            // m_ctlDateRange
            // 
            dateRange1.EndDate = null;
            dateRange1.StartDate = null;
            this.m_ctlDateRange.EditValue = dateRange1;
            this.m_ctlDateRange.Location = new System.Drawing.Point(66, 5);
            this.m_ctlDateRange.Name = "m_ctlDateRange";
            this.m_ctlDateRange.Size = new System.Drawing.Size(272, 20);
            this.m_ctlDateRange.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "&Dates";
            // 
            // ReportRugProductionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_pnlReportContent);
            this.Name = "ReportRugProductionView";
            this.Size = new System.Drawing.Size(886, 437);
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).EndInit();
            this.m_pnlReportContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRugProduction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRugProductionView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.PanelControl m_pnlReportContent;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl m_gridShortcut;
        internal Dalworth.Server.MainForm.Components.DateRange m_ctlDateRange;
        internal DevExpress.XtraGrid.GridControl m_gridRugProduction;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridRugProductionView;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridVisit;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridPendingPrint;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnDate;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnPickupScheduledQty;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnPickupCompleted;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnPickupCompletedPct;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnPickupRugsNumber;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnPickupEstimatedAmt;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnOrderBookedAvg;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnDeliveryCompletedQty;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnDeliveryRugsNumber;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnCleaningClosedAmt;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnOrderCompletedAvg;        
    }
}
