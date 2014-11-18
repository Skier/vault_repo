namespace Dalworth.Server.MainForm.AddEquipment
{
    partial class AddEquipmentView
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
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_cmbInventoryRoom = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_cmbStatus = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_cmbType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtSerialNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbInventoryRoom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtSerialNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_cmbInventoryRoom);
            this.panelControl1.Controls.Add(this.m_cmbStatus);
            this.panelControl1.Controls.Add(this.m_cmbType);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_txtSerialNumber);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(486, 93);
            this.panelControl1.TabIndex = 0;
            // 
            // m_cmbInventoryRoom
            // 
            this.m_cmbInventoryRoom.Location = new System.Drawing.Point(337, 31);
            this.m_cmbInventoryRoom.Name = "m_cmbInventoryRoom";
            this.m_cmbInventoryRoom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbInventoryRoom.Size = new System.Drawing.Size(144, 20);
            this.m_cmbInventoryRoom.TabIndex = 7;
            // 
            // m_cmbStatus
            // 
            this.m_cmbStatus.Location = new System.Drawing.Point(337, 5);
            this.m_cmbStatus.Name = "m_cmbStatus";
            this.m_cmbStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbStatus.Size = new System.Drawing.Size(144, 20);
            this.m_cmbStatus.TabIndex = 5;
            // 
            // m_cmbType
            // 
            this.m_cmbType.Location = new System.Drawing.Point(77, 31);
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbType.Size = new System.Drawing.Size(144, 20);
            this.m_cmbType.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(251, 34);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(78, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "&Inventory Room";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(251, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(31, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "St&atus";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 34);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "&Type";
            // 
            // m_txtSerialNumber
            // 
            this.m_txtSerialNumber.Location = new System.Drawing.Point(77, 5);
            this.m_txtSerialNumber.Name = "m_txtSerialNumber";
            this.m_txtSerialNumber.Properties.Mask.EditMask = "\\d{1,4}";
            this.m_txtSerialNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtSerialNumber.Properties.MaxLength = 4;
            this.m_txtSerialNumber.Size = new System.Drawing.Size(144, 20);
            this.m_txtSerialNumber.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "&Serial Number";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(406, 65);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 9;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(325, 65);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 8;
            this.m_btnOk.Text = "&OK";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // AddEquipmentView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(486, 93);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEquipmentView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddEquipmentView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbInventoryRoom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtSerialNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.TextEdit m_txtSerialNumber;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbInventoryRoom;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbStatus;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbType;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
    }
}