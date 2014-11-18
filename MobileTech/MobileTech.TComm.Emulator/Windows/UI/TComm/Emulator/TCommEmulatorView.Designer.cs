namespace MobileTech.Windows.UI.TComm.Emulator
{
    partial class TCommEmulatorView
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
            this.m_btDone = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_progress = new System.Windows.Forms.ProgressBar();
            this.m_btBegin = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_lbProgressMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_btDone
            // 
            this.m_btDone.BackColor = System.Drawing.Color.White;
            this.m_btDone.BackDownColor = System.Drawing.Color.Black;
            this.m_btDone.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_btDone.ForeDownColor = System.Drawing.Color.White;
            this.m_btDone.IconMargin = 3;
            this.m_btDone.IconShift = false;
            this.m_btDone.IconTextSpace = 3;
            this.m_btDone.Location = new System.Drawing.Point(188, 227);
            this.m_btDone.Name = "m_btDone";
            this.m_btDone.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.m_btDone.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_btDone.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_btDone.ShowBorder = true;
            this.m_btDone.ShowFocusBorder = true;
            this.m_btDone.Size = new System.Drawing.Size(48, 64);
            this.m_btDone.TabIndex = 11;
            this.m_btDone.Text = "Done";
            this.m_btDone.TextShift = false;
            this.m_btDone.TransparentIcon = true;
            this.m_btDone.TransparentImage = true;
            this.m_btDone.Click += new System.EventHandler(this.OnDoneClick);
            // 
            // m_progress
            // 
            this.m_progress.Location = new System.Drawing.Point(3, 197);
            this.m_progress.Name = "m_progress";
            this.m_progress.Size = new System.Drawing.Size(234, 24);
            // 
            // m_btBegin
            // 
            this.m_btBegin.BackColor = System.Drawing.Color.White;
            this.m_btBegin.BackDownColor = System.Drawing.Color.Black;
            this.m_btBegin.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_btBegin.ForeDownColor = System.Drawing.Color.White;
            this.m_btBegin.IconMargin = 3;
            this.m_btBegin.IconShift = false;
            this.m_btBegin.IconTextSpace = 3;
            this.m_btBegin.Location = new System.Drawing.Point(3, 227);
            this.m_btBegin.Name = "m_btBegin";
            this.m_btBegin.Picture = MobileTech.Windows.UI.ImageKeys.Select_Small;
            this.m_btBegin.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Select_SmallDisabled;
            this.m_btBegin.PictureFocus = MobileTech.Windows.UI.ImageKeys.Select_SmallFocus;
            this.m_btBegin.ShowBorder = true;
            this.m_btBegin.ShowFocusBorder = true;
            this.m_btBegin.Size = new System.Drawing.Size(48, 64);
            this.m_btBegin.TabIndex = 10;
            this.m_btBegin.Text = "Start";
            this.m_btBegin.TextShift = false;
            this.m_btBegin.TransparentIcon = true;
            this.m_btBegin.TransparentImage = true;
            this.m_btBegin.Click += new System.EventHandler(this.OnBeginClick);
            // 
            // m_lbProgressMessage
            // 
            this.m_lbProgressMessage.Location = new System.Drawing.Point(3, 174);
            this.m_lbProgressMessage.Name = "m_lbProgressMessage";
            this.m_lbProgressMessage.Size = new System.Drawing.Size(233, 20);
            // 
            // TCommEmulatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_lbProgressMessage);
            this.Controls.Add(this.m_btDone);
            this.Controls.Add(this.m_progress);
            this.Controls.Add(this.m_btBegin);
            this.Name = "TCommEmulatorView";
            this.Text = "7000 - Transmit Data";
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.MenuButton m_btDone;
        private System.Windows.Forms.ProgressBar m_progress;
        private MobileTech.Windows.UI.Controls.MenuButton m_btBegin;
        private System.Windows.Forms.Label m_lbProgressMessage;

    }
}