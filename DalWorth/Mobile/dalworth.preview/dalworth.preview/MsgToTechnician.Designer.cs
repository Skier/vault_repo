namespace dalworth.preview
{
    partial class MsgToTechnician
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
            this.m_menuSend = new System.Windows.Forms.MenuItem();
            this.m_cmbTechnician = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_txtMessage = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.m_menuCancel);
            this.mainMenu1.MenuItems.Add(this.m_menuSend);
            // 
            // m_menuCancel
            // 
            this.m_menuCancel.Text = "Cancel";
            this.m_menuCancel.Click += new System.EventHandler(this.OnCancelClick);
            // 
            // m_menuSend
            // 
            this.m_menuSend.Text = "Send";
            this.m_menuSend.Click += new System.EventHandler(this.OnSendClick);
            // 
            // m_cmbTechnician
            // 
            this.m_cmbTechnician.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTechnician.Items.Add("Chris Corcoran");
            this.m_cmbTechnician.Items.Add("Joe  Crider");
            this.m_cmbTechnician.Items.Add("David Andres");
            this.m_cmbTechnician.Items.Add("Dru Gregory");
            this.m_cmbTechnician.Items.Add("Richard Leon");
            this.m_cmbTechnician.Items.Add("Ron McCale");
            this.m_cmbTechnician.Location = new System.Drawing.Point(82, 3);
            this.m_cmbTechnician.Name = "m_cmbTechnician";
            this.m_cmbTechnician.Size = new System.Drawing.Size(155, 22);
            this.m_cmbTechnician.TabIndex = 0;
            this.m_cmbTechnician.SelectedIndexChanged += new System.EventHandler(this.OnTechnicianChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.Text = "Technician";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_cmbTechnician);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 50);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.Text = "Message:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_txtMessage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 218);
            // 
            // m_txtMessage
            // 
            this.m_txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtMessage.Location = new System.Drawing.Point(0, 0);
            this.m_txtMessage.Multiline = true;
            this.m_txtMessage.Name = "m_txtMessage";
            this.m_txtMessage.Size = new System.Drawing.Size(240, 218);
            this.m_txtMessage.TabIndex = 0;
            this.m_txtMessage.TextChanged += new System.EventHandler(this.OnMessageChanged);
            // 
            // MsgToTechnician
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Menu = this.mainMenu1;
            this.Name = "MsgToTechnician";
            this.Text = "0530 Msg To Technician";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox m_cmbTechnician;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox m_txtMessage;
        private System.Windows.Forms.MenuItem m_menuCancel;
        private System.Windows.Forms.MenuItem m_menuSend;
    }
}