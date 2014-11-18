namespace QuickBooksAgent.Windows.UI.Banking.WriteCheck
{
    partial class ExpenceLineView
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
            this.m_cmbAccount = new System.Windows.Forms.ComboBox();
            this.m_lblAccount = new System.Windows.Forms.Label();
            this.m_cmbCustomer = new System.Windows.Forms.ComboBox();
            this.m_lblCustomer = new System.Windows.Forms.Label();
            this.m_lblAmount = new System.Windows.Forms.Label();
            this.m_txtMemo = new System.Windows.Forms.TextBox();
            this.m_lblMemo = new System.Windows.Forms.Label();
            this.m_lblAmountLeftLabel = new System.Windows.Forms.Label();
            this.m_lblAmountLeft = new System.Windows.Forms.Label();
            this.m_curAmount = new QuickBooksAgent.Windows.UI.Controls.CurrencyEdit();
            this.SuspendLayout();
            // 
            // m_cmbAccount
            // 
            this.m_cmbAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbAccount.DisplayMember = "FullName";
            this.m_cmbAccount.Location = new System.Drawing.Point(63, 22);
            this.m_cmbAccount.Name = "m_cmbAccount";
            this.m_cmbAccount.Size = new System.Drawing.Size(174, 22);
            this.m_cmbAccount.TabIndex = 6;
            this.m_cmbAccount.ValueMember = "FullName";
            // 
            // m_lblAccount
            // 
            this.m_lblAccount.Location = new System.Drawing.Point(10, 24);
            this.m_lblAccount.Name = "m_lblAccount";
            this.m_lblAccount.Size = new System.Drawing.Size(53, 20);
            this.m_lblAccount.Text = "Account";
            // 
            // m_cmbCustomer
            // 
            this.m_cmbCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbCustomer.DisplayMember = "FullName";
            this.m_cmbCustomer.Location = new System.Drawing.Point(63, 46);
            this.m_cmbCustomer.Name = "m_cmbCustomer";
            this.m_cmbCustomer.Size = new System.Drawing.Size(174, 22);
            this.m_cmbCustomer.TabIndex = 9;
            this.m_cmbCustomer.ValueMember = "FullName";
            // 
            // m_lblCustomer
            // 
            this.m_lblCustomer.Location = new System.Drawing.Point(1, 48);
            this.m_lblCustomer.Name = "m_lblCustomer";
            this.m_lblCustomer.Size = new System.Drawing.Size(58, 20);
            this.m_lblCustomer.Text = "Customer";
            this.m_lblCustomer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblAmount
            // 
            this.m_lblAmount.Location = new System.Drawing.Point(79, 72);
            this.m_lblAmount.Name = "m_lblAmount";
            this.m_lblAmount.Size = new System.Drawing.Size(51, 20);
            this.m_lblAmount.Text = "Amount";
            // 
            // m_txtMemo
            // 
            this.m_txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtMemo.Location = new System.Drawing.Point(63, 93);
            this.m_txtMemo.MaxLength = 4000;
            this.m_txtMemo.Multiline = true;
            this.m_txtMemo.Name = "m_txtMemo";
            this.m_txtMemo.Size = new System.Drawing.Size(175, 58);
            this.m_txtMemo.TabIndex = 26;
            // 
            // m_lblMemo
            // 
            this.m_lblMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblMemo.Location = new System.Drawing.Point(23, 93);
            this.m_lblMemo.Name = "m_lblMemo";
            this.m_lblMemo.Size = new System.Drawing.Size(39, 19);
            this.m_lblMemo.Text = "Memo";
            // 
            // m_lblAmountLeftLabel
            // 
            this.m_lblAmountLeftLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAmountLeftLabel.Location = new System.Drawing.Point(3, 3);
            this.m_lblAmountLeftLabel.Name = "m_lblAmountLeftLabel";
            this.m_lblAmountLeftLabel.Size = new System.Drawing.Size(103, 19);
            this.m_lblAmountLeftLabel.Text = "Amount Left, $: ";
            // 
            // m_lblAmountLeft
            // 
            this.m_lblAmountLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAmountLeft.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAmountLeft.Location = new System.Drawing.Point(110, 3);
            this.m_lblAmountLeft.Name = "m_lblAmountLeft";
            this.m_lblAmountLeft.Size = new System.Drawing.Size(124, 19);
            // 
            // m_curAmount
            // 
            this.m_curAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curAmount.IsAllowNegative = true;
            this.m_curAmount.IsAllowNull = false;
            this.m_curAmount.Location = new System.Drawing.Point(128, 70);
            this.m_curAmount.MaxLength = 9;
            this.m_curAmount.Name = "m_curAmount";
            this.m_curAmount.Size = new System.Drawing.Size(109, 21);
            this.m_curAmount.TabIndex = 31;
            this.m_curAmount.Value = null;
            // 
            // ExpenceLineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_curAmount);
            this.Controls.Add(this.m_lblAmountLeftLabel);
            this.Controls.Add(this.m_lblAmountLeft);
            this.Controls.Add(this.m_txtMemo);
            this.Controls.Add(this.m_lblMemo);
            this.Controls.Add(this.m_lblAmount);
            this.Controls.Add(this.m_cmbCustomer);
            this.Controls.Add(this.m_lblCustomer);
            this.Controls.Add(this.m_cmbAccount);
            this.Controls.Add(this.m_lblAccount);
            this.Name = "ExpenceLineView";
            this.Size = new System.Drawing.Size(240, 294);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ComboBox m_cmbAccount;
        internal System.Windows.Forms.Label m_lblAccount;
        internal System.Windows.Forms.ComboBox m_cmbCustomer;
        internal System.Windows.Forms.Label m_lblCustomer;
        internal System.Windows.Forms.Label m_lblAmount;
        internal System.Windows.Forms.TextBox m_txtMemo;
        internal System.Windows.Forms.Label m_lblMemo;
        internal System.Windows.Forms.Label m_lblAmountLeftLabel;
        internal System.Windows.Forms.Label m_lblAmountLeft;
        internal QuickBooksAgent.Windows.UI.Controls.CurrencyEdit m_curAmount;
    }
}
