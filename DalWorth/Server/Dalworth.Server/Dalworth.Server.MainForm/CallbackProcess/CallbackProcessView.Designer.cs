using Dalworth.Server.MainForm.Components;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.CallbackProcess
{
    partial class CallbackProcessView
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
            this.m_btnProcess = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_groupNextCall = new DevExpress.XtraEditors.GroupControl();
            this.m_cmbNextCallPeriod = new DevExpress.XtraScheduler.UI.DurationEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_dtpNextCallDate = new DevExpress.XtraEditors.DateEdit();
            this.m_chkCreateVisit = new DevExpress.XtraEditors.CheckEdit();
            this.m_chkCallReschedule = new DevExpress.XtraEditors.CheckEdit();
            this.m_chkBusy = new DevExpress.XtraEditors.CheckEdit();
            this.m_chkLeftMessage = new DevExpress.XtraEditors.CheckEdit();
            this.m_chkDoNotCall = new DevExpress.XtraEditors.CheckEdit();
            this.m_chkNotInterested = new DevExpress.XtraEditors.CheckEdit();
            this.m_ctlCustomer = new Dalworth.Server.MainForm.Components.CustomerViewEdit();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_groupNextCall)).BeginInit();
            this.m_groupNextCall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbNextCallPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpNextCallDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpNextCallDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkCreateVisit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkCallReschedule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkBusy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkLeftMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkDoNotCall.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkNotInterested.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnProcess
            // 
            this.m_btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnProcess.Location = new System.Drawing.Point(208, 222);
            this.m_btnProcess.Name = "m_btnProcess";
            this.m_btnProcess.Size = new System.Drawing.Size(75, 23);
            this.m_btnProcess.TabIndex = 8;
            this.m_btnProcess.Text = "&Process";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(289, 222);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 9;
            this.m_btnCancel.Text = "C&ancel";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_groupNextCall);
            this.panelControl1.Controls.Add(this.m_chkCreateVisit);
            this.panelControl1.Controls.Add(this.m_chkCallReschedule);
            this.panelControl1.Controls.Add(this.m_chkBusy);
            this.panelControl1.Controls.Add(this.m_chkLeftMessage);
            this.panelControl1.Controls.Add(this.m_chkDoNotCall);
            this.panelControl1.Controls.Add(this.m_chkNotInterested);
            this.panelControl1.Controls.Add(this.m_ctlCustomer);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnProcess);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(369, 250);
            this.panelControl1.TabIndex = 0;
            // 
            // m_groupNextCall
            // 
            this.m_groupNextCall.Controls.Add(this.m_cmbNextCallPeriod);
            this.m_groupNextCall.Controls.Add(this.labelControl1);
            this.m_groupNextCall.Controls.Add(this.labelControl2);
            this.m_groupNextCall.Controls.Add(this.m_dtpNextCallDate);
            this.m_groupNextCall.Location = new System.Drawing.Point(5, 119);
            this.m_groupNextCall.Name = "m_groupNextCall";
            this.m_groupNextCall.Size = new System.Drawing.Size(232, 84);
            this.m_groupNextCall.TabIndex = 7;
            this.m_groupNextCall.Text = "Next Call";
            // 
            // m_cmbNextCallPeriod
            // 
            this.m_cmbNextCallPeriod.Location = new System.Drawing.Point(72, 27);
            this.m_cmbNextCallPeriod.Name = "m_cmbNextCallPeriod";
            this.m_cmbNextCallPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbNextCallPeriod.Size = new System.Drawing.Size(155, 20);
            this.m_cmbNextCallPeriod.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 58);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "&Exact Date";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(38, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "In&terval";
            // 
            // m_dtpNextCallDate
            // 
            this.m_dtpNextCallDate.EditValue = null;
            this.m_dtpNextCallDate.Location = new System.Drawing.Point(72, 55);
            this.m_dtpNextCallDate.Name = "m_dtpNextCallDate";
            this.m_dtpNextCallDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpNextCallDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpNextCallDate.Size = new System.Drawing.Size(155, 20);
            this.m_dtpNextCallDate.TabIndex = 3;
            // 
            // m_chkCreateVisit
            // 
            this.m_chkCreateVisit.Location = new System.Drawing.Point(243, 55);
            this.m_chkCreateVisit.Name = "m_chkCreateVisit";
            this.m_chkCreateVisit.Properties.Caption = "&Create Visit";
            this.m_chkCreateVisit.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.m_chkCreateVisit.Properties.RadioGroupIndex = 1;
            this.m_chkCreateVisit.Size = new System.Drawing.Size(75, 19);
            this.m_chkCreateVisit.TabIndex = 3;
            this.m_chkCreateVisit.TabStop = false;
            // 
            // m_chkCallReschedule
            // 
            this.m_chkCallReschedule.Location = new System.Drawing.Point(243, 130);
            this.m_chkCallReschedule.Name = "m_chkCallReschedule";
            this.m_chkCallReschedule.Properties.Caption = "Call &Reschedule";
            this.m_chkCallReschedule.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.m_chkCallReschedule.Properties.RadioGroupIndex = 1;
            this.m_chkCallReschedule.Size = new System.Drawing.Size(108, 19);
            this.m_chkCallReschedule.TabIndex = 6;
            this.m_chkCallReschedule.TabStop = false;
            // 
            // m_chkBusy
            // 
            this.m_chkBusy.Location = new System.Drawing.Point(243, 105);
            this.m_chkBusy.Name = "m_chkBusy";
            this.m_chkBusy.Properties.Caption = "&Busy";
            this.m_chkBusy.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.m_chkBusy.Properties.RadioGroupIndex = 1;
            this.m_chkBusy.Size = new System.Drawing.Size(75, 19);
            this.m_chkBusy.TabIndex = 5;
            this.m_chkBusy.TabStop = false;
            // 
            // m_chkLeftMessage
            // 
            this.m_chkLeftMessage.Location = new System.Drawing.Point(243, 80);
            this.m_chkLeftMessage.Name = "m_chkLeftMessage";
            this.m_chkLeftMessage.Properties.Caption = "&Left a Message";
            this.m_chkLeftMessage.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.m_chkLeftMessage.Properties.RadioGroupIndex = 1;
            this.m_chkLeftMessage.Size = new System.Drawing.Size(108, 19);
            this.m_chkLeftMessage.TabIndex = 4;
            this.m_chkLeftMessage.TabStop = false;
            // 
            // m_chkDoNotCall
            // 
            this.m_chkDoNotCall.Location = new System.Drawing.Point(243, 30);
            this.m_chkDoNotCall.Name = "m_chkDoNotCall";
            this.m_chkDoNotCall.Properties.Caption = "&Do not Call";
            this.m_chkDoNotCall.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.m_chkDoNotCall.Properties.RadioGroupIndex = 1;
            this.m_chkDoNotCall.Size = new System.Drawing.Size(75, 19);
            this.m_chkDoNotCall.TabIndex = 2;
            this.m_chkDoNotCall.TabStop = false;
            // 
            // m_chkNotInterested
            // 
            this.m_chkNotInterested.EditValue = true;
            this.m_chkNotInterested.Location = new System.Drawing.Point(243, 5);
            this.m_chkNotInterested.Name = "m_chkNotInterested";
            this.m_chkNotInterested.Properties.Caption = "&Not Interested";
            this.m_chkNotInterested.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.m_chkNotInterested.Properties.RadioGroupIndex = 1;
            this.m_chkNotInterested.Size = new System.Drawing.Size(108, 19);
            this.m_chkNotInterested.TabIndex = 1;
            // 
            // m_ctlCustomer
            // 
            this.m_ctlCustomer.Address = null;
            this.m_ctlCustomer.Customer = null;
            this.m_ctlCustomer.EmailVisible = false;
            this.m_ctlCustomer.Location = new System.Drawing.Point(5, 5);
            this.m_ctlCustomer.Name = "m_ctlCustomer";
            this.m_ctlCustomer.Size = new System.Drawing.Size(232, 107);
            this.m_ctlCustomer.TabIndex = 0;
            this.m_ctlCustomer.TabStop = false;
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // CallbackProcessView
            // 
            this.AcceptButton = this.m_btnProcess;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(369, 250);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CallbackProcessView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CallbackProcessView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_groupNextCall)).EndInit();
            this.m_groupNextCall.ResumeLayout(false);
            this.m_groupNextCall.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbNextCallPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpNextCallDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpNextCallDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkCreateVisit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkCallReschedule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkBusy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkLeftMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkDoNotCall.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkNotInterested.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal SimpleButton m_btnProcess;
        internal SimpleButton m_btnCancel;
        private PanelControl panelControl1;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal CustomerViewEdit m_ctlCustomer;
        internal CheckEdit m_chkCallReschedule;
        internal CheckEdit m_chkBusy;
        internal CheckEdit m_chkLeftMessage;
        internal CheckEdit m_chkDoNotCall;
        internal CheckEdit m_chkNotInterested;
        private LabelControl labelControl1;
        internal DevExpress.XtraEditors.DateEdit m_dtpNextCallDate;
        private LabelControl labelControl2;
        internal CheckEdit m_chkCreateVisit;
        internal DevExpress.XtraScheduler.UI.DurationEdit m_cmbNextCallPeriod;
        internal GroupControl m_groupNextCall;
    }
}