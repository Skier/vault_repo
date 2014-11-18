namespace QuickBooksAgent.Windows.UI.CustomerOperations.Invoice
{
    partial class InvoiceLineView
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
            this.m_lblProductService = new System.Windows.Forms.Label();
            this.m_lblAmount = new System.Windows.Forms.Label();
            this.m_cmbProductService = new System.Windows.Forms.ComboBox();
            this.m_lblServiceDate = new System.Windows.Forms.Label();
            this.m_dtpServiceDate = new QuickBooksAgent.Windows.UI.Controls.NullableDateTimePicker();
            this.m_txtDescription = new System.Windows.Forms.TextBox();
            this.m_lblDescription = new System.Windows.Forms.Label();
            this.m_txtQty = new System.Windows.Forms.TextBox();
            this.m_lblQty = new System.Windows.Forms.Label();
            this.m_lblRate = new System.Windows.Forms.Label();
            this.m_txtRate = new System.Windows.Forms.TextBox();
            this.m_chkRateAsPercent = new System.Windows.Forms.CheckBox();
            this.m_chkTax = new System.Windows.Forms.CheckBox();
            this.m_curAmount = new QuickBooksAgent.Windows.UI.Controls.CurrencyEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblProductService
            // 
            this.m_lblProductService.Location = new System.Drawing.Point(1, 30);
            this.m_lblProductService.Name = "m_lblProductService";
            this.m_lblProductService.Size = new System.Drawing.Size(95, 20);
            this.m_lblProductService.Text = "Product/Service";
            this.m_lblProductService.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblAmount
            // 
            this.m_lblAmount.Location = new System.Drawing.Point(41, 131);
            this.m_lblAmount.Name = "m_lblAmount";
            this.m_lblAmount.Size = new System.Drawing.Size(100, 20);
            this.m_lblAmount.Text = "Amount";
            this.m_lblAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_cmbProductService
            // 
            this.m_cmbProductService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbProductService.Location = new System.Drawing.Point(99, 28);
            this.m_cmbProductService.Name = "m_cmbProductService";
            this.m_cmbProductService.Size = new System.Drawing.Size(136, 22);
            this.m_cmbProductService.TabIndex = 3;
            // 
            // m_lblServiceDate
            // 
            this.m_lblServiceDate.Location = new System.Drawing.Point(9, 6);
            this.m_lblServiceDate.Name = "m_lblServiceDate";
            this.m_lblServiceDate.Size = new System.Drawing.Size(86, 20);
            this.m_lblServiceDate.Text = "Service Date";
            this.m_lblServiceDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_dtpServiceDate
            // 
            this.m_dtpServiceDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpServiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpServiceDate.Location = new System.Drawing.Point(99, 3);
            this.m_dtpServiceDate.Name = "m_dtpServiceDate";
            this.m_dtpServiceDate.Size = new System.Drawing.Size(136, 22);
            this.m_dtpServiceDate.TabIndex = 9;
            // 
            // m_txtDescription
            // 
            this.m_txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDescription.Location = new System.Drawing.Point(99, 53);
            this.m_txtDescription.MaxLength = 4000;
            this.m_txtDescription.Name = "m_txtDescription";
            this.m_txtDescription.Size = new System.Drawing.Size(136, 21);
            this.m_txtDescription.TabIndex = 10;
            // 
            // m_lblDescription
            // 
            this.m_lblDescription.Location = new System.Drawing.Point(25, 54);
            this.m_lblDescription.Name = "m_lblDescription";
            this.m_lblDescription.Size = new System.Drawing.Size(71, 20);
            this.m_lblDescription.Text = "Description";
            this.m_lblDescription.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_txtQty
            // 
            this.m_txtQty.Location = new System.Drawing.Point(33, 77);
            this.m_txtQty.Name = "m_txtQty";
            this.m_txtQty.Size = new System.Drawing.Size(66, 21);
            this.m_txtQty.TabIndex = 13;
            // 
            // m_lblQty
            // 
            this.m_lblQty.Location = new System.Drawing.Point(1, 78);
            this.m_lblQty.Name = "m_lblQty";
            this.m_lblQty.Size = new System.Drawing.Size(26, 20);
            this.m_lblQty.Text = "Qty";
            this.m_lblQty.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblRate
            // 
            this.m_lblRate.Location = new System.Drawing.Point(107, 79);
            this.m_lblRate.Name = "m_lblRate";
            this.m_lblRate.Size = new System.Drawing.Size(35, 20);
            this.m_lblRate.Text = "Rate";
            this.m_lblRate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_txtRate
            // 
            this.m_txtRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtRate.Location = new System.Drawing.Point(146, 77);
            this.m_txtRate.Name = "m_txtRate";
            this.m_txtRate.Size = new System.Drawing.Size(89, 21);
            this.m_txtRate.TabIndex = 17;
            // 
            // m_chkRateAsPercent
            // 
            this.m_chkRateAsPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkRateAsPercent.Location = new System.Drawing.Point(1, 1);
            this.m_chkRateAsPercent.Name = "m_chkRateAsPercent";
            this.m_chkRateAsPercent.Size = new System.Drawing.Size(114, 20);
            this.m_chkRateAsPercent.TabIndex = 19;
            this.m_chkRateAsPercent.Text = "Rate as percent";
            // 
            // m_chkTax
            // 
            this.m_chkTax.Location = new System.Drawing.Point(1, 1);
            this.m_chkTax.Name = "m_chkTax";
            this.m_chkTax.Size = new System.Drawing.Size(69, 20);
            this.m_chkTax.TabIndex = 20;
            this.m_chkTax.Text = "Tax";
            // 
            // m_curAmount
            // 
            this.m_curAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curAmount.Location = new System.Drawing.Point(146, 129);
            this.m_curAmount.Name = "m_curAmount";
            this.m_curAmount.IsAllowNegative = true;
            this.m_curAmount.Size = new System.Drawing.Size(89, 21);
            this.m_curAmount.TabIndex = 21;
            this.m_curAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkTax);
            this.panel1.Location = new System.Drawing.Point(2, 101);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(88, 23);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_chkRateAsPercent);
            this.panel2.Location = new System.Drawing.Point(118, 101);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(120, 23);
            // 
            // InvoiceLineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_curAmount);
            this.Controls.Add(this.m_lblRate);
            this.Controls.Add(this.m_txtRate);
            this.Controls.Add(this.m_lblQty);
            this.Controls.Add(this.m_txtQty);
            this.Controls.Add(this.m_lblDescription);
            this.Controls.Add(this.m_txtDescription);
            this.Controls.Add(this.m_dtpServiceDate);
            this.Controls.Add(this.m_lblServiceDate);
            this.Controls.Add(this.m_cmbProductService);
            this.Controls.Add(this.m_lblAmount);
            this.Controls.Add(this.m_lblProductService);
            this.Name = "InvoiceLineView";
            this.Size = new System.Drawing.Size(240, 294);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblProductService;
        private System.Windows.Forms.Label m_lblAmount;
        internal System.Windows.Forms.ComboBox m_cmbProductService;
        private System.Windows.Forms.Label m_lblServiceDate;
        internal QuickBooksAgent.Windows.UI.Controls.NullableDateTimePicker m_dtpServiceDate;
        private System.Windows.Forms.Label m_lblDescription;
        private System.Windows.Forms.Label m_lblQty;
        private System.Windows.Forms.Label m_lblRate;
        internal System.Windows.Forms.TextBox m_txtDescription;
        internal System.Windows.Forms.TextBox m_txtRate;
        internal System.Windows.Forms.CheckBox m_chkRateAsPercent;
        internal System.Windows.Forms.CheckBox m_chkTax;
        internal QuickBooksAgent.Windows.UI.Controls.CurrencyEdit m_curAmount;
        internal System.Windows.Forms.TextBox m_txtQty;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
