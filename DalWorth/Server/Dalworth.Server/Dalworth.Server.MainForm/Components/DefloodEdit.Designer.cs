namespace Dalworth.Server.MainForm.Components
{
    partial class DefloodEdit
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnReadings = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtCubicFeet = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbClass = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_dtpFloodDate = new DateEdit();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCubicFeet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpFloodDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpFloodDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_btnReadings);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_txtCubicFeet);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_cmbClass);
            this.panelControl1.Controls.Add(this.m_dtpFloodDate);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(528, 193);
            this.panelControl1.TabIndex = 0;
            // 
            // m_btnReadings
            // 
            this.m_btnReadings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnReadings.Location = new System.Drawing.Point(448, 165);
            this.m_btnReadings.Name = "m_btnReadings";
            this.m_btnReadings.Size = new System.Drawing.Size(75, 23);
            this.m_btnReadings.TabIndex = 7;
            this.m_btnReadings.Text = "&Readings";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(74, 26);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "C&ubic feet of \r\ndrying chamber";
            // 
            // m_txtCubicFeet
            // 
            this.m_txtCubicFeet.Location = new System.Drawing.Point(85, 66);
            this.m_txtCubicFeet.Name = "m_txtCubicFeet";
            this.m_txtCubicFeet.Properties.DisplayFormat.FormatString = "n";
            this.m_txtCubicFeet.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtCubicFeet.Properties.EditFormat.FormatString = "n";
            this.m_txtCubicFeet.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtCubicFeet.Properties.Mask.EditMask = "n0";
            this.m_txtCubicFeet.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m_txtCubicFeet.Size = new System.Drawing.Size(100, 20);
            this.m_txtCubicFeet.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(25, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "C&lass";
            // 
            // m_cmbClass
            // 
            this.m_cmbClass.Location = new System.Drawing.Point(85, 31);
            this.m_cmbClass.Name = "m_cmbClass";
            this.m_cmbClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbClass.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("I", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("II", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("III", 3, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("IV", 4, -1)});
            this.m_cmbClass.Size = new System.Drawing.Size(100, 20);
            this.m_cmbClass.TabIndex = 4;
            // 
            // m_dtpFloodDate
            // 
            this.m_dtpFloodDate.EditValue = null;
            this.m_dtpFloodDate.Location = new System.Drawing.Point(85, 5);
            this.m_dtpFloodDate.Name = "m_dtpFloodDate";
            this.m_dtpFloodDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpFloodDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpFloodDate.Properties.NullText = "Undefined";
            this.m_dtpFloodDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpFloodDate.Size = new System.Drawing.Size(100, 20);
            this.m_dtpFloodDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Flood &Date";
            // 
            // DefloodEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "DefloodEdit";
            this.Size = new System.Drawing.Size(528, 193);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCubicFeet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpFloodDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpFloodDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit m_txtCubicFeet;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit m_cmbClass;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DateEdit m_dtpFloodDate;
        private DevExpress.XtraEditors.SimpleButton m_btnReadings;
    }
}
