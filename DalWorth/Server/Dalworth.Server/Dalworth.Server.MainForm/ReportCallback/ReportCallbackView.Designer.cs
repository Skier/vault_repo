namespace Dalworth.Server.MainForm.ReportCallback
{
    partial class ReportCallbackView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportCallbackView));
            this.m_pnlReportContent = new DevExpress.XtraEditors.PanelControl();
            this.m_gridCallback = new DevExpress.XtraGrid.GridControl();
            this.m_gridCallbackView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_images = new System.Windows.Forms.ImageList(this.components);
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colProcessLink = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_linkProcess = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_gridShortcut = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnProcess = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).BeginInit();
            this.m_pnlReportContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCallback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCallbackView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlReportContent
            // 
            this.m_pnlReportContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_pnlReportContent.Controls.Add(this.m_gridCallback);
            this.m_pnlReportContent.Controls.Add(this.m_gridShortcut);
            this.m_pnlReportContent.Controls.Add(this.panelControl1);
            this.m_pnlReportContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlReportContent.Location = new System.Drawing.Point(0, 0);
            this.m_pnlReportContent.Name = "m_pnlReportContent";
            this.m_pnlReportContent.Size = new System.Drawing.Size(970, 437);
            this.m_pnlReportContent.TabIndex = 6;
            // 
            // m_gridCallback
            // 
            this.m_gridCallback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridCallback.EmbeddedNavigator.Name = "";
            this.m_gridCallback.Location = new System.Drawing.Point(0, 32);
            this.m_gridCallback.MainView = this.m_gridCallbackView;
            this.m_gridCallback.Name = "m_gridCallback";
            this.m_gridCallback.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.m_linkProcess});
            this.m_gridCallback.ShowOnlyPredefinedDetails = true;
            this.m_gridCallback.Size = new System.Drawing.Size(970, 405);
            this.m_gridCallback.TabIndex = 13;
            this.m_gridCallback.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridCallbackView});
            // 
            // m_gridCallbackView
            // 
            this.m_gridCallbackView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.m_colProcessLink});
            this.m_gridCallbackView.GridControl = this.m_gridCallback;
            this.m_gridCallbackView.Name = "m_gridCallbackView";
            this.m_gridCallbackView.OptionsCustomization.AllowFilter = false;
            this.m_gridCallbackView.OptionsCustomization.AllowGroup = false;
            this.m_gridCallbackView.OptionsNavigation.UseTabKey = false;
            this.m_gridCallbackView.OptionsSelection.MultiSelect = true;
            this.m_gridCallbackView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridColumn1.FieldName = "StatusImageIndex";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 20;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 3, 2)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.m_images;
            // 
            // m_images
            // 
            this.m_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_images.ImageStream")));
            this.m_images.TransparentColor = System.Drawing.Color.Magenta;
            this.m_images.Images.SetKeyName(0, "phone7.bmp");
            this.m_images.Images.SetKeyName(1, "tape2.bmp");
            this.m_images.Images.SetKeyName(2, "busy.bmp");
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Customer";
            this.gridColumn2.FieldName = "CustomerName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 269;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Callback Reason";
            this.gridColumn3.FieldName = "CallbackReason";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 178;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "LM/Busy Count";
            this.gridColumn4.FieldName = "LeftMessageBusyCount";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 84;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Last Process Date";
            this.gridColumn5.DisplayFormat.FormatString = "g";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn5.FieldName = "CallbackLastAttemptDate";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 100;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Processing Description";
            this.gridColumn6.FieldName = "ProcessingDescription";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 298;
            // 
            // m_colProcessLink
            // 
            this.m_colProcessLink.ColumnEdit = this.m_linkProcess;
            this.m_colProcessLink.FieldName = "ProcessLinkText";
            this.m_colProcessLink.Name = "m_colProcessLink";
            this.m_colProcessLink.Visible = true;
            this.m_colProcessLink.VisibleIndex = 6;
            this.m_colProcessLink.Width = 57;
            // 
            // m_linkProcess
            // 
            this.m_linkProcess.AutoHeight = false;
            this.m_linkProcess.Name = "m_linkProcess";
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
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_btnProcess);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(970, 32);
            this.panelControl1.TabIndex = 0;
            // 
            // m_btnProcess
            // 
            this.m_btnProcess.Location = new System.Drawing.Point(238, 5);
            this.m_btnProcess.Name = "m_btnProcess";
            this.m_btnProcess.Size = new System.Drawing.Size(75, 23);
            this.m_btnProcess.TabIndex = 0;
            this.m_btnProcess.Text = "Pr&ocess";
            // 
            // ReportCallbackView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_pnlReportContent);
            this.Name = "ReportCallbackView";
            this.Size = new System.Drawing.Size(970, 437);
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlReportContent)).EndInit();
            this.m_pnlReportContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCallback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCallbackView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.PanelControl m_pnlReportContent;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl m_gridShortcut;
        internal DevExpress.XtraGrid.GridControl m_gridCallback;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridCallbackView;
        internal System.Windows.Forms.ImageList m_images;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        internal DevExpress.XtraEditors.SimpleButton m_btnProcess;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colProcessLink;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkProcess;        
    }
}
