namespace Dalworth.Server.MainForm.TaskEdit
{
    partial class TaskEditView
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_messages = new Dalworth.Server.MainForm.Components.Messages();
            this.m_rugsView = new Dalworth.Server.MainForm.Components.RugsView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_cmbDuration = new DevExpress.XtraScheduler.UI.DurationEdit();
            this.m_dtpServiceDate = new DevExpress.XtraEditors.DateEdit();
            this.m_lblCreateDate = new DevExpress.XtraEditors.LabelControl();
            this.m_lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.m_lblType = new DevExpress.XtraEditors.LabelControl();
            this.m_lblNumber = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbDuration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpServiceDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpServiceDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_messages);
            this.panelControl1.Controls.Add(this.m_rugsView);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(746, 391);
            this.panelControl1.TabIndex = 0;
            // 
            // m_messages
            // 
            this.m_messages.Location = new System.Drawing.Point(5, 204);
            this.m_messages.Message1LabelText = "Description";
            this.m_messages.Message1Text = "";
            this.m_messages.Message2LabelText = "Notes";
            this.m_messages.Message2Text = "";
            this.m_messages.Message3LabelText = "Message";
            this.m_messages.Message3Text = "";
            this.m_messages.Name = "m_messages";
            this.m_messages.ReadOnly = false;
            this.m_messages.Size = new System.Drawing.Size(737, 151);
            this.m_messages.TabIndex = 12;
            // 
            // m_rugsView
            // 
            this.m_rugsView.IsEditable = false;
            this.m_rugsView.Items = null;
            this.m_rugsView.Location = new System.Drawing.Point(219, 5);
            this.m_rugsView.Name = "m_rugsView";
            this.m_rugsView.Size = new System.Drawing.Size(523, 196);
            this.m_rugsView.TabIndex = 11;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_cmbDuration);
            this.groupControl1.Controls.Add(this.m_dtpServiceDate);
            this.groupControl1.Controls.Add(this.m_lblCreateDate);
            this.groupControl1.Controls.Add(this.m_lblStatus);
            this.groupControl1.Controls.Add(this.m_lblType);
            this.groupControl1.Controls.Add(this.m_lblNumber);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(5, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(208, 193);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "General";
            // 
            // m_cmbDuration
            // 
            this.m_cmbDuration.Location = new System.Drawing.Point(85, 125);
            this.m_cmbDuration.Name = "m_cmbDuration";
            this.m_cmbDuration.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbDuration.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_cmbDuration.Size = new System.Drawing.Size(118, 20);
            this.m_cmbDuration.TabIndex = 1;
            // 
            // m_dtpServiceDate
            // 
            this.m_dtpServiceDate.EditValue = null;
            this.m_dtpServiceDate.Location = new System.Drawing.Point(85, 99);
            this.m_dtpServiceDate.Name = "m_dtpServiceDate";
            this.m_dtpServiceDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.m_dtpServiceDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpServiceDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpServiceDate.Size = new System.Drawing.Size(118, 20);
            this.m_dtpServiceDate.TabIndex = 0;
            // 
            // m_lblCreateDate
            // 
            this.m_lblCreateDate.AllowDrop = true;
            this.m_lblCreateDate.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblCreateDate.Appearance.Options.UseFont = true;
            this.m_lblCreateDate.Location = new System.Drawing.Point(87, 80);
            this.m_lblCreateDate.Name = "m_lblCreateDate";
            this.m_lblCreateDate.Size = new System.Drawing.Size(44, 13);
            this.m_lblCreateDate.TabIndex = 9;
            this.m_lblCreateDate.Text = "Number";
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.AllowDrop = true;
            this.m_lblStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblStatus.Appearance.Options.UseFont = true;
            this.m_lblStatus.Location = new System.Drawing.Point(87, 61);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(44, 13);
            this.m_lblStatus.TabIndex = 8;
            this.m_lblStatus.Text = "Number";
            // 
            // m_lblType
            // 
            this.m_lblType.AllowDrop = true;
            this.m_lblType.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblType.Appearance.Options.UseFont = true;
            this.m_lblType.Location = new System.Drawing.Point(87, 42);
            this.m_lblType.Name = "m_lblType";
            this.m_lblType.Size = new System.Drawing.Size(44, 13);
            this.m_lblType.TabIndex = 7;
            this.m_lblType.Text = "Number";
            // 
            // m_lblNumber
            // 
            this.m_lblNumber.AllowDrop = true;
            this.m_lblNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblNumber.Appearance.Options.UseFont = true;
            this.m_lblNumber.Location = new System.Drawing.Point(87, 23);
            this.m_lblNumber.Name = "m_lblNumber";
            this.m_lblNumber.Size = new System.Drawing.Size(44, 13);
            this.m_lblNumber.TabIndex = 6;
            this.m_lblNumber.Text = "Number";
            // 
            // labelControl6
            // 
            this.labelControl6.AllowDrop = true;
            this.labelControl6.Location = new System.Drawing.Point(7, 129);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(41, 13);
            this.labelControl6.TabIndex = 5;
            this.labelControl6.Text = "Duration";
            // 
            // labelControl5
            // 
            this.labelControl5.AllowDrop = true;
            this.labelControl5.Location = new System.Drawing.Point(7, 102);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(61, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Service Date";
            // 
            // labelControl4
            // 
            this.labelControl4.AllowDrop = true;
            this.labelControl4.Location = new System.Drawing.Point(7, 80);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(39, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Created";
            // 
            // labelControl3
            // 
            this.labelControl3.AllowDrop = true;
            this.labelControl3.Location = new System.Drawing.Point(7, 61);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(31, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Status";
            // 
            // labelControl2
            // 
            this.labelControl2.AllowDrop = true;
            this.labelControl2.Location = new System.Drawing.Point(7, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Type";
            // 
            // labelControl1
            // 
            this.labelControl1.AllowDrop = true;
            this.labelControl1.Location = new System.Drawing.Point(7, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Number";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(667, 361);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 7;
            this.m_btnCancel.Text = "Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(586, 361);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 6;
            this.m_btnOk.Text = "OK";
            // 
            // TaskEditView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(746, 391);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskEditView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TaskEditView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbDuration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpServiceDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpServiceDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraScheduler.UI.DurationEdit m_cmbDuration;
        internal DevExpress.XtraEditors.DateEdit m_dtpServiceDate;
        internal DevExpress.XtraEditors.LabelControl m_lblCreateDate;
        internal DevExpress.XtraEditors.LabelControl m_lblStatus;
        internal DevExpress.XtraEditors.LabelControl m_lblType;
        internal DevExpress.XtraEditors.LabelControl m_lblNumber;
        internal Dalworth.Server.MainForm.Components.RugsView m_rugsView;
        internal Dalworth.Server.MainForm.Components.Messages m_messages;
    }
}