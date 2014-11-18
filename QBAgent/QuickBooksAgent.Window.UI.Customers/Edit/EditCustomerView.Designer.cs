namespace QuickBooksAgent.Windows.UI.Customers.Edit
{
    partial class EditCustomerView
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
            this.m_txtPrintAs = new System.Windows.Forms.TextBox();
            this.m_menuEditCustomer = new System.Windows.Forms.MainMenu();
            this.m_pnlBottom = new System.Windows.Forms.Panel();
            this.m_tabs = new System.Windows.Forms.TabControl();
            this.m_tpInfo = new System.Windows.Forms.TabPage();
            this.m_txtCompanyName = new System.Windows.Forms.TextBox();
            this.m_txtDisplayAs = new System.Windows.Forms.TextBox();
            this.m_cmbSalutation = new System.Windows.Forms.ComboBox();
            this.m_cmbSuffix = new System.Windows.Forms.ComboBox();
            this.m_txtMiddleName = new System.Windows.Forms.TextBox();
            this.m_txtLastName = new System.Windows.Forms.TextBox();
            this.m_txtFirstName = new System.Windows.Forms.TextBox();
            this.m_lblPrintAs = new System.Windows.Forms.Label();
            this.m_lblDisplayAs = new System.Windows.Forms.Label();
            this.m_lblTitle = new System.Windows.Forms.Label();
            this.m_lblSuffix = new System.Windows.Forms.Label();
            this.m_lblMiddleName = new System.Windows.Forms.Label();
            this.m_lblFirstName = new System.Windows.Forms.Label();
            this.m_lblLastName = new System.Windows.Forms.Label();
            this.m_lblCompanyName = new System.Windows.Forms.Label();
            this.m_tpContactInfo = new System.Windows.Forms.TabPage();
            this.m_txtOther = new System.Windows.Forms.TextBox();
            this.m_txtPager = new System.Windows.Forms.TextBox();
            this.m_txtFax = new System.Windows.Forms.TextBox();
            this.m_txtPhone = new System.Windows.Forms.TextBox();
            this.m_txtEmail = new System.Windows.Forms.TextBox();
            this.m_txtMobile = new System.Windows.Forms.TextBox();
            this.m_lblOther = new System.Windows.Forms.Label();
            this.m_lblPager = new System.Windows.Forms.Label();
            this.m_lblFax = new System.Windows.Forms.Label();
            this.m_lblEmail = new System.Windows.Forms.Label();
            this.m_lblMobile = new System.Windows.Forms.Label();
            this.m_lblPhone = new System.Windows.Forms.Label();
            this.m_tpBillingAddress = new System.Windows.Forms.TabPage();
            this.m_txtState = new System.Windows.Forms.TextBox();
            this.m_txtCountry = new System.Windows.Forms.TextBox();
            this.m_txtCity = new System.Windows.Forms.TextBox();
            this.m_txtPostalCode = new System.Windows.Forms.TextBox();
            this.m_txtAddress = new System.Windows.Forms.TextBox();
            this.m_lblState = new System.Windows.Forms.Label();
            this.m_lblCountry = new System.Windows.Forms.Label();
            this.m_lblCity = new System.Windows.Forms.Label();
            this.m_lblZip = new System.Windows.Forms.Label();
            this.m_lblStreet = new System.Windows.Forms.Label();
            this.m_tpShippingAddress = new System.Windows.Forms.TabPage();
            this.m_txtShipCountry = new System.Windows.Forms.TextBox();
            this.m_txtShipCity = new System.Windows.Forms.TextBox();
            this.m_txtShipState = new System.Windows.Forms.TextBox();
            this.m_txtShipPostalCode = new System.Windows.Forms.TextBox();
            this.m_txtShipAddress = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chkSameAsBill = new System.Windows.Forms.CheckBox();
            this.m_lblShipCountry = new System.Windows.Forms.Label();
            this.m_lblShipCity = new System.Windows.Forms.Label();
            this.m_lblShipState = new System.Windows.Forms.Label();
            this.m_lblShipZip = new System.Windows.Forms.Label();
            this.m_lblShipStreet = new System.Windows.Forms.Label();
            this.m_tpTax = new System.Windows.Forms.TabPage();
            this.m_txtResale = new System.Windows.Forms.TextBox();
            this.m_lblResale = new System.Windows.Forms.Label();
            this.m_tpCustomer = new System.Windows.Forms.TabPage();
            this.m_curBalance = new QuickBooksAgent.Windows.UI.Controls.CurrencyEdit();
            this.m_cmbDelivery = new System.Windows.Forms.ComboBox();
            this.m_cmbTerms = new System.Windows.Forms.ComboBox();
            this.m_dtpBalanceDate = new System.Windows.Forms.DateTimePicker();
            this.m_lblDelivery = new System.Windows.Forms.Label();
            this.m_lblTerms = new System.Windows.Forms.Label();
            this.m_lblBalanceDate = new System.Windows.Forms.Label();
            this.m_lblBalance = new System.Windows.Forms.Label();
            this.m_pnlBottom.SuspendLayout();
            this.m_tabs.SuspendLayout();
            this.m_tpInfo.SuspendLayout();
            this.m_tpContactInfo.SuspendLayout();
            this.m_tpBillingAddress.SuspendLayout();
            this.m_tpShippingAddress.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_tpTax.SuspendLayout();
            this.m_tpCustomer.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtPrintAs
            // 
            this.m_txtPrintAs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtPrintAs.Location = new System.Drawing.Point(84, 119);
            this.m_txtPrintAs.MaxLength = 100;
            this.m_txtPrintAs.Name = "m_txtPrintAs";
            this.m_txtPrintAs.Size = new System.Drawing.Size(153, 21);
            this.m_txtPrintAs.TabIndex = 23;
            // 
            // m_pnlBottom
            // 
            this.m_pnlBottom.AutoScroll = true;
            this.m_pnlBottom.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.m_pnlBottom.Controls.Add(this.m_tabs);
            this.m_pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlBottom.Location = new System.Drawing.Point(0, 0);
            this.m_pnlBottom.Name = "m_pnlBottom";
            this.m_pnlBottom.Size = new System.Drawing.Size(240, 268);
            // 
            // m_tabs
            // 
            this.m_tabs.Controls.Add(this.m_tpInfo);
            this.m_tabs.Controls.Add(this.m_tpContactInfo);
            this.m_tabs.Controls.Add(this.m_tpBillingAddress);
            this.m_tabs.Controls.Add(this.m_tpShippingAddress);
            this.m_tabs.Controls.Add(this.m_tpTax);
            this.m_tabs.Controls.Add(this.m_tpCustomer);
            this.m_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabs.Location = new System.Drawing.Point(0, 0);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedIndex = 0;
            this.m_tabs.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.TabIndex = 0;
            // 
            // m_tpInfo
            // 
            this.m_tpInfo.AutoScroll = true;
            this.m_tpInfo.Controls.Add(this.m_txtCompanyName);
            this.m_tpInfo.Controls.Add(this.m_txtPrintAs);
            this.m_tpInfo.Controls.Add(this.m_txtDisplayAs);
            this.m_tpInfo.Controls.Add(this.m_cmbSalutation);
            this.m_tpInfo.Controls.Add(this.m_cmbSuffix);
            this.m_tpInfo.Controls.Add(this.m_txtMiddleName);
            this.m_tpInfo.Controls.Add(this.m_txtLastName);
            this.m_tpInfo.Controls.Add(this.m_txtFirstName);
            this.m_tpInfo.Controls.Add(this.m_lblPrintAs);
            this.m_tpInfo.Controls.Add(this.m_lblDisplayAs);
            this.m_tpInfo.Controls.Add(this.m_lblTitle);
            this.m_tpInfo.Controls.Add(this.m_lblSuffix);
            this.m_tpInfo.Controls.Add(this.m_lblMiddleName);
            this.m_tpInfo.Controls.Add(this.m_lblFirstName);
            this.m_tpInfo.Controls.Add(this.m_lblLastName);
            this.m_tpInfo.Controls.Add(this.m_lblCompanyName);
            this.m_tpInfo.Location = new System.Drawing.Point(0, 0);
            this.m_tpInfo.Name = "m_tpInfo";
            this.m_tpInfo.Size = new System.Drawing.Size(240, 245);
            this.m_tpInfo.Text = "Info";
            // 
            // m_txtCompanyName
            // 
            this.m_txtCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCompanyName.Location = new System.Drawing.Point(84, 142);
            this.m_txtCompanyName.MaxLength = 50;
            this.m_txtCompanyName.Name = "m_txtCompanyName";
            this.m_txtCompanyName.Size = new System.Drawing.Size(153, 21);
            this.m_txtCompanyName.TabIndex = 5;
            this.m_txtCompanyName.Tag = "";
            // 
            // m_txtDisplayAs
            // 
            this.m_txtDisplayAs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDisplayAs.Location = new System.Drawing.Point(84, 96);
            this.m_txtDisplayAs.MaxLength = 100;
            this.m_txtDisplayAs.Name = "m_txtDisplayAs";
            this.m_txtDisplayAs.Size = new System.Drawing.Size(153, 21);
            this.m_txtDisplayAs.TabIndex = 16;
            // 
            // m_cmbSalutation
            // 
            this.m_cmbSalutation.Location = new System.Drawing.Point(54, 3);
            this.m_cmbSalutation.Name = "m_cmbSalutation";
            this.m_cmbSalutation.Size = new System.Drawing.Size(63, 22);
            this.m_cmbSalutation.TabIndex = 0;
            // 
            // m_cmbSuffix
            // 
            this.m_cmbSuffix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbSuffix.Location = new System.Drawing.Point(170, 3);
            this.m_cmbSuffix.Name = "m_cmbSuffix";
            this.m_cmbSuffix.Size = new System.Drawing.Size(67, 22);
            this.m_cmbSuffix.TabIndex = 4;
            // 
            // m_txtMiddleName
            // 
            this.m_txtMiddleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtMiddleName.Location = new System.Drawing.Point(84, 50);
            this.m_txtMiddleName.MaxLength = 25;
            this.m_txtMiddleName.Name = "m_txtMiddleName";
            this.m_txtMiddleName.Size = new System.Drawing.Size(153, 21);
            this.m_txtMiddleName.TabIndex = 2;
            // 
            // m_txtLastName
            // 
            this.m_txtLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtLastName.Location = new System.Drawing.Point(84, 73);
            this.m_txtLastName.MaxLength = 25;
            this.m_txtLastName.Name = "m_txtLastName";
            this.m_txtLastName.Size = new System.Drawing.Size(153, 21);
            this.m_txtLastName.TabIndex = 3;
            // 
            // m_txtFirstName
            // 
            this.m_txtFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFirstName.Location = new System.Drawing.Point(84, 27);
            this.m_txtFirstName.MaxLength = 25;
            this.m_txtFirstName.Name = "m_txtFirstName";
            this.m_txtFirstName.Size = new System.Drawing.Size(153, 21);
            this.m_txtFirstName.TabIndex = 1;
            // 
            // m_lblPrintAs
            // 
            this.m_lblPrintAs.Location = new System.Drawing.Point(32, 120);
            this.m_lblPrintAs.Name = "m_lblPrintAs";
            this.m_lblPrintAs.Size = new System.Drawing.Size(58, 20);
            this.m_lblPrintAs.Text = "Print As";
            // 
            // m_lblDisplayAs
            // 
            this.m_lblDisplayAs.Location = new System.Drawing.Point(21, 97);
            this.m_lblDisplayAs.Name = "m_lblDisplayAs";
            this.m_lblDisplayAs.Size = new System.Drawing.Size(63, 20);
            this.m_lblDisplayAs.Text = "Display As";
            // 
            // m_lblTitle
            // 
            this.m_lblTitle.Location = new System.Drawing.Point(21, 5);
            this.m_lblTitle.Name = "m_lblTitle";
            this.m_lblTitle.Size = new System.Drawing.Size(34, 20);
            this.m_lblTitle.Text = "Title";
            // 
            // m_lblSuffix
            // 
            this.m_lblSuffix.Location = new System.Drawing.Point(133, 5);
            this.m_lblSuffix.Name = "m_lblSuffix";
            this.m_lblSuffix.Size = new System.Drawing.Size(36, 20);
            this.m_lblSuffix.Text = "Suffix";
            // 
            // m_lblMiddleName
            // 
            this.m_lblMiddleName.Location = new System.Drawing.Point(6, 51);
            this.m_lblMiddleName.Name = "m_lblMiddleName";
            this.m_lblMiddleName.Size = new System.Drawing.Size(81, 20);
            this.m_lblMiddleName.Text = "Middle Name";
            // 
            // m_lblFirstName
            // 
            this.m_lblFirstName.Location = new System.Drawing.Point(18, 28);
            this.m_lblFirstName.Name = "m_lblFirstName";
            this.m_lblFirstName.Size = new System.Drawing.Size(63, 20);
            this.m_lblFirstName.Text = "First Name";
            // 
            // m_lblLastName
            // 
            this.m_lblLastName.Location = new System.Drawing.Point(18, 73);
            this.m_lblLastName.Name = "m_lblLastName";
            this.m_lblLastName.Size = new System.Drawing.Size(63, 20);
            this.m_lblLastName.Text = "Last Name";
            // 
            // m_lblCompanyName
            // 
            this.m_lblCompanyName.Location = new System.Drawing.Point(24, 143);
            this.m_lblCompanyName.Name = "m_lblCompanyName";
            this.m_lblCompanyName.Size = new System.Drawing.Size(61, 20);
            this.m_lblCompanyName.Text = "Company";
            // 
            // m_tpContactInfo
            // 
            this.m_tpContactInfo.AutoScroll = true;
            this.m_tpContactInfo.Controls.Add(this.m_txtOther);
            this.m_tpContactInfo.Controls.Add(this.m_txtPager);
            this.m_tpContactInfo.Controls.Add(this.m_txtFax);
            this.m_tpContactInfo.Controls.Add(this.m_txtPhone);
            this.m_tpContactInfo.Controls.Add(this.m_txtEmail);
            this.m_tpContactInfo.Controls.Add(this.m_txtMobile);
            this.m_tpContactInfo.Controls.Add(this.m_lblOther);
            this.m_tpContactInfo.Controls.Add(this.m_lblPager);
            this.m_tpContactInfo.Controls.Add(this.m_lblFax);
            this.m_tpContactInfo.Controls.Add(this.m_lblEmail);
            this.m_tpContactInfo.Controls.Add(this.m_lblMobile);
            this.m_tpContactInfo.Controls.Add(this.m_lblPhone);
            this.m_tpContactInfo.Location = new System.Drawing.Point(0, 0);
            this.m_tpContactInfo.Name = "m_tpContactInfo";
            this.m_tpContactInfo.Size = new System.Drawing.Size(240, 245);
            this.m_tpContactInfo.Text = "Contacts";
            // 
            // m_txtOther
            // 
            this.m_txtOther.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtOther.Location = new System.Drawing.Point(68, 118);
            this.m_txtOther.MaxLength = 21;
            this.m_txtOther.Name = "m_txtOther";
            this.m_txtOther.Size = new System.Drawing.Size(169, 21);
            this.m_txtOther.TabIndex = 17;
            // 
            // m_txtPager
            // 
            this.m_txtPager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtPager.Location = new System.Drawing.Point(68, 72);
            this.m_txtPager.MaxLength = 21;
            this.m_txtPager.Name = "m_txtPager";
            this.m_txtPager.Size = new System.Drawing.Size(169, 21);
            this.m_txtPager.TabIndex = 15;
            // 
            // m_txtFax
            // 
            this.m_txtFax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFax.Location = new System.Drawing.Point(68, 49);
            this.m_txtFax.MaxLength = 21;
            this.m_txtFax.Name = "m_txtFax";
            this.m_txtFax.Size = new System.Drawing.Size(169, 21);
            this.m_txtFax.TabIndex = 14;
            // 
            // m_txtPhone
            // 
            this.m_txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtPhone.Location = new System.Drawing.Point(68, 3);
            this.m_txtPhone.MaxLength = 21;
            this.m_txtPhone.Name = "m_txtPhone";
            this.m_txtPhone.Size = new System.Drawing.Size(169, 21);
            this.m_txtPhone.TabIndex = 12;
            // 
            // m_txtEmail
            // 
            this.m_txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtEmail.Location = new System.Drawing.Point(68, 95);
            this.m_txtEmail.MaxLength = 100;
            this.m_txtEmail.Name = "m_txtEmail";
            this.m_txtEmail.Size = new System.Drawing.Size(169, 21);
            this.m_txtEmail.TabIndex = 16;
            // 
            // m_txtMobile
            // 
            this.m_txtMobile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtMobile.Location = new System.Drawing.Point(68, 26);
            this.m_txtMobile.MaxLength = 21;
            this.m_txtMobile.Name = "m_txtMobile";
            this.m_txtMobile.Size = new System.Drawing.Size(169, 21);
            this.m_txtMobile.TabIndex = 13;
            // 
            // m_lblOther
            // 
            this.m_lblOther.Location = new System.Drawing.Point(27, 119);
            this.m_lblOther.Name = "m_lblOther";
            this.m_lblOther.Size = new System.Drawing.Size(43, 20);
            this.m_lblOther.Text = "Other";
            // 
            // m_lblPager
            // 
            this.m_lblPager.Location = new System.Drawing.Point(28, 72);
            this.m_lblPager.Name = "m_lblPager";
            this.m_lblPager.Size = new System.Drawing.Size(40, 20);
            this.m_lblPager.Text = "Pager";
            // 
            // m_lblFax
            // 
            this.m_lblFax.Location = new System.Drawing.Point(41, 49);
            this.m_lblFax.Name = "m_lblFax";
            this.m_lblFax.Size = new System.Drawing.Size(33, 20);
            this.m_lblFax.Text = "Fax";
            // 
            // m_lblEmail
            // 
            this.m_lblEmail.Location = new System.Drawing.Point(31, 95);
            this.m_lblEmail.Name = "m_lblEmail";
            this.m_lblEmail.Size = new System.Drawing.Size(40, 20);
            this.m_lblEmail.Text = "Email";
            // 
            // m_lblMobile
            // 
            this.m_lblMobile.Location = new System.Drawing.Point(26, 27);
            this.m_lblMobile.Name = "m_lblMobile";
            this.m_lblMobile.Size = new System.Drawing.Size(43, 20);
            this.m_lblMobile.Text = "Mobile";
            // 
            // m_lblPhone
            // 
            this.m_lblPhone.Location = new System.Drawing.Point(25, 4);
            this.m_lblPhone.Name = "m_lblPhone";
            this.m_lblPhone.Size = new System.Drawing.Size(40, 20);
            this.m_lblPhone.Text = "Phone";
            // 
            // m_tpBillingAddress
            // 
            this.m_tpBillingAddress.AutoScroll = true;
            this.m_tpBillingAddress.Controls.Add(this.m_txtState);
            this.m_tpBillingAddress.Controls.Add(this.m_txtCountry);
            this.m_tpBillingAddress.Controls.Add(this.m_txtCity);
            this.m_tpBillingAddress.Controls.Add(this.m_txtPostalCode);
            this.m_tpBillingAddress.Controls.Add(this.m_txtAddress);
            this.m_tpBillingAddress.Controls.Add(this.m_lblState);
            this.m_tpBillingAddress.Controls.Add(this.m_lblCountry);
            this.m_tpBillingAddress.Controls.Add(this.m_lblCity);
            this.m_tpBillingAddress.Controls.Add(this.m_lblZip);
            this.m_tpBillingAddress.Controls.Add(this.m_lblStreet);
            this.m_tpBillingAddress.Location = new System.Drawing.Point(0, 0);
            this.m_tpBillingAddress.Name = "m_tpBillingAddress";
            this.m_tpBillingAddress.Size = new System.Drawing.Size(240, 245);
            this.m_tpBillingAddress.Text = "Billing Addr";
            // 
            // m_txtState
            // 
            this.m_txtState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtState.Location = new System.Drawing.Point(177, 49);
            this.m_txtState.MaxLength = 255;
            this.m_txtState.Name = "m_txtState";
            this.m_txtState.Size = new System.Drawing.Size(60, 21);
            this.m_txtState.TabIndex = 3;
            // 
            // m_txtCountry
            // 
            this.m_txtCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCountry.Location = new System.Drawing.Point(68, 72);
            this.m_txtCountry.MaxLength = 255;
            this.m_txtCountry.Name = "m_txtCountry";
            this.m_txtCountry.Size = new System.Drawing.Size(169, 21);
            this.m_txtCountry.TabIndex = 4;
            // 
            // m_txtCity
            // 
            this.m_txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCity.Location = new System.Drawing.Point(68, 26);
            this.m_txtCity.MaxLength = 255;
            this.m_txtCity.Name = "m_txtCity";
            this.m_txtCity.Size = new System.Drawing.Size(169, 21);
            this.m_txtCity.TabIndex = 1;
            // 
            // m_txtPostalCode
            // 
            this.m_txtPostalCode.Location = new System.Drawing.Point(68, 49);
            this.m_txtPostalCode.MaxLength = 30;
            this.m_txtPostalCode.Name = "m_txtPostalCode";
            this.m_txtPostalCode.Size = new System.Drawing.Size(60, 21);
            this.m_txtPostalCode.TabIndex = 2;
            // 
            // m_txtAddress
            // 
            this.m_txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtAddress.Location = new System.Drawing.Point(68, 3);
            this.m_txtAddress.MaxLength = 500;
            this.m_txtAddress.Name = "m_txtAddress";
            this.m_txtAddress.Size = new System.Drawing.Size(169, 21);
            this.m_txtAddress.TabIndex = 0;
            // 
            // m_lblState
            // 
            this.m_lblState.Location = new System.Drawing.Point(139, 50);
            this.m_lblState.Name = "m_lblState";
            this.m_lblState.Size = new System.Drawing.Size(34, 20);
            this.m_lblState.Text = "State";
            // 
            // m_lblCountry
            // 
            this.m_lblCountry.Location = new System.Drawing.Point(17, 73);
            this.m_lblCountry.Name = "m_lblCountry";
            this.m_lblCountry.Size = new System.Drawing.Size(59, 20);
            this.m_lblCountry.Text = "Country";
            // 
            // m_lblCity
            // 
            this.m_lblCity.Location = new System.Drawing.Point(40, 27);
            this.m_lblCity.Name = "m_lblCity";
            this.m_lblCity.Size = new System.Drawing.Size(40, 20);
            this.m_lblCity.Text = "City";
            // 
            // m_lblZip
            // 
            this.m_lblZip.Location = new System.Drawing.Point(44, 50);
            this.m_lblZip.Name = "m_lblZip";
            this.m_lblZip.Size = new System.Drawing.Size(40, 20);
            this.m_lblZip.Text = "Zip";
            // 
            // m_lblStreet
            // 
            this.m_lblStreet.Location = new System.Drawing.Point(25, 4);
            this.m_lblStreet.Name = "m_lblStreet";
            this.m_lblStreet.Size = new System.Drawing.Size(40, 20);
            this.m_lblStreet.Text = "Street";
            // 
            // m_tpShippingAddress
            // 
            this.m_tpShippingAddress.AutoScroll = true;
            this.m_tpShippingAddress.Controls.Add(this.m_txtShipCountry);
            this.m_tpShippingAddress.Controls.Add(this.m_txtShipCity);
            this.m_tpShippingAddress.Controls.Add(this.m_txtShipState);
            this.m_tpShippingAddress.Controls.Add(this.m_txtShipPostalCode);
            this.m_tpShippingAddress.Controls.Add(this.m_txtShipAddress);
            this.m_tpShippingAddress.Controls.Add(this.panel1);
            this.m_tpShippingAddress.Controls.Add(this.m_lblShipCountry);
            this.m_tpShippingAddress.Controls.Add(this.m_lblShipCity);
            this.m_tpShippingAddress.Controls.Add(this.m_lblShipState);
            this.m_tpShippingAddress.Controls.Add(this.m_lblShipZip);
            this.m_tpShippingAddress.Controls.Add(this.m_lblShipStreet);
            this.m_tpShippingAddress.Location = new System.Drawing.Point(0, 0);
            this.m_tpShippingAddress.Name = "m_tpShippingAddress";
            this.m_tpShippingAddress.Size = new System.Drawing.Size(240, 245);
            this.m_tpShippingAddress.Text = "Shipping Addr";
            // 
            // m_txtShipCountry
            // 
            this.m_txtShipCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtShipCountry.Location = new System.Drawing.Point(68, 72);
            this.m_txtShipCountry.MaxLength = 255;
            this.m_txtShipCountry.Name = "m_txtShipCountry";
            this.m_txtShipCountry.Size = new System.Drawing.Size(169, 21);
            this.m_txtShipCountry.TabIndex = 4;
            this.m_txtShipCountry.TabStop = false;
            // 
            // m_txtShipCity
            // 
            this.m_txtShipCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtShipCity.Location = new System.Drawing.Point(68, 26);
            this.m_txtShipCity.MaxLength = 255;
            this.m_txtShipCity.Name = "m_txtShipCity";
            this.m_txtShipCity.Size = new System.Drawing.Size(169, 21);
            this.m_txtShipCity.TabIndex = 1;
            this.m_txtShipCity.TabStop = false;
            // 
            // m_txtShipState
            // 
            this.m_txtShipState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtShipState.Location = new System.Drawing.Point(177, 49);
            this.m_txtShipState.MaxLength = 255;
            this.m_txtShipState.Name = "m_txtShipState";
            this.m_txtShipState.Size = new System.Drawing.Size(60, 21);
            this.m_txtShipState.TabIndex = 3;
            this.m_txtShipState.TabStop = false;
            // 
            // m_txtShipPostalCode
            // 
            this.m_txtShipPostalCode.Location = new System.Drawing.Point(68, 49);
            this.m_txtShipPostalCode.MaxLength = 30;
            this.m_txtShipPostalCode.Name = "m_txtShipPostalCode";
            this.m_txtShipPostalCode.Size = new System.Drawing.Size(60, 21);
            this.m_txtShipPostalCode.TabIndex = 2;
            this.m_txtShipPostalCode.TabStop = false;
            // 
            // m_txtShipAddress
            // 
            this.m_txtShipAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtShipAddress.Location = new System.Drawing.Point(68, 3);
            this.m_txtShipAddress.MaxLength = 500;
            this.m_txtShipAddress.Name = "m_txtShipAddress";
            this.m_txtShipAddress.Size = new System.Drawing.Size(169, 21);
            this.m_txtShipAddress.TabIndex = 0;
            this.m_txtShipAddress.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkSameAsBill);
            this.panel1.Location = new System.Drawing.Point(3, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(162, 31);
            // 
            // m_chkSameAsBill
            // 
            this.m_chkSameAsBill.Checked = true;
            this.m_chkSameAsBill.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkSameAsBill.Location = new System.Drawing.Point(3, 3);
            this.m_chkSameAsBill.Name = "m_chkSameAsBill";
            this.m_chkSameAsBill.Size = new System.Drawing.Size(158, 20);
            this.m_chkSameAsBill.TabIndex = 1;
            this.m_chkSameAsBill.TabStop = false;
            this.m_chkSameAsBill.Text = "Same as Billing Address";
            // 
            // m_lblShipCountry
            // 
            this.m_lblShipCountry.Location = new System.Drawing.Point(17, 73);
            this.m_lblShipCountry.Name = "m_lblShipCountry";
            this.m_lblShipCountry.Size = new System.Drawing.Size(59, 20);
            this.m_lblShipCountry.Text = "Country";
            // 
            // m_lblShipCity
            // 
            this.m_lblShipCity.Location = new System.Drawing.Point(40, 27);
            this.m_lblShipCity.Name = "m_lblShipCity";
            this.m_lblShipCity.Size = new System.Drawing.Size(40, 20);
            this.m_lblShipCity.Text = "City";
            // 
            // m_lblShipState
            // 
            this.m_lblShipState.Location = new System.Drawing.Point(139, 50);
            this.m_lblShipState.Name = "m_lblShipState";
            this.m_lblShipState.Size = new System.Drawing.Size(40, 20);
            this.m_lblShipState.Text = "State";
            // 
            // m_lblShipZip
            // 
            this.m_lblShipZip.Location = new System.Drawing.Point(44, 50);
            this.m_lblShipZip.Name = "m_lblShipZip";
            this.m_lblShipZip.Size = new System.Drawing.Size(40, 20);
            this.m_lblShipZip.Text = "Zip";
            // 
            // m_lblShipStreet
            // 
            this.m_lblShipStreet.Location = new System.Drawing.Point(25, 4);
            this.m_lblShipStreet.Name = "m_lblShipStreet";
            this.m_lblShipStreet.Size = new System.Drawing.Size(40, 20);
            this.m_lblShipStreet.Text = "Street";
            // 
            // m_tpTax
            // 
            this.m_tpTax.AutoScroll = true;
            this.m_tpTax.Controls.Add(this.m_txtResale);
            this.m_tpTax.Controls.Add(this.m_lblResale);
            this.m_tpTax.Location = new System.Drawing.Point(0, 0);
            this.m_tpTax.Name = "m_tpTax";
            this.m_tpTax.Size = new System.Drawing.Size(240, 245);
            this.m_tpTax.Text = "Tax";
            // 
            // m_txtResale
            // 
            this.m_txtResale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtResale.Location = new System.Drawing.Point(68, 3);
            this.m_txtResale.MaxLength = 21;
            this.m_txtResale.Name = "m_txtResale";
            this.m_txtResale.Size = new System.Drawing.Size(169, 21);
            this.m_txtResale.TabIndex = 14;
            // 
            // m_lblResale
            // 
            this.m_lblResale.Location = new System.Drawing.Point(12, 4);
            this.m_lblResale.Name = "m_lblResale";
            this.m_lblResale.Size = new System.Drawing.Size(59, 20);
            this.m_lblResale.Text = "Resale #";
            // 
            // m_tpCustomer
            // 
            this.m_tpCustomer.AutoScroll = true;
            this.m_tpCustomer.Controls.Add(this.m_curBalance);
            this.m_tpCustomer.Controls.Add(this.m_cmbDelivery);
            this.m_tpCustomer.Controls.Add(this.m_cmbTerms);
            this.m_tpCustomer.Controls.Add(this.m_dtpBalanceDate);
            this.m_tpCustomer.Controls.Add(this.m_lblDelivery);
            this.m_tpCustomer.Controls.Add(this.m_lblTerms);
            this.m_tpCustomer.Controls.Add(this.m_lblBalanceDate);
            this.m_tpCustomer.Controls.Add(this.m_lblBalance);
            this.m_tpCustomer.Location = new System.Drawing.Point(0, 0);
            this.m_tpCustomer.Name = "m_tpCustomer";
            this.m_tpCustomer.Size = new System.Drawing.Size(240, 245);
            this.m_tpCustomer.Text = "Customer Billing";
            // 
            // m_curBalance
            // 
            this.m_curBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curBalance.IsAllowNegative = true;
            this.m_curBalance.IsAllowNull = true;
            this.m_curBalance.Location = new System.Drawing.Point(108, 51);
            this.m_curBalance.MaxLength = 9;
            this.m_curBalance.Name = "m_curBalance";
            this.m_curBalance.Size = new System.Drawing.Size(129, 21);
            this.m_curBalance.TabIndex = 17;
            this.m_curBalance.Value = null;
            // 
            // m_cmbDelivery
            // 
            this.m_cmbDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbDelivery.Location = new System.Drawing.Point(68, 27);
            this.m_cmbDelivery.Name = "m_cmbDelivery";
            this.m_cmbDelivery.Size = new System.Drawing.Size(169, 22);
            this.m_cmbDelivery.TabIndex = 4;
            // 
            // m_cmbTerms
            // 
            this.m_cmbTerms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTerms.Location = new System.Drawing.Point(68, 3);
            this.m_cmbTerms.Name = "m_cmbTerms";
            this.m_cmbTerms.Size = new System.Drawing.Size(169, 22);
            this.m_cmbTerms.TabIndex = 1;
            // 
            // m_dtpBalanceDate
            // 
            this.m_dtpBalanceDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpBalanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpBalanceDate.Location = new System.Drawing.Point(108, 74);
            this.m_dtpBalanceDate.Name = "m_dtpBalanceDate";
            this.m_dtpBalanceDate.Size = new System.Drawing.Size(129, 22);
            this.m_dtpBalanceDate.TabIndex = 7;
            // 
            // m_lblDelivery
            // 
            this.m_lblDelivery.Location = new System.Drawing.Point(16, 28);
            this.m_lblDelivery.Name = "m_lblDelivery";
            this.m_lblDelivery.Size = new System.Drawing.Size(59, 20);
            this.m_lblDelivery.Text = "Delivery";
            // 
            // m_lblTerms
            // 
            this.m_lblTerms.Location = new System.Drawing.Point(24, 4);
            this.m_lblTerms.Name = "m_lblTerms";
            this.m_lblTerms.Size = new System.Drawing.Size(59, 20);
            this.m_lblTerms.Text = "Terms";
            // 
            // m_lblBalanceDate
            // 
            this.m_lblBalanceDate.Location = new System.Drawing.Point(26, 76);
            this.m_lblBalanceDate.Name = "m_lblBalanceDate";
            this.m_lblBalanceDate.Size = new System.Drawing.Size(80, 20);
            this.m_lblBalanceDate.Text = "Balance as of";
            // 
            // m_lblBalance
            // 
            this.m_lblBalance.Location = new System.Drawing.Point(57, 52);
            this.m_lblBalance.Name = "m_lblBalance";
            this.m_lblBalance.Size = new System.Drawing.Size(62, 20);
            this.m_lblBalance.Text = "Balance";
            // 
            // EditCustomerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.m_pnlBottom);
            this.Name = "EditCustomerView";
            this.Size = new System.Drawing.Size(240, 268);
            this.m_pnlBottom.ResumeLayout(false);
            this.m_tabs.ResumeLayout(false);
            this.m_tpInfo.ResumeLayout(false);
            this.m_tpContactInfo.ResumeLayout(false);
            this.m_tpBillingAddress.ResumeLayout(false);
            this.m_tpShippingAddress.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_tpTax.ResumeLayout(false);
            this.m_tpCustomer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.MainMenu m_menuEditCustomer;
        private System.Windows.Forms.Panel m_pnlBottom;
        internal System.Windows.Forms.TabControl m_tabs;
        internal System.Windows.Forms.TabPage m_tpInfo;
        private System.Windows.Forms.Label m_lblTitle;
        private System.Windows.Forms.Label m_lblSuffix;
        internal System.Windows.Forms.ComboBox m_cmbSalutation;
        internal System.Windows.Forms.ComboBox m_cmbSuffix;
        internal System.Windows.Forms.Label m_lblBalance;
        internal System.Windows.Forms.TextBox m_txtMiddleName;
        private System.Windows.Forms.Label m_lblMiddleName;
        internal System.Windows.Forms.TextBox m_txtLastName;
        internal System.Windows.Forms.TextBox m_txtFirstName;
        internal System.Windows.Forms.TextBox m_txtCompanyName;
        private System.Windows.Forms.Label m_lblCompanyName;
        internal System.Windows.Forms.Label m_lblFirstName;
        private System.Windows.Forms.Label m_lblLastName;
        internal System.Windows.Forms.TabPage m_tpContactInfo;
        internal System.Windows.Forms.TextBox m_txtOther;
        private System.Windows.Forms.Label m_lblOther;
        internal System.Windows.Forms.TextBox m_txtPager;
        private System.Windows.Forms.Label m_lblPager;
        internal System.Windows.Forms.TextBox m_txtFax;
        private System.Windows.Forms.Label m_lblFax;
        internal System.Windows.Forms.TextBox m_txtPhone;
        internal System.Windows.Forms.TextBox m_txtEmail;
        internal System.Windows.Forms.TextBox m_txtMobile;
        private System.Windows.Forms.Label m_lblEmail;
        private System.Windows.Forms.Label m_lblMobile;
        private System.Windows.Forms.Label m_lblPhone;
        internal System.Windows.Forms.TabPage m_tpBillingAddress;
        private System.Windows.Forms.Label m_lblState;
        internal System.Windows.Forms.TextBox m_txtCountry;
        private System.Windows.Forms.Label m_lblCountry;
        internal System.Windows.Forms.TextBox m_txtCity;
        private System.Windows.Forms.Label m_lblCity;
        internal System.Windows.Forms.TextBox m_txtState;
        internal System.Windows.Forms.TextBox m_txtPostalCode;
        private System.Windows.Forms.Label m_lblZip;
        internal System.Windows.Forms.TextBox m_txtAddress;
        private System.Windows.Forms.Label m_lblStreet;
        internal System.Windows.Forms.TabPage m_tpShippingAddress;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.CheckBox m_chkSameAsBill;
        internal System.Windows.Forms.TextBox m_txtShipCountry;
        internal System.Windows.Forms.Label m_lblShipCountry;
        internal System.Windows.Forms.TextBox m_txtShipCity;
        internal System.Windows.Forms.Label m_lblShipCity;
        internal System.Windows.Forms.TextBox m_txtShipState;
        internal System.Windows.Forms.Label m_lblShipState;
        internal System.Windows.Forms.TextBox m_txtShipPostalCode;
        internal System.Windows.Forms.Label m_lblShipZip;
        internal System.Windows.Forms.TextBox m_txtShipAddress;
        internal System.Windows.Forms.Label m_lblShipStreet;
        internal System.Windows.Forms.TabPage m_tpTax;
        internal System.Windows.Forms.TextBox m_txtResale;
        private System.Windows.Forms.Label m_lblResale;
        internal System.Windows.Forms.TabPage m_tpCustomer;
        private System.Windows.Forms.Label m_lblDelivery;
        internal System.Windows.Forms.ComboBox m_cmbDelivery;
        private System.Windows.Forms.Label m_lblTerms;
        internal System.Windows.Forms.ComboBox m_cmbTerms;
        internal System.Windows.Forms.Label m_lblBalanceDate;
        internal System.Windows.Forms.DateTimePicker m_dtpBalanceDate;
        internal System.Windows.Forms.TextBox m_txtDisplayAs;
        private System.Windows.Forms.Label m_lblDisplayAs;
        private System.Windows.Forms.Label m_lblPrintAs;
        internal System.Windows.Forms.TextBox m_txtPrintAs;
        internal QuickBooksAgent.Windows.UI.Controls.CurrencyEdit m_curBalance;

    }
}