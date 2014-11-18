namespace Dalworth.Windows.StartDay.WorkSummary
{
    partial class WorkSummaryView
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_lblTechnicianName = new System.Windows.Forms.Label();
            this.m_lblVanNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.Text = "Technician";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.Text = "Van Number";
            // 
            // m_lblTechnicianName
            // 
            this.m_lblTechnicianName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTechnicianName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTechnicianName.Location = new System.Drawing.Point(85, 2);
            this.m_lblTechnicianName.Name = "m_lblTechnicianName";
            this.m_lblTechnicianName.Size = new System.Drawing.Size(152, 20);
            this.m_lblTechnicianName.Text = "label3";
            // 
            // m_lblVanNumber
            // 
            this.m_lblVanNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblVanNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblVanNumber.Location = new System.Drawing.Point(85, 22);
            this.m_lblVanNumber.Name = "m_lblVanNumber";
            this.m_lblVanNumber.Size = new System.Drawing.Size(152, 20);
            this.m_lblVanNumber.Text = "label4";
            // 
            // WorkSummaryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_lblVanNumber);
            this.Controls.Add(this.m_lblTechnicianName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "WorkSummaryView";
            this.Size = new System.Drawing.Size(240, 268);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label m_lblTechnicianName;
        internal System.Windows.Forms.Label m_lblVanNumber;
    }
}
