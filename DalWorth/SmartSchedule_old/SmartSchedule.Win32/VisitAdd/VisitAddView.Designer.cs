
namespace SmartSchedule.Win32.VisitAdd
{
    partial class VisitAddView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisitAddView));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_gridSortedFrames = new DevExpress.XtraGrid.GridControl();
            this.m_gridSortedFramesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_btnRerankSelection = new DevExpress.XtraEditors.SimpleButton();
            this.m_gridTimeFrames = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.m_date = new DevExpress.XtraPivotGrid.PivotGridField();
            this.m_text = new DevExpress.XtraPivotGrid.PivotGridField();
            this.m_secondaryRank = new DevExpress.XtraPivotGrid.PivotGridField();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtLocation = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtDuration = new DevExpress.XtraEditors.TextEdit();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_images = new System.Windows.Forms.ImageList(this.components);
            this.m_dashboardStorage = new DevExpress.XtraScheduler.SchedulerStorage(this.components);
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridSortedFrames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridSortedFramesView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTimeFrames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtDuration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboardStorage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_gridSortedFrames);
            this.panelControl1.Controls.Add(this.m_btnRerankSelection);
            this.panelControl1.Controls.Add(this.m_gridTimeFrames);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.m_txtLocation);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_txtDuration);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(635, 375);
            this.panelControl1.TabIndex = 0;
            // 
            // m_gridSortedFrames
            // 
            this.m_gridSortedFrames.Location = new System.Drawing.Point(469, 35);
            this.m_gridSortedFrames.MainView = this.m_gridSortedFramesView;
            this.m_gridSortedFrames.Name = "m_gridSortedFrames";
            this.m_gridSortedFrames.Size = new System.Drawing.Size(163, 308);
            this.m_gridSortedFrames.TabIndex = 14;
            this.m_gridSortedFrames.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridSortedFramesView,
            this.gridView2});
            // 
            // m_gridSortedFramesView
            // 
            this.m_gridSortedFramesView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.m_gridSortedFramesView.GridControl = this.m_gridSortedFrames;
            this.m_gridSortedFramesView.Name = "m_gridSortedFramesView";
            this.m_gridSortedFramesView.OptionsBehavior.Editable = false;
            this.m_gridSortedFramesView.OptionsCustomization.AllowFilter = false;
            this.m_gridSortedFramesView.OptionsCustomization.AllowGroup = false;
            this.m_gridSortedFramesView.OptionsCustomization.AllowSort = false;
            this.m_gridSortedFramesView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridSortedFramesView.OptionsView.ShowColumnHeaders = false;
            this.m_gridSortedFramesView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.DisplayFormat.FormatString = "d";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "Date";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 68;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.FieldName = "Text";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 74;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.m_gridSortedFrames;
            this.gridView2.Name = "gridView2";
            // 
            // m_btnRerankSelection
            // 
            this.m_btnRerankSelection.Enabled = false;
            this.m_btnRerankSelection.Location = new System.Drawing.Point(534, 6);
            this.m_btnRerankSelection.Name = "m_btnRerankSelection";
            this.m_btnRerankSelection.Size = new System.Drawing.Size(98, 23);
            this.m_btnRerankSelection.TabIndex = 13;
            this.m_btnRerankSelection.Text = "Rerank Selection";
            // 
            // m_gridTimeFrames
            // 
            this.m_gridTimeFrames.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_gridTimeFrames.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.m_date,
            this.m_text,
            this.m_secondaryRank});
            this.m_gridTimeFrames.Location = new System.Drawing.Point(3, 35);
            this.m_gridTimeFrames.Name = "m_gridTimeFrames";
            this.m_gridTimeFrames.OptionsCustomization.AllowDrag = false;
            this.m_gridTimeFrames.OptionsCustomization.AllowExpand = false;
            this.m_gridTimeFrames.OptionsCustomization.AllowFilter = false;
            this.m_gridTimeFrames.OptionsCustomization.AllowHideFields = DevExpress.XtraPivotGrid.AllowHideFieldsType.Always;
            this.m_gridTimeFrames.OptionsCustomization.AllowPrefilter = false;
            this.m_gridTimeFrames.OptionsCustomization.AllowSort = false;
            this.m_gridTimeFrames.OptionsCustomization.AllowSortBySummary = false;
            this.m_gridTimeFrames.OptionsLayout.Columns.AddNewColumns = false;
            this.m_gridTimeFrames.OptionsLayout.Columns.RemoveOldColumns = false;
            this.m_gridTimeFrames.OptionsMenu.EnableFieldValueMenu = false;
            this.m_gridTimeFrames.OptionsMenu.EnableHeaderAreaMenu = false;
            this.m_gridTimeFrames.OptionsMenu.EnableHeaderMenu = false;
            this.m_gridTimeFrames.OptionsView.ShowColumnGrandTotals = false;
            this.m_gridTimeFrames.OptionsView.ShowColumnHeaders = false;
            this.m_gridTimeFrames.OptionsView.ShowColumnTotals = false;
            this.m_gridTimeFrames.OptionsView.ShowDataHeaders = false;
            this.m_gridTimeFrames.OptionsView.ShowFilterHeaders = false;
            this.m_gridTimeFrames.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.m_gridTimeFrames.OptionsView.ShowRowGrandTotals = false;
            this.m_gridTimeFrames.OptionsView.ShowRowHeaders = false;
            this.m_gridTimeFrames.Size = new System.Drawing.Size(460, 308);
            this.m_gridTimeFrames.TabIndex = 12;
            // 
            // m_date
            // 
            this.m_date.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.m_date.AreaIndex = 0;
            this.m_date.FieldName = "Date";
            this.m_date.Name = "m_date";
            this.m_date.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.m_date.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.m_date.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.m_date.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.m_date.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.m_date.Options.ShowCustomTotals = false;
            this.m_date.Options.ShowGrandTotal = false;
            this.m_date.Options.ShowInCustomizationForm = false;
            this.m_date.Options.ShowTotals = false;
            this.m_date.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.m_date.ValueFormat.FormatString = "d";
            this.m_date.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.m_date.Width = 86;
            // 
            // m_text
            // 
            this.m_text.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.m_text.AreaIndex = 0;
            this.m_text.FieldName = "Text";
            this.m_text.Name = "m_text";
            this.m_text.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.m_text.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.m_text.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.m_text.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.m_text.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.m_text.Options.ShowCustomTotals = false;
            this.m_text.Options.ShowGrandTotal = false;
            this.m_text.Options.ShowInCustomizationForm = false;
            this.m_text.Options.ShowTotals = false;
            this.m_text.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            // 
            // m_secondaryRank
            // 
            this.m_secondaryRank.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.m_secondaryRank.AreaIndex = 0;
            this.m_secondaryRank.FieldName = "SecondaryRankText";
            this.m_secondaryRank.Name = "m_secondaryRank";
            this.m_secondaryRank.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.m_secondaryRank.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.m_secondaryRank.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.m_secondaryRank.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.m_secondaryRank.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.m_secondaryRank.Options.ShowCustomTotals = false;
            this.m_secondaryRank.Options.ShowGrandTotal = false;
            this.m_secondaryRank.Options.ShowInCustomizationForm = false;
            this.m_secondaryRank.Options.ShowTotals = false;
            this.m_secondaryRank.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(172, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Location";
            // 
            // m_txtLocation
            // 
            this.m_txtLocation.Location = new System.Drawing.Point(218, 9);
            this.m_txtLocation.Name = "m_txtLocation";
            this.m_txtLocation.Properties.Mask.EditMask = "\\d{1,2}/\\d{1,2}";
            this.m_txtLocation.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtLocation.Size = new System.Drawing.Size(73, 20);
            this.m_txtLocation.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Duration";
            // 
            // m_txtDuration
            // 
            this.m_txtDuration.Location = new System.Drawing.Point(75, 9);
            this.m_txtDuration.Name = "m_txtDuration";
            this.m_txtDuration.Properties.Mask.EditMask = "\\d{1,6}";
            this.m_txtDuration.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtDuration.Size = new System.Drawing.Size(73, 20);
            this.m_txtDuration.TabIndex = 0;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(557, 349);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "Cancel";
            // 
            // m_images
            // 
            this.m_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_images.ImageStream")));
            this.m_images.TransparentColor = System.Drawing.Color.Magenta;
            this.m_images.Images.SetKeyName(0, "no.bmp");
            this.m_images.Images.SetKeyName(1, "yes.bmp");
            // 
            // m_dashboardStorage
            // 
            this.m_dashboardStorage.Appointments.Mappings.End = "TimeEnd";
            this.m_dashboardStorage.Appointments.Mappings.ResourceId = "TechnicianId";
            this.m_dashboardStorage.Appointments.Mappings.Start = "TimeStart";
            this.m_dashboardStorage.Resources.Mappings.Caption = "Name";
            this.m_dashboardStorage.Resources.Mappings.Id = "Id";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // VisitAddView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(635, 375);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisitAddView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VisitAddView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridSortedFrames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridSortedFramesView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTimeFrames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtDuration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboardStorage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal DevExpress.XtraScheduler.SchedulerStorage m_dashboardStorage;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.TextEdit m_txtDuration;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ImageList m_images;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.TextEdit m_txtLocation;
        internal DevExpress.XtraPivotGrid.PivotGridControl m_gridTimeFrames;
        private DevExpress.XtraPivotGrid.PivotGridField m_date;
        private DevExpress.XtraPivotGrid.PivotGridField m_text;
        private DevExpress.XtraPivotGrid.PivotGridField m_secondaryRank;
        internal DevExpress.XtraEditors.SimpleButton m_btnRerankSelection;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        internal DevExpress.XtraGrid.GridControl m_gridSortedFrames;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridSortedFramesView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;

    }
}