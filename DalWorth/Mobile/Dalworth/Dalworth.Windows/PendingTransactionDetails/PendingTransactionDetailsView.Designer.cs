namespace Dalworth.Windows.PendingTransactionDetails
{
    partial class PendingTransactionDetailsView
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
            this.m_txtText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_txtText
            // 
            this.m_txtText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtText.Location = new System.Drawing.Point(0, 0);
            this.m_txtText.Multiline = true;
            this.m_txtText.Name = "m_txtText";
            this.m_txtText.ReadOnly = true;
            this.m_txtText.Size = new System.Drawing.Size(227, 255);
            this.m_txtText.TabIndex = 0;
            // 
            // PendingTransactionDetailsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_txtText);
            this.Name = "PendingTransactionDetailsView";
            this.Size = new System.Drawing.Size(227, 255);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TextBox m_txtText;
    }
}
