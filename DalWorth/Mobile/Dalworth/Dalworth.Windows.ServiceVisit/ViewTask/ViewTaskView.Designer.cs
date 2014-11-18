namespace Dalworth.Windows.ServiceVisit.ViewTask
{
    partial class ViewTaskView
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
            this.m_tblRugs = new Dalworth.Controls.Table();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_lblTaskSubTotal = new System.Windows.Forms.Label();
            this.m_lblTaskTax = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_lblTaskTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tblRugs
            // 
            this.m_tblRugs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tblRugs.Location = new System.Drawing.Point(0, 0);
            this.m_tblRugs.Name = "m_tblRugs";
            this.m_tblRugs.Size = new System.Drawing.Size(240, 211);
            this.m_tblRugs.TabIndex = 0;
            this.m_tblRugs.Text = "table1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_lblTaskSubTotal);
            this.panel3.Controls.Add(this.m_lblTaskTax);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.m_lblTaskTotal);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 211);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 57);
            // 
            // m_lblTaskSubTotal
            // 
            this.m_lblTaskSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaskSubTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTaskSubTotal.Location = new System.Drawing.Point(82, 3);
            this.m_lblTaskSubTotal.Name = "m_lblTaskSubTotal";
            this.m_lblTaskSubTotal.Size = new System.Drawing.Size(155, 16);
            this.m_lblTaskSubTotal.Tag = "";
            this.m_lblTaskSubTotal.Text = "$50.00";
            this.m_lblTaskSubTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblTaskTax
            // 
            this.m_lblTaskTax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaskTax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTaskTax.Location = new System.Drawing.Point(76, 19);
            this.m_lblTaskTax.Name = "m_lblTaskTax";
            this.m_lblTaskTax.Size = new System.Drawing.Size(161, 16);
            this.m_lblTaskTax.Tag = "";
            this.m_lblTaskTax.Text = "$50.00";
            this.m_lblTaskTax.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(3, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.Tag = "";
            this.label4.Text = "Tax:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.Tag = "";
            this.label3.Text = "Task Subtotal:";
            // 
            // m_lblTaskTotal
            // 
            this.m_lblTaskTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaskTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTaskTotal.Location = new System.Drawing.Point(65, 37);
            this.m_lblTaskTotal.Name = "m_lblTaskTotal";
            this.m_lblTaskTotal.Size = new System.Drawing.Size(172, 16);
            this.m_lblTaskTotal.Tag = "";
            this.m_lblTaskTotal.Text = "$50.00";
            this.m_lblTaskTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.Tag = "";
            this.label2.Text = "Task Total:";
            // 
            // ViewTaskView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tblRugs);
            this.Controls.Add(this.panel3);
            this.Name = "ViewTaskView";
            this.Size = new System.Drawing.Size(240, 268);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal Dalworth.Controls.Table m_tblRugs;
        internal System.Windows.Forms.Label m_lblTaskSubTotal;
        internal System.Windows.Forms.Label m_lblTaskTax;
        internal System.Windows.Forms.Label m_lblTaskTotal;
    }
}
