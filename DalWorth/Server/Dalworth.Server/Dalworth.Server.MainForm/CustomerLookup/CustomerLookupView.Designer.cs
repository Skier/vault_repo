namespace Dalworth.Server.MainForm.CustomerLookup
{
    partial class CustomerLookupView
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
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_gridShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_gridCustomers = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewCustomers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_colFirstName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colLastName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colPhones = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.m_colBlock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colStreet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colZip = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbCustomerStatus = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_txtBusinessPhone = new DevExpress.XtraEditors.TextEdit();
            this.m_ctrlAddress = new Dalworth.Server.MainForm.Components.AddressEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtLastName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtHomePhone = new DevExpress.XtraEditors.TextEdit();
            this.m_txtFirstName = new DevExpress.XtraEditors.TextEdit();
            this.m_timerFilter = new System.Windows.Forms.Timer(this.components);
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCustomerStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtBusinessPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtLastName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtHomePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtFirstName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_gridShortcut);
            this.panelControl1.Controls.Add(this.m_gridCustomers);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(968, 670);
            this.panelControl1.TabIndex = 0;
            // 
            // m_gridShortcut
            // 
            this.m_gridShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_gridShortcut.Location = new System.Drawing.Point(12, 185);
            this.m_gridShortcut.Name = "m_gridShortcut";
            this.m_gridShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_gridShortcut.TabIndex = 1;
            this.m_gridShortcut.Text = "&B Shortcut";
            // 
            // m_gridCustomers
            // 
            this.m_gridCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridCustomers.EmbeddedNavigator.Name = "";
            this.m_gridCustomers.Location = new System.Drawing.Point(0, 108);
            this.m_gridCustomers.MainView = this.m_gridViewCustomers;
            this.m_gridCustomers.Name = "m_gridCustomers";
            this.m_gridCustomers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.m_gridCustomers.Size = new System.Drawing.Size(968, 531);
            this.m_gridCustomers.TabIndex = 2;
            this.m_gridCustomers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewCustomers});
            // 
            // m_gridViewCustomers
            // 
            this.m_gridViewCustomers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_colFirstName,
            this.m_colLastName,
            this.m_colPhones,
            this.m_colBlock,
            this.m_colStreet,
            this.m_colZip,
            this.gridColumn5});
            this.m_gridViewCustomers.GridControl = this.m_gridCustomers;
            this.m_gridViewCustomers.GroupFormat = "{0}[#image]{1} {2}";
            this.m_gridViewCustomers.Name = "m_gridViewCustomers";
            this.m_gridViewCustomers.OptionsBehavior.AutoExpandAllGroups = true;
            this.m_gridViewCustomers.OptionsCustomization.AllowFilter = false;
            this.m_gridViewCustomers.OptionsCustomization.AllowGroup = false;
            this.m_gridViewCustomers.OptionsCustomization.AllowSort = false;
            this.m_gridViewCustomers.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewCustomers.OptionsNavigation.UseTabKey = false;
            this.m_gridViewCustomers.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.m_gridViewCustomers.OptionsView.RowAutoHeight = true;
            this.m_gridViewCustomers.OptionsView.ShowGroupPanel = false;
            // 
            // m_colFirstName
            // 
            this.m_colFirstName.Caption = "First Name";
            this.m_colFirstName.FieldName = "FirstName";
            this.m_colFirstName.Name = "m_colFirstName";
            this.m_colFirstName.OptionsColumn.AllowEdit = false;
            this.m_colFirstName.Visible = true;
            this.m_colFirstName.VisibleIndex = 1;
            this.m_colFirstName.Width = 110;
            // 
            // m_colLastName
            // 
            this.m_colLastName.Caption = "Last Name";
            this.m_colLastName.FieldName = "LastName";
            this.m_colLastName.Name = "m_colLastName";
            this.m_colLastName.OptionsColumn.AllowEdit = false;
            this.m_colLastName.Visible = true;
            this.m_colLastName.VisibleIndex = 0;
            this.m_colLastName.Width = 162;
            // 
            // m_colPhones
            // 
            this.m_colPhones.Caption = "Phones";
            this.m_colPhones.ColumnEdit = this.repositoryItemMemoEdit1;
            this.m_colPhones.FieldName = "PhoneFormatted";
            this.m_colPhones.Name = "m_colPhones";
            this.m_colPhones.OptionsColumn.AllowEdit = false;
            this.m_colPhones.Visible = true;
            this.m_colPhones.VisibleIndex = 2;
            this.m_colPhones.Width = 122;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // m_colBlock
            // 
            this.m_colBlock.Caption = "Block";
            this.m_colBlock.FieldName = "Block";
            this.m_colBlock.Name = "m_colBlock";
            this.m_colBlock.OptionsColumn.AllowEdit = false;
            this.m_colBlock.Visible = true;
            this.m_colBlock.VisibleIndex = 4;
            this.m_colBlock.Width = 62;
            // 
            // m_colStreet
            // 
            this.m_colStreet.Caption = "Street";
            this.m_colStreet.FieldName = "Street";
            this.m_colStreet.Name = "m_colStreet";
            this.m_colStreet.OptionsColumn.AllowEdit = false;
            this.m_colStreet.Visible = true;
            this.m_colStreet.VisibleIndex = 5;
            this.m_colStreet.Width = 149;
            // 
            // m_colZip
            // 
            this.m_colZip.Caption = "Zip";
            this.m_colZip.FieldName = "Zip";
            this.m_colZip.Name = "m_colZip";
            this.m_colZip.OptionsColumn.AllowEdit = false;
            this.m_colZip.Visible = true;
            this.m_colZip.VisibleIndex = 3;
            this.m_colZip.Width = 59;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Address";
            this.gridColumn5.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn5.FieldName = "AddressText";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 283;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.m_btnDelete);
            this.panelControl3.Controls.Add(this.m_btnCreate);
            this.panelControl3.Controls.Add(this.m_btnCancel);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 639);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(968, 31);
            this.panelControl3.TabIndex = 15;
            // 
            // m_btnDelete
            // 
            this.m_btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDelete.Location = new System.Drawing.Point(547, 5);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Size = new System.Drawing.Size(75, 23);
            this.m_btnDelete.TabIndex = 2;
            this.m_btnDelete.Text = "Delete";
            this.m_btnDelete.Visible = false;
            // 
            // m_btnCreate
            // 
            this.m_btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCreate.Location = new System.Drawing.Point(809, 5);
            this.m_btnCreate.Name = "m_btnCreate";
            this.m_btnCreate.Size = new System.Drawing.Size(75, 23);
            this.m_btnCreate.TabIndex = 0;
            this.m_btnCreate.Text = "&Create";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(890, 5);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.Text = "C&ancel";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.m_cmbCustomerStatus);
            this.panelControl2.Controls.Add(this.m_txtBusinessPhone);
            this.panelControl2.Controls.Add(this.m_ctrlAddress);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.m_txtLastName);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.m_txtHomePhone);
            this.panelControl2.Controls.Add(this.m_txtFirstName);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(968, 108);
            this.panelControl2.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 58);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "&Type";
            // 
            // m_cmbCustomerStatus
            // 
            this.m_cmbCustomerStatus.EditValue = 2;
            this.m_cmbCustomerStatus.Location = new System.Drawing.Point(98, 55);
            this.m_cmbCustomerStatus.Name = "m_cmbCustomerStatus";
            this.m_cmbCustomerStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbCustomerStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Business", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Residential", 2, -1)});
            this.m_cmbCustomerStatus.Size = new System.Drawing.Size(147, 20);
            this.m_cmbCustomerStatus.TabIndex = 7;
            // 
            // m_txtBusinessPhone
            // 
            this.m_txtBusinessPhone.Location = new System.Drawing.Point(251, 29);
            this.m_txtBusinessPhone.Name = "m_txtBusinessPhone";
            this.m_txtBusinessPhone.Properties.Mask.EditMask = "\\(\\d\\d\\d\\) \\d\\d\\d-\\d\\d\\d\\d";
            this.m_txtBusinessPhone.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtBusinessPhone.Properties.Mask.SaveLiteral = false;
            this.m_txtBusinessPhone.Size = new System.Drawing.Size(126, 20);
            this.m_txtBusinessPhone.TabIndex = 5;
            // 
            // m_ctrlAddress
            // 
            this.m_ctrlAddress.Address = null;
            this.m_ctrlAddress.IsValidationDisabled = false;
            this.m_ctrlAddress.Location = new System.Drawing.Point(415, 3);
            this.m_ctrlAddress.MinimumSize = new System.Drawing.Size(432, 90);
            this.m_ctrlAddress.Name = "m_ctrlAddress";
            this.m_ctrlAddress.Size = new System.Drawing.Size(502, 104);
            this.m_ctrlAddress.TabIndex = 8;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(4, 32);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(84, 13);
            this.labelControl6.TabIndex = 3;
            this.labelControl6.Text = "&Home, Bus Phone";
            // 
            // m_txtLastName
            // 
            this.m_txtLastName.Location = new System.Drawing.Point(98, 3);
            this.m_txtLastName.Name = "m_txtLastName";
            this.m_txtLastName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtLastName.Properties.MaxLength = 40;
            this.m_txtLastName.Size = new System.Drawing.Size(147, 20);
            this.m_txtLastName.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(4, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(78, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "&Last, First Name";
            // 
            // m_txtHomePhone
            // 
            this.m_txtHomePhone.Location = new System.Drawing.Point(98, 29);
            this.m_txtHomePhone.Name = "m_txtHomePhone";
            this.m_txtHomePhone.Properties.Mask.EditMask = "\\(\\d\\d\\d\\) \\d\\d\\d-\\d\\d\\d\\d";
            this.m_txtHomePhone.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtHomePhone.Properties.Mask.SaveLiteral = false;
            this.m_txtHomePhone.Size = new System.Drawing.Size(147, 20);
            this.m_txtHomePhone.TabIndex = 4;
            // 
            // m_txtFirstName
            // 
            this.m_txtFirstName.Location = new System.Drawing.Point(251, 3);
            this.m_txtFirstName.Name = "m_txtFirstName";
            this.m_txtFirstName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtFirstName.Properties.MaxLength = 40;
            this.m_txtFirstName.Size = new System.Drawing.Size(126, 20);
            this.m_txtFirstName.TabIndex = 2;
            // 
            // m_timerFilter
            // 
            this.m_timerFilter.Interval = 500;
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // CustomerLookupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(968, 670);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerLookupView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CustomerLookupView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCustomerStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtBusinessPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtLastName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtHomePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtFirstName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnDelete;
        internal DevExpress.XtraEditors.SimpleButton m_btnCreate;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        internal DevExpress.XtraGrid.GridControl m_gridCustomers;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewCustomers;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        internal DevExpress.XtraEditors.TextEdit m_txtHomePhone;
        internal DevExpress.XtraEditors.TextEdit m_txtFirstName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        internal DevExpress.XtraEditors.TextEdit m_txtLastName;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colFirstName;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colLastName;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colBlock;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colStreet;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colZip;
        internal System.Windows.Forms.Timer m_timerFilter;
        internal Dalworth.Server.MainForm.Components.AddressEdit m_ctrlAddress;
        internal DevExpress.XtraEditors.TextEdit m_txtBusinessPhone;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbCustomerStatus;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colPhones;
        private DevExpress.XtraEditors.LabelControl m_gridShortcut;
    }
}