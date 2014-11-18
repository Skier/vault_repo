namespace Dalworth.Server.MainForm.ProjectCustomerHistory
{
    partial class ProjectCustomerHistoryView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_lblBControl = new DevExpress.XtraEditors.LabelControl();
            this.m_gridProjects = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewProjects = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_ctlCustomer = new Dalworth.Server.MainForm.Components.CustomerViewEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridProjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewProjects)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnNew);
            this.panelControl1.Controls.Add(this.m_btnEdit);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.m_ctlCustomer);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(929, 390);
            this.panelControl1.TabIndex = 0;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(832, 362);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(92, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_btnNew
            // 
            this.m_btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnNew.Location = new System.Drawing.Point(636, 362);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Size = new System.Drawing.Size(92, 23);
            this.m_btnNew.TabIndex = 2;
            this.m_btnNew.Text = "&New Project";
            // 
            // m_btnEdit
            // 
            this.m_btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnEdit.Location = new System.Drawing.Point(734, 362);
            this.m_btnEdit.Name = "m_btnEdit";
            this.m_btnEdit.Size = new System.Drawing.Size(92, 23);
            this.m_btnEdit.TabIndex = 3;
            this.m_btnEdit.Text = "&Edit Project";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_lblBControl);
            this.groupControl1.Controls.Add(this.m_gridProjects);
            this.groupControl1.Location = new System.Drawing.Point(5, 93);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(920, 263);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Recent Projects";
            // 
            // m_lblBControl
            // 
            this.m_lblBControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblBControl.Location = new System.Drawing.Point(98, 122);
            this.m_lblBControl.Name = "m_lblBControl";
            this.m_lblBControl.Size = new System.Drawing.Size(0, 0);
            this.m_lblBControl.TabIndex = 0;
            this.m_lblBControl.Text = "&B control";
            // 
            // m_gridProjects
            // 
            this.m_gridProjects.EmbeddedNavigator.Name = "";
            this.m_gridProjects.Location = new System.Drawing.Point(0, 23);
            this.m_gridProjects.MainView = this.m_gridViewProjects;
            this.m_gridProjects.Name = "m_gridProjects";
            this.m_gridProjects.Size = new System.Drawing.Size(920, 240);
            this.m_gridProjects.TabIndex = 1;
            this.m_gridProjects.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewProjects});
            // 
            // m_gridViewProjects
            // 
            this.m_gridViewProjects.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.m_gridViewProjects.GridControl = this.m_gridProjects;
            this.m_gridViewProjects.Name = "m_gridViewProjects";
            this.m_gridViewProjects.OptionsBehavior.Editable = false;
            this.m_gridViewProjects.OptionsBehavior.FocusLeaveOnTab = true;
            this.m_gridViewProjects.OptionsCustomization.AllowColumnMoving = false;
            this.m_gridViewProjects.OptionsCustomization.AllowFilter = false;
            this.m_gridViewProjects.OptionsCustomization.AllowGroup = false;
            this.m_gridViewProjects.OptionsCustomization.AllowSort = false;
            this.m_gridViewProjects.OptionsFilter.AllowFilterEditor = false;
            this.m_gridViewProjects.OptionsNavigation.UseTabKey = false;
            this.m_gridViewProjects.OptionsView.ShowDetailButtons = false;
            this.m_gridViewProjects.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Id";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 42;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Type";
            this.gridColumn2.FieldName = "ProjectTypeText";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 98;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Status";
            this.gridColumn3.FieldName = "ProjectStatusText";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 78;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Progress";
            this.gridColumn4.FieldName = "Progress";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 104;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Account Manager";
            this.gridColumn5.FieldName = "AccountManagerName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 172;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Project Manager";
            this.gridColumn6.FieldName = "ProjectManagerName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 182;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Closed Amount";
            this.gridColumn7.DisplayFormat.FormatString = "c";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "ClosedAmount";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 108;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Modified";
            this.gridColumn8.DisplayFormat.FormatString = "d";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn8.FieldName = "LastModifiedDate";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 115;
            // 
            // m_ctlCustomer
            // 
            this.m_ctlCustomer.Address = null;
            this.m_ctlCustomer.Customer = null;
            this.m_ctlCustomer.EmailVisible = false;
            this.m_ctlCustomer.Location = new System.Drawing.Point(5, 5);
            this.m_ctlCustomer.Name = "m_ctlCustomer";
            this.m_ctlCustomer.Size = new System.Drawing.Size(920, 82);
            this.m_ctlCustomer.TabIndex = 0;
            // 
            // ProjectCustomerHistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(929, 390);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectCustomerHistoryView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recent projects";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridProjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewProjects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewProjects;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        internal DevExpress.XtraEditors.SimpleButton m_btnNew;
        internal DevExpress.XtraEditors.SimpleButton m_btnEdit;
        internal DevExpress.XtraGrid.GridControl m_gridProjects;
        internal Dalworth.Server.MainForm.Components.CustomerViewEdit m_ctlCustomer;
        private DevExpress.XtraEditors.LabelControl m_lblBControl;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;

    }
}