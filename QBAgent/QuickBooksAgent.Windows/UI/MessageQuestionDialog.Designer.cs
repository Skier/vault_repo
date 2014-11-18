namespace QuickBooksAgent.Windows.UI
{
    partial class MessageQuestionDialog
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
            this.m_pnBottom = new System.Windows.Forms.Panel();
            this.m_mbNo = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbYes = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_pnTop = new System.Windows.Forms.Panel();
            this.m_txMessage = new System.Windows.Forms.TextBox();
            this.m_pnBottom.SuspendLayout();
            this.m_pnTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnBottom
            // 
            this.m_pnBottom.Controls.Add(this.m_mbNo);
            this.m_pnBottom.Controls.Add(this.m_mbYes);
            this.m_pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnBottom.Location = new System.Drawing.Point(0, 226);
            this.m_pnBottom.Name = "m_pnBottom";
            this.m_pnBottom.Size = new System.Drawing.Size(240, 68);
            // 
            // m_mbNo
            // 
            this.m_mbNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_mbNo.Location = new System.Drawing.Point(0, 5);
            this.m_mbNo.Name = "m_mbNo";
            this.m_mbNo.Picture = QuickBooksAgent.Windows.UI.ImageKeys.None;
            this.m_mbNo.ShowBorder = true;
            this.m_mbNo.Size = new System.Drawing.Size(48, 64);
            this.m_mbNo.TabIndex = 6;
            this.m_mbNo.Text = "No";
            this.m_mbNo.Click += new System.EventHandler(this.OnNoClick);
            // 
            // m_mbYes
            // 
            this.m_mbYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mbYes.Location = new System.Drawing.Point(192, 5);
            this.m_mbYes.Name = "m_mbYes";
            this.m_mbYes.Picture = QuickBooksAgent.Windows.UI.ImageKeys.None;
            this.m_mbYes.ShowBorder = true;
            this.m_mbYes.Size = new System.Drawing.Size(48, 64);
            this.m_mbYes.TabIndex = 7;
            this.m_mbYes.Text = "Yes";
            this.m_mbYes.Click += new System.EventHandler(this.OnYesClick);
            // 
            // m_pnTop
            // 
            this.m_pnTop.Controls.Add(this.m_txMessage);
            this.m_pnTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnTop.Location = new System.Drawing.Point(0, 0);
            this.m_pnTop.Name = "m_pnTop";
            this.m_pnTop.Size = new System.Drawing.Size(240, 226);
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
            this.m_txMessage.Size = new System.Drawing.Size(240, 226);
            this.m_txMessage.TabIndex = 1;
            // 
            // MessageQuestionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.ControlBox = false;
            this.Controls.Add(this.m_pnTop);
            this.Controls.Add(this.m_pnBottom);
            this.MinimizeBox = false;
            this.Name = "MessageQuestionDialog";
            this.Text = "Q-Agent";
            this.m_pnBottom.ResumeLayout(false);
            this.m_pnTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnBottom;
        private QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbNo;
        private QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbYes;
        private System.Windows.Forms.Panel m_pnTop;
        private System.Windows.Forms.TextBox m_txMessage;


    }
}