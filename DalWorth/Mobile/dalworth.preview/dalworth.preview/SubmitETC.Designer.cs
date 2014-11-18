namespace dalworth.preview
{
    partial class SubmitETC
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
            this.m_menuCancel = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.m_menuDone = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_curSale = new QuickBooksAgent.Windows.UI.Controls.CurrencyEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_lblJobType = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblTicketNumber = new System.Windows.Forms.Label();
            this.m_cmbEtcHH = new System.Windows.Forms.ComboBox();
            this.m_cmbEtcMM = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m_txtNotes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.m_menuDone);
            // 
            // m_menuCancel
            // 
            this.m_menuCancel.Text = "Cancel";
            this.m_menuCancel.Click += new System.EventHandler(this.OnCancelClick);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.m_menuCancel);
            this.menuItem1.Text = "Menu";
            // 
            // m_menuDone
            // 
            this.m_menuDone.Text = "Done";
            this.m_menuDone.Click += new System.EventHandler(this.OnDoneClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.Text = "Sale, $";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.Text = "ETC (hh:mm)";
            // 
            // m_curSale
            // 
            this.m_curSale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curSale.Location = new System.Drawing.Point(111, 57);
            this.m_curSale.Name = "m_curSale";
            this.m_curSale.Size = new System.Drawing.Size(126, 20);
            this.m_curSale.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.Text = "TKT";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.Text = "Customer";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.Text = "Job Type";
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
            // m_cmbEtcHH
            // 
            this.m_cmbEtcHH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbEtcHH.Items.Add("");
            this.m_cmbEtcHH.Items.Add("0");
            this.m_cmbEtcHH.Items.Add("1");
            this.m_cmbEtcHH.Items.Add("2");
            this.m_cmbEtcHH.Items.Add("3");
            this.m_cmbEtcHH.Items.Add("4");
            this.m_cmbEtcHH.Items.Add("5");
            this.m_cmbEtcHH.Items.Add("6");
            this.m_cmbEtcHH.Items.Add("7");
            this.m_cmbEtcHH.Items.Add("8");
            this.m_cmbEtcHH.Items.Add("9");
            this.m_cmbEtcHH.Items.Add("10");
            this.m_cmbEtcHH.Items.Add("11");
            this.m_cmbEtcHH.Items.Add("12");
            this.m_cmbEtcHH.Location = new System.Drawing.Point(111, 80);
            this.m_cmbEtcHH.Name = "m_cmbEtcHH";
            this.m_cmbEtcHH.Size = new System.Drawing.Size(57, 22);
            this.m_cmbEtcHH.TabIndex = 28;
            this.m_cmbEtcHH.SelectedIndexChanged += new System.EventHandler(this.OnHourChanged);
            // 
            // m_cmbEtcMM
            // 
            this.m_cmbEtcMM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbEtcMM.Items.Add("");
            this.m_cmbEtcMM.Items.Add("0");
            this.m_cmbEtcMM.Items.Add("15");
            this.m_cmbEtcMM.Items.Add("30");
            this.m_cmbEtcMM.Items.Add("45");
            this.m_cmbEtcMM.Location = new System.Drawing.Point(178, 80);
            this.m_cmbEtcMM.Name = "m_cmbEtcMM";
            this.m_cmbEtcMM.Size = new System.Drawing.Size(59, 22);
            this.m_cmbEtcMM.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(170, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 20);
            this.label6.Text = ":";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 20);
            this.label7.Text = "Notes";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNotes.Location = new System.Drawing.Point(4, 119);
            this.m_txtNotes.Multiline = true;
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Size = new System.Drawing.Size(233, 62);
            this.m_txtNotes.TabIndex = 41;
            // 
            // SubmitETC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_txtNotes);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_cmbEtcHH);
            this.Controls.Add(this.m_cmbEtcMM);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_lblJobType);
            this.Controls.Add(this.m_lblCustomerName);
            this.Controls.Add(this.m_lblTicketNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_curSale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "SubmitETC";
            this.Text = "0240 Submit ETC";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem m_menuCancel;
        private System.Windows.Forms.MenuItem m_menuDone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private QuickBooksAgent.Windows.UI.Controls.CurrencyEdit m_curSale;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label m_lblJobType;
        private System.Windows.Forms.Label m_lblCustomerName;
        private System.Windows.Forms.Label m_lblTicketNumber;
        private System.Windows.Forms.ComboBox m_cmbEtcHH;
        private System.Windows.Forms.ComboBox m_cmbEtcMM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m_txtNotes;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}