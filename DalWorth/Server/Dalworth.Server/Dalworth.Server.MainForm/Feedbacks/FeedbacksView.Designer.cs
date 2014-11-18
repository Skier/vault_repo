namespace Dalworth.Server.MainForm.Feedbacks
{
    partial class FeedbacksView
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
            Dalworth.Server.Domain.DateRange dateRange1 = new Dalworth.Server.Domain.DateRange();
            Dalworth.Server.Domain.DateRange dateRange2 = new Dalworth.Server.Domain.DateRange();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.m_dateCallbackRange = new Dalworth.Server.MainForm.Components.DateRange();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbStatus = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_dateRange = new Dalworth.Server.MainForm.Components.DateRange();
            this.m_btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.m_txtCustomer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.m_gridFeedbacks = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewFeedbacks = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridFeedbacks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewFeedbacks)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.m_dateCallbackRange);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.m_cmbStatus);
            this.panelControl2.Controls.Add(this.labelControl9);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.m_dateRange);
            this.panelControl2.Controls.Add(this.m_btnClear);
            this.panelControl2.Controls.Add(this.m_txtCustomer);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(879, 103);
            this.panelControl2.TabIndex = 1;
            // 
            // m_dateCallbackRange
            // 
            dateRange1.EndDate = null;
            dateRange1.StartDate = null;
            this.m_dateCallbackRange.EditValue = dateRange1;
            this.m_dateCallbackRange.Location = new System.Drawing.Point(331, 31);
            this.m_dateCallbackRange.Name = "m_dateCallbackRange";
            this.m_dateCallbackRange.Size = new System.Drawing.Size(323, 20);
            this.m_dateCallbackRange.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(255, 34);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Call&back Dates";
            // 
            // m_cmbStatus
            // 
            this.m_cmbStatus.EditValue = 0;
            this.m_cmbStatus.Location = new System.Drawing.Point(55, 29);
            this.m_cmbStatus.Name = "m_cmbStatus";
            this.m_cmbStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("New", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Processed", 3, -1)});
            this.m_cmbStatus.Size = new System.Drawing.Size(156, 20);
            this.m_cmbStatus.TabIndex = 2;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(3, 34);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(31, 13);
            this.labelControl9.TabIndex = 8;
            this.labelControl9.Text = "&Status";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(255, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Created &Date";
            // 
            // m_dateRange
            // 
            dateRange2.EndDate = null;
            dateRange2.StartDate = null;
            this.m_dateRange.EditValue = dateRange2;
            this.m_dateRange.Location = new System.Drawing.Point(331, 5);
            this.m_dateRange.Name = "m_dateRange";
            this.m_dateRange.Size = new System.Drawing.Size(323, 20);
            this.m_dateRange.TabIndex = 3;
            // 
            // m_btnClear
            // 
            this.m_btnClear.Location = new System.Drawing.Point(680, 5);
            this.m_btnClear.Name = "m_btnClear";
            this.m_btnClear.Size = new System.Drawing.Size(83, 23);
            this.m_btnClear.TabIndex = 6;
            this.m_btnClear.Text = "&Clear";
            // 
            // m_txtCustomer
            // 
            this.m_txtCustomer.Location = new System.Drawing.Point(55, 3);
            this.m_txtCustomer.Name = "m_txtCustomer";
            this.m_txtCustomer.Size = new System.Drawing.Size(184, 20);
            this.m_txtCustomer.TabIndex = 1;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(3, 6);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(46, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "C&ustomer";
            // 
            // m_gridFeedbacks
            // 
            this.m_gridFeedbacks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridFeedbacks.EmbeddedNavigator.Name = "";
            this.m_gridFeedbacks.Location = new System.Drawing.Point(3, 98);
            this.m_gridFeedbacks.MainView = this.m_gridViewFeedbacks;
            this.m_gridFeedbacks.Name = "m_gridFeedbacks";
            this.m_gridFeedbacks.Size = new System.Drawing.Size(876, 438);
            this.m_gridFeedbacks.TabIndex = 5;
            this.m_gridFeedbacks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewFeedbacks});
            // 
            // m_gridViewFeedbacks
            // 
            this.m_gridViewFeedbacks.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn11,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.m_gridViewFeedbacks.GridControl = this.m_gridFeedbacks;
            this.m_gridViewFeedbacks.Name = "m_gridViewFeedbacks";
            this.m_gridViewFeedbacks.OptionsBehavior.FocusLeaveOnTab = true;
            this.m_gridViewFeedbacks.OptionsCustomization.AllowFilter = false;
            this.m_gridViewFeedbacks.OptionsCustomization.AllowGroup = false;
            this.m_gridViewFeedbacks.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridViewFeedbacks.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.m_gridViewFeedbacks.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Id";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 36;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Customer";
            this.gridColumn2.FieldName = "CustomerName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 78;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Phone";
            this.gridColumn11.FieldName = "Phone1";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            this.gridColumn11.Width = 56;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "ProjectType";
            this.gridColumn3.FieldName = "ProjectType";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 82;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Created";
            this.gridColumn4.FieldName = "DatePosted";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 99;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Rating";
            this.gridColumn5.FieldName = "Rate";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 64;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Note";
            this.gridColumn6.FieldName = "CustomerNote";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 10;
            this.gridColumn6.Width = 153;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Review Date";
            this.gridColumn7.FieldName = "DateReviewed";
            this.gridColumn7.MinWidth = 10;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 47;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Reviewed By";
            this.gridColumn8.FieldName = "ReviewedByEmployeeName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 76;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Is Published";
            this.gridColumn9.FieldName = "IsPublished";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            this.gridColumn9.Width = 78;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Callback Date";
            this.gridColumn10.FieldName = "CallbackDate";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 6;
            this.gridColumn10.Width = 86;
            // 
            // FeedbacksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_gridFeedbacks);
            this.Controls.Add(this.panelControl2);
            this.Name = "FeedbacksView";
            this.Size = new System.Drawing.Size(879, 536);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridFeedbacks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewFeedbacks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbStatus;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal Dalworth.Server.MainForm.Components.DateRange m_dateRange;
        internal DevExpress.XtraEditors.SimpleButton m_btnClear;
        internal DevExpress.XtraEditors.TextEdit m_txtCustomer;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        internal DevExpress.XtraGrid.GridControl m_gridFeedbacks;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal Dalworth.Server.MainForm.Components.DateRange m_dateCallbackRange;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewFeedbacks;

    }
}
