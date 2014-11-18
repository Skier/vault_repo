namespace Dalworth.Windows.ServiceVisit.ServiceVisit
{
    partial class ServiceVisitView
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
            this.m_tabs = new System.Windows.Forms.TabControl();
            this.m_tabVisitInfo = new System.Windows.Forms.TabPage();
            this.m_txtAddress = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_linkPhone2 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_linkPhone1 = new System.Windows.Forms.LinkLabel();
            this.m_txtNotes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblDate = new System.Windows.Forms.Label();
            this.m_lblMap = new System.Windows.Forms.Label();
            this.m_lblTaskType = new System.Windows.Forms.Label();
            this.m_lblTaskNumber = new System.Windows.Forms.Label();
            this.m_tabMessage = new System.Windows.Forms.TabPage();
            this.m_txtMessage = new System.Windows.Forms.TextBox();
            this.m_tabTasks = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_tblTasks = new Dalworth.Controls.Table();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_lblVisitSubTotal = new System.Windows.Forms.Label();
            this.m_lblVisitTax = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_lblVisitTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_tabNotes = new System.Windows.Forms.TabPage();
            this.m_txtUserNotes = new System.Windows.Forms.TextBox();
            this.m_tabs.SuspendLayout();
            this.m_tabVisitInfo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_tabMessage.SuspendLayout();
            this.m_tabTasks.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.m_tabNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabs
            // 
            this.m_tabs.Controls.Add(this.m_tabVisitInfo);
            this.m_tabs.Controls.Add(this.m_tabMessage);
            this.m_tabs.Controls.Add(this.m_tabTasks);
            this.m_tabs.Controls.Add(this.m_tabNotes);
            this.m_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabs.Location = new System.Drawing.Point(0, 0);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedIndex = 0;
            this.m_tabs.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.TabIndex = 1;
            // 
            // m_tabVisitInfo
            // 
            this.m_tabVisitInfo.Controls.Add(this.m_txtAddress);
            this.m_tabVisitInfo.Controls.Add(this.panel2);
            this.m_tabVisitInfo.Controls.Add(this.panel1);
            this.m_tabVisitInfo.Controls.Add(this.m_txtNotes);
            this.m_tabVisitInfo.Controls.Add(this.label1);
            this.m_tabVisitInfo.Controls.Add(this.m_lblCustomerName);
            this.m_tabVisitInfo.Controls.Add(this.m_lblDate);
            this.m_tabVisitInfo.Controls.Add(this.m_lblMap);
            this.m_tabVisitInfo.Controls.Add(this.m_lblTaskType);
            this.m_tabVisitInfo.Controls.Add(this.m_lblTaskNumber);
            this.m_tabVisitInfo.Location = new System.Drawing.Point(0, 0);
            this.m_tabVisitInfo.Name = "m_tabVisitInfo";
            this.m_tabVisitInfo.Size = new System.Drawing.Size(240, 245);
            this.m_tabVisitInfo.Text = "Info";
            // 
            // m_txtAddress
            // 
            this.m_txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtAddress.Location = new System.Drawing.Point(3, 36);
            this.m_txtAddress.Multiline = true;
            this.m_txtAddress.Name = "m_txtAddress";
            this.m_txtAddress.ReadOnly = true;
            this.m_txtAddress.Size = new System.Drawing.Size(234, 37);
            this.m_txtAddress.TabIndex = 34;
            this.m_txtAddress.Text = "7016 Randall Way\r\nPlano, TX, 75025";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_linkPhone2);
            this.panel2.Location = new System.Drawing.Point(119, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(117, 20);
            // 
            // m_linkPhone2
            // 
            this.m_linkPhone2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_linkPhone2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.m_linkPhone2.Location = new System.Drawing.Point(5, 0);
            this.m_linkPhone2.Name = "m_linkPhone2";
            this.m_linkPhone2.Size = new System.Drawing.Size(112, 20);
            this.m_linkPhone2.TabIndex = 27;
            this.m_linkPhone2.Text = "BS (972) 471-9223";
            this.m_linkPhone2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_linkPhone1);
            this.panel1.Location = new System.Drawing.Point(4, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 20);
            // 
            // m_linkPhone1
            // 
            this.m_linkPhone1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.m_linkPhone1.Location = new System.Drawing.Point(0, 0);
            this.m_linkPhone1.Name = "m_linkPhone1";
            this.m_linkPhone1.Size = new System.Drawing.Size(109, 20);
            this.m_linkPhone1.TabIndex = 26;
            this.m_linkPhone1.Text = "HM (972) 314-6984";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNotes.Location = new System.Drawing.Point(3, 127);
            this.m_txtNotes.Multiline = true;
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.ReadOnly = true;
            this.m_txtNotes.Size = new System.Drawing.Size(234, 37);
            this.m_txtNotes.TabIndex = 27;
            this.m_txtNotes.Text = "Pick up 2 rugs. Cust would like 3-5 time frame. Please call in AM to confirm.";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.Text = "Notes:";
            // 
            // m_lblCustomerName
            // 
            this.m_lblCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCustomerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblCustomerName.Location = new System.Drawing.Point(3, 19);
            this.m_lblCustomerName.Name = "m_lblCustomerName";
            this.m_lblCustomerName.Size = new System.Drawing.Size(234, 20);
            this.m_lblCustomerName.Text = "Love, Rob";
            // 
            // m_lblDate
            // 
            this.m_lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblDate.Location = new System.Drawing.Point(3, 3);
            this.m_lblDate.Name = "m_lblDate";
            this.m_lblDate.Size = new System.Drawing.Size(149, 20);
            this.m_lblDate.Text = "Sat, Apr 14, 2007";
            // 
            // m_lblMap
            // 
            this.m_lblMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblMap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblMap.ForeColor = System.Drawing.Color.Red;
            this.m_lblMap.Location = new System.Drawing.Point(158, 3);
            this.m_lblMap.Name = "m_lblMap";
            this.m_lblMap.Size = new System.Drawing.Size(79, 20);
            this.m_lblMap.Text = "MAP: 45Q";
            this.m_lblMap.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblTaskType
            // 
            this.m_lblTaskType.Location = new System.Drawing.Point(3, 93);
            this.m_lblTaskType.Name = "m_lblTaskType";
            this.m_lblTaskType.Size = new System.Drawing.Size(110, 20);
            this.m_lblTaskType.Text = "Rug Pickup";
            // 
            // m_lblTaskNumber
            // 
            this.m_lblTaskNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaskNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTaskNumber.Location = new System.Drawing.Point(119, 93);
            this.m_lblTaskNumber.Name = "m_lblTaskNumber";
            this.m_lblTaskNumber.Size = new System.Drawing.Size(118, 20);
            this.m_lblTaskNumber.Text = "TKT: 1001";
            this.m_lblTaskNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_tabMessage
            // 
            this.m_tabMessage.Controls.Add(this.m_txtMessage);
            this.m_tabMessage.Location = new System.Drawing.Point(0, 0);
            this.m_tabMessage.Name = "m_tabMessage";
            this.m_tabMessage.Size = new System.Drawing.Size(240, 245);
            this.m_tabMessage.Text = "Message";
            // 
            // m_txtMessage
            // 
            this.m_txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtMessage.Location = new System.Drawing.Point(0, 0);
            this.m_txtMessage.Multiline = true;
            this.m_txtMessage.Name = "m_txtMessage";
            this.m_txtMessage.ReadOnly = true;
            this.m_txtMessage.Size = new System.Drawing.Size(240, 245);
            this.m_txtMessage.TabIndex = 3;
            this.m_txtMessage.Text = "Be careful - evil dog wandering outside\r\nPlease call before come in";
            // 
            // m_tabTasks
            // 
            this.m_tabTasks.Controls.Add(this.panel4);
            this.m_tabTasks.Controls.Add(this.panel3);
            this.m_tabTasks.Location = new System.Drawing.Point(0, 0);
            this.m_tabTasks.Name = "m_tabTasks";
            this.m_tabTasks.Size = new System.Drawing.Size(240, 245);
            this.m_tabTasks.Text = "Tasks";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_tblTasks);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(240, 188);
            // 
            // m_tblTasks
            // 
            this.m_tblTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tblTasks.Location = new System.Drawing.Point(0, 0);
            this.m_tblTasks.Name = "m_tblTasks";
            this.m_tblTasks.Size = new System.Drawing.Size(240, 188);
            this.m_tblTasks.TabIndex = 0;
            this.m_tblTasks.Text = "table1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_lblVisitSubTotal);
            this.panel3.Controls.Add(this.m_lblVisitTax);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.m_lblVisitTotal);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 188);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 57);
            // 
            // m_lblVisitSubTotal
            // 
            this.m_lblVisitSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblVisitSubTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblVisitSubTotal.Location = new System.Drawing.Point(82, 3);
            this.m_lblVisitSubTotal.Name = "m_lblVisitSubTotal";
            this.m_lblVisitSubTotal.Size = new System.Drawing.Size(155, 16);
            this.m_lblVisitSubTotal.Tag = "";
            this.m_lblVisitSubTotal.Text = "$50.00";
            this.m_lblVisitSubTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblVisitTax
            // 
            this.m_lblVisitTax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblVisitTax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblVisitTax.Location = new System.Drawing.Point(76, 19);
            this.m_lblVisitTax.Name = "m_lblVisitTax";
            this.m_lblVisitTax.Size = new System.Drawing.Size(161, 16);
            this.m_lblVisitTax.Tag = "";
            this.m_lblVisitTax.Text = "$50.00";
            this.m_lblVisitTax.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            this.label3.Text = "Visit Subtotal:";
            // 
            // m_lblVisitTotal
            // 
            this.m_lblVisitTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblVisitTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblVisitTotal.Location = new System.Drawing.Point(65, 37);
            this.m_lblVisitTotal.Name = "m_lblVisitTotal";
            this.m_lblVisitTotal.Size = new System.Drawing.Size(172, 16);
            this.m_lblVisitTotal.Tag = "";
            this.m_lblVisitTotal.Text = "$50.00";
            this.m_lblVisitTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.Tag = "";
            this.label2.Text = "Visit Total:";
            // 
            // m_tabNotes
            // 
            this.m_tabNotes.Controls.Add(this.m_txtUserNotes);
            this.m_tabNotes.Location = new System.Drawing.Point(0, 0);
            this.m_tabNotes.Name = "m_tabNotes";
            this.m_tabNotes.Size = new System.Drawing.Size(240, 245);
            this.m_tabNotes.Text = "Notes";
            // 
            // m_txtUserNotes
            // 
            this.m_txtUserNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtUserNotes.Location = new System.Drawing.Point(0, 0);
            this.m_txtUserNotes.Multiline = true;
            this.m_txtUserNotes.Name = "m_txtUserNotes";
            this.m_txtUserNotes.Size = new System.Drawing.Size(240, 245);
            this.m_txtUserNotes.TabIndex = 0;
            // 
            // ServiceVisitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tabs);
            this.Name = "ServiceVisitView";
            this.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.ResumeLayout(false);
            this.m_tabVisitInfo.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_tabMessage.ResumeLayout(false);
            this.m_tabTasks.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.m_tabNotes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox m_txtAddress;
        internal Dalworth.Controls.Table m_tblTasks;
        internal System.Windows.Forms.TabControl m_tabs;
        internal System.Windows.Forms.TabPage m_tabVisitInfo;
        internal System.Windows.Forms.LinkLabel m_linkPhone2;
        internal System.Windows.Forms.LinkLabel m_linkPhone1;
        internal System.Windows.Forms.TextBox m_txtNotes;
        internal System.Windows.Forms.Label m_lblCustomerName;
        internal System.Windows.Forms.Label m_lblDate;
        internal System.Windows.Forms.Label m_lblMap;
        internal System.Windows.Forms.Label m_lblTaskType;
        internal System.Windows.Forms.Label m_lblTaskNumber;
        internal System.Windows.Forms.TextBox m_txtMessage;
        internal System.Windows.Forms.Label m_lblVisitSubTotal;
        internal System.Windows.Forms.Label m_lblVisitTax;
        internal System.Windows.Forms.Label m_lblVisitTotal;
        internal System.Windows.Forms.TextBox m_txtUserNotes;
        internal System.Windows.Forms.TabPage m_tabMessage;
        internal System.Windows.Forms.TabPage m_tabTasks;
        internal System.Windows.Forms.TabPage m_tabNotes;
    }
}
