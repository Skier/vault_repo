namespace dalworth.preview
{
    partial class TicketIncome
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
            this.m_menuDecline = new System.Windows.Forms.MenuItem();
            this.m_menuAccept = new System.Windows.Forms.MenuItem();
            this.m_btnAccept = new System.Windows.Forms.Button();
            this.m_timerBlinking = new System.Windows.Forms.Timer();
            this.m_timerSound = new System.Windows.Forms.Timer();
            this.m_txtNotes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblDate = new System.Windows.Forms.Label();
            this.m_lblJobType = new System.Windows.Forms.Label();
            this.m_lblTicketNumber = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.m_menuAccept);
            // 
            // m_menuDecline
            // 
            this.m_menuDecline.Text = "Decline";
            this.m_menuDecline.Click += new System.EventHandler(this.OnDeclineClick);
            // 
            // m_menuAccept
            // 
            this.m_menuAccept.Text = "Accept";
            this.m_menuAccept.Click += new System.EventHandler(this.OnAcceptClick);
            // 
            // m_btnAccept
            // 
            this.m_btnAccept.BackColor = System.Drawing.Color.White;
            this.m_btnAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_btnAccept.Location = new System.Drawing.Point(0, 0);
            this.m_btnAccept.Name = "m_btnAccept";
            this.m_btnAccept.Size = new System.Drawing.Size(117, 28);
            this.m_btnAccept.TabIndex = 0;
            this.m_btnAccept.Text = "Accept";
            // 
            // m_timerBlinking
            // 
            this.m_timerBlinking.Interval = 500;
            this.m_timerBlinking.Tick += new System.EventHandler(this.OnTimerBlinkingTick);
            // 
            // m_timerSound
            // 
            this.m_timerSound.Interval = 5000;
            this.m_timerSound.Tick += new System.EventHandler(this.OnTimerSoundTick);
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNotes.Location = new System.Drawing.Point(3, 71);
            this.m_txtNotes.Multiline = true;
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.ReadOnly = true;
            this.m_txtNotes.Size = new System.Drawing.Size(234, 39);
            this.m_txtNotes.TabIndex = 29;
            this.m_txtNotes.Text = "Pick up 2 rugs. Cust would like 3-5 time frame. Please call in AM to confirm.";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.Text = "Notes:";
            // 
            // m_lblCustomerName
            // 
            this.m_lblCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCustomerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblCustomerName.Location = new System.Drawing.Point(3, 19);
            this.m_lblCustomerName.Name = "m_lblCustomerName";
            this.m_lblCustomerName.Size = new System.Drawing.Size(234, 20);
            this.m_lblCustomerName.Text = "Love, Rob";
            // 
            // m_lblDate
            // 
            this.m_lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblDate.Location = new System.Drawing.Point(3, 3);
            this.m_lblDate.Name = "m_lblDate";
            this.m_lblDate.Size = new System.Drawing.Size(149, 20);
            this.m_lblDate.Text = "Sat, Apr 14, 2007";
            // 
            // m_lblJobType
            // 
            this.m_lblJobType.Location = new System.Drawing.Point(3, 37);
            this.m_lblJobType.Name = "m_lblJobType";
            this.m_lblJobType.Size = new System.Drawing.Size(110, 20);
            this.m_lblJobType.Text = "Rug Pickup";
            // 
            // m_lblTicketNumber
            // 
            this.m_lblTicketNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTicketNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTicketNumber.Location = new System.Drawing.Point(119, 37);
            this.m_lblTicketNumber.Name = "m_lblTicketNumber";
            this.m_lblTicketNumber.Size = new System.Drawing.Size(118, 20);
            this.m_lblTicketNumber.Text = "TKT: 1001";
            this.m_lblTicketNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.m_btnAccept);
            this.panel1.Location = new System.Drawing.Point(62, 237);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(117, 28);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.m_menuDecline);
            this.menuItem1.Text = "Menu";
            // 
            // TicketIncome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_txtNotes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_lblCustomerName);
            this.Controls.Add(this.m_lblDate);
            this.Controls.Add(this.m_lblJobType);
            this.Controls.Add(this.m_lblTicketNumber);
            this.Menu = this.mainMenu1;
            this.Name = "TicketIncome";
            this.Text = "0211 Ticket Income";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem m_menuDecline;
        private System.Windows.Forms.MenuItem m_menuAccept;
        private System.Windows.Forms.Button m_btnAccept;
        private System.Windows.Forms.Timer m_timerBlinking;
        private System.Windows.Forms.Timer m_timerSound;
        private System.Windows.Forms.TextBox m_txtNotes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label m_lblCustomerName;
        private System.Windows.Forms.Label m_lblDate;
        private System.Windows.Forms.Label m_lblJobType;
        private System.Windows.Forms.Label m_lblTicketNumber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}