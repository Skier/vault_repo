namespace Dalworth.Server.MainForm.ReportRevenue
{
    partial class ReportRevenueView
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
            Dalworth.Server.Domain.DateRange dateRange2 = new Dalworth.Server.Domain.DateRange();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblGrandTotal = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_ctlDateRange = new Dalworth.Server.MainForm.Components.DateRange();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_gridRevenue = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewRevenue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_columnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnRugsAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnFloodsAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnConstructionAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnContentAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.m_linkGridVisit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_linkGridPendingPrint = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRevenue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewRevenue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblGrandTotal);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_ctlDateRange);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(857, 32);
            this.panelControl1.TabIndex = 1;
            // 
            // m_lblGrandTotal
            // 
            this.m_lblGrandTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblGrandTotal.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.m_lblGrandTotal.Appearance.Options.UseFont = true;
            this.m_lblGrandTotal.Appearance.Options.UseTextOptions = true;
            this.m_lblGrandTotal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblGrandTotal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblGrandTotal.Location = new System.Drawing.Point(742, 8);
            this.m_lblGrandTotal.Name = "m_lblGrandTotal";
            this.m_lblGrandTotal.Size = new System.Drawing.Size(103, 16);
            this.m_lblGrandTotal.TabIndex = 4;
            this.m_lblGrandTotal.Text = "$(1000000.00)";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(664, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 16);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Grand Total:";
            // 
            // m_ctlDateRange
            // 
            dateRange2.EndDate = null;
            dateRange2.StartDate = null;
            this.m_ctlDateRange.EditValue = dateRange2;
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
            // m_gridRevenue
            // 
            this.m_gridRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridRevenue.EmbeddedNavigator.Name = "";
            this.m_gridRevenue.Location = new System.Drawing.Point(0, 32);
            this.m_gridRevenue.MainView = this.m_gridViewRevenue;
            this.m_gridRevenue.Name = "m_gridRevenue";
            this.m_gridRevenue.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.m_linkGridVisit,
            this.m_linkGridPendingPrint});
            this.m_gridRevenue.ShowOnlyPredefinedDetails = true;
            this.m_gridRevenue.Size = new System.Drawing.Size(857, 491);
            this.m_gridRevenue.TabIndex = 14;
            this.m_gridRevenue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewRevenue});
            // 
            // m_gridViewRevenue
            // 
            this.m_gridViewRevenue.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_columnDate,
            this.m_columnRugsAmt,
            this.m_columnFloodsAmt,
            this.m_columnConstructionAmt,
            this.m_columnContentAmt});
            this.m_gridViewRevenue.GridControl = this.m_gridRevenue;
            this.m_gridViewRevenue.Name = "m_gridViewRevenue";
            this.m_gridViewRevenue.OptionsCustomization.AllowFilter = false;
            this.m_gridViewRevenue.OptionsCustomization.AllowGroup = false;
            this.m_gridViewRevenue.OptionsNavigation.UseTabKey = false;
            this.m_gridViewRevenue.OptionsView.ShowFooter = true;
            this.m_gridViewRevenue.OptionsView.ShowGroupPanel = false;
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
            // m_columnRugsAmt
            // 
            this.m_columnRugsAmt.Caption = "Rugs";
            this.m_columnRugsAmt.DisplayFormat.FormatString = "c";
            this.m_columnRugsAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnRugsAmt.FieldName = "RugsAmt";
            this.m_columnRugsAmt.Name = "m_columnRugsAmt";
            this.m_columnRugsAmt.OptionsColumn.AllowEdit = false;
            this.m_columnRugsAmt.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnRugsAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnRugsAmt.Visible = true;
            this.m_columnRugsAmt.VisibleIndex = 1;
            this.m_columnRugsAmt.Width = 137;
            // 
            // m_columnFloodsAmt
            // 
            this.m_columnFloodsAmt.Caption = "Floods";
            this.m_columnFloodsAmt.DisplayFormat.FormatString = "c";
            this.m_columnFloodsAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnFloodsAmt.FieldName = "FloodsAmt";
            this.m_columnFloodsAmt.Name = "m_columnFloodsAmt";
            this.m_columnFloodsAmt.OptionsColumn.AllowEdit = false;
            this.m_columnFloodsAmt.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnFloodsAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnFloodsAmt.Visible = true;
            this.m_columnFloodsAmt.VisibleIndex = 2;
            this.m_columnFloodsAmt.Width = 131;
            // 
            // m_columnConstructionAmt
            // 
            this.m_columnConstructionAmt.Caption = "Construction";
            this.m_columnConstructionAmt.DisplayFormat.FormatString = "c";
            this.m_columnConstructionAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnConstructionAmt.FieldName = "ConstructionAmt";
            this.m_columnConstructionAmt.Name = "m_columnConstructionAmt";
            this.m_columnConstructionAmt.OptionsColumn.AllowEdit = false;
            this.m_columnConstructionAmt.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnConstructionAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnConstructionAmt.Visible = true;
            this.m_columnConstructionAmt.VisibleIndex = 3;
            this.m_columnConstructionAmt.Width = 131;
            // 
            // m_columnContentAmt
            // 
            this.m_columnContentAmt.Caption = "Content";
            this.m_columnContentAmt.DisplayFormat.FormatString = "c";
            this.m_columnContentAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_columnContentAmt.FieldName = "ContentAmt";
            this.m_columnContentAmt.Name = "m_columnContentAmt";
            this.m_columnContentAmt.OptionsColumn.AllowEdit = false;
            this.m_columnContentAmt.SummaryItem.DisplayFormat = "{0:C2}";
            this.m_columnContentAmt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.m_columnContentAmt.Visible = true;
            this.m_columnContentAmt.VisibleIndex = 4;
            this.m_columnContentAmt.Width = 131;
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
            // ReportRevenueView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_gridRevenue);
            this.Controls.Add(this.panelControl1);
            this.Name = "ReportRevenueView";
            this.Size = new System.Drawing.Size(857, 523);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRevenue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewRevenue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal Dalworth.Server.MainForm.Components.DateRange m_ctlDateRange;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraGrid.GridControl m_gridRevenue;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewRevenue;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnDate;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnRugsAmt;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridVisit;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridPendingPrint;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnFloodsAmt;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnContentAmt;
        private DevExpress.XtraGrid.Columns.GridColumn m_columnConstructionAmt;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.LabelControl m_lblGrandTotal;
    }
}
