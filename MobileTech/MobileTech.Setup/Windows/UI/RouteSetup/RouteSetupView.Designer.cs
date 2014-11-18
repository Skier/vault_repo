namespace MobileTech.Windows.UI.RouteSetup
{
    partial class RouteSetupView
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
            this.m_lbRoute = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtLocation = new MobileTech.Windows.UI.Controls.MaskedEdit();
            this.m_txtRoute = new MobileTech.Windows.UI.Controls.MaskedEdit();
            this.m_mbAccept = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_lbRoute
            // 
            this.m_lbRoute.Location = new System.Drawing.Point(3, 47);
            this.m_lbRoute.Name = "m_lbRoute";
            this.m_lbRoute.Size = new System.Drawing.Size(72, 21);
            this.m_lbRoute.Text = "Route";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.Text = "Location";
            // 
            // m_txtLocation
            // 
            this.m_txtLocation.ErrorInvalid = true;
            this.m_txtLocation.Location = new System.Drawing.Point(81, 11);
            this.m_txtLocation.MaxLength = 3;
            this.m_txtLocation.Name = "m_txtLocation";
            this.m_txtLocation.Size = new System.Drawing.Size(36, 21);
            this.m_txtLocation.TabIndex = 1;
            this.m_txtLocation.Text = "0__";
            this.m_txtLocation.vStdyInputMask = MobileTech.Windows.UI.Controls.MaskedEdit.yInputMaskType.Custom;
            this.m_txtLocation.wInputChar = '_';
            this.m_txtLocation.yInputMask = "099";
            this.m_txtLocation.zValue = "0";
            this.m_txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.control_KeyPress);
            // 
            // m_txtRoute
            // 
            this.m_txtRoute.ErrorInvalid = true;
            this.m_txtRoute.Location = new System.Drawing.Point(81, 47);
            this.m_txtRoute.MaxLength = 3;
            this.m_txtRoute.Name = "m_txtRoute";
            this.m_txtRoute.Size = new System.Drawing.Size(36, 21);
            this.m_txtRoute.TabIndex = 2;
            this.m_txtRoute.Text = "0__";
            this.m_txtRoute.vStdyInputMask = MobileTech.Windows.UI.Controls.MaskedEdit.yInputMaskType.Custom;
            this.m_txtRoute.wInputChar = '_';
            this.m_txtRoute.yInputMask = "099";
            this.m_txtRoute.zValue = "0";
            this.m_txtRoute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.control_KeyPress);
            // 
            // m_mbAccept
            // 
            this.m_mbAccept.BackColor = System.Drawing.Color.White;
            this.m_mbAccept.Location = new System.Drawing.Point(192, 230);
            this.m_mbAccept.Name = "m_mbAccept";
            this.m_mbAccept.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.m_mbAccept.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_mbAccept.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_mbAccept.ShowBorder = true;
            this.m_mbAccept.ShowFocusBorder = true;
            this.m_mbAccept.Size = new System.Drawing.Size(48, 64);
            this.m_mbAccept.TabIndex = 3;
            this.m_mbAccept.Text = "Done";
            this.m_mbAccept.Click += new System.EventHandler(this.OnAcceptClick);
            // 
            // RouteSetupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_mbAccept);
            this.Controls.Add(this.m_txtRoute);
            this.Controls.Add(this.m_txtLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_lbRoute);
            this.Name = "RouteSetupView";
            this.Text = "8100 - Route Setup";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lbRoute;
        private System.Windows.Forms.Label label1;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbAccept;
        private MobileTech.Windows.UI.Controls.MaskedEdit m_txtLocation;
        private MobileTech.Windows.UI.Controls.MaskedEdit m_txtRoute;
    }

}