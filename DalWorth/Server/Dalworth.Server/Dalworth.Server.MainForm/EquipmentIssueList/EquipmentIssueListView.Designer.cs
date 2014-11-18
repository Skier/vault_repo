namespace Dalworth.Server.MainForm.EquipmentIssueList
{
    partial class EquipmentIssueListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EquipmentIssueListView));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_gridIssue = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewIssue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnContinue = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblShortcut = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridIssue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewIssue)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblShortcut);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_gridIssue);
            this.panelControl1.Controls.Add(this.m_btnBack);
            this.panelControl1.Controls.Add(this.m_btnContinue);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(500, 345);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(492, 39);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = resources.GetString("labelControl1.Text");
            // 
            // m_gridIssue
            // 
            this.m_gridIssue.EmbeddedNavigator.Name = "";
            this.m_gridIssue.Location = new System.Drawing.Point(3, 48);
            this.m_gridIssue.MainView = this.m_gridViewIssue;
            this.m_gridIssue.Name = "m_gridIssue";
            this.m_gridIssue.Size = new System.Drawing.Size(494, 261);
            this.m_gridIssue.TabIndex = 2;
            this.m_gridIssue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewIssue});
            // 
            // m_gridViewIssue
            // 
            this.m_gridViewIssue.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn1,
            this.gridColumn5});
            this.m_gridViewIssue.GridControl = this.m_gridIssue;
            this.m_gridViewIssue.GroupCount = 1;
            this.m_gridViewIssue.GroupFormat = "{0}[#image]{1} {2}";
            this.m_gridViewIssue.Name = "m_gridViewIssue";
            this.m_gridViewIssue.OptionsBehavior.AutoExpandAllGroups = true;
            this.m_gridViewIssue.OptionsCustomization.AllowFilter = false;
            this.m_gridViewIssue.OptionsCustomization.AllowGroup = false;
            this.m_gridViewIssue.OptionsCustomization.AllowSort = false;
            this.m_gridViewIssue.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewIssue.OptionsNavigation.UseTabKey = false;
            this.m_gridViewIssue.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.m_gridViewIssue.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.m_gridViewIssue.OptionsView.RowAutoHeight = true;
            this.m_gridViewIssue.OptionsView.ShowGroupPanel = false;
            this.m_gridViewIssue.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn3
            // 
            this.gridColumn3.FieldName = "IssueGroupText";
            this.gridColumn3.ImageIndex = 2;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Width = 50;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Serial Number";
            this.gridColumn4.FieldName = "SerialNumber";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 78;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Type";
            this.gridColumn1.FieldName = "TypeText";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 77;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Issue";
            this.gridColumn5.FieldName = "IssueText";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 234;
            // 
            // m_btnBack
            // 
            this.m_btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnBack.Location = new System.Drawing.Point(422, 315);
            this.m_btnBack.Name = "m_btnBack";
            this.m_btnBack.Size = new System.Drawing.Size(75, 23);
            this.m_btnBack.TabIndex = 4;
            this.m_btnBack.Text = "&Back";
            // 
            // m_btnContinue
            // 
            this.m_btnContinue.Location = new System.Drawing.Point(341, 315);
            this.m_btnContinue.Name = "m_btnContinue";
            this.m_btnContinue.Size = new System.Drawing.Size(75, 23);
            this.m_btnContinue.TabIndex = 3;
            this.m_btnContinue.Text = "&Continue";
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut.Location = new System.Drawing.Point(23, 115);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 1;
            this.m_lblShortcut.Text = "&B shortcut";
            // 
            // EquipmentIssueListView
            // 
            this.AcceptButton = this.m_btnContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnBack;
            this.ClientSize = new System.Drawing.Size(500, 345);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EquipmentIssueListView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EquipmentErrorListView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridIssue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewIssue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnBack;
        internal DevExpress.XtraEditors.SimpleButton m_btnContinue;
        internal DevExpress.XtraGrid.GridControl m_gridIssue;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewIssue;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.LabelControl m_lblShortcut;
    }
}