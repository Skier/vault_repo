using Dalworth.Server.MainForm.Components;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.SubmitEtc
{
    partial class SubmitEtcView
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
            this.m_txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtEstimatedClosedAmount = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_dtpEstimatedCompletionTime = new Dalworth.Server.MainForm.Components.TimeEditEx();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtEstimatedClosedAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpEstimatedCompletionTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(104, 187);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 6;
            this.m_btnOk.Text = "&OK";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(185, 187);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 7;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Location = new System.Drawing.Point(7, 79);
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Properties.MaxLength = 200;
            this.m_txtNotes.Size = new System.Drawing.Size(253, 102);
            this.m_txtNotes.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estimated Closed &Amount";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Estimated Completion &Time";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "&Notes";
            // 
            // m_txtEstimatedClosedAmount
            // 
            this.m_txtEstimatedClosedAmount.Location = new System.Drawing.Point(148, 5);
            this.m_txtEstimatedClosedAmount.Name = "m_txtEstimatedClosedAmount";
            this.m_txtEstimatedClosedAmount.Properties.Mask.EditMask = "$###,###,###,##0.00;($###,###,###,##0.00)";
            this.m_txtEstimatedClosedAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m_txtEstimatedClosedAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.m_txtEstimatedClosedAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtEstimatedClosedAmount.Size = new System.Drawing.Size(112, 20);
            this.m_txtEstimatedClosedAmount.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_dtpEstimatedCompletionTime);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Controls.Add(this.m_txtEstimatedClosedAmount);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.m_txtNotes);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(266, 214);
            this.panelControl1.TabIndex = 0;
            // 
            // m_dtpEstimatedCompletionTime
            // 
            this.m_dtpEstimatedCompletionTime.EditValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.m_dtpEstimatedCompletionTime.Location = new System.Drawing.Point(148, 31);
            this.m_dtpEstimatedCompletionTime.Name = "m_dtpEstimatedCompletionTime";
            this.m_dtpEstimatedCompletionTime.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.m_dtpEstimatedCompletionTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpEstimatedCompletionTime.Properties.Mask.EditMask = "t";
            this.m_dtpEstimatedCompletionTime.Properties.NullText = "Any";
            this.m_dtpEstimatedCompletionTime.Size = new System.Drawing.Size(112, 20);
            this.m_dtpEstimatedCompletionTime.TabIndex = 3;
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // SubmitEtcView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(266, 214);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubmitEtcView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SubmitEtcView";
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtEstimatedClosedAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpEstimatedCompletionTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal SimpleButton m_btnOk;
        internal SimpleButton m_btnCancel;
        private LabelControl label1;
        private LabelControl label2;
        private LabelControl label3;
        internal MemoEdit m_txtNotes;
        private PanelControl panelControl1;
        internal TextEdit m_txtEstimatedClosedAmount;
        internal TimeEditEx m_dtpEstimatedCompletionTime;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
    }
}