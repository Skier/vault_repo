
namespace SmartSchedule.Win32.UserEdit
{
    partial class UserEditView
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtPasswordConfirm = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.m_chkIsActive = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbUserRole = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtLogin = new DevExpress.XtraEditors.TextEdit();
            this.m_btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPasswordConfirm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkIsActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbUserRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.CausesValidation = false;
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.m_txtPasswordConfirm);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_txtPassword);
            this.panelControl1.Controls.Add(this.m_chkIsActive);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_cmbUserRole);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.m_txtLogin);
            this.panelControl1.Controls.Add(this.m_btnSave);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(479, 112);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(240, 34);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(110, 13);
            this.labelControl4.TabIndex = 47;
            this.labelControl4.Text = "Confirm New Password";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(268, 200);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(0, 13);
            this.labelControl3.TabIndex = 46;
            // 
            // m_txtPasswordConfirm
            // 
            this.m_txtPasswordConfirm.Location = new System.Drawing.Point(356, 31);
            this.m_txtPasswordConfirm.Name = "m_txtPasswordConfirm";
            this.m_txtPasswordConfirm.Properties.Mask.ShowPlaceHolders = false;
            this.m_txtPasswordConfirm.Properties.PasswordChar = '*';
            this.m_txtPasswordConfirm.Size = new System.Drawing.Size(119, 20);
            this.m_txtPasswordConfirm.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(240, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 13);
            this.labelControl2.TabIndex = 44;
            this.labelControl2.Text = "New Password";
            // 
            // m_txtPassword
            // 
            this.m_txtPassword.Location = new System.Drawing.Point(356, 5);
            this.m_txtPassword.Name = "m_txtPassword";
            this.m_txtPassword.Properties.Mask.ShowPlaceHolders = false;
            this.m_txtPassword.Properties.PasswordChar = '*';
            this.m_txtPassword.Size = new System.Drawing.Size(119, 20);
            this.m_txtPassword.TabIndex = 3;
            // 
            // m_chkIsActive
            // 
            this.m_chkIsActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkIsActive.Location = new System.Drawing.Point(47, 57);
            this.m_chkIsActive.Name = "m_chkIsActive";
            this.m_chkIsActive.Properties.Caption = "Active";
            this.m_chkIsActive.Size = new System.Drawing.Size(78, 19);
            this.m_chkIsActive.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(21, 13);
            this.labelControl1.TabIndex = 30;
            this.labelControl1.Text = "Role";
            // 
            // m_cmbUserRole
            // 
            this.m_cmbUserRole.Location = new System.Drawing.Point(49, 31);
            this.m_cmbUserRole.Name = "m_cmbUserRole";
            this.m_cmbUserRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbUserRole.Properties.Sorted = true;
            this.m_cmbUserRole.Size = new System.Drawing.Size(145, 20);
            this.m_cmbUserRole.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 8);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(27, 13);
            this.labelControl5.TabIndex = 28;
            this.labelControl5.Text = "Name";
            // 
            // m_txtLogin
            // 
            this.m_txtLogin.Location = new System.Drawing.Point(49, 5);
            this.m_txtLogin.Name = "m_txtLogin";
            this.m_txtLogin.Properties.Mask.EditMask = "\\d{6,6}";
            this.m_txtLogin.Properties.Mask.ShowPlaceHolders = false;
            this.m_txtLogin.Properties.MaxLength = 100;
            this.m_txtLogin.Size = new System.Drawing.Size(145, 20);
            this.m_txtLogin.TabIndex = 0;
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.Location = new System.Drawing.Point(320, 86);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(75, 23);
            this.m_btnSave.TabIndex = 5;
            this.m_btnSave.Text = "Save";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(401, 86);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 6;
            this.m_btnCancel.Text = "Cancel";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // UserEditView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(479, 112);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserEditView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Edit";
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPasswordConfirm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkIsActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbUserRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        internal DevExpress.XtraEditors.TextEdit m_txtLogin;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbUserRole;
        internal DevExpress.XtraEditors.CheckEdit m_chkIsActive;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.TextEdit m_txtPasswordConfirm;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.TextEdit m_txtPassword;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;

    }
}