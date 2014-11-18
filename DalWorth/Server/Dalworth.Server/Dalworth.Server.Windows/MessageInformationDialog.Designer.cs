using System.Windows.Forms;

namespace Dalworth.Server.Windows
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
            this.m_txMessage = new System.Windows.Forms.TextBox();
            this.m_btnOk = new Button();
            this.SuspendLayout();
            // 
            // m_txMessage
            // 
            this.m_txMessage.BackColor = System.Drawing.Color.White;
            this.m_txMessage.Enabled = false;
            this.m_txMessage.ForeColor = System.Drawing.Color.Black;
            this.m_txMessage.Location = new System.Drawing.Point(3, 59);
            this.m_txMessage.Multiline = true;
            this.m_txMessage.Name = "m_txMessage";
            this.m_txMessage.Size = new System.Drawing.Size(234, 105);
            this.m_txMessage.TabIndex = 0;
            // 
            // m_mbOk
            // 

            this.m_btnOk.Location = new System.Drawing.Point(192, 230);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(48, 64);
            this.m_btnOk.TabIndex = 5;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.Click += new System.EventHandler(this.m_mbOk_Click);
            // 
            // MessageInformationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.ControlBox = false;
            this.Controls.Add(this.m_txMessage);
            this.Controls.Add(this.m_btnOk);
            this.MinimizeBox = false;
            this.Name = "MessageInformationDialog";
            this.Text = "MessageInformationDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_txMessage;
        private Button m_btnOk;


    }
}