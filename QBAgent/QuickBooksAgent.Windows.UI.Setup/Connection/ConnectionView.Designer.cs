namespace QuickBooksAgent.Windows.UI.Setup.Connection
{
    partial class ConnectionView
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
            this.m_lbConnectionTicket = new System.Windows.Forms.Label();
            this.m_ticket = new System.Windows.Forms.TextBox();
            this.m_lbAuthType = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chbSecure = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_defaultLogin = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lbConnectionTicket
            // 
            this.m_lbConnectionTicket.Location = new System.Drawing.Point(11, 79);
            this.m_lbConnectionTicket.Name = "m_lbConnectionTicket";
            this.m_lbConnectionTicket.Size = new System.Drawing.Size(104, 21);
            this.m_lbConnectionTicket.Text = "Connection Ticket";
            // 
            // m_ticket
            // 
            this.m_ticket.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.m_ticket.Location = new System.Drawing.Point(26, 103);
            this.m_ticket.Name = "m_ticket";
            this.m_ticket.Size = new System.Drawing.Size(200, 19);
            this.m_ticket.TabIndex = 1;
            // 
            // m_lbAuthType
            // 
            this.m_lbAuthType.Location = new System.Drawing.Point(11, 9);
            this.m_lbAuthType.Name = "m_lbAuthType";
            this.m_lbAuthType.Size = new System.Drawing.Size(129, 23);
            this.m_lbAuthType.Text = "Connection Type";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chbSecure);
            this.panel1.Location = new System.Drawing.Point(23, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 47);
            // 
            // m_chbSecure
            // 
            this.m_chbSecure.Enabled = false;
            this.m_chbSecure.Location = new System.Drawing.Point(0, 8);
            this.m_chbSecure.Name = "m_chbSecure";
            this.m_chbSecure.Size = new System.Drawing.Size(200, 20);
            this.m_chbSecure.TabIndex = 1;
            this.m_chbSecure.Text = "Use Secure Connection";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 21);
            this.label1.Text = "Default QuickBooks Login";
            // 
            // m_defaultLogin
            // 
            this.m_defaultLogin.Enabled = false;
            this.m_defaultLogin.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.m_defaultLogin.Location = new System.Drawing.Point(26, 151);
            this.m_defaultLogin.Name = "m_defaultLogin";
            this.m_defaultLogin.Size = new System.Drawing.Size(200, 19);
            this.m_defaultLogin.TabIndex = 1;
            // 
            // ConnectionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_lbAuthType);
            this.Controls.Add(this.m_defaultLogin);
            this.Controls.Add(this.m_ticket);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_lbConnectionTicket);
            this.Name = "ConnectionView";
            this.Size = new System.Drawing.Size(240, 186);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lbConnectionTicket;
        private System.Windows.Forms.Label m_lbAuthType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox m_ticket;
        internal System.Windows.Forms.TextBox m_defaultLogin;
        internal System.Windows.Forms.CheckBox m_chbSecure;
    }
}
