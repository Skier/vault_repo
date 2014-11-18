
using SmartSchedule.Win32.Controls;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.DelayedVisitProcess
{
    partial class DelayedVisitProcessView
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
            this.m_lblBucketReason = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnProcessIgnoreExclusivity = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.m_ctlVisitInfo = new SmartSchedule.Win32.Controls.VisitInfo();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.m_gridWorkHoursExtension = new Grid();
            this.m_gridWorkHoursExtensionView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_btnProcessWorkingHoursExtension = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnShowCurrentTechnician = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.m_gridTimeFrame = new Grid();
            this.m_gridTimeFrameView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_btnProcessTimeFrameChange = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.m_cmbExclusiveTechnician = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnProcessTempAssignment = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridWorkHoursExtension)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridWorkHoursExtensionView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTimeFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTimeFrameView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbExclusiveTechnician.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.CausesValidation = false;
            this.panelControl1.Controls.Add(this.m_lblBucketReason);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_btnProcessIgnoreExclusivity);
            this.panelControl1.Controls.Add(this.m_btnSave);
            this.panelControl1.Controls.Add(this.m_ctlVisitInfo);
            this.panelControl1.Controls.Add(this.groupControl4);
            this.panelControl1.Controls.Add(this.groupControl3);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(662, 739);
            this.panelControl1.TabIndex = 0;
            // 
            // m_lblBucketReason
            // 
            this.m_lblBucketReason.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblBucketReason.Appearance.Options.UseFont = true;
            this.m_lblBucketReason.Appearance.Options.UseTextOptions = true;
            this.m_lblBucketReason.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.m_lblBucketReason.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.m_lblBucketReason.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblBucketReason.Location = new System.Drawing.Point(301, 3);
            this.m_lblBucketReason.Name = "m_lblBucketReason";
            this.m_lblBucketReason.Size = new System.Drawing.Size(355, 29);
            this.m_lblBucketReason.TabIndex = 10;
            this.m_lblBucketReason.Text = "Neither of allowed technicians can peform all the required services";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(301, 46);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "1. Ignore Exclusivity";
            // 
            // m_btnProcessIgnoreExclusivity
            // 
            this.m_btnProcessIgnoreExclusivity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnProcessIgnoreExclusivity.Location = new System.Drawing.Point(581, 42);
            this.m_btnProcessIgnoreExclusivity.Name = "m_btnProcessIgnoreExclusivity";
            this.m_btnProcessIgnoreExclusivity.Size = new System.Drawing.Size(75, 23);
            this.m_btnProcessIgnoreExclusivity.TabIndex = 0;
            this.m_btnProcessIgnoreExclusivity.Text = "Process";
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnSave.Location = new System.Drawing.Point(500, 713);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(75, 23);
            this.m_btnSave.TabIndex = 6;
            this.m_btnSave.Text = "Save";
            // 
            // m_ctlVisitInfo
            // 
            this.m_ctlVisitInfo.Caption = "Bucket Visit";
            this.m_ctlVisitInfo.IsShowDurationAmount = true;
            this.m_ctlVisitInfo.Location = new System.Drawing.Point(3, 3);
            this.m_ctlVisitInfo.MinimumSize = new System.Drawing.Size(290, 168);
            this.m_ctlVisitInfo.Name = "m_ctlVisitInfo";
            this.m_ctlVisitInfo.Size = new System.Drawing.Size(290, 168);
            this.m_ctlVisitInfo.TabIndex = 5;
            this.m_ctlVisitInfo.Visit = null;
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.m_gridWorkHoursExtension);
            this.groupControl4.Controls.Add(this.m_btnProcessWorkingHoursExtension);
            this.groupControl4.Controls.Add(this.m_btnShowCurrentTechnician);
            this.groupControl4.Location = new System.Drawing.Point(3, 177);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(653, 276);
            this.groupControl4.TabIndex = 2;
            this.groupControl4.Text = "3. Extend Working Hours";
            // 
            // m_gridWorkHoursExtension
            // 
            this.m_gridWorkHoursExtension.Location = new System.Drawing.Point(5, 23);
            this.m_gridWorkHoursExtension.MainView = this.m_gridWorkHoursExtensionView;
            this.m_gridWorkHoursExtension.Name = "m_gridWorkHoursExtension";
            this.m_gridWorkHoursExtension.Size = new System.Drawing.Size(643, 217);
            this.m_gridWorkHoursExtension.TabIndex = 0;
            this.m_gridWorkHoursExtension.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridWorkHoursExtensionView});
            // 
            // m_gridWorkHoursExtensionView
            // 
            this.m_gridWorkHoursExtensionView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn12,
            this.gridColumn11,
            this.gridColumn9});
            this.m_gridWorkHoursExtensionView.GridControl = this.m_gridWorkHoursExtension;
            this.m_gridWorkHoursExtensionView.Name = "m_gridWorkHoursExtensionView";
            this.m_gridWorkHoursExtensionView.OptionsBehavior.Editable = false;
            this.m_gridWorkHoursExtensionView.OptionsCustomization.AllowFilter = false;
            this.m_gridWorkHoursExtensionView.OptionsCustomization.AllowGroup = false;
            this.m_gridWorkHoursExtensionView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridWorkHoursExtensionView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridWorkHoursExtensionView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridWorkHoursExtensionView.OptionsView.ShowDetailButtons = false;
            this.m_gridWorkHoursExtensionView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.FieldName = "TechnicianName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 110;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Old Work Hours";
            this.gridColumn7.FieldName = "OldWorkingHoursText";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 101;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "New Work Hours";
            this.gridColumn8.FieldName = "NewWorkingHoursText";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 98;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Extension";
            this.gridColumn10.FieldName = "DurationDeltaText";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            this.gridColumn10.Width = 72;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Cost";
            this.gridColumn12.DisplayFormat.FormatString = "0.00";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn12.FieldName = "CostChange";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 4;
            this.gridColumn12.Width = 66;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Sec. Area";
            this.gridColumn11.FieldName = "SecondaryAreaText";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 5;
            this.gridColumn11.Width = 57;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Visit";
            this.gridColumn9.FieldName = "VisitShortInfoText";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            this.gridColumn9.Width = 118;
            // 
            // m_btnProcessWorkingHoursExtension
            // 
            this.m_btnProcessWorkingHoursExtension.Location = new System.Drawing.Point(573, 246);
            this.m_btnProcessWorkingHoursExtension.Name = "m_btnProcessWorkingHoursExtension";
            this.m_btnProcessWorkingHoursExtension.Size = new System.Drawing.Size(75, 23);
            this.m_btnProcessWorkingHoursExtension.TabIndex = 2;
            this.m_btnProcessWorkingHoursExtension.Text = "Process";
            // 
            // m_btnShowCurrentTechnician
            // 
            this.m_btnShowCurrentTechnician.Location = new System.Drawing.Point(413, 246);
            this.m_btnShowCurrentTechnician.Name = "m_btnShowCurrentTechnician";
            this.m_btnShowCurrentTechnician.Size = new System.Drawing.Size(154, 23);
            this.m_btnShowCurrentTechnician.TabIndex = 1;
            this.m_btnShowCurrentTechnician.Text = "Show Single Technician Only";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.m_gridTimeFrame);
            this.groupControl3.Controls.Add(this.m_btnProcessTimeFrameChange);
            this.groupControl3.Location = new System.Drawing.Point(3, 459);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(653, 251);
            this.groupControl3.TabIndex = 3;
            this.groupControl3.Text = "4. Time Frame change";
            // 
            // m_gridTimeFrame
            // 
            this.m_gridTimeFrame.Location = new System.Drawing.Point(5, 23);
            this.m_gridTimeFrame.MainView = this.m_gridTimeFrameView;
            this.m_gridTimeFrame.Name = "m_gridTimeFrame";
            this.m_gridTimeFrame.Size = new System.Drawing.Size(643, 193);
            this.m_gridTimeFrame.TabIndex = 0;
            this.m_gridTimeFrame.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridTimeFrameView});
            // 
            // m_gridTimeFrameView
            // 
            this.m_gridTimeFrameView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.m_gridTimeFrameView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.m_gridTimeFrameView.ColumnPanelRowHeight = 0;
            this.m_gridTimeFrameView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.m_gridTimeFrameView.GridControl = this.m_gridTimeFrame;
            this.m_gridTimeFrameView.Name = "m_gridTimeFrameView";
            this.m_gridTimeFrameView.OptionsBehavior.Editable = false;
            this.m_gridTimeFrameView.OptionsCustomization.AllowFilter = false;
            this.m_gridTimeFrameView.OptionsCustomization.AllowGroup = false;
            this.m_gridTimeFrameView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridTimeFrameView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridTimeFrameView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridTimeFrameView.OptionsView.ShowDetailButtons = false;
            this.m_gridTimeFrameView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "New Frame";
            this.gridColumn1.FieldName = "NewTimeFrameText";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 155;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Cost";
            this.gridColumn2.DisplayFormat.FormatString = "0.00";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "CostChange";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 197;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Secondary Area";
            this.gridColumn3.FieldName = "SecondaryAreaText";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 270;
            // 
            // m_btnProcessTimeFrameChange
            // 
            this.m_btnProcessTimeFrameChange.Location = new System.Drawing.Point(573, 222);
            this.m_btnProcessTimeFrameChange.Name = "m_btnProcessTimeFrameChange";
            this.m_btnProcessTimeFrameChange.Size = new System.Drawing.Size(75, 23);
            this.m_btnProcessTimeFrameChange.TabIndex = 2;
            this.m_btnProcessTimeFrameChange.Text = "Process";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.m_cmbExclusiveTechnician);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.m_btnProcessTempAssignment);
            this.groupControl2.Location = new System.Drawing.Point(301, 71);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(355, 100);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "2. Ignore Zip Code";
            // 
            // m_cmbExclusiveTechnician
            // 
            this.m_cmbExclusiveTechnician.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbExclusiveTechnician.Location = new System.Drawing.Point(168, 28);
            this.m_cmbExclusiveTechnician.Name = "m_cmbExclusiveTechnician";
            this.m_cmbExclusiveTechnician.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbExclusiveTechnician.Size = new System.Drawing.Size(182, 20);
            this.m_cmbExclusiveTechnician.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Location = new System.Drawing.Point(10, 31);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(152, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Temporary Exclusive Technician";
            // 
            // m_btnProcessTempAssignment
            // 
            this.m_btnProcessTempAssignment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnProcessTempAssignment.Location = new System.Drawing.Point(275, 72);
            this.m_btnProcessTempAssignment.Name = "m_btnProcessTempAssignment";
            this.m_btnProcessTempAssignment.Size = new System.Drawing.Size(75, 23);
            this.m_btnProcessTempAssignment.TabIndex = 2;
            this.m_btnProcessTempAssignment.Text = "Process";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(581, 713);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "Cancel";
            // 
            // DelayedVisitProcessView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(662, 739);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DelayedVisitProcessView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DelayedVisitProcess";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridWorkHoursExtension)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridWorkHoursExtensionView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTimeFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTimeFrameView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbExclusiveTechnician.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbExclusiveTechnician;
        internal DevExpress.XtraEditors.SimpleButton m_btnProcessTempAssignment;
        internal DevExpress.XtraEditors.SimpleButton m_btnProcessIgnoreExclusivity;
        internal DevExpress.XtraEditors.SimpleButton m_btnProcessTimeFrameChange;
        internal Grid m_gridTimeFrame;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridTimeFrameView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        internal DevExpress.XtraEditors.SimpleButton m_btnProcessWorkingHoursExtension;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        internal Grid m_gridWorkHoursExtension;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridWorkHoursExtensionView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        internal DevExpress.XtraEditors.SimpleButton m_btnShowCurrentTechnician;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        internal SmartSchedule.Win32.Controls.VisitInfo m_ctlVisitInfo;
        internal DevExpress.XtraEditors.SimpleButton m_btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.LabelControl m_lblBucketReason;

    }
}