namespace Dalworth.Server.MainForm.Components
{
    partial class ProjectEdit
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
            this.components = new System.ComponentModel.Container();
            Dalworth.Server.Domain.ProjectInsurance projectInsurance1 = new Dalworth.Server.Domain.ProjectInsurance();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_tabVisitHistory = new DevExpress.XtraTab.XtraTabControl();
            this.m_tabPageViewHistory = new DevExpress.XtraTab.XtraTabPage();
            this.m_gridVisits = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Technician = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Task = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClosedAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_tabPageInsurance = new DevExpress.XtraTab.XtraTabPage();
            this.m_groupAdvertising = new DevExpress.XtraEditors.GroupControl();
            this.m_lblSalesRep = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbQbSalesRep = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_cmbQbCustomerTypeLevel1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_cmbQbCustomerTypeLevel0 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.m_ctlProjectInsurance = new Dalworth.Server.MainForm.Components.ProjectInsuranceEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabVisitHistory)).BeginInit();
            this.m_tabVisitHistory.SuspendLayout();
            this.m_tabPageViewHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.m_tabPageInsurance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_groupAdvertising)).BeginInit();
            this.m_groupAdvertising.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbSalesRep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbCustomerTypeLevel1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbCustomerTypeLevel0.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_tabVisitHistory);
            this.panelControl1.Controls.Add(this.m_groupAdvertising);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(623, 347);
            this.panelControl1.TabIndex = 0;
            // 
            // m_tabVisitHistory
            // 
            this.m_tabVisitHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabVisitHistory.Location = new System.Drawing.Point(0, 81);
            this.m_tabVisitHistory.Name = "m_tabVisitHistory";
            this.m_tabVisitHistory.SelectedTabPage = this.m_tabPageViewHistory;
            this.m_tabVisitHistory.Size = new System.Drawing.Size(623, 266);
            this.m_tabVisitHistory.TabIndex = 2;
            this.m_tabVisitHistory.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.m_tabPageViewHistory,
            this.m_tabPageInsurance});
            this.m_tabVisitHistory.Text = "Insurance";
            // 
            // m_tabPageViewHistory
            // 
            this.m_tabPageViewHistory.Controls.Add(this.m_gridVisits);
            this.m_tabPageViewHistory.Name = "m_tabPageViewHistory";
            this.m_tabPageViewHistory.Size = new System.Drawing.Size(614, 235);
            this.m_tabPageViewHistory.Text = "Visit History";
            // 
            // m_gridVisits
            // 
            this.m_gridVisits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridVisits.EmbeddedNavigator.Name = "";
            this.m_gridVisits.Location = new System.Drawing.Point(0, 0);
            this.m_gridVisits.MainView = this.gridView1;
            this.m_gridVisits.Name = "m_gridVisits";
            this.m_gridVisits.Size = new System.Drawing.Size(614, 235);
            this.m_gridVisits.TabIndex = 0;
            this.m_gridVisits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.ActiveFilterEnabled = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.Date,
            this.Technician,
            this.Task,
            this.Status,
            this.ClosedAmount});
            this.gridView1.GridControl = this.m_gridVisits;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsDetail.AllowZoomDetail = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsDetail.ShowDetailTabs = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.AllowFocus = false;
            this.ID.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ID.Visible = true;
            this.ID.VisibleIndex = 0;
            this.ID.Width = 62;
            // 
            // Date
            // 
            this.Date.Caption = "Date";
            this.Date.DisplayFormat.FormatString = "MM/dd/yyyy";
            this.Date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Date.FieldName = "Date";
            this.Date.Name = "Date";
            this.Date.OptionsColumn.AllowEdit = false;
            this.Date.Visible = true;
            this.Date.VisibleIndex = 4;
            this.Date.Width = 99;
            // 
            // Technician
            // 
            this.Technician.Caption = "Technician";
            this.Technician.FieldName = "TechnicianName";
            this.Technician.Name = "Technician";
            this.Technician.OptionsColumn.AllowEdit = false;
            this.Technician.Visible = true;
            this.Technician.VisibleIndex = 2;
            this.Technician.Width = 157;
            // 
            // Task
            // 
            this.Task.Caption = "Task";
            this.Task.FieldName = "MainTaskName";
            this.Task.Name = "Task";
            this.Task.OptionsColumn.AllowEdit = false;
            this.Task.Visible = true;
            this.Task.VisibleIndex = 3;
            this.Task.Width = 119;
            // 
            // Status
            // 
            this.Status.Caption = "Status";
            this.Status.FieldName = "VisitStatusText";
            this.Status.Name = "Status";
            this.Status.OptionsColumn.AllowEdit = false;
            this.Status.Visible = true;
            this.Status.VisibleIndex = 1;
            this.Status.Width = 68;
            // 
            // ClosedAmount
            // 
            this.ClosedAmount.Caption = "Amount";
            this.ClosedAmount.DisplayFormat.FormatString = "C";
            this.ClosedAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ClosedAmount.FieldName = "ClosedAmount";
            this.ClosedAmount.Name = "ClosedAmount";
            this.ClosedAmount.OptionsColumn.AllowEdit = false;
            this.ClosedAmount.Visible = true;
            this.ClosedAmount.VisibleIndex = 5;
            this.ClosedAmount.Width = 88;
            // 
            // m_tabPageInsurance
            // 
            this.m_tabPageInsurance.Controls.Add(this.m_ctlProjectInsurance);
            this.m_tabPageInsurance.Name = "m_tabPageInsurance";
            this.m_tabPageInsurance.Size = new System.Drawing.Size(614, 235);
            this.m_tabPageInsurance.Text = "Insurance";
            // 
            // m_groupAdvertising
            // 
            this.m_groupAdvertising.Controls.Add(this.m_lblSalesRep);
            this.m_groupAdvertising.Controls.Add(this.labelControl10);
            this.m_groupAdvertising.Controls.Add(this.m_cmbQbSalesRep);
            this.m_groupAdvertising.Controls.Add(this.m_cmbQbCustomerTypeLevel1);
            this.m_groupAdvertising.Controls.Add(this.m_cmbQbCustomerTypeLevel0);
            this.m_groupAdvertising.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_groupAdvertising.Location = new System.Drawing.Point(0, 0);
            this.m_groupAdvertising.Name = "m_groupAdvertising";
            this.m_groupAdvertising.Size = new System.Drawing.Size(623, 81);
            this.m_groupAdvertising.TabIndex = 0;
            this.m_groupAdvertising.Text = "Advertising Sources, Sales Reps";
            // 
            // m_lblSalesRep
            // 
            this.m_lblSalesRep.Location = new System.Drawing.Point(8, 52);
            this.m_lblSalesRep.Name = "m_lblSalesRep";
            this.m_lblSalesRep.Size = new System.Drawing.Size(47, 13);
            this.m_lblSalesRep.TabIndex = 6;
            this.m_lblSalesRep.Text = "Sales Rep";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(8, 23);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(90, 13);
            this.labelControl10.TabIndex = 5;
            this.labelControl10.Text = "Advertising Source";
            // 
            // m_cmbQbSalesRep
            // 
            this.m_cmbQbSalesRep.Location = new System.Drawing.Point(109, 49);
            this.m_cmbQbSalesRep.Name = "m_cmbQbSalesRep";
            this.m_cmbQbSalesRep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbQbSalesRep.Size = new System.Drawing.Size(206, 20);
            this.m_cmbQbSalesRep.TabIndex = 4;
            // 
            // m_cmbQbCustomerTypeLevel1
            // 
            this.m_cmbQbCustomerTypeLevel1.Location = new System.Drawing.Point(321, 23);
            this.m_cmbQbCustomerTypeLevel1.Name = "m_cmbQbCustomerTypeLevel1";
            this.m_cmbQbCustomerTypeLevel1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbQbCustomerTypeLevel1.Size = new System.Drawing.Size(160, 20);
            this.m_cmbQbCustomerTypeLevel1.TabIndex = 3;
            // 
            // m_cmbQbCustomerTypeLevel0
            // 
            this.m_cmbQbCustomerTypeLevel0.Location = new System.Drawing.Point(109, 23);
            this.m_cmbQbCustomerTypeLevel0.Name = "m_cmbQbCustomerTypeLevel0";
            this.m_cmbQbCustomerTypeLevel0.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbQbCustomerTypeLevel0.Size = new System.Drawing.Size(206, 20);
            this.m_cmbQbCustomerTypeLevel0.TabIndex = 1;
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // m_ctlProjectInsurance
            // 
            this.m_ctlProjectInsurance.Location = new System.Drawing.Point(5, 3);
            this.m_ctlProjectInsurance.Name = "m_ctlProjectInsurance";
            projectInsurance1.Address1 = "";
            projectInsurance1.Address2 = "";
            projectInsurance1.ClaimNumber = "";
            projectInsurance1.Company = "";
            projectInsurance1.Contact = "";
            projectInsurance1.DeductibleAmount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            projectInsurance1.Fax = "";
            projectInsurance1.Phone = "";
            projectInsurance1.ProjectId = 0;
            this.m_ctlProjectInsurance.ProjectInsurance = projectInsurance1;
            this.m_ctlProjectInsurance.Size = new System.Drawing.Size(337, 216);
            this.m_ctlProjectInsurance.TabIndex = 0;
            // 
            // ProjectEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ProjectEdit";
            this.Size = new System.Drawing.Size(623, 347);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_tabVisitHistory)).EndInit();
            this.m_tabVisitHistory.ResumeLayout(false);
            this.m_tabPageViewHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.m_tabPageInsurance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_groupAdvertising)).EndInit();
            this.m_groupAdvertising.ResumeLayout(false);
            this.m_groupAdvertising.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbSalesRep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbCustomerTypeLevel1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbCustomerTypeLevel0.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl m_groupAdvertising;
        private DevExpress.XtraEditors.ImageComboBoxEdit m_cmbQbCustomerTypeLevel0;
        private DevExpress.XtraEditors.ImageComboBoxEdit m_cmbQbCustomerTypeLevel1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraTab.XtraTabPage m_tabPageViewHistory;
        internal DevExpress.XtraTab.XtraTabControl m_tabVisitHistory;
        internal DevExpress.XtraTab.XtraTabPage m_tabPageInsurance;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn Status;
        private DevExpress.XtraGrid.Columns.GridColumn Technician;
        private DevExpress.XtraGrid.Columns.GridColumn Task;
        private DevExpress.XtraGrid.Columns.GridColumn Date;
        private DevExpress.XtraGrid.Columns.GridColumn ClosedAmount;
        internal DevExpress.XtraGrid.GridControl m_gridVisits;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbQbSalesRep;
        internal DevExpress.XtraEditors.LabelControl m_lblSalesRep;
        private ProjectInsuranceEdit m_ctlProjectInsurance;
    }
}
