namespace Dalworth.Server.MainForm.ReportEquipmentSummary
{
    partial class ReportEquipmentSummaryView
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
            this.m_gridEquipmentSummary = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewEquipmentSummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_colLinkLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).BeginInit();
            this.m_pnlReportContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewEquipmentSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pnlReportContent
            // 
            this.m_pnlReportContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlReportContent.Controls.Add(this.m_gridShortcut);
            this.m_pnlReportContent.Controls.Add(this.m_gridEquipmentSummary);
            this.m_pnlReportContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlReportContent.Location = new System.Drawing.Point(0, 0);
            this.m_pnlReportContent.Name = "m_pnlReportContent";
            this.m_pnlReportContent.Size = new System.Drawing.Size(591, 437);
            this.m_pnlReportContent.TabIndex = 6;
            // 
            // m_gridShortcut
            // 
            this.m_gridShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_gridShortcut.Location = new System.Drawing.Point(3, 110);
            this.m_gridShortcut.Name = "m_gridShortcut";
            this.m_gridShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_gridShortcut.TabIndex = 2;
            this.m_gridShortcut.Text = "&B table schortcut";
            // 
            // m_gridEquipmentSummary
            // 
            this.m_gridEquipmentSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridEquipmentSummary.EmbeddedNavigator.Name = "";
            this.m_gridEquipmentSummary.Location = new System.Drawing.Point(0, 0);
            this.m_gridEquipmentSummary.MainView = this.m_gridViewEquipmentSummary;
            this.m_gridEquipmentSummary.Name = "m_gridEquipmentSummary";
            this.m_gridEquipmentSummary.Size = new System.Drawing.Size(591, 437);
            this.m_gridEquipmentSummary.TabIndex = 3;
            this.m_gridEquipmentSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewEquipmentSummary});
            // 
            // m_gridViewEquipmentSummary
            // 
            this.m_gridViewEquipmentSummary.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_colLinkLocation,
            this.gridColumn2,
            this.gridColumn3});
            this.m_gridViewEquipmentSummary.GridControl = this.m_gridEquipmentSummary;
            this.m_gridViewEquipmentSummary.GroupCount = 2;
            this.m_gridViewEquipmentSummary.GroupFormat = "[#image]{1} {2}";
            this.m_gridViewEquipmentSummary.Name = "m_gridViewEquipmentSummary";
            this.m_gridViewEquipmentSummary.OptionsBehavior.AutoExpandAllGroups = true;
            this.m_gridViewEquipmentSummary.OptionsCustomization.AllowColumnMoving = false;
            this.m_gridViewEquipmentSummary.OptionsCustomization.AllowFilter = false;
            this.m_gridViewEquipmentSummary.OptionsCustomization.AllowGroup = false;
            this.m_gridViewEquipmentSummary.OptionsCustomization.AllowSort = false;
            this.m_gridViewEquipmentSummary.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewEquipmentSummary.OptionsNavigation.UseTabKey = false;
            this.m_gridViewEquipmentSummary.OptionsView.ColumnAutoWidth = false;
            this.m_gridViewEquipmentSummary.OptionsView.ShowGroupPanel = false;
            this.m_gridViewEquipmentSummary.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn3, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // m_colLinkLocation
            // 
            this.m_colLinkLocation.Caption = "Location";
            this.m_colLinkLocation.FieldName = "LocationText";
            this.m_colLinkLocation.Name = "m_colLinkLocation";
            this.m_colLinkLocation.OptionsColumn.AllowEdit = false;
            this.m_colLinkLocation.Visible = true;
            this.m_colLinkLocation.VisibleIndex = 0;
            this.m_colLinkLocation.Width = 246;
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "LocationTypeText";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Area";
            this.gridColumn3.FieldName = "AreaText";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // ReportEquipmentSummaryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_pnlReportContent);
            this.Name = "ReportEquipmentSummaryView";
            this.Size = new System.Drawing.Size(591, 437);
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).EndInit();
            this.m_pnlReportContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewEquipmentSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.PanelControl m_pnlReportContent;
        internal DevExpress.XtraGrid.GridControl m_gridEquipmentSummary;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewEquipmentSummary;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.LabelControl m_gridShortcut;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colLinkLocation;        
    }
}
