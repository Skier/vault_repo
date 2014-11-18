namespace QuickBooksAgent.Windows.UI.Banking.WriteCheck
{
    partial class WriteCheckView
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
            this.m_tabs = new System.Windows.Forms.TabControl();
            this.m_tabGeneral = new System.Windows.Forms.TabPage();
            this.m_curAmount = new QuickBooksAgent.Windows.UI.Controls.CurrencyEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chkToBePrinted = new System.Windows.Forms.CheckBox();
            this.m_lblAmount = new System.Windows.Forms.Label();
            this.m_dtpDate = new System.Windows.Forms.DateTimePicker();
            this.m_lblDate = new System.Windows.Forms.Label();
            this.m_txtCheckNumber = new System.Windows.Forms.TextBox();
            this.m_lblCheckNumber = new System.Windows.Forms.Label();
            this.m_cmbPayee = new System.Windows.Forms.ComboBox();
            this.m_cmbPayeeType = new System.Windows.Forms.ComboBox();
            this.m_cmbBankAccount = new System.Windows.Forms.ComboBox();
            this.m_lblBankAccount = new System.Windows.Forms.Label();
            this.m_lblBalanceLabel = new System.Windows.Forms.Label();
            this.m_lblBalance = new System.Windows.Forms.Label();
            this.m_tabAdditional = new System.Windows.Forms.TabPage();
            this.m_txtMemo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_pnlTop = new System.Windows.Forms.Panel();
            this.m_lblPrintAs = new System.Windows.Forms.Label();
            this.m_lblAddress = new System.Windows.Forms.Label();
            this.m_lblPrintAsLabel = new System.Windows.Forms.Label();
            this.m_lblAddressLabel = new System.Windows.Forms.Label();
            this.m_tabExpences = new System.Windows.Forms.TabPage();
            this.m_table = new QuickBooksAgent.Windows.UI.Controls.Table();
            this.m_pnlTop2 = new System.Windows.Forms.Panel();
            this.m_lblAmountLeftLabel = new System.Windows.Forms.Label();
            this.m_lblAmountLeft = new System.Windows.Forms.Label();
            this.m_tabs.SuspendLayout();
            this.m_tabGeneral.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_tabAdditional.SuspendLayout();
            this.m_pnlTop.SuspendLayout();
            this.m_tabExpences.SuspendLayout();
            this.m_pnlTop2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabs
            // 
            this.m_tabs.Controls.Add(this.m_tabGeneral);
            this.m_tabs.Controls.Add(this.m_tabAdditional);
            this.m_tabs.Controls.Add(this.m_tabExpences);
            this.m_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabs.Location = new System.Drawing.Point(0, 0);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedIndex = 0;
            this.m_tabs.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.TabIndex = 0;
            // 
            // m_tabGeneral
            // 
            this.m_tabGeneral.AutoScroll = true;
            this.m_tabGeneral.Controls.Add(this.m_curAmount);
            this.m_tabGeneral.Controls.Add(this.panel1);
            this.m_tabGeneral.Controls.Add(this.m_lblAmount);
            this.m_tabGeneral.Controls.Add(this.m_dtpDate);
            this.m_tabGeneral.Controls.Add(this.m_lblDate);
            this.m_tabGeneral.Controls.Add(this.m_txtCheckNumber);
            this.m_tabGeneral.Controls.Add(this.m_lblCheckNumber);
            this.m_tabGeneral.Controls.Add(this.m_cmbPayee);
            this.m_tabGeneral.Controls.Add(this.m_cmbPayeeType);
            this.m_tabGeneral.Controls.Add(this.m_cmbBankAccount);
            this.m_tabGeneral.Controls.Add(this.m_lblBankAccount);
            this.m_tabGeneral.Controls.Add(this.m_lblBalanceLabel);
            this.m_tabGeneral.Controls.Add(this.m_lblBalance);
            this.m_tabGeneral.Location = new System.Drawing.Point(0, 0);
            this.m_tabGeneral.Name = "m_tabGeneral";
            this.m_tabGeneral.Size = new System.Drawing.Size(240, 245);
            this.m_tabGeneral.Text = "General";
            // 
            // m_curAmount
            // 
            this.m_curAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curAmount.IsAllowNegative = false;
            this.m_curAmount.IsAllowNull = false;
            this.m_curAmount.Location = new System.Drawing.Point(176, 114);
            this.m_curAmount.MaxLength = 9;
            this.m_curAmount.Name = "m_curAmount";
            this.m_curAmount.Size = new System.Drawing.Size(61, 21);
            this.m_curAmount.TabIndex = 26;
            this.m_curAmount.Value = null;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkToBePrinted);
            this.panel1.Location = new System.Drawing.Point(1, 113);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(119, 25);
            // 
            // m_chkToBePrinted
            // 
            this.m_chkToBePrinted.Location = new System.Drawing.Point(1, 3);
            this.m_chkToBePrinted.Name = "m_chkToBePrinted";
            this.m_chkToBePrinted.Size = new System.Drawing.Size(107, 20);
            this.m_chkToBePrinted.TabIndex = 13;
            this.m_chkToBePrinted.Text = "To Be Printed";
            // 
            // m_lblAmount
            // 
            this.m_lblAmount.Location = new System.Drawing.Point(128, 117);
            this.m_lblAmount.Name = "m_lblAmount";
            this.m_lblAmount.Size = new System.Drawing.Size(51, 20);
            this.m_lblAmount.Text = "Amount";
            // 
            // m_dtpDate
            // 
            this.m_dtpDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpDate.Location = new System.Drawing.Point(86, 90);
            this.m_dtpDate.Name = "m_dtpDate";
            this.m_dtpDate.Size = new System.Drawing.Size(151, 22);
            this.m_dtpDate.TabIndex = 17;
            // 
            // m_lblDate
            // 
            this.m_lblDate.Location = new System.Drawing.Point(53, 93);
            this.m_lblDate.Name = "m_lblDate";
            this.m_lblDate.Size = new System.Drawing.Size(32, 20);
            this.m_lblDate.Text = "Date";
            // 
            // m_txtCheckNumber
            // 
            this.m_txtCheckNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCheckNumber.Location = new System.Drawing.Point(86, 67);
            this.m_txtCheckNumber.MaxLength = 21;
            this.m_txtCheckNumber.Name = "m_txtCheckNumber";
            this.m_txtCheckNumber.Size = new System.Drawing.Size(151, 21);
            this.m_txtCheckNumber.TabIndex = 14;
            // 
            // m_lblCheckNumber
            // 
            this.m_lblCheckNumber.Location = new System.Drawing.Point(34, 70);
            this.m_lblCheckNumber.Name = "m_lblCheckNumber";
            this.m_lblCheckNumber.Size = new System.Drawing.Size(51, 20);
            this.m_lblCheckNumber.Text = "Check #";
            // 
            // m_cmbPayee
            // 
            this.m_cmbPayee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbPayee.DisplayMember = "FullName";
            this.m_cmbPayee.Location = new System.Drawing.Point(86, 43);
            this.m_cmbPayee.Name = "m_cmbPayee";
            this.m_cmbPayee.Size = new System.Drawing.Size(151, 22);
            this.m_cmbPayee.TabIndex = 6;
            this.m_cmbPayee.ValueMember = "FullName";
            // 
            // m_cmbPayeeType
            // 
            this.m_cmbPayeeType.Items.Add("Vendor");
            this.m_cmbPayeeType.Items.Add("Customer");
            this.m_cmbPayeeType.Items.Add("Employee");
            this.m_cmbPayeeType.Location = new System.Drawing.Point(2, 43);
            this.m_cmbPayeeType.Name = "m_cmbPayeeType";
            this.m_cmbPayeeType.Size = new System.Drawing.Size(81, 22);
            this.m_cmbPayeeType.TabIndex = 5;
            // 
            // m_cmbBankAccount
            // 
            this.m_cmbBankAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbBankAccount.DisplayMember = "FullName";
            this.m_cmbBankAccount.Location = new System.Drawing.Point(86, 2);
            this.m_cmbBankAccount.Name = "m_cmbBankAccount";
            this.m_cmbBankAccount.Size = new System.Drawing.Size(151, 22);
            this.m_cmbBankAccount.TabIndex = 4;
            this.m_cmbBankAccount.ValueMember = "FullName";
            // 
            // m_lblBankAccount
            // 
            this.m_lblBankAccount.Location = new System.Drawing.Point(4, 5);
            this.m_lblBankAccount.Name = "m_lblBankAccount";
            this.m_lblBankAccount.Size = new System.Drawing.Size(82, 20);
            this.m_lblBankAccount.Text = "Bank Account";
            // 
            // m_lblBalanceLabel
            // 
            this.m_lblBalanceLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblBalanceLabel.Location = new System.Drawing.Point(4, 25);
            this.m_lblBalanceLabel.Name = "m_lblBalanceLabel";
            this.m_lblBalanceLabel.Size = new System.Drawing.Size(76, 19);
            this.m_lblBalanceLabel.Text = "Balance, $: ";
            // 
            // m_lblBalance
            // 
            this.m_lblBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblBalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblBalance.Location = new System.Drawing.Point(88, 25);
            this.m_lblBalance.Name = "m_lblBalance";
            this.m_lblBalance.Size = new System.Drawing.Size(149, 19);
            // 
            // m_tabAdditional
            // 
            this.m_tabAdditional.AutoScroll = true;
            this.m_tabAdditional.Controls.Add(this.m_txtMemo);
            this.m_tabAdditional.Controls.Add(this.label1);
            this.m_tabAdditional.Controls.Add(this.m_pnlTop);
            this.m_tabAdditional.Location = new System.Drawing.Point(0, 0);
            this.m_tabAdditional.Name = "m_tabAdditional";
            this.m_tabAdditional.Size = new System.Drawing.Size(240, 245);
            this.m_tabAdditional.Text = "Additional";
            // 
            // m_txtMemo
            // 
            this.m_txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtMemo.Location = new System.Drawing.Point(47, 108);
            this.m_txtMemo.MaxLength = 4000;
            this.m_txtMemo.Multiline = true;
            this.m_txtMemo.Name = "m_txtMemo";
            this.m_txtMemo.Size = new System.Drawing.Size(190, 51);
            this.m_txtMemo.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 19);
            this.label1.Text = "Memo";
            // 
            // m_pnlTop
            // 
            this.m_pnlTop.Controls.Add(this.m_lblPrintAs);
            this.m_pnlTop.Controls.Add(this.m_lblAddress);
            this.m_pnlTop.Controls.Add(this.m_lblPrintAsLabel);
            this.m_pnlTop.Controls.Add(this.m_lblAddressLabel);
            this.m_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTop.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTop.Name = "m_pnlTop";
            this.m_pnlTop.Size = new System.Drawing.Size(240, 107);
            // 
            // m_lblPrintAs
            // 
            this.m_lblPrintAs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblPrintAs.Location = new System.Drawing.Point(60, 2);
            this.m_lblPrintAs.Name = "m_lblPrintAs";
            this.m_lblPrintAs.Size = new System.Drawing.Size(177, 19);
            // 
            // m_lblAddress
            // 
            this.m_lblAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAddress.Location = new System.Drawing.Point(3, 40);
            this.m_lblAddress.Name = "m_lblAddress";
            this.m_lblAddress.Size = new System.Drawing.Size(234, 62);
            // 
            // m_lblPrintAsLabel
            // 
            this.m_lblPrintAsLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblPrintAsLabel.Location = new System.Drawing.Point(3, 3);
            this.m_lblPrintAsLabel.Name = "m_lblPrintAsLabel";
            this.m_lblPrintAsLabel.Size = new System.Drawing.Size(58, 19);
            this.m_lblPrintAsLabel.Text = "Print As: ";
            // 
            // m_lblAddressLabel
            // 
            this.m_lblAddressLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAddressLabel.Location = new System.Drawing.Point(3, 22);
            this.m_lblAddressLabel.Name = "m_lblAddressLabel";
            this.m_lblAddressLabel.Size = new System.Drawing.Size(237, 19);
            this.m_lblAddressLabel.Text = "Address:";
            // 
            // m_tabExpences
            // 
            this.m_tabExpences.AutoScroll = true;
            this.m_tabExpences.Controls.Add(this.m_table);
            this.m_tabExpences.Controls.Add(this.m_pnlTop2);
            this.m_tabExpences.Location = new System.Drawing.Point(0, 0);
            this.m_tabExpences.Name = "m_tabExpences";
            this.m_tabExpences.Size = new System.Drawing.Size(240, 245);
            this.m_tabExpences.Text = "Expences";
            // 
            // m_table
            // 
            this.m_table.AllowColumnResize = false;
            this.m_table.AltBackColor = System.Drawing.Color.Linen;
            this.m_table.AltForeColor = System.Drawing.Color.Black;
            this.m_table.AutoColumnSize = true;
            this.m_table.AutoMoveRow = true;
            this.m_table.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_table.ColumnBackColor = System.Drawing.Color.LightGray;
            this.m_table.ColumnFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.m_table.ColumnForeColor = System.Drawing.Color.Black;
            this.m_table.DefaultLineAligment = System.Drawing.StringAlignment.Center;
            this.m_table.DefaultRowHeight = 20;
            this.m_table.DefaultTextAligment = System.Drawing.StringAlignment.Center;
            this.m_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_table.DrawGridBorder = true;
            this.m_table.FocusCellBackColor = System.Drawing.Color.Black;
            this.m_table.FocusCellForeColor = System.Drawing.Color.White;
            this.m_table.GreyOut = false;
            this.m_table.LeftHeader = false;
            this.m_table.Location = new System.Drawing.Point(0, 24);
            this.m_table.MultipleSelection = false;
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(240, 221);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = QuickBooksAgent.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 12;
            this.m_table.Text = "m_table";
            // 
            // m_pnlTop2
            // 
            this.m_pnlTop2.Controls.Add(this.m_lblAmountLeftLabel);
            this.m_pnlTop2.Controls.Add(this.m_lblAmountLeft);
            this.m_pnlTop2.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTop2.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTop2.Name = "m_pnlTop2";
            this.m_pnlTop2.Size = new System.Drawing.Size(240, 24);
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
            // WriteCheckView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tabs);
            this.Name = "WriteCheckView";
            this.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.ResumeLayout(false);
            this.m_tabGeneral.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_tabAdditional.ResumeLayout(false);
            this.m_pnlTop.ResumeLayout(false);
            this.m_tabExpences.ResumeLayout(false);
            this.m_pnlTop2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabControl m_tabs;
        internal System.Windows.Forms.TabPage m_tabGeneral;
        internal System.Windows.Forms.Label m_lblBalanceLabel;
        internal System.Windows.Forms.Label m_lblBankAccount;
        internal System.Windows.Forms.ComboBox m_cmbBankAccount;
        internal System.Windows.Forms.ComboBox m_cmbPayeeType;
        internal System.Windows.Forms.ComboBox m_cmbPayee;
        internal System.Windows.Forms.Label m_lblCheckNumber;
        internal System.Windows.Forms.TextBox m_txtCheckNumber;
        internal System.Windows.Forms.Label m_lblDate;
        internal System.Windows.Forms.Label m_lblAmount;
        internal System.Windows.Forms.DateTimePicker m_dtpDate;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.CheckBox m_chkToBePrinted;
        internal System.Windows.Forms.Label m_lblPrintAsLabel;
        private System.Windows.Forms.Panel m_pnlTop;
        internal System.Windows.Forms.Label m_lblAddress;
        internal System.Windows.Forms.Label m_lblAddressLabel;
        internal System.Windows.Forms.TextBox m_txtMemo;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label m_lblBalance;
        internal System.Windows.Forms.Label m_lblPrintAs;
        internal System.Windows.Forms.Label m_lblAmountLeftLabel;
        internal System.Windows.Forms.Label m_lblAmountLeft;
        internal QuickBooksAgent.Windows.UI.Controls.Table m_table;
        private System.Windows.Forms.Panel m_pnlTop2;
        internal System.Windows.Forms.TabPage m_tabAdditional;
        internal System.Windows.Forms.TabPage m_tabExpences;
        internal QuickBooksAgent.Windows.UI.Controls.CurrencyEdit m_curAmount;

    }
}
