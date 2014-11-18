namespace Dalworth.Server.MainForm.AddressEdit
{
    partial class AddressSelectView
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
            this.m_lblGridCaption = new System.Windows.Forms.Label();
            this.m_gridAddresses = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewAddresses = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_colAddressSelector = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_chkAddress = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.m_colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colMapsco = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_chkUseBaseAddress = new DevExpress.XtraEditors.CheckEdit();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblShortcut = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridAddresses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewAddresses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkUseBaseAddress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblShortcut);
            this.panelControl1.Controls.Add(this.m_btnEdit);
            this.panelControl1.Controls.Add(this.m_lblGridCaption);
            this.panelControl1.Controls.Add(this.m_gridAddresses);
            this.panelControl1.Controls.Add(this.m_chkUseBaseAddress);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnAdd);
            this.panelControl1.Controls.Add(this.m_btnDelete);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(653, 380);
            this.panelControl1.TabIndex = 0;
            // 
            // m_btnEdit
            // 
            this.m_btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnEdit.Location = new System.Drawing.Point(265, 349);
            this.m_btnEdit.Name = "m_btnEdit";
            this.m_btnEdit.Size = new System.Drawing.Size(75, 23);
            this.m_btnEdit.TabIndex = 4;
            this.m_btnEdit.Text = "&Edit";
            // 
            // m_lblGridCaption
            // 
            this.m_lblGridCaption.AutoSize = true;
            this.m_lblGridCaption.Location = new System.Drawing.Point(5, 29);
            this.m_lblGridCaption.Name = "m_lblGridCaption";
            this.m_lblGridCaption.Size = new System.Drawing.Size(104, 13);
            this.m_lblGridCaption.TabIndex = 13;
            this.m_lblGridCaption.Text = "Additional addresses";
            // 
            // m_gridAddresses
            // 
            this.m_gridAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridAddresses.EmbeddedNavigator.Name = "";
            this.m_gridAddresses.Location = new System.Drawing.Point(6, 48);
            this.m_gridAddresses.MainView = this.m_gridViewAddresses;
            this.m_gridAddresses.Name = "m_gridAddresses";
            this.m_gridAddresses.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.m_chkAddress});
            this.m_gridAddresses.ShowOnlyPredefinedDetails = true;
            this.m_gridAddresses.Size = new System.Drawing.Size(641, 296);
            this.m_gridAddresses.TabIndex = 2;
            this.m_gridAddresses.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewAddresses});
            // 
            // m_gridViewAddresses
            // 
            this.m_gridViewAddresses.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_colAddressSelector,
            this.m_colAddress,
            this.m_colMapsco});
            this.m_gridViewAddresses.GridControl = this.m_gridAddresses;
            this.m_gridViewAddresses.Name = "m_gridViewAddresses";
            this.m_gridViewAddresses.OptionsCustomization.AllowFilter = false;
            this.m_gridViewAddresses.OptionsCustomization.AllowGroup = false;
            this.m_gridViewAddresses.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewAddresses.OptionsNavigation.UseTabKey = false;
            this.m_gridViewAddresses.OptionsView.ShowGroupPanel = false;
            // 
            // m_colAddressSelector
            // 
            this.m_colAddressSelector.ColumnEdit = this.m_chkAddress;
            this.m_colAddressSelector.FieldName = "IsSelected";
            this.m_colAddressSelector.Name = "m_colAddressSelector";
            this.m_colAddressSelector.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.m_colAddressSelector.OptionsFilter.AllowAutoFilter = false;
            this.m_colAddressSelector.OptionsFilter.AllowFilter = false;
            this.m_colAddressSelector.Visible = true;
            this.m_colAddressSelector.VisibleIndex = 0;
            this.m_colAddressSelector.Width = 21;
            // 
            // m_chkAddress
            // 
            this.m_chkAddress.AutoHeight = false;
            this.m_chkAddress.Name = "m_chkAddress";
            // 
            // m_colAddress
            // 
            this.m_colAddress.Caption = "Address";
            this.m_colAddress.FieldName = "AddressSingleLine";
            this.m_colAddress.Name = "m_colAddress";
            this.m_colAddress.OptionsColumn.AllowEdit = false;
            this.m_colAddress.Visible = true;
            this.m_colAddress.VisibleIndex = 1;
            this.m_colAddress.Width = 425;
            // 
            // m_colMapsco
            // 
            this.m_colMapsco.Caption = "Mapsco";
            this.m_colMapsco.FieldName = "Map";
            this.m_colMapsco.Name = "m_colMapsco";
            this.m_colMapsco.OptionsColumn.AllowEdit = false;
            this.m_colMapsco.Visible = true;
            this.m_colMapsco.VisibleIndex = 2;
            this.m_colMapsco.Width = 174;
            // 
            // m_chkUseBaseAddress
            // 
            this.m_chkUseBaseAddress.Location = new System.Drawing.Point(6, 4);
            this.m_chkUseBaseAddress.Name = "m_chkUseBaseAddress";
            this.m_chkUseBaseAddress.Properties.Caption = "&Use";
            this.m_chkUseBaseAddress.Size = new System.Drawing.Size(155, 19);
            this.m_chkUseBaseAddress.TabIndex = 0;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(572, 350);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 7;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAdd.Location = new System.Drawing.Point(184, 349);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(75, 23);
            this.m_btnAdd.TabIndex = 3;
            this.m_btnAdd.Text = "&Add";
            // 
            // m_btnDelete
            // 
            this.m_btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDelete.Location = new System.Drawing.Point(346, 350);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Size = new System.Drawing.Size(75, 23);
            this.m_btnDelete.TabIndex = 5;
            this.m_btnDelete.Text = "&Delete";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(491, 350);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 6;
            this.m_btnOk.Text = "&OK";
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.Location = new System.Drawing.Point(12, 121);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 1;
            this.m_lblShortcut.Text = "&B Shortcut";
            // 
            // AddressSelectView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(653, 380);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddressSelectView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CustomerEditView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridAddresses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewAddresses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkUseBaseAddress.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        internal DevExpress.XtraEditors.CheckEdit m_chkUseBaseAddress;
        internal DevExpress.XtraGrid.GridControl m_gridAddresses;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewAddresses;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colAddress;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colMapsco;
        internal DevExpress.XtraEditors.SimpleButton m_btnAdd;
        internal DevExpress.XtraEditors.SimpleButton m_btnDelete;
        private System.Windows.Forms.Label m_lblGridCaption;
        internal DevExpress.XtraEditors.SimpleButton m_btnEdit;
        internal DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit m_chkAddress;
        private DevExpress.XtraGrid.Columns.GridColumn m_colAddressSelector;
        private System.Windows.Forms.Label m_lblShortcut;
    }
}