namespace Dalworth.Windows.ServiceVisit.PayByCash
{
    partial class PayByCashView
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
            this.m_lblAmountDue = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.m_lblTaskType = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblVisitNumber = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_lblAmountDue
            // 
            this.m_lblAmountDue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAmountDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAmountDue.Location = new System.Drawing.Point(82, 57);
            this.m_lblAmountDue.Name = "m_lblAmountDue";
            this.m_lblAmountDue.Size = new System.Drawing.Size(153, 20);
            this.m_lblAmountDue.Text = "$15.00";
            this.m_lblAmountDue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(3, 57);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(77, 20);
            this.label35.Text = "Amount Due";
            // 
            // m_lblTaskType
            // 
            this.m_lblTaskType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaskType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTaskType.Location = new System.Drawing.Point(71, 37);
            this.m_lblTaskType.Name = "m_lblTaskType";
            this.m_lblTaskType.Size = new System.Drawing.Size(166, 20);
            this.m_lblTaskType.Text = "Rug Pickup";
            this.m_lblTaskType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblCustomerName
            // 
            this.m_lblCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCustomerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblCustomerName.Location = new System.Drawing.Point(71, 20);
            this.m_lblCustomerName.Name = "m_lblCustomerName";
            this.m_lblCustomerName.Size = new System.Drawing.Size(166, 20);
            this.m_lblCustomerName.Text = "Love, Rob";
            this.m_lblCustomerName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblVisitNumber
            // 
            this.m_lblVisitNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblVisitNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblVisitNumber.Location = new System.Drawing.Point(71, 3);
            this.m_lblVisitNumber.Name = "m_lblVisitNumber";
            this.m_lblVisitNumber.Size = new System.Drawing.Size(166, 20);
            this.m_lblVisitNumber.Text = "1001";
            this.m_lblVisitNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(3, 37);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(62, 20);
            this.label30.Text = "Task Type";
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(3, 20);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(62, 20);
            this.label31.Text = "Customer";
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(3, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(62, 20);
            this.label32.Text = "TKT";
            // 
            // PayByCashView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_lblAmountDue);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.m_lblTaskType);
            this.Controls.Add(this.m_lblCustomerName);
            this.Controls.Add(this.m_lblVisitNumber);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label32);
            this.Name = "PayByCashView";
            this.Size = new System.Drawing.Size(240, 268);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label m_lblAmountDue;
        private System.Windows.Forms.Label label35;
        internal System.Windows.Forms.Label m_lblTaskType;
        internal System.Windows.Forms.Label m_lblCustomerName;
        internal System.Windows.Forms.Label m_lblVisitNumber;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
    }
}
