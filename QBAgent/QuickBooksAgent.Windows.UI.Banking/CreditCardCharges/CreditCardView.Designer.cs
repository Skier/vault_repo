namespace QuickBooksAgent.Windows.UI.Banking.CreditCardCharges
{
    partial class CreditCardView
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
            this.m_lblTransactionType = new System.Windows.Forms.Label();
            this.m_cmbTransactionType = new System.Windows.Forms.ComboBox();
            this.m_curAmount = new QuickBooksAgent.Windows.UI.Controls.CurrencyEdit();
            this.m_lblAmount = new System.Windows.Forms.Label();
            this.m_dtpDate = new System.Windows.Forms.DateTimePicker();
            this.m_lblDate = new System.Windows.Forms.Label();
            this.m_txtRefNumber = new System.Windows.Forms.TextBox();
            this.m_lblRefNumber = new System.Windows.Forms.Label();
            this.m_cmbPayee = new System.Windows.Forms.ComboBox();
            this.m_cmbPayeeType = new System.Windows.Forms.ComboBox();
            this.m_cmbCreditCard = new System.Windows.Forms.ComboBox();
            this.m_lblCreditCard = new System.Windows.Forms.Label();
            this.m_lblBalanceLabel = new System.Windows.Forms.Label();
            this.m_lblBalance = new System.Windows.Forms.Label();
            this.m_tabNotes = new System.Windows.Forms.TabPage();
            this.m_txtNotes = new System.Windows.Forms.TextBox();
            this.m_tabExpences = new System.Windows.Forms.TabPage();
            this.m_table = new QuickBooksAgent.Windows.UI.Controls.Table();
            this.m_pnlTop2 = new System.Windows.Forms.Panel();
            this.m_lblAmountLeftLabel = new System.Windows.Forms.Label();
            this.m_lblAmountLeft = new System.Windows.Forms.Label();
            this.m_tabs.SuspendLayout();
            this.m_tabGeneral.SuspendLayout();
            this.m_tabNotes.SuspendLayout();
            this.m_tabExpences.SuspendLayout();
            this.m_pnlTop2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabs
            // 
            this.m_tabs.Controls.Add(this.m_tabGeneral);
            this.m_tabs.Controls.Add(this.m_tabNotes);
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
            this.m_tabGeneral.Controls.Add(this.m_lblTransactionType);
            this.m_tabGeneral.Controls.Add(this.m_cmbTransactionType);
            this.m_tabGeneral.Controls.Add(this.m_curAmount);
            this.m_tabGeneral.Controls.Add(this.m_lblAmount);
            this.m_tabGeneral.Controls.Add(this.m_dtpDate);
            this.m_tabGeneral.Controls.Add(this.m_lblDate);
            this.m_tabGeneral.Controls.Add(this.m_txtRefNumber);
            this.m_tabGeneral.Controls.Add(this.m_lblRefNumber);
            this.m_tabGeneral.Controls.Add(this.m_cmbPayee);
            this.m_tabGeneral.Controls.Add(this.m_cmbPayeeType);
            this.m_tabGeneral.Controls.Add(this.m_cmbCreditCard);
            this.m_tabGeneral.Controls.Add(this.m_lblCreditCard);
            this.m_tabGeneral.Controls.Add(this.m_lblBalanceLabel);
            this.m_tabGeneral.Controls.Add(this.m_lblBalance);
            this.m_tabGeneral.Location = new System.Drawing.Point(0, 0);
            this.m_tabGeneral.Name = "m_tabGeneral";
            this.m_tabGeneral.Size = new System.Drawing.Size(240, 245);
            this.m_tabGeneral.Text = "General";
            // 
            // m_lblTransactionType
            // 
            this.m_lblTransactionType.Location = new System.Drawing.Point(4, 44);
            this.m_lblTransactionType.Name = "m_lblTransactionType";
            this.m_lblTransactionType.Size = new System.Drawing.Size(76, 20);
            this.m_lblTransactionType.Text = "Transaction";
            // 
            // m_cmbTransactionType
            // 
            this.m_cmbTransactionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTransactionType.Items.Add("Charge");
            this.m_cmbTransactionType.Items.Add("Credit");
            this.m_cmbTransactionType.Location = new System.Drawing.Point(86, 42);
            this.m_cmbTransactionType.Name = "m_cmbTransactionType";
            this.m_cmbTransactionType.Size = new System.Drawing.Size(151, 22);
            this.m_cmbTransactionType.TabIndex = 34;
            // 
            // m_curAmount
            // 
            this.m_curAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curAmount.IsAllowNegative = false;
            this.m_curAmount.IsAllowNull = false;
            this.m_curAmount.Location = new System.Drawing.Point(145, 137);
            this.m_curAmount.MaxLength = 9;
            this.m_curAmount.Name = "m_curAmount";
            this.m_curAmount.Size = new System.Drawing.Size(92, 21);
            this.m_curAmount.TabIndex = 26;
            this.m_curAmount.Value = null;
            // 
            // m_lblAmount
            // 
            this.m_lblAmount.Location = new System.Drawing.Point(93, 138);
            this.m_lblAmount.Name = "m_lblAmount";
            this.m_lblAmount.Size = new System.Drawing.Size(51, 20);
            this.m_lblAmount.Text = "Amount";
            // 
            // m_dtpDate
            // 
            this.m_dtpDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpDate.Location = new System.Drawing.Point(86, 113);
            this.m_dtpDate.Name = "m_dtpDate";
            this.m_dtpDate.Size = new System.Drawing.Size(151, 22);
            this.m_dtpDate.TabIndex = 17;
            // 
            // m_lblDate
            // 
            this.m_lblDate.Location = new System.Drawing.Point(53, 116);
            this.m_lblDate.Name = "m_lblDate";
            this.m_lblDate.Size = new System.Drawing.Size(32, 20);
            this.m_lblDate.Text = "Date";
            // 
            // m_txtRefNumber
            // 
            this.m_txtRefNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtRefNumber.Location = new System.Drawing.Point(86, 90);
            this.m_txtRefNumber.MaxLength = 21;
            this.m_txtRefNumber.Name = "m_txtRefNumber";
            this.m_txtRefNumber.Size = new System.Drawing.Size(151, 21);
            this.m_txtRefNumber.TabIndex = 14;
            // 
            // m_lblRefNumber
            // 
            this.m_lblRefNumber.Location = new System.Drawing.Point(48, 93);
            this.m_lblRefNumber.Name = "m_lblRefNumber";
            this.m_lblRefNumber.Size = new System.Drawing.Size(35, 20);
            this.m_lblRefNumber.Text = "Ref #";
            // 
            // m_cmbPayee
            // 
            this.m_cmbPayee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbPayee.DisplayMember = "FullName";
            this.m_cmbPayee.Location = new System.Drawing.Point(86, 66);
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
            this.m_cmbPayeeType.Location = new System.Drawing.Point(2, 66);
            this.m_cmbPayeeType.Name = "m_cmbPayeeType";
            this.m_cmbPayeeType.Size = new System.Drawing.Size(81, 22);
            this.m_cmbPayeeType.TabIndex = 5;
            // 
            // m_cmbCreditCard
            // 
            this.m_cmbCreditCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbCreditCard.DisplayMember = "FullName";
            this.m_cmbCreditCard.Location = new System.Drawing.Point(86, 2);
            this.m_cmbCreditCard.Name = "m_cmbCreditCard";
            this.m_cmbCreditCard.Size = new System.Drawing.Size(151, 22);
            this.m_cmbCreditCard.TabIndex = 4;
            this.m_cmbCreditCard.ValueMember = "FullName";
            // 
            // m_lblCreditCard
            // 
            this.m_lblCreditCard.Location = new System.Drawing.Point(4, 5);
            this.m_lblCreditCard.Name = "m_lblCreditCard";
            this.m_lblCreditCard.Size = new System.Drawing.Size(82, 20);
            this.m_lblCreditCard.Text = "Credit Card";
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
            // m_tabNotes
            // 
            this.m_tabNotes.AutoScroll = true;
            this.m_tabNotes.Controls.Add(this.m_txtNotes);
            this.m_tabNotes.Location = new System.Drawing.Point(0, 0);
            this.m_tabNotes.Name = "m_tabNotes";
            this.m_tabNotes.Size = new System.Drawing.Size(232, 242);
            this.m_tabNotes.Text = "Notes";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtNotes.Location = new System.Drawing.Point(0, 0);
            this.m_txtNotes.MaxLength = 4000;
            this.m_txtNotes.Multiline = true;
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Size = new System.Drawing.Size(232, 242);
            this.m_txtNotes.TabIndex = 24;
            // 
            // m_tabExpences
            // 
            this.m_tabExpences.AutoScroll = true;
            this.m_tabExpences.Controls.Add(this.m_table);
            this.m_tabExpences.Controls.Add(this.m_pnlTop2);
            this.m_tabExpences.Location = new System.Drawing.Point(0, 0);
            this.m_tabExpences.Name = "m_tabExpences";
            this.m_tabExpences.Size = new System.Drawing.Size(232, 242);
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
            this.m_table.Size = new System.Drawing.Size(232, 218);
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
            this.m_pnlTop2.Size = new System.Drawing.Size(232, 24);
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
            this.m_lblAmountLeft.Size = new System.Drawing.Size(116, 19);
            // 
            // CreditCardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tabs);
            this.Name = "CreditCardView";
            this.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.ResumeLayout(false);
            this.m_tabGeneral.ResumeLayout(false);
            this.m_tabNotes.ResumeLayout(false);
            this.m_tabExpences.ResumeLayout(false);
            this.m_pnlTop2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabControl m_tabs;
        internal System.Windows.Forms.TabPage m_tabGeneral;
        internal System.Windows.Forms.Label m_lblBalanceLabel;
        internal System.Windows.Forms.Label m_lblCreditCard;
        internal System.Windows.Forms.ComboBox m_cmbCreditCard;
        internal System.Windows.Forms.ComboBox m_cmbPayeeType;
        internal System.Windows.Forms.ComboBox m_cmbPayee;
        internal System.Windows.Forms.Label m_lblRefNumber;
        internal System.Windows.Forms.TextBox m_txtRefNumber;
        internal System.Windows.Forms.Label m_lblDate;
        internal System.Windows.Forms.Label m_lblAmount;
        internal System.Windows.Forms.DateTimePicker m_dtpDate;
        internal System.Windows.Forms.TextBox m_txtNotes;
        internal System.Windows.Forms.Label m_lblBalance;
        internal System.Windows.Forms.Label m_lblAmountLeftLabel;
        internal System.Windows.Forms.Label m_lblAmountLeft;
        internal QuickBooksAgent.Windows.UI.Controls.Table m_table;
        private System.Windows.Forms.Panel m_pnlTop2;
        internal System.Windows.Forms.TabPage m_tabNotes;
        internal System.Windows.Forms.TabPage m_tabExpences;
        internal QuickBooksAgent.Windows.UI.Controls.CurrencyEdit m_curAmount;
        internal System.Windows.Forms.Label m_lblTransactionType;
        internal System.Windows.Forms.ComboBox m_cmbTransactionType;        
    }
}
