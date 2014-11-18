namespace QuickBooksAgent.Windows.UI.Setup.Menu
{
    partial class SetupMenuView
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
            this.m_mbApplication = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbConnection = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_btnRegister = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_btnAbout = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_mbApplication
            // 
            this.m_mbApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mbApplication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbApplication.Location = new System.Drawing.Point(122, 3);
            this.m_mbApplication.Name = "m_mbApplication";
            this.m_mbApplication.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Application;
            this.m_mbApplication.ShowBorder = true;
            this.m_mbApplication.Size = new System.Drawing.Size(115, 82);
            this.m_mbApplication.TabIndex = 3;
            this.m_mbApplication.Tag = "";
            this.m_mbApplication.Text = "Application";
            // 
            // m_mbConnection
            // 
            this.m_mbConnection.Enabled = false;
            this.m_mbConnection.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbConnection.Location = new System.Drawing.Point(122, 91);
            this.m_mbConnection.Name = "m_mbConnection";
            this.m_mbConnection.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Connection;
            this.m_mbConnection.ShowBorder = true;
            this.m_mbConnection.Size = new System.Drawing.Size(115, 82);
            this.m_mbConnection.TabIndex = 2;
            this.m_mbConnection.Tag = "";
            this.m_mbConnection.Text = "Connection";
            this.m_mbConnection.Visible = false;
            // 
            // m_btnRegister
            // 
            this.m_btnRegister.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_btnRegister.Location = new System.Drawing.Point(3, 3);
            this.m_btnRegister.Name = "m_btnRegister";
            this.m_btnRegister.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Register;
            this.m_btnRegister.ShowBorder = true;
            this.m_btnRegister.Size = new System.Drawing.Size(115, 82);
            this.m_btnRegister.TabIndex = 4;
            this.m_btnRegister.Tag = "";
            this.m_btnRegister.Text = "Register";
            // 
            // m_btnAbout
            // 
            this.m_btnAbout.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_btnAbout.Location = new System.Drawing.Point(3, 91);
            this.m_btnAbout.Name = "m_btnAbout";
            this.m_btnAbout.Picture = QuickBooksAgent.Windows.UI.ImageKeys.About;
            this.m_btnAbout.ShowBorder = true;
            this.m_btnAbout.Size = new System.Drawing.Size(115, 82);
            this.m_btnAbout.TabIndex = 5;
            this.m_btnAbout.Tag = "";
            this.m_btnAbout.Text = "About";
            // 
            // SetupMenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_btnAbout);
            this.Controls.Add(this.m_btnRegister);
            this.Controls.Add(this.m_mbApplication);
            this.Controls.Add(this.m_mbConnection);
            this.Name = "SetupMenuView";
            this.Size = new System.Drawing.Size(240, 294);
            this.ResumeLayout(false);

        }

        #endregion

        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbApplication;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbConnection;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_btnRegister;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_btnAbout;
    }
}
