namespace Dalworth.Windows
{
    partial class MessageErrorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageErrorDialog));
            this.m_pnCenter = new System.Windows.Forms.Panel();
            this.m_txtDetail = new System.Windows.Forms.TextBox();
            this.m_pnTop = new System.Windows.Forms.Panel();
            this.m_linkDetails = new System.Windows.Forms.LinkLabel();
            this.m_lbMessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_pnCenter.SuspendLayout();
            this.m_pnTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnCenter
            // 
            this.m_pnCenter.Controls.Add(this.m_txtDetail);
            this.m_pnCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnCenter.Location = new System.Drawing.Point(0, 72);
            this.m_pnCenter.Name = "m_pnCenter";
            this.m_pnCenter.Size = new System.Drawing.Size(240, 222);
            // 
            // m_txtDetail
            // 
            this.m_txtDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtDetail.Location = new System.Drawing.Point(0, 0);
            this.m_txtDetail.Multiline = true;
            this.m_txtDetail.Name = "m_txtDetail";
            this.m_txtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_txtDetail.Size = new System.Drawing.Size(240, 222);
            this.m_txtDetail.TabIndex = 2;
            this.m_txtDetail.Visible = false;
            // 
            // m_pnTop
            // 
            this.m_pnTop.Controls.Add(this.m_linkDetails);
            this.m_pnTop.Controls.Add(this.m_lbMessage);
            this.m_pnTop.Controls.Add(this.pictureBox1);
            this.m_pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnTop.Location = new System.Drawing.Point(0, 0);
            this.m_pnTop.Name = "m_pnTop";
            this.m_pnTop.Size = new System.Drawing.Size(240, 72);
            // 
            // m_linkDetails
            // 
            this.m_linkDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_linkDetails.Location = new System.Drawing.Point(164, 49);
            this.m_linkDetails.Name = "m_linkDetails";
            this.m_linkDetails.Size = new System.Drawing.Size(69, 20);
            this.m_linkDetails.TabIndex = 17;
            this.m_linkDetails.Text = "Details >>";
            this.m_linkDetails.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.m_linkDetails.Click += new System.EventHandler(this.OnDetailsClick);
            // 
            // m_lbMessage
            // 
            this.m_lbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lbMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lbMessage.Location = new System.Drawing.Point(56, 4);
            this.m_lbMessage.Name = "m_lbMessage";
            this.m_lbMessage.Size = new System.Drawing.Size(177, 42);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            // 
            // MessageErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_pnCenter);
            this.Controls.Add(this.m_pnTop);
            this.Name = "MessageErrorDialog";
            this.Text = "Error Report";
            this.m_pnCenter.ResumeLayout(false);
            this.m_pnTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnCenter;
        private System.Windows.Forms.TextBox m_txtDetail;
        private System.Windows.Forms.Panel m_pnTop;
        private System.Windows.Forms.Label m_lbMessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel m_linkDetails;

    }
}