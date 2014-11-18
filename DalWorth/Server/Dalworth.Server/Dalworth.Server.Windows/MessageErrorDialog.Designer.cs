using System.Windows.Forms;

namespace Dalworth.Server.Windows
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
            this.m_txtDetail = new System.Windows.Forms.TextBox();
            this.m_btnDone = new Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_lbMessage = new System.Windows.Forms.Label();
            this.m_btDetail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_txtDetail
            // 
            this.m_txtDetail.Location = new System.Drawing.Point(3, 76);
            this.m_txtDetail.Multiline = true;
            this.m_txtDetail.Name = "m_txtDetail";
            this.m_txtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_txtDetail.Size = new System.Drawing.Size(234, 148);
            this.m_txtDetail.TabIndex = 1;
            this.m_txtDetail.Visible = false;
            // 
            // m_mbDone
            // 
            this.m_btnDone.BackColor = System.Drawing.Color.White;
            this.m_btnDone.Location = new System.Drawing.Point(192, 230);
            this.m_btnDone.Name = "m_btnDone";
            this.m_btnDone.Size = new System.Drawing.Size(48, 64);
            this.m_btnDone.TabIndex = 2;
            this.m_btnDone.Text = "Done";
            this.m_btnDone.Click += new System.EventHandler(this.OnDoneClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            // 
            // m_lbMessage
            // 
            this.m_lbMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lbMessage.Location = new System.Drawing.Point(60, 5);
            this.m_lbMessage.Name = "m_lbMessage";
            this.m_lbMessage.Size = new System.Drawing.Size(177, 42);
            // 
            // m_btDetail
            // 
            this.m_btDetail.Location = new System.Drawing.Point(165, 51);
            this.m_btDetail.Name = "m_btDetail";
            this.m_btDetail.Size = new System.Drawing.Size(72, 20);
            this.m_btDetail.TabIndex = 5;
            this.m_btDetail.Text = "Detail >>";
            this.m_btDetail.Click += new System.EventHandler(this.OnDetailClick);
            // 
            // MessageErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_btDetail);
            this.Controls.Add(this.m_lbMessage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.m_btnDone);
            this.Controls.Add(this.m_txtDetail);
            this.Name = "MessageErrorDialog";
            this.Text = "Error Report";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtDetail;
        private Button m_btnDone;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label m_lbMessage;
        private System.Windows.Forms.Button m_btDetail;
    }
}