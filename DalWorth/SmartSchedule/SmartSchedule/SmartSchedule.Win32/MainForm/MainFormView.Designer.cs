
using SmartSchedule.Win32.Controls;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.MainForm
{
    partial class MainFormView
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
            DevExpress.XtraScheduler.SchedulerColorSchema schedulerColorSchema1 = new DevExpress.XtraScheduler.SchedulerColorSchema();
            DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_chkBucketShowAllDates = new DevExpress.XtraEditors.CheckEdit();
            this.m_splitContainer = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.m_dashboard = new SmartSchedule.Win32.Controls.Scheduler();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.m_menuTechnicianDayOrder = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuTechnicianDefaultOrder = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuUserActionReport = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuUserManagement = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuPrintAll = new DevExpress.XtraBars.BarButtonItem();
            this.m_dashboardStorage = new DevExpress.XtraScheduler.SchedulerStorage(this.components);
            this.m_tooltipController = new DevExpress.Utils.ToolTipController(this.components);
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnMisc = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblCurrentUser = new DevExpress.XtraEditors.LabelControl();
            this.m_btnTechnicianReorder = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnLogOut = new DevExpress.XtraEditors.SimpleButton();
            this.m_chkSuspendRecommendations = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.m_dtpDashboardDate = new DevExpress.XtraEditors.DateEdit();
            this.m_tabs = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.m_gridDelayedVisits = new SmartSchedule.Win32.Controls.Grid();
            this.m_gridDelayedVisitsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.m_gridTempAssignment = new SmartSchedule.Win32.Controls.Grid();
            this.m_gridTempAssignmentView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblTechniciansCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbTechnician = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_btnFindBadRoute = new DevExpress.XtraEditors.SimpleButton();
            this.m_txtZip = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblVisitsCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.m_btnFindTechnician = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblVisitsCount = new DevExpress.XtraEditors.LabelControl();
            this.m_btnFindZip = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnFindVisit = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblBucketTempAssignmentCount = new DevExpress.XtraEditors.LabelControl();
            this.m_txtVisitNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.m_menuTechnicianArrange = new DevExpress.XtraBars.PopupMenu(this.components);
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.m_timerKeepAlive = new System.Windows.Forms.Timer(this.components);
            this.m_menuMisc = new DevExpress.XtraBars.PopupMenu(this.components);
            this.m_menuAddTechnician = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkBucketShowAllDates.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainer)).BeginInit();
            this.m_splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboardStorage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkSuspendRecommendations.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabs)).BeginInit();
            this.m_tabs.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridDelayedVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridDelayedVisitsView)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTempAssignment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTempAssignmentView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTechnician.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtZip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVisitNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuTechnicianArrange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuMisc)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_chkBucketShowAllDates);
            this.panelControl1.Controls.Add(this.m_splitContainer);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1253, 678);
            this.panelControl1.TabIndex = 0;
            // 
            // m_chkBucketShowAllDates
            // 
            this.m_chkBucketShowAllDates.Location = new System.Drawing.Point(895, 511);
            this.m_chkBucketShowAllDates.Name = "m_chkBucketShowAllDates";
            this.m_chkBucketShowAllDates.Properties.Caption = "Show all dates";
            this.m_chkBucketShowAllDates.Size = new System.Drawing.Size(95, 19);
            this.m_chkBucketShowAllDates.TabIndex = 42;
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.m_splitContainer.Horizontal = false;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer.MinimumSize = new System.Drawing.Size(750, 0);
            this.m_splitContainer.Name = "m_splitContainer";
            this.m_splitContainer.Panel1.Controls.Add(this.panelControl4);
            this.m_splitContainer.Panel1.Controls.Add(this.panelControl3);
            this.m_splitContainer.Panel1.Text = "Panel1";
            this.m_splitContainer.Panel2.Controls.Add(this.m_tabs);
            this.m_splitContainer.Panel2.Controls.Add(this.panelControl2);
            this.m_splitContainer.Panel2.MinSize = 168;
            this.m_splitContainer.Panel2.Text = "Panel2";
            this.m_splitContainer.Size = new System.Drawing.Size(1253, 678);
            this.m_splitContainer.SplitterPosition = 168;
            this.m_splitContainer.TabIndex = 0;
            this.m_splitContainer.Text = "splitContainerControl1";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.CausesValidation = false;
            this.panelControl4.Controls.Add(this.m_dashboard);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 31);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(1253, 473);
            this.panelControl4.TabIndex = 3;
            // 
            // m_dashboard
            // 
            this.m_dashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dashboard.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
            this.m_dashboard.Location = new System.Drawing.Point(0, 0);
            this.m_dashboard.MenuManager = this.barManager1;
            this.m_dashboard.Name = "m_dashboard";
            this.m_dashboard.OptionsCustomization.AllowAppointmentCopy = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.m_dashboard.OptionsCustomization.AllowAppointmentCreate = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.m_dashboard.OptionsCustomization.AllowAppointmentMultiSelect = false;
            this.m_dashboard.OptionsCustomization.AllowDisplayAppointmentForm = DevExpress.XtraScheduler.AllowDisplayAppointmentForm.Never;
            this.m_dashboard.OptionsCustomization.AllowInplaceEditor = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.m_dashboard.OptionsView.ToolTipVisibility = DevExpress.XtraScheduler.ToolTipVisibility.Always;
            schedulerColorSchema1.Cell = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(188)))));
            schedulerColorSchema1.CellBorder = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(228)))), ((int)(((byte)(177)))));
            schedulerColorSchema1.CellBorderDark = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(208)))), ((int)(((byte)(152)))));
            schedulerColorSchema1.CellLight = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(213)))));
            schedulerColorSchema1.CellLightBorder = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(199)))));
            schedulerColorSchema1.CellLightBorderDark = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(219)))), ((int)(((byte)(162)))));
            this.m_dashboard.ResourceColorSchemas.Add(schedulerColorSchema1);
            this.m_dashboard.Size = new System.Drawing.Size(1253, 473);
            this.m_dashboard.Start = new System.DateTime(2009, 3, 18, 0, 0, 0, 0);
            this.m_dashboard.Storage = this.m_dashboardStorage;
            this.m_dashboard.TabIndex = 0;
            this.m_dashboard.Text = "schedulerControl1";
            this.m_dashboard.ToolTipController = this.m_tooltipController;
            this.m_dashboard.Views.DayView.Appearance.ResourceHeaderCaption.Options.UseTextOptions = true;
            this.m_dashboard.Views.DayView.Appearance.ResourceHeaderCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.m_dashboard.Views.DayView.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Always;
            this.m_dashboard.Views.DayView.AppointmentDisplayOptions.SnapToCellsMode = DevExpress.XtraScheduler.AppointmentSnapToCellsMode.Never;
            this.m_dashboard.Views.DayView.AppointmentDisplayOptions.StartTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Always;
            this.m_dashboard.Views.DayView.NavigationButtonVisibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never;
            this.m_dashboard.Views.DayView.ResourcesPerPage = 7;
            this.m_dashboard.Views.DayView.ShowAllDayArea = false;
            this.m_dashboard.Views.DayView.ShowDayHeaders = false;
            this.m_dashboard.Views.DayView.TimeRulers.Add(timeRuler1);
            this.m_dashboard.Views.DayView.TimeScale = System.TimeSpan.Parse("00:15:00");
            this.m_dashboard.Views.DayView.VisibleTime.End = System.TimeSpan.Parse("23:45:00");
            this.m_dashboard.Views.DayView.VisibleTime.Start = System.TimeSpan.Parse("06:00:00");
            this.m_dashboard.Views.DayView.WorkTime.End = System.TimeSpan.Parse("21:00:00");
            this.m_dashboard.Views.DayView.WorkTime.Start = System.TimeSpan.Parse("06:00:00");
            this.m_dashboard.Views.MonthView.Enabled = false;
            this.m_dashboard.Views.TimelineView.Enabled = false;
            this.m_dashboard.Views.WeekView.Enabled = false;
            this.m_dashboard.Views.WorkWeekView.Enabled = false;
            this.m_dashboard.Views.WorkWeekView.TimeRulers.Add(timeRuler2);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.m_menuTechnicianDayOrder,
            this.m_menuTechnicianDefaultOrder,
            this.m_menuUserActionReport,
            this.m_menuUserManagement,
            this.m_menuPrintAll,
            this.m_menuAddTechnician});
            this.barManager1.MaxItemId = 6;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1253, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 678);
            this.barDockControlBottom.Size = new System.Drawing.Size(1253, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 678);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1253, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 678);
            // 
            // m_menuTechnicianDayOrder
            // 
            this.m_menuTechnicianDayOrder.Caption = "Day Order";
            this.m_menuTechnicianDayOrder.Id = 0;
            this.m_menuTechnicianDayOrder.Name = "m_menuTechnicianDayOrder";
            // 
            // m_menuTechnicianDefaultOrder
            // 
            this.m_menuTechnicianDefaultOrder.Caption = "Default Order";
            this.m_menuTechnicianDefaultOrder.Id = 1;
            this.m_menuTechnicianDefaultOrder.Name = "m_menuTechnicianDefaultOrder";
            // 
            // m_menuUserActionReport
            // 
            this.m_menuUserActionReport.Caption = "User Action Report";
            this.m_menuUserActionReport.Id = 2;
            this.m_menuUserActionReport.Name = "m_menuUserActionReport";
            // 
            // m_menuUserManagement
            // 
            this.m_menuUserManagement.Caption = "User Management";
            this.m_menuUserManagement.Id = 3;
            this.m_menuUserManagement.Name = "m_menuUserManagement";
            // 
            // m_menuPrintAll
            // 
            this.m_menuPrintAll.Caption = "Print All";
            this.m_menuPrintAll.Id = 4;
            this.m_menuPrintAll.Name = "m_menuPrintAll";
            // 
            // m_dashboardStorage
            // 
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.SystemColors.Window, "Normal", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(194)))), ((int)(((byte)(190))))), "Secondary Area", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(213)))), ((int)(((byte)(255))))), "Temp Assignment", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(244)))), ((int)(((byte)(156))))), "Exclusive", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Red, "Error", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Gray, "Blockout", ""));
            this.m_dashboardStorage.Appointments.Mappings.Description = "Caption";
            this.m_dashboardStorage.Appointments.Mappings.End = "TimeEnd";
            this.m_dashboardStorage.Appointments.Mappings.Label = "LabelId";
            this.m_dashboardStorage.Appointments.Mappings.ResourceId = "TechnicianId";
            this.m_dashboardStorage.Appointments.Mappings.Start = "TimeStart";
            this.m_dashboardStorage.Appointments.Mappings.Status = "StatusId";
            this.m_dashboardStorage.Appointments.Statuses.Add(new DevExpress.XtraScheduler.AppointmentStatus(DevExpress.XtraScheduler.AppointmentStatusType.Free, "Unknown", ""));
            this.m_dashboardStorage.Appointments.Statuses.Add(new DevExpress.XtraScheduler.AppointmentStatus(DevExpress.XtraScheduler.AppointmentStatusType.Tentative, System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(135)))), ((int)(((byte)(226))))), "General", ""));
            this.m_dashboardStorage.Appointments.Statuses.Add(new DevExpress.XtraScheduler.AppointmentStatus(DevExpress.XtraScheduler.AppointmentStatusType.Busy, System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(135)))), ((int)(((byte)(226))))), "GoodJoint", ""));
            this.m_dashboardStorage.Appointments.Statuses.Add(new DevExpress.XtraScheduler.AppointmentStatus(DevExpress.XtraScheduler.AppointmentStatusType.OutOfOffice, System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(83))))), "NotAllowed", ""));
            this.m_dashboardStorage.Resources.Mappings.Caption = "Caption";
            this.m_dashboardStorage.Resources.Mappings.Id = "ID";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.CausesValidation = false;
            this.panelControl3.Controls.Add(this.m_btnMisc);
            this.panelControl3.Controls.Add(this.m_lblCurrentUser);
            this.panelControl3.Controls.Add(this.m_btnTechnicianReorder);
            this.panelControl3.Controls.Add(this.m_btnLogOut);
            this.panelControl3.Controls.Add(this.m_chkSuspendRecommendations);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.m_dtpDashboardDate);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1253, 31);
            this.panelControl3.TabIndex = 2;
            // 
            // m_btnMisc
            // 
            this.m_btnMisc.Location = new System.Drawing.Point(281, 4);
            this.m_btnMisc.Name = "m_btnMisc";
            this.m_btnMisc.Size = new System.Drawing.Size(75, 23);
            this.m_btnMisc.TabIndex = 3;
            this.m_btnMisc.Text = "Misc";
            // 
            // m_lblCurrentUser
            // 
            this.m_lblCurrentUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCurrentUser.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblCurrentUser.Appearance.Options.UseFont = true;
            this.m_lblCurrentUser.Location = new System.Drawing.Point(1111, 9);
            this.m_lblCurrentUser.Name = "m_lblCurrentUser";
            this.m_lblCurrentUser.Size = new System.Drawing.Size(35, 13);
            this.m_lblCurrentUser.TabIndex = 43;
            this.m_lblCurrentUser.Text = "Buddy";
            // 
            // m_btnTechnicianReorder
            // 
            this.m_btnTechnicianReorder.Location = new System.Drawing.Point(200, 4);
            this.m_btnTechnicianReorder.Name = "m_btnTechnicianReorder";
            this.m_btnTechnicianReorder.Size = new System.Drawing.Size(75, 23);
            this.m_btnTechnicianReorder.TabIndex = 2;
            this.m_btnTechnicianReorder.Text = "Tech Reorder";
            // 
            // m_btnLogOut
            // 
            this.m_btnLogOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnLogOut.Location = new System.Drawing.Point(1030, 4);
            this.m_btnLogOut.Name = "m_btnLogOut";
            this.m_btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.m_btnLogOut.TabIndex = 4;
            this.m_btnLogOut.Text = "Log Out";
            // 
            // m_chkSuspendRecommendations
            // 
            this.m_chkSuspendRecommendations.Location = new System.Drawing.Point(362, 6);
            this.m_chkSuspendRecommendations.Name = "m_chkSuspendRecommendations";
            this.m_chkSuspendRecommendations.Properties.Caption = "Suspend Recommendations";
            this.m_chkSuspendRecommendations.Size = new System.Drawing.Size(157, 19);
            this.m_chkSuspendRecommendations.TabIndex = 41;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(5, 7);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(77, 13);
            this.labelControl5.TabIndex = 40;
            this.labelControl5.Text = "Dashboard date";
            // 
            // m_dtpDashboardDate
            // 
            this.m_dtpDashboardDate.EditValue = null;
            this.m_dtpDashboardDate.Location = new System.Drawing.Point(88, 5);
            this.m_dtpDashboardDate.Name = "m_dtpDashboardDate";
            this.m_dtpDashboardDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.m_dtpDashboardDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpDashboardDate.Properties.ValidateOnEnterKey = true;
            this.m_dtpDashboardDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpDashboardDate.Size = new System.Drawing.Size(105, 20);
            this.m_dtpDashboardDate.TabIndex = 0;
            // 
            // m_tabs
            // 
            this.m_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabs.Location = new System.Drawing.Point(0, 0);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedTabPage = this.xtraTabPage1;
            this.m_tabs.Size = new System.Drawing.Size(1001, 168);
            this.m_tabs.TabIndex = 17;
            this.m_tabs.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.m_gridDelayedVisits);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(994, 139);
            this.xtraTabPage1.Text = "Bucket";
            // 
            // m_gridDelayedVisits
            // 
            this.m_gridDelayedVisits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridDelayedVisits.IsReadOnly = false;
            this.m_gridDelayedVisits.Location = new System.Drawing.Point(0, 0);
            this.m_gridDelayedVisits.MainView = this.m_gridDelayedVisitsView;
            this.m_gridDelayedVisits.Name = "m_gridDelayedVisits";
            this.m_gridDelayedVisits.Size = new System.Drawing.Size(994, 139);
            this.m_gridDelayedVisits.TabIndex = 0;
            this.m_gridDelayedVisits.ToolTipController = this.m_tooltipController;
            this.m_gridDelayedVisits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridDelayedVisitsView});
            // 
            // m_gridDelayedVisitsView
            // 
            this.m_gridDelayedVisitsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn19,
            this.gridColumn17,
            this.gridColumn13,
            this.gridColumn16,
            this.gridColumn6,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn21,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.m_gridDelayedVisitsView.GridControl = this.m_gridDelayedVisits;
            this.m_gridDelayedVisitsView.Name = "m_gridDelayedVisitsView";
            this.m_gridDelayedVisitsView.OptionsBehavior.Editable = false;
            this.m_gridDelayedVisitsView.OptionsCustomization.AllowFilter = false;
            this.m_gridDelayedVisitsView.OptionsCustomization.AllowGroup = false;
            this.m_gridDelayedVisitsView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridDelayedVisitsView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridDelayedVisitsView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridDelayedVisitsView.OptionsView.ShowDetailButtons = false;
            this.m_gridDelayedVisitsView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = " ";
            this.gridColumn19.FieldName = "Option";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 0;
            this.gridColumn19.Width = 20;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "#";
            this.gridColumn17.FieldName = "TicketNumber";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 1;
            this.gridColumn17.Width = 48;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Schedule";
            this.gridColumn13.DisplayFormat.FormatString = "d";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn13.FieldName = "TimeStart.Date";
            this.gridColumn13.FieldNameSortGroup = "TimeStart.Date";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            this.gridColumn13.Width = 72;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Call";
            this.gridColumn16.DisplayFormat.FormatString = "g";
            this.gridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn16.FieldName = "CallDateTime";
            this.gridColumn16.FieldNameSortGroup = "CallDateTime";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 3;
            this.gridColumn16.Width = 96;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Time Frame";
            this.gridColumn6.FieldName = "TimeFrameText";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 61;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Customer";
            this.gridColumn1.FieldName = "CustomerName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 5;
            this.gridColumn1.Width = 109;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Address";
            this.gridColumn2.FieldName = "Address";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            this.gridColumn2.Width = 172;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Map";
            this.gridColumn21.FieldName = "Mapsco";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 7;
            this.gridColumn21.Width = 41;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Zip";
            this.gridColumn3.FieldName = "Zip";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 8;
            this.gridColumn3.Width = 46;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Cost";
            this.gridColumn4.DisplayFormat.FormatString = "C";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "Cost";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 9;
            this.gridColumn4.Width = 54;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Exclusivity";
            this.gridColumn5.FieldName = "ExclusivityName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 10;
            this.gridColumn5.Width = 226;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.m_gridTempAssignment);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(994, 139);
            this.xtraTabPage2.Text = "Temporary Assignments";
            // 
            // m_gridTempAssignment
            // 
            this.m_gridTempAssignment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridTempAssignment.IsReadOnly = false;
            this.m_gridTempAssignment.Location = new System.Drawing.Point(0, 0);
            this.m_gridTempAssignment.MainView = this.m_gridTempAssignmentView;
            this.m_gridTempAssignment.Name = "m_gridTempAssignment";
            this.m_gridTempAssignment.Size = new System.Drawing.Size(994, 139);
            this.m_gridTempAssignment.TabIndex = 16;
            this.m_gridTempAssignment.ToolTipController = this.m_tooltipController;
            this.m_gridTempAssignment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridTempAssignmentView});
            // 
            // m_gridTempAssignmentView
            // 
            this.m_gridTempAssignmentView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn20,
            this.gridColumn18,
            this.gridColumn7,
            this.gridColumn14,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn22,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.m_gridTempAssignmentView.GridControl = this.m_gridTempAssignment;
            this.m_gridTempAssignmentView.Name = "m_gridTempAssignmentView";
            this.m_gridTempAssignmentView.OptionsBehavior.Editable = false;
            this.m_gridTempAssignmentView.OptionsCustomization.AllowFilter = false;
            this.m_gridTempAssignmentView.OptionsCustomization.AllowGroup = false;
            this.m_gridTempAssignmentView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridTempAssignmentView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridTempAssignmentView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridTempAssignmentView.OptionsView.ShowDetailButtons = false;
            this.m_gridTempAssignmentView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn20
            // 
            this.gridColumn20.FieldName = "Option";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 0;
            this.gridColumn20.Width = 21;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "#";
            this.gridColumn18.FieldName = "TicketNumber";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 1;
            this.gridColumn18.Width = 53;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Time Frame";
            this.gridColumn7.FieldName = "TimeFrameText";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 66;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Technician";
            this.gridColumn14.FieldName = "TechnicianName";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 3;
            this.gridColumn14.Width = 131;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Customer";
            this.gridColumn8.FieldName = "CustomerName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 121;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Address";
            this.gridColumn9.FieldName = "Address";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            this.gridColumn9.Width = 227;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Map";
            this.gridColumn22.FieldName = "Mapsco";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 6;
            this.gridColumn22.Width = 35;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Zip";
            this.gridColumn10.FieldName = "Zip";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 7;
            this.gridColumn10.Width = 40;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Cost";
            this.gridColumn11.DisplayFormat.FormatString = "C";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "Cost";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 8;
            this.gridColumn11.Width = 50;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Exclusivity";
            this.gridColumn12.FieldName = "ExclusivityName";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 9;
            this.gridColumn12.Width = 223;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.CausesValidation = false;
            this.panelControl2.Controls.Add(this.m_lblTechniciansCount);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.m_cmbTechnician);
            this.panelControl2.Controls.Add(this.m_btnFindBadRoute);
            this.panelControl2.Controls.Add(this.m_txtZip);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.m_lblVisitsCountLabel);
            this.panelControl2.Controls.Add(this.m_btnFindTechnician);
            this.panelControl2.Controls.Add(this.m_lblVisitsCount);
            this.panelControl2.Controls.Add(this.m_btnFindZip);
            this.panelControl2.Controls.Add(this.m_btnFindVisit);
            this.panelControl2.Controls.Add(this.m_lblBucketTempAssignmentCount);
            this.panelControl2.Controls.Add(this.m_txtVisitNumber);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(1001, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(252, 168);
            this.panelControl2.TabIndex = 1;
            // 
            // m_lblTechniciansCount
            // 
            this.m_lblTechniciansCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTechniciansCount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblTechniciansCount.Appearance.Options.UseFont = true;
            this.m_lblTechniciansCount.Appearance.Options.UseTextOptions = true;
            this.m_lblTechniciansCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblTechniciansCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblTechniciansCount.Location = new System.Drawing.Point(162, 42);
            this.m_lblTechniciansCount.Name = "m_lblTechniciansCount";
            this.m_lblTechniciansCount.Size = new System.Drawing.Size(85, 13);
            this.m_lblTechniciansCount.TabIndex = 38;
            this.m_lblTechniciansCount.Text = "40";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(6, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 13);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "Technician Count";
            // 
            // m_cmbTechnician
            // 
            this.m_cmbTechnician.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTechnician.Location = new System.Drawing.Point(6, 61);
            this.m_cmbTechnician.Name = "m_cmbTechnician";
            this.m_cmbTechnician.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTechnician.Properties.Sorted = true;
            this.m_cmbTechnician.Size = new System.Drawing.Size(162, 20);
            this.m_cmbTechnician.TabIndex = 0;
            // 
            // m_btnFindBadRoute
            // 
            this.m_btnFindBadRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnFindBadRoute.Location = new System.Drawing.Point(172, 139);
            this.m_btnFindBadRoute.Name = "m_btnFindBadRoute";
            this.m_btnFindBadRoute.Size = new System.Drawing.Size(75, 23);
            this.m_btnFindBadRoute.TabIndex = 6;
            this.m_btnFindBadRoute.Text = "Find";
            // 
            // m_txtZip
            // 
            this.m_txtZip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtZip.Location = new System.Drawing.Point(45, 87);
            this.m_txtZip.Name = "m_txtZip";
            this.m_txtZip.Properties.Mask.EditMask = "\\d{0,5}";
            this.m_txtZip.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtZip.Properties.Mask.ShowPlaceHolders = false;
            this.m_txtZip.Size = new System.Drawing.Size(123, 20);
            this.m_txtZip.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Location = new System.Drawing.Point(7, 89);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(14, 13);
            this.labelControl3.TabIndex = 21;
            this.labelControl3.Text = "Zip";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Location = new System.Drawing.Point(6, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(150, 13);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "Bucket/Temp Assignment count";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(7, 145);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 13);
            this.labelControl1.TabIndex = 37;
            this.labelControl1.Text = "Unfeasible Route";
            // 
            // m_lblVisitsCountLabel
            // 
            this.m_lblVisitsCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblVisitsCountLabel.Location = new System.Drawing.Point(6, 4);
            this.m_lblVisitsCountLabel.Name = "m_lblVisitsCountLabel";
            this.m_lblVisitsCountLabel.Size = new System.Drawing.Size(109, 13);
            this.m_lblVisitsCountLabel.TabIndex = 14;
            this.m_lblVisitsCountLabel.Text = "Dashboard visits count";
            // 
            // m_btnFindTechnician
            // 
            this.m_btnFindTechnician.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnFindTechnician.Location = new System.Drawing.Point(172, 59);
            this.m_btnFindTechnician.Name = "m_btnFindTechnician";
            this.m_btnFindTechnician.Size = new System.Drawing.Size(75, 23);
            this.m_btnFindTechnician.TabIndex = 1;
            this.m_btnFindTechnician.Text = "Find";
            // 
            // m_lblVisitsCount
            // 
            this.m_lblVisitsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblVisitsCount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblVisitsCount.Appearance.Options.UseFont = true;
            this.m_lblVisitsCount.Appearance.Options.UseTextOptions = true;
            this.m_lblVisitsCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblVisitsCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblVisitsCount.Location = new System.Drawing.Point(162, 4);
            this.m_lblVisitsCount.Name = "m_lblVisitsCount";
            this.m_lblVisitsCount.Size = new System.Drawing.Size(85, 13);
            this.m_lblVisitsCount.TabIndex = 15;
            this.m_lblVisitsCount.Text = "124";
            // 
            // m_btnFindZip
            // 
            this.m_btnFindZip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnFindZip.Location = new System.Drawing.Point(172, 86);
            this.m_btnFindZip.Name = "m_btnFindZip";
            this.m_btnFindZip.Size = new System.Drawing.Size(75, 23);
            this.m_btnFindZip.TabIndex = 3;
            this.m_btnFindZip.Text = "Find";
            // 
            // m_btnFindVisit
            // 
            this.m_btnFindVisit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnFindVisit.Location = new System.Drawing.Point(172, 112);
            this.m_btnFindVisit.Name = "m_btnFindVisit";
            this.m_btnFindVisit.Size = new System.Drawing.Size(75, 23);
            this.m_btnFindVisit.TabIndex = 5;
            this.m_btnFindVisit.Text = "Find";
            // 
            // m_lblBucketTempAssignmentCount
            // 
            this.m_lblBucketTempAssignmentCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblBucketTempAssignmentCount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblBucketTempAssignmentCount.Appearance.Options.UseFont = true;
            this.m_lblBucketTempAssignmentCount.Appearance.Options.UseTextOptions = true;
            this.m_lblBucketTempAssignmentCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblBucketTempAssignmentCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblBucketTempAssignmentCount.Location = new System.Drawing.Point(162, 23);
            this.m_lblBucketTempAssignmentCount.Name = "m_lblBucketTempAssignmentCount";
            this.m_lblBucketTempAssignmentCount.Size = new System.Drawing.Size(85, 13);
            this.m_lblBucketTempAssignmentCount.TabIndex = 17;
            this.m_lblBucketTempAssignmentCount.Text = "18/12";
            // 
            // m_txtVisitNumber
            // 
            this.m_txtVisitNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtVisitNumber.Location = new System.Drawing.Point(45, 113);
            this.m_txtVisitNumber.Name = "m_txtVisitNumber";
            this.m_txtVisitNumber.Properties.Mask.EditMask = "\\d{0,6}";
            this.m_txtVisitNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtVisitNumber.Properties.Mask.ShowPlaceHolders = false;
            this.m_txtVisitNumber.Size = new System.Drawing.Size(123, 20);
            this.m_txtVisitNumber.TabIndex = 4;
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl6.Location = new System.Drawing.Point(7, 116);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(19, 13);
            this.labelControl6.TabIndex = 24;
            this.labelControl6.Text = "Visit";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // m_menuTechnicianArrange
            // 
            this.m_menuTechnicianArrange.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuTechnicianDayOrder),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuTechnicianDefaultOrder)});
            this.m_menuTechnicianArrange.Manager = this.barManager1;
            this.m_menuTechnicianArrange.Name = "m_menuTechnicianArrange";
            // 
            // m_timerKeepAlive
            // 
            this.m_timerKeepAlive.Enabled = true;
            this.m_timerKeepAlive.Interval = 120000;
            // 
            // m_menuMisc
            // 
            this.m_menuMisc.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuUserActionReport),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuUserManagement),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuPrintAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuAddTechnician)});
            this.m_menuMisc.Manager = this.barManager1;
            this.m_menuMisc.Name = "m_menuMisc";
            // 
            // m_menuAddTechnician
            // 
            this.m_menuAddTechnician.Caption = "Add Technician";
            this.m_menuAddTechnician.Id = 5;
            this.m_menuAddTechnician.Name = "m_menuAddTechnician";
            // 
            // MainFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 678);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MainFormView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainFormView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.barDockControlTop, 0);
            this.Controls.SetChildIndex(this.barDockControlBottom, 0);
            this.Controls.SetChildIndex(this.barDockControlRight, 0);
            this.Controls.SetChildIndex(this.barDockControlLeft, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_chkBucketShowAllDates.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainer)).EndInit();
            this.m_splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboardStorage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkSuspendRecommendations.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabs)).EndInit();
            this.m_tabs.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridDelayedVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridDelayedVisitsView)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTempAssignment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTempAssignmentView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTechnician.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtZip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVisitNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuTechnicianArrange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuMisc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal Scheduler m_dashboard;
        internal DevExpress.XtraScheduler.SchedulerStorage m_dashboardStorage;
        internal DevExpress.Utils.ToolTipController m_tooltipController;
        internal DevExpress.XtraEditors.SimpleButton m_btnTechnicianReorder;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbTechnician;
        internal DevExpress.XtraEditors.SimpleButton m_btnFindZip;
        internal DevExpress.XtraEditors.SimpleButton m_btnFindTechnician;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.TextEdit m_txtZip;
        internal Grid m_gridDelayedVisits;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridDelayedVisitsView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        internal Grid m_gridTempAssignment;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridTempAssignmentView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        internal DevExpress.XtraEditors.LabelControl m_lblBucketTempAssignmentCount;
        internal DevExpress.XtraEditors.LabelControl m_lblVisitsCount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        internal DevExpress.XtraEditors.SimpleButton m_btnFindVisit;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        internal DevExpress.XtraEditors.TextEdit m_txtVisitNumber;
        internal DevExpress.XtraBars.PopupMenu m_menuTechnicianArrange;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        internal DevExpress.XtraBars.BarButtonItem m_menuTechnicianDayOrder;
        internal DevExpress.XtraBars.BarButtonItem m_menuTechnicianDefaultOrder;
        internal DevExpress.XtraEditors.LabelControl m_lblVisitsCountLabel;
        internal DevExpress.XtraTab.XtraTabControl m_tabs;
        internal System.Windows.Forms.Timer m_timer;
        internal System.Windows.Forms.Timer m_timerKeepAlive;
        internal DevExpress.XtraEditors.SimpleButton m_btnFindBadRoute;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.DateEdit m_dtpDashboardDate;
        internal DevExpress.XtraEditors.LabelControl labelControl5;
        internal DevExpress.XtraEditors.CheckEdit m_chkSuspendRecommendations;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        internal DevExpress.XtraEditors.CheckEdit m_chkBucketShowAllDates;
        internal DevExpress.XtraEditors.SplitContainerControl m_splitContainer;
        internal DevExpress.XtraEditors.SimpleButton m_btnLogOut;
        internal DevExpress.XtraEditors.LabelControl m_lblCurrentUser;
        internal DevExpress.XtraEditors.SimpleButton m_btnMisc;
        internal DevExpress.XtraBars.BarButtonItem m_menuUserActionReport;
        internal DevExpress.XtraBars.BarButtonItem m_menuUserManagement;
        internal DevExpress.XtraBars.PopupMenu m_menuMisc;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        internal DevExpress.XtraBars.BarButtonItem m_menuPrintAll;
        internal DevExpress.XtraEditors.LabelControl m_lblTechniciansCount;
        internal DevExpress.XtraBars.BarButtonItem m_menuAddTechnician;

    }
}