namespace Dalworth.Server.MainForm.Works
{
    partial class WorksView
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
            Dalworth.Server.Domain.DateRange dateRange3 = new Dalworth.Server.Domain.DateRange();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_cmbTechnician = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_cmbStatus = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.m_dateRange = new Dalworth.Server.MainForm.Components.DateRange();
            this.m_txtDispatch = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtVan = new DevExpress.XtraEditors.TextEdit();
            this.m_txtWorkId = new DevExpress.XtraEditors.TextEdit();
            this.m_gridVisits = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewVisits = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colLinkVisit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_linkVisitVisit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_colLinkDashboard = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_linkVisitDashboard = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.m_gridShortcut2 = new DevExpress.XtraEditors.LabelControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.m_gridWorks = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewWorks = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_gridShortcut1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTechnician.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtDispatch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtWorkId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkVisitVisit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkVisitDashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridWorks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewWorks)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_cmbTechnician);
            this.panelControl1.Controls.Add(this.m_cmbStatus);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_btnRefresh);
            this.panelControl1.Controls.Add(this.m_btnClear);
            this.panelControl1.Controls.Add(this.m_dateRange);
            this.panelControl1.Controls.Add(this.m_txtDispatch);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.m_txtVan);
            this.panelControl1.Controls.Add(this.m_txtWorkId);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(996, 84);
            this.panelControl1.TabIndex = 0;
            // 
            // m_cmbTechnician
            // 
            this.m_cmbTechnician.EditValue = 0;
            this.m_cmbTechnician.Location = new System.Drawing.Point(57, 6);
            this.m_cmbTechnician.Name = "m_cmbTechnician";
            this.m_cmbTechnician.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTechnician.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("All", 0, -1)});
            this.m_cmbTechnician.Size = new System.Drawing.Size(184, 20);
            this.m_cmbTechnician.TabIndex = 1;
            // 
            // m_cmbStatus
            // 
            this.m_cmbStatus.EditValue = 0;
            this.m_cmbStatus.Location = new System.Drawing.Point(342, 31);
            this.m_cmbStatus.Name = "m_cmbStatus";
            this.m_cmbStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("All", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pending", 3, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ready", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Working", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Completed", 4, -1)});
            this.m_cmbStatus.Size = new System.Drawing.Size(156, 20);
            this.m_cmbStatus.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(294, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "D&ate";
            // 
            // m_btnRefresh
            // 
            this.m_btnRefresh.Location = new System.Drawing.Point(682, 31);
            this.m_btnRefresh.Name = "m_btnRefresh";
            this.m_btnRefresh.Size = new System.Drawing.Size(83, 23);
            this.m_btnRefresh.TabIndex = 13;
            this.m_btnRefresh.Text = "&Refresh";
            // 
            // m_btnClear
            // 
            this.m_btnClear.Location = new System.Drawing.Point(682, 5);
            this.m_btnClear.Name = "m_btnClear";
            this.m_btnClear.Size = new System.Drawing.Size(83, 23);
            this.m_btnClear.TabIndex = 12;
            this.m_btnClear.Text = "&Clear";
            // 
            // m_dateRange
            // 
            dateRange3.EndDate = null;
            dateRange3.StartDate = null;
            this.m_dateRange.EditValue = dateRange3;
            this.m_dateRange.Location = new System.Drawing.Point(342, 6);
            this.m_dateRange.Name = "m_dateRange";
            this.m_dateRange.Size = new System.Drawing.Size(323, 20);
            this.m_dateRange.TabIndex = 7;
            // 
            // m_txtDispatch
            // 
            this.m_txtDispatch.Location = new System.Drawing.Point(57, 32);
            this.m_txtDispatch.Name = "m_txtDispatch";
            this.m_txtDispatch.Size = new System.Drawing.Size(184, 20);
            this.m_txtDispatch.TabIndex = 3;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(294, 35);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(31, 13);
            this.labelControl9.TabIndex = 8;
            this.labelControl9.Text = "&Status";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(3, 35);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(41, 13);
            this.labelControl7.TabIndex = 2;
            this.labelControl7.Text = "&Dispatch";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(3, 9);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(50, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Tec&hnician";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(3, 61);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(18, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "&Van";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(507, 35);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(39, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "&Work ID";
            // 
            // m_txtVan
            // 
            this.m_txtVan.Location = new System.Drawing.Point(57, 58);
            this.m_txtVan.Name = "m_txtVan";
            this.m_txtVan.Size = new System.Drawing.Size(101, 20);
            this.m_txtVan.TabIndex = 5;
            // 
            // m_txtWorkId
            // 
            this.m_txtWorkId.Location = new System.Drawing.Point(552, 32);
            this.m_txtWorkId.Name = "m_txtWorkId";
            this.m_txtWorkId.Size = new System.Drawing.Size(113, 20);
            this.m_txtWorkId.TabIndex = 11;
            // 
            // m_gridVisits
            // 
            this.m_gridVisits.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_gridVisits.EmbeddedNavigator.Name = "";
            this.m_gridVisits.Location = new System.Drawing.Point(2, 2);
            this.m_gridVisits.MainView = this.m_gridViewVisits;
            this.m_gridVisits.Name = "m_gridVisits";
            this.m_gridVisits.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.m_linkVisitDashboard,
            this.m_linkVisitVisit});
            this.m_gridVisits.ShowOnlyPredefinedDetails = true;
            this.m_gridVisits.Size = new System.Drawing.Size(541, 223);
            this.m_gridVisits.TabIndex = 1;
            this.m_gridVisits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewVisits});
            // 
            // m_gridViewVisits
            // 
            this.m_gridViewVisits.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn8,
            this.gridColumn13,
            this.m_colLinkVisit,
            this.m_colLinkDashboard});
            this.m_gridViewVisits.GridControl = this.m_gridVisits;
            this.m_gridViewVisits.Name = "m_gridViewVisits";
            this.m_gridViewVisits.OptionsCustomization.AllowFilter = false;
            this.m_gridViewVisits.OptionsCustomization.AllowGroup = false;
            this.m_gridViewVisits.OptionsNavigation.UseTabKey = false;
            this.m_gridViewVisits.OptionsView.ShowGroupPanel = false;
            this.m_gridViewVisits.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn8, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Status";
            this.gridColumn11.FieldName = "VisitStatusText";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            this.gridColumn11.Width = 69;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Time";
            this.gridColumn8.DisplayFormat.FormatString = "t";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn8.FieldName = "StartTime";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Customer";
            this.gridColumn13.FieldName = "CustomerName";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            this.gridColumn13.Width = 324;
            // 
            // m_colLinkVisit
            // 
            this.m_colLinkVisit.ColumnEdit = this.m_linkVisitVisit;
            this.m_colLinkVisit.FieldName = "VisitLink";
            this.m_colLinkVisit.Name = "m_colLinkVisit";
            this.m_colLinkVisit.Visible = true;
            this.m_colLinkVisit.VisibleIndex = 3;
            this.m_colLinkVisit.Width = 50;
            // 
            // m_linkVisitVisit
            // 
            this.m_linkVisitVisit.AutoHeight = false;
            this.m_linkVisitVisit.Name = "m_linkVisitVisit";
            // 
            // m_colLinkDashboard
            // 
            this.m_colLinkDashboard.ColumnEdit = this.m_linkVisitDashboard;
            this.m_colLinkDashboard.FieldName = "DashboardLink";
            this.m_colLinkDashboard.Name = "m_colLinkDashboard";
            this.m_colLinkDashboard.Visible = true;
            this.m_colLinkDashboard.VisibleIndex = 4;
            this.m_colLinkDashboard.Width = 77;
            // 
            // m_linkVisitDashboard
            // 
            this.m_linkVisitDashboard.AutoHeight = false;
            this.m_linkVisitDashboard.Name = "m_linkVisitDashboard";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.m_gridShortcut2);
            this.panelControl2.Controls.Add(this.m_gridVisits);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 348);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(996, 227);
            this.panelControl2.TabIndex = 2;
            // 
            // m_gridShortcut2
            // 
            this.m_gridShortcut2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_gridShortcut2.Location = new System.Drawing.Point(12, 86);
            this.m_gridShortcut2.Name = "m_gridShortcut2";
            this.m_gridShortcut2.Size = new System.Drawing.Size(0, 0);
            this.m_gridShortcut2.TabIndex = 0;
            this.m_gridShortcut2.Text = "&B Shortcut";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(0, 342);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(996, 6);
            this.splitterControl1.TabIndex = 6;
            this.splitterControl1.TabStop = false;
            // 
            // m_gridWorks
            // 
            this.m_gridWorks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridWorks.EmbeddedNavigator.Name = "";
            this.m_gridWorks.Location = new System.Drawing.Point(0, 84);
            this.m_gridWorks.MainView = this.m_gridViewWorks;
            this.m_gridWorks.Name = "m_gridWorks";
            this.m_gridWorks.ShowOnlyPredefinedDetails = true;
            this.m_gridWorks.Size = new System.Drawing.Size(996, 258);
            this.m_gridWorks.TabIndex = 2;
            this.m_gridWorks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewWorks});
            // 
            // m_gridViewWorks
            // 
            this.m_gridViewWorks.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn5,
            this.gridColumn2,
            this.gridColumn4});
            this.m_gridViewWorks.GridControl = this.m_gridWorks;
            this.m_gridViewWorks.Name = "m_gridViewWorks";
            this.m_gridViewWorks.OptionsCustomization.AllowFilter = false;
            this.m_gridViewWorks.OptionsCustomization.AllowGroup = false;
            this.m_gridViewWorks.OptionsNavigation.UseTabKey = false;
            this.m_gridViewWorks.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 20;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Technician";
            this.gridColumn3.FieldName = "TechnicianName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Start Date";
            this.gridColumn6.DisplayFormat.FormatString = "d";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn6.FieldName = "StartDate";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Status";
            this.gridColumn5.FieldName = "WorkStatusText";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Dispatch";
            this.gridColumn2.FieldName = "DispatchName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Van";
            this.gridColumn4.FieldName = "VanName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Start Message";
            this.gridColumn7.FieldName = "StartMessage";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // m_gridShortcut1
            // 
            this.m_gridShortcut1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_gridShortcut1.Location = new System.Drawing.Point(12, 161);
            this.m_gridShortcut1.Name = "m_gridShortcut1";
            this.m_gridShortcut1.Size = new System.Drawing.Size(0, 0);
            this.m_gridShortcut1.TabIndex = 1;
            this.m_gridShortcut1.Text = "&B Shortcut";
            // 
            // WorksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_gridShortcut1);
            this.Controls.Add(this.m_gridWorks);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "WorksView";
            this.Size = new System.Drawing.Size(996, 575);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTechnician.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtDispatch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtWorkId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkVisitVisit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkVisitDashboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridWorks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewWorks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraGrid.GridControl m_gridVisits;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        internal DevExpress.XtraGrid.GridControl m_gridWorks;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewWorks;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.TextEdit m_txtVan;
        internal DevExpress.XtraEditors.TextEdit m_txtWorkId;
        internal DevExpress.XtraEditors.TextEdit m_txtDispatch;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewVisits;
        internal DevExpress.XtraEditors.SimpleButton m_btnClear;
        internal DevExpress.XtraEditors.SimpleButton m_btnRefresh;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkVisitDashboard;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkVisitVisit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal Dalworth.Server.MainForm.Components.DateRange m_dateRange;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbTechnician;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colLinkVisit;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colLinkDashboard;
        private DevExpress.XtraEditors.LabelControl m_gridShortcut2;
        private DevExpress.XtraEditors.LabelControl m_gridShortcut1;
    }
}
