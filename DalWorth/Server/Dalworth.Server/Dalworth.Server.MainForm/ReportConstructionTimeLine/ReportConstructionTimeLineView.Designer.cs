namespace Dalworth.Server.MainForm.ReportConstructionTimeLine
{
    partial class ReportConstructionTimeLineView
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
            this.m_pnlReportContent = new DevExpress.XtraEditors.PanelControl();
            this.m_grid = new DevExpress.XtraGrid.GridControl();
            this.m_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.m_linkGridVisit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_linkGridPendingPrint = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_gridShortcut = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_dtpMonth = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).BeginInit();
            this.m_pnlReportContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpMonth.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpMonth.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pnlReportContent
            // 
            this.m_pnlReportContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlReportContent.Controls.Add(this.m_grid);
            this.m_pnlReportContent.Controls.Add(this.m_gridShortcut);
            this.m_pnlReportContent.Controls.Add(this.panelControl1);
            this.m_pnlReportContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlReportContent.Location = new System.Drawing.Point(0, 0);
            this.m_pnlReportContent.Name = "m_pnlReportContent";
            this.m_pnlReportContent.Size = new System.Drawing.Size(920, 437);
            this.m_pnlReportContent.TabIndex = 6;
            // 
            // m_grid
            // 
            this.m_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grid.EmbeddedNavigator.Name = "";
            this.m_grid.Location = new System.Drawing.Point(0, 32);
            this.m_grid.MainView = this.m_gridView;
            this.m_grid.Name = "m_grid";
            this.m_grid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.m_linkGridVisit,
            this.m_linkGridPendingPrint});
            this.m_grid.ShowOnlyPredefinedDetails = true;
            this.m_grid.Size = new System.Drawing.Size(920, 405);
            this.m_grid.TabIndex = 13;
            this.m_grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridView});
            // 
            // m_gridView
            // 
            this.m_gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.m_gridView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.m_gridView.ColumnPanelRowHeight = 50;
            this.m_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
            this.m_gridView.GridControl = this.m_grid;
            this.m_gridView.GroupCount = 1;
            this.m_gridView.Name = "m_gridView";
            this.m_gridView.OptionsCustomization.AllowFilter = false;
            this.m_gridView.OptionsCustomization.AllowGroup = false;
            this.m_gridView.OptionsNavigation.UseTabKey = false;
            this.m_gridView.OptionsView.ShowGroupPanel = false;
            this.m_gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Manager";
            this.gridColumn1.FieldName = "ProjectManagerName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Width = 59;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Customer";
            this.gridColumn2.FieldName = "CustomerName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 158;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Lead Date";
            this.gridColumn3.DisplayFormat.FormatString = "d";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn3.FieldName = "LeadDate";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 52;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Appt. Date";
            this.gridColumn4.DisplayFormat.FormatString = "d";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "ScopeDate";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 52;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Self Generated Lead";
            this.gridColumn5.FieldName = "SelfGeneratedLead";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 52;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Estimate Amount";
            this.gridColumn6.DisplayFormat.FormatString = "C";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "EstimatedAmount";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 69;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Job Declined Date";
            this.gridColumn7.DisplayFormat.FormatString = "d";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn7.FieldName = "DeclineDate";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 52;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Sign Up Date";
            this.gridColumn8.DisplayFormat.FormatString = "d";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn8.FieldName = "SignUpDate";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            this.gridColumn8.Width = 49;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Billing Date";
            this.gridColumn12.DisplayFormat.FormatString = "d";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn12.FieldName = "BillingDate";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 7;
            this.gridColumn12.Width = 47;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Payment Date";
            this.gridColumn13.DisplayFormat.FormatString = "d";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn13.FieldName = "PaymentDate";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 8;
            this.gridColumn13.Width = 47;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Amount Billed";
            this.gridColumn14.DisplayFormat.FormatString = "C";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn14.FieldName = "AmountBilled";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 9;
            this.gridColumn14.Width = 80;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Amount Collected";
            this.gridColumn15.DisplayFormat.FormatString = "C";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn15.FieldName = "AmountCollected";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 10;
            this.gridColumn15.Width = 94;
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
            this.panelControl1.Controls.Add(this.m_dtpMonth);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(920, 32);
            this.panelControl1.TabIndex = 0;
            // 
            // m_dtpMonth
            // 
            this.m_dtpMonth.EditValue = null;
            this.m_dtpMonth.Location = new System.Drawing.Point(66, 5);
            this.m_dtpMonth.Name = "m_dtpMonth";
            this.m_dtpMonth.Properties.ActionButtonIndex = 1;
            this.m_dtpMonth.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.m_dtpMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.Utils.HorzAlignment.Center, null)});
            this.m_dtpMonth.Properties.DisplayFormat.FormatString = "y";
            this.m_dtpMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.m_dtpMonth.Properties.Mask.EditMask = "y";
            this.m_dtpMonth.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.m_dtpMonth.Properties.ValidateOnEnterKey = true;
            this.m_dtpMonth.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpMonth.Size = new System.Drawing.Size(165, 20);
            this.m_dtpMonth.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "&Month";
            // 
            // ReportConstructionTimeLineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_pnlReportContent);
            this.Name = "ReportConstructionTimeLineView";
            this.Size = new System.Drawing.Size(920, 437);
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).EndInit();
            this.m_pnlReportContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpMonth.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpMonth.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.PanelControl m_pnlReportContent;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl m_gridShortcut;
        internal DevExpress.XtraGrid.GridControl m_grid;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridVisit;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridPendingPrint;
        internal DevExpress.XtraEditors.DateEdit m_dtpMonth;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;        
    }
}
