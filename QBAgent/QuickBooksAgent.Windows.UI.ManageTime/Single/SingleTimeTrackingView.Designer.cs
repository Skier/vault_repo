namespace QuickBooksAgent.Windows.UI.ManageTime.Single
{
    partial class SingleTimeTrackingView
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
            this.m_cmbPersonType = new System.Windows.Forms.ComboBox();
            this.m_cmbPerson = new System.Windows.Forms.ComboBox();
            this.m_dtpDate = new System.Windows.Forms.DateTimePicker();
            this.m_lblDate = new System.Windows.Forms.Label();
            this.m_tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_curRate = new QuickBooksAgent.Windows.UI.Controls.CurrencyEdit();
            this.m_lblSummaryCost = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chkBillable = new System.Windows.Forms.CheckBox();
            this.m_cmbService = new System.Windows.Forms.ComboBox();
            this.m_lblService = new System.Windows.Forms.Label();
            this.m_cmbCustomer = new System.Windows.Forms.ComboBox();
            this.m_lblCustomer = new System.Windows.Forms.Label();
            this.m_lblRate = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_txtTimeHours = new System.Windows.Forms.TextBox();
            this.m_txtBreakHours = new System.Windows.Forms.TextBox();
            this.m_cmbEndPm = new System.Windows.Forms.ComboBox();
            this.m_cmbEndHours = new System.Windows.Forms.ComboBox();
            this.m_cmbEndMins = new System.Windows.Forms.ComboBox();
            this.m_lblEndSeparator = new System.Windows.Forms.Label();
            this.m_cmbBreakMins = new System.Windows.Forms.ComboBox();
            this.m_lblBreakSeparator = new System.Windows.Forms.Label();
            this.m_cmbTimeMins = new System.Windows.Forms.ComboBox();
            this.m_lblTimeSeparator = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_chkStartEnd = new System.Windows.Forms.CheckBox();
            this.m_lblSummary = new System.Windows.Forms.Label();
            this.m_lbStart = new System.Windows.Forms.Label();
            this.m_lbBreak = new System.Windows.Forms.Label();
            this.m_lbEnd = new System.Windows.Forms.Label();
            this.m_lblTime = new System.Windows.Forms.Label();
            this.m_cmbStartPm = new System.Windows.Forms.ComboBox();
            this.m_cmbStartHours = new System.Windows.Forms.ComboBox();
            this.m_cmbStartMins = new System.Windows.Forms.ComboBox();
            this.m_lblStartSeparator = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.m_txtNotes = new System.Windows.Forms.TextBox();
            this.m_tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_cmbPersonType
            // 
            this.m_cmbPersonType.Location = new System.Drawing.Point(2, 2);
            this.m_cmbPersonType.Name = "m_cmbPersonType";
            this.m_cmbPersonType.Size = new System.Drawing.Size(81, 22);
            this.m_cmbPersonType.TabIndex = 0;
            // 
            // m_cmbPerson
            // 
            this.m_cmbPerson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbPerson.DisplayMember = "Name";
            this.m_cmbPerson.Location = new System.Drawing.Point(86, 2);
            this.m_cmbPerson.Name = "m_cmbPerson";
            this.m_cmbPerson.Size = new System.Drawing.Size(151, 22);
            this.m_cmbPerson.TabIndex = 1;
            this.m_cmbPerson.ValueMember = "Name";
            // 
            // m_dtpDate
            // 
            this.m_dtpDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpDate.Location = new System.Drawing.Point(86, 27);
            this.m_dtpDate.Name = "m_dtpDate";
            this.m_dtpDate.Size = new System.Drawing.Size(151, 22);
            this.m_dtpDate.TabIndex = 2;
            // 
            // m_lblDate
            // 
            this.m_lblDate.Location = new System.Drawing.Point(48, 29);
            this.m_lblDate.Name = "m_lblDate";
            this.m_lblDate.Size = new System.Drawing.Size(32, 20);
            this.m_lblDate.Text = "Date";
            // 
            // m_tabs
            // 
            this.m_tabs.Controls.Add(this.tabPage1);
            this.m_tabs.Controls.Add(this.tabPage2);
            this.m_tabs.Controls.Add(this.tabPage3);
            this.m_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabs.Location = new System.Drawing.Point(0, 0);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedIndex = 0;
            this.m_tabs.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.TabIndex = 29;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.m_curRate);
            this.tabPage1.Controls.Add(this.m_lblSummaryCost);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.m_cmbService);
            this.tabPage1.Controls.Add(this.m_lblService);
            this.tabPage1.Controls.Add(this.m_cmbCustomer);
            this.tabPage1.Controls.Add(this.m_lblCustomer);
            this.tabPage1.Controls.Add(this.m_lblRate);
            this.tabPage1.Controls.Add(this.m_cmbPersonType);
            this.tabPage1.Controls.Add(this.m_cmbPerson);
            this.tabPage1.Controls.Add(this.m_lblDate);
            this.tabPage1.Controls.Add(this.m_dtpDate);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(240, 245);
            this.tabPage1.Text = "General";
            // 
            // m_curRate
            // 
            this.m_curRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curRate.IsAllowNull = true;
            this.m_curRate.Location = new System.Drawing.Point(168, 102);
            this.m_curRate.MaxLength = 9;
            this.m_curRate.Name = "m_curRate";
            this.m_curRate.Size = new System.Drawing.Size(69, 21);
            this.m_curRate.TabIndex = 16;
            // 
            // m_lblSummaryCost
            // 
            this.m_lblSummaryCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblSummaryCost.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblSummaryCost.Location = new System.Drawing.Point(7, 128);
            this.m_lblSummaryCost.Name = "m_lblSummaryCost";
            this.m_lblSummaryCost.Size = new System.Drawing.Size(226, 19);
            this.m_lblSummaryCost.Text = "Summary, $: undefined";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkBillable);
            this.panel1.Location = new System.Drawing.Point(2, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(81, 27);
            // 
            // m_chkBillable
            // 
            this.m_chkBillable.Location = new System.Drawing.Point(3, 4);
            this.m_chkBillable.Name = "m_chkBillable";
            this.m_chkBillable.Size = new System.Drawing.Size(72, 20);
            this.m_chkBillable.TabIndex = 13;
            this.m_chkBillable.Text = "Billable";
            // 
            // m_cmbService
            // 
            this.m_cmbService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbService.DisplayMember = "Name";
            this.m_cmbService.Location = new System.Drawing.Point(86, 77);
            this.m_cmbService.Name = "m_cmbService";
            this.m_cmbService.Size = new System.Drawing.Size(151, 22);
            this.m_cmbService.TabIndex = 11;
            // 
            // m_lblService
            // 
            this.m_lblService.Location = new System.Drawing.Point(34, 79);
            this.m_lblService.Name = "m_lblService";
            this.m_lblService.Size = new System.Drawing.Size(45, 20);
            this.m_lblService.Text = "Service";
            // 
            // m_cmbCustomer
            // 
            this.m_cmbCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbCustomer.DisplayMember = "FullName";
            this.m_cmbCustomer.Location = new System.Drawing.Point(86, 52);
            this.m_cmbCustomer.Name = "m_cmbCustomer";
            this.m_cmbCustomer.Size = new System.Drawing.Size(151, 22);
            this.m_cmbCustomer.TabIndex = 10;
            // 
            // m_lblCustomer
            // 
            this.m_lblCustomer.Location = new System.Drawing.Point(22, 54);
            this.m_lblCustomer.Name = "m_lblCustomer";
            this.m_lblCustomer.Size = new System.Drawing.Size(58, 20);
            this.m_lblCustomer.Text = "Customer";
            // 
            // m_lblRate
            // 
            this.m_lblRate.Location = new System.Drawing.Point(86, 104);
            this.m_lblRate.Name = "m_lblRate";
            this.m_lblRate.Size = new System.Drawing.Size(76, 20);
            this.m_lblRate.Text = "Rate, $/hour";
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.m_txtTimeHours);
            this.tabPage2.Controls.Add(this.m_txtBreakHours);
            this.tabPage2.Controls.Add(this.m_cmbEndPm);
            this.tabPage2.Controls.Add(this.m_cmbEndHours);
            this.tabPage2.Controls.Add(this.m_cmbEndMins);
            this.tabPage2.Controls.Add(this.m_lblEndSeparator);
            this.tabPage2.Controls.Add(this.m_cmbBreakMins);
            this.tabPage2.Controls.Add(this.m_lblBreakSeparator);
            this.tabPage2.Controls.Add(this.m_cmbTimeMins);
            this.tabPage2.Controls.Add(this.m_lblTimeSeparator);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.m_lblSummary);
            this.tabPage2.Controls.Add(this.m_lbStart);
            this.tabPage2.Controls.Add(this.m_lbBreak);
            this.tabPage2.Controls.Add(this.m_lbEnd);
            this.tabPage2.Controls.Add(this.m_lblTime);
            this.tabPage2.Controls.Add(this.m_cmbStartPm);
            this.tabPage2.Controls.Add(this.m_cmbStartHours);
            this.tabPage2.Controls.Add(this.m_cmbStartMins);
            this.tabPage2.Controls.Add(this.m_lblStartSeparator);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(240, 245);
            this.tabPage2.Text = "Time";
            // 
            // m_txtTimeHours
            // 
            this.m_txtTimeHours.Location = new System.Drawing.Point(97, 5);
            this.m_txtTimeHours.MaxLength = 4;
            this.m_txtTimeHours.Name = "m_txtTimeHours";
            this.m_txtTimeHours.Size = new System.Drawing.Size(95, 21);
            this.m_txtTimeHours.TabIndex = 77;
            // 
            // m_txtBreakHours
            // 
            this.m_txtBreakHours.Location = new System.Drawing.Point(97, 56);
            this.m_txtBreakHours.MaxLength = 4;
            this.m_txtBreakHours.Name = "m_txtBreakHours";
            this.m_txtBreakHours.Size = new System.Drawing.Size(95, 21);
            this.m_txtBreakHours.TabIndex = 76;
            this.m_txtBreakHours.Visible = false;
            // 
            // m_cmbEndPm
            // 
            this.m_cmbEndPm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbEndPm.Items.Add("AM");
            this.m_cmbEndPm.Items.Add("PM");
            this.m_cmbEndPm.Location = new System.Drawing.Point(198, 30);
            this.m_cmbEndPm.Name = "m_cmbEndPm";
            this.m_cmbEndPm.Size = new System.Drawing.Size(39, 22);
            this.m_cmbEndPm.TabIndex = 59;
            this.m_cmbEndPm.Visible = false;
            // 
            // m_cmbEndHours
            // 
            this.m_cmbEndHours.Items.Add("0");
            this.m_cmbEndHours.Items.Add("1");
            this.m_cmbEndHours.Items.Add("2");
            this.m_cmbEndHours.Items.Add("3");
            this.m_cmbEndHours.Items.Add("4");
            this.m_cmbEndHours.Items.Add("5");
            this.m_cmbEndHours.Items.Add("6");
            this.m_cmbEndHours.Items.Add("7");
            this.m_cmbEndHours.Items.Add("8");
            this.m_cmbEndHours.Items.Add("9");
            this.m_cmbEndHours.Items.Add("10");
            this.m_cmbEndHours.Items.Add("11");
            this.m_cmbEndHours.Items.Add("12");
            this.m_cmbEndHours.Location = new System.Drawing.Point(97, 30);
            this.m_cmbEndHours.Name = "m_cmbEndHours";
            this.m_cmbEndHours.Size = new System.Drawing.Size(55, 22);
            this.m_cmbEndHours.TabIndex = 57;
            this.m_cmbEndHours.Visible = false;
            // 
            // m_cmbEndMins
            // 
            this.m_cmbEndMins.Items.Add("00");
            this.m_cmbEndMins.Items.Add("01");
            this.m_cmbEndMins.Items.Add("02");
            this.m_cmbEndMins.Items.Add("03");
            this.m_cmbEndMins.Items.Add("04");
            this.m_cmbEndMins.Items.Add("05");
            this.m_cmbEndMins.Items.Add("06");
            this.m_cmbEndMins.Items.Add("07");
            this.m_cmbEndMins.Items.Add("08");
            this.m_cmbEndMins.Items.Add("09");
            this.m_cmbEndMins.Items.Add("10");
            this.m_cmbEndMins.Items.Add("11");
            this.m_cmbEndMins.Items.Add("12");
            this.m_cmbEndMins.Items.Add("13");
            this.m_cmbEndMins.Items.Add("14");
            this.m_cmbEndMins.Items.Add("15");
            this.m_cmbEndMins.Items.Add("16");
            this.m_cmbEndMins.Items.Add("17");
            this.m_cmbEndMins.Items.Add("18");
            this.m_cmbEndMins.Items.Add("19");
            this.m_cmbEndMins.Items.Add("20");
            this.m_cmbEndMins.Items.Add("21");
            this.m_cmbEndMins.Items.Add("22");
            this.m_cmbEndMins.Items.Add("23");
            this.m_cmbEndMins.Items.Add("24");
            this.m_cmbEndMins.Items.Add("25");
            this.m_cmbEndMins.Items.Add("26");
            this.m_cmbEndMins.Items.Add("27");
            this.m_cmbEndMins.Items.Add("28");
            this.m_cmbEndMins.Items.Add("29");
            this.m_cmbEndMins.Items.Add("30");
            this.m_cmbEndMins.Items.Add("31");
            this.m_cmbEndMins.Items.Add("32");
            this.m_cmbEndMins.Items.Add("33");
            this.m_cmbEndMins.Items.Add("34");
            this.m_cmbEndMins.Items.Add("35");
            this.m_cmbEndMins.Items.Add("36");
            this.m_cmbEndMins.Items.Add("37");
            this.m_cmbEndMins.Items.Add("38");
            this.m_cmbEndMins.Items.Add("39");
            this.m_cmbEndMins.Items.Add("40");
            this.m_cmbEndMins.Items.Add("41");
            this.m_cmbEndMins.Items.Add("42");
            this.m_cmbEndMins.Items.Add("43");
            this.m_cmbEndMins.Items.Add("44");
            this.m_cmbEndMins.Items.Add("45");
            this.m_cmbEndMins.Items.Add("46");
            this.m_cmbEndMins.Items.Add("47");
            this.m_cmbEndMins.Items.Add("48");
            this.m_cmbEndMins.Items.Add("49");
            this.m_cmbEndMins.Items.Add("50");
            this.m_cmbEndMins.Items.Add("51");
            this.m_cmbEndMins.Items.Add("52");
            this.m_cmbEndMins.Items.Add("53");
            this.m_cmbEndMins.Items.Add("54");
            this.m_cmbEndMins.Items.Add("55");
            this.m_cmbEndMins.Items.Add("56");
            this.m_cmbEndMins.Items.Add("57");
            this.m_cmbEndMins.Items.Add("58");
            this.m_cmbEndMins.Items.Add("59");
            this.m_cmbEndMins.Location = new System.Drawing.Point(158, 30);
            this.m_cmbEndMins.Name = "m_cmbEndMins";
            this.m_cmbEndMins.Size = new System.Drawing.Size(39, 22);
            this.m_cmbEndMins.TabIndex = 58;
            this.m_cmbEndMins.Visible = false;
            // 
            // m_lblEndSeparator
            // 
            this.m_lblEndSeparator.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblEndSeparator.Location = new System.Drawing.Point(152, 33);
            this.m_lblEndSeparator.Name = "m_lblEndSeparator";
            this.m_lblEndSeparator.Size = new System.Drawing.Size(10, 16);
            this.m_lblEndSeparator.Text = ":";
            this.m_lblEndSeparator.Visible = false;
            // 
            // m_cmbBreakMins
            // 
            this.m_cmbBreakMins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbBreakMins.Items.Add("00");
            this.m_cmbBreakMins.Items.Add("01");
            this.m_cmbBreakMins.Items.Add("02");
            this.m_cmbBreakMins.Items.Add("03");
            this.m_cmbBreakMins.Items.Add("04");
            this.m_cmbBreakMins.Items.Add("05");
            this.m_cmbBreakMins.Items.Add("06");
            this.m_cmbBreakMins.Items.Add("07");
            this.m_cmbBreakMins.Items.Add("08");
            this.m_cmbBreakMins.Items.Add("09");
            this.m_cmbBreakMins.Items.Add("10");
            this.m_cmbBreakMins.Items.Add("11");
            this.m_cmbBreakMins.Items.Add("12");
            this.m_cmbBreakMins.Items.Add("13");
            this.m_cmbBreakMins.Items.Add("14");
            this.m_cmbBreakMins.Items.Add("15");
            this.m_cmbBreakMins.Items.Add("16");
            this.m_cmbBreakMins.Items.Add("17");
            this.m_cmbBreakMins.Items.Add("18");
            this.m_cmbBreakMins.Items.Add("19");
            this.m_cmbBreakMins.Items.Add("20");
            this.m_cmbBreakMins.Items.Add("21");
            this.m_cmbBreakMins.Items.Add("22");
            this.m_cmbBreakMins.Items.Add("23");
            this.m_cmbBreakMins.Items.Add("24");
            this.m_cmbBreakMins.Items.Add("25");
            this.m_cmbBreakMins.Items.Add("26");
            this.m_cmbBreakMins.Items.Add("27");
            this.m_cmbBreakMins.Items.Add("28");
            this.m_cmbBreakMins.Items.Add("29");
            this.m_cmbBreakMins.Items.Add("30");
            this.m_cmbBreakMins.Items.Add("31");
            this.m_cmbBreakMins.Items.Add("32");
            this.m_cmbBreakMins.Items.Add("33");
            this.m_cmbBreakMins.Items.Add("34");
            this.m_cmbBreakMins.Items.Add("35");
            this.m_cmbBreakMins.Items.Add("36");
            this.m_cmbBreakMins.Items.Add("37");
            this.m_cmbBreakMins.Items.Add("38");
            this.m_cmbBreakMins.Items.Add("39");
            this.m_cmbBreakMins.Items.Add("40");
            this.m_cmbBreakMins.Items.Add("41");
            this.m_cmbBreakMins.Items.Add("42");
            this.m_cmbBreakMins.Items.Add("43");
            this.m_cmbBreakMins.Items.Add("44");
            this.m_cmbBreakMins.Items.Add("45");
            this.m_cmbBreakMins.Items.Add("46");
            this.m_cmbBreakMins.Items.Add("47");
            this.m_cmbBreakMins.Items.Add("48");
            this.m_cmbBreakMins.Items.Add("49");
            this.m_cmbBreakMins.Items.Add("50");
            this.m_cmbBreakMins.Items.Add("51");
            this.m_cmbBreakMins.Items.Add("52");
            this.m_cmbBreakMins.Items.Add("53");
            this.m_cmbBreakMins.Items.Add("54");
            this.m_cmbBreakMins.Items.Add("55");
            this.m_cmbBreakMins.Items.Add("56");
            this.m_cmbBreakMins.Items.Add("57");
            this.m_cmbBreakMins.Items.Add("58");
            this.m_cmbBreakMins.Items.Add("59");
            this.m_cmbBreakMins.Location = new System.Drawing.Point(198, 56);
            this.m_cmbBreakMins.Name = "m_cmbBreakMins";
            this.m_cmbBreakMins.Size = new System.Drawing.Size(39, 22);
            this.m_cmbBreakMins.TabIndex = 67;
            this.m_cmbBreakMins.Visible = false;
            // 
            // m_lblBreakSeparator
            // 
            this.m_lblBreakSeparator.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblBreakSeparator.Location = new System.Drawing.Point(192, 59);
            this.m_lblBreakSeparator.Name = "m_lblBreakSeparator";
            this.m_lblBreakSeparator.Size = new System.Drawing.Size(10, 16);
            this.m_lblBreakSeparator.Text = ":";
            this.m_lblBreakSeparator.Visible = false;
            // 
            // m_cmbTimeMins
            // 
            this.m_cmbTimeMins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTimeMins.Items.Add("00");
            this.m_cmbTimeMins.Items.Add("01");
            this.m_cmbTimeMins.Items.Add("02");
            this.m_cmbTimeMins.Items.Add("03");
            this.m_cmbTimeMins.Items.Add("04");
            this.m_cmbTimeMins.Items.Add("05");
            this.m_cmbTimeMins.Items.Add("06");
            this.m_cmbTimeMins.Items.Add("07");
            this.m_cmbTimeMins.Items.Add("08");
            this.m_cmbTimeMins.Items.Add("09");
            this.m_cmbTimeMins.Items.Add("10");
            this.m_cmbTimeMins.Items.Add("11");
            this.m_cmbTimeMins.Items.Add("12");
            this.m_cmbTimeMins.Items.Add("13");
            this.m_cmbTimeMins.Items.Add("14");
            this.m_cmbTimeMins.Items.Add("15");
            this.m_cmbTimeMins.Items.Add("16");
            this.m_cmbTimeMins.Items.Add("17");
            this.m_cmbTimeMins.Items.Add("18");
            this.m_cmbTimeMins.Items.Add("19");
            this.m_cmbTimeMins.Items.Add("20");
            this.m_cmbTimeMins.Items.Add("21");
            this.m_cmbTimeMins.Items.Add("22");
            this.m_cmbTimeMins.Items.Add("23");
            this.m_cmbTimeMins.Items.Add("24");
            this.m_cmbTimeMins.Items.Add("25");
            this.m_cmbTimeMins.Items.Add("26");
            this.m_cmbTimeMins.Items.Add("27");
            this.m_cmbTimeMins.Items.Add("28");
            this.m_cmbTimeMins.Items.Add("29");
            this.m_cmbTimeMins.Items.Add("30");
            this.m_cmbTimeMins.Items.Add("31");
            this.m_cmbTimeMins.Items.Add("32");
            this.m_cmbTimeMins.Items.Add("33");
            this.m_cmbTimeMins.Items.Add("34");
            this.m_cmbTimeMins.Items.Add("35");
            this.m_cmbTimeMins.Items.Add("36");
            this.m_cmbTimeMins.Items.Add("37");
            this.m_cmbTimeMins.Items.Add("38");
            this.m_cmbTimeMins.Items.Add("39");
            this.m_cmbTimeMins.Items.Add("40");
            this.m_cmbTimeMins.Items.Add("41");
            this.m_cmbTimeMins.Items.Add("42");
            this.m_cmbTimeMins.Items.Add("43");
            this.m_cmbTimeMins.Items.Add("44");
            this.m_cmbTimeMins.Items.Add("45");
            this.m_cmbTimeMins.Items.Add("46");
            this.m_cmbTimeMins.Items.Add("47");
            this.m_cmbTimeMins.Items.Add("48");
            this.m_cmbTimeMins.Items.Add("49");
            this.m_cmbTimeMins.Items.Add("50");
            this.m_cmbTimeMins.Items.Add("51");
            this.m_cmbTimeMins.Items.Add("52");
            this.m_cmbTimeMins.Items.Add("53");
            this.m_cmbTimeMins.Items.Add("54");
            this.m_cmbTimeMins.Items.Add("55");
            this.m_cmbTimeMins.Items.Add("56");
            this.m_cmbTimeMins.Items.Add("57");
            this.m_cmbTimeMins.Items.Add("58");
            this.m_cmbTimeMins.Items.Add("59");
            this.m_cmbTimeMins.Location = new System.Drawing.Point(198, 5);
            this.m_cmbTimeMins.Name = "m_cmbTimeMins";
            this.m_cmbTimeMins.Size = new System.Drawing.Size(39, 22);
            this.m_cmbTimeMins.TabIndex = 63;
            // 
            // m_lblTimeSeparator
            // 
            this.m_lblTimeSeparator.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTimeSeparator.Location = new System.Drawing.Point(192, 8);
            this.m_lblTimeSeparator.Name = "m_lblTimeSeparator";
            this.m_lblTimeSeparator.Size = new System.Drawing.Size(10, 16);
            this.m_lblTimeSeparator.Text = ":";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_chkStartEnd);
            this.panel2.Location = new System.Drawing.Point(3, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(189, 26);
            // 
            // m_chkStartEnd
            // 
            this.m_chkStartEnd.Location = new System.Drawing.Point(3, 2);
            this.m_chkStartEnd.Name = "m_chkStartEnd";
            this.m_chkStartEnd.Size = new System.Drawing.Size(176, 21);
            this.m_chkStartEnd.TabIndex = 11;
            this.m_chkStartEnd.Text = "Enter Start And  End Times";
            // 
            // m_lblSummary
            // 
            this.m_lblSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblSummary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblSummary.Location = new System.Drawing.Point(4, 110);
            this.m_lblSummary.Name = "m_lblSummary";
            this.m_lblSummary.Size = new System.Drawing.Size(233, 18);
            this.m_lblSummary.Text = "Summary: undefined";
            // 
            // m_lbStart
            // 
            this.m_lbStart.Location = new System.Drawing.Point(3, 9);
            this.m_lbStart.Name = "m_lbStart";
            this.m_lbStart.Size = new System.Drawing.Size(88, 16);
            this.m_lbStart.Text = "Start";
            this.m_lbStart.Visible = false;
            // 
            // m_lbBreak
            // 
            this.m_lbBreak.Location = new System.Drawing.Point(4, 59);
            this.m_lbBreak.Name = "m_lbBreak";
            this.m_lbBreak.Size = new System.Drawing.Size(95, 16);
            this.m_lbBreak.Text = "Break (hh:mm)";
            this.m_lbBreak.Visible = false;
            // 
            // m_lbEnd
            // 
            this.m_lbEnd.Location = new System.Drawing.Point(4, 35);
            this.m_lbEnd.Name = "m_lbEnd";
            this.m_lbEnd.Size = new System.Drawing.Size(84, 16);
            this.m_lbEnd.Text = "End";
            this.m_lbEnd.Visible = false;
            // 
            // m_lblTime
            // 
            this.m_lblTime.Location = new System.Drawing.Point(2, 8);
            this.m_lblTime.Name = "m_lblTime";
            this.m_lblTime.Size = new System.Drawing.Size(85, 20);
            this.m_lblTime.Text = "Time (hh:mm)";
            // 
            // m_cmbStartPm
            // 
            this.m_cmbStartPm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbStartPm.Items.Add("AM");
            this.m_cmbStartPm.Items.Add("PM");
            this.m_cmbStartPm.Location = new System.Drawing.Point(198, 5);
            this.m_cmbStartPm.Name = "m_cmbStartPm";
            this.m_cmbStartPm.Size = new System.Drawing.Size(39, 22);
            this.m_cmbStartPm.TabIndex = 47;
            this.m_cmbStartPm.Visible = false;
            // 
            // m_cmbStartHours
            // 
            this.m_cmbStartHours.Items.Add("0");
            this.m_cmbStartHours.Items.Add("1");
            this.m_cmbStartHours.Items.Add("2");
            this.m_cmbStartHours.Items.Add("3");
            this.m_cmbStartHours.Items.Add("4");
            this.m_cmbStartHours.Items.Add("5");
            this.m_cmbStartHours.Items.Add("6");
            this.m_cmbStartHours.Items.Add("7");
            this.m_cmbStartHours.Items.Add("8");
            this.m_cmbStartHours.Items.Add("9");
            this.m_cmbStartHours.Items.Add("10");
            this.m_cmbStartHours.Items.Add("11");
            this.m_cmbStartHours.Items.Add("12");
            this.m_cmbStartHours.Location = new System.Drawing.Point(97, 5);
            this.m_cmbStartHours.Name = "m_cmbStartHours";
            this.m_cmbStartHours.Size = new System.Drawing.Size(55, 22);
            this.m_cmbStartHours.TabIndex = 45;
            this.m_cmbStartHours.Visible = false;
            // 
            // m_cmbStartMins
            // 
            this.m_cmbStartMins.Items.Add("00");
            this.m_cmbStartMins.Items.Add("01");
            this.m_cmbStartMins.Items.Add("02");
            this.m_cmbStartMins.Items.Add("03");
            this.m_cmbStartMins.Items.Add("04");
            this.m_cmbStartMins.Items.Add("05");
            this.m_cmbStartMins.Items.Add("06");
            this.m_cmbStartMins.Items.Add("07");
            this.m_cmbStartMins.Items.Add("08");
            this.m_cmbStartMins.Items.Add("09");
            this.m_cmbStartMins.Items.Add("10");
            this.m_cmbStartMins.Items.Add("11");
            this.m_cmbStartMins.Items.Add("12");
            this.m_cmbStartMins.Items.Add("13");
            this.m_cmbStartMins.Items.Add("14");
            this.m_cmbStartMins.Items.Add("15");
            this.m_cmbStartMins.Items.Add("16");
            this.m_cmbStartMins.Items.Add("17");
            this.m_cmbStartMins.Items.Add("18");
            this.m_cmbStartMins.Items.Add("19");
            this.m_cmbStartMins.Items.Add("20");
            this.m_cmbStartMins.Items.Add("21");
            this.m_cmbStartMins.Items.Add("22");
            this.m_cmbStartMins.Items.Add("23");
            this.m_cmbStartMins.Items.Add("24");
            this.m_cmbStartMins.Items.Add("25");
            this.m_cmbStartMins.Items.Add("26");
            this.m_cmbStartMins.Items.Add("27");
            this.m_cmbStartMins.Items.Add("28");
            this.m_cmbStartMins.Items.Add("29");
            this.m_cmbStartMins.Items.Add("30");
            this.m_cmbStartMins.Items.Add("31");
            this.m_cmbStartMins.Items.Add("32");
            this.m_cmbStartMins.Items.Add("33");
            this.m_cmbStartMins.Items.Add("34");
            this.m_cmbStartMins.Items.Add("35");
            this.m_cmbStartMins.Items.Add("36");
            this.m_cmbStartMins.Items.Add("37");
            this.m_cmbStartMins.Items.Add("38");
            this.m_cmbStartMins.Items.Add("39");
            this.m_cmbStartMins.Items.Add("40");
            this.m_cmbStartMins.Items.Add("41");
            this.m_cmbStartMins.Items.Add("42");
            this.m_cmbStartMins.Items.Add("43");
            this.m_cmbStartMins.Items.Add("44");
            this.m_cmbStartMins.Items.Add("45");
            this.m_cmbStartMins.Items.Add("46");
            this.m_cmbStartMins.Items.Add("47");
            this.m_cmbStartMins.Items.Add("48");
            this.m_cmbStartMins.Items.Add("49");
            this.m_cmbStartMins.Items.Add("50");
            this.m_cmbStartMins.Items.Add("51");
            this.m_cmbStartMins.Items.Add("52");
            this.m_cmbStartMins.Items.Add("53");
            this.m_cmbStartMins.Items.Add("54");
            this.m_cmbStartMins.Items.Add("55");
            this.m_cmbStartMins.Items.Add("56");
            this.m_cmbStartMins.Items.Add("57");
            this.m_cmbStartMins.Items.Add("58");
            this.m_cmbStartMins.Items.Add("59");
            this.m_cmbStartMins.Location = new System.Drawing.Point(158, 5);
            this.m_cmbStartMins.Name = "m_cmbStartMins";
            this.m_cmbStartMins.Size = new System.Drawing.Size(39, 22);
            this.m_cmbStartMins.TabIndex = 46;
            this.m_cmbStartMins.Visible = false;
            // 
            // m_lblStartSeparator
            // 
            this.m_lblStartSeparator.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblStartSeparator.Location = new System.Drawing.Point(152, 8);
            this.m_lblStartSeparator.Name = "m_lblStartSeparator";
            this.m_lblStartSeparator.Size = new System.Drawing.Size(10, 16);
            this.m_lblStartSeparator.Text = ":";
            this.m_lblStartSeparator.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.m_txtNotes);
            this.tabPage3.Location = new System.Drawing.Point(0, 0);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(240, 245);
            this.tabPage3.Text = "Description";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtNotes.Location = new System.Drawing.Point(0, 0);
            this.m_txtNotes.Multiline = true;
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Size = new System.Drawing.Size(240, 245);
            this.m_txtNotes.TabIndex = 23;
            // 
            // SingleTimeTrackingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tabs);
            this.Name = "SingleTimeTrackingView";
            this.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ComboBox m_cmbPersonType;
        internal System.Windows.Forms.ComboBox m_cmbPerson;
        internal System.Windows.Forms.DateTimePicker m_dtpDate;
        internal System.Windows.Forms.Label m_lblDate;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        internal System.Windows.Forms.TextBox m_txtNotes;
        internal System.Windows.Forms.ComboBox m_cmbService;
        internal System.Windows.Forms.Label m_lblService;
        internal System.Windows.Forms.ComboBox m_cmbCustomer;
        internal System.Windows.Forms.Label m_lblCustomer;
        internal System.Windows.Forms.Label m_lblRate;
        internal System.Windows.Forms.Label m_lblTime;
        internal System.Windows.Forms.Label m_lblSummary;
        internal System.Windows.Forms.Label m_lbStart;
        internal System.Windows.Forms.Label m_lbBreak;
        internal System.Windows.Forms.Label m_lbEnd;
        internal System.Windows.Forms.TabControl m_tabs;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.CheckBox m_chkBillable;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.CheckBox m_chkStartEnd;
        internal System.Windows.Forms.Label m_lblStartSeparator;
        internal System.Windows.Forms.ComboBox m_cmbStartPm;
        internal System.Windows.Forms.ComboBox m_cmbStartMins;
        internal System.Windows.Forms.ComboBox m_cmbStartHours;
        internal System.Windows.Forms.ComboBox m_cmbEndPm;
        internal System.Windows.Forms.ComboBox m_cmbEndHours;
        internal System.Windows.Forms.ComboBox m_cmbEndMins;
        internal System.Windows.Forms.Label m_lblEndSeparator;
        internal System.Windows.Forms.ComboBox m_cmbTimeMins;
        internal System.Windows.Forms.Label m_lblTimeSeparator;
        internal System.Windows.Forms.ComboBox m_cmbBreakMins;
        internal System.Windows.Forms.Label m_lblBreakSeparator;
        internal System.Windows.Forms.Label m_lblSummaryCost;
        internal System.Windows.Forms.TextBox m_txtBreakHours;
        internal System.Windows.Forms.TextBox m_txtTimeHours;
        internal QuickBooksAgent.Windows.UI.Controls.CurrencyEdit m_curRate;
    }
}
