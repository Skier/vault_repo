namespace Dalworth.Server.MainForm.Accounting
{
    partial class InvoiceRequest
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnSkip = new DevExpress.XtraEditors.SimpleButton();
            this.m_ctrlShippingAddress = new Dalworth.Server.MainForm.Accounting.QbAddress();
            this.m_ctrlBillingAddress = new Dalworth.Server.MainForm.Accounting.QbAddress();
            this.m_txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtTax = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.m_grpExistingCustomers = new DevExpress.XtraEditors.GroupControl();
            this.m_gridExistingCustomers = new DevExpress.XtraGrid.GridControl();
            this.m_gridExistingCustomersView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_btnRestoreNewCustomer = new DevExpress.XtraEditors.SimpleButton();
            this.m_dtInvoiceDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtInvoiceNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbSalesReps = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_txtProjectName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.m_btnDontSync = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtTax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_grpExistingCustomers)).BeginInit();
            this.m_grpExistingCustomers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridExistingCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridExistingCustomersView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtInvoiceDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtInvoiceDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtInvoiceNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbSalesReps.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtProjectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_btnDontSync);
            this.panelControl1.Controls.Add(this.m_btnSkip);
            this.panelControl1.Controls.Add(this.m_ctrlShippingAddress);
            this.panelControl1.Controls.Add(this.m_ctrlBillingAddress);
            this.panelControl1.Controls.Add(this.m_txtTotal);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.m_txtTax);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.m_btnSave);
            this.panelControl1.Controls.Add(this.m_grpExistingCustomers);
            this.panelControl1.Controls.Add(this.m_dtInvoiceDate);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.m_txtInvoiceNumber);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.m_cmbSalesReps);
            this.panelControl1.Controls.Add(this.m_txtProjectName);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1015, 598);
            this.panelControl1.TabIndex = 0;
            // 
            // m_btnSkip
            // 
            this.m_btnSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnSkip.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnSkip.Appearance.Options.UseFont = true;
            this.m_btnSkip.Location = new System.Drawing.Point(0, 560);
            this.m_btnSkip.Name = "m_btnSkip";
            this.m_btnSkip.Size = new System.Drawing.Size(178, 38);
            this.m_btnSkip.TabIndex = 34;
            this.m_btnSkip.Text = "SKIP";
            // 
            // m_ctrlShippingAddress
            // 
            this.m_ctrlShippingAddress.Address1 = null;
            this.m_ctrlShippingAddress.Address2 = null;
            this.m_ctrlShippingAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ctrlShippingAddress.City = null;
            this.m_ctrlShippingAddress.HeaderText = null;
            this.m_ctrlShippingAddress.Location = new System.Drawing.Point(619, 55);
            this.m_ctrlShippingAddress.Name = "m_ctrlShippingAddress";
            this.m_ctrlShippingAddress.Size = new System.Drawing.Size(391, 160);
            this.m_ctrlShippingAddress.State = null;
            this.m_ctrlShippingAddress.TabIndex = 33;
            this.m_ctrlShippingAddress.Zip = null;
            // 
            // m_ctrlBillingAddress
            // 
            this.m_ctrlBillingAddress.Address1 = null;
            this.m_ctrlBillingAddress.Address2 = null;
            this.m_ctrlBillingAddress.City = null;
            this.m_ctrlBillingAddress.HeaderText = null;
            this.m_ctrlBillingAddress.Location = new System.Drawing.Point(5, 55);
            this.m_ctrlBillingAddress.Name = "m_ctrlBillingAddress";
            this.m_ctrlBillingAddress.Size = new System.Drawing.Size(391, 160);
            this.m_ctrlBillingAddress.State = null;
            this.m_ctrlBillingAddress.TabIndex = 32;
            this.m_ctrlBillingAddress.Zip = null;
            // 
            // m_txtTotal
            // 
            this.m_txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtTotal.Enabled = false;
            this.m_txtTotal.Location = new System.Drawing.Point(906, 497);
            this.m_txtTotal.Name = "m_txtTotal";
            this.m_txtTotal.Size = new System.Drawing.Size(102, 20);
            this.m_txtTotal.TabIndex = 31;
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl7.Location = new System.Drawing.Point(876, 500);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 13);
            this.labelControl7.TabIndex = 30;
            this.labelControl7.Text = "Total";
            // 
            // m_txtTax
            // 
            this.m_txtTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtTax.Enabled = false;
            this.m_txtTax.Location = new System.Drawing.Point(906, 471);
            this.m_txtTax.Name = "m_txtTax";
            this.m_txtTax.Size = new System.Drawing.Size(102, 20);
            this.m_txtTax.TabIndex = 29;
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl6.Location = new System.Drawing.Point(876, 474);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(18, 13);
            this.labelControl6.TabIndex = 28;
            this.labelControl6.Text = "Tax";
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnSave.Appearance.Options.UseFont = true;
            this.m_btnSave.Location = new System.Drawing.Point(837, 560);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(178, 38);
            this.m_btnSave.TabIndex = 17;
            this.m_btnSave.Text = "SAVE";
            // 
            // m_grpExistingCustomers
            // 
            this.m_grpExistingCustomers.Controls.Add(this.m_gridExistingCustomers);
            this.m_grpExistingCustomers.Controls.Add(this.m_btnRestoreNewCustomer);
            this.m_grpExistingCustomers.Location = new System.Drawing.Point(5, 271);
            this.m_grpExistingCustomers.Name = "m_grpExistingCustomers";
            this.m_grpExistingCustomers.Size = new System.Drawing.Size(1001, 193);
            this.m_grpExistingCustomers.TabIndex = 27;
            // 
            // m_gridExistingCustomers
            // 
            this.m_gridExistingCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridExistingCustomers.EmbeddedNavigator.Name = "";
            this.m_gridExistingCustomers.Location = new System.Drawing.Point(2, 20);
            this.m_gridExistingCustomers.MainView = this.m_gridExistingCustomersView;
            this.m_gridExistingCustomers.Name = "m_gridExistingCustomers";
            this.m_gridExistingCustomers.Size = new System.Drawing.Size(997, 171);
            this.m_gridExistingCustomers.TabIndex = 12;
            this.m_gridExistingCustomers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridExistingCustomersView});
            // 
            // m_gridExistingCustomersView
            // 
            this.m_gridExistingCustomersView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn9,
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.m_gridExistingCustomersView.GridControl = this.m_gridExistingCustomers;
            this.m_gridExistingCustomersView.Name = "m_gridExistingCustomersView";
            this.m_gridExistingCustomersView.OptionsCustomization.AllowFilter = false;
            this.m_gridExistingCustomersView.OptionsCustomization.AllowGroup = false;
            this.m_gridExistingCustomersView.OptionsFilter.AllowFilterEditor = false;
            this.m_gridExistingCustomersView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Item";
            this.gridColumn9.FieldName = "QbItemName";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 0;
            this.gridColumn9.Width = 98;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Quantity";
            this.gridColumn7.FieldName = "Quantity";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 96;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Description";
            this.gridColumn1.FieldName = "Description";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 407;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Rate";
            this.gridColumn2.DisplayFormat.FormatString = "C";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "Rate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 106;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Amount";
            this.gridColumn3.DisplayFormat.FormatString = "C";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "Amount";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 69;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Tax";
            this.gridColumn4.FieldName = "QbSalesTaxCodeName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 82;
            // 
            // m_btnRestoreNewCustomer
            // 
            this.m_btnRestoreNewCustomer.Location = new System.Drawing.Point(729, 165);
            this.m_btnRestoreNewCustomer.Name = "m_btnRestoreNewCustomer";
            this.m_btnRestoreNewCustomer.Size = new System.Drawing.Size(126, 23);
            this.m_btnRestoreNewCustomer.TabIndex = 14;
            this.m_btnRestoreNewCustomer.Text = "Restore New Customer ";
            // 
            // m_dtInvoiceDate
            // 
            this.m_dtInvoiceDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtInvoiceDate.EditValue = null;
            this.m_dtInvoiceDate.Location = new System.Drawing.Point(906, 26);
            this.m_dtInvoiceDate.Name = "m_dtInvoiceDate";
            this.m_dtInvoiceDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtInvoiceDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtInvoiceDate.Size = new System.Drawing.Size(100, 20);
            this.m_dtInvoiceDate.TabIndex = 26;
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Location = new System.Drawing.Point(871, 29);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(23, 13);
            this.labelControl5.TabIndex = 25;
            this.labelControl5.Text = "Date";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Location = new System.Drawing.Point(642, 29);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(75, 13);
            this.labelControl4.TabIndex = 24;
            this.labelControl4.Text = "Invoice Number";
            // 
            // m_txtInvoiceNumber
            // 
            this.m_txtInvoiceNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtInvoiceNumber.Location = new System.Drawing.Point(723, 26);
            this.m_txtInvoiceNumber.Name = "m_txtInvoiceNumber";
            this.m_txtInvoiceNumber.Size = new System.Drawing.Size(102, 20);
            this.m_txtInvoiceNumber.TabIndex = 23;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 231);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(47, 13);
            this.labelControl3.TabIndex = 22;
            this.labelControl3.Text = "Sales Rep";
            // 
            // m_cmbSalesReps
            // 
            this.m_cmbSalesReps.Location = new System.Drawing.Point(72, 228);
            this.m_cmbSalesReps.Name = "m_cmbSalesReps";
            this.m_cmbSalesReps.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbSalesReps.Size = new System.Drawing.Size(240, 20);
            this.m_cmbSalesReps.TabIndex = 21;
            // 
            // m_txtProjectName
            // 
            this.m_txtProjectName.Enabled = false;
            this.m_txtProjectName.Location = new System.Drawing.Point(87, 26);
            this.m_txtProjectName.Name = "m_txtProjectName";
            this.m_txtProjectName.Size = new System.Drawing.Size(345, 20);
            this.m_txtProjectName.TabIndex = 20;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(34, 13);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "Project";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(229, 23);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Create New Service Invoice";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // m_btnDontSync
            // 
            this.m_btnDontSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnDontSync.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnDontSync.Appearance.Options.UseFont = true;
            this.m_btnDontSync.Location = new System.Drawing.Point(184, 560);
            this.m_btnDontSync.Name = "m_btnDontSync";
            this.m_btnDontSync.Size = new System.Drawing.Size(178, 38);
            this.m_btnDontSync.TabIndex = 35;
            this.m_btnDontSync.Text = "DON\'T SYNC";
            // 
            // InvoiceRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "InvoiceRequest";
            this.Size = new System.Drawing.Size(1015, 598);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtTax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_grpExistingCustomers)).EndInit();
            this.m_grpExistingCustomers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridExistingCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridExistingCustomersView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtInvoiceDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtInvoiceDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtInvoiceNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbSalesReps.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtProjectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit m_txtInvoiceNumber;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ImageComboBoxEdit m_cmbSalesReps;
        private DevExpress.XtraEditors.TextEdit m_txtProjectName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit m_dtInvoiceDate;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.GroupControl m_grpExistingCustomers;
        internal DevExpress.XtraGrid.GridControl m_gridExistingCustomers;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridExistingCustomersView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.SimpleButton m_btnRestoreNewCustomer;
        private DevExpress.XtraEditors.SimpleButton m_btnSave;
        private DevExpress.XtraEditors.TextEdit m_txtTotal;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit m_txtTax;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private QbAddress m_ctrlShippingAddress;
        private QbAddress m_ctrlBillingAddress;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private DevExpress.XtraEditors.SimpleButton m_btnSkip;
        private DevExpress.XtraEditors.SimpleButton m_btnDontSync;
    }
}
