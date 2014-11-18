
namespace SmartSchedule.Win32.UserManage
{
    partial class UserManageView
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
            this.m_btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.m_gridUsers = new DevExpress.XtraGrid.GridControl();
            this.m_gridUsersView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridUsersView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.CausesValidation = false;
            this.panelControl1.Controls.Add(this.m_btnEdit);
            this.panelControl1.Controls.Add(this.m_btnAdd);
            this.panelControl1.Controls.Add(this.m_gridUsers);
            this.panelControl1.Controls.Add(this.m_btnClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(506, 357);
            this.panelControl1.TabIndex = 0;
            // 
            // m_btnEdit
            // 
            this.m_btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnEdit.Location = new System.Drawing.Point(112, 331);
            this.m_btnEdit.Name = "m_btnEdit";
            this.m_btnEdit.Size = new System.Drawing.Size(75, 23);
            this.m_btnEdit.TabIndex = 2;
            this.m_btnEdit.Text = "Edit";
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAdd.Location = new System.Drawing.Point(31, 331);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(75, 23);
            this.m_btnAdd.TabIndex = 1;
            this.m_btnAdd.Text = "Add";
            // 
            // m_gridUsers
            // 
            this.m_gridUsers.Location = new System.Drawing.Point(3, 3);
            this.m_gridUsers.MainView = this.m_gridUsersView;
            this.m_gridUsers.Name = "m_gridUsers";
            this.m_gridUsers.Size = new System.Drawing.Size(500, 325);
            this.m_gridUsers.TabIndex = 0;
            this.m_gridUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridUsersView});
            // 
            // m_gridUsersView
            // 
            this.m_gridUsersView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.m_gridUsersView.GridControl = this.m_gridUsers;
            this.m_gridUsersView.Name = "m_gridUsersView";
            this.m_gridUsersView.OptionsBehavior.Editable = false;
            this.m_gridUsersView.OptionsCustomization.AllowFilter = false;
            this.m_gridUsersView.OptionsCustomization.AllowGroup = false;
            this.m_gridUsersView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridUsersView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridUsersView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridUsersView.OptionsSelection.MultiSelect = true;
            this.m_gridUsersView.OptionsView.ShowDetailButtons = false;
            this.m_gridUsersView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Name";
            this.gridColumn1.FieldName = "Login";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 269;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Role";
            this.gridColumn2.FieldName = "UserRoleText";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 163;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Active";
            this.gridColumn3.FieldName = "ActiveText";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 47;
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(428, 331);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 3;
            this.m_btnClose.Text = "Close";
            // 
            // UserManageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(506, 357);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserManageView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Management";
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridUsersView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnClose;
        internal DevExpress.XtraGrid.GridControl m_gridUsers;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridUsersView;
        internal DevExpress.XtraEditors.SimpleButton m_btnEdit;
        internal DevExpress.XtraEditors.SimpleButton m_btnAdd;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;

    }
}