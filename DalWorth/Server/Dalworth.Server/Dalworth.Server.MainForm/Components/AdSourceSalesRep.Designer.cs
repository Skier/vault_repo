namespace Dalworth.Server.MainForm.Components
{
    partial class AdSourceSalesRep
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
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_cmbQbSalesRep = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_cmbQbCustomerTypeLevel1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_cmbQbCustomerTypeLevel0 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbSalesRep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbCustomerTypeLevel1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbCustomerTypeLevel0.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_cmbQbSalesRep);
            this.panelControl1.Controls.Add(this.m_cmbQbCustomerTypeLevel1);
            this.panelControl1.Controls.Add(this.m_cmbQbCustomerTypeLevel0);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(373, 51);
            this.panelControl1.TabIndex = 0;
            // 
            // m_cmbQbSalesRep
            // 
            this.m_cmbQbSalesRep.Location = new System.Drawing.Point(0, 31);
            this.m_cmbQbSalesRep.Margin = new System.Windows.Forms.Padding(0);
            this.m_cmbQbSalesRep.Name = "m_cmbQbSalesRep";
            this.m_cmbQbSalesRep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbQbSalesRep.Size = new System.Drawing.Size(206, 20);
            this.m_cmbQbSalesRep.TabIndex = 9;
            // 
            // m_cmbQbCustomerTypeLevel1
            // 
            this.m_cmbQbCustomerTypeLevel1.Location = new System.Drawing.Point(209, 5);
            this.m_cmbQbCustomerTypeLevel1.Margin = new System.Windows.Forms.Padding(0);
            this.m_cmbQbCustomerTypeLevel1.Name = "m_cmbQbCustomerTypeLevel1";
            this.m_cmbQbCustomerTypeLevel1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbQbCustomerTypeLevel1.Size = new System.Drawing.Size(160, 20);
            this.m_cmbQbCustomerTypeLevel1.TabIndex = 8;
            // 
            // m_cmbQbCustomerTypeLevel0
            // 
            this.m_cmbQbCustomerTypeLevel0.Location = new System.Drawing.Point(0, 5);
            this.m_cmbQbCustomerTypeLevel0.Margin = new System.Windows.Forms.Padding(0);
            this.m_cmbQbCustomerTypeLevel0.Name = "m_cmbQbCustomerTypeLevel0";
            this.m_cmbQbCustomerTypeLevel0.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbQbCustomerTypeLevel0.Size = new System.Drawing.Size(206, 20);
            this.m_cmbQbCustomerTypeLevel0.TabIndex = 7;
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // AdSourceSalesRep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AdSourceSalesRep";
            this.Size = new System.Drawing.Size(373, 51);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbSalesRep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbCustomerTypeLevel1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbQbCustomerTypeLevel0.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbQbSalesRep;
        private DevExpress.XtraEditors.ImageComboBoxEdit m_cmbQbCustomerTypeLevel1;
        private DevExpress.XtraEditors.ImageComboBoxEdit m_cmbQbCustomerTypeLevel0;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
    }
}
