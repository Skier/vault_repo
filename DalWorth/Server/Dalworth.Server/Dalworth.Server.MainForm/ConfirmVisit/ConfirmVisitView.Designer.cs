using Dalworth.Server.MainForm.Components;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.ConfirmVisit
{
    partial class ConfirmVisitView
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
            this.m_btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label13 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_chkWillCall = new DevExpress.XtraEditors.CheckEdit();
            this.m_chkBusy = new DevExpress.XtraEditors.CheckEdit();
            this.m_dtpConfirmTimeEnd = new Dalworth.Server.MainForm.Components.TimeEditEx();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.m_dtpConfirmTimeStart = new Dalworth.Server.MainForm.Components.TimeEditEx();
            this.m_lblServiceDate = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbTimeFrameTemplate = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_chkCallOnYourWay = new DevExpress.XtraEditors.CheckEdit();
            this.m_chkLeftMessage = new DevExpress.XtraEditors.CheckEdit();
            this.m_dtpPrefferedTimeFrameBegin = new Dalworth.Server.MainForm.Components.TimeEditEx();
            this.m_dtpPrefferedTimeFrameEnd = new Dalworth.Server.MainForm.Components.TimeEditEx();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_pnlContent = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.m_lblLastConfirmationMethod = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblLastConfirmedTimeFrame = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblLastConfirmationTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.m_ctlCustomer = new Dalworth.Server.MainForm.Components.CustomerViewEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.m_lblShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_txtVisitNotes = new DevExpress.XtraEditors.MemoEdit();
            this.m_ctlAddress = new Dalworth.Server.MainForm.Components.AddressViewEdit();
            this.m_btnDispatchWithoutConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblNotificationReasons = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkWillCall.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkBusy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpConfirmTimeEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpConfirmTimeStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTimeFrameTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkCallOnYourWay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkLeftMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpPrefferedTimeFrameBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpPrefferedTimeFrameEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlContent)).BeginInit();
            this.m_pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVisitNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnConfirm
            // 
            this.m_btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnConfirm.Location = new System.Drawing.Point(618, 321);
            this.m_btnConfirm.Name = "m_btnConfirm";
            this.m_btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.m_btnConfirm.TabIndex = 2;
            this.m_btnConfirm.Text = "C&onfirm";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(699, 321);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "C&ancel";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Service Date";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(5, 45);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(119, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "&Confirmation Time Frame";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_chkWillCall);
            this.groupControl1.Controls.Add(this.m_chkBusy);
            this.groupControl1.Controls.Add(this.m_dtpConfirmTimeEnd);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.m_dtpConfirmTimeStart);
            this.groupControl1.Controls.Add(this.m_lblServiceDate);
            this.groupControl1.Controls.Add(this.m_cmbTimeFrameTemplate);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.m_chkCallOnYourWay);
            this.groupControl1.Controls.Add(this.m_chkLeftMessage);
            this.groupControl1.Controls.Add(this.m_dtpPrefferedTimeFrameBegin);
            this.groupControl1.Controls.Add(this.m_dtpPrefferedTimeFrameEnd);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label13);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(296, 160);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Date and Time";
            // 
            // m_chkWillCall
            // 
            this.m_chkWillCall.Location = new System.Drawing.Point(229, 138);
            this.m_chkWillCall.Name = "m_chkWillCall";
            this.m_chkWillCall.Properties.Caption = "&Will call";
            this.m_chkWillCall.Size = new System.Drawing.Size(62, 19);
            this.m_chkWillCall.TabIndex = 12;
            // 
            // m_chkBusy
            // 
            this.m_chkBusy.Location = new System.Drawing.Point(229, 120);
            this.m_chkBusy.Name = "m_chkBusy";
            this.m_chkBusy.Properties.Caption = "&Busy";
            this.m_chkBusy.Size = new System.Drawing.Size(48, 19);
            this.m_chkBusy.TabIndex = 10;
            // 
            // m_dtpConfirmTimeEnd
            // 
            this.m_dtpConfirmTimeEnd.EditValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.m_dtpConfirmTimeEnd.Location = new System.Drawing.Point(221, 42);
            this.m_dtpConfirmTimeEnd.Name = "m_dtpConfirmTimeEnd";
            this.m_dtpConfirmTimeEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.m_dtpConfirmTimeEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpConfirmTimeEnd.Properties.Mask.EditMask = "h tt";
            this.m_dtpConfirmTimeEnd.Properties.NullText = "Any";
            this.m_dtpConfirmTimeEnd.Size = new System.Drawing.Size(70, 20);
            this.m_dtpConfirmTimeEnd.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(210, 45);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(4, 13);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "-";
            // 
            // m_dtpConfirmTimeStart
            // 
            this.m_dtpConfirmTimeStart.EditValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.m_dtpConfirmTimeStart.Location = new System.Drawing.Point(131, 42);
            this.m_dtpConfirmTimeStart.Name = "m_dtpConfirmTimeStart";
            this.m_dtpConfirmTimeStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.m_dtpConfirmTimeStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpConfirmTimeStart.Properties.Mask.EditMask = "h tt";
            this.m_dtpConfirmTimeStart.Properties.NullText = "Any";
            this.m_dtpConfirmTimeStart.Size = new System.Drawing.Size(70, 20);
            this.m_dtpConfirmTimeStart.TabIndex = 3;
            // 
            // m_lblServiceDate
            // 
            this.m_lblServiceDate.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblServiceDate.Appearance.Options.UseFont = true;
            this.m_lblServiceDate.Location = new System.Drawing.Point(131, 26);
            this.m_lblServiceDate.Name = "m_lblServiceDate";
            this.m_lblServiceDate.Size = new System.Drawing.Size(54, 13);
            this.m_lblServiceDate.TabIndex = 1;
            this.m_lblServiceDate.Text = "1/1/2008";
            // 
            // m_cmbTimeFrameTemplate
            // 
            this.m_cmbTimeFrameTemplate.EditValue = 0;
            this.m_cmbTimeFrameTemplate.Location = new System.Drawing.Point(131, 68);
            this.m_cmbTimeFrameTemplate.Name = "m_cmbTimeFrameTemplate";
            this.m_cmbTimeFrameTemplate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTimeFrameTemplate.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Custom", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("In AM", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("In PM", 3, -1)});
            this.m_cmbTimeFrameTemplate.Size = new System.Drawing.Size(160, 20);
            this.m_cmbTimeFrameTemplate.TabIndex = 6;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(210, 97);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(4, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "-";
            // 
            // m_chkCallOnYourWay
            // 
            this.m_chkCallOnYourWay.Location = new System.Drawing.Point(129, 138);
            this.m_chkCallOnYourWay.Name = "m_chkCallOnYourWay";
            this.m_chkCallOnYourWay.Properties.Caption = "Call on the wa&y";
            this.m_chkCallOnYourWay.Size = new System.Drawing.Size(97, 19);
            this.m_chkCallOnYourWay.TabIndex = 11;
            // 
            // m_chkLeftMessage
            // 
            this.m_chkLeftMessage.Location = new System.Drawing.Point(129, 120);
            this.m_chkLeftMessage.Name = "m_chkLeftMessage";
            this.m_chkLeftMessage.Properties.Caption = "&Left Message";
            this.m_chkLeftMessage.Size = new System.Drawing.Size(86, 19);
            this.m_chkLeftMessage.TabIndex = 9;
            // 
            // m_dtpPrefferedTimeFrameBegin
            // 
            this.m_dtpPrefferedTimeFrameBegin.EditValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.m_dtpPrefferedTimeFrameBegin.Location = new System.Drawing.Point(131, 94);
            this.m_dtpPrefferedTimeFrameBegin.Name = "m_dtpPrefferedTimeFrameBegin";
            this.m_dtpPrefferedTimeFrameBegin.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpPrefferedTimeFrameBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpPrefferedTimeFrameBegin.Properties.Mask.EditMask = "h tt";
            this.m_dtpPrefferedTimeFrameBegin.Properties.NullText = "Any";
            this.m_dtpPrefferedTimeFrameBegin.Size = new System.Drawing.Size(70, 20);
            this.m_dtpPrefferedTimeFrameBegin.TabIndex = 7;
            // 
            // m_dtpPrefferedTimeFrameEnd
            // 
            this.m_dtpPrefferedTimeFrameEnd.EditValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.m_dtpPrefferedTimeFrameEnd.Location = new System.Drawing.Point(221, 94);
            this.m_dtpPrefferedTimeFrameEnd.Name = "m_dtpPrefferedTimeFrameEnd";
            this.m_dtpPrefferedTimeFrameEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpPrefferedTimeFrameEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpPrefferedTimeFrameEnd.Properties.Mask.EditMask = "h tt";
            this.m_dtpPrefferedTimeFrameEnd.Properties.NullText = "Any";
            this.m_dtpPrefferedTimeFrameEnd.Size = new System.Drawing.Size(70, 20);
            this.m_dtpPrefferedTimeFrameEnd.TabIndex = 8;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 69);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 26);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "&Preffered\r\nTime Frame";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnConfirm);
            this.panelControl1.Controls.Add(this.m_pnlContent);
            this.panelControl1.Controls.Add(this.m_btnDispatchWithoutConfirm);
            this.panelControl1.Controls.Add(this.m_lblNotificationReasons);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(779, 349);
            this.panelControl1.TabIndex = 0;
            // 
            // m_pnlContent
            // 
            this.m_pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_pnlContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlContent.Controls.Add(this.groupControl1);
            this.m_pnlContent.Controls.Add(this.groupControl2);
            this.m_pnlContent.Controls.Add(this.m_ctlCustomer);
            this.m_pnlContent.Controls.Add(this.groupControl3);
            this.m_pnlContent.Controls.Add(this.m_ctlAddress);
            this.m_pnlContent.Location = new System.Drawing.Point(5, 66);
            this.m_pnlContent.Name = "m_pnlContent";
            this.m_pnlContent.Size = new System.Drawing.Size(770, 245);
            this.m_pnlContent.TabIndex = 51;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.m_lblLastConfirmationMethod);
            this.groupControl2.Controls.Add(this.labelControl9);
            this.groupControl2.Controls.Add(this.m_lblLastConfirmedTimeFrame);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.m_lblLastConfirmationTime);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Location = new System.Drawing.Point(0, 163);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(296, 80);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "Last Confirmation";
            // 
            // m_lblLastConfirmationMethod
            // 
            this.m_lblLastConfirmationMethod.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblLastConfirmationMethod.Appearance.Options.UseFont = true;
            this.m_lblLastConfirmationMethod.Location = new System.Drawing.Point(131, 61);
            this.m_lblLastConfirmationMethod.Name = "m_lblLastConfirmationMethod";
            this.m_lblLastConfirmationMethod.Size = new System.Drawing.Size(76, 13);
            this.m_lblLastConfirmationMethod.TabIndex = 29;
            this.m_lblLastConfirmationMethod.Text = "Left Message";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(5, 61);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(100, 13);
            this.labelControl9.TabIndex = 28;
            this.labelControl9.Text = "Confirmation Method";
            // 
            // m_lblLastConfirmedTimeFrame
            // 
            this.m_lblLastConfirmedTimeFrame.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblLastConfirmedTimeFrame.Appearance.Options.UseFont = true;
            this.m_lblLastConfirmedTimeFrame.Location = new System.Drawing.Point(131, 42);
            this.m_lblLastConfirmedTimeFrame.Name = "m_lblLastConfirmedTimeFrame";
            this.m_lblLastConfirmedTimeFrame.Size = new System.Drawing.Size(120, 13);
            this.m_lblLastConfirmedTimeFrame.TabIndex = 27;
            this.m_lblLastConfirmedTimeFrame.Text = "11:00 PM - 12:00 PM";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(5, 42);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(119, 13);
            this.labelControl6.TabIndex = 26;
            this.labelControl6.Text = "Confirmation Time Frame";
            // 
            // m_lblLastConfirmationTime
            // 
            this.m_lblLastConfirmationTime.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblLastConfirmationTime.Appearance.Options.UseFont = true;
            this.m_lblLastConfirmationTime.Location = new System.Drawing.Point(131, 23);
            this.m_lblLastConfirmationTime.Name = "m_lblLastConfirmationTime";
            this.m_lblLastConfirmationTime.Size = new System.Drawing.Size(54, 13);
            this.m_lblLastConfirmationTime.TabIndex = 25;
            this.m_lblLastConfirmationTime.Text = "11:00 PM";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(55, 13);
            this.labelControl7.TabIndex = 24;
            this.labelControl7.Text = "Time of Call";
            // 
            // m_ctlCustomer
            // 
            this.m_ctlCustomer.Address = null;
            this.m_ctlCustomer.Customer = null;
            this.m_ctlCustomer.EmailVisible = false;
            this.m_ctlCustomer.Location = new System.Drawing.Point(302, 0);
            this.m_ctlCustomer.Name = "m_ctlCustomer";
            this.m_ctlCustomer.Size = new System.Drawing.Size(232, 107);
            this.m_ctlCustomer.TabIndex = 36;
            this.m_ctlCustomer.TabStop = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.m_lblShortcut);
            this.groupControl3.Controls.Add(this.m_txtVisitNotes);
            this.groupControl3.Location = new System.Drawing.Point(302, 113);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(468, 130);
            this.groupControl3.TabIndex = 50;
            this.groupControl3.Text = "&Notes";
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut.Location = new System.Drawing.Point(5, 36);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 0;
            this.m_lblShortcut.Text = "&N shortcut";
            // 
            // m_txtVisitNotes
            // 
            this.m_txtVisitNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtVisitNotes.Location = new System.Drawing.Point(2, 20);
            this.m_txtVisitNotes.Name = "m_txtVisitNotes";
            this.m_txtVisitNotes.Properties.MaxLength = 1500;
            this.m_txtVisitNotes.Size = new System.Drawing.Size(464, 108);
            this.m_txtVisitNotes.TabIndex = 1;
            // 
            // m_ctlAddress
            // 
            this.m_ctlAddress.BaseAddress = null;
            this.m_ctlAddress.BaseAddressName = null;
            this.m_ctlAddress.Caption = null;
            this.m_ctlAddress.CurrentAddress = null;
            this.m_ctlAddress.EditButtonText = "Edi&t";
            this.m_ctlAddress.IsBaseAddressActive = false;
            this.m_ctlAddress.Location = new System.Drawing.Point(540, 0);
            this.m_ctlAddress.Name = "m_ctlAddress";
            this.m_ctlAddress.Size = new System.Drawing.Size(230, 107);
            this.m_ctlAddress.TabIndex = 35;
            this.m_ctlAddress.TabStop = false;
            // 
            // m_btnDispatchWithoutConfirm
            // 
            this.m_btnDispatchWithoutConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDispatchWithoutConfirm.Location = new System.Drawing.Point(579, 321);
            this.m_btnDispatchWithoutConfirm.Name = "m_btnDispatchWithoutConfirm";
            this.m_btnDispatchWithoutConfirm.Size = new System.Drawing.Size(114, 23);
            this.m_btnDispatchWithoutConfirm.TabIndex = 3;
            this.m_btnDispatchWithoutConfirm.Text = "&Dispatch w/o confirm";
            this.m_btnDispatchWithoutConfirm.Visible = false;
            // 
            // m_lblNotificationReasons
            // 
            this.m_lblNotificationReasons.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblNotificationReasons.Appearance.Options.UseFont = true;
            this.m_lblNotificationReasons.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.m_lblNotificationReasons.Location = new System.Drawing.Point(5, 24);
            this.m_lblNotificationReasons.Name = "m_lblNotificationReasons";
            this.m_lblNotificationReasons.Size = new System.Drawing.Size(557, 39);
            this.m_lblNotificationReasons.TabIndex = 1;
            this.m_lblNotificationReasons.Text = "* Estimated arrival time is out of confirmed time frame\r\n* We couldn\'t get into t" +
                "ouch with a customer during last confirmation call and left a message\r\n* Custome" +
                "r asked to call on the way";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(300, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "It is suggested to call a customer. Reason(s) to do this is(are):";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // ConfirmVisitView
            // 
            this.AcceptButton = this.m_btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(779, 349);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmVisitView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConfirmVisitView";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkWillCall.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkBusy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpConfirmTimeEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpConfirmTimeStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTimeFrameTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkCallOnYourWay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkLeftMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpPrefferedTimeFrameBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpPrefferedTimeFrameEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlContent)).EndInit();
            this.m_pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVisitNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal SimpleButton m_btnConfirm;
        internal SimpleButton m_btnCancel;
        private LabelControl label2;
        private LabelControl label13;
        private GroupControl groupControl1;
        private PanelControl panelControl1;
        internal Dalworth.Server.MainForm.Components.AddressViewEdit m_ctlAddress;
        internal Dalworth.Server.MainForm.Components.CustomerViewEdit m_ctlCustomer;
        internal MemoEdit m_txtVisitNotes;
        internal CheckEdit m_chkCallOnYourWay;
        internal CheckEdit m_chkLeftMessage;
        internal ImageComboBoxEdit m_cmbTimeFrameTemplate;
        private LabelControl labelControl2;
        internal TimeEditEx m_dtpPrefferedTimeFrameBegin;
        internal TimeEditEx m_dtpPrefferedTimeFrameEnd;
        private LabelControl labelControl3;
        private GroupControl groupControl2;
        internal LabelControl m_lblLastConfirmationMethod;
        private LabelControl labelControl9;
        internal LabelControl m_lblLastConfirmedTimeFrame;
        private LabelControl labelControl6;
        internal LabelControl m_lblLastConfirmationTime;
        private LabelControl labelControl7;
        private GroupControl groupControl3;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal LabelControl m_lblServiceDate;
        internal PanelControl m_pnlContent;
        internal SimpleButton m_btnDispatchWithoutConfirm;
        internal LabelControl m_lblNotificationReasons;
        private LabelControl labelControl1;
        internal TimeEditEx m_dtpConfirmTimeStart;
        private LabelControl m_lblShortcut;
        internal TimeEditEx m_dtpConfirmTimeEnd;
        private LabelControl labelControl4;
        internal CheckEdit m_chkBusy;
        internal CheckEdit m_chkWillCall;
    }
}