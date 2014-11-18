namespace MobileTech.Windows.UI.EndDay
{
    partial class EndDayView
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
            this.m_mbEndDay = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_mbEndDay
            // 
            this.m_mbEndDay.BackColor = System.Drawing.Color.White;
            this.m_mbEndDay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbEndDay.Location = new System.Drawing.Point(123, 225);
            this.m_mbEndDay.Name = "m_mbEndDay";
            this.m_mbEndDay.Picture = MobileTech.Windows.UI.ImageKeys.EndDayDone;
            this.m_mbEndDay.PictureDisabled = MobileTech.Windows.UI.ImageKeys.EndDayDoneDisabled;
            this.m_mbEndDay.PictureFocus = MobileTech.Windows.UI.ImageKeys.EndDayDoneFocus;
            this.m_mbEndDay.ShowBorder = false;
            this.m_mbEndDay.ShowFocusBorder = true;
            this.m_mbEndDay.Size = new System.Drawing.Size(114, 66);
            this.m_mbEndDay.TabIndex = 1;
            this.m_mbEndDay.Text = "End Day Done";
            this.m_mbEndDay.Click += new System.EventHandler(this.OnEndDayClick);
            // 
            // EndDayView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_mbEndDay);
            this.Name = "EndDayView";
            this.Text = "4000 - End Day";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.MenuButton m_mbEndDay;
    }
}