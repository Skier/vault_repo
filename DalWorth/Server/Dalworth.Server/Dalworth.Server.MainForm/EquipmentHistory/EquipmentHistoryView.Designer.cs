namespace Dalworth.Server.MainForm.EquipmentHistory
{
    partial class EquipmentHistoryView
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
            this.m_lblShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_gridEquipmentHistory = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewEquipmentHistory = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.df = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.m_colTransaction = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.m_linkTransaction = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.m_cmbStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.m_cmbLocationType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.m_cmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_cmbArea = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_txtTransaction = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.m_cmbHistoryDepth = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewEquipmentHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbLocationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbHistoryDepth.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblShortcut);
            this.panelControl1.Controls.Add(this.m_gridEquipmentHistory);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(873, 642);
            this.panelControl1.TabIndex = 0;
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut.Location = new System.Drawing.Point(28, 167);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 1;
            this.m_lblShortcut.Text = "&B shortcut";
            // 
            // m_gridEquipmentHistory
            // 
            this.m_gridEquipmentHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridEquipmentHistory.EmbeddedNavigator.Name = "";
            this.m_gridEquipmentHistory.Location = new System.Drawing.Point(0, 39);
            this.m_gridEquipmentHistory.MainView = this.m_gridViewEquipmentHistory;
            this.m_gridEquipmentHistory.Name = "m_gridEquipmentHistory";
            this.m_gridEquipmentHistory.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.m_cmbStatus,
            this.m_cmbType,
            this.m_cmbLocationType,
            this.m_cmbArea,
            this.m_linkTransaction,
            this.m_txtTransaction});
            this.m_gridEquipmentHistory.ShowOnlyPredefinedDetails = true;
            this.m_gridEquipmentHistory.Size = new System.Drawing.Size(873, 567);
            this.m_gridEquipmentHistory.TabIndex = 2;
            this.m_gridEquipmentHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewEquipmentHistory});
            // 
            // m_gridViewEquipmentHistory
            // 
            this.m_gridViewEquipmentHistory.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand2,
            this.gridBand1});
            this.m_gridViewEquipmentHistory.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.df,
            this.gridColumn14,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn5,
            this.m_colTransaction,
            this.gridColumn7,
            this.gridColumn8});
            this.m_gridViewEquipmentHistory.CustomizationFormBounds = new System.Drawing.Rectangle(609, 594, 217, 199);
            this.m_gridViewEquipmentHistory.GridControl = this.m_gridEquipmentHistory;
            this.m_gridViewEquipmentHistory.Name = "m_gridViewEquipmentHistory";
            this.m_gridViewEquipmentHistory.OptionsBehavior.AutoExpandAllGroups = true;
            this.m_gridViewEquipmentHistory.OptionsNavigation.UseTabKey = false;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "gridBand2";
            this.gridBand2.Columns.Add(this.gridColumn5);
            this.gridBand2.Columns.Add(this.df);
            this.gridBand2.Columns.Add(this.m_colTransaction);
            this.gridBand2.Columns.Add(this.gridColumn7);
            this.gridBand2.Columns.Add(this.gridColumn8);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.OptionsBand.AllowHotTrack = false;
            this.gridBand2.OptionsBand.AllowMove = false;
            this.gridBand2.OptionsBand.AllowPress = false;
            this.gridBand2.OptionsBand.ShowCaption = false;
            this.gridBand2.OptionsBand.ShowInCustomizationForm = false;
            this.gridBand2.Width = 352;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Date";
            this.gridColumn5.DisplayFormat.FormatString = "g";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn5.FieldName = "SequenceDate";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.Width = 130;
            // 
            // df
            // 
            this.df.AppearanceHeader.Options.UseTextOptions = true;
            this.df.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.df.Caption = "Equipment";
            this.df.FieldName = "SerialNumberAndEquipmentType";
            this.df.Name = "df";
            this.df.OptionsColumn.AllowEdit = false;
            this.df.Visible = true;
            this.df.Width = 114;
            // 
            // m_colTransaction
            // 
            this.m_colTransaction.AppearanceHeader.Options.UseTextOptions = true;
            this.m_colTransaction.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.m_colTransaction.Caption = "Transaction";
            this.m_colTransaction.ColumnEdit = this.m_linkTransaction;
            this.m_colTransaction.FieldName = "TransactionTypeText";
            this.m_colTransaction.Name = "m_colTransaction";
            this.m_colTransaction.Visible = true;
            this.m_colTransaction.Width = 108;
            // 
            // m_linkTransaction
            // 
            this.m_linkTransaction.AutoHeight = false;
            this.m_linkTransaction.Name = "m_linkTransaction";
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Dispatch";
            this.gridColumn7.FieldName = "DispatchName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.RowIndex = 1;
            this.gridColumn7.Visible = true;
            this.gridColumn7.Width = 130;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Technician";
            this.gridColumn8.FieldName = "TechnicianName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.RowIndex = 1;
            this.gridColumn8.Visible = true;
            this.gridColumn8.Width = 222;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Columns.Add(this.gridColumn14);
            this.gridBand1.Columns.Add(this.gridColumn1);
            this.gridBand1.Columns.Add(this.gridColumn4);
            this.gridBand1.Columns.Add(this.gridColumn2);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.OptionsBand.AllowHotTrack = false;
            this.gridBand1.OptionsBand.AllowMove = false;
            this.gridBand1.OptionsBand.AllowPress = false;
            this.gridBand1.OptionsBand.ShowCaption = false;
            this.gridBand1.OptionsBand.ShowInCustomizationForm = false;
            this.gridBand1.Width = 480;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.Caption = "Status";
            this.gridColumn14.ColumnEdit = this.m_cmbStatus;
            this.gridColumn14.FieldName = "StatusId";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.Width = 161;
            // 
            // m_cmbStatus
            // 
            this.m_cmbStatus.AutoHeight = false;
            this.m_cmbStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbStatus.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("In Service", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Retired", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Broken", 3, -1)});
            this.m_cmbStatus.Name = "m_cmbStatus";
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Location Type";
            this.gridColumn1.ColumnEdit = this.m_cmbLocationType;
            this.gridColumn1.FieldName = "LocationTypeId";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.Width = 137;
            // 
            // m_cmbLocationType
            // 
            this.m_cmbLocationType.AutoHeight = false;
            this.m_cmbLocationType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbLocationType.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Inventory Room", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Van", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Customer", 3, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Lost", 4, -1)});
            this.m_cmbLocationType.Name = "m_cmbLocationType";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Customer";
            this.gridColumn4.FieldName = "CustomerName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.Width = 182;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Location";
            this.gridColumn2.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn2.FieldName = "Location";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.RowIndex = 1;
            this.gridColumn2.Visible = true;
            this.gridColumn2.Width = 319;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // m_cmbType
            // 
            this.m_cmbType.AutoHeight = false;
            this.m_cmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbType.Name = "m_cmbType";
            // 
            // m_cmbArea
            // 
            this.m_cmbArea.AutoHeight = false;
            this.m_cmbArea.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbArea.Name = "m_cmbArea";
            // 
            // m_txtTransaction
            // 
            this.m_txtTransaction.AutoHeight = false;
            this.m_txtTransaction.Name = "m_txtTransaction";
            this.m_txtTransaction.ReadOnly = true;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.m_btnClose);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 606);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(873, 36);
            this.panelControl3.TabIndex = 13;
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(795, 10);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 0;
            this.m_btnClose.Text = "&Close";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.m_cmbHistoryDepth);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(873, 39);
            this.panelControl2.TabIndex = 0;
            // 
            // m_cmbHistoryDepth
            // 
            this.m_cmbHistoryDepth.EditValue = 31;
            this.m_cmbHistoryDepth.Location = new System.Drawing.Point(95, 10);
            this.m_cmbHistoryDepth.Name = "m_cmbHistoryDepth";
            this.m_cmbHistoryDepth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbHistoryDepth.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("2 weeks", 14, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("1 Month", 31, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("2 Months", 61, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("3 Months", 92, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("6 Months", 183, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("1 Year", 366, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("2 Years", 731, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("3 Years", 1096, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("5 Years", 1827, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("All", 0, -1)});
            this.m_cmbHistoryDepth.Size = new System.Drawing.Size(118, 20);
            this.m_cmbHistoryDepth.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "&History Depth";
            // 
            // EquipmentHistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(873, 642);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EquipmentHistoryView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EquipmentHistory";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewEquipmentHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbLocationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbHistoryDepth.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnClose;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        internal DevExpress.XtraGrid.GridControl m_gridEquipmentHistory;
        internal DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_cmbType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_cmbStatus;
        internal DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_cmbArea;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_cmbLocationType;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn14;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn df;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbHistoryDepth;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkTransaction;
        internal DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView m_gridViewEquipmentHistory;
        internal DevExpress.XtraEditors.Repository.RepositoryItemTextEdit m_txtTransaction;
        internal DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn m_colTransaction;
        private DevExpress.XtraEditors.LabelControl m_lblShortcut;
    }
}