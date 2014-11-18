namespace SmartSchedule.Win32.Controls
{
    partial class BaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.m_btnLock = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnLock)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnLock
            // 
            this.m_btnLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnLock.Image = ((System.Drawing.Image)(resources.GetObject("Locked")));
            this.m_btnLock.InitialImage = null;
            this.m_btnLock.Location = new System.Drawing.Point(12, 12);
            this.m_btnLock.Name = "m_btnLock";
            this.m_btnLock.Size = new System.Drawing.Size(24, 24);
            this.m_btnLock.TabIndex = 0;
            this.m_btnLock.TabStop = false;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(284, 245);
            this.Controls.Add(this.m_btnLock);
            this.Name = "BaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.m_btnLock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox m_btnLock;


    }
}