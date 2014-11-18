namespace Dalworth.Server.MainForm.Leads
{
    partial class LeadsView
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtLeadId = new DevExpress.XtraEditors.TextEdit();
            this.m_cmbStatus = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtPhoneNo = new DevExpress.XtraEditors.TextEdit();
            this.m_dateRange = new Dalworth.Server.MainForm.Components.DateRange();
            this.m_btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.m_txtAddress = new DevExpress.XtraEditors.TextEdit();
            this.m_txtCustomer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.m_gridLeads = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewLeads = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_memoDispatchNotes = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.AdvertisingSourceAcronym = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtLeadId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPhoneNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridLeads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewLeads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_memoDispatchNotes)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.m_txtLeadId);
            this.panelControl1.Controls.Add(this.m_cmbStatus);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_txtPhoneNo);
            this.panelControl1.Controls.Add(this.m_dateRange);
            this.panelControl1.Controls.Add(this.m_btnRefresh);
            this.panelControl1.Controls.Add(this.m_btnClear);
            this.panelControl1.Controls.Add(this.m_txtAddress);
            this.panelControl1.Controls.Add(this.m_txtCustomer);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(942, 83);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(262, 58);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "&Lead ID";
            // 
            // m_txtLeadId
            // 
            this.m_txtLeadId.Location = new System.Drawing.Point(331, 55);
            this.m_txtLeadId.Name = "m_txtLeadId";
            this.m_txtLeadId.Size = new System.Drawing.Size(77, 20);
            this.m_txtLeadId.TabIndex = 11;
            // 
            // m_cmbStatus
            // 
            this.m_cmbStatus.EditValue = 0;
            this.m_cmbStatus.Location = new System.Drawing.Point(331, 31);
            this.m_cmbStatus.Name = "m_cmbStatus";
            this.m_cmbStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("New and Pending", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Converted", 3, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cancelled", 4, -1)});
            this.m_cmbStatus.Size = new System.Drawing.Size(156, 20);
            this.m_cmbStatus.TabIndex = 9;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(262, 32);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(31, 13);
            this.labelControl9.TabIndex = 8;
            this.labelControl9.Text = "&Status";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(262, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Created &Date";
            // 
            // m_txtPhoneNo
            // 
            this.m_txtPhoneNo.Location = new System.Drawing.Point(55, 56);
            this.m_txtPhoneNo.Name = "m_txtPhoneNo";
            this.m_txtPhoneNo.Properties.Mask.EditMask = "\\(\\d\\d\\d\\) \\d\\d\\d-\\d\\d\\d\\d";
            this.m_txtPhoneNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtPhoneNo.Properties.Mask.SaveLiteral = false;
            this.m_txtPhoneNo.Size = new System.Drawing.Size(147, 20);
            this.m_txtPhoneNo.TabIndex = 5;
            // 
            // m_dateRange
            // 
            dateRange1.EndDate = null;
            dateRange1.StartDate = null;
            this.m_dateRange.EditValue = dateRange1;
            this.m_dateRange.Location = new System.Drawing.Point(331, 5);
            this.m_dateRange.Name = "m_dateRange";
            this.m_dateRange.Size = new System.Drawing.Size(323, 20);
            this.m_dateRange.TabIndex = 7;
            // 
            // m_btnRefresh
            // 
            this.m_btnRefresh.Location = new System.Drawing.Point(680, 34);
            this.m_btnRefresh.Name = "m_btnRefresh";
            this.m_btnRefresh.Size = new System.Drawing.Size(83, 23);
            this.m_btnRefresh.TabIndex = 13;
            this.m_btnRefresh.Text = "&Refresh";
            // 
            // m_btnClear
            // 
            this.m_btnClear.Location = new System.Drawing.Point(680, 5);
            this.m_btnClear.Name = "m_btnClear";
            this.m_btnClear.Size = new System.Drawing.Size(83, 23);
            this.m_btnClear.TabIndex = 12;
            this.m_btnClear.Text = "&Clear";
            // 
            // m_txtAddress
            // 
            this.m_txtAddress.Location = new System.Drawing.Point(55, 29);
            this.m_txtAddress.Name = "m_txtAddress";
            this.m_txtAddress.Size = new System.Drawing.Size(184, 20);
            this.m_txtAddress.TabIndex = 3;
            // 
            // m_txtCustomer
            // 
            this.m_txtCustomer.Location = new System.Drawing.Point(55, 3);
            this.m_txtCustomer.Name = "m_txtCustomer";
            this.m_txtCustomer.Size = new System.Drawing.Size(184, 20);
            this.m_txtCustomer.TabIndex = 1;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(3, 32);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(39, 13);
            this.labelControl7.TabIndex = 2;
            this.labelControl7.Text = "&Address";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(3, 6);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(46, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "C&ustomer";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(3, 58);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(46, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "&P&hone No";
            // 
            // m_gridLeads
            // 
            this.m_gridLeads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridLeads.EmbeddedNavigator.Name = "";
            this.m_gridLeads.Location = new System.Drawing.Point(0, 82);
            this.m_gridLeads.MainView = this.m_gridViewLeads;
            this.m_gridLeads.Name = "m_gridLeads";
            this.m_gridLeads.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.m_memoDispatchNotes});
            this.m_gridLeads.Size = new System.Drawing.Size(942, 461);
            this.m_gridLeads.TabIndex = 0;
            this.m_gridLeads.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewLeads});
            // 
            // m_gridViewLeads
            // 
            this.m_gridViewLeads.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn10,
            this.gridColumn7,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn12,
            this.gridColumn11,
            this.AdvertisingSourceAcronym,
            this.gridColumn5,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn13,
            this.gridColumn14});
            this.m_gridViewLeads.GridControl = this.m_gridLeads;
            this.m_gridViewLeads.Name = "m_gridViewLeads";
            this.m_gridViewLeads.OptionsBehavior.FocusLeaveOnTab = true;
            this.m_gridViewLeads.OptionsCustomization.AllowFilter = false;
            this.m_gridViewLeads.OptionsCustomization.AllowGroup = false;
            this.m_gridViewLeads.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridViewLeads.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.m_gridViewLeads.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Id";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 20;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Business Partner";
            this.gridColumn10.FieldName = "BusinessPartnerName";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            this.gridColumn10.Width = 92;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Project Type";
            this.gridColumn7.FieldName = "ProjectTypeStr";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Customer";
            this.gridColumn2.FieldName = "FullName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 93;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Address";
            this.gridColumn3.FieldName = "Address";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 148;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Phone";
            this.gridColumn4.FieldName = "Phones";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 66;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Preferred Date";
            this.gridColumn6.DisplayFormat.FormatString = "d";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn6.FieldName = "PreferredServiceDate";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 69;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Created";
            this.gridColumn12.DisplayFormat.FormatString = "g";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn12.FieldName = "DateCreated";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 7;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Dispatch Notes";
            this.gridColumn11.ColumnEdit = this.m_memoDispatchNotes;
            this.gridColumn11.FieldName = "DispatchNotes";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 8;
            this.gridColumn11.Width = 47;
            // 
            // m_memoDispatchNotes
            // 
            this.m_memoDispatchNotes.AutoHeight = false;
            this.m_memoDispatchNotes.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_memoDispatchNotes.MaxLength = 500;
            this.m_memoDispatchNotes.Name = "m_memoDispatchNotes";
            // 
            // AdvertisingSourceAcronym
            // 
            this.AdvertisingSourceAcronym.Caption = "AdSource";
            this.AdvertisingSourceAcronym.FieldName = "AdvertisingSourceAcronym";
            this.AdvertisingSourceAcronym.Name = "AdvertisingSourceAcronym";
            this.AdvertisingSourceAcronym.OptionsColumn.AllowEdit = false;
            this.AdvertisingSourceAcronym.Visible = true;
            this.AdvertisingSourceAcronym.VisibleIndex = 9;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Status";
            this.gridColumn5.FieldName = "StatusStr";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 10;
            this.gridColumn5.Width = 62;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Last Updated";
            this.gridColumn8.FieldName = "LastUpdatedByDate";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 11;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Last Updated By";
            this.gridColumn9.FieldName = "LastUpdatedByEmployee";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 12;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "First Updated";
            this.gridColumn13.FieldName = "FirstUpdatedByDate";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 13;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "First Updated By";
            this.gridColumn14.FieldName = "FirstUpdatedByEmployee";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 14;
            // 
            // LeadsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_gridLeads);
            this.Controls.Add(this.panelControl1);
            this.Name = "LeadsView";
            this.Size = new System.Drawing.Size(942, 543);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtLeadId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPhoneNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridLeads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewLeads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_memoDispatchNotes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.TextEdit m_txtPhoneNo;
        internal Dalworth.Server.MainForm.Components.DateRange m_dateRange;
        internal DevExpress.XtraEditors.SimpleButton m_btnRefresh;
        internal DevExpress.XtraEditors.SimpleButton m_btnClear;
        internal DevExpress.XtraEditors.TextEdit m_txtAddress;
        internal DevExpress.XtraEditors.TextEdit m_txtCustomer;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbStatus;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        internal DevExpress.XtraGrid.GridControl m_gridLeads;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewLeads;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.TextEdit m_txtLeadId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        internal DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit m_memoDispatchNotes;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn AdvertisingSourceAcronym;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
    }
}
