namespace Dalworth.Server.MainForm.ReportBooking
{
    partial class ReportBookingView
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
            this.m_gridBooking = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewBooking = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_columnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columRugCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnFloodCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnConstructionCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnContentCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnMiscellaneousCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.m_linkGridVisit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_linkGridPendingPrint = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewBooking)).BeginInit();
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
            // m_gridBooking
            // 
            this.m_gridBooking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridBooking.EmbeddedNavigator.Name = "";
            this.m_gridBooking.Location = new System.Drawing.Point(0, 32);
            this.m_gridBooking.MainView = this.m_gridViewBooking;
            this.m_gridBooking.Name = "m_gridBooking";
            this.m_gridBooking.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.m_linkGridVisit,
            this.m_linkGridPendingPrint});
            this.m_gridBooking.ShowOnlyPredefinedDetails = true;
            this.m_gridBooking.Size = new System.Drawing.Size(857, 491);
            this.m_gridBooking.TabIndex = 14;
            this.m_gridBooking.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewBooking});
            // 
            // m_gridViewBooking
            // 
            this.m_gridViewBooking.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_columnDate,
            this.m_columRugCount,
            this.m_columnFloodCount,
            this.m_columnConstructionCount,
            this.m_columnContentCount,
            this.m_columnMiscellaneousCount});
            this.m_gridViewBooking.GridControl = this.m_gridBooking;
            this.m_gridViewBooking.Name = "m_gridViewBooking";
            this.m_gridViewBooking.OptionsCustomization.AllowFilter = false;
            this.m_gridViewBooking.OptionsCustomization.AllowGroup = false;
            this.m_gridViewBooking.OptionsNavigation.UseTabKey = false;
            this.m_gridViewBooking.OptionsView.ShowFooter = true;
            this.m_gridViewBooking.OptionsView.ShowGroupPanel = false;
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
            // m_columRugCount
            // 
            this.m_columRugCount.Caption = "Rugs";
            this.m_columRugCount.FieldName = "RugCount";
            this.m_columRugCount.Name = "m_columRugCount";
            this.m_columRugCount.OptionsColumn.AllowEdit = false;
            this.m_columRugCount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columRugCount.Visible = true;
            this.m_columRugCount.VisibleIndex = 1;
            this.m_columRugCount.Width = 137;
            // 
            // m_columnFloodCount
            // 
            this.m_columnFloodCount.Caption = "Floods";
            this.m_columnFloodCount.FieldName = "FloodCount";
            this.m_columnFloodCount.Name = "m_columnFloodCount";
            this.m_columnFloodCount.OptionsColumn.AllowEdit = false;
            this.m_columnFloodCount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnFloodCount.Visible = true;
            this.m_columnFloodCount.VisibleIndex = 2;
            this.m_columnFloodCount.Width = 131;
            // 
            // m_columnConstructionCount
            // 
            this.m_columnConstructionCount.Caption = "Construction";
            this.m_columnConstructionCount.FieldName = "ConstructionCount";
            this.m_columnConstructionCount.Name = "m_columnConstructionCount";
            this.m_columnConstructionCount.OptionsColumn.AllowEdit = false;
            this.m_columnConstructionCount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnConstructionCount.Visible = true;
            this.m_columnConstructionCount.VisibleIndex = 3;
            this.m_columnConstructionCount.Width = 131;
            // 
            // m_columnContentCount
            // 
            this.m_columnContentCount.Caption = "Content";
            this.m_columnContentCount.FieldName = "ContentCount";
            this.m_columnContentCount.Name = "m_columnContentCount";
            this.m_columnContentCount.OptionsColumn.AllowEdit = false;
            this.m_columnContentCount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnContentCount.Visible = true;
            this.m_columnContentCount.VisibleIndex = 4;
            this.m_columnContentCount.Width = 131;
            // 
            // m_columnMiscellaneousCount
            // 
            this.m_columnMiscellaneousCount.Caption = "Misc";
            this.m_columnMiscellaneousCount.FieldName = "MiscellaneousCount";
            this.m_columnMiscellaneousCount.Name = "m_columnMiscellaneousCount";
            this.m_columnMiscellaneousCount.OptionsColumn.AllowEdit = false;
            this.m_columnMiscellaneousCount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnMiscellaneousCount.Visible = true;
            this.m_columnMiscellaneousCount.VisibleIndex = 5;
            this.m_columnMiscellaneousCount.Width = 144;
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
            // ReportBookingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_gridBooking);
            this.Controls.Add(this.panelControl1);
            this.Name = "ReportBookingView";
            this.Size = new System.Drawing.Size(857, 523);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal Dalworth.Server.MainForm.Components.DateRange m_ctlDateRange;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraGrid.GridControl m_gridBooking;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewBooking;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnDate;
        private DevExpress.XtraGrid.Columns.GridColumn m_columRugCount;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridVisit;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridPendingPrint;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnFloodCount;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnContentCount;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnConstructionCount;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnMiscellaneousCount;
    }
}
