namespace Dalworth.Server.MainForm.ReportFloodProduction
{
    partial class ReportFloodProductionView
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
            this.m_gridFloodProduction = new DevExpress.XtraGrid.GridControl();
            this.m_gridFloodProductionView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_columnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnFloodScheduledQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnFloodsSoldQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnSoldPct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnFloodRevenue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.m_linkGridVisit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_linkGridPendingPrint = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_gridShortcut = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_ctlDateRange = new Dalworth.Server.MainForm.Components.DateRange();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).BeginInit();
            this.m_pnlReportContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridFloodProduction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridFloodProductionView)).BeginInit();
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
            this.m_pnlReportContent.Controls.Add(this.m_gridFloodProduction);
            this.m_pnlReportContent.Controls.Add(this.m_gridShortcut);
            this.m_pnlReportContent.Controls.Add(this.panelControl1);
            this.m_pnlReportContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlReportContent.Location = new System.Drawing.Point(0, 0);
            this.m_pnlReportContent.Name = "m_pnlReportContent";
            this.m_pnlReportContent.Size = new System.Drawing.Size(591, 437);
            this.m_pnlReportContent.TabIndex = 6;
            // 
            // m_gridFloodProduction
            // 
            this.m_gridFloodProduction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridFloodProduction.EmbeddedNavigator.Name = "";
            this.m_gridFloodProduction.Location = new System.Drawing.Point(0, 32);
            this.m_gridFloodProduction.MainView = this.m_gridFloodProductionView;
            this.m_gridFloodProduction.Name = "m_gridFloodProduction";
            this.m_gridFloodProduction.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.m_linkGridVisit,
            this.m_linkGridPendingPrint});
            this.m_gridFloodProduction.ShowOnlyPredefinedDetails = true;
            this.m_gridFloodProduction.Size = new System.Drawing.Size(591, 405);
            this.m_gridFloodProduction.TabIndex = 13;
            this.m_gridFloodProduction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridFloodProductionView});
            // 
            // m_gridFloodProductionView
            // 
            this.m_gridFloodProductionView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_columnDate,
            this.m_columnFloodScheduledQty,
            this.m_columnFloodsSoldQty,
            this.m_columnSoldPct,
            this.m_columnFloodRevenue});
            this.m_gridFloodProductionView.GridControl = this.m_gridFloodProduction;
            this.m_gridFloodProductionView.Name = "m_gridFloodProductionView";
            this.m_gridFloodProductionView.OptionsCustomization.AllowFilter = false;
            this.m_gridFloodProductionView.OptionsCustomization.AllowGroup = false;
            this.m_gridFloodProductionView.OptionsNavigation.UseTabKey = false;
            this.m_gridFloodProductionView.OptionsView.ShowFooter = true;
            this.m_gridFloodProductionView.OptionsView.ShowGroupPanel = false;
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
            this.m_columnDate.Width = 126;
            // 
            // m_columnFloodScheduledQty
            // 
            this.m_columnFloodScheduledQty.Caption = "Scheduled floods";
            this.m_columnFloodScheduledQty.FieldName = "FloodScheduledQty";
            this.m_columnFloodScheduledQty.Name = "m_columnFloodScheduledQty";
            this.m_columnFloodScheduledQty.OptionsColumn.AllowEdit = false;
            this.m_columnFloodScheduledQty.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnFloodScheduledQty.Visible = true;
            this.m_columnFloodScheduledQty.VisibleIndex = 1;
            this.m_columnFloodScheduledQty.Width = 105;
            // 
            // m_columnFloodsSoldQty
            // 
            this.m_columnFloodsSoldQty.Caption = "Sold Floods";
            this.m_columnFloodsSoldQty.FieldName = "FloodSoldQty";
            this.m_columnFloodsSoldQty.Name = "m_columnFloodsSoldQty";
            this.m_columnFloodsSoldQty.OptionsColumn.AllowEdit = false;
            this.m_columnFloodsSoldQty.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnFloodsSoldQty.Visible = true;
            this.m_columnFloodsSoldQty.VisibleIndex = 2;
            this.m_columnFloodsSoldQty.Width = 83;
            // 
            // m_columnSoldPct
            // 
            this.m_columnSoldPct.Caption = "Sold %";
            this.m_columnSoldPct.DisplayFormat.FormatString = "p";
            this.m_columnSoldPct.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnSoldPct.FieldName = "FloodSoldPct";
            this.m_columnSoldPct.Name = "m_columnSoldPct";
            this.m_columnSoldPct.OptionsColumn.AllowEdit = false;
            this.m_columnSoldPct.SummaryItem.DisplayFormat = "{0:P0}";
            this.m_columnSoldPct.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            this.m_columnSoldPct.Visible = true;
            this.m_columnSoldPct.VisibleIndex = 3;
            this.m_columnSoldPct.Width = 91;
            // 
            // m_columnFloodRevenue
            // 
            this.m_columnFloodRevenue.Caption = "Revenue";
            this.m_columnFloodRevenue.DisplayFormat.FormatString = "C";
            this.m_columnFloodRevenue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnFloodRevenue.FieldName = "FloodRevenue";
            this.m_columnFloodRevenue.Name = "m_columnFloodRevenue";
            this.m_columnFloodRevenue.OptionsColumn.AllowEdit = false;
            this.m_columnFloodRevenue.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnFloodRevenue.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnFloodRevenue.Visible = true;
            this.m_columnFloodRevenue.VisibleIndex = 4;
            this.m_columnFloodRevenue.Width = 165;
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
            this.panelControl1.Size = new System.Drawing.Size(591, 32);
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
            // ReportFloodProductionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_pnlReportContent);
            this.Name = "ReportFloodProductionView";
            this.Size = new System.Drawing.Size(591, 437);
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).EndInit();
            this.m_pnlReportContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridFloodProduction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridFloodProductionView)).EndInit();
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
        internal DevExpress.XtraGrid.GridControl m_gridFloodProduction;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridFloodProductionView;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridVisit;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridPendingPrint;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnDate;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnFloodRevenue;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnFloodScheduledQty;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnFloodsSoldQty;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnSoldPct;        
    }
}
