namespace MobileTech.Windows.UI.CustomerOperations.Commit
{
    partial class CustomerOperationsCommitView
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
            this.m_lbInfo = new System.Windows.Forms.Label();
            this.m_mbFinal = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbBack = new MobileTech.Windows.UI.Controls.MenuButton();
 
            this.SuspendLayout();
            // 
            // m_lbInfo
            // 
            this.m_lbInfo.BackColor = System.Drawing.Color.White;
            this.m_lbInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lbInfo.Location = new System.Drawing.Point(3, 113);
            this.m_lbInfo.Name = "m_lbInfo";
            this.m_lbInfo.Size = new System.Drawing.Size(236, 44);
            this.m_lbInfo.Text = "Info";
            this.m_lbInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // m_mbFinal
            // 
            this.m_mbFinal.BackColor = System.Drawing.Color.White;
            this.m_mbFinal.BackDownColor = System.Drawing.Color.Black;
            this.m_mbFinal.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbFinal.ForeDownColor = System.Drawing.Color.White;
            this.m_mbFinal.IconMargin = 3;
            this.m_mbFinal.IconShift = false;
            this.m_mbFinal.IconTextSpace = 3;
            this.m_mbFinal.ImageDown = null;
            this.m_mbFinal.ImageUp = null;
            this.m_mbFinal.Location = new System.Drawing.Point(192, 230);
            this.m_mbFinal.Name = "m_mbFinal";
            this.m_mbFinal.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.m_mbFinal.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_mbFinal.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_mbFinal.ShowBorder = true;
            this.m_mbFinal.ShowFocusBorder = true;
            this.m_mbFinal.Size = new System.Drawing.Size(48, 64);
            this.m_mbFinal.TabIndex = 1;
            this.m_mbFinal.Text = "Final";
            this.m_mbFinal.TextShift = false;
            this.m_mbFinal.TransparentIcon = true;
            this.m_mbFinal.TransparentImage = true;
            this.m_mbFinal.Click += new System.EventHandler(this.OnFinalClick);
            // 
           // 
            // m_mbBack
            // 
            this.m_mbBack.BackColor = System.Drawing.Color.White;
            this.m_mbBack.BackDownColor = System.Drawing.Color.Black;
            this.m_mbBack.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbBack.ForeDownColor = System.Drawing.Color.White;
            this.m_mbBack.IconMargin = 3;
            this.m_mbBack.IconShift = false;
            this.m_mbBack.IconTextSpace = 3;
            this.m_mbBack.ImageDown = null;
            this.m_mbBack.ImageUp = null;
            this.m_mbBack.Location = new System.Drawing.Point(0, 230);
            this.m_mbBack.Name = "m_mbBack";
            this.m_mbBack.Picture = MobileTech.Windows.UI.ImageKeys.Back_Small;
            this.m_mbBack.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Back_SmallDisabled;
            this.m_mbBack.PictureFocus = MobileTech.Windows.UI.ImageKeys.Back_SmallFocus;
            this.m_mbBack.ShowBorder = true;
            this.m_mbBack.ShowFocusBorder = true;
            this.m_mbBack.Size = new System.Drawing.Size(48, 64);
            this.m_mbBack.TabIndex = 3;
            this.m_mbBack.Text = "Back";
            this.m_mbBack.TextShift = false;
            this.m_mbBack.TransparentIcon = true;
            this.m_mbBack.TransparentImage = true;
            this.m_mbBack.Click += new System.EventHandler(this.OnBackClick);
            // TransactionCompletionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_mbBack);
            this.Controls.Add(this.m_mbFinal);
            this.Controls.Add(this.m_lbInfo);
            this.Name = "TransactionCompletionView";
            this.Text = "3050 - Transaction Completion";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lbInfo;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbFinal;
     	private MobileTech.Windows.UI.Controls.MenuButton m_mbBack;
 
    }
}