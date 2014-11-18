using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.DispatchConfirm
{
    partial class DispatchConfirmView
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
            this.m_btnYes = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnNoContinueDispatch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblReasons = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnNoCancelDispatch = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnYes
            // 
            this.m_btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnYes.Location = new System.Drawing.Point(113, 108);
            this.m_btnYes.Name = "m_btnYes";
            this.m_btnYes.Size = new System.Drawing.Size(75, 23);
            this.m_btnYes.TabIndex = 2;
            this.m_btnYes.Text = "Yes";
            // 
            // m_btnNoContinueDispatch
            // 
            this.m_btnNoContinueDispatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnNoContinueDispatch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnNoContinueDispatch.Location = new System.Drawing.Point(194, 108);
            this.m_btnNoContinueDispatch.Name = "m_btnNoContinueDispatch";
            this.m_btnNoContinueDispatch.Size = new System.Drawing.Size(127, 23);
            this.m_btnNoContinueDispatch.TabIndex = 3;
            this.m_btnNoContinueDispatch.Text = "No (continue dispatch)";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_lblReasons);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_btnNoCancelDispatch);
            this.panelControl1.Controls.Add(this.m_btnNoContinueDispatch);
            this.panelControl1.Controls.Add(this.m_btnYes);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(445, 136);
            this.panelControl1.TabIndex = 4;
            // 
            // m_lblReasons
            // 
            this.m_lblReasons.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblReasons.Appearance.Options.UseFont = true;
            this.m_lblReasons.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.m_lblReasons.Location = new System.Drawing.Point(5, 24);
            this.m_lblReasons.Name = "m_lblReasons";
            this.m_lblReasons.Size = new System.Drawing.Size(435, 52);
            this.m_lblReasons.TabIndex = 7;
            this.m_lblReasons.Text = "* Estimated arrival time is out of confirmed time frame\r\n* We couldn\'t get into t" +
                "ouch with a customer during last confirmation call and left a message\r\n* Custome" +
                "r asked to call on the way";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 82);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(157, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Would you like to reconfirm visit?";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(378, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "It is suggested to reconfirm visit with a customer. Reason(s) to do this is (are)" +
                ":";
            // 
            // m_btnNoCancelDispatch
            // 
            this.m_btnNoCancelDispatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnNoCancelDispatch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnNoCancelDispatch.Location = new System.Drawing.Point(327, 108);
            this.m_btnNoCancelDispatch.Name = "m_btnNoCancelDispatch";
            this.m_btnNoCancelDispatch.Size = new System.Drawing.Size(113, 23);
            this.m_btnNoCancelDispatch.TabIndex = 4;
            this.m_btnNoCancelDispatch.Text = "No (cancel dispatch)";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // DispatchConfirmView
            // 
            this.AcceptButton = this.m_btnYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnNoCancelDispatch;
            this.ClientSize = new System.Drawing.Size(445, 136);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DispatchConfirmView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DispatchConfirmView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal SimpleButton m_btnYes;
        internal SimpleButton m_btnNoContinueDispatch;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal SimpleButton m_btnNoCancelDispatch;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        internal LabelControl m_lblReasons;
    }
}