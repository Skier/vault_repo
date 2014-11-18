namespace Dalworth.Server.MainForm.ReportConstructionSummary
{
    partial class ReportConstructionSummaryView
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_ctlDateRange = new Dalworth.Server.MainForm.Components.DateRange();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_gridConstructionSummary = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewConstructionSummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_columnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnLeadsQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnSignUpsQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnScopeAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnBilledAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnWorkInProgressAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.m_linkGridVisit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_linkGridPendingPrint = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridConstructionSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewConstructionSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_ctlDateRange);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(857, 32);
            this.panelControl1.TabIndex = 1;
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
            // m_gridConstructionSummary
            // 
            this.m_gridConstructionSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridConstructionSummary.EmbeddedNavigator.Name = "";
            this.m_gridConstructionSummary.Location = new System.Drawing.Point(0, 32);
            this.m_gridConstructionSummary.MainView = this.m_gridViewConstructionSummary;
            this.m_gridConstructionSummary.Name = "m_gridConstructionSummary";
            this.m_gridConstructionSummary.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.m_linkGridVisit,
            this.m_linkGridPendingPrint});
            this.m_gridConstructionSummary.ShowOnlyPredefinedDetails = true;
            this.m_gridConstructionSummary.Size = new System.Drawing.Size(857, 491);
            this.m_gridConstructionSummary.TabIndex = 14;
            this.m_gridConstructionSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewConstructionSummary});
            // 
            // m_gridViewConstructionSummary
            // 
            this.m_gridViewConstructionSummary.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_columnDate,
            this.m_columnLeadsQty,
            this.m_columnSignUpsQty,
            this.m_columnScopeAmt,
            this.m_columnBilledAmt,
            this.m_columnWorkInProgressAmt});
            this.m_gridViewConstructionSummary.GridControl = this.m_gridConstructionSummary;
            this.m_gridViewConstructionSummary.Name = "m_gridViewConstructionSummary";
            this.m_gridViewConstructionSummary.OptionsCustomization.AllowFilter = false;
            this.m_gridViewConstructionSummary.OptionsCustomization.AllowGroup = false;
            this.m_gridViewConstructionSummary.OptionsNavigation.UseTabKey = false;
            this.m_gridViewConstructionSummary.OptionsView.ShowFooter = true;
            this.m_gridViewConstructionSummary.OptionsView.ShowGroupPanel = false;
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
            this.m_columnDate.Width = 162;
            // 
            // m_columnLeadsQty
            // 
            this.m_columnLeadsQty.Caption = "#Leads";
            this.m_columnLeadsQty.FieldName = "LeadsQty";
            this.m_columnLeadsQty.Name = "m_columnLeadsQty";
            this.m_columnLeadsQty.OptionsColumn.AllowEdit = false;
            this.m_columnLeadsQty.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnLeadsQty.Visible = true;
            this.m_columnLeadsQty.VisibleIndex = 1;
            this.m_columnLeadsQty.Width = 137;
            // 
            // m_columnSignUpsQty
            // 
            this.m_columnSignUpsQty.Caption = "#Signups";
            this.m_columnSignUpsQty.FieldName = "SignUpsQty";
            this.m_columnSignUpsQty.Name = "m_columnSignUpsQty";
            this.m_columnSignUpsQty.OptionsColumn.AllowEdit = false;
            this.m_columnSignUpsQty.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnSignUpsQty.Visible = true;
            this.m_columnSignUpsQty.VisibleIndex = 2;
            this.m_columnSignUpsQty.Width = 131;
            // 
            // m_columnScopeAmt
            // 
            this.m_columnScopeAmt.Caption = "Total Scope";
            this.m_columnScopeAmt.DisplayFormat.FormatString = "c";
            this.m_columnScopeAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnScopeAmt.FieldName = "ScopeAmt";
            this.m_columnScopeAmt.Name = "m_columnScopeAmt";
            this.m_columnScopeAmt.OptionsColumn.AllowEdit = false;
            this.m_columnScopeAmt.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnScopeAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnScopeAmt.Visible = true;
            this.m_columnScopeAmt.VisibleIndex = 3;
            this.m_columnScopeAmt.Width = 131;
            // 
            // m_columnBilledAmt
            // 
            this.m_columnBilledAmt.Caption = "Amount Billed";
            this.m_columnBilledAmt.DisplayFormat.FormatString = "c";
            this.m_columnBilledAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnBilledAmt.FieldName = "BilledAmt";
            this.m_columnBilledAmt.Name = "m_columnBilledAmt";
            this.m_columnBilledAmt.OptionsColumn.AllowEdit = false;
            this.m_columnBilledAmt.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnBilledAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnBilledAmt.Visible = true;
            this.m_columnBilledAmt.VisibleIndex = 4;
            this.m_columnBilledAmt.Width = 131;
            // 
            // m_columnWorkInProgressAmt
            // 
            this.m_columnWorkInProgressAmt.Caption = "Work In Progress";
            this.m_columnWorkInProgressAmt.DisplayFormat.FormatString = "c";
            this.m_columnWorkInProgressAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnWorkInProgressAmt.FieldName = "WorkInProgressAmt";
            this.m_columnWorkInProgressAmt.Name = "m_columnWorkInProgressAmt";
            this.m_columnWorkInProgressAmt.OptionsColumn.AllowEdit = false;
            this.m_columnWorkInProgressAmt.Visible = true;
            this.m_columnWorkInProgressAmt.VisibleIndex = 5;
            this.m_columnWorkInProgressAmt.Width = 144;
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
            // ReportConstructionSummaryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_gridConstructionSummary);
            this.Controls.Add(this.panelControl1);
            this.Name = "ReportConstructionSummaryView";
            this.Size = new System.Drawing.Size(857, 523);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridConstructionSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewConstructionSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal Dalworth.Server.MainForm.Components.DateRange m_ctlDateRange;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraGrid.GridControl m_gridConstructionSummary;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewConstructionSummary;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridVisit;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridPendingPrint;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnSignUpsQty;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnBilledAmt;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnScopeAmt;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnWorkInProgressAmt;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnLeadsQty;
    }
}
