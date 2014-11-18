namespace dalworth.preview
{
    partial class Messages
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
            this.m_menuBack = new System.Windows.Forms.MenuItem();
            this.m_menuNext = new System.Windows.Forms.MenuItem();
            this.m_txtMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.m_menuNext);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.m_menuBack);
            this.menuItem1.Text = "Menu";
            // 
            // m_menuBack
            // 
            this.m_menuBack.Text = "Back";
            this.m_menuBack.Click += new System.EventHandler(this.OnBackClick);
            // 
            // m_menuNext
            // 
            this.m_menuNext.Text = "Next";
            this.m_menuNext.Click += new System.EventHandler(this.OnNextClick);
            // 
            // m_txtMessage
            // 
            this.m_txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtMessage.Location = new System.Drawing.Point(0, 0);
            this.m_txtMessage.Multiline = true;
            this.m_txtMessage.Name = "m_txtMessage";
            this.m_txtMessage.ReadOnly = true;
            this.m_txtMessage.Size = new System.Drawing.Size(240, 268);
            this.m_txtMessage.TabIndex = 0;
            this.m_txtMessage.Text = "Oil change due";
            // 
            // Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_txtMessage);
            this.Menu = this.mainMenu1;
            this.Name = "Messages";
            this.Text = "0110 Messages";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem m_menuBack;
        private System.Windows.Forms.MenuItem m_menuNext;
        private System.Windows.Forms.TextBox m_txtMessage;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}