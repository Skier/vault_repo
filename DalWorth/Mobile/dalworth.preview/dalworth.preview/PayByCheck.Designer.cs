namespace dalworth.preview
{
    partial class PayByCheck
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.m_menuCancel = new System.Windows.Forms.MenuItem();
            this.m_menuDone = new System.Windows.Forms.MenuItem();
            this.m_txtZip = new System.Windows.Forms.TextBox();
            this.m_lblCity = new System.Windows.Forms.Label();
            this.m_cmbState = new System.Windows.Forms.ComboBox();
            this.m_txtCity = new System.Windows.Forms.TextBox();
            this.m_txtLastName = new System.Windows.Forms.TextBox();
            this.m_lblStateZip = new System.Windows.Forms.Label();
            this.m_lblLastName = new System.Windows.Forms.Label();
            this.m_txtFirstName = new System.Windows.Forms.TextBox();
            this.m_lblFirstName = new System.Windows.Forms.Label();
            this.m_tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_lblJobType = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblTicketNumber = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_lblAmountDue = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.m_txtBankRouteNumber = new System.Windows.Forms.TextBox();
            this.m_lblCheckNumber = new System.Windows.Forms.Label();
            this.m_txtCheckNumber = new System.Windows.Forms.TextBox();
            this.m_lblBankRouteNumber = new System.Windows.Forms.Label();
            this.m_tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.m_menuDone);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.m_menuCancel);
            this.menuItem1.Text = "Menu";
            // 
            // m_menuCancel
            // 
            this.m_menuCancel.Text = "Cancel";
            this.m_menuCancel.Click += new System.EventHandler(this.OnCancelClick);
            // 
            // m_menuDone
            // 
            this.m_menuDone.Text = "Done";
            this.m_menuDone.Click += new System.EventHandler(this.OnDoneClick);
            // 
            // m_txtZip
            // 
            this.m_txtZip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtZip.Location = new System.Drawing.Point(145, 128);
            this.m_txtZip.Name = "m_txtZip";
            this.m_txtZip.Size = new System.Drawing.Size(92, 21);
            this.m_txtZip.TabIndex = 53;
            this.m_txtZip.Text = "75025";
            // 
            // m_lblCity
            // 
            this.m_lblCity.ForeColor = System.Drawing.Color.Red;
            this.m_lblCity.Location = new System.Drawing.Point(4, 104);
            this.m_lblCity.Name = "m_lblCity";
            this.m_lblCity.Size = new System.Drawing.Size(63, 20);
            this.m_lblCity.Text = "City";
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
            this.m_cmbState.Location = new System.Drawing.Point(70, 128);
            this.m_cmbState.Name = "m_cmbState";
            this.m_cmbState.Size = new System.Drawing.Size(72, 22);
            this.m_cmbState.TabIndex = 51;
            // 
            // m_txtCity
            // 
            this.m_txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCity.Location = new System.Drawing.Point(70, 104);
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
            // m_lblStateZip
            // 
            this.m_lblStateZip.ForeColor = System.Drawing.Color.Red;
            this.m_lblStateZip.Location = new System.Drawing.Point(3, 130);
            this.m_lblStateZip.Name = "m_lblStateZip";
            this.m_lblStateZip.Size = new System.Drawing.Size(64, 20);
            this.m_lblStateZip.Text = "State, Zip";
            // 
            // m_lblLastName
            // 
            this.m_lblLastName.ForeColor = System.Drawing.Color.Red;
            this.m_lblLastName.Location = new System.Drawing.Point(3, 81);
            this.m_lblLastName.Name = "m_lblLastName";
            this.m_lblLastName.Size = new System.Drawing.Size(64, 20);
            this.m_lblLastName.Text = "Last Name";
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
            // m_lblFirstName
            // 
            this.m_lblFirstName.ForeColor = System.Drawing.Color.Red;
            this.m_lblFirstName.Location = new System.Drawing.Point(3, 57);
            this.m_lblFirstName.Name = "m_lblFirstName";
            this.m_lblFirstName.Size = new System.Drawing.Size(76, 20);
            this.m_lblFirstName.Text = "First Name";
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
            this.m_tabs.TabIndex = 60;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_txtFirstName);
            this.tabPage1.Controls.Add(this.m_lblJobType);
            this.tabPage1.Controls.Add(this.m_lblCustomerName);
            this.tabPage1.Controls.Add(this.m_lblTicketNumber);
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
            // m_lblJobType
            // 
            this.m_lblJobType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblJobType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblJobType.Location = new System.Drawing.Point(71, 37);
            this.m_lblJobType.Name = "m_lblJobType";
            this.m_lblJobType.Size = new System.Drawing.Size(166, 20);
            this.m_lblJobType.Text = "Rug Pickup";
            this.m_lblJobType.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // m_lblTicketNumber
            // 
            this.m_lblTicketNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTicketNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTicketNumber.Location = new System.Drawing.Point(71, 3);
            this.m_lblTicketNumber.Name = "m_lblTicketNumber";
            this.m_lblTicketNumber.Size = new System.Drawing.Size(166, 20);
            this.m_lblTicketNumber.Text = "1001";
            this.m_lblTicketNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.Text = "Job Type";
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
            // tabPage2
            // 
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
            // m_lblAmountDue
            // 
            this.m_lblAmountDue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAmountDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAmountDue.Location = new System.Drawing.Point(82, 50);
            this.m_lblAmountDue.Name = "m_lblAmountDue";
            this.m_lblAmountDue.Size = new System.Drawing.Size(153, 20);
            this.m_lblAmountDue.Text = "$15.00";
            this.m_lblAmountDue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 20);
            this.label10.Text = "Amount Due";
            // 
            // m_txtBankRouteNumber
            // 
            this.m_txtBankRouteNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtBankRouteNumber.Location = new System.Drawing.Point(119, 26);
            this.m_txtBankRouteNumber.Name = "m_txtBankRouteNumber";
            this.m_txtBankRouteNumber.Size = new System.Drawing.Size(117, 21);
            this.m_txtBankRouteNumber.TabIndex = 54;
            // 
            // m_lblCheckNumber
            // 
            this.m_lblCheckNumber.ForeColor = System.Drawing.Color.Red;
            this.m_lblCheckNumber.Location = new System.Drawing.Point(3, 3);
            this.m_lblCheckNumber.Name = "m_lblCheckNumber";
            this.m_lblCheckNumber.Size = new System.Drawing.Size(60, 20);
            this.m_lblCheckNumber.Text = "Check #";
            // 
            // m_txtCheckNumber
            // 
            this.m_txtCheckNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCheckNumber.Location = new System.Drawing.Point(69, 2);
            this.m_txtCheckNumber.Name = "m_txtCheckNumber";
            this.m_txtCheckNumber.Size = new System.Drawing.Size(167, 21);
            this.m_txtCheckNumber.TabIndex = 53;
            // 
            // m_lblBankRouteNumber
            // 
            this.m_lblBankRouteNumber.ForeColor = System.Drawing.Color.Red;
            this.m_lblBankRouteNumber.Location = new System.Drawing.Point(3, 28);
            this.m_lblBankRouteNumber.Name = "m_lblBankRouteNumber";
            this.m_lblBankRouteNumber.Size = new System.Drawing.Size(102, 20);
            this.m_lblBankRouteNumber.Text = "Bank Route #";
            // 
            // PayByCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_tabs);
            this.Menu = this.mainMenu1;
            this.Name = "PayByCheck";
            this.Text = "0264 Pay By Check";
            this.m_tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem m_menuCancel;
        private System.Windows.Forms.MenuItem m_menuDone;
        private System.Windows.Forms.TextBox m_txtZip;
        private System.Windows.Forms.Label m_lblCity;
        private System.Windows.Forms.ComboBox m_cmbState;
        private System.Windows.Forms.TextBox m_txtCity;
        private System.Windows.Forms.TextBox m_txtLastName;
        private System.Windows.Forms.Label m_lblStateZip;
        private System.Windows.Forms.Label m_lblLastName;
        private System.Windows.Forms.TextBox m_txtFirstName;
        private System.Windows.Forms.Label m_lblFirstName;
        private System.Windows.Forms.TabControl m_tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox m_txtBankRouteNumber;
        private System.Windows.Forms.Label m_lblCheckNumber;
        private System.Windows.Forms.TextBox m_txtCheckNumber;
        private System.Windows.Forms.Label m_lblBankRouteNumber;
        private System.Windows.Forms.Label m_lblJobType;
        private System.Windows.Forms.Label m_lblCustomerName;
        private System.Windows.Forms.Label m_lblTicketNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label m_lblAmountDue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}