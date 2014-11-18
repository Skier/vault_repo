namespace Dalworth.Server.MainForm.Components
{
    partial class AddressViewEditClear
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
            this.m_group = new DevExpress.XtraEditors.GroupControl();
            this.Mapsco = new DevExpress.XtraEditors.LabelControl();
            this.m_btnAddressEdit = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblMapsco = new DevExpress.XtraEditors.LabelControl();
            this.m_lblCityStateZip = new DevExpress.XtraEditors.LabelControl();
            this.m_lblAddress = new DevExpress.XtraEditors.LabelControl();
            this.m_btnAddressClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.m_group)).BeginInit();
            this.m_group.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_group
            // 
            this.m_group.Controls.Add(this.m_btnAddressClear);
            this.m_group.Controls.Add(this.Mapsco);
            this.m_group.Controls.Add(this.m_btnAddressEdit);
            this.m_group.Controls.Add(this.m_lblMapsco);
            this.m_group.Controls.Add(this.m_lblCityStateZip);
            this.m_group.Controls.Add(this.m_lblAddress);
            this.m_group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_group.Location = new System.Drawing.Point(0, 0);
            this.m_group.Name = "m_group";
            this.m_group.Size = new System.Drawing.Size(230, 100);
            this.m_group.TabIndex = 5;
            this.m_group.Text = "Address";
            // 
            // Mapsco
            // 
            this.Mapsco.Location = new System.Drawing.Point(5, 62);
            this.Mapsco.Name = "Mapsco";
            this.Mapsco.Size = new System.Drawing.Size(36, 13);
            this.Mapsco.TabIndex = 6;
            this.Mapsco.Text = "Mapsco";
            // 
            // m_btnAddressEdit
            // 
            this.m_btnAddressEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAddressEdit.Location = new System.Drawing.Point(112, 72);
            this.m_btnAddressEdit.Name = "m_btnAddressEdit";
            this.m_btnAddressEdit.Size = new System.Drawing.Size(54, 23);
            this.m_btnAddressEdit.TabIndex = 12;
            this.m_btnAddressEdit.Text = "Edit";
            // 
            // m_lblMapsco
            // 
            this.m_lblMapsco.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblMapsco.Appearance.Options.UseFont = true;
            this.m_lblMapsco.Location = new System.Drawing.Point(47, 62);
            this.m_lblMapsco.Name = "m_lblMapsco";
            this.m_lblMapsco.Size = new System.Drawing.Size(27, 13);
            this.m_lblMapsco.TabIndex = 3;
            this.m_lblMapsco.Text = "234F";
            // 
            // m_lblCityStateZip
            // 
            this.m_lblCityStateZip.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblCityStateZip.Appearance.Options.UseFont = true;
            this.m_lblCityStateZip.Location = new System.Drawing.Point(5, 43);
            this.m_lblCityStateZip.Name = "m_lblCityStateZip";
            this.m_lblCityStateZip.Size = new System.Drawing.Size(95, 13);
            this.m_lblCityStateZip.TabIndex = 2;
            this.m_lblCityStateZip.Text = "Dallas, TX, 75025";
            // 
            // m_lblAddress
            // 
            this.m_lblAddress.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblAddress.Appearance.Options.UseFont = true;
            this.m_lblAddress.Location = new System.Drawing.Point(5, 24);
            this.m_lblAddress.Name = "m_lblAddress";
            this.m_lblAddress.Size = new System.Drawing.Size(101, 13);
            this.m_lblAddress.TabIndex = 1;
            this.m_lblAddress.Text = "7016 Randall Way";
            // 
            // m_btnAddressClear
            // 
            this.m_btnAddressClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAddressClear.Location = new System.Drawing.Point(171, 72);
            this.m_btnAddressClear.Name = "m_btnAddressClear";
            this.m_btnAddressClear.Size = new System.Drawing.Size(54, 23);
            this.m_btnAddressClear.TabIndex = 13;
            this.m_btnAddressClear.Text = "Clear";
            // 
            // AddressViewEditClear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_group);
            this.Name = "AddressViewEditClear";
            this.Size = new System.Drawing.Size(230, 100);
            ((System.ComponentModel.ISupportInitialize)(this.m_group)).EndInit();
            this.m_group.ResumeLayout(false);
            this.m_group.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl m_group;
        private DevExpress.XtraEditors.LabelControl Mapsco;
        private DevExpress.XtraEditors.SimpleButton m_btnAddressEdit;
        private DevExpress.XtraEditors.LabelControl m_lblMapsco;
        private DevExpress.XtraEditors.LabelControl m_lblCityStateZip;
        private DevExpress.XtraEditors.LabelControl m_lblAddress;
        private DevExpress.XtraEditors.SimpleButton m_btnAddressClear;
    }
}
