namespace Dalworth.Server.MainForm.VisitMerge
{
    partial class VisitMergeView
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_lblExplanation = new DevExpress.XtraEditors.LabelControl();
            this.m_gridMergedVisits = new DevExpress.XtraGrid.GridControl();
            this.m_gridMergedVisitsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_chkVisitSelector = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colServiceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colPrefferedTimeFrame = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colTechnician = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMergedVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMergedVisitsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkVisitSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_lblShortcut);
            this.panelControl1.Controls.Add(this.m_lblExplanation);
            this.panelControl1.Controls.Add(this.m_gridMergedVisits);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(582, 300);
            this.panelControl1.TabIndex = 0;
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut.Location = new System.Drawing.Point(36, 100);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 1;
            this.m_lblShortcut.Text = "&B shortcut";
            // 
            // m_lblExplanation
            // 
            this.m_lblExplanation.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblExplanation.Appearance.Options.UseFont = true;
            this.m_lblExplanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblExplanation.Location = new System.Drawing.Point(6, 5);
            this.m_lblExplanation.Name = "m_lblExplanation";
            this.m_lblExplanation.Size = new System.Drawing.Size(573, 29);
            this.m_lblExplanation.TabIndex = 0;
            this.m_lblExplanation.Text = "There are ready pending visits for {0} that can be performed this day. Please sel" +
                "ect visits \r\nto be merged with current visit and press OK. If you don\'t want to " +
                "merge visits press Cancel.";
            // 
            // m_gridMergedVisits
            // 
            this.m_gridMergedVisits.EmbeddedNavigator.Name = "";
            this.m_gridMergedVisits.Location = new System.Drawing.Point(2, 40);
            this.m_gridMergedVisits.MainView = this.m_gridMergedVisitsView;
            this.m_gridMergedVisits.Name = "m_gridMergedVisits";
            this.m_gridMergedVisits.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.m_chkVisitSelector});
            this.m_gridMergedVisits.ShowOnlyPredefinedDetails = true;
            this.m_gridMergedVisits.Size = new System.Drawing.Size(577, 227);
            this.m_gridMergedVisits.TabIndex = 2;
            this.m_gridMergedVisits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridMergedVisitsView});
            // 
            // m_gridMergedVisitsView
            // 
            this.m_gridMergedVisitsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.m_colServiceDate,
            this.m_colPrefferedTimeFrame,
            this.m_colTechnician,
            this.gridColumn6});
            this.m_gridMergedVisitsView.GridControl = this.m_gridMergedVisits;
            this.m_gridMergedVisitsView.Name = "m_gridMergedVisitsView";
            this.m_gridMergedVisitsView.OptionsCustomization.AllowFilter = false;
            this.m_gridMergedVisitsView.OptionsCustomization.AllowGroup = false;
            this.m_gridMergedVisitsView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridMergedVisitsView.OptionsNavigation.UseTabKey = false;
            this.m_gridMergedVisitsView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.m_chkVisitSelector;
            this.gridColumn1.FieldName = "IsSelected";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 20;
            // 
            // m_chkVisitSelector
            // 
            this.m_chkVisitSelector.AutoHeight = false;
            this.m_chkVisitSelector.Name = "m_chkVisitSelector";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Visit";
            this.gridColumn2.FieldName = "ID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 45;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tasks";
            this.gridColumn3.FieldName = "TasksText";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 188;
            // 
            // m_colServiceDate
            // 
            this.m_colServiceDate.Caption = "Service Date";
            this.m_colServiceDate.FieldName = "ServiceDate";
            this.m_colServiceDate.Name = "m_colServiceDate";
            this.m_colServiceDate.OptionsColumn.AllowEdit = false;
            this.m_colServiceDate.Visible = true;
            this.m_colServiceDate.VisibleIndex = 3;
            this.m_colServiceDate.Width = 71;
            // 
            // m_colPrefferedTimeFrame
            // 
            this.m_colPrefferedTimeFrame.Caption = "Pref. Time Frame";
            this.m_colPrefferedTimeFrame.FieldName = "TimeFrame";
            this.m_colPrefferedTimeFrame.Name = "m_colPrefferedTimeFrame";
            this.m_colPrefferedTimeFrame.OptionsColumn.AllowEdit = false;
            this.m_colPrefferedTimeFrame.Visible = true;
            this.m_colPrefferedTimeFrame.VisibleIndex = 4;
            this.m_colPrefferedTimeFrame.Width = 77;
            // 
            // m_colTechnician
            // 
            this.m_colTechnician.Caption = "Technician";
            this.m_colTechnician.FieldName = "TechnicianName";
            this.m_colTechnician.Name = "m_colTechnician";
            this.m_colTechnician.OptionsColumn.AllowEdit = false;
            this.m_colTechnician.Visible = true;
            this.m_colTechnician.VisibleIndex = 5;
            this.m_colTechnician.Width = 111;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Notes";
            this.gridColumn6.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.gridColumn6.FieldName = "Notes";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 44;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(502, 272);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(421, 272);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "&OK";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // VisitMergeView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(582, 300);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisitMergeView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VisitMergeView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMergedVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMergedVisitsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkVisitSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal DevExpress.XtraGrid.GridControl m_gridMergedVisits;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridMergedVisitsView;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        internal DevExpress.XtraEditors.LabelControl m_lblExplanation;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colServiceDate;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colTechnician;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colPrefferedTimeFrame;
        internal DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit m_chkVisitSelector;
        private DevExpress.XtraEditors.LabelControl m_lblShortcut;
    }
}