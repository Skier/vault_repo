namespace QuickBooksAgent.Windows.UI.Password
{
    partial class PasswordView
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtLogin = new System.Windows.Forms.TextBox();
            this.m_txtPassword = new System.Windows.Forms.TextBox();
            this.m_cmbTransactionHistory = new System.Windows.Forms.ComboBox();
            this.m_lblTransactionHistory = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 21);
            this.label1.Text = "Login:";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 21);
            this.label2.Text = "Password:";
            // 
            // m_txtLogin
            // 
            this.m_txtLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtLogin.Location = new System.Drawing.Point(81, 31);
            this.m_txtLogin.Name = "m_txtLogin";
            this.m_txtLogin.Size = new System.Drawing.Size(131, 21);
            this.m_txtLogin.TabIndex = 2;
            this.m_txtLogin.Visible = false;
            // 
            // m_txtPassword
            // 
            this.m_txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtPassword.Location = new System.Drawing.Point(81, 60);
            this.m_txtPassword.Name = "m_txtPassword";
            this.m_txtPassword.PasswordChar = '*';
            this.m_txtPassword.Size = new System.Drawing.Size(131, 21);
            this.m_txtPassword.TabIndex = 2;
            // 
            // m_cmbTransactionHistory
            // 
            this.m_cmbTransactionHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTransactionHistory.DisplayMember = "Description";
            this.m_cmbTransactionHistory.Location = new System.Drawing.Point(81, 144);
            this.m_cmbTransactionHistory.Name = "m_cmbTransactionHistory";
            this.m_cmbTransactionHistory.Size = new System.Drawing.Size(131, 22);
            this.m_cmbTransactionHistory.TabIndex = 5;
            this.m_cmbTransactionHistory.ValueMember = "DaysCount";
            // 
            // m_lblTransactionHistory
            // 
            this.m_lblTransactionHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTransactionHistory.Location = new System.Drawing.Point(15, 89);
            this.m_lblTransactionHistory.Name = "m_lblTransactionHistory";
            this.m_lblTransactionHistory.Size = new System.Drawing.Size(197, 47);
            this.m_lblTransactionHistory.Text = "At the first time sync you can specify how many transactions you want to get:";
            // 
            // PasswordView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_lblTransactionHistory);
            this.Controls.Add(this.m_cmbTransactionHistory);
            this.Controls.Add(this.m_txtPassword);
            this.Controls.Add(this.m_txtLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PasswordView";
            this.Size = new System.Drawing.Size(224, 237);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox m_txtLogin;
        internal System.Windows.Forms.TextBox m_txtPassword;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label m_lblTransactionHistory;
        internal System.Windows.Forms.ComboBox m_cmbTransactionHistory;
    }
}
