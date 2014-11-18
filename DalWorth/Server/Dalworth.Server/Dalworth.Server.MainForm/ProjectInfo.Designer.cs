namespace Dalworth.Server.MainForm
{
    partial class ProjectInfo
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
            this.m_grpPRoject = new DevExpress.XtraEditors.GroupControl();
            this.m_btnEditProject = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.m_gridVisits = new DevExpress.XtraGrid.GridControl();
            this.m_gridVisitsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Technician = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Task = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClosedAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_lblClosedAmount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProjectType = new DevExpress.XtraEditors.LabelControl();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProjectAdvertisingSource = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProjectSalesRep = new DevExpress.XtraEditors.LabelControl();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProjectProgress = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProjectBalance = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProjectId = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProjectManager = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProjectStatus = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_grpPRoject)).BeginInit();
            this.m_grpPRoject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridVisitsView)).BeginInit();
            this.SuspendLayout();
            // 
            // m_grpPRoject
            // 
            this.m_grpPRoject.Controls.Add(this.m_btnEditProject);
            this.m_grpPRoject.Controls.Add(this.groupControl3);
            this.m_grpPRoject.Controls.Add(this.m_lblClosedAmount);
            this.m_grpPRoject.Controls.Add(this.labelControl23);
            this.m_grpPRoject.Controls.Add(this.m_lblProjectType);
            this.m_grpPRoject.Controls.Add(this.labelControl22);
            this.m_grpPRoject.Controls.Add(this.m_lblProjectAdvertisingSource);
            this.m_grpPRoject.Controls.Add(this.labelControl18);
            this.m_grpPRoject.Controls.Add(this.m_lblProjectSalesRep);
            this.m_grpPRoject.Controls.Add(this.labelControl20);
            this.m_grpPRoject.Controls.Add(this.m_lblProjectProgress);
            this.m_grpPRoject.Controls.Add(this.labelControl5);
            this.m_grpPRoject.Controls.Add(this.m_lblProjectBalance);
            this.m_grpPRoject.Controls.Add(this.labelControl3);
            this.m_grpPRoject.Controls.Add(this.labelControl10);
            this.m_grpPRoject.Controls.Add(this.labelControl7);
            this.m_grpPRoject.Controls.Add(this.m_lblProjectId);
            this.m_grpPRoject.Controls.Add(this.m_lblProjectManager);
            this.m_grpPRoject.Controls.Add(this.m_lblProjectStatus);
            this.m_grpPRoject.Controls.Add(this.labelControl1);
            this.m_grpPRoject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grpPRoject.Location = new System.Drawing.Point(0, 0);
            this.m_grpPRoject.Name = "m_grpPRoject";
            this.m_grpPRoject.Size = new System.Drawing.Size(620, 443);
            this.m_grpPRoject.TabIndex = 2;
            this.m_grpPRoject.Text = "Project";
            // 
            // m_btnEditProject
            // 
            this.m_btnEditProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnEditProject.Location = new System.Drawing.Point(532, 24);
            this.m_btnEditProject.Name = "m_btnEditProject";
            this.m_btnEditProject.Size = new System.Drawing.Size(83, 23);
            this.m_btnEditProject.TabIndex = 28;
            this.m_btnEditProject.Text = "Edit Project";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.m_gridVisits);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl3.Location = new System.Drawing.Point(2, 179);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(616, 262);
            this.groupControl3.TabIndex = 1;
            this.groupControl3.Text = "Visit History";
            // 
            // m_gridVisits
            // 
            this.m_gridVisits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridVisits.EmbeddedNavigator.Name = "";
            this.m_gridVisits.Location = new System.Drawing.Point(2, 20);
            this.m_gridVisits.MainView = this.m_gridVisitsView;
            this.m_gridVisits.Name = "m_gridVisits";
            this.m_gridVisits.Size = new System.Drawing.Size(612, 240);
            this.m_gridVisits.TabIndex = 16;
            this.m_gridVisits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridVisitsView});
            // 
            // m_gridVisitsView
            // 
            this.m_gridVisitsView.ActiveFilterEnabled = false;
            this.m_gridVisitsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.Date,
            this.Technician,
            this.Task,
            this.Status,
            this.ClosedAmount});
            this.m_gridVisitsView.GridControl = this.m_gridVisits;
            this.m_gridVisitsView.Name = "m_gridVisitsView";
            this.m_gridVisitsView.OptionsCustomization.AllowFilter = false;
            this.m_gridVisitsView.OptionsCustomization.AllowGroup = false;
            this.m_gridVisitsView.OptionsCustomization.AllowSort = false;
            this.m_gridVisitsView.OptionsDetail.AllowZoomDetail = false;
            this.m_gridVisitsView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridVisitsView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridVisitsView.OptionsView.ShowDetailButtons = false;
            this.m_gridVisitsView.OptionsView.ShowGroupPanel = false;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.AllowFocus = false;
            this.ID.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ID.Visible = true;
            this.ID.VisibleIndex = 0;
            this.ID.Width = 62;
            // 
            // Date
            // 
            this.Date.Caption = "Date";
            this.Date.DisplayFormat.FormatString = "MM/dd/yyyy";
            this.Date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Date.FieldName = "Date";
            this.Date.Name = "Date";
            this.Date.OptionsColumn.AllowEdit = false;
            this.Date.Visible = true;
            this.Date.VisibleIndex = 4;
            this.Date.Width = 99;
            // 
            // Technician
            // 
            this.Technician.Caption = "Technician";
            this.Technician.FieldName = "TechnicianName";
            this.Technician.Name = "Technician";
            this.Technician.OptionsColumn.AllowEdit = false;
            this.Technician.Visible = true;
            this.Technician.VisibleIndex = 2;
            this.Technician.Width = 157;
            // 
            // Task
            // 
            this.Task.Caption = "Task";
            this.Task.FieldName = "MainTaskName";
            this.Task.Name = "Task";
            this.Task.OptionsColumn.AllowEdit = false;
            this.Task.Visible = true;
            this.Task.VisibleIndex = 3;
            this.Task.Width = 119;
            // 
            // Status
            // 
            this.Status.Caption = "Status";
            this.Status.FieldName = "VisitStatusText";
            this.Status.Name = "Status";
            this.Status.OptionsColumn.AllowEdit = false;
            this.Status.Visible = true;
            this.Status.VisibleIndex = 1;
            this.Status.Width = 68;
            // 
            // ClosedAmount
            // 
            this.ClosedAmount.Caption = "Amount";
            this.ClosedAmount.DisplayFormat.FormatString = "C";
            this.ClosedAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ClosedAmount.FieldName = "ClosedAmount";
            this.ClosedAmount.Name = "ClosedAmount";
            this.ClosedAmount.OptionsColumn.AllowEdit = false;
            this.ClosedAmount.Visible = true;
            this.ClosedAmount.VisibleIndex = 5;
            this.ClosedAmount.Width = 88;
            // 
            // m_lblClosedAmount
            // 
            this.m_lblClosedAmount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblClosedAmount.Appearance.Options.UseFont = true;
            this.m_lblClosedAmount.Appearance.Options.UseTextOptions = true;
            this.m_lblClosedAmount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblClosedAmount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblClosedAmount.Location = new System.Drawing.Point(367, 23);
            this.m_lblClosedAmount.Name = "m_lblClosedAmount";
            this.m_lblClosedAmount.Size = new System.Drawing.Size(76, 13);
            this.m_lblClosedAmount.TabIndex = 27;
            this.m_lblClosedAmount.Text = "$456.00";
            // 
            // labelControl23
            // 
            this.labelControl23.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl23.Appearance.Options.UseFont = true;
            this.labelControl23.Location = new System.Drawing.Point(289, 23);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(72, 13);
            this.labelControl23.TabIndex = 25;
            this.labelControl23.Text = "Closed Amount";
            // 
            // m_lblProjectType
            // 
            this.m_lblProjectType.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectType.Appearance.Options.UseFont = true;
            this.m_lblProjectType.Location = new System.Drawing.Point(400, 67);
            this.m_lblProjectType.Name = "m_lblProjectType";
            this.m_lblProjectType.Size = new System.Drawing.Size(43, 13);
            this.m_lblProjectType.TabIndex = 23;
            this.m_lblProjectType.Text = "Deflood";
            // 
            // labelControl22
            // 
            this.labelControl22.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl22.Appearance.Options.UseFont = true;
            this.labelControl22.Location = new System.Drawing.Point(289, 67);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(61, 13);
            this.labelControl22.TabIndex = 24;
            this.labelControl22.Text = "Project Type";
            // 
            // m_lblProjectAdvertisingSource
            // 
            this.m_lblProjectAdvertisingSource.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectAdvertisingSource.Appearance.Options.UseFont = true;
            this.m_lblProjectAdvertisingSource.Location = new System.Drawing.Point(114, 86);
            this.m_lblProjectAdvertisingSource.Name = "m_lblProjectAdvertisingSource";
            this.m_lblProjectAdvertisingSource.Size = new System.Drawing.Size(62, 13);
            this.m_lblProjectAdvertisingSource.TabIndex = 22;
            this.m_lblProjectAdvertisingSource.Text = "Sales - ERP";
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Location = new System.Drawing.Point(5, 86);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(90, 13);
            this.labelControl18.TabIndex = 21;
            this.labelControl18.Text = "Advertising Source";
            // 
            // m_lblProjectSalesRep
            // 
            this.m_lblProjectSalesRep.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectSalesRep.Appearance.Options.UseFont = true;
            this.m_lblProjectSalesRep.Location = new System.Drawing.Point(114, 67);
            this.m_lblProjectSalesRep.Name = "m_lblProjectSalesRep";
            this.m_lblProjectSalesRep.Size = new System.Drawing.Size(67, 13);
            this.m_lblProjectSalesRep.TabIndex = 20;
            this.m_lblProjectSalesRep.Text = "Baker, Cody";
            // 
            // labelControl20
            // 
            this.labelControl20.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl20.Appearance.Options.UseFont = true;
            this.labelControl20.Location = new System.Drawing.Point(5, 67);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(47, 13);
            this.labelControl20.TabIndex = 19;
            this.labelControl20.Text = "Sales Rep";
            // 
            // m_lblProjectProgress
            // 
            this.m_lblProjectProgress.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectProgress.Appearance.Options.UseFont = true;
            this.m_lblProjectProgress.Location = new System.Drawing.Point(400, 86);
            this.m_lblProjectProgress.Name = "m_lblProjectProgress";
            this.m_lblProjectProgress.Size = new System.Drawing.Size(59, 13);
            this.m_lblProjectProgress.TabIndex = 14;
            this.m_lblProjectProgress.Text = "In Process";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(289, 86);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(42, 13);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "Progress";
            // 
            // m_lblProjectBalance
            // 
            this.m_lblProjectBalance.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectBalance.Appearance.Options.UseFont = true;
            this.m_lblProjectBalance.Appearance.Options.UseTextOptions = true;
            this.m_lblProjectBalance.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblProjectBalance.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblProjectBalance.Location = new System.Drawing.Point(367, 42);
            this.m_lblProjectBalance.Name = "m_lblProjectBalance";
            this.m_lblProjectBalance.Size = new System.Drawing.Size(76, 13);
            this.m_lblProjectBalance.TabIndex = 6;
            this.m_lblProjectBalance.Text = "$456.00";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(289, 42);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Balance";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(5, 105);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(42, 13);
            this.labelControl10.TabIndex = 12;
            this.labelControl10.Text = "Manager";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(5, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(11, 13);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "ID";
            // 
            // m_lblProjectId
            // 
            this.m_lblProjectId.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectId.Appearance.Options.UseFont = true;
            this.m_lblProjectId.Location = new System.Drawing.Point(114, 21);
            this.m_lblProjectId.Name = "m_lblProjectId";
            this.m_lblProjectId.Size = new System.Drawing.Size(20, 19);
            this.m_lblProjectId.TabIndex = 7;
            this.m_lblProjectId.Text = "34";
            // 
            // m_lblProjectManager
            // 
            this.m_lblProjectManager.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectManager.Appearance.Options.UseFont = true;
            this.m_lblProjectManager.Location = new System.Drawing.Point(114, 105);
            this.m_lblProjectManager.Name = "m_lblProjectManager";
            this.m_lblProjectManager.Size = new System.Drawing.Size(67, 13);
            this.m_lblProjectManager.TabIndex = 11;
            this.m_lblProjectManager.Text = "Baker, Cody";
            // 
            // m_lblProjectStatus
            // 
            this.m_lblProjectStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectStatus.Appearance.Options.UseFont = true;
            this.m_lblProjectStatus.Location = new System.Drawing.Point(114, 48);
            this.m_lblProjectStatus.Name = "m_lblProjectStatus";
            this.m_lblProjectStatus.Size = new System.Drawing.Size(29, 13);
            this.m_lblProjectStatus.TabIndex = 9;
            this.m_lblProjectStatus.Text = "Open";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(4, 48);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(31, 13);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "Status";
            // 
            // ProjectInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_grpPRoject);
            this.Name = "ProjectInfo";
            this.Size = new System.Drawing.Size(620, 443);
            ((System.ComponentModel.ISupportInitialize)(this.m_grpPRoject)).EndInit();
            this.m_grpPRoject.ResumeLayout(false);
            this.m_grpPRoject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridVisitsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.GroupControl m_grpPRoject;
        internal DevExpress.XtraEditors.SimpleButton m_btnEditProject;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        internal DevExpress.XtraGrid.GridControl m_gridVisits;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridVisitsView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn Date;
        private DevExpress.XtraGrid.Columns.GridColumn Technician;
        private DevExpress.XtraGrid.Columns.GridColumn Task;
        private DevExpress.XtraGrid.Columns.GridColumn Status;
        private DevExpress.XtraGrid.Columns.GridColumn ClosedAmount;
        internal DevExpress.XtraEditors.LabelControl m_lblClosedAmount;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        internal DevExpress.XtraEditors.LabelControl m_lblProjectType;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        internal DevExpress.XtraEditors.LabelControl m_lblProjectAdvertisingSource;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        internal DevExpress.XtraEditors.LabelControl m_lblProjectSalesRep;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        internal DevExpress.XtraEditors.LabelControl m_lblProjectProgress;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        internal DevExpress.XtraEditors.LabelControl m_lblProjectBalance;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        internal DevExpress.XtraEditors.LabelControl m_lblProjectId;
        internal DevExpress.XtraEditors.LabelControl m_lblProjectManager;
        internal DevExpress.XtraEditors.LabelControl m_lblProjectStatus;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
