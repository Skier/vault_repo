namespace dalworth.preview
{
    partial class PayByCashDone
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
            this.m_lblJobType = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblTicketNumber = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.m_lblAmountDue = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
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
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(3, 37);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(62, 20);
            this.label30.Text = "Job Type";
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
            // m_lblAmountDue
            // 
            this.m_lblAmountDue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAmountDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAmountDue.Location = new System.Drawing.Point(82, 57);
            this.m_lblAmountDue.Name = "m_lblAmountDue";
            this.m_lblAmountDue.Size = new System.Drawing.Size(153, 20);
            this.m_lblAmountDue.Text = "$15.00";
            this.m_lblAmountDue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(3, 57);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(77, 20);
            this.label35.Text = "Amount Due";
            // 
            // PayByCashDone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_lblAmountDue);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.m_lblJobType);
            this.Controls.Add(this.m_lblCustomerName);
            this.Controls.Add(this.m_lblTicketNumber);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label32);
            this.Menu = this.mainMenu1;
            this.Name = "PayByCashDone";
            this.Text = "0261 Pay By Cash";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem m_menuCancel;
        private System.Windows.Forms.MenuItem m_menuDone;
        private System.Windows.Forms.Label m_lblJobType;
        private System.Windows.Forms.Label m_lblCustomerName;
        private System.Windows.Forms.Label m_lblTicketNumber;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label m_lblAmountDue;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}