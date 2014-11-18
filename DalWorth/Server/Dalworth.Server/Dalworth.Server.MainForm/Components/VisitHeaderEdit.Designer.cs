namespace Dalworth.Server.MainForm.Components
{
    partial class VisitHeaderEdit
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
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblVisitNumber = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_dtpTimeFrameBegin = new Dalworth.Server.MainForm.Components.TimeEditEx();
            this.m_dtpTimeFrameEnd = new Dalworth.Server.MainForm.Components.TimeEditEx();
            this.label9 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbTimeFrameTemplate = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.m_dtpServiceDate = new Dalworth.Server.MainForm.Components.DateEdit();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpTimeFrameBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpTimeFrameEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTimeFrameTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpServiceDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpServiceDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblVisitNumber);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.tableLayoutPanel1);
            this.panelControl1.Controls.Add(this.m_cmbTimeFrameTemplate);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_txtNotes);
            this.panelControl1.Controls.Add(this.m_dtpServiceDate);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(252, 229);
            this.panelControl1.TabIndex = 0;
            // 
            // m_lblVisitNumber
            // 
            this.m_lblVisitNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblVisitNumber.Appearance.Options.UseFont = true;
            this.m_lblVisitNumber.Location = new System.Drawing.Point(87, 3);
            this.m_lblVisitNumber.Name = "m_lblVisitNumber";
            this.m_lblVisitNumber.Size = new System.Drawing.Size(21, 13);
            this.m_lblVisitNumber.TabIndex = 8;
            this.m_lblVisitNumber.Text = "123";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(2, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(30, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Visit #";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.m_dtpTimeFrameBegin, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_dtpTimeFrameEnd, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(87, 74);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(162, 22);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // m_dtpTimeFrameBegin
            // 
            this.m_dtpTimeFrameBegin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpTimeFrameBegin.EditValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.m_dtpTimeFrameBegin.Location = new System.Drawing.Point(0, 0);
            this.m_dtpTimeFrameBegin.Margin = new System.Windows.Forms.Padding(0);
            this.m_dtpTimeFrameBegin.Name = "m_dtpTimeFrameBegin";
            this.m_dtpTimeFrameBegin.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpTimeFrameBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpTimeFrameBegin.Properties.Mask.EditMask = "h tt";
            this.m_dtpTimeFrameBegin.Properties.NullText = "Any";
            this.m_dtpTimeFrameBegin.Size = new System.Drawing.Size(71, 20);
            this.m_dtpTimeFrameBegin.TabIndex = 1;
            // 
            // m_dtpTimeFrameEnd
            // 
            this.m_dtpTimeFrameEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpTimeFrameEnd.EditValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.m_dtpTimeFrameEnd.Location = new System.Drawing.Point(91, 0);
            this.m_dtpTimeFrameEnd.Margin = new System.Windows.Forms.Padding(0);
            this.m_dtpTimeFrameEnd.Name = "m_dtpTimeFrameEnd";
            this.m_dtpTimeFrameEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpTimeFrameEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpTimeFrameEnd.Properties.Mask.EditMask = "h tt";
            this.m_dtpTimeFrameEnd.Properties.NullText = "Any";
            this.m_dtpTimeFrameEnd.Size = new System.Drawing.Size(71, 20);
            this.m_dtpTimeFrameEnd.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(74, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "  -  ";
            // 
            // m_cmbTimeFrameTemplate
            // 
            this.m_cmbTimeFrameTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTimeFrameTemplate.EditValue = 0;
            this.m_cmbTimeFrameTemplate.Location = new System.Drawing.Point(87, 48);
            this.m_cmbTimeFrameTemplate.Name = "m_cmbTimeFrameTemplate";
            this.m_cmbTimeFrameTemplate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTimeFrameTemplate.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Custom", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("In AM", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("In PM", 3, -1)});
            this.m_cmbTimeFrameTemplate.Size = new System.Drawing.Size(162, 20);
            this.m_cmbTimeFrameTemplate.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 82);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "&Notes";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNotes.Location = new System.Drawing.Point(2, 102);
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Properties.MaxLength = 1500;
            this.m_txtNotes.Size = new System.Drawing.Size(247, 124);
            this.m_txtNotes.TabIndex = 6;
            // 
            // m_dtpServiceDate
            // 
            this.m_dtpServiceDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpServiceDate.EditValue = null;
            this.m_dtpServiceDate.Location = new System.Drawing.Point(87, 22);
            this.m_dtpServiceDate.Name = "m_dtpServiceDate";
            this.m_dtpServiceDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpServiceDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpServiceDate.Properties.NullText = "Undefined";
            this.m_dtpServiceDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpServiceDate.Size = new System.Drawing.Size(162, 20);
            this.m_dtpServiceDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "&Service Date";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(2, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 26);
            this.label8.TabIndex = 2;
            this.label8.Text = "&Preffered\r\nTime Frame";
            // 
            // VisitHeaderEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "VisitHeaderEdit";
            this.Size = new System.Drawing.Size(252, 229);
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpTimeFrameBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpTimeFrameEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTimeFrameTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpServiceDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpServiceDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbTimeFrameTemplate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.MemoEdit m_txtNotes;
        internal DateEdit m_dtpServiceDate;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.LabelControl label9;
        internal TimeEditEx m_dtpTimeFrameBegin;
        internal TimeEditEx m_dtpTimeFrameEnd;
        private DevExpress.XtraEditors.LabelControl label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal DevExpress.XtraEditors.LabelControl m_lblVisitNumber;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}