namespace Dalworth.Server.MainForm.Components
{
    partial class CustomerViewEdit
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.m_lblEmail = new DevExpress.XtraEditors.LabelControl();
            this.m_btnCustomerEdit = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblWorkPhone = new DevExpress.XtraEditors.LabelControl();
            this.m_lblHomePhone = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblCustomerName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtEmail.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_txtEmail);
            this.groupControl1.Controls.Add(this.m_lblEmail);
            this.groupControl1.Controls.Add(this.m_btnCustomerEdit);
            this.groupControl1.Controls.Add(this.m_lblWorkPhone);
            this.groupControl1.Controls.Add(this.m_lblHomePhone);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.m_lblCustomerName);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(274, 134);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Customer";
            // 
            // m_txtEmail
            // 
            this.m_txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtEmail.Location = new System.Drawing.Point(45, 78);
            this.m_txtEmail.Name = "m_txtEmail";
            this.m_txtEmail.Properties.Mask.EditMask = "[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}";
            this.m_txtEmail.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtEmail.Size = new System.Drawing.Size(166, 20);
            this.m_txtEmail.TabIndex = 0;
            this.m_txtEmail.Leave += new System.EventHandler(this.OnEmailLeave);
            // 
            // m_lblEmail
            // 
            this.m_lblEmail.Location = new System.Drawing.Point(6, 81);
            this.m_lblEmail.Name = "m_lblEmail";
            this.m_lblEmail.Size = new System.Drawing.Size(24, 13);
            this.m_lblEmail.TabIndex = 12;
            this.m_lblEmail.Text = "Email";
            // 
            // m_btnCustomerEdit
            // 
            this.m_btnCustomerEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCustomerEdit.Location = new System.Drawing.Point(217, 106);
            this.m_btnCustomerEdit.Name = "m_btnCustomerEdit";
            this.m_btnCustomerEdit.Size = new System.Drawing.Size(54, 23);
            this.m_btnCustomerEdit.TabIndex = 1;
            this.m_btnCustomerEdit.TabStop = false;
            this.m_btnCustomerEdit.Text = "Ed&it";
            // 
            // m_lblWorkPhone
            // 
            this.m_lblWorkPhone.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblWorkPhone.Appearance.Options.UseFont = true;
            this.m_lblWorkPhone.Location = new System.Drawing.Point(45, 62);
            this.m_lblWorkPhone.Name = "m_lblWorkPhone";
            this.m_lblWorkPhone.Size = new System.Drawing.Size(49, 13);
            this.m_lblWorkPhone.TabIndex = 4;
            this.m_lblWorkPhone.Text = "7890245";
            // 
            // m_lblHomePhone
            // 
            this.m_lblHomePhone.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblHomePhone.Appearance.Options.UseFont = true;
            this.m_lblHomePhone.Location = new System.Drawing.Point(45, 43);
            this.m_lblHomePhone.Name = "m_lblHomePhone";
            this.m_lblHomePhone.Size = new System.Drawing.Size(42, 13);
            this.m_lblHomePhone.TabIndex = 3;
            this.m_lblHomePhone.Text = "123456";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Work";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 43);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Home";
            // 
            // m_lblCustomerName
            // 
            this.m_lblCustomerName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblCustomerName.Appearance.Options.UseFont = true;
            this.m_lblCustomerName.Location = new System.Drawing.Point(6, 24);
            this.m_lblCustomerName.Name = "m_lblCustomerName";
            this.m_lblCustomerName.Size = new System.Drawing.Size(76, 13);
            this.m_lblCustomerName.TabIndex = 0;
            this.m_lblCustomerName.Text = "Gary, Oldman";
            // 
            // CustomerViewEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "CustomerViewEdit";
            this.Size = new System.Drawing.Size(274, 134);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtEmail.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        protected DevExpress.XtraEditors.SimpleButton m_btnCustomerEdit;
        private DevExpress.XtraEditors.LabelControl m_lblWorkPhone;
        private DevExpress.XtraEditors.LabelControl m_lblHomePhone;
        private DevExpress.XtraEditors.LabelControl m_lblCustomerName;
        private DevExpress.XtraEditors.TextEdit m_txtEmail;
        private DevExpress.XtraEditors.LabelControl m_lblEmail;
    }
}
