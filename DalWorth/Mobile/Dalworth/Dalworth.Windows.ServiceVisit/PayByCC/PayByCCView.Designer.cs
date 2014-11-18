using Dalworth.Controls;

namespace Dalworth.Windows.ServiceVisit.PayByCC
{
    partial class PayByCCView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayByCCView));
            this.m_tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_txtFirstName = new System.Windows.Forms.TextBox();
            this.m_lblTaskType = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblVisitNumber = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.m_lblFirstName = new System.Windows.Forms.Label();
            this.m_txtLastName = new System.Windows.Forms.TextBox();
            this.m_lblLastName = new System.Windows.Forms.Label();
            this.m_lblStateZip = new System.Windows.Forms.Label();
            this.m_txtZip = new Dalworth.Controls.DigitsEdit();
            this.m_lblCity = new System.Windows.Forms.Label();
            this.m_txtCity = new System.Windows.Forms.TextBox();
            this.m_cmbState = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.m_rbnDiscover = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.m_rbnDinner = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.m_rbnMaster = new System.Windows.Forms.RadioButton();
            this.m_rbnVisa = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_lblAmountDue = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.m_cmbExpYear = new System.Windows.Forms.ComboBox();
            this.m_lblNumber = new System.Windows.Forms.Label();
            this.m_lblExpirationDate = new System.Windows.Forms.Label();
            this.m_lblCVV2 = new System.Windows.Forms.Label();
            this.m_cmbExpMonth = new System.Windows.Forms.ComboBox();
            this.m_txtCCNumber = new Dalworth.Controls.DigitsEdit();
            this.m_txtCVV2 = new Dalworth.Controls.DigitsEdit();
            this.m_lblAddress = new System.Windows.Forms.Label();
            this.m_txtAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmbCVV2Type = new System.Windows.Forms.ComboBox();
            this.m_tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabs
            // 
            this.m_tabs.Controls.Add(this.tabPage1);
            this.m_tabs.Controls.Add(this.tabPage2);
            this.m_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabs.Location = new System.Drawing.Point(0, 0);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedIndex = 0;
            this.m_tabs.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.TabIndex = 46;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_lblAddress);
            this.tabPage1.Controls.Add(this.m_txtAddress);
            this.tabPage1.Controls.Add(this.m_txtFirstName);
            this.tabPage1.Controls.Add(this.m_lblTaskType);
            this.tabPage1.Controls.Add(this.m_lblCustomerName);
            this.tabPage1.Controls.Add(this.m_lblVisitNumber);
            this.tabPage1.Controls.Add(this.label30);
            this.tabPage1.Controls.Add(this.label31);
            this.tabPage1.Controls.Add(this.label32);
            this.tabPage1.Controls.Add(this.m_lblFirstName);
            this.tabPage1.Controls.Add(this.m_txtLastName);
            this.tabPage1.Controls.Add(this.m_lblLastName);
            this.tabPage1.Controls.Add(this.m_lblStateZip);
            this.tabPage1.Controls.Add(this.m_txtZip);
            this.tabPage1.Controls.Add(this.m_lblCity);
            this.tabPage1.Controls.Add(this.m_txtCity);
            this.tabPage1.Controls.Add(this.m_cmbState);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(240, 245);
            this.tabPage1.Text = "Info";
            // 
            // m_txtFirstName
            // 
            this.m_txtFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFirstName.Location = new System.Drawing.Point(70, 56);
            this.m_txtFirstName.Name = "m_txtFirstName";
            this.m_txtFirstName.Size = new System.Drawing.Size(167, 21);
            this.m_txtFirstName.TabIndex = 47;
            this.m_txtFirstName.Text = "Rob";
            // 
            // m_lblTaskType
            // 
            this.m_lblTaskType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaskType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTaskType.Location = new System.Drawing.Point(71, 37);
            this.m_lblTaskType.Name = "m_lblTaskType";
            this.m_lblTaskType.Size = new System.Drawing.Size(166, 20);
            this.m_lblTaskType.Text = "Rug Pickup";
            this.m_lblTaskType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblCustomerName
            // 
            this.m_lblCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCustomerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblCustomerName.Location = new System.Drawing.Point(71, 20);
            this.m_lblCustomerName.Name = "m_lblCustomerName";
            this.m_lblCustomerName.Size = new System.Drawing.Size(166, 20);
            this.m_lblCustomerName.Text = "Love, Rob";
            this.m_lblCustomerName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblVisitNumber
            // 
            this.m_lblVisitNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblVisitNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblVisitNumber.Location = new System.Drawing.Point(71, 3);
            this.m_lblVisitNumber.Name = "m_lblVisitNumber";
            this.m_lblVisitNumber.Size = new System.Drawing.Size(166, 20);
            this.m_lblVisitNumber.Text = "1001";
            this.m_lblVisitNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(3, 37);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(62, 20);
            this.label30.Text = "Task Type";
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(3, 20);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(62, 20);
            this.label31.Text = "Customer";
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(3, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(62, 20);
            this.label32.Text = "TKT";
            // 
            // m_lblFirstName
            // 
            this.m_lblFirstName.ForeColor = System.Drawing.Color.Red;
            this.m_lblFirstName.Location = new System.Drawing.Point(3, 57);
            this.m_lblFirstName.Name = "m_lblFirstName";
            this.m_lblFirstName.Size = new System.Drawing.Size(76, 20);
            this.m_lblFirstName.Text = "First Name";
            // 
            // m_txtLastName
            // 
            this.m_txtLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtLastName.Location = new System.Drawing.Point(70, 80);
            this.m_txtLastName.Name = "m_txtLastName";
            this.m_txtLastName.Size = new System.Drawing.Size(167, 21);
            this.m_txtLastName.TabIndex = 15;
            this.m_txtLastName.Text = "Love";
            // 
            // m_lblLastName
            // 
            this.m_lblLastName.ForeColor = System.Drawing.Color.Red;
            this.m_lblLastName.Location = new System.Drawing.Point(3, 81);
            this.m_lblLastName.Name = "m_lblLastName";
            this.m_lblLastName.Size = new System.Drawing.Size(64, 20);
            this.m_lblLastName.Text = "Last Name";
            // 
            // m_lblStateZip
            // 
            this.m_lblStateZip.ForeColor = System.Drawing.Color.Red;
            this.m_lblStateZip.Location = new System.Drawing.Point(3, 154);
            this.m_lblStateZip.Name = "m_lblStateZip";
            this.m_lblStateZip.Size = new System.Drawing.Size(64, 20);
            this.m_lblStateZip.Text = "State, Zip";
            // 
            // m_txtZip
            // 
            this.m_txtZip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtZip.Location = new System.Drawing.Point(145, 152);
            this.m_txtZip.Name = "m_txtZip";
            this.m_txtZip.Size = new System.Drawing.Size(92, 21);
            this.m_txtZip.TabIndex = 23;
            this.m_txtZip.Text = "75025";
            // 
            // m_lblCity
            // 
            this.m_lblCity.ForeColor = System.Drawing.Color.Red;
            this.m_lblCity.Location = new System.Drawing.Point(4, 128);
            this.m_lblCity.Name = "m_lblCity";
            this.m_lblCity.Size = new System.Drawing.Size(63, 20);
            this.m_lblCity.Text = "City";
            // 
            // m_txtCity
            // 
            this.m_txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCity.Location = new System.Drawing.Point(70, 128);
            this.m_txtCity.Name = "m_txtCity";
            this.m_txtCity.Size = new System.Drawing.Size(167, 21);
            this.m_txtCity.TabIndex = 16;
            this.m_txtCity.Text = "Plano";
            // 
            // m_cmbState
            // 
            this.m_cmbState.Items.Add("AL");
            this.m_cmbState.Items.Add("AK");
            this.m_cmbState.Items.Add("AZ");
            this.m_cmbState.Items.Add("AR");
            this.m_cmbState.Items.Add("CA");
            this.m_cmbState.Items.Add("CO");
            this.m_cmbState.Items.Add("CT");
            this.m_cmbState.Items.Add("DC");
            this.m_cmbState.Items.Add("DE");
            this.m_cmbState.Items.Add("FL");
            this.m_cmbState.Items.Add("GA");
            this.m_cmbState.Items.Add("HI");
            this.m_cmbState.Items.Add("ID");
            this.m_cmbState.Items.Add("IL");
            this.m_cmbState.Items.Add("IN");
            this.m_cmbState.Items.Add("IA");
            this.m_cmbState.Items.Add("KS");
            this.m_cmbState.Items.Add("KY");
            this.m_cmbState.Items.Add("LA");
            this.m_cmbState.Items.Add("ME");
            this.m_cmbState.Items.Add("MD");
            this.m_cmbState.Items.Add("MA");
            this.m_cmbState.Items.Add("MI");
            this.m_cmbState.Items.Add("MN");
            this.m_cmbState.Items.Add("MS");
            this.m_cmbState.Items.Add("MO");
            this.m_cmbState.Items.Add("MT");
            this.m_cmbState.Items.Add("NE");
            this.m_cmbState.Items.Add("NV");
            this.m_cmbState.Items.Add("NH");
            this.m_cmbState.Items.Add("NJ");
            this.m_cmbState.Items.Add("NM");
            this.m_cmbState.Items.Add("NY");
            this.m_cmbState.Items.Add("NC");
            this.m_cmbState.Items.Add("ND");
            this.m_cmbState.Items.Add("OH");
            this.m_cmbState.Items.Add("OK");
            this.m_cmbState.Items.Add("OR");
            this.m_cmbState.Items.Add("PA");
            this.m_cmbState.Items.Add("RI");
            this.m_cmbState.Items.Add("SC");
            this.m_cmbState.Items.Add("SD");
            this.m_cmbState.Items.Add("TN");
            this.m_cmbState.Items.Add("TX");
            this.m_cmbState.Items.Add("UT");
            this.m_cmbState.Items.Add("VT");
            this.m_cmbState.Items.Add("VA");
            this.m_cmbState.Items.Add("WA");
            this.m_cmbState.Items.Add("WV");
            this.m_cmbState.Items.Add("WI");
            this.m_cmbState.Items.Add("WY");
            this.m_cmbState.Location = new System.Drawing.Point(70, 152);
            this.m_cmbState.Name = "m_cmbState";
            this.m_cmbState.Size = new System.Drawing.Size(72, 22);
            this.m_cmbState.TabIndex = 19;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_cmbCVV2Type);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.m_lblAmountDue);
            this.tabPage2.Controls.Add(this.label35);
            this.tabPage2.Controls.Add(this.m_cmbExpYear);
            this.tabPage2.Controls.Add(this.m_lblNumber);
            this.tabPage2.Controls.Add(this.m_lblExpirationDate);
            this.tabPage2.Controls.Add(this.m_lblCVV2);
            this.tabPage2.Controls.Add(this.m_cmbExpMonth);
            this.tabPage2.Controls.Add(this.m_txtCCNumber);
            this.tabPage2.Controls.Add(this.m_txtCVV2);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(240, 245);
            this.tabPage2.Text = "Payment";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pictureBox4);
            this.panel5.Controls.Add(this.m_rbnDiscover);
            this.panel5.Controls.Add(this.pictureBox3);
            this.panel5.Controls.Add(this.m_rbnDinner);
            this.panel5.Controls.Add(this.pictureBox2);
            this.panel5.Controls.Add(this.m_rbnMaster);
            this.panel5.Controls.Add(this.m_rbnVisa);
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Location = new System.Drawing.Point(4, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(232, 20);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(201, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(28, 18);
            // 
            // m_rbnDiscover
            // 
            this.m_rbnDiscover.Location = new System.Drawing.Point(180, 0);
            this.m_rbnDiscover.Name = "m_rbnDiscover";
            this.m_rbnDiscover.Size = new System.Drawing.Size(21, 20);
            this.m_rbnDiscover.TabIndex = 86;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(141, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(28, 18);
            // 
            // m_rbnDinner
            // 
            this.m_rbnDinner.Location = new System.Drawing.Point(120, 0);
            this.m_rbnDinner.Name = "m_rbnDinner";
            this.m_rbnDinner.Size = new System.Drawing.Size(21, 20);
            this.m_rbnDinner.TabIndex = 46;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(79, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(28, 18);
            // 
            // m_rbnMaster
            // 
            this.m_rbnMaster.Location = new System.Drawing.Point(58, 0);
            this.m_rbnMaster.Name = "m_rbnMaster";
            this.m_rbnMaster.Size = new System.Drawing.Size(21, 20);
            this.m_rbnMaster.TabIndex = 45;
            // 
            // m_rbnVisa
            // 
            this.m_rbnVisa.Location = new System.Drawing.Point(1, 0);
            this.m_rbnVisa.Name = "m_rbnVisa";
            this.m_rbnVisa.Size = new System.Drawing.Size(21, 20);
            this.m_rbnVisa.TabIndex = 41;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(21, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 18);
            // 
            // m_lblAmountDue
            // 
            this.m_lblAmountDue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAmountDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAmountDue.Location = new System.Drawing.Point(83, 123);
            this.m_lblAmountDue.Name = "m_lblAmountDue";
            this.m_lblAmountDue.Size = new System.Drawing.Size(153, 20);
            this.m_lblAmountDue.Text = "$15.00";
            this.m_lblAmountDue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(4, 123);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(77, 20);
            this.label35.Text = "Amount Due";
            // 
            // m_cmbExpYear
            // 
            this.m_cmbExpYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbExpYear.Items.Add("2007");
            this.m_cmbExpYear.Items.Add("2008");
            this.m_cmbExpYear.Items.Add("2009");
            this.m_cmbExpYear.Items.Add("2010");
            this.m_cmbExpYear.Items.Add("2011");
            this.m_cmbExpYear.Items.Add("2012");
            this.m_cmbExpYear.Items.Add("2013");
            this.m_cmbExpYear.Items.Add("2014");
            this.m_cmbExpYear.Items.Add("2015");
            this.m_cmbExpYear.Location = new System.Drawing.Point(145, 47);
            this.m_cmbExpYear.Name = "m_cmbExpYear";
            this.m_cmbExpYear.Size = new System.Drawing.Size(92, 22);
            this.m_cmbExpYear.TabIndex = 47;
            // 
            // m_lblNumber
            // 
            this.m_lblNumber.ForeColor = System.Drawing.Color.Red;
            this.m_lblNumber.Location = new System.Drawing.Point(4, 24);
            this.m_lblNumber.Name = "m_lblNumber";
            this.m_lblNumber.Size = new System.Drawing.Size(60, 20);
            this.m_lblNumber.Text = "Number";
            // 
            // m_lblExpirationDate
            // 
            this.m_lblExpirationDate.ForeColor = System.Drawing.Color.Red;
            this.m_lblExpirationDate.Location = new System.Drawing.Point(3, 48);
            this.m_lblExpirationDate.Name = "m_lblExpirationDate";
            this.m_lblExpirationDate.Size = new System.Drawing.Size(64, 20);
            this.m_lblExpirationDate.Text = "Exp Date";
            // 
            // m_lblCVV2
            // 
            this.m_lblCVV2.ForeColor = System.Drawing.Color.Red;
            this.m_lblCVV2.Location = new System.Drawing.Point(4, 98);
            this.m_lblCVV2.Name = "m_lblCVV2";
            this.m_lblCVV2.Size = new System.Drawing.Size(63, 20);
            this.m_lblCVV2.Text = "CVV2";
            // 
            // m_cmbExpMonth
            // 
            this.m_cmbExpMonth.Items.Add("01");
            this.m_cmbExpMonth.Items.Add("02");
            this.m_cmbExpMonth.Items.Add("03");
            this.m_cmbExpMonth.Items.Add("04");
            this.m_cmbExpMonth.Items.Add("05");
            this.m_cmbExpMonth.Items.Add("06");
            this.m_cmbExpMonth.Items.Add("07");
            this.m_cmbExpMonth.Items.Add("08");
            this.m_cmbExpMonth.Items.Add("09");
            this.m_cmbExpMonth.Items.Add("10");
            this.m_cmbExpMonth.Items.Add("11");
            this.m_cmbExpMonth.Items.Add("12");
            this.m_cmbExpMonth.Location = new System.Drawing.Point(70, 47);
            this.m_cmbExpMonth.Name = "m_cmbExpMonth";
            this.m_cmbExpMonth.Size = new System.Drawing.Size(72, 22);
            this.m_cmbExpMonth.TabIndex = 44;
            // 
            // m_txtCCNumber
            // 
            this.m_txtCCNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCCNumber.Location = new System.Drawing.Point(70, 23);
            this.m_txtCCNumber.Name = "m_txtCCNumber";
            this.m_txtCCNumber.Size = new System.Drawing.Size(167, 21);
            this.m_txtCCNumber.TabIndex = 42;
            // 
            // m_txtCVV2
            // 
            this.m_txtCVV2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCVV2.Location = new System.Drawing.Point(70, 97);
            this.m_txtCVV2.Name = "m_txtCVV2";
            this.m_txtCVV2.Size = new System.Drawing.Size(167, 21);
            this.m_txtCVV2.TabIndex = 43;
            // 
            // m_lblAddress
            // 
            this.m_lblAddress.ForeColor = System.Drawing.Color.Red;
            this.m_lblAddress.Location = new System.Drawing.Point(4, 104);
            this.m_lblAddress.Name = "m_lblAddress";
            this.m_lblAddress.Size = new System.Drawing.Size(63, 20);
            this.m_lblAddress.Text = "Address";
            // 
            // m_txtAddress
            // 
            this.m_txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtAddress.Location = new System.Drawing.Point(70, 104);
            this.m_txtAddress.Name = "m_txtAddress";
            this.m_txtAddress.Size = new System.Drawing.Size(167, 21);
            this.m_txtAddress.TabIndex = 59;
            this.m_txtAddress.Text = "1234 Some Street";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.Text = "CVV2 Type";
            // 
            // m_cmbCVV2Type
            // 
            this.m_cmbCVV2Type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbCVV2Type.Items.Add("Not Used");
            this.m_cmbCVV2Type.Items.Add("Used");
            this.m_cmbCVV2Type.Items.Add("Illegible");
            this.m_cmbCVV2Type.Items.Add("No CVV2 Imprinted");
            this.m_cmbCVV2Type.Location = new System.Drawing.Point(70, 72);
            this.m_cmbCVV2Type.Name = "m_cmbCVV2Type";
            this.m_cmbCVV2Type.Size = new System.Drawing.Size(167, 22);
            this.m_cmbCVV2Type.TabIndex = 53;
            // 
            // PayByCCView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tabs);
            this.Name = "PayByCCView";
            this.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.TextBox m_txtFirstName;
        internal System.Windows.Forms.Label m_lblTaskType;
        internal System.Windows.Forms.Label m_lblCustomerName;
        internal System.Windows.Forms.Label m_lblVisitNumber;
        internal System.Windows.Forms.Label label30;
        internal System.Windows.Forms.Label label31;
        internal System.Windows.Forms.Label label32;
        internal System.Windows.Forms.Label m_lblFirstName;
        internal System.Windows.Forms.TextBox m_txtLastName;
        internal System.Windows.Forms.Label m_lblLastName;
        internal System.Windows.Forms.Label m_lblStateZip;
        internal DigitsEdit m_txtZip;
        internal System.Windows.Forms.Label m_lblCity;
        internal System.Windows.Forms.TextBox m_txtCity;
        internal System.Windows.Forms.ComboBox m_cmbState;
        private System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Label m_lblAmountDue;
        internal System.Windows.Forms.Label label35;
        internal System.Windows.Forms.ComboBox m_cmbExpYear;
        internal System.Windows.Forms.Label m_lblNumber;
        internal System.Windows.Forms.Label m_lblExpirationDate;
        internal System.Windows.Forms.Label m_lblCVV2;
        internal System.Windows.Forms.ComboBox m_cmbExpMonth;
        internal DigitsEdit m_txtCCNumber;
        internal DigitsEdit m_txtCVV2;
        internal System.Windows.Forms.RadioButton m_rbnDiscover;
        internal System.Windows.Forms.RadioButton m_rbnDinner;
        internal System.Windows.Forms.RadioButton m_rbnMaster;
        internal System.Windows.Forms.RadioButton m_rbnVisa;
        internal System.Windows.Forms.TabControl m_tabs;
        internal System.Windows.Forms.Label m_lblAddress;
        internal System.Windows.Forms.TextBox m_txtAddress;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox m_cmbCVV2Type;
    }
}
