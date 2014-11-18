namespace Dalworth.Server.MainForm.DashboardCustomize
{
    partial class DashboardCustomizeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardCustomizeView));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblShortcut = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtVisibleCount = new DevExpress.XtraEditors.SpinEdit();
            this.m_cmbTechnicians = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_btnDown = new DevExpress.XtraEditors.SimpleButton();
            this.m_imagesUpDown = new System.Windows.Forms.ImageList(this.components);
            this.m_btnUp = new DevExpress.XtraEditors.SimpleButton();
            this.m_lstTechnicians = new DevExpress.XtraEditors.ImageListBoxControl();
            this.m_imagesYesNo = new System.Windows.Forms.ImageList(this.components);
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVisibleCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTechnicians.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_lstTechnicians)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblShortcut);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_txtVisibleCount);
            this.panelControl1.Controls.Add(this.m_cmbTechnicians);
            this.panelControl1.Controls.Add(this.m_btnDown);
            this.panelControl1.Controls.Add(this.m_btnUp);
            this.panelControl1.Controls.Add(this.m_lstTechnicians);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(307, 338);
            this.panelControl1.TabIndex = 0;
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut.Location = new System.Drawing.Point(12, 50);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 3;
            this.m_lblShortcut.Text = "&B shortcut";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(88, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Active &Technicians";
            // 
            // m_txtVisibleCount
            // 
            this.m_txtVisibleCount.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_txtVisibleCount.Location = new System.Drawing.Point(112, 5);
            this.m_txtVisibleCount.Name = "m_txtVisibleCount";
            this.m_txtVisibleCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_txtVisibleCount.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.m_txtVisibleCount.Properties.IsFloatValue = false;
            this.m_txtVisibleCount.Properties.Mask.EditMask = "N00";
            this.m_txtVisibleCount.Properties.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_txtVisibleCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_txtVisibleCount.Size = new System.Drawing.Size(57, 20);
            this.m_txtVisibleCount.TabIndex = 1;
            // 
            // m_cmbTechnicians
            // 
            this.m_cmbTechnicians.Location = new System.Drawing.Point(40, 233);
            this.m_cmbTechnicians.Name = "m_cmbTechnicians";
            this.m_cmbTechnicians.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTechnicians.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("1", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("2", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("3", 2, -1)});
            this.m_cmbTechnicians.Size = new System.Drawing.Size(226, 20);
            this.m_cmbTechnicians.TabIndex = 15;
            this.m_cmbTechnicians.TabStop = false;
            this.m_cmbTechnicians.Visible = false;
            // 
            // m_btnDown
            // 
            this.m_btnDown.ImageIndex = 0;
            this.m_btnDown.ImageList = this.m_imagesUpDown;
            this.m_btnDown.Location = new System.Drawing.Point(276, 133);
            this.m_btnDown.Name = "m_btnDown";
            this.m_btnDown.Size = new System.Drawing.Size(25, 32);
            this.m_btnDown.TabIndex = 6;
            // 
            // m_imagesUpDown
            // 
            this.m_imagesUpDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesUpDown.ImageStream")));
            this.m_imagesUpDown.TransparentColor = System.Drawing.Color.Magenta;
            this.m_imagesUpDown.Images.SetKeyName(0, "down.bmp");
            this.m_imagesUpDown.Images.SetKeyName(1, "up.bmp");
            // 
            // m_btnUp
            // 
            this.m_btnUp.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_btnUp.Appearance.Options.UseFont = true;
            this.m_btnUp.ImageIndex = 1;
            this.m_btnUp.ImageList = this.m_imagesUpDown;
            this.m_btnUp.Location = new System.Drawing.Point(276, 95);
            this.m_btnUp.Name = "m_btnUp";
            this.m_btnUp.Size = new System.Drawing.Size(25, 32);
            this.m_btnUp.TabIndex = 5;
            // 
            // m_lstTechnicians
            // 
            this.m_lstTechnicians.DisplayMember = "Name";
            this.m_lstTechnicians.ImageIndexMember = "ImageIndex";
            this.m_lstTechnicians.ImageList = this.m_imagesYesNo;
            this.m_lstTechnicians.Location = new System.Drawing.Point(3, 31);
            this.m_lstTechnicians.Name = "m_lstTechnicians";
            this.m_lstTechnicians.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_lstTechnicians.Size = new System.Drawing.Size(267, 275);
            this.m_lstTechnicians.TabIndex = 4;
            // 
            // m_imagesYesNo
            // 
            this.m_imagesYesNo.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesYesNo.ImageStream")));
            this.m_imagesYesNo.TransparentColor = System.Drawing.Color.Magenta;
            this.m_imagesYesNo.Images.SetKeyName(0, "no.bmp");
            this.m_imagesYesNo.Images.SetKeyName(1, "yes.bmp");
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(229, 312);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 8;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(148, 312);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 7;
            this.m_btnOk.Text = "&OK";
            // 
            // DashboardCustomizeView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(307, 338);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DashboardCustomizeView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DashboardCustomize";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVisibleCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTechnicians.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_lstTechnicians)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        internal DevExpress.XtraEditors.ImageListBoxControl m_lstTechnicians;
        private System.Windows.Forms.ImageList m_imagesUpDown;
        internal DevExpress.XtraEditors.SimpleButton m_btnDown;
        internal DevExpress.XtraEditors.SimpleButton m_btnUp;
        private System.Windows.Forms.ImageList m_imagesYesNo;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbTechnicians;
        internal DevExpress.XtraEditors.SpinEdit m_txtVisibleCount;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl m_lblShortcut;
    }
}