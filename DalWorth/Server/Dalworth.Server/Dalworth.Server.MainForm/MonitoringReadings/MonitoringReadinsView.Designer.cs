namespace Dalworth.Server.MainForm.MonitoringReadings
{
    partial class MonitoringReadingsView
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
            this.m_gridReadings = new DevExpress.XtraGrid.GridControl();
            this.m_gridReadingsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_colTechnician = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.m_btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.m_lblShortcut = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridReadings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridReadingsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblShortcut);
            this.panelControl1.Controls.Add(this.m_gridReadings);
            this.panelControl1.Controls.Add(this.m_btnClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(859, 658);
            this.panelControl1.TabIndex = 0;
            // 
            // m_gridReadings
            // 
            this.m_gridReadings.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_gridReadings.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.m_gridReadings.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.m_gridReadings.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.m_gridReadings.EmbeddedNavigator.Buttons.First.Visible = false;
            this.m_gridReadings.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.m_gridReadings.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.m_gridReadings.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.m_gridReadings.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.m_gridReadings.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.m_gridReadings.EmbeddedNavigator.Name = "";
            this.m_gridReadings.Location = new System.Drawing.Point(0, 0);
            this.m_gridReadings.MainView = this.m_gridReadingsView;
            this.m_gridReadings.Name = "m_gridReadings";
            this.m_gridReadings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.repositoryItemTextEdit1});
            this.m_gridReadings.ShowOnlyPredefinedDetails = true;
            this.m_gridReadings.Size = new System.Drawing.Size(859, 624);
            this.m_gridReadings.TabIndex = 1;
            this.m_gridReadings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridReadingsView});
            // 
            // m_gridReadingsView
            // 
            this.m_gridReadingsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_colTechnician,
            this.m_colDate});
            this.m_gridReadingsView.GridControl = this.m_gridReadings;
            this.m_gridReadingsView.Name = "m_gridReadingsView";
            this.m_gridReadingsView.OptionsBehavior.Editable = false;
            this.m_gridReadingsView.OptionsCustomization.AllowFilter = false;
            this.m_gridReadingsView.OptionsCustomization.AllowGroup = false;
            this.m_gridReadingsView.OptionsCustomization.AllowSort = false;
            this.m_gridReadingsView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridReadingsView.OptionsMenu.EnableFooterMenu = false;
            this.m_gridReadingsView.OptionsMenu.EnableGroupPanelMenu = false;
            this.m_gridReadingsView.OptionsNavigation.UseTabKey = false;
            this.m_gridReadingsView.OptionsView.ShowGroupPanel = false;
            // 
            // m_colTechnician
            // 
            this.m_colTechnician.Caption = "Technician";
            this.m_colTechnician.FieldName = "TechnicianName";
            this.m_colTechnician.Name = "m_colTechnician";
            this.m_colTechnician.Visible = true;
            this.m_colTechnician.VisibleIndex = 0;
            // 
            // m_colDate
            // 
            this.m_colDate.Caption = "Date";
            this.m_colDate.DisplayFormat.FormatString = "g";
            this.m_colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.m_colDate.FieldName = "ReadingDate";
            this.m_colDate.Name = "m_colDate";
            this.m_colDate.Visible = true;
            this.m_colDate.VisibleIndex = 1;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(779, 630);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 2;
            this.m_btnClose.Text = "&Close";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut.Location = new System.Drawing.Point(27, 70);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 0;
            this.m_lblShortcut.Text = "&B shortcut";
            // 
            // MonitoringReadingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(859, 658);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MonitoringReadingsView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MonitoringReadingsView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridReadings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridReadingsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnClose;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        internal DevExpress.XtraGrid.GridControl m_gridReadings;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridReadingsView;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colTechnician;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colDate;
        private DevExpress.XtraEditors.LabelControl m_lblShortcut;
    }
}