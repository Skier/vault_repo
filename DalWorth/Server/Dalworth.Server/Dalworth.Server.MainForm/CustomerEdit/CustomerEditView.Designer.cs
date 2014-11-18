namespace Dalworth.Server.MainForm.CustomerEdit
{
    partial class CustomerEditView
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
            Dalworth.Server.Domain.Address address2 = new Dalworth.Server.Domain.Address();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_grpAdsourceSalesRep = new DevExpress.XtraEditors.GroupControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_ctrlAdSourceSalesRep = new Dalworth.Server.MainForm.Components.AdSourceSalesRep();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.m_ctrlAddressEdit = new Dalworth.Server.MainForm.Components.AddressEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.m_cmbStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtWorkPhone = new DevExpress.XtraEditors.TextEdit();
            this.m_txtHomePhone = new DevExpress.XtraEditors.TextEdit();
            this.m_txtLastName = new DevExpress.XtraEditors.TextEdit();
            this.m_txtFirstName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_grpAdsourceSalesRep)).BeginInit();
            this.m_grpAdsourceSalesRep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtWorkPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtHomePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtLastName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtFirstName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_grpAdsourceSalesRep);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(510, 422);
            this.panelControl1.TabIndex = 0;
            // 
            // m_grpAdsourceSalesRep
            // 
            this.m_grpAdsourceSalesRep.Controls.Add(this.labelControl4);
            this.m_grpAdsourceSalesRep.Controls.Add(this.labelControl3);
            this.m_grpAdsourceSalesRep.Controls.Add(this.m_ctrlAdSourceSalesRep);
            this.m_grpAdsourceSalesRep.Location = new System.Drawing.Point(8, 260);
            this.m_grpAdsourceSalesRep.Name = "m_grpAdsourceSalesRep";
            this.m_grpAdsourceSalesRep.Size = new System.Drawing.Size(498, 110);
            this.m_grpAdsourceSalesRep.TabIndex = 5;
            this.m_grpAdsourceSalesRep.Text = "Adsource Sales Rep";
            // 
            // labelControl4
            // 
            this.labelControl4.AllowDrop = true;
            this.labelControl4.Location = new System.Drawing.Point(7, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Sales Rep";
            // 
            // labelControl3
            // 
            this.labelControl3.AllowDrop = true;
            this.labelControl3.Location = new System.Drawing.Point(7, 42);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(49, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Ad Source";
            // 
            // m_ctrlAdSourceSalesRep
            // 
            this.m_ctrlAdSourceSalesRep.AutoSize = true;
            this.m_ctrlAdSourceSalesRep.IsQbSalesRepRequired = false;
            this.m_ctrlAdSourceSalesRep.IsQbSalesRepVisible = true;
            this.m_ctrlAdSourceSalesRep.Location = new System.Drawing.Point(68, 34);
            this.m_ctrlAdSourceSalesRep.Margin = new System.Windows.Forms.Padding(0);
            this.m_ctrlAdSourceSalesRep.Name = "m_ctrlAdSourceSalesRep";
            this.m_ctrlAdSourceSalesRep.Size = new System.Drawing.Size(369, 51);
            this.m_ctrlAdSourceSalesRep.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.m_ctrlAddressEdit);
            this.groupControl2.Location = new System.Drawing.Point(5, 126);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(498, 128);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "Customer Address";
            // 
            // m_ctrlAddressEdit
            // 
            address2.Address2 = "";
            address2.AreaId = null;
            address2.Block = "";
            address2.City = "";
            address2.ID = 0;
            address2.MapLetter = "";
            address2.MapPage = "";
            address2.Modified = new System.DateTime(((long)(0)));
            address2.Prefix = "";
            address2.State = "";
            address2.Street = "";
            address2.Suffix = "";
            address2.Unit = "";
            address2.Zip = null;
            this.m_ctrlAddressEdit.Address = address2;
            this.m_ctrlAddressEdit.IsValidationDisabled = false;
            this.m_ctrlAddressEdit.Location = new System.Drawing.Point(3, 23);
            this.m_ctrlAddressEdit.Name = "m_ctrlAddressEdit";
            this.m_ctrlAddressEdit.Size = new System.Drawing.Size(490, 102);
            this.m_ctrlAddressEdit.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_txtEmail);
            this.groupControl1.Controls.Add(this.m_cmbStatus);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.m_txtWorkPhone);
            this.groupControl1.Controls.Add(this.m_txtHomePhone);
            this.groupControl1.Controls.Add(this.m_txtLastName);
            this.groupControl1.Controls.Add(this.m_txtFirstName);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(5, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(498, 115);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Customer";
            // 
            // m_txtEmail
            // 
            this.m_txtEmail.Location = new System.Drawing.Point(288, 83);
            this.m_txtEmail.Name = "m_txtEmail";
            this.m_txtEmail.Properties.Mask.EditMask = "[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}";
            this.m_txtEmail.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtEmail.Properties.Mask.SaveLiteral = false;
            this.m_txtEmail.Properties.Mask.ShowPlaceHolders = false;
            this.m_txtEmail.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.m_txtEmail.Size = new System.Drawing.Size(205, 20);
            this.m_txtEmail.TabIndex = 8;
            // 
            // m_cmbStatus
            // 
            this.m_cmbStatus.EditValue = "Residential";
            this.m_cmbStatus.Location = new System.Drawing.Point(96, 83);
            this.m_cmbStatus.Name = "m_cmbStatus";
            this.m_cmbStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbStatus.Properties.Items.AddRange(new object[] {
            "Business",
            "Residential"});
            this.m_cmbStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_cmbStatus.Size = new System.Drawing.Size(186, 20);
            this.m_cmbStatus.TabIndex = 7;
            // 
            // labelControl6
            // 
            this.labelControl6.AllowDrop = true;
            this.labelControl6.Location = new System.Drawing.Point(10, 86);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(55, 13);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "&Type, Email";
            // 
            // m_txtWorkPhone
            // 
            this.m_txtWorkPhone.Location = new System.Drawing.Point(288, 56);
            this.m_txtWorkPhone.Name = "m_txtWorkPhone";
            this.m_txtWorkPhone.Properties.Mask.EditMask = "(\\d?\\d?\\d?) \\d\\d\\d-\\d\\d\\d\\d";
            this.m_txtWorkPhone.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.m_txtWorkPhone.Properties.Mask.SaveLiteral = false;
            this.m_txtWorkPhone.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.m_txtWorkPhone.Size = new System.Drawing.Size(205, 20);
            this.m_txtWorkPhone.TabIndex = 5;
            // 
            // m_txtHomePhone
            // 
            this.m_txtHomePhone.Location = new System.Drawing.Point(96, 56);
            this.m_txtHomePhone.Name = "m_txtHomePhone";
            this.m_txtHomePhone.Properties.Mask.EditMask = "(\\d?\\d?\\d?) \\d\\d\\d-\\d\\d\\d\\d";
            this.m_txtHomePhone.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.m_txtHomePhone.Properties.Mask.SaveLiteral = false;
            this.m_txtHomePhone.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.m_txtHomePhone.Size = new System.Drawing.Size(186, 20);
            this.m_txtHomePhone.TabIndex = 4;
            // 
            // m_txtLastName
            // 
            this.m_txtLastName.Location = new System.Drawing.Point(96, 30);
            this.m_txtLastName.Name = "m_txtLastName";
            this.m_txtLastName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtLastName.Properties.MaxLength = 40;
            this.m_txtLastName.Size = new System.Drawing.Size(186, 20);
            this.m_txtLastName.TabIndex = 1;
            // 
            // m_txtFirstName
            // 
            this.m_txtFirstName.Location = new System.Drawing.Point(288, 30);
            this.m_txtFirstName.Name = "m_txtFirstName";
            this.m_txtFirstName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtFirstName.Properties.MaxLength = 40;
            this.m_txtFirstName.Size = new System.Drawing.Size(205, 20);
            this.m_txtFirstName.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.AllowDrop = true;
            this.labelControl2.Location = new System.Drawing.Point(10, 59);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "&Home, Work";
            // 
            // labelControl1
            // 
            this.labelControl1.AllowDrop = true;
            this.labelControl1.Location = new System.Drawing.Point(10, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "&Last, First Name";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(432, 395);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(351, 395);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "O&K";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // CustomerEditView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(510, 422);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerEditView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CustomerEditView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_grpAdsourceSalesRep)).EndInit();
            this.m_grpAdsourceSalesRep.ResumeLayout(false);
            this.m_grpAdsourceSalesRep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtWorkPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtHomePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtLastName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtFirstName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        internal DevExpress.XtraEditors.TextEdit m_txtWorkPhone;
        internal DevExpress.XtraEditors.TextEdit m_txtHomePhone;
        internal DevExpress.XtraEditors.TextEdit m_txtLastName;
        internal DevExpress.XtraEditors.TextEdit m_txtFirstName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        internal DevExpress.XtraEditors.ComboBoxEdit m_cmbStatus;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal Dalworth.Server.MainForm.Components.AddressEdit m_ctrlAddressEdit;
        internal DevExpress.XtraEditors.TextEdit m_txtEmail;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal Components.AdSourceSalesRep m_ctrlAdSourceSalesRep;
        internal DevExpress.XtraEditors.GroupControl m_grpAdsourceSalesRep;
    }
}