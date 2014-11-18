namespace Dalworth.Server.MainForm.CustomerLookup
{
    partial class InsuranceCompanyLookupView
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
            this.m_gridCompanies = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewCompanies = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_lnkCompany = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCompanies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewCompanies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_lnkCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_gridCompanies);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(686, 508);
            this.panelControl1.TabIndex = 0;
            // 
            // m_gridCompanies
            // 
            this.m_gridCompanies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridCompanies.EmbeddedNavigator.Name = "";
            this.m_gridCompanies.Location = new System.Drawing.Point(0, 0);
            this.m_gridCompanies.MainView = this.m_gridViewCompanies;
            this.m_gridCompanies.Name = "m_gridCompanies";
            this.m_gridCompanies.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.m_lnkCompany});
            this.m_gridCompanies.Size = new System.Drawing.Size(686, 477);
            this.m_gridCompanies.TabIndex = 16;
            this.m_gridCompanies.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewCompanies});
            // 
            // m_gridViewCompanies
            // 
            this.m_gridViewCompanies.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn1,
            this.gridColumn5});
            this.m_gridViewCompanies.GridControl = this.m_gridCompanies;
            this.m_gridViewCompanies.GroupFormat = "{0}[#image]{1} {2}";
            this.m_gridViewCompanies.Name = "m_gridViewCompanies";
            this.m_gridViewCompanies.OptionsBehavior.AutoExpandAllGroups = true;
            this.m_gridViewCompanies.OptionsCustomization.AllowFilter = false;
            this.m_gridViewCompanies.OptionsCustomization.AllowGroup = false;
            this.m_gridViewCompanies.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewCompanies.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.m_gridViewCompanies.OptionsView.RowAutoHeight = true;
            this.m_gridViewCompanies.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Name";
            this.gridColumn4.ColumnEdit = this.m_lnkCompany;
            this.gridColumn4.FieldName = "Name";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 123;
            // 
            // m_lnkCompany
            // 
            this.m_lnkCompany.AutoHeight = false;
            this.m_lnkCompany.Name = "m_lnkCompany";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mapsco";
            this.gridColumn1.FieldName = "Mapsco";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 105;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Address";
            this.gridColumn5.FieldName = "AddressText";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 323;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.m_btnDelete);
            this.panelControl3.Controls.Add(this.m_btnAdd);
            this.panelControl3.Controls.Add(this.m_btnCancel);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 477);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(686, 31);
            this.panelControl3.TabIndex = 15;
            // 
            // m_btnDelete
            // 
            this.m_btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDelete.Location = new System.Drawing.Point(527, 5);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Size = new System.Drawing.Size(75, 23);
            this.m_btnDelete.TabIndex = 13;
            this.m_btnDelete.Text = "Delete";
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAdd.Location = new System.Drawing.Point(446, 5);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(75, 23);
            this.m_btnAdd.TabIndex = 12;
            this.m_btnAdd.Text = "Add";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(608, 5);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 11;
            this.m_btnCancel.Text = "Cancel";
            // 
            // InsuranceCompanyLookupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(686, 508);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsuranceCompanyLookupView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CustomerLookupView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCompanies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewCompanies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_lnkCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnDelete;
        internal DevExpress.XtraEditors.SimpleButton m_btnAdd;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        internal DevExpress.XtraGrid.GridControl m_gridCompanies;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewCompanies;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_lnkCompany;
    }
}