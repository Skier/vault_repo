namespace Dalworth.Windows.ServiceVisit.ReceiveVisit
{
    partial class ReceiveVisitView
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
            this.m_lblDate = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblTaskType = new System.Windows.Forms.Label();
            this.m_lblTaskNumber = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_txtNotes = new System.Windows.Forms.TextBox();
            this.m_btnAccept = new System.Windows.Forms.Button();
            this.m_timerBlink = new System.Windows.Forms.Timer();
            this.m_timerSound = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // m_lblDate
            // 
            this.m_lblDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblDate.Location = new System.Drawing.Point(3, 3);
            this.m_lblDate.Name = "m_lblDate";
            this.m_lblDate.Size = new System.Drawing.Size(117, 20);
            this.m_lblDate.Text = "Thu, Apr 26 2007";
            // 
            // m_lblCustomerName
            // 
            this.m_lblCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCustomerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblCustomerName.Location = new System.Drawing.Point(4, 22);
            this.m_lblCustomerName.Name = "m_lblCustomerName";
            this.m_lblCustomerName.Size = new System.Drawing.Size(233, 20);
            this.m_lblCustomerName.Text = "Price, Gwen";
            // 
            // m_lblTaskType
            // 
            this.m_lblTaskType.Location = new System.Drawing.Point(4, 42);
            this.m_lblTaskType.Name = "m_lblTaskType";
            this.m_lblTaskType.Size = new System.Drawing.Size(116, 20);
            this.m_lblTaskType.Text = "Rug Delivery";
            // 
            // m_lblTaskNumber
            // 
            this.m_lblTaskNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaskNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTaskNumber.Location = new System.Drawing.Point(129, 42);
            this.m_lblTaskNumber.Name = "m_lblTaskNumber";
            this.m_lblTaskNumber.Size = new System.Drawing.Size(111, 20);
            this.m_lblTaskNumber.Text = "TKT: 1004";
            this.m_lblTaskNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(4, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Notes:";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNotes.Location = new System.Drawing.Point(3, 82);
            this.m_txtNotes.Multiline = true;
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.ReadOnly = true;
            this.m_txtNotes.Size = new System.Drawing.Size(233, 44);
            this.m_txtNotes.TabIndex = 14;
            this.m_txtNotes.Text = "Deliver 2 rugs";
            // 
            // m_btnAccept
            // 
            this.m_btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAccept.BackColor = System.Drawing.Color.White;
            this.m_btnAccept.Location = new System.Drawing.Point(69, 240);
            this.m_btnAccept.Name = "m_btnAccept";
            this.m_btnAccept.Size = new System.Drawing.Size(111, 25);
            this.m_btnAccept.TabIndex = 21;
            this.m_btnAccept.Text = "Accept";
            // 
            // m_timerBlink
            // 
            this.m_timerBlink.Interval = 500;
            // 
            // m_timerSound
            // 
            this.m_timerSound.Interval = 5000;
            // 
            // ReceiveVisitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_btnAccept);
            this.Controls.Add(this.m_txtNotes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_lblTaskNumber);
            this.Controls.Add(this.m_lblTaskType);
            this.Controls.Add(this.m_lblCustomerName);
            this.Controls.Add(this.m_lblDate);
            this.Name = "ReceiveVisitView";
            this.Size = new System.Drawing.Size(240, 268);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label m_lblDate;
        internal System.Windows.Forms.Label m_lblCustomerName;
        internal System.Windows.Forms.Label m_lblTaskType;
        internal System.Windows.Forms.Label m_lblTaskNumber;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox m_txtNotes;
        internal System.Windows.Forms.Button m_btnAccept;
        internal System.Windows.Forms.Timer m_timerBlink;
        internal System.Windows.Forms.Timer m_timerSound;

    }
}
