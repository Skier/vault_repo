namespace MobileTech.Windows.UI.Odometer
{
    partial class OdometerView
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
            this.m_lbOdometerReading = new System.Windows.Forms.Label();
            this.m_mbDone = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_txtReading = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_lbOdometerReading
            // 
            this.m_lbOdometerReading.Location = new System.Drawing.Point(13, 81);
            this.m_lbOdometerReading.Name = "m_lbOdometerReading";
            this.m_lbOdometerReading.Size = new System.Drawing.Size(118, 20);
            this.m_lbOdometerReading.TabIndex = 6;
            this.m_lbOdometerReading.Text = "Odometer Reading:";
            // 
            // m_mbDone
            // 
            this.m_mbDone.BackDownColor = System.Drawing.Color.Black;
            this.m_mbDone.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbDone.ForeDownColor = System.Drawing.Color.White;
            this.m_mbDone.IconMargin = 3;
            this.m_mbDone.IconShift = false;
            this.m_mbDone.IconTextSpace = 3;
            this.m_mbDone.ImageDown = null;
            this.m_mbDone.ImageUp = null;
            this.m_mbDone.Location = new System.Drawing.Point(191, 233);
            this.m_mbDone.Name = "m_mbDone";
            this.m_mbDone.Picture = MobileTech.Windows.UI.ImageKeys.Detail_Small;
            this.m_mbDone.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_mbDone.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_mbDone.ShowBorder = true;
            this.m_mbDone.ShowFocusBorder = true;
            this.m_mbDone.Size = new System.Drawing.Size(46, 58);
            this.m_mbDone.TabIndex = 5;
            this.m_mbDone.Text = "Done";
            this.m_mbDone.TextShift = false;
            this.m_mbDone.TransparentIcon = true;
            this.m_mbDone.TransparentImage = true;
            this.m_mbDone.Click += new System.EventHandler(this.OnDoneClick);
            // 
            // m_txtReading
            // 
            this.m_txtReading.Location = new System.Drawing.Point(128, 78);
            this.m_txtReading.Name = "m_txtReading";
            this.m_txtReading.Size = new System.Drawing.Size(100, 20);
            this.m_txtReading.TabIndex = 1;
            // 
            // OdometerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_txtReading);
            this.Controls.Add(this.m_mbDone);
            this.Controls.Add(this.m_lbOdometerReading);
            this.Name = "OdometerView";
            this.Text = "9080 - Odometer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lbOdometerReading;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbDone;
        private System.Windows.Forms.TextBox m_txtReading;
    }
}