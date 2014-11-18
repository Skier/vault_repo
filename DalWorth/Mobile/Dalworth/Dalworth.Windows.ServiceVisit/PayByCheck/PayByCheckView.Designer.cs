using Dalworth.Controls;

namespace Dalworth.Windows.ServiceVisit.PayByCheck
{
    partial class PayByCheckView
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_lblAddress = new System.Windows.Forms.Label();
            this.m_txtAddress = new System.Windows.Forms.TextBox();
            this.m_txtFirstName = new System.Windows.Forms.TextBox();
            this.m_lblTaskType = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblVisitNumber = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_txtZip = new Dalworth.Controls.DigitsEdit();
            this.m_lblFirstName = new System.Windows.Forms.Label();
            this.m_lblCity = new System.Windows.Forms.Label();
            this.m_lblLastName = new System.Windows.Forms.Label();
            this.m_cmbState = new System.Windows.Forms.ComboBox();
            this.m_lblStateZip = new System.Windows.Forms.Label();
            this.m_txtCity = new System.Windows.Forms.TextBox();
            this.m_txtLastName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_lblAccountNumber = new System.Windows.Forms.Label();
            this.m_txtAccountNumber = new Dalworth.Controls.DigitsEdit();
            this.m_txtBankName = new System.Windows.Forms.TextBox();
            this.m_lblBankName = new System.Windows.Forms.Label();
            this.m_txtCompany = new System.Windows.Forms.TextBox();
            this.m_lblCompanyName = new System.Windows.Forms.Label();
            this.m_cmbAccountType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_lblAmountDue = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.m_txtBankRouteNumber = new Dalworth.Controls.DigitsEdit();
            this.m_lblCheckNumber = new System.Windows.Forms.Label();
            this.m_txtCheckNumber = new Dalworth.Controls.DigitsEdit();
            this.m_lblBankRouteNumber = new System.Windows.Forms.Label();
            this.m_tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.m_tabs.TabIndex = 61;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_lblAddress);
            this.tabPage1.Controls.Add(this.m_txtAddress);
            this.tabPage1.Controls.Add(this.m_txtFirstName);
            this.tabPage1.Controls.Add(this.m_lblTaskType);
            this.tabPage1.Controls.Add(this.m_lblCustomerName);
            this.tabPage1.Controls.Add(this.m_lblVisitNumber);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.m_txtZip);
            this.tabPage1.Controls.Add(this.m_lblFirstName);
            this.tabPage1.Controls.Add(this.m_lblCity);
            this.tabPage1.Controls.Add(this.m_lblLastName);
            this.tabPage1.Controls.Add(this.m_cmbState);
            this.tabPage1.Controls.Add(this.m_lblStateZip);
            this.tabPage1.Controls.Add(this.m_txtCity);
            this.tabPage1.Controls.Add(this.m_txtLastName);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(240, 245);
            this.tabPage1.Text = "Info";
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
            this.m_txtAddress.Text = "234 Some Street";
            // 
            // m_txtFirstName
            // 
            this.m_txtFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFirstName.Location = new System.Drawing.Point(70, 56);
            this.m_txtFirstName.Name = "m_txtFirstName";
            this.m_txtFirstName.Size = new System.Drawing.Size(167, 21);
            this.m_txtFirstName.TabIndex = 45;
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
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.Text = "Task Type";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 20);
            this.label6.Text = "Customer";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 20);
            this.label9.Text = "TKT";
            // 
            // m_txtZip
            // 
            this.m_txtZip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtZip.Location = new System.Drawing.Point(145, 152);
            this.m_txtZip.Name = "m_txtZip";
            this.m_txtZip.Size = new System.Drawing.Size(92, 21);
            this.m_txtZip.TabIndex = 53;
            this.m_txtZip.Text = "75025";
            // 
            // m_lblFirstName
            // 
            this.m_lblFirstName.ForeColor = System.Drawing.Color.Red;
            this.m_lblFirstName.Location = new System.Drawing.Point(3, 57);
            this.m_lblFirstName.Name = "m_lblFirstName";
            this.m_lblFirstName.Size = new System.Drawing.Size(76, 20);
            this.m_lblFirstName.Text = "First Name";
            // 
            // m_lblCity
            // 
            this.m_lblCity.ForeColor = System.Drawing.Color.Red;
            this.m_lblCity.Location = new System.Drawing.Point(4, 128);
            this.m_lblCity.Name = "m_lblCity";
            this.m_lblCity.Size = new System.Drawing.Size(63, 20);
            this.m_lblCity.Text = "City";
            // 
            // m_lblLastName
            // 
            this.m_lblLastName.ForeColor = System.Drawing.Color.Red;
            this.m_lblLastName.Location = new System.Drawing.Point(3, 81);
            this.m_lblLastName.Name = "m_lblLastName";
            this.m_lblLastName.Size = new System.Drawing.Size(64, 20);
            this.m_lblLastName.Text = "Last Name";
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
            this.m_cmbState.TabIndex = 51;
            // 
            // m_lblStateZip
            // 
            this.m_lblStateZip.ForeColor = System.Drawing.Color.Red;
            this.m_lblStateZip.Location = new System.Drawing.Point(3, 154);
            this.m_lblStateZip.Name = "m_lblStateZip";
            this.m_lblStateZip.Size = new System.Drawing.Size(64, 20);
            this.m_lblStateZip.Text = "State, Zip";
            // 
            // m_txtCity
            // 
            this.m_txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCity.Location = new System.Drawing.Point(70, 128);
            this.m_txtCity.Name = "m_txtCity";
            this.m_txtCity.Size = new System.Drawing.Size(167, 21);
            this.m_txtCity.TabIndex = 48;
            this.m_txtCity.Text = "Plano";
            // 
            // m_txtLastName
            // 
            this.m_txtLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtLastName.Location = new System.Drawing.Point(70, 80);
            this.m_txtLastName.Name = "m_txtLastName";
            this.m_txtLastName.Size = new System.Drawing.Size(167, 21);
            this.m_txtLastName.TabIndex = 47;
            this.m_txtLastName.Text = "Love";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_lblAccountNumber);
            this.tabPage2.Controls.Add(this.m_txtAccountNumber);
            this.tabPage2.Controls.Add(this.m_txtBankName);
            this.tabPage2.Controls.Add(this.m_lblBankName);
            this.tabPage2.Controls.Add(this.m_txtCompany);
            this.tabPage2.Controls.Add(this.m_lblCompanyName);
            this.tabPage2.Controls.Add(this.m_cmbAccountType);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.m_lblAmountDue);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.m_txtBankRouteNumber);
            this.tabPage2.Controls.Add(this.m_lblCheckNumber);
            this.tabPage2.Controls.Add(this.m_txtCheckNumber);
            this.tabPage2.Controls.Add(this.m_lblBankRouteNumber);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(240, 245);
            this.tabPage2.Text = "Payment";
            // 
            // m_lblAccountNumber
            // 
            this.m_lblAccountNumber.ForeColor = System.Drawing.Color.Red;
            this.m_lblAccountNumber.Location = new System.Drawing.Point(3, 76);
            this.m_lblAccountNumber.Name = "m_lblAccountNumber";
            this.m_lblAccountNumber.Size = new System.Drawing.Size(85, 20);
            this.m_lblAccountNumber.Text = "Account #";
            // 
            // m_txtAccountNumber
            // 
            this.m_txtAccountNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtAccountNumber.Location = new System.Drawing.Point(94, 75);
            this.m_txtAccountNumber.Name = "m_txtAccountNumber";
            this.m_txtAccountNumber.Size = new System.Drawing.Size(143, 21);
            this.m_txtAccountNumber.TabIndex = 68;
            // 
            // m_txtBankName
            // 
            this.m_txtBankName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtBankName.Location = new System.Drawing.Point(94, 51);
            this.m_txtBankName.Name = "m_txtBankName";
            this.m_txtBankName.Size = new System.Drawing.Size(143, 21);
            this.m_txtBankName.TabIndex = 65;
            // 
            // m_lblBankName
            // 
            this.m_lblBankName.ForeColor = System.Drawing.Color.Red;
            this.m_lblBankName.Location = new System.Drawing.Point(3, 51);
            this.m_lblBankName.Name = "m_lblBankName";
            this.m_lblBankName.Size = new System.Drawing.Size(85, 20);
            this.m_lblBankName.Text = "Bank Name";
            // 
            // m_txtCompany
            // 
            this.m_txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCompany.Location = new System.Drawing.Point(94, 27);
            this.m_txtCompany.Name = "m_txtCompany";
            this.m_txtCompany.Size = new System.Drawing.Size(143, 21);
            this.m_txtCompany.TabIndex = 63;
            // 
            // m_lblCompanyName
            // 
            this.m_lblCompanyName.ForeColor = System.Drawing.Color.Red;
            this.m_lblCompanyName.Location = new System.Drawing.Point(3, 27);
            this.m_lblCompanyName.Name = "m_lblCompanyName";
            this.m_lblCompanyName.Size = new System.Drawing.Size(85, 20);
            this.m_lblCompanyName.Text = "Company";
            // 
            // m_cmbAccountType
            // 
            this.m_cmbAccountType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbAccountType.Items.Add("Personal");
            this.m_cmbAccountType.Items.Add("Company");
            this.m_cmbAccountType.Location = new System.Drawing.Point(94, 2);
            this.m_cmbAccountType.Name = "m_cmbAccountType";
            this.m_cmbAccountType.Size = new System.Drawing.Size(143, 22);
            this.m_cmbAccountType.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.Text = "Account Type";
            // 
            // m_lblAmountDue
            // 
            this.m_lblAmountDue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAmountDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAmountDue.Location = new System.Drawing.Point(81, 147);
            this.m_lblAmountDue.Name = "m_lblAmountDue";
            this.m_lblAmountDue.Size = new System.Drawing.Size(153, 20);
            this.m_lblAmountDue.Text = "$15.00";
            this.m_lblAmountDue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(2, 147);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 20);
            this.label10.Text = "Amount Due";
            // 
            // m_txtBankRouteNumber
            // 
            this.m_txtBankRouteNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtBankRouteNumber.Location = new System.Drawing.Point(94, 123);
            this.m_txtBankRouteNumber.Name = "m_txtBankRouteNumber";
            this.m_txtBankRouteNumber.Size = new System.Drawing.Size(143, 21);
            this.m_txtBankRouteNumber.TabIndex = 54;
            // 
            // m_lblCheckNumber
            // 
            this.m_lblCheckNumber.ForeColor = System.Drawing.Color.Red;
            this.m_lblCheckNumber.Location = new System.Drawing.Point(2, 100);
            this.m_lblCheckNumber.Name = "m_lblCheckNumber";
            this.m_lblCheckNumber.Size = new System.Drawing.Size(86, 20);
            this.m_lblCheckNumber.Text = "Check #";
            // 
            // m_txtCheckNumber
            // 
            this.m_txtCheckNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCheckNumber.Location = new System.Drawing.Point(94, 99);
            this.m_txtCheckNumber.Name = "m_txtCheckNumber";
            this.m_txtCheckNumber.Size = new System.Drawing.Size(143, 21);
            this.m_txtCheckNumber.TabIndex = 53;
            // 
            // m_lblBankRouteNumber
            // 
            this.m_lblBankRouteNumber.ForeColor = System.Drawing.Color.Red;
            this.m_lblBankRouteNumber.Location = new System.Drawing.Point(2, 124);
            this.m_lblBankRouteNumber.Name = "m_lblBankRouteNumber";
            this.m_lblBankRouteNumber.Size = new System.Drawing.Size(86, 20);
            this.m_lblBankRouteNumber.Text = "Bank Route #";
            // 
            // PayByCheckView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tabs);
            this.Name = "PayByCheckView";
            this.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.TextBox m_txtFirstName;
        internal System.Windows.Forms.Label m_lblTaskType;
        internal System.Windows.Forms.Label m_lblCustomerName;
        internal System.Windows.Forms.Label m_lblVisitNumber;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label9;
        internal DigitsEdit m_txtZip;
        internal System.Windows.Forms.Label m_lblFirstName;
        internal System.Windows.Forms.Label m_lblCity;
        internal System.Windows.Forms.Label m_lblLastName;
        internal System.Windows.Forms.ComboBox m_cmbState;
        internal System.Windows.Forms.Label m_lblStateZip;
        internal System.Windows.Forms.TextBox m_txtCity;
        internal System.Windows.Forms.TextBox m_txtLastName;
        internal System.Windows.Forms.Label m_lblAmountDue;
        internal System.Windows.Forms.Label label10;
        internal DigitsEdit m_txtBankRouteNumber;
        internal System.Windows.Forms.Label m_lblCheckNumber;
        internal DigitsEdit m_txtCheckNumber;
        internal System.Windows.Forms.Label m_lblBankRouteNumber;
        internal System.Windows.Forms.TabControl m_tabs;
        internal System.Windows.Forms.Label m_lblAddress;
        internal System.Windows.Forms.TextBox m_txtAddress;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label m_lblAccountNumber;
        internal DigitsEdit m_txtAccountNumber;
        internal System.Windows.Forms.Label m_lblBankName;
        internal System.Windows.Forms.TextBox m_txtCompany;
        internal System.Windows.Forms.Label m_lblCompanyName;
        internal System.Windows.Forms.ComboBox m_cmbAccountType;
        internal System.Windows.Forms.TextBox m_txtBankName;
    }
}
