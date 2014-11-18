using Dalworth.Server.MainForm.Components;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.CreateProjectScope
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
            this.m_btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbScopeType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_dtpScopeDate = new Dalworth.Server.MainForm.Components.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtJobType = new DevExpress.XtraEditors.TextEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.m_txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.m_btnSaveAndAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbScopeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpScopeDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpScopeDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtJobType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.Location = new System.Drawing.Point(110, 194);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(75, 23);
            this.m_btnSave.TabIndex = 2;
            this.m_btnSave.Text = "&Save";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(191, 194);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 3;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_btnSaveAndAdd);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.m_btnSave);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(269, 220);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.m_cmbScopeType);
            this.panelControl2.Controls.Add(this.m_dtpScopeDate);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.m_txtAmount);
            this.panelControl2.Controls.Add(this.labelControl17);
            this.panelControl2.Controls.Add(this.m_txtJobType);
            this.panelControl2.Controls.Add(this.labelControl20);
            this.panelControl2.Controls.Add(this.m_lblNotes);
            this.panelControl2.Controls.Add(this.m_txtNotes);
            this.panelControl2.Location = new System.Drawing.Point(3, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(263, 187);
            this.panelControl2.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "&Job Type";
            // 
            // m_cmbScopeType
            // 
            this.m_cmbScopeType.Location = new System.Drawing.Point(85, 58);
            this.m_cmbScopeType.Name = "m_cmbScopeType";
            this.m_cmbScopeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbScopeType.Size = new System.Drawing.Size(173, 20);
            this.m_cmbScopeType.TabIndex = 5;
            // 
            // m_dtpScopeDate
            // 
            this.m_dtpScopeDate.EditValue = null;
            this.m_dtpScopeDate.Location = new System.Drawing.Point(85, 32);
            this.m_dtpScopeDate.Name = "m_dtpScopeDate";
            this.m_dtpScopeDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpScopeDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpScopeDate.Properties.NullText = "Undefined";
            this.m_dtpScopeDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpScopeDate.Size = new System.Drawing.Size(173, 20);
            this.m_dtpScopeDate.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Scope &Type";
            // 
            // m_txtAmount
            // 
            this.m_txtAmount.Location = new System.Drawing.Point(85, 84);
            this.m_txtAmount.Name = "m_txtAmount";
            this.m_txtAmount.Properties.DisplayFormat.FormatString = "C";
            this.m_txtAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtAmount.Properties.Mask.EditMask = "$###,###,###,##0.00;($###,###,###,##0.00)";
            this.m_txtAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m_txtAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtAmount.Size = new System.Drawing.Size(173, 20);
            this.m_txtAmount.TabIndex = 7;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(5, 35);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(55, 13);
            this.labelControl17.TabIndex = 2;
            this.labelControl17.Text = "Scope &Date";
            // 
            // m_txtJobType
            // 
            this.m_txtJobType.Location = new System.Drawing.Point(85, 6);
            this.m_txtJobType.Name = "m_txtJobType";
            this.m_txtJobType.Properties.MaxLength = 50;
            this.m_txtJobType.Size = new System.Drawing.Size(173, 20);
            this.m_txtJobType.TabIndex = 1;
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(5, 87);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(37, 13);
            this.labelControl20.TabIndex = 6;
            this.labelControl20.Text = "A&mount";
            // 
            // m_lblNotes
            // 
            this.m_lblNotes.Location = new System.Drawing.Point(5, 113);
            this.m_lblNotes.Name = "m_lblNotes";
            this.m_lblNotes.Size = new System.Drawing.Size(28, 13);
            this.m_lblNotes.TabIndex = 8;
            this.m_lblNotes.Text = "&Notes";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Location = new System.Drawing.Point(85, 110);
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Properties.MaxLength = 200;
            this.m_txtNotes.Size = new System.Drawing.Size(173, 72);
            this.m_txtNotes.TabIndex = 9;
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // m_btnSaveAndAdd
            // 
            this.m_btnSaveAndAdd.Location = new System.Drawing.Point(4, 194);
            this.m_btnSaveAndAdd.Name = "m_btnSaveAndAdd";
            this.m_btnSaveAndAdd.Size = new System.Drawing.Size(100, 23);
            this.m_btnSaveAndAdd.TabIndex = 1;
            this.m_btnSaveAndAdd.Text = "Save and &Add";
            // 
            // CreateProjectScopeView
            // 
            this.AcceptButton = this.m_btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(269, 220);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateProjectScopeView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateProjectScopeView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbScopeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpScopeDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpScopeDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtJobType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal SimpleButton m_btnSave;
        internal SimpleButton m_btnCancel;
        private PanelControl panelControl1;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private LabelControl labelControl20;
        internal Dalworth.Server.MainForm.Components.DateEdit m_dtpScopeDate;
        private LabelControl labelControl17;
        internal TextEdit m_txtAmount;
        private LabelControl m_lblNotes;
        internal MemoEdit m_txtNotes;
        private LabelControl labelControl1;
        internal TextEdit m_txtJobType;
        private LabelControl labelControl2;
        internal ImageComboBoxEdit m_cmbScopeType;
        private PanelControl panelControl2;
        internal SimpleButton m_btnSaveAndAdd;
    }
}