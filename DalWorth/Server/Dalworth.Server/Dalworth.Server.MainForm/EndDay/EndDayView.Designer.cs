using TimeEditEx=Dalworth.Server.MainForm.Components.TimeEditEx;

namespace Dalworth.Server.MainForm.EndDay
{
    partial class EndDayView
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
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_timeEndDay = new Dalworth.Server.MainForm.Components.TimeEditEx();
            this.m_lblEndDayTime = new DevExpress.XtraEditors.LabelControl();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_timeEndDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "gridBand1";
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Width = 225;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_timeEndDay);
            this.panelControl1.Controls.Add(this.m_lblEndDayTime);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(303, 122);
            this.panelControl1.TabIndex = 0;
            // 
            // m_timeEndDay
            // 
            this.m_timeEndDay.EditValue = new System.DateTime(2008, 2, 19, 23, 30, 0, 0);
            this.m_timeEndDay.Location = new System.Drawing.Point(148, 37);
            this.m_timeEndDay.Name = "m_timeEndDay";
            this.m_timeEndDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_timeEndDay.Properties.Mask.EditMask = "t";
            this.m_timeEndDay.Size = new System.Drawing.Size(100, 20);
            this.m_timeEndDay.TabIndex = 1;
            // 
            // m_lblEndDayTime
            // 
            this.m_lblEndDayTime.AllowDrop = true;
            this.m_lblEndDayTime.Location = new System.Drawing.Point(68, 40);
            this.m_lblEndDayTime.Name = "m_lblEndDayTime";
            this.m_lblEndDayTime.Size = new System.Drawing.Size(65, 13);
            this.m_lblEndDayTime.TabIndex = 0;
            this.m_lblEndDayTime.Text = "&End Day Time";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(145, 93);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Text = "&OK";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(226, 93);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 3;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // EndDayView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(303, 122);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EndDayView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EndDayView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_timeEndDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal TimeEditEx m_timeEndDay;
        internal DevExpress.XtraEditors.LabelControl m_lblEndDayTime;

    }
}