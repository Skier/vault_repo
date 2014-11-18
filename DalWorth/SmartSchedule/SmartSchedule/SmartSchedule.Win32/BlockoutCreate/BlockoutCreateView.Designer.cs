
namespace SmartSchedule.Win32.BlockoutCreate
{
    partial class BlockoutCreateView
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
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.m_timeStart = new SmartSchedule.Win32.Controls.TimeEditEx();
            this.m_timeEnd = new SmartSchedule.Win32.Controls.TimeEditEx();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_timeStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_timeEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.CausesValidation = false;
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.m_txtNotes);
            this.panelControl1.Controls.Add(this.m_timeStart);
            this.panelControl1.Controls.Add(this.m_timeEnd);
            this.panelControl1.Controls.Add(this.labelControl15);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(284, 270);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(7, 12);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(46, 13);
            this.labelControl5.TabIndex = 46;
            this.labelControl5.Text = "Start/End";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Location = new System.Drawing.Point(7, 35);
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Size = new System.Drawing.Size(271, 198);
            this.m_txtNotes.TabIndex = 45;
            // 
            // m_timeStart
            // 
            this.m_timeStart.EditValue = "7:38 PM";
            this.m_timeStart.Location = new System.Drawing.Point(67, 9);
            this.m_timeStart.MinuteIncrementInterval = 15;
            this.m_timeStart.Name = "m_timeStart";
            this.m_timeStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.m_timeStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_timeStart.Properties.Mask.EditMask = "t";
            this.m_timeStart.Properties.NullText = "Empty";
            this.m_timeStart.Size = new System.Drawing.Size(98, 20);
            this.m_timeStart.TabIndex = 42;
            // 
            // m_timeEnd
            // 
            this.m_timeEnd.EditValue = "7:38 PM";
            this.m_timeEnd.Location = new System.Drawing.Point(181, 9);
            this.m_timeEnd.MinuteIncrementInterval = 15;
            this.m_timeEnd.Name = "m_timeEnd";
            this.m_timeEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.m_timeEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_timeEnd.Properties.Mask.EditMask = "t";
            this.m_timeEnd.Properties.NullText = "Empty";
            this.m_timeEnd.Size = new System.Drawing.Size(97, 20);
            this.m_timeEnd.TabIndex = 43;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(171, 12);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(4, 13);
            this.labelControl15.TabIndex = 44;
            this.labelControl15.Text = "-";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(206, 244);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 12;
            this.m_btnCancel.Text = "Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(125, 244);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 11;
            this.m_btnOk.Text = "OK";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // BlockoutCreateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 270);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BlockoutCreateView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BlockoutCreateView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_timeStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_timeEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal Controls.TimeEditEx m_timeStart;
        internal Controls.TimeEditEx m_timeEnd;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        internal DevExpress.XtraEditors.MemoEdit m_txtNotes;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;

    }
}