namespace dalworth.preview
{
    partial class DeclineReason
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
            this.m_txtMessage = new System.Windows.Forms.TextBox();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
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
            // m_txtMessage
            // 
            this.m_txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtMessage.Location = new System.Drawing.Point(0, 0);
            this.m_txtMessage.Multiline = true;
            this.m_txtMessage.Name = "m_txtMessage";
            this.m_txtMessage.Size = new System.Drawing.Size(240, 268);
            this.m_txtMessage.TabIndex = 0;
            this.m_txtMessage.TextChanged += new System.EventHandler(this.OnMessageChanged);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.m_menuCancel);
            this.menuItem1.Text = "Menu";
            // 
            // DeclineReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_txtMessage);
            this.Menu = this.mainMenu1;
            this.Name = "DeclineReason";
            this.Text = "0212 Decline Reason";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem m_menuCancel;
        private System.Windows.Forms.MenuItem m_menuSend;
        private System.Windows.Forms.TextBox m_txtMessage;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}