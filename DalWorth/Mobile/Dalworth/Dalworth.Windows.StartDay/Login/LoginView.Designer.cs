namespace Dalworth.Windows.StartDay.Login
{
    partial class LoginView
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cmbTechnician = new System.Windows.Forms.ComboBox();
            this.m_txtPassword = new System.Windows.Forms.TextBox();            
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.Text = "Password";
            // 
            // m_cmbTechnician
            // 
            this.m_cmbTechnician.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTechnician.DisplayMember = "DisplayName";
            this.m_cmbTechnician.Location = new System.Drawing.Point(68, 72);
            this.m_cmbTechnician.Name = "m_cmbTechnician";
            this.m_cmbTechnician.Size = new System.Drawing.Size(169, 22);
            this.m_cmbTechnician.TabIndex = 3;
            this.m_cmbTechnician.ValueMember = "ID";
            // 
            // m_txtPassword
            // 
            this.m_txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtPassword.Location = new System.Drawing.Point(68, 97);
            this.m_txtPassword.Name = "m_txtPassword";
            this.m_txtPassword.PasswordChar = '*';
            this.m_txtPassword.Size = new System.Drawing.Size(169, 21);
            this.m_txtPassword.TabIndex = 4;
            // 
            // LoginView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_txtPassword);
            this.Controls.Add(this.m_cmbTechnician);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LoginView";
            this.Size = new System.Drawing.Size(240, 268);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ComboBox m_cmbTechnician;
        internal System.Windows.Forms.TextBox m_txtPassword;
    }
}
