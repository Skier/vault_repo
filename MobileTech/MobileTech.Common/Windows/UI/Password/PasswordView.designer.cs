namespace MobileTech.Windows.UI.Password
{
    partial class PasswordView
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
            this.m_lbPassword = new System.Windows.Forms.Label();
            this.m_mbDone = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_lbPass = new System.Windows.Forms.Label();
            this.m_txtPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_lbPassword
            // 
            this.m_lbPassword.Location = new System.Drawing.Point(23, 72);
            this.m_lbPassword.Name = "m_lbPassword";
            this.m_lbPassword.Size = new System.Drawing.Size(96, 20);
            this.m_lbPassword.Text = "Enter Password:";
            // 
            // m_mbDone
            // 
            this.m_mbDone.BackDownColor = System.Drawing.Color.Black;
            this.m_mbDone.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbDone.ForeDownColor = System.Drawing.Color.White;
            this.m_mbDone.IconMargin = 3;
            this.m_mbDone.IconShift = false;
            this.m_mbDone.IconTextSpace = 3;
            this.m_mbDone.Location = new System.Drawing.Point(192, 230);
            this.m_mbDone.Name = "m_mbDone";
            this.m_mbDone.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.m_mbDone.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_mbDone.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_mbDone.ShowBorder = true;
            this.m_mbDone.ShowFocusBorder = true;
            this.m_mbDone.Size = new System.Drawing.Size(48, 64);
            this.m_mbDone.TabIndex = 8;
            this.m_mbDone.Text = "Done";
            this.m_mbDone.TextShift = false;
            this.m_mbDone.TransparentIcon = true;
            this.m_mbDone.TransparentImage = true;
            this.m_mbDone.Click += new System.EventHandler(this.OnDoneClick);
            // 
            // m_lbPass
            // 
            this.m_lbPass.Location = new System.Drawing.Point(23, 72);
            this.m_lbPass.Name = "m_lbPass";
            this.m_lbPass.Size = new System.Drawing.Size(96, 20);
            this.m_lbPass.Text = "Enter Password:";
            // 
            // m_txtPassword
            // 
            this.m_txtPassword.Location = new System.Drawing.Point(122, 71);
            this.m_txtPassword.Name = "m_txtPassword";
            this.m_txtPassword.Size = new System.Drawing.Size(115, 21);
            this.m_txtPassword.TabIndex = 1;
            this.m_txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // PasswordView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_txtPassword);
            this.Controls.Add(this.m_mbDone);
            this.Controls.Add(this.m_lbPassword);
            this.Name = "PasswordView";
            this.Text = "9090 - Password";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lbPassword;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbDone;
        private System.Windows.Forms.Label m_lbPass;
        private System.Windows.Forms.TextBox m_txtPassword;

    }
}