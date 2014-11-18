namespace Dalworth.Server.MainForm.MonitoringReadingEdit
{
    partial class MonitoringReadingEditView
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtReading = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtBtu = new DevExpress.XtraEditors.TextEdit();
            this.m_cmbReadingType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtSerialNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtReading.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtBtu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbReadingType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtSerialNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.m_txtReading);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Controls.Add(this.m_txtNotes);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.m_txtBtu);
            this.panelControl1.Controls.Add(this.m_cmbReadingType);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_txtSerialNumber);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(487, 117);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 38);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(53, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "T/&RH - GPP";
            // 
            // m_txtReading
            // 
            this.m_txtReading.Location = new System.Drawing.Point(101, 35);
            this.m_txtReading.Name = "m_txtReading";
            this.m_txtReading.Properties.DisplayFormat.FormatString = "n";
            this.m_txtReading.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtReading.Properties.EditFormat.FormatString = "n";
            this.m_txtReading.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtReading.Properties.Mask.EditMask = "\\d{1,3}/\\d{1,3}-\\d{1,3}";
            this.m_txtReading.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtReading.Size = new System.Drawing.Size(144, 20);
            this.m_txtReading.TabIndex = 3;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(264, 38);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(28, 13);
            this.labelControl10.TabIndex = 8;
            this.labelControl10.Text = "&Notes";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Location = new System.Drawing.Point(301, 36);
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Properties.MaxLength = 50;
            this.m_txtNotes.Size = new System.Drawing.Size(176, 45);
            this.m_txtNotes.TabIndex = 9;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(12, 64);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(70, 13);
            this.labelControl9.TabIndex = 4;
            this.labelControl9.Text = "&BTU (tonnage)";
            // 
            // m_txtBtu
            // 
            this.m_txtBtu.Location = new System.Drawing.Point(101, 61);
            this.m_txtBtu.Name = "m_txtBtu";
            this.m_txtBtu.Properties.DisplayFormat.FormatString = "n";
            this.m_txtBtu.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtBtu.Properties.EditFormat.FormatString = "n";
            this.m_txtBtu.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtBtu.Properties.Mask.EditMask = "###,###,###,##0.00";
            this.m_txtBtu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m_txtBtu.Size = new System.Drawing.Size(144, 20);
            this.m_txtBtu.TabIndex = 5;
            // 
            // m_cmbReadingType
            // 
            this.m_cmbReadingType.Location = new System.Drawing.Point(101, 9);
            this.m_cmbReadingType.Name = "m_cmbReadingType";
            this.m_cmbReadingType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbReadingType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Outside", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Inside", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unaffected", 3, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Dehumidifier", 4, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("AC Unit", 5, -1)});
            this.m_cmbReadingType.Size = new System.Drawing.Size(144, 20);
            this.m_cmbReadingType.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Reading &Type";
            // 
            // m_txtSerialNumber
            // 
            this.m_txtSerialNumber.Location = new System.Drawing.Point(348, 9);
            this.m_txtSerialNumber.Name = "m_txtSerialNumber";
            this.m_txtSerialNumber.Properties.MaxLength = 50;
            this.m_txtSerialNumber.Size = new System.Drawing.Size(129, 20);
            this.m_txtSerialNumber.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(264, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "E&quipment Label";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(407, 89);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 11;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(326, 89);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 10;
            this.m_btnOk.Text = "&OK";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // MonitoringReadingEditView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(487, 117);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MonitoringReadingEditView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MonitoringReadingEditView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtReading.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtBtu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbReadingType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtSerialNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.TextEdit m_txtSerialNumber;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbReadingType;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal DevExpress.XtraEditors.TextEdit m_txtBtu;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        internal DevExpress.XtraEditors.MemoEdit m_txtNotes;
        internal DevExpress.XtraEditors.TextEdit m_txtReading;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}