namespace Dalworth.Server.MainForm.CustomerEdit
{
    partial class InsuranceCompanyEditView
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
            Dalworth.Server.Domain.Address address1 = new Dalworth.Server.Domain.Address();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.m_ctrlAddressEdit = new Dalworth.Server.MainForm.Components.AddressEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtPhone2 = new DevExpress.XtraEditors.TextEdit();
            this.m_txtPhone1 = new DevExpress.XtraEditors.TextEdit();
            this.m_txtContactPerson = new DevExpress.XtraEditors.TextEdit();
            this.m_txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPhone2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPhone1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtContactPerson.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(444, 268);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.m_ctrlAddressEdit);
            this.groupControl2.Location = new System.Drawing.Point(5, 124);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(435, 113);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "Company Address";
            // 
            // m_ctrlAddressEdit
            // 
            address1.Address1 = "";
            address1.Address2 = "";
            address1.AreaId = null;
            address1.City = "";
            address1.ID = 0;
            address1.Map = "";
            address1.State = "";
            address1.Zip = "";
            this.m_ctrlAddressEdit.Address = address1;
            this.m_ctrlAddressEdit.Location = new System.Drawing.Point(1, 21);
            this.m_ctrlAddressEdit.MinimumSize = new System.Drawing.Size(432, 90);
            this.m_ctrlAddressEdit.Name = "m_ctrlAddressEdit";
            this.m_ctrlAddressEdit.Size = new System.Drawing.Size(432, 90);
            this.m_ctrlAddressEdit.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.m_txtPhone2);
            this.groupControl1.Controls.Add(this.m_txtPhone1);
            this.groupControl1.Controls.Add(this.m_txtContactPerson);
            this.groupControl1.Controls.Add(this.m_txtName);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(5, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(435, 113);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "Company";
            // 
            // labelControl6
            // 
            this.labelControl6.AllowDrop = true;
            this.labelControl6.Location = new System.Drawing.Point(10, 86);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(74, 13);
            this.labelControl6.TabIndex = 4;
            this.labelControl6.Text = "Contact Person";
            // 
            // m_txtPhone2
            // 
            this.m_txtPhone2.Location = new System.Drawing.Point(253, 56);
            this.m_txtPhone2.Name = "m_txtPhone2";
            this.m_txtPhone2.Size = new System.Drawing.Size(171, 20);
            this.m_txtPhone2.TabIndex = 3;
            // 
            // m_txtPhone1
            // 
            this.m_txtPhone1.Location = new System.Drawing.Point(96, 56);
            this.m_txtPhone1.Name = "m_txtPhone1";
            this.m_txtPhone1.Size = new System.Drawing.Size(151, 20);
            this.m_txtPhone1.TabIndex = 2;
            // 
            // m_txtContactPerson
            // 
            this.m_txtContactPerson.Location = new System.Drawing.Point(96, 82);
            this.m_txtContactPerson.Name = "m_txtContactPerson";
            this.m_txtContactPerson.Size = new System.Drawing.Size(328, 20);
            this.m_txtContactPerson.TabIndex = 1;
            // 
            // m_txtName
            // 
            this.m_txtName.Location = new System.Drawing.Point(96, 30);
            this.m_txtName.Name = "m_txtName";
            this.m_txtName.Size = new System.Drawing.Size(328, 20);
            this.m_txtName.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.AllowDrop = true;
            this.labelControl2.Location = new System.Drawing.Point(10, 59);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Phone 1, Phone 2";
            // 
            // labelControl1
            // 
            this.labelControl1.AllowDrop = true;
            this.labelControl1.Location = new System.Drawing.Point(10, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Name";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(366, 241);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 11;
            this.m_btnCancel.Text = "Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(285, 241);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 10;
            this.m_btnOk.Text = "OK";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // InsuranceCompanyEditView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(444, 268);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsuranceCompanyEditView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CustomerEditView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPhone2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPhone1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtContactPerson.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtName.Properties)).EndInit();
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
        internal DevExpress.XtraEditors.TextEdit m_txtPhone2;
        internal DevExpress.XtraEditors.TextEdit m_txtPhone1;
        internal DevExpress.XtraEditors.TextEdit m_txtContactPerson;
        internal DevExpress.XtraEditors.TextEdit m_txtName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal Dalworth.Server.MainForm.Components.AddressEdit m_ctrlAddressEdit;
    }
}