namespace Dalworth.Server.MainForm.ReportEquipmentDetails
{
    partial class ReportEquipmentDetailsView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_pnlReportContent = new DevExpress.XtraEditors.PanelControl();
            this.m_gridShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_gridEquipmentDetails = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewEquipmentDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblLocationAddressRow2 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblLocationAddressRow1 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblLocation = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).BeginInit();
            this.m_pnlReportContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewEquipmentDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlReportContent
            // 
            this.m_pnlReportContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlReportContent.Controls.Add(this.m_gridShortcut);
            this.m_pnlReportContent.Controls.Add(this.m_gridEquipmentDetails);
            this.m_pnlReportContent.Controls.Add(this.panelControl1);
            this.m_pnlReportContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlReportContent.Location = new System.Drawing.Point(0, 0);
            this.m_pnlReportContent.Name = "m_pnlReportContent";
            this.m_pnlReportContent.Size = new System.Drawing.Size(455, 426);
            this.m_pnlReportContent.TabIndex = 6;
            // 
            // m_gridShortcut
            // 
            this.m_gridShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_gridShortcut.Location = new System.Drawing.Point(8, 139);
            this.m_gridShortcut.Name = "m_gridShortcut";
            this.m_gridShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_gridShortcut.TabIndex = 1;
            this.m_gridShortcut.Text = "&B Shortcut";
            // 
            // m_gridEquipmentDetails
            // 
            this.m_gridEquipmentDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridEquipmentDetails.EmbeddedNavigator.Name = "";
            this.m_gridEquipmentDetails.Location = new System.Drawing.Point(0, 65);
            this.m_gridEquipmentDetails.MainView = this.m_gridViewEquipmentDetails;
            this.m_gridEquipmentDetails.Name = "m_gridEquipmentDetails";
            this.m_gridEquipmentDetails.Size = new System.Drawing.Size(455, 361);
            this.m_gridEquipmentDetails.TabIndex = 2;
            this.m_gridEquipmentDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewEquipmentDetails});
            // 
            // m_gridViewEquipmentDetails
            // 
            this.m_gridViewEquipmentDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn5});
            this.m_gridViewEquipmentDetails.GridControl = this.m_gridEquipmentDetails;
            this.m_gridViewEquipmentDetails.GroupCount = 1;
            this.m_gridViewEquipmentDetails.GroupFormat = "[#image]{1} {2}";
            this.m_gridViewEquipmentDetails.Name = "m_gridViewEquipmentDetails";
            this.m_gridViewEquipmentDetails.OptionsBehavior.AutoExpandAllGroups = true;
            this.m_gridViewEquipmentDetails.OptionsCustomization.AllowFilter = false;
            this.m_gridViewEquipmentDetails.OptionsCustomization.AllowGroup = false;
            this.m_gridViewEquipmentDetails.OptionsNavigation.UseTabKey = false;
            this.m_gridViewEquipmentDetails.OptionsView.ColumnAutoWidth = false;
            this.m_gridViewEquipmentDetails.OptionsView.ShowGroupPanel = false;
            this.m_gridViewEquipmentDetails.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn3
            // 
            this.gridColumn3.FieldName = "EquipmentTypeName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "SerialNumber1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 93;
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "SerialNumber2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.FieldName = "SerialNumber3";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gridColumn5
            // 
            this.gridColumn5.FieldName = "SerialNumber4";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_btnPrint);
            this.panelControl1.Controls.Add(this.m_btnPreview);
            this.panelControl1.Controls.Add(this.m_lblLocationAddressRow2);
            this.panelControl1.Controls.Add(this.m_lblLocationAddressRow1);
            this.panelControl1.Controls.Add(this.m_lblLocation);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(455, 65);
            this.panelControl1.TabIndex = 0;
            // 
            // m_btnPrint
            // 
            this.m_btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPrint.Location = new System.Drawing.Point(377, 35);
            this.m_btnPrint.Name = "m_btnPrint";
            this.m_btnPrint.Size = new System.Drawing.Size(75, 23);
            this.m_btnPrint.TabIndex = 1;
            this.m_btnPrint.Text = "P&rint";
            // 
            // m_btnPreview
            // 
            this.m_btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPreview.Location = new System.Drawing.Point(377, 7);
            this.m_btnPreview.Name = "m_btnPreview";
            this.m_btnPreview.Size = new System.Drawing.Size(75, 23);
            this.m_btnPreview.TabIndex = 0;
            this.m_btnPreview.Text = "&Preview";
            // 
            // m_lblLocationAddressRow2
            // 
            this.m_lblLocationAddressRow2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblLocationAddressRow2.Appearance.Options.UseFont = true;
            this.m_lblLocationAddressRow2.Location = new System.Drawing.Point(63, 45);
            this.m_lblLocationAddressRow2.Name = "m_lblLocationAddressRow2";
            this.m_lblLocationAddressRow2.Size = new System.Drawing.Size(0, 13);
            this.m_lblLocationAddressRow2.TabIndex = 3;
            // 
            // m_lblLocationAddressRow1
            // 
            this.m_lblLocationAddressRow1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblLocationAddressRow1.Appearance.Options.UseFont = true;
            this.m_lblLocationAddressRow1.Location = new System.Drawing.Point(63, 26);
            this.m_lblLocationAddressRow1.Name = "m_lblLocationAddressRow1";
            this.m_lblLocationAddressRow1.Size = new System.Drawing.Size(0, 13);
            this.m_lblLocationAddressRow1.TabIndex = 2;
            // 
            // m_lblLocation
            // 
            this.m_lblLocation.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblLocation.Appearance.Options.UseFont = true;
            this.m_lblLocation.Location = new System.Drawing.Point(63, 7);
            this.m_lblLocation.Name = "m_lblLocation";
            this.m_lblLocation.Size = new System.Drawing.Size(149, 13);
            this.m_lblLocation.TabIndex = 1;
            this.m_lblLocation.Text = "Customer - Carcoran Carol";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Location";
            // 
            // ReportEquipmentDetailsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 426);
            this.Controls.Add(this.m_pnlReportContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReportEquipmentDetailsView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ReportEquipmentDetailsView";
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).EndInit();
            this.m_pnlReportContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewEquipmentDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.PanelControl m_pnlReportContent;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraGrid.GridControl m_gridEquipmentDetails;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewEquipmentDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        internal DevExpress.XtraEditors.LabelControl m_lblLocation;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        internal DevExpress.XtraEditors.SimpleButton m_btnPrint;
        internal DevExpress.XtraEditors.SimpleButton m_btnPreview;
        internal DevExpress.XtraEditors.LabelControl m_lblLocationAddressRow2;
        internal DevExpress.XtraEditors.LabelControl m_lblLocationAddressRow1;
        private DevExpress.XtraEditors.LabelControl m_gridShortcut;        
    }
}
