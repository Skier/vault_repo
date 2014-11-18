namespace Dalworth.Windows.ServiceVisit.PayByCheckDone
{
    partial class PayByCheckDoneView
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
            this.m_lblAmount = new System.Windows.Forms.Label();
            this.m_lblResponse = new System.Windows.Forms.Label();
            this.m_lblCheckNumber = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_lblAmount
            // 
            this.m_lblAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAmount.Location = new System.Drawing.Point(69, 55);
            this.m_lblAmount.Name = "m_lblAmount";
            this.m_lblAmount.Size = new System.Drawing.Size(167, 20);
            this.m_lblAmount.Text = "$560.12";
            this.m_lblAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblResponse
            // 
            this.m_lblResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblResponse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblResponse.Location = new System.Drawing.Point(3, 94);
            this.m_lblResponse.Name = "m_lblResponse";
            this.m_lblResponse.Size = new System.Drawing.Size(233, 75);
            this.m_lblResponse.Text = "782366018659";
            // 
            // m_lblCheckNumber
            // 
            this.m_lblCheckNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCheckNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblCheckNumber.Location = new System.Drawing.Point(96, 38);
            this.m_lblCheckNumber.Name = "m_lblCheckNumber";
            this.m_lblCheckNumber.Size = new System.Drawing.Size(140, 20);
            this.m_lblCheckNumber.Text = "2323344";
            this.m_lblCheckNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.Text = "Amount";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 20);
            this.label3.Text = "Response";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.Text = "Check Number";
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblStatus.Location = new System.Drawing.Point(3, 3);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(233, 35);
            this.m_lblStatus.Text = "Bank check payment has been successfully processed";
            // 
            // PayByCheckDoneView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_lblAmount);
            this.Controls.Add(this.m_lblResponse);
            this.Controls.Add(this.m_lblCheckNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_lblStatus);
            this.Name = "PayByCheckDoneView";
            this.Size = new System.Drawing.Size(240, 268);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label m_lblAmount;
        internal System.Windows.Forms.Label m_lblResponse;
        internal System.Windows.Forms.Label m_lblCheckNumber;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label m_lblStatus;
    }
}
