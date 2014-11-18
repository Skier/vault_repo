namespace Dalworth.Server.MainForm.Components
{
    partial class ProjectViewEdit
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_btnCreatePayment = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCreateInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblClosedAmount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProjectType = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblDateCreated = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProjectId = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.m_btnCreatePayment);
            this.groupControl1.Controls.Add(this.m_btnCreateInvoice);
            this.groupControl1.Controls.Add(this.m_lblClosedAmount);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.m_lblStatus);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.m_lblProjectType);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.m_lblDateCreated);
            this.groupControl1.Controls.Add(this.m_lblProjectId);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(424, 168);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Project Details";
            // 
            // m_btnCreatePayment
            // 
            this.m_btnCreatePayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCreatePayment.Location = new System.Drawing.Point(184, 140);
            this.m_btnCreatePayment.Name = "m_btnCreatePayment";
            this.m_btnCreatePayment.Size = new System.Drawing.Size(109, 23);
            this.m_btnCreatePayment.TabIndex = 15;
            this.m_btnCreatePayment.Text = "Post Payment";
            // 
            // m_btnCreateInvoice
            // 
            this.m_btnCreateInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCreateInvoice.Location = new System.Drawing.Point(310, 140);
            this.m_btnCreateInvoice.Name = "m_btnCreateInvoice";
            this.m_btnCreateInvoice.Size = new System.Drawing.Size(109, 23);
            this.m_btnCreateInvoice.TabIndex = 2;
            this.m_btnCreateInvoice.Text = "Create Invoice";
            // 
            // m_lblClosedAmount
            // 
            this.m_lblClosedAmount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblClosedAmount.Appearance.Options.UseFont = true;
            this.m_lblClosedAmount.Location = new System.Drawing.Point(340, 42);
            this.m_lblClosedAmount.Name = "m_lblClosedAmount";
            this.m_lblClosedAmount.Size = new System.Drawing.Size(45, 13);
            this.m_lblClosedAmount.TabIndex = 14;
            this.m_lblClosedAmount.Text = "$20,000";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(241, 42);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 13);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "Closed Amount";
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblStatus.Appearance.Options.UseFont = true;
            this.m_lblStatus.Location = new System.Drawing.Point(340, 23);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(61, 13);
            this.m_lblStatus.TabIndex = 12;
            this.m_lblStatus.Text = "Completed";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(241, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(31, 13);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "Status";
            // 
            // m_lblProjectType
            // 
            this.m_lblProjectType.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectType.Appearance.Options.UseFont = true;
            this.m_lblProjectType.Location = new System.Drawing.Point(104, 42);
            this.m_lblProjectType.Name = "m_lblProjectType";
            this.m_lblProjectType.Size = new System.Drawing.Size(70, 13);
            this.m_lblProjectType.TabIndex = 10;
            this.m_lblProjectType.Text = "RugCleaning";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 42);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 13);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "Type";
            // 
            // m_lblDateCreated
            // 
            this.m_lblDateCreated.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblDateCreated.Appearance.Options.UseFont = true;
            this.m_lblDateCreated.Location = new System.Drawing.Point(104, 61);
            this.m_lblDateCreated.Name = "m_lblDateCreated";
            this.m_lblDateCreated.Size = new System.Drawing.Size(68, 13);
            this.m_lblDateCreated.TabIndex = 8;
            this.m_lblDateCreated.Text = "10/10/2010";
            // 
            // m_lblProjectId
            // 
            this.m_lblProjectId.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectId.Appearance.Options.UseFont = true;
            this.m_lblProjectId.Location = new System.Drawing.Point(104, 23);
            this.m_lblProjectId.Name = "m_lblProjectId";
            this.m_lblProjectId.Size = new System.Drawing.Size(42, 13);
            this.m_lblProjectId.TabIndex = 7;
            this.m_lblProjectId.Text = "123456";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Date Created";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(11, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "ID";
            // 
            // ProjectViewEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "ProjectViewEdit";
            this.Size = new System.Drawing.Size(427, 171);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl m_lblDateCreated;
        private DevExpress.XtraEditors.LabelControl m_lblProjectId;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl m_lblProjectType;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl m_lblStatus;
        private DevExpress.XtraEditors.LabelControl m_lblClosedAmount;
        public DevExpress.XtraEditors.SimpleButton m_btnCreateInvoice;
        public DevExpress.XtraEditors.SimpleButton m_btnCreatePayment;

    }
}
