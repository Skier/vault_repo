namespace MobileTech.Windows.UI.Forms
{
    partial class ErrorView
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
            this.m_txtMessage = new System.Windows.Forms.TextBox();
            this.m_txtDetail = new System.Windows.Forms.TextBox();
            this.menuButton1 = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_txtMessage
            // 
            this.m_txtMessage.Location = new System.Drawing.Point(3, 3);
            this.m_txtMessage.Multiline = true;
            this.m_txtMessage.Name = "m_txtMessage";
            this.m_txtMessage.Size = new System.Drawing.Size(234, 50);
            this.m_txtMessage.TabIndex = 0;
            // 
            // m_txtDetail
            // 
            this.m_txtDetail.Location = new System.Drawing.Point(3, 59);
            this.m_txtDetail.Multiline = true;
            this.m_txtDetail.Name = "m_txtDetail";
            this.m_txtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_txtDetail.Size = new System.Drawing.Size(234, 165);
            this.m_txtDetail.TabIndex = 1;
            // 
            // menuButton1
            // 
            this.menuButton1.BackColor = System.Drawing.Color.White;
            this.menuButton1.Location = new System.Drawing.Point(192, 230);
            this.menuButton1.Name = "menuButton1";
            this.menuButton1.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.menuButton1.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.menuButton1.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.menuButton1.ShowBorder = true;
            this.menuButton1.ShowFocusBorder = true;
            this.menuButton1.Size = new System.Drawing.Size(48, 64);
            this.menuButton1.TabIndex = 2;
            this.menuButton1.Text = "Done";
            this.menuButton1.Click += new System.EventHandler(this.menuButton1_Click);
            // 
            // ErrorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.menuButton1);
            this.Controls.Add(this.m_txtDetail);
            this.Controls.Add(this.m_txtMessage);
            this.Name = "ErrorView";
            this.Text = "Error Report";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtMessage;
        private System.Windows.Forms.TextBox m_txtDetail;
        private MobileTech.Windows.UI.Controls.MenuButton menuButton1;
    }
}