using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.CustomerOperations.Invoice
{
    partial class InvoiceView
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
            this.m_pnlBottom = new System.Windows.Forms.Panel();
            this.m_tabs = new System.Windows.Forms.TabControl();
            this.m_tabInfo = new System.Windows.Forms.TabPage();
            this.m_txtMemo = new System.Windows.Forms.TextBox();
            this.m_lblMemo = new System.Windows.Forms.Label();
            this.m_pnlDelivery = new System.Windows.Forms.Panel();
            this.m_chkDelivery = new System.Windows.Forms.CheckBox();
            this.m_dtpShipDate = new QuickBooksAgent.Windows.UI.Controls.NullableDateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_btnClearShipDate = new System.Windows.Forms.Button();
            this.m_lblShipDate = new System.Windows.Forms.Label();
            this.m_lblDueDate = new System.Windows.Forms.Label();
            this.m_dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.m_lblTerms = new System.Windows.Forms.Label();
            this.m_cmbTerms = new System.Windows.Forms.ComboBox();
            this.m_lblInvoiceDate = new System.Windows.Forms.Label();
            this.m_dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.m_tabNewCharges = new System.Windows.Forms.TabPage();
            this.m_lblSubtotalNewChargesValue = new System.Windows.Forms.Label();
            this.m_lblSubtotalNewCharges = new System.Windows.Forms.Label();
            this.m_pnlTop = new System.Windows.Forms.Panel();
            this.m_table = new QuickBooksAgent.Windows.UI.Controls.Table();
            this.m_tabBillTo = new System.Windows.Forms.TabPage();
            this.m_txtBillToState = new System.Windows.Forms.TextBox();
            this.m_txtBillToCountry = new System.Windows.Forms.TextBox();
            this.m_txtBillToCity = new System.Windows.Forms.TextBox();
            this.m_txtBillToZip = new System.Windows.Forms.TextBox();
            this.m_txtBillToStreet = new System.Windows.Forms.TextBox();
            this.m_lblBillToState = new System.Windows.Forms.Label();
            this.m_lblBillToCountry = new System.Windows.Forms.Label();
            this.m_lblBillToCity = new System.Windows.Forms.Label();
            this.m_lblBillToZip = new System.Windows.Forms.Label();
            this.m_lblBillToStreet = new System.Windows.Forms.Label();
            this.m_tabShipTo = new System.Windows.Forms.TabPage();
            this.m_txtShipToState = new System.Windows.Forms.TextBox();
            this.m_txtShipToCountry = new System.Windows.Forms.TextBox();
            this.m_txtShipToCity = new System.Windows.Forms.TextBox();
            this.m_txtShipToZip = new System.Windows.Forms.TextBox();
            this.m_txtShipToStreet = new System.Windows.Forms.TextBox();
            this.m_lblShipToState = new System.Windows.Forms.Label();
            this.m_lblShipToCountry = new System.Windows.Forms.Label();
            this.m_lblShipToCity = new System.Windows.Forms.Label();
            this.m_lblShipToZip = new System.Windows.Forms.Label();
            this.m_lblShipToStreet = new System.Windows.Forms.Label();
            this.m_tabAdditional = new System.Windows.Forms.TabPage();
            this.m_txtDiscountPercent = new System.Windows.Forms.TextBox();
            this.m_lblBalanceDueAmount = new System.Windows.Forms.Label();
            this.m_lblBalanceDue = new System.Windows.Forms.Label();
            this.m_curShipping = new QuickBooksAgent.Windows.UI.Controls.CurrencyEdit();
            this.m_curTaxAmount = new QuickBooksAgent.Windows.UI.Controls.CurrencyEdit();
            this.m_txtTaxPercent = new System.Windows.Forms.TextBox();
            this.m_lblTax = new System.Windows.Forms.Label();
            this.m_lblTaxSubtotalAmount = new System.Windows.Forms.Label();
            this.m_lblTaxSubtotal = new System.Windows.Forms.Label();
            this.m_lblCalcTaxSubtotal = new System.Windows.Forms.Label();
            this.m_cmbCalcTaxSubtotal = new System.Windows.Forms.ComboBox();
            this.m_curDiscountAmount = new QuickBooksAgent.Windows.UI.Controls.CurrencyEdit();
            this.m_lblDiscountPercentSing = new System.Windows.Forms.Label();
            this.m_lblSubtotalValue = new System.Windows.Forms.Label();
            this.m_lblSubtotal = new System.Windows.Forms.Label();
            this.m_lblShipping = new System.Windows.Forms.Label();
            this.m_lblDiscount = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chkIsCustomerTaxable = new System.Windows.Forms.CheckBox();
            this.m_lblPercentSignTax = new System.Windows.Forms.Label();
            this.m_tabAccounts = new System.Windows.Forms.TabPage();
            this.m_cmbShippingAccount = new System.Windows.Forms.ComboBox();
            this.m_lblShippingAccount = new System.Windows.Forms.Label();
            this.m_cmbTaxAccount = new System.Windows.Forms.ComboBox();
            this.m_lblTaxAccount = new System.Windows.Forms.Label();
            this.m_cmbDiscountAccount = new System.Windows.Forms.ComboBox();
            this.m_lblDiscountAccount = new System.Windows.Forms.Label();
            this.m_pnlBottom.SuspendLayout();
            this.m_tabs.SuspendLayout();
            this.m_tabInfo.SuspendLayout();
            this.m_pnlDelivery.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_tabNewCharges.SuspendLayout();
            this.m_pnlTop.SuspendLayout();
            this.m_tabBillTo.SuspendLayout();
            this.m_tabShipTo.SuspendLayout();
            this.m_tabAdditional.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_tabAccounts.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlBottom
            // 
            this.m_pnlBottom.AutoScroll = true;
            this.m_pnlBottom.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.m_pnlBottom.Controls.Add(this.m_tabs);
            this.m_pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlBottom.Location = new System.Drawing.Point(0, 0);
            this.m_pnlBottom.Name = "m_pnlBottom";
            this.m_pnlBottom.Size = new System.Drawing.Size(240, 294);
            // 
            // m_tabs
            // 
            this.m_tabs.Controls.Add(this.m_tabInfo);
            this.m_tabs.Controls.Add(this.m_tabNewCharges);
            this.m_tabs.Controls.Add(this.m_tabBillTo);
            this.m_tabs.Controls.Add(this.m_tabShipTo);
            this.m_tabs.Controls.Add(this.m_tabAdditional);
            this.m_tabs.Controls.Add(this.m_tabAccounts);
            this.m_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabs.Location = new System.Drawing.Point(0, 0);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedIndex = 0;
            this.m_tabs.Size = new System.Drawing.Size(240, 294);
            this.m_tabs.TabIndex = 0;
            // 
            // m_tabInfo
            // 
            this.m_tabInfo.AutoScroll = true;
            this.m_tabInfo.Controls.Add(this.m_txtMemo);
            this.m_tabInfo.Controls.Add(this.m_lblMemo);
            this.m_tabInfo.Controls.Add(this.m_pnlDelivery);
            this.m_tabInfo.Controls.Add(this.m_dtpShipDate);
            this.m_tabInfo.Controls.Add(this.panel2);
            this.m_tabInfo.Controls.Add(this.m_lblShipDate);
            this.m_tabInfo.Controls.Add(this.m_lblDueDate);
            this.m_tabInfo.Controls.Add(this.m_dtpDueDate);
            this.m_tabInfo.Controls.Add(this.m_lblTerms);
            this.m_tabInfo.Controls.Add(this.m_cmbTerms);
            this.m_tabInfo.Controls.Add(this.m_lblInvoiceDate);
            this.m_tabInfo.Controls.Add(this.m_dtpInvoiceDate);
            this.m_tabInfo.Location = new System.Drawing.Point(0, 0);
            this.m_tabInfo.Name = "m_tabInfo";
            this.m_tabInfo.Size = new System.Drawing.Size(240, 271);
            this.m_tabInfo.Text = "Info";
            // 
            // m_txtMemo
            // 
            this.m_txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtMemo.Location = new System.Drawing.Point(99, 102);
            this.m_txtMemo.MaxLength = 4000;
            this.m_txtMemo.Name = "m_txtMemo";
            this.m_txtMemo.Size = new System.Drawing.Size(136, 21);
            this.m_txtMemo.TabIndex = 11;
            this.m_txtMemo.TabStop = false;
            // 
            // m_lblMemo
            // 
            this.m_lblMemo.Location = new System.Drawing.Point(29, 103);
            this.m_lblMemo.Name = "m_lblMemo";
            this.m_lblMemo.Size = new System.Drawing.Size(66, 20);
            this.m_lblMemo.Text = "Memo";
            this.m_lblMemo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_pnlDelivery
            // 
            this.m_pnlDelivery.Controls.Add(this.m_chkDelivery);
            this.m_pnlDelivery.Location = new System.Drawing.Point(47, 123);
            this.m_pnlDelivery.Name = "m_pnlDelivery";
            this.m_pnlDelivery.Size = new System.Drawing.Size(193, 22);
            // 
            // m_chkDelivery
            // 
            this.m_chkDelivery.Location = new System.Drawing.Point(1, 1);
            this.m_chkDelivery.Name = "m_chkDelivery";
            this.m_chkDelivery.Size = new System.Drawing.Size(193, 20);
            this.m_chkDelivery.TabIndex = 1;
            this.m_chkDelivery.TabStop = false;
            this.m_chkDelivery.Text = "Delivery information is printed  ";
            // 
            // m_dtpShipDate
            // 
            this.m_dtpShipDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpShipDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpShipDate.Location = new System.Drawing.Point(99, 77);
            this.m_dtpShipDate.Name = "m_dtpShipDate";
            this.m_dtpShipDate.Size = new System.Drawing.Size(90, 22);
            this.m_dtpShipDate.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_btnClearShipDate);
            this.panel2.Location = new System.Drawing.Point(188, 77);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(47, 25);
            // 
            // m_btnClearShipDate
            // 
            this.m_btnClearShipDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClearShipDate.Location = new System.Drawing.Point(4, 0);
            this.m_btnClearShipDate.Name = "m_btnClearShipDate";
            this.m_btnClearShipDate.Size = new System.Drawing.Size(43, 22);
            this.m_btnClearShipDate.TabIndex = 15;
            this.m_btnClearShipDate.Text = "Clear";
            // 
            // m_lblShipDate
            // 
            this.m_lblShipDate.Location = new System.Drawing.Point(12, 77);
            this.m_lblShipDate.Name = "m_lblShipDate";
            this.m_lblShipDate.Size = new System.Drawing.Size(83, 20);
            this.m_lblShipDate.Text = "Ship Date";
            this.m_lblShipDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblDueDate
            // 
            this.m_lblDueDate.Location = new System.Drawing.Point(15, 53);
            this.m_lblDueDate.Name = "m_lblDueDate";
            this.m_lblDueDate.Size = new System.Drawing.Size(80, 20);
            this.m_lblDueDate.Text = "Due Date";
            this.m_lblDueDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_dtpDueDate
            // 
            this.m_dtpDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpDueDate.Location = new System.Drawing.Point(99, 52);
            this.m_dtpDueDate.Name = "m_dtpDueDate";
            this.m_dtpDueDate.Size = new System.Drawing.Size(136, 22);
            this.m_dtpDueDate.TabIndex = 3;
            // 
            // m_lblTerms
            // 
            this.m_lblTerms.Location = new System.Drawing.Point(12, 28);
            this.m_lblTerms.Name = "m_lblTerms";
            this.m_lblTerms.Size = new System.Drawing.Size(83, 20);
            this.m_lblTerms.Text = "Terms";
            this.m_lblTerms.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_cmbTerms
            // 
            this.m_cmbTerms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTerms.Location = new System.Drawing.Point(99, 28);
            this.m_cmbTerms.Name = "m_cmbTerms";
            this.m_cmbTerms.Size = new System.Drawing.Size(136, 22);
            this.m_cmbTerms.TabIndex = 2;
            // 
            // m_lblInvoiceDate
            // 
            this.m_lblInvoiceDate.Location = new System.Drawing.Point(11, 5);
            this.m_lblInvoiceDate.Name = "m_lblInvoiceDate";
            this.m_lblInvoiceDate.Size = new System.Drawing.Size(84, 20);
            this.m_lblInvoiceDate.Text = "Invoice Date";
            this.m_lblInvoiceDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_dtpInvoiceDate
            // 
            this.m_dtpInvoiceDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpInvoiceDate.Location = new System.Drawing.Point(99, 3);
            this.m_dtpInvoiceDate.Name = "m_dtpInvoiceDate";
            this.m_dtpInvoiceDate.Size = new System.Drawing.Size(136, 22);
            this.m_dtpInvoiceDate.TabIndex = 0;
            // 
            // m_tabNewCharges
            // 
            this.m_tabNewCharges.AutoScroll = true;
            this.m_tabNewCharges.Controls.Add(this.m_lblSubtotalNewChargesValue);
            this.m_tabNewCharges.Controls.Add(this.m_lblSubtotalNewCharges);
            this.m_tabNewCharges.Controls.Add(this.m_pnlTop);
            this.m_tabNewCharges.Location = new System.Drawing.Point(0, 0);
            this.m_tabNewCharges.Name = "m_tabNewCharges";
            this.m_tabNewCharges.Size = new System.Drawing.Size(240, 271);
            this.m_tabNewCharges.Text = "New Charges";
            // 
            // m_lblSubtotalNewChargesValue
            // 
            this.m_lblSubtotalNewChargesValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblSubtotalNewChargesValue.Location = new System.Drawing.Point(156, 248);
            this.m_lblSubtotalNewChargesValue.Name = "m_lblSubtotalNewChargesValue";
            this.m_lblSubtotalNewChargesValue.Size = new System.Drawing.Size(78, 20);
            this.m_lblSubtotalNewChargesValue.Text = "0.00";
            this.m_lblSubtotalNewChargesValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblSubtotalNewCharges
            // 
            this.m_lblSubtotalNewCharges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblSubtotalNewCharges.Location = new System.Drawing.Point(4, 248);
            this.m_lblSubtotalNewCharges.Name = "m_lblSubtotalNewCharges";
            this.m_lblSubtotalNewCharges.Size = new System.Drawing.Size(146, 18);
            this.m_lblSubtotalNewCharges.Text = "Subtotal of new charges:";
            // 
            // m_pnlTop
            // 
            this.m_pnlTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlTop.Controls.Add(this.m_table);
            this.m_pnlTop.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTop.Name = "m_pnlTop";
            this.m_pnlTop.Size = new System.Drawing.Size(240, 243);
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
            this.m_table.DefaultLineAligment = System.Drawing.StringAlignment.Near;
            this.m_table.DefaultRowHeight = 20;
            this.m_table.DefaultTextAligment = System.Drawing.StringAlignment.Near;
            this.m_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_table.DrawGridBorder = true;
            this.m_table.FocusCellBackColor = System.Drawing.Color.Black;
            this.m_table.FocusCellForeColor = System.Drawing.Color.White;
            this.m_table.GreyOut = false;
            this.m_table.LeftHeader = false;
            this.m_table.Location = new System.Drawing.Point(0, 0);
            this.m_table.MultipleSelection = false;
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(240, 243);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = QuickBooksAgent.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 3;
            // 
            // m_tabBillTo
            // 
            this.m_tabBillTo.AutoScroll = true;
            this.m_tabBillTo.Controls.Add(this.m_txtBillToState);
            this.m_tabBillTo.Controls.Add(this.m_txtBillToCountry);
            this.m_tabBillTo.Controls.Add(this.m_txtBillToCity);
            this.m_tabBillTo.Controls.Add(this.m_txtBillToZip);
            this.m_tabBillTo.Controls.Add(this.m_txtBillToStreet);
            this.m_tabBillTo.Controls.Add(this.m_lblBillToState);
            this.m_tabBillTo.Controls.Add(this.m_lblBillToCountry);
            this.m_tabBillTo.Controls.Add(this.m_lblBillToCity);
            this.m_tabBillTo.Controls.Add(this.m_lblBillToZip);
            this.m_tabBillTo.Controls.Add(this.m_lblBillToStreet);
            this.m_tabBillTo.Location = new System.Drawing.Point(0, 0);
            this.m_tabBillTo.Name = "m_tabBillTo";
            this.m_tabBillTo.Size = new System.Drawing.Size(240, 271);
            this.m_tabBillTo.Text = "Bill To";
            // 
            // m_txtBillToState
            // 
            this.m_txtBillToState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtBillToState.Location = new System.Drawing.Point(175, 94);
            this.m_txtBillToState.MaxLength = 255;
            this.m_txtBillToState.Name = "m_txtBillToState";
            this.m_txtBillToState.Size = new System.Drawing.Size(60, 21);
            this.m_txtBillToState.TabIndex = 13;
            // 
            // m_txtBillToCountry
            // 
            this.m_txtBillToCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtBillToCountry.Location = new System.Drawing.Point(64, 117);
            this.m_txtBillToCountry.MaxLength = 255;
            this.m_txtBillToCountry.Name = "m_txtBillToCountry";
            this.m_txtBillToCountry.Size = new System.Drawing.Size(171, 21);
            this.m_txtBillToCountry.TabIndex = 14;
            // 
            // m_txtBillToCity
            // 
            this.m_txtBillToCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtBillToCity.Location = new System.Drawing.Point(64, 71);
            this.m_txtBillToCity.MaxLength = 255;
            this.m_txtBillToCity.Name = "m_txtBillToCity";
            this.m_txtBillToCity.Size = new System.Drawing.Size(171, 21);
            this.m_txtBillToCity.TabIndex = 11;
            // 
            // m_txtBillToZip
            // 
            this.m_txtBillToZip.Location = new System.Drawing.Point(64, 94);
            this.m_txtBillToZip.MaxLength = 30;
            this.m_txtBillToZip.Name = "m_txtBillToZip";
            this.m_txtBillToZip.Size = new System.Drawing.Size(60, 21);
            this.m_txtBillToZip.TabIndex = 12;
            // 
            // m_txtBillToStreet
            // 
            this.m_txtBillToStreet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtBillToStreet.Location = new System.Drawing.Point(64, 3);
            this.m_txtBillToStreet.MaxLength = 4000;
            this.m_txtBillToStreet.Multiline = true;
            this.m_txtBillToStreet.Name = "m_txtBillToStreet";
            this.m_txtBillToStreet.Size = new System.Drawing.Size(171, 66);
            this.m_txtBillToStreet.TabIndex = 10;
            // 
            // m_lblBillToState
            // 
            this.m_lblBillToState.Location = new System.Drawing.Point(135, 95);
            this.m_lblBillToState.Name = "m_lblBillToState";
            this.m_lblBillToState.Size = new System.Drawing.Size(34, 20);
            this.m_lblBillToState.Text = "State";
            // 
            // m_lblBillToCountry
            // 
            this.m_lblBillToCountry.Location = new System.Drawing.Point(13, 118);
            this.m_lblBillToCountry.Name = "m_lblBillToCountry";
            this.m_lblBillToCountry.Size = new System.Drawing.Size(59, 20);
            this.m_lblBillToCountry.Text = "Country";
            // 
            // m_lblBillToCity
            // 
            this.m_lblBillToCity.Location = new System.Drawing.Point(36, 72);
            this.m_lblBillToCity.Name = "m_lblBillToCity";
            this.m_lblBillToCity.Size = new System.Drawing.Size(40, 20);
            this.m_lblBillToCity.Text = "City";
            // 
            // m_lblBillToZip
            // 
            this.m_lblBillToZip.Location = new System.Drawing.Point(40, 95);
            this.m_lblBillToZip.Name = "m_lblBillToZip";
            this.m_lblBillToZip.Size = new System.Drawing.Size(40, 20);
            this.m_lblBillToZip.Text = "Zip";
            // 
            // m_lblBillToStreet
            // 
            this.m_lblBillToStreet.Location = new System.Drawing.Point(20, 4);
            this.m_lblBillToStreet.Name = "m_lblBillToStreet";
            this.m_lblBillToStreet.Size = new System.Drawing.Size(40, 20);
            this.m_lblBillToStreet.Text = "Street";
            // 
            // m_tabShipTo
            // 
            this.m_tabShipTo.AutoScroll = true;
            this.m_tabShipTo.Controls.Add(this.m_txtShipToState);
            this.m_tabShipTo.Controls.Add(this.m_txtShipToCountry);
            this.m_tabShipTo.Controls.Add(this.m_txtShipToCity);
            this.m_tabShipTo.Controls.Add(this.m_txtShipToZip);
            this.m_tabShipTo.Controls.Add(this.m_txtShipToStreet);
            this.m_tabShipTo.Controls.Add(this.m_lblShipToState);
            this.m_tabShipTo.Controls.Add(this.m_lblShipToCountry);
            this.m_tabShipTo.Controls.Add(this.m_lblShipToCity);
            this.m_tabShipTo.Controls.Add(this.m_lblShipToZip);
            this.m_tabShipTo.Controls.Add(this.m_lblShipToStreet);
            this.m_tabShipTo.Location = new System.Drawing.Point(0, 0);
            this.m_tabShipTo.Name = "m_tabShipTo";
            this.m_tabShipTo.Size = new System.Drawing.Size(240, 271);
            this.m_tabShipTo.Text = "Ship To";
            // 
            // m_txtShipToState
            // 
            this.m_txtShipToState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtShipToState.Location = new System.Drawing.Point(175, 94);
            this.m_txtShipToState.MaxLength = 255;
            this.m_txtShipToState.Name = "m_txtShipToState";
            this.m_txtShipToState.Size = new System.Drawing.Size(60, 21);
            this.m_txtShipToState.TabIndex = 23;
            // 
            // m_txtShipToCountry
            // 
            this.m_txtShipToCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtShipToCountry.Location = new System.Drawing.Point(64, 117);
            this.m_txtShipToCountry.MaxLength = 255;
            this.m_txtShipToCountry.Name = "m_txtShipToCountry";
            this.m_txtShipToCountry.Size = new System.Drawing.Size(171, 21);
            this.m_txtShipToCountry.TabIndex = 24;
            // 
            // m_txtShipToCity
            // 
            this.m_txtShipToCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtShipToCity.Location = new System.Drawing.Point(64, 71);
            this.m_txtShipToCity.MaxLength = 255;
            this.m_txtShipToCity.Name = "m_txtShipToCity";
            this.m_txtShipToCity.Size = new System.Drawing.Size(171, 21);
            this.m_txtShipToCity.TabIndex = 21;
            // 
            // m_txtShipToZip
            // 
            this.m_txtShipToZip.Location = new System.Drawing.Point(64, 94);
            this.m_txtShipToZip.MaxLength = 30;
            this.m_txtShipToZip.Name = "m_txtShipToZip";
            this.m_txtShipToZip.Size = new System.Drawing.Size(60, 21);
            this.m_txtShipToZip.TabIndex = 22;
            // 
            // m_txtShipToStreet
            // 
            this.m_txtShipToStreet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtShipToStreet.Location = new System.Drawing.Point(64, 3);
            this.m_txtShipToStreet.MaxLength = 4000;
            this.m_txtShipToStreet.Multiline = true;
            this.m_txtShipToStreet.Name = "m_txtShipToStreet";
            this.m_txtShipToStreet.Size = new System.Drawing.Size(171, 66);
            this.m_txtShipToStreet.TabIndex = 20;
            // 
            // m_lblShipToState
            // 
            this.m_lblShipToState.Location = new System.Drawing.Point(135, 95);
            this.m_lblShipToState.Name = "m_lblShipToState";
            this.m_lblShipToState.Size = new System.Drawing.Size(34, 20);
            this.m_lblShipToState.Text = "State";
            // 
            // m_lblShipToCountry
            // 
            this.m_lblShipToCountry.Location = new System.Drawing.Point(13, 118);
            this.m_lblShipToCountry.Name = "m_lblShipToCountry";
            this.m_lblShipToCountry.Size = new System.Drawing.Size(59, 20);
            this.m_lblShipToCountry.Text = "Country";
            // 
            // m_lblShipToCity
            // 
            this.m_lblShipToCity.Location = new System.Drawing.Point(36, 72);
            this.m_lblShipToCity.Name = "m_lblShipToCity";
            this.m_lblShipToCity.Size = new System.Drawing.Size(40, 20);
            this.m_lblShipToCity.Text = "City";
            // 
            // m_lblShipToZip
            // 
            this.m_lblShipToZip.Location = new System.Drawing.Point(40, 95);
            this.m_lblShipToZip.Name = "m_lblShipToZip";
            this.m_lblShipToZip.Size = new System.Drawing.Size(40, 20);
            this.m_lblShipToZip.Text = "Zip";
            // 
            // m_lblShipToStreet
            // 
            this.m_lblShipToStreet.Location = new System.Drawing.Point(20, 4);
            this.m_lblShipToStreet.Name = "m_lblShipToStreet";
            this.m_lblShipToStreet.Size = new System.Drawing.Size(40, 20);
            this.m_lblShipToStreet.Text = "Street";
            // 
            // m_tabAdditional
            // 
            this.m_tabAdditional.AutoScroll = true;
            this.m_tabAdditional.Controls.Add(this.m_txtDiscountPercent);
            this.m_tabAdditional.Controls.Add(this.m_lblBalanceDueAmount);
            this.m_tabAdditional.Controls.Add(this.m_lblBalanceDue);
            this.m_tabAdditional.Controls.Add(this.m_curShipping);
            this.m_tabAdditional.Controls.Add(this.m_curTaxAmount);
            this.m_tabAdditional.Controls.Add(this.m_txtTaxPercent);
            this.m_tabAdditional.Controls.Add(this.m_lblTax);
            this.m_tabAdditional.Controls.Add(this.m_lblTaxSubtotalAmount);
            this.m_tabAdditional.Controls.Add(this.m_lblTaxSubtotal);
            this.m_tabAdditional.Controls.Add(this.m_lblCalcTaxSubtotal);
            this.m_tabAdditional.Controls.Add(this.m_cmbCalcTaxSubtotal);
            this.m_tabAdditional.Controls.Add(this.m_curDiscountAmount);
            this.m_tabAdditional.Controls.Add(this.m_lblDiscountPercentSing);
            this.m_tabAdditional.Controls.Add(this.m_lblSubtotalValue);
            this.m_tabAdditional.Controls.Add(this.m_lblSubtotal);
            this.m_tabAdditional.Controls.Add(this.m_lblShipping);
            this.m_tabAdditional.Controls.Add(this.m_lblDiscount);
            this.m_tabAdditional.Controls.Add(this.panel1);
            this.m_tabAdditional.Controls.Add(this.m_lblPercentSignTax);
            this.m_tabAdditional.Location = new System.Drawing.Point(0, 0);
            this.m_tabAdditional.Name = "m_tabAdditional";
            this.m_tabAdditional.Size = new System.Drawing.Size(240, 271);
            this.m_tabAdditional.Text = "Totals";
            // 
            // m_txtDiscountPercent
            // 
            this.m_txtDiscountPercent.Location = new System.Drawing.Point(56, 20);
            this.m_txtDiscountPercent.MaxLength = 8;
            this.m_txtDiscountPercent.Name = "m_txtDiscountPercent";
            this.m_txtDiscountPercent.Size = new System.Drawing.Size(53, 21);
            this.m_txtDiscountPercent.TabIndex = 2;
            // 
            // m_lblBalanceDueAmount
            // 
            this.m_lblBalanceDueAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblBalanceDueAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblBalanceDueAmount.Location = new System.Drawing.Point(120, 151);
            this.m_lblBalanceDueAmount.Name = "m_lblBalanceDueAmount";
            this.m_lblBalanceDueAmount.Size = new System.Drawing.Size(113, 20);
            this.m_lblBalanceDueAmount.Text = "$0.00";
            this.m_lblBalanceDueAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblBalanceDue
            // 
            this.m_lblBalanceDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblBalanceDue.Location = new System.Drawing.Point(12, 151);
            this.m_lblBalanceDue.Name = "m_lblBalanceDue";
            this.m_lblBalanceDue.Size = new System.Drawing.Size(110, 20);
            this.m_lblBalanceDue.Text = "Balance Due:";
            this.m_lblBalanceDue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_curShipping
            // 
            this.m_curShipping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curShipping.IsAllowNegative = false;
            this.m_curShipping.IsAllowNull = true;
            this.m_curShipping.Location = new System.Drawing.Point(123, 127);
            this.m_curShipping.Name = "m_curShipping";
            this.m_curShipping.Size = new System.Drawing.Size(112, 21);
            this.m_curShipping.TabIndex = 35;
            this.m_curShipping.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // m_curTaxAmount
            // 
            this.m_curTaxAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curTaxAmount.Enabled = false;
            this.m_curTaxAmount.IsAllowNegative = true;
            this.m_curTaxAmount.IsAllowNull = true;
            this.m_curTaxAmount.Location = new System.Drawing.Point(123, 103);
            this.m_curTaxAmount.Name = "m_curTaxAmount";
            this.m_curTaxAmount.Size = new System.Drawing.Size(112, 21);
            this.m_curTaxAmount.TabIndex = 32;
            this.m_curTaxAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // m_txtTaxPercent
            // 
            this.m_txtTaxPercent.Enabled = false;
            this.m_txtTaxPercent.Location = new System.Drawing.Point(56, 103);
            this.m_txtTaxPercent.MaxLength = 8;
            this.m_txtTaxPercent.Name = "m_txtTaxPercent";
            this.m_txtTaxPercent.Size = new System.Drawing.Size(53, 21);
            this.m_txtTaxPercent.TabIndex = 31;
            // 
            // m_lblTax
            // 
            this.m_lblTax.Enabled = false;
            this.m_lblTax.Location = new System.Drawing.Point(16, 104);
            this.m_lblTax.Name = "m_lblTax";
            this.m_lblTax.Size = new System.Drawing.Size(37, 20);
            this.m_lblTax.Text = "Tax";
            this.m_lblTax.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblTaxSubtotalAmount
            // 
            this.m_lblTaxSubtotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaxSubtotalAmount.Enabled = false;
            this.m_lblTaxSubtotalAmount.Location = new System.Drawing.Point(120, 86);
            this.m_lblTaxSubtotalAmount.Name = "m_lblTaxSubtotalAmount";
            this.m_lblTaxSubtotalAmount.Size = new System.Drawing.Size(113, 20);
            this.m_lblTaxSubtotalAmount.Text = "$0.00";
            this.m_lblTaxSubtotalAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblTaxSubtotal
            // 
            this.m_lblTaxSubtotal.Enabled = false;
            this.m_lblTaxSubtotal.Location = new System.Drawing.Point(12, 86);
            this.m_lblTaxSubtotal.Name = "m_lblTaxSubtotal";
            this.m_lblTaxSubtotal.Size = new System.Drawing.Size(110, 20);
            this.m_lblTaxSubtotal.Text = "Tax Subtotal:";
            this.m_lblTaxSubtotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblCalcTaxSubtotal
            // 
            this.m_lblCalcTaxSubtotal.Enabled = false;
            this.m_lblCalcTaxSubtotal.Location = new System.Drawing.Point(11, 64);
            this.m_lblCalcTaxSubtotal.Name = "m_lblCalcTaxSubtotal";
            this.m_lblCalcTaxSubtotal.Size = new System.Drawing.Size(110, 20);
            this.m_lblCalcTaxSubtotal.Text = "Calc Tax Subtotal";
            this.m_lblCalcTaxSubtotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_cmbCalcTaxSubtotal
            // 
            this.m_cmbCalcTaxSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbCalcTaxSubtotal.Enabled = false;
            this.m_cmbCalcTaxSubtotal.Items.Add("After discount");
            this.m_cmbCalcTaxSubtotal.Items.Add("Before discount");
            this.m_cmbCalcTaxSubtotal.Location = new System.Drawing.Point(123, 62);
            this.m_cmbCalcTaxSubtotal.Name = "m_cmbCalcTaxSubtotal";
            this.m_cmbCalcTaxSubtotal.Size = new System.Drawing.Size(112, 22);
            this.m_cmbCalcTaxSubtotal.TabIndex = 22;
            // 
            // m_curDiscountAmount
            // 
            this.m_curDiscountAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curDiscountAmount.IsAllowNegative = false;
            this.m_curDiscountAmount.IsAllowNull = true;
            this.m_curDiscountAmount.Location = new System.Drawing.Point(123, 20);
            this.m_curDiscountAmount.Name = "m_curDiscountAmount";
            this.m_curDiscountAmount.Size = new System.Drawing.Size(112, 21);
            this.m_curDiscountAmount.TabIndex = 14;
            this.m_curDiscountAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // m_lblDiscountPercentSing
            // 
            this.m_lblDiscountPercentSing.Location = new System.Drawing.Point(106, 22);
            this.m_lblDiscountPercentSing.Name = "m_lblDiscountPercentSing";
            this.m_lblDiscountPercentSing.Size = new System.Drawing.Size(18, 20);
            this.m_lblDiscountPercentSing.Text = "%";
            this.m_lblDiscountPercentSing.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblSubtotalValue
            // 
            this.m_lblSubtotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblSubtotalValue.Location = new System.Drawing.Point(122, 3);
            this.m_lblSubtotalValue.Name = "m_lblSubtotalValue";
            this.m_lblSubtotalValue.Size = new System.Drawing.Size(113, 20);
            this.m_lblSubtotalValue.Text = "$0.00";
            this.m_lblSubtotalValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblSubtotal
            // 
            this.m_lblSubtotal.Location = new System.Drawing.Point(12, 3);
            this.m_lblSubtotal.Name = "m_lblSubtotal";
            this.m_lblSubtotal.Size = new System.Drawing.Size(110, 20);
            this.m_lblSubtotal.Text = "Subtotal:";
            this.m_lblSubtotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblShipping
            // 
            this.m_lblShipping.Location = new System.Drawing.Point(11, 129);
            this.m_lblShipping.Name = "m_lblShipping";
            this.m_lblShipping.Size = new System.Drawing.Size(110, 20);
            this.m_lblShipping.Text = "Shipping";
            this.m_lblShipping.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblDiscount
            // 
            this.m_lblDiscount.Location = new System.Drawing.Point(3, 22);
            this.m_lblDiscount.Name = "m_lblDiscount";
            this.m_lblDiscount.Size = new System.Drawing.Size(53, 20);
            this.m_lblDiscount.Text = "Discount";
            this.m_lblDiscount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkIsCustomerTaxable);
            this.panel1.Location = new System.Drawing.Point(55, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 23);
            // 
            // m_chkIsCustomerTaxable
            // 
            this.m_chkIsCustomerTaxable.Location = new System.Drawing.Point(1, 1);
            this.m_chkIsCustomerTaxable.Name = "m_chkIsCustomerTaxable";
            this.m_chkIsCustomerTaxable.Size = new System.Drawing.Size(141, 20);
            this.m_chkIsCustomerTaxable.TabIndex = 21;
            this.m_chkIsCustomerTaxable.Text = "Customer is Taxable";
            // 
            // m_lblPercentSignTax
            // 
            this.m_lblPercentSignTax.Enabled = false;
            this.m_lblPercentSignTax.Location = new System.Drawing.Point(106, 106);
            this.m_lblPercentSignTax.Name = "m_lblPercentSignTax";
            this.m_lblPercentSignTax.Size = new System.Drawing.Size(18, 20);
            this.m_lblPercentSignTax.Text = "%";
            this.m_lblPercentSignTax.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_tabAccounts
            // 
            this.m_tabAccounts.AutoScroll = true;
            this.m_tabAccounts.Controls.Add(this.m_cmbShippingAccount);
            this.m_tabAccounts.Controls.Add(this.m_lblShippingAccount);
            this.m_tabAccounts.Controls.Add(this.m_cmbTaxAccount);
            this.m_tabAccounts.Controls.Add(this.m_lblTaxAccount);
            this.m_tabAccounts.Controls.Add(this.m_cmbDiscountAccount);
            this.m_tabAccounts.Controls.Add(this.m_lblDiscountAccount);
            this.m_tabAccounts.Location = new System.Drawing.Point(0, 0);
            this.m_tabAccounts.Name = "m_tabAccounts";
            this.m_tabAccounts.Size = new System.Drawing.Size(240, 271);
            this.m_tabAccounts.Text = "Accounts";
            // 
            // m_cmbShippingAccount
            // 
            this.m_cmbShippingAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbShippingAccount.Location = new System.Drawing.Point(70, 53);
            this.m_cmbShippingAccount.Name = "m_cmbShippingAccount";
            this.m_cmbShippingAccount.Size = new System.Drawing.Size(165, 22);
            this.m_cmbShippingAccount.TabIndex = 6;
            // 
            // m_lblShippingAccount
            // 
            this.m_lblShippingAccount.Location = new System.Drawing.Point(13, 54);
            this.m_lblShippingAccount.Name = "m_lblShippingAccount";
            this.m_lblShippingAccount.Size = new System.Drawing.Size(53, 20);
            this.m_lblShippingAccount.Text = "Shipping";
            this.m_lblShippingAccount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_cmbTaxAccount
            // 
            this.m_cmbTaxAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTaxAccount.Location = new System.Drawing.Point(70, 28);
            this.m_cmbTaxAccount.Name = "m_cmbTaxAccount";
            this.m_cmbTaxAccount.Size = new System.Drawing.Size(165, 22);
            this.m_cmbTaxAccount.TabIndex = 3;
            // 
            // m_lblTaxAccount
            // 
            this.m_lblTaxAccount.Location = new System.Drawing.Point(25, 29);
            this.m_lblTaxAccount.Name = "m_lblTaxAccount";
            this.m_lblTaxAccount.Size = new System.Drawing.Size(41, 20);
            this.m_lblTaxAccount.Text = "Tax";
            this.m_lblTaxAccount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_cmbDiscountAccount
            // 
            this.m_cmbDiscountAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbDiscountAccount.Location = new System.Drawing.Point(70, 3);
            this.m_cmbDiscountAccount.Name = "m_cmbDiscountAccount";
            this.m_cmbDiscountAccount.Size = new System.Drawing.Size(165, 22);
            this.m_cmbDiscountAccount.TabIndex = 1;
            // 
            // m_lblDiscountAccount
            // 
            this.m_lblDiscountAccount.Location = new System.Drawing.Point(9, 5);
            this.m_lblDiscountAccount.Name = "m_lblDiscountAccount";
            this.m_lblDiscountAccount.Size = new System.Drawing.Size(57, 20);
            this.m_lblDiscountAccount.Text = "Discount";
            this.m_lblDiscountAccount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // InvoiceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.m_pnlBottom);
            this.Name = "InvoiceView";
            this.Size = new System.Drawing.Size(240, 294);
            this.m_pnlBottom.ResumeLayout(false);
            this.m_tabs.ResumeLayout(false);
            this.m_tabInfo.ResumeLayout(false);
            this.m_pnlDelivery.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.m_tabNewCharges.ResumeLayout(false);
            this.m_pnlTop.ResumeLayout(false);
            this.m_tabBillTo.ResumeLayout(false);
            this.m_tabShipTo.ResumeLayout(false);
            this.m_tabAdditional.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_tabAccounts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnlBottom;
        internal System.Windows.Forms.TabControl m_tabs;
        internal System.Windows.Forms.TabPage m_tabNewCharges;
        internal System.Windows.Forms.TabPage m_tabInfo;
        internal System.Windows.Forms.TabPage m_tabAdditional;
        internal System.Windows.Forms.DateTimePicker m_dtpInvoiceDate;
        private System.Windows.Forms.Label m_lblTerms;
        internal System.Windows.Forms.ComboBox m_cmbTerms;
        internal NullableDateTimePicker m_dtpShipDate;
        internal System.Windows.Forms.DateTimePicker m_dtpDueDate;
        private System.Windows.Forms.Label m_lblShipping;
        internal System.Windows.Forms.TextBox m_txtDiscountPercent;
        private System.Windows.Forms.Label m_lblDiscount;
        private System.Windows.Forms.Label m_lblInvoiceDate;
        private System.Windows.Forms.Label m_lblShipDate;
        private System.Windows.Forms.Label m_lblDueDate;
        private System.Windows.Forms.Panel m_pnlTop;
        internal QuickBooksAgent.Windows.UI.Controls.Table m_table;
        internal System.Windows.Forms.TabPage m_tabBillTo;
        internal System.Windows.Forms.TabPage m_tabShipTo;
        internal System.Windows.Forms.TextBox m_txtBillToState;
        internal System.Windows.Forms.TextBox m_txtBillToCountry;
        internal System.Windows.Forms.TextBox m_txtBillToCity;
        internal System.Windows.Forms.TextBox m_txtBillToZip;
        internal System.Windows.Forms.TextBox m_txtBillToStreet;
        private System.Windows.Forms.Label m_lblBillToState;
        private System.Windows.Forms.Label m_lblBillToCountry;
        private System.Windows.Forms.Label m_lblBillToCity;
        private System.Windows.Forms.Label m_lblBillToZip;
        private System.Windows.Forms.Label m_lblBillToStreet;
        internal System.Windows.Forms.TextBox m_txtShipToState;
        internal System.Windows.Forms.TextBox m_txtShipToCountry;
        internal System.Windows.Forms.TextBox m_txtShipToCity;
        internal System.Windows.Forms.TextBox m_txtShipToZip;
        internal System.Windows.Forms.TextBox m_txtShipToStreet;
        private System.Windows.Forms.Label m_lblShipToState;
        private System.Windows.Forms.Label m_lblShipToCountry;
        private System.Windows.Forms.Label m_lblShipToCity;
        private System.Windows.Forms.Label m_lblShipToZip;
        private System.Windows.Forms.Label m_lblShipToStreet;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Button m_btnClearShipDate;
        internal System.Windows.Forms.Label m_lblSubtotalNewChargesValue;
        private System.Windows.Forms.Label m_lblSubtotalNewCharges;
        private System.Windows.Forms.Panel m_pnlDelivery;
        internal System.Windows.Forms.CheckBox m_chkDelivery;
        internal System.Windows.Forms.TextBox m_txtMemo;
        private System.Windows.Forms.Label m_lblMemo;
        private System.Windows.Forms.Label m_lblSubtotal;
        private System.Windows.Forms.Label m_lblDiscountPercentSing;
        internal System.Windows.Forms.TextBox m_txtTaxPercent;
        private System.Windows.Forms.Label m_lblBalanceDue;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label m_lblSubtotalValue;
        internal CurrencyEdit m_curDiscountAmount;
        internal System.Windows.Forms.ComboBox m_cmbCalcTaxSubtotal;
        internal System.Windows.Forms.CheckBox m_chkIsCustomerTaxable;
        internal CurrencyEdit m_curShipping;
        internal CurrencyEdit m_curTaxAmount;
        internal System.Windows.Forms.Label m_lblTaxSubtotalAmount;
        internal System.Windows.Forms.Label m_lblBalanceDueAmount;
        internal System.Windows.Forms.Label m_lblCalcTaxSubtotal;
        internal System.Windows.Forms.Label m_lblPercentSignTax;
        internal System.Windows.Forms.Label m_lblTax;
        internal System.Windows.Forms.Label m_lblTaxSubtotal;
        internal System.Windows.Forms.TabPage m_tabAccounts;
        private System.Windows.Forms.Label m_lblDiscountAccount;
        internal System.Windows.Forms.ComboBox m_cmbDiscountAccount;
        internal System.Windows.Forms.ComboBox m_cmbShippingAccount;
        private System.Windows.Forms.Label m_lblShippingAccount;
        internal System.Windows.Forms.ComboBox m_cmbTaxAccount;
        private System.Windows.Forms.Label m_lblTaxAccount;

    }
}