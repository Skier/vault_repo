namespace Dalworth.Windows.ServiceVisit.NoGo
{
    partial class NoGoView
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
            this.m_btnNo = new System.Windows.Forms.Button();
            this.m_btnYes = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_btnNo
            // 
            this.m_btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnNo.Location = new System.Drawing.Point(119, 93);
            this.m_btnNo.Name = "m_btnNo";
            this.m_btnNo.Size = new System.Drawing.Size(72, 20);
            this.m_btnNo.TabIndex = 7;
            this.m_btnNo.Text = "No";
            // 
            // m_btnYes
            // 
            this.m_btnYes.Location = new System.Drawing.Point(41, 93);
            this.m_btnYes.Name = "m_btnYes";
            this.m_btnYes.Size = new System.Drawing.Size(72, 20);
            this.m_btnYes.TabIndex = 6;
            this.m_btnYes.Text = "Yes";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 35);
            this.label2.Text = "Did dispatcher confirm NO GO?";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 33);
            this.label1.Text = "Please call dispatch to confirm NO GO";
            // 
            // NoGoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_btnNo);
            this.Controls.Add(this.m_btnYes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NoGoView";
            this.Size = new System.Drawing.Size(240, 268);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button m_btnNo;
        internal System.Windows.Forms.Button m_btnYes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
