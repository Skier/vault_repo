using System.Windows.Forms;

namespace Dalworth.Server.Windows
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
            this.m_btnNo = new Button();
            this.m_btnYes = new Button();
            this.m_txMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_mbNo
            // 

            this.m_btnNo.Location = new System.Drawing.Point(0, 230);
            this.m_btnNo.Name = "m_btnNo";
            this.m_btnNo.Size = new System.Drawing.Size(48, 64);
            this.m_btnNo.TabIndex = 4;
            this.m_btnNo.Text = "No";
            this.m_btnNo.Click += new System.EventHandler(this.OnNoClick);
            // 
            // m_mbYes
            // 
            this.m_btnYes.Location = new System.Drawing.Point(192, 230);
            this.m_btnYes.Name = "m_btnYes";
            this.m_btnYes.Size = new System.Drawing.Size(48, 64);
            this.m_btnYes.TabIndex = 5;
            this.m_btnYes.Text = "Yes";
            this.m_btnYes.Click += new System.EventHandler(this.OnYesClick);
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
            // MessageQuestionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.ControlBox = false;
            this.Controls.Add(this.m_txMessage);
            this.Controls.Add(this.m_btnNo);
            this.Controls.Add(this.m_btnYes);
            this.MinimizeBox = false;
            this.Name = "MessageQuestionDialog";
            this.Text = "RouteNet";
            this.ResumeLayout(false);

        }

        #endregion

        private Button m_btnNo;
        private Button m_btnYes;
        private System.Windows.Forms.TextBox m_txMessage;

    }
}