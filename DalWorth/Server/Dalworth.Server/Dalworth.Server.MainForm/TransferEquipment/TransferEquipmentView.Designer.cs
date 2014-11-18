namespace Dalworth.Server.MainForm.TransferEquipment
{
    partial class TransferEquipmentView
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.m_pnlCustomer = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtCustomerId = new DevExpress.XtraEditors.TextEdit();
            this.m_btnCustomerLookup = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbCustomerAddress = new DevExpress.XtraEditors.LookUpEdit();
            this.m_lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.m_lblAddress = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.m_cmbLocationType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_pnlVan = new DevExpress.XtraEditors.PanelControl();
            this.m_lblVan = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbVan = new DevExpress.XtraEditors.LookUpEdit();
            this.m_pnlInventoryRoom = new DevExpress.XtraEditors.PanelControl();
            this.m_cmbInventoryRoom = new DevExpress.XtraEditors.LookUpEdit();
            this.m_lblInventoryRoom = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_gridEquipment = new DevExpress.XtraGrid.GridControl();
            this.m_gridEquipmentView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_colEquipmentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colSerialNumber1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_lblShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlCustomer)).BeginInit();
            this.m_pnlCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCustomerId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCustomerAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbLocationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlVan)).BeginInit();
            this.m_pnlVan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbVan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlInventoryRoom)).BeginInit();
            this.m_pnlInventoryRoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbInventoryRoom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(681, 311);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.m_pnlCustomer);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.m_txtNotes);
            this.groupControl2.Controls.Add(this.m_cmbLocationType);
            this.groupControl2.Controls.Add(this.m_pnlVan);
            this.groupControl2.Controls.Add(this.m_pnlInventoryRoom);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Location = new System.Drawing.Point(349, 5);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(326, 271);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Destination";
            // 
            // m_pnlCustomer
            // 
            this.m_pnlCustomer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlCustomer.Controls.Add(this.labelControl2);
            this.m_pnlCustomer.Controls.Add(this.m_txtCustomerId);
            this.m_pnlCustomer.Controls.Add(this.m_btnCustomerLookup);
            this.m_pnlCustomer.Controls.Add(this.m_lblCustomerName);
            this.m_pnlCustomer.Controls.Add(this.m_cmbCustomerAddress);
            this.m_pnlCustomer.Controls.Add(this.m_lblCustomer);
            this.m_pnlCustomer.Controls.Add(this.m_lblAddress);
            this.m_pnlCustomer.Location = new System.Drawing.Point(5, 49);
            this.m_pnlCustomer.Name = "m_pnlCustomer";
            this.m_pnlCustomer.Size = new System.Drawing.Size(319, 75);
            this.m_pnlCustomer.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(2, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Customer &ID";
            // 
            // m_txtCustomerId
            // 
            this.m_txtCustomerId.Location = new System.Drawing.Point(100, 1);
            this.m_txtCustomerId.Name = "m_txtCustomerId";
            this.m_txtCustomerId.Properties.Mask.EditMask = "\\d{0,20}";
            this.m_txtCustomerId.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtCustomerId.Size = new System.Drawing.Size(215, 20);
            this.m_txtCustomerId.TabIndex = 5;
            // 
            // m_btnCustomerLookup
            // 
            this.m_btnCustomerLookup.Location = new System.Drawing.Point(288, 27);
            this.m_btnCustomerLookup.Name = "m_btnCustomerLookup";
            this.m_btnCustomerLookup.Size = new System.Drawing.Size(28, 18);
            this.m_btnCustomerLookup.TabIndex = 7;
            this.m_btnCustomerLookup.Text = "&...";
            // 
            // m_lblCustomerName
            // 
            this.m_lblCustomerName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblCustomerName.Appearance.Options.UseFont = true;
            this.m_lblCustomerName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblCustomerName.Location = new System.Drawing.Point(100, 28);
            this.m_lblCustomerName.Name = "m_lblCustomerName";
            this.m_lblCustomerName.Size = new System.Drawing.Size(178, 13);
            this.m_lblCustomerName.TabIndex = 13;
            this.m_lblCustomerName.Text = "[No Customer]";
            // 
            // m_cmbCustomerAddress
            // 
            this.m_cmbCustomerAddress.Location = new System.Drawing.Point(100, 51);
            this.m_cmbCustomerAddress.Name = "m_cmbCustomerAddress";
            this.m_cmbCustomerAddress.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbCustomerAddress.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AddressSingleLine", "AddressSingleLine", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.m_cmbCustomerAddress.Properties.DisplayMember = "AddressSingleLine";
            this.m_cmbCustomerAddress.Properties.NullText = "";
            this.m_cmbCustomerAddress.Properties.ShowFooter = false;
            this.m_cmbCustomerAddress.Properties.ShowHeader = false;
            this.m_cmbCustomerAddress.Properties.ValueMember = "ID";
            this.m_cmbCustomerAddress.Size = new System.Drawing.Size(215, 20);
            this.m_cmbCustomerAddress.TabIndex = 9;
            // 
            // m_lblCustomer
            // 
            this.m_lblCustomer.Location = new System.Drawing.Point(2, 28);
            this.m_lblCustomer.Name = "m_lblCustomer";
            this.m_lblCustomer.Size = new System.Drawing.Size(46, 13);
            this.m_lblCustomer.TabIndex = 6;
            this.m_lblCustomer.Text = "&Customer";
            // 
            // m_lblAddress
            // 
            this.m_lblAddress.Location = new System.Drawing.Point(2, 54);
            this.m_lblAddress.Name = "m_lblAddress";
            this.m_lblAddress.Size = new System.Drawing.Size(39, 13);
            this.m_lblAddress.TabIndex = 8;
            this.m_lblAddress.Text = "&Address";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 130);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "&Notes";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Location = new System.Drawing.Point(105, 128);
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Properties.MaxLength = 200;
            this.m_txtNotes.Size = new System.Drawing.Size(215, 135);
            this.m_txtNotes.TabIndex = 15;
            // 
            // m_cmbLocationType
            // 
            this.m_cmbLocationType.EditValue = 1;
            this.m_cmbLocationType.Location = new System.Drawing.Point(105, 23);
            this.m_cmbLocationType.Name = "m_cmbLocationType";
            this.m_cmbLocationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbLocationType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Inventory Room", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Van", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Customer", 3, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Lost", 4, -1)});
            this.m_cmbLocationType.Size = new System.Drawing.Size(215, 20);
            this.m_cmbLocationType.TabIndex = 2;
            // 
            // m_pnlVan
            // 
            this.m_pnlVan.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlVan.Controls.Add(this.m_lblVan);
            this.m_pnlVan.Controls.Add(this.m_cmbVan);
            this.m_pnlVan.Location = new System.Drawing.Point(5, 49);
            this.m_pnlVan.Name = "m_pnlVan";
            this.m_pnlVan.Size = new System.Drawing.Size(315, 49);
            this.m_pnlVan.TabIndex = 8;
            // 
            // m_lblVan
            // 
            this.m_lblVan.Location = new System.Drawing.Point(3, 3);
            this.m_lblVan.Name = "m_lblVan";
            this.m_lblVan.Size = new System.Drawing.Size(18, 13);
            this.m_lblVan.TabIndex = 9;
            this.m_lblVan.Text = "&Van";
            // 
            // m_cmbVan
            // 
            this.m_cmbVan.Location = new System.Drawing.Point(100, 0);
            this.m_cmbVan.Name = "m_cmbVan";
            this.m_cmbVan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbVan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LicensePlateNumber", "LicensePlateNumber", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.m_cmbVan.Properties.DisplayMember = "LicensePlateNumber";
            this.m_cmbVan.Properties.NullText = "";
            this.m_cmbVan.Properties.ShowFooter = false;
            this.m_cmbVan.Properties.ShowHeader = false;
            this.m_cmbVan.Properties.ValueMember = "ID";
            this.m_cmbVan.Size = new System.Drawing.Size(215, 20);
            this.m_cmbVan.TabIndex = 10;
            // 
            // m_pnlInventoryRoom
            // 
            this.m_pnlInventoryRoom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlInventoryRoom.Controls.Add(this.m_cmbInventoryRoom);
            this.m_pnlInventoryRoom.Controls.Add(this.m_lblInventoryRoom);
            this.m_pnlInventoryRoom.Location = new System.Drawing.Point(5, 49);
            this.m_pnlInventoryRoom.Name = "m_pnlInventoryRoom";
            this.m_pnlInventoryRoom.Size = new System.Drawing.Size(315, 49);
            this.m_pnlInventoryRoom.TabIndex = 11;
            // 
            // m_cmbInventoryRoom
            // 
            this.m_cmbInventoryRoom.Location = new System.Drawing.Point(100, 0);
            this.m_cmbInventoryRoom.Name = "m_cmbInventoryRoom";
            this.m_cmbInventoryRoom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbInventoryRoom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.m_cmbInventoryRoom.Properties.DisplayMember = "Name";
            this.m_cmbInventoryRoom.Properties.NullText = "";
            this.m_cmbInventoryRoom.Properties.ShowFooter = false;
            this.m_cmbInventoryRoom.Properties.ShowHeader = false;
            this.m_cmbInventoryRoom.Properties.ValueMember = "ID";
            this.m_cmbInventoryRoom.Size = new System.Drawing.Size(215, 20);
            this.m_cmbInventoryRoom.TabIndex = 13;
            // 
            // m_lblInventoryRoom
            // 
            this.m_lblInventoryRoom.Location = new System.Drawing.Point(3, 3);
            this.m_lblInventoryRoom.Name = "m_lblInventoryRoom";
            this.m_lblInventoryRoom.Size = new System.Drawing.Size(78, 13);
            this.m_lblInventoryRoom.TabIndex = 12;
            this.m_lblInventoryRoom.Text = "&Inventory Room";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 26);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(81, 13);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "&Destination Type";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_gridEquipment);
            this.groupControl1.Controls.Add(this.m_lblShortcut);
            this.groupControl1.Location = new System.Drawing.Point(5, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(338, 271);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Equipment";
            // 
            // m_gridEquipment
            // 
            this.m_gridEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridEquipment.EmbeddedNavigator.Name = "";
            this.m_gridEquipment.Location = new System.Drawing.Point(2, 20);
            this.m_gridEquipment.MainView = this.m_gridEquipmentView;
            this.m_gridEquipment.Name = "m_gridEquipment";
            this.m_gridEquipment.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit2});
            this.m_gridEquipment.Size = new System.Drawing.Size(334, 249);
            this.m_gridEquipment.TabIndex = 0;
            this.m_gridEquipment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridEquipmentView});
            // 
            // m_gridEquipmentView
            // 
            this.m_gridEquipmentView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_colEquipmentType,
            this.m_colSerialNumber1,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.m_gridEquipmentView.GridControl = this.m_gridEquipment;
            this.m_gridEquipmentView.GroupCount = 1;
            this.m_gridEquipmentView.GroupFormat = "[#image]{1} {2}";
            this.m_gridEquipmentView.Name = "m_gridEquipmentView";
            this.m_gridEquipmentView.OptionsBehavior.AutoExpandAllGroups = true;
            this.m_gridEquipmentView.OptionsCustomization.AllowFilter = false;
            this.m_gridEquipmentView.OptionsCustomization.AllowGroup = false;
            this.m_gridEquipmentView.OptionsCustomization.AllowSort = false;
            this.m_gridEquipmentView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridEquipmentView.OptionsNavigation.EnterMoveNextColumn = true;
            this.m_gridEquipmentView.OptionsNavigation.UseTabKey = false;
            this.m_gridEquipmentView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.m_gridEquipmentView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.m_gridEquipmentView.OptionsView.ShowGroupPanel = false;
            this.m_gridEquipmentView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.m_colEquipmentType, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // m_colEquipmentType
            // 
            this.m_colEquipmentType.FieldName = "EquipmentTypeName";
            this.m_colEquipmentType.Name = "m_colEquipmentType";
            this.m_colEquipmentType.OptionsColumn.AllowEdit = false;
            this.m_colEquipmentType.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.m_colEquipmentType.Visible = true;
            this.m_colEquipmentType.VisibleIndex = 0;
            // 
            // m_colSerialNumber1
            // 
            this.m_colSerialNumber1.ColumnEdit = this.repositoryItemTextEdit2;
            this.m_colSerialNumber1.FieldName = "SerialNumber1";
            this.m_colSerialNumber1.Name = "m_colSerialNumber1";
            this.m_colSerialNumber1.Visible = true;
            this.m_colSerialNumber1.VisibleIndex = 0;
            this.m_colSerialNumber1.Width = 73;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Mask.EditMask = "\\d{1,4}";
            this.repositoryItemTextEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // gridColumn8
            // 
            this.gridColumn8.ColumnEdit = this.repositoryItemTextEdit2;
            this.gridColumn8.FieldName = "SerialNumber2";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 53;
            // 
            // gridColumn9
            // 
            this.gridColumn9.ColumnEdit = this.repositoryItemTextEdit2;
            this.gridColumn9.FieldName = "SerialNumber3";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            this.gridColumn9.Width = 53;
            // 
            // gridColumn10
            // 
            this.gridColumn10.ColumnEdit = this.repositoryItemTextEdit2;
            this.gridColumn10.FieldName = "SerialNumber4";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            this.gridColumn10.Width = 58;
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut.Location = new System.Drawing.Point(32, 85);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 0;
            this.m_lblShortcut.Text = "&B shortcut";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(600, 282);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 17;
            this.m_btnCancel.Text = "Canc&el";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(519, 282);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 16;
            this.m_btnOk.Text = "&OK";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // TransferEquipmentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(681, 311);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransferEquipmentView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CustomerEditView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlCustomer)).EndInit();
            this.m_pnlCustomer.ResumeLayout(false);
            this.m_pnlCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCustomerId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbCustomerAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbLocationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlVan)).EndInit();
            this.m_pnlVan.ResumeLayout(false);
            this.m_pnlVan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbVan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlInventoryRoom)).EndInit();
            this.m_pnlInventoryRoom.ResumeLayout(false);
            this.m_pnlInventoryRoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbInventoryRoom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbLocationType;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        internal DevExpress.XtraEditors.MemoEdit m_txtNotes;
        private DevExpress.XtraEditors.LabelControl m_lblAddress;
        private DevExpress.XtraEditors.LabelControl m_lblCustomer;
        private DevExpress.XtraEditors.LabelControl m_lblVan;
        private DevExpress.XtraEditors.LabelControl m_lblInventoryRoom;
        internal DevExpress.XtraEditors.LookUpEdit m_cmbCustomerAddress;
        internal DevExpress.XtraEditors.LookUpEdit m_cmbVan;
        internal DevExpress.XtraEditors.LookUpEdit m_cmbInventoryRoom;
        internal DevExpress.XtraEditors.PanelControl m_pnlCustomer;
        internal DevExpress.XtraEditors.PanelControl m_pnlVan;
        internal DevExpress.XtraEditors.PanelControl m_pnlInventoryRoom;
        private DevExpress.XtraEditors.LabelControl m_lblShortcut;
        internal DevExpress.XtraGrid.GridControl m_gridEquipment;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridEquipmentView;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colEquipmentType;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        internal DevExpress.XtraEditors.SimpleButton m_btnCustomerLookup;
        internal DevExpress.XtraEditors.LabelControl m_lblCustomerName;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colSerialNumber1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.TextEdit m_txtCustomerId;
    }
}