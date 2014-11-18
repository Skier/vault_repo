using Dalworth.Server.MainForm.Components;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.CreateProjectBillPay
{
    partial class CreateProjectScopeView
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
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtNumber = new DevExpress.XtraEditors.TextEdit();
            this.m_lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.m_txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtCollectedAmount = new DevExpress.XtraEditors.TextEdit();
            this.m_dtpPaymentDate = new Dalworth.Server.MainForm.Components.DateEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbTransactionType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCollectedAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpPaymentDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpPaymentDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTransactionType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(110, 218);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "&OK";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(191, 218);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.groupControl3);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_cmbTransactionType);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(269, 244);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(3, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(81, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "&Transaction type";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.m_txtNumber);
            this.groupControl3.Controls.Add(this.m_lblNotes);
            this.groupControl3.Controls.Add(this.m_txtNotes);
            this.groupControl3.Controls.Add(this.labelControl20);
            this.groupControl3.Controls.Add(this.m_txtCollectedAmount);
            this.groupControl3.Controls.Add(this.m_dtpPaymentDate);
            this.groupControl3.Controls.Add(this.labelControl17);
            this.groupControl3.Location = new System.Drawing.Point(3, 35);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(263, 179);
            this.groupControl3.TabIndex = 2;
            this.groupControl3.Text = "Details";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "&Issue Number";
            // 
            // m_txtNumber
            // 
            this.m_txtNumber.Location = new System.Drawing.Point(89, 24);
            this.m_txtNumber.Name = "m_txtNumber";
            this.m_txtNumber.Properties.MaxLength = 50;
            this.m_txtNumber.Size = new System.Drawing.Size(165, 20);
            this.m_txtNumber.TabIndex = 1;
            // 
            // m_lblNotes
            // 
            this.m_lblNotes.Location = new System.Drawing.Point(9, 105);
            this.m_lblNotes.Name = "m_lblNotes";
            this.m_lblNotes.Size = new System.Drawing.Size(28, 13);
            this.m_lblNotes.TabIndex = 6;
            this.m_lblNotes.Text = "&Notes";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Location = new System.Drawing.Point(89, 102);
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Properties.MaxLength = 200;
            this.m_txtNotes.Size = new System.Drawing.Size(165, 72);
            this.m_txtNotes.TabIndex = 7;
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(9, 79);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(37, 13);
            this.labelControl20.TabIndex = 4;
            this.labelControl20.Text = "&Amount";
            // 
            // m_txtCollectedAmount
            // 
            this.m_txtCollectedAmount.Location = new System.Drawing.Point(89, 76);
            this.m_txtCollectedAmount.Name = "m_txtCollectedAmount";
            this.m_txtCollectedAmount.Properties.DisplayFormat.FormatString = "C";
            this.m_txtCollectedAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtCollectedAmount.Properties.Mask.EditMask = "$###,###,###,##0.00;($###,###,###,##0.00)";
            this.m_txtCollectedAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m_txtCollectedAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtCollectedAmount.Size = new System.Drawing.Size(165, 20);
            this.m_txtCollectedAmount.TabIndex = 5;
            // 
            // m_dtpPaymentDate
            // 
            this.m_dtpPaymentDate.EditValue = null;
            this.m_dtpPaymentDate.Location = new System.Drawing.Point(89, 50);
            this.m_dtpPaymentDate.Name = "m_dtpPaymentDate";
            this.m_dtpPaymentDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpPaymentDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpPaymentDate.Properties.NullText = "Undefined";
            this.m_dtpPaymentDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpPaymentDate.Size = new System.Drawing.Size(165, 20);
            this.m_dtpPaymentDate.TabIndex = 3;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(9, 53);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(52, 13);
            this.labelControl17.TabIndex = 2;
            this.labelControl17.Text = "Issue &Date";
            // 
            // m_cmbTransactionType
            // 
            this.m_cmbTransactionType.Location = new System.Drawing.Point(92, 9);
            this.m_cmbTransactionType.Name = "m_cmbTransactionType";
            this.m_cmbTransactionType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTransactionType.Size = new System.Drawing.Size(174, 20);
            this.m_cmbTransactionType.TabIndex = 1;
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // CreateProjectScopeView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(269, 244);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateProjectScopeView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateProjectBillPayView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCollectedAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpPaymentDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpPaymentDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTransactionType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal SimpleButton m_btnOk;
        internal SimpleButton m_btnCancel;
        private PanelControl panelControl1;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private GroupControl groupControl3;
        private LabelControl labelControl20;
        internal Dalworth.Server.MainForm.Components.DateEdit m_dtpPaymentDate;
        private LabelControl labelControl17;
        internal TextEdit m_txtCollectedAmount;
        private LabelControl labelControl2;
        internal ImageComboBoxEdit m_cmbTransactionType;
        private LabelControl m_lblNotes;
        internal MemoEdit m_txtNotes;
        private LabelControl labelControl1;
        internal TextEdit m_txtNumber;
    }
}