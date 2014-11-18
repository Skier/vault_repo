namespace QuickBooksAgent.Windows.UI
{
    partial class MessageInformationDialog
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
            this.m_pnTop = new System.Windows.Forms.Panel();
            this.m_txMessage = new System.Windows.Forms.TextBox();
            this.m_pnBottom = new System.Windows.Forms.Panel();
            this.m_mbOk = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_pnTop.SuspendLayout();
            this.m_pnBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnTop
            // 
            this.m_pnTop.Controls.Add(this.m_txMessage);
            this.m_pnTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnTop.Location = new System.Drawing.Point(0, 0);
            this.m_pnTop.Name = "m_pnTop";
            this.m_pnTop.Size = new System.Drawing.Size(240, 236);
            // 
            // m_txMessage
            // 
            this.m_txMessage.BackColor = System.Drawing.Color.White;
            this.m_txMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txMessage.Enabled = false;
            this.m_txMessage.ForeColor = System.Drawing.Color.Black;
            this.m_txMessage.Location = new System.Drawing.Point(0, 0);
            this.m_txMessage.Multiline = true;
            this.m_txMessage.Name = "m_txMessage";
            this.m_txMessage.Size = new System.Drawing.Size(240, 236);
            this.m_txMessage.TabIndex = 1;
            // 
            // m_pnBottom
            // 
            this.m_pnBottom.Controls.Add(this.m_mbOk);
            this.m_pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnBottom.Location = new System.Drawing.Point(0, 236);
            this.m_pnBottom.Name = "m_pnBottom";
            this.m_pnBottom.Size = new System.Drawing.Size(240, 58);
            // 
            // m_mbOk
            // 
            this.m_mbOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mbOk.Location = new System.Drawing.Point(192, 6);
            this.m_mbOk.Name = "m_mbOk";
            this.m_mbOk.Picture = QuickBooksAgent.Windows.UI.ImageKeys.None;
            this.m_mbOk.ShowBorder = true;
            this.m_mbOk.Size = new System.Drawing.Size(48, 50);
            this.m_mbOk.TabIndex = 6;
            this.m_mbOk.Text = "OK";
            this.m_mbOk.Click += new System.EventHandler(this.m_mbOk_Click);
            // 
            // MessageInformationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.ControlBox = false;
            this.Controls.Add(this.m_pnTop);
            this.Controls.Add(this.m_pnBottom);
            this.MinimizeBox = false;
            this.Name = "MessageInformationDialog";
            this.Text = "MessageInformationDialog";
            this.m_pnTop.ResumeLayout(false);
            this.m_pnBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnTop;
        private System.Windows.Forms.TextBox m_txMessage;
        private System.Windows.Forms.Panel m_pnBottom;
        private QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbOk;



    }
}