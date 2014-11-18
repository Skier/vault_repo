using Dalworth.Server.MainForm.Components;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DateEdit=Dalworth.Server.MainForm.Components.DateEdit;

namespace Dalworth.Server.MainForm.Dashboard
{
    partial class DashboardView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardView));
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.m_dashboard = new Dalworth.Server.MainForm.Components.Scheduler();
            this.m_barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.m_menuPendingVisitPrint = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuPendingVisitSplit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuPendingVisitNewVisit = new DevExpress.XtraBars.BarButtonItem();
            this.m_images = new System.Windows.Forms.ImageList(this.components);
            this.m_dashboardStorage = new DevExpress.XtraScheduler.SchedulerStorage(this.components);
            this.m_toolTipDashboard = new DevExpress.Utils.ToolTipController(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblDispatcher = new DevExpress.XtraEditors.LabelControl();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.m_lblProjectFeedbackCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.m_lblLeadsOpened = new DevExpress.XtraEditors.LabelControl();
            this.m_lblLeadsPending = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.m_lblDShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_dtpDashboardDate = new Dalworth.Server.MainForm.Components.DateEdit();
            this.m_lblCurrentTime = new DevExpress.XtraEditors.LabelControl();
            this.m_btnPrevDate = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnNextDate = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblDashboardDate = new DevExpress.XtraEditors.LabelControl();
            this.m_lblDayTotalAmount = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblFShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_gridPendingVisits = new DevExpress.XtraGrid.GridControl();
            this.m_gridPendingVisitsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_columnVisitNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_linkGridVisit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_columnReady = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnMap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnTaskType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnServiceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_columnTimeFrame = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_notes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.m_colPendingMore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_linkGridPendingPrint = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_lblCShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_btnNewVisit = new DevExpress.XtraEditors.SimpleButton();
            this.m_txtPendingVisitCustomerFilter = new DevExpress.XtraEditors.ButtonEdit();
            this.m_cmbPendingVisitsFilter = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.m_timerCurrentTime = new System.Windows.Forms.Timer(this.components);
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.m_appointmentImages = new System.Windows.Forms.ImageList(this.components);
            this.m_menuPendingVisitAction = new DevExpress.XtraBars.PopupMenu(this.components);
            this.m_timerBlinking = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboardStorage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridPendingVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridPendingVisitsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPendingVisitCustomerFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbPendingVisitsFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuPendingVisitAction)).BeginInit();
            this.SuspendLayout();
            // 
            // m_timer
            // 
            this.m_timer.Interval = 5000;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.splitterControl1);
            this.panel1.Controls.Add(this.panelControl2);
            this.panel1.Controls.Add(this.panelControl3);
            this.panel1.Controls.Add(this.panelControl1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1114, 689);
            this.m_toolTipDashboard.SetSuperTip(this.panel1, null);
            this.panel1.TabIndex = 5;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(0, 472);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(1114, 6);
            this.m_toolTipDashboard.SetSuperTip(this.splitterControl1, null);
            this.splitterControl1.TabIndex = 13;
            this.splitterControl1.TabStop = false;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.m_dashboard);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 36);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1114, 442);
            this.m_toolTipDashboard.SetSuperTip(this.panelControl2, null);
            this.panelControl2.TabIndex = 12;
            // 
            // m_dashboard
            // 
            this.m_dashboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dashboard.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
            this.m_dashboard.Location = new System.Drawing.Point(0, 0);
            this.m_dashboard.MenuManager = this.m_barManager;
            this.m_dashboard.Name = "m_dashboard";
            this.m_dashboard.NextControl = null;
            this.m_dashboard.OptionsCustomization.AllowAppointmentConflicts = DevExpress.XtraScheduler.AppointmentConflictsMode.Forbidden;
            this.m_dashboard.OptionsCustomization.AllowAppointmentCopy = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.m_dashboard.OptionsCustomization.AllowAppointmentCreate = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.m_dashboard.OptionsCustomization.AllowAppointmentDrag = DevExpress.XtraScheduler.UsedAppointmentType.Custom;
            this.m_dashboard.OptionsCustomization.AllowAppointmentMultiSelect = false;
            this.m_dashboard.OptionsCustomization.AllowAppointmentResize = DevExpress.XtraScheduler.UsedAppointmentType.Custom;
            this.m_dashboard.OptionsCustomization.AllowDisplayAppointmentForm = DevExpress.XtraScheduler.AllowDisplayAppointmentForm.Never;
            this.m_dashboard.OptionsCustomization.AllowInplaceEditor = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.m_dashboard.OptionsView.ToolTipVisibility = DevExpress.XtraScheduler.ToolTipVisibility.Always;
            this.m_dashboard.PreviousControl = null;
            schedulerColorSchema1.Cell = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(188)))));
            schedulerColorSchema1.CellBorder = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(228)))), ((int)(((byte)(177)))));
            schedulerColorSchema1.CellBorderDark = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(208)))), ((int)(((byte)(152)))));
            schedulerColorSchema1.CellLight = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(213)))));
            schedulerColorSchema1.CellLightBorder = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(199)))));
            schedulerColorSchema1.CellLightBorderDark = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(219)))), ((int)(((byte)(162)))));
            this.m_dashboard.ResourceColorSchemas.Add(schedulerColorSchema1);
            this.m_dashboard.ResourceNavigator.Buttons.ImageList = this.m_images;
            this.m_dashboard.Size = new System.Drawing.Size(1114, 436);
            this.m_dashboard.Start = new System.DateTime(2007, 8, 27, 0, 0, 0, 0);
            this.m_dashboard.Storage = this.m_dashboardStorage;
            this.m_dashboard.TabIndex = 5;
            this.m_dashboard.Text = "schedulerControl1";
            this.m_dashboard.ToolTipController = this.m_toolTipDashboard;
            this.m_dashboard.Views.DayView.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never;
            this.m_dashboard.Views.DayView.AppointmentDisplayOptions.SnapToCells = false;
            this.m_dashboard.Views.DayView.AppointmentDisplayOptions.StartTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never;
            this.m_dashboard.Views.DayView.NavigationButtonVisibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never;
            this.m_dashboard.Views.DayView.ShowAllDayArea = false;
            this.m_dashboard.Views.DayView.ShowDayHeaders = false;
            this.m_dashboard.Views.DayView.TimeRulers.Add(timeRuler1);
            this.m_dashboard.Views.DayView.TimeScale = System.TimeSpan.Parse("00:15:00");
            this.m_dashboard.Views.DayView.VisibleTime.End = System.TimeSpan.Parse("23:59:59");
            this.m_dashboard.Views.DayView.WorkTime.Start = System.TimeSpan.Parse("07:30:00");
            this.m_dashboard.Views.MonthView.Enabled = false;
            this.m_dashboard.Views.TimelineView.Enabled = false;
            this.m_dashboard.Views.WeekView.Enabled = false;
            this.m_dashboard.Views.WorkWeekView.Enabled = false;
            this.m_dashboard.Views.WorkWeekView.TimeRulers.Add(timeRuler2);
            // 
            // m_barManager
            // 
            this.m_barManager.DockControls.Add(this.barDockControlTop);
            this.m_barManager.DockControls.Add(this.barDockControlBottom);
            this.m_barManager.DockControls.Add(this.barDockControlLeft);
            this.m_barManager.DockControls.Add(this.barDockControlRight);
            this.m_barManager.Form = this;
            this.m_barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.barStaticItem1,
            this.m_menuPendingVisitPrint,
            this.m_menuPendingVisitSplit,
            this.barButtonItem1,
            this.m_menuPendingVisitNewVisit});
            this.m_barManager.MaxItemId = 6;
            // 
            // barDockControlTop
            // 
            this.m_toolTipDashboard.SetSuperTip(this.barDockControlTop, null);
            // 
            // barDockControlBottom
            // 
            this.m_toolTipDashboard.SetSuperTip(this.barDockControlBottom, null);
            // 
            // barDockControlLeft
            // 
            this.m_toolTipDashboard.SetSuperTip(this.barDockControlLeft, null);
            // 
            // barDockControlRight
            // 
            this.m_toolTipDashboard.SetSuperTip(this.barDockControlRight, null);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "barSubItem1";
            this.barSubItem1.Id = 0;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "barStaticItem1";
            this.barStaticItem1.Id = 1;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // m_menuPendingVisitPrint
            // 
            this.m_menuPendingVisitPrint.Caption = "Print";
            this.m_menuPendingVisitPrint.Id = 2;
            this.m_menuPendingVisitPrint.Name = "m_menuPendingVisitPrint";
            // 
            // m_menuPendingVisitSplit
            // 
            this.m_menuPendingVisitSplit.Caption = "Split";
            this.m_menuPendingVisitSplit.Id = 3;
            this.m_menuPendingVisitSplit.Name = "m_menuPendingVisitSplit";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // m_menuPendingVisitNewVisit
            // 
            this.m_menuPendingVisitNewVisit.Caption = "New Visit";
            this.m_menuPendingVisitNewVisit.Id = 5;
            this.m_menuPendingVisitNewVisit.Name = "m_menuPendingVisitNewVisit";
            // 
            // m_images
            // 
            this.m_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_images.ImageStream")));
            this.m_images.TransparentColor = System.Drawing.Color.Magenta;
            this.m_images.Images.SetKeyName(0, "customization.bmp");
            // 
            // m_dashboardStorage
            // 
            this.m_dashboardStorage.Appointments.CustomFieldMappings.Add(new DevExpress.XtraScheduler.AppointmentCustomFieldMapping("AppointmentWrapper", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel("Unknown", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel("Pending", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Gray, "Completed", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.LightGreen, "Assigned", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Yellow, "Assigned For Execution", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.LightBlue, "Accepted", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Red, "Declined", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Pink, "Arrived", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Orange, "NoGo", ""));
            this.m_dashboardStorage.Appointments.Labels.Add(new DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Maroon, "Reschedule Needed", ""));
            this.m_dashboardStorage.Appointments.Mappings.Description = "Description";
            this.m_dashboardStorage.Appointments.Mappings.End = "End";
            this.m_dashboardStorage.Appointments.Mappings.Label = "Label";
            this.m_dashboardStorage.Appointments.Mappings.ResourceId = "ResourceId";
            this.m_dashboardStorage.Appointments.Mappings.Start = "Start";
            this.m_dashboardStorage.Appointments.Mappings.Status = "Status";
            this.m_dashboardStorage.Appointments.Mappings.Subject = "Subject";
            this.m_dashboardStorage.Appointments.Mappings.Type = "Type";
            this.m_dashboardStorage.EnableReminders = false;
            this.m_dashboardStorage.Resources.Mappings.Caption = "Caption";
            this.m_dashboardStorage.Resources.Mappings.Id = "ID";
            // 
            // m_toolTipDashboard
            // 
            this.m_toolTipDashboard.AutoPopDelay = 120000;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblDispatcher);
            this.panelControl1.Controls.Add(this.layoutControl3);
            this.panelControl1.Controls.Add(this.layoutControl1);
            this.panelControl1.Controls.Add(this.m_lblDShortcut);
            this.panelControl1.Controls.Add(this.m_dtpDashboardDate);
            this.panelControl1.Controls.Add(this.m_lblCurrentTime);
            this.panelControl1.Controls.Add(this.m_btnPrevDate);
            this.panelControl1.Controls.Add(this.m_btnNextDate);
            this.panelControl1.Controls.Add(this.m_lblDashboardDate);
            this.panelControl1.Controls.Add(this.m_lblDayTotalAmount);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1114, 36);
            this.m_toolTipDashboard.SetSuperTip(this.panelControl1, null);
            this.panelControl1.TabIndex = 0;
            // 
            // m_lblDispatcher
            // 
            this.m_lblDispatcher.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblDispatcher.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(133)))), ((int)(((byte)(111)))));
            this.m_lblDispatcher.Appearance.Options.UseFont = true;
            this.m_lblDispatcher.Appearance.Options.UseForeColor = true;
            this.m_lblDispatcher.Location = new System.Drawing.Point(376, 7);
            this.m_lblDispatcher.Name = "m_lblDispatcher";
            this.m_lblDispatcher.Size = new System.Drawing.Size(90, 19);
            this.m_lblDispatcher.TabIndex = 60;
            this.m_lblDispatcher.Text = "Nick Hobbs";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl3.AutoScroll = false;
            this.layoutControl3.Controls.Add(this.m_lblProjectFeedbackCount);
            this.layoutControl3.Controls.Add(this.labelControl8);
            this.layoutControl3.Location = new System.Drawing.Point(911, 3);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(92, 27);
            this.m_toolTipDashboard.SetSuperTip(this.layoutControl3, null);
            this.layoutControl3.TabIndex = 59;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // m_lblProjectFeedbackCount
            // 
            this.m_lblProjectFeedbackCount.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.m_lblProjectFeedbackCount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_lblProjectFeedbackCount.Appearance.ForeColor = System.Drawing.Color.Red;
            this.m_lblProjectFeedbackCount.Appearance.Options.UseBackColor = true;
            this.m_lblProjectFeedbackCount.Appearance.Options.UseFont = true;
            this.m_lblProjectFeedbackCount.Appearance.Options.UseForeColor = true;
            this.m_lblProjectFeedbackCount.Location = new System.Drawing.Point(69, 7);
            this.m_lblProjectFeedbackCount.Name = "m_lblProjectFeedbackCount";
            this.m_lblProjectFeedbackCount.Size = new System.Drawing.Size(14, 13);
            this.m_lblProjectFeedbackCount.StyleController = this.layoutControl3;
            this.m_lblProjectFeedbackCount.TabIndex = 8;
            this.m_lblProjectFeedbackCount.Text = "55";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(7, 7);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(51, 13);
            this.labelControl8.StyleController = this.layoutControl3;
            this.labelControl8.TabIndex = 4;
            this.labelControl8.Text = "Feedbacks";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem10});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup1";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(92, 27);
            this.layoutControlGroup3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Text = "layoutControlGroup1";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.labelControl8;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem1";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem6.Size = new System.Drawing.Size(62, 25);
            this.layoutControlItem6.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem6.Text = "layoutControlItem1";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.m_lblProjectFeedbackCount;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem10.Location = new System.Drawing.Point(62, 0);
            this.layoutControlItem10.Name = "layoutControlItem2";
            this.layoutControlItem10.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem10.Size = new System.Drawing.Size(28, 25);
            this.layoutControlItem10.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem10.Text = "layoutControlItem2";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.AutoScroll = false;
            this.layoutControl1.Controls.Add(this.m_lblLeadsOpened);
            this.layoutControl1.Controls.Add(this.m_lblLeadsPending);
            this.layoutControl1.Controls.Add(this.labelControl3);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Location = new System.Drawing.Point(772, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(118, 27);
            this.m_toolTipDashboard.SetSuperTip(this.layoutControl1, null);
            this.layoutControl1.TabIndex = 58;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // m_lblLeadsOpened
            // 
            this.m_lblLeadsOpened.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.m_lblLeadsOpened.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_lblLeadsOpened.Appearance.ForeColor = System.Drawing.Color.Red;
            this.m_lblLeadsOpened.Appearance.Options.UseBackColor = true;
            this.m_lblLeadsOpened.Appearance.Options.UseFont = true;
            this.m_lblLeadsOpened.Appearance.Options.UseForeColor = true;
            this.m_lblLeadsOpened.Location = new System.Drawing.Point(46, 7);
            this.m_lblLeadsOpened.Name = "m_lblLeadsOpened";
            this.m_lblLeadsOpened.Size = new System.Drawing.Size(14, 13);
            this.m_lblLeadsOpened.StyleController = this.layoutControl1;
            this.m_lblLeadsOpened.TabIndex = 8;
            this.m_lblLeadsOpened.Text = "55";
            // 
            // m_lblLeadsPending
            // 
            this.m_lblLeadsPending.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.m_lblLeadsPending.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_lblLeadsPending.Appearance.ForeColor = System.Drawing.Color.MediumBlue;
            this.m_lblLeadsPending.Appearance.Options.UseBackColor = true;
            this.m_lblLeadsPending.Appearance.Options.UseFont = true;
            this.m_lblLeadsPending.Appearance.Options.UseForeColor = true;
            this.m_lblLeadsPending.Location = new System.Drawing.Point(86, 7);
            this.m_lblLeadsPending.Name = "m_lblLeadsPending";
            this.m_lblLeadsPending.Size = new System.Drawing.Size(14, 13);
            this.m_lblLeadsPending.StyleController = this.layoutControl1;
            this.m_lblLeadsPending.TabIndex = 7;
            this.m_lblLeadsPending.Text = "33";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(71, 7);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(4, 13);
            this.labelControl3.StyleController = this.layoutControl1;
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "/";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Leads";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(118, 27);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.labelControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(39, 25);
            this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.labelControl3;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(64, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(15, 25);
            this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.m_lblLeadsPending;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(79, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem4.Size = new System.Drawing.Size(37, 25);
            this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.m_lblLeadsOpened;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(39, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(25, 25);
            this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // m_lblDShortcut
            // 
            this.m_lblDShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblDShortcut.Location = new System.Drawing.Point(601, 12);
            this.m_lblDShortcut.Name = "m_lblDShortcut";
            this.m_lblDShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblDShortcut.TabIndex = 3;
            this.m_lblDShortcut.Text = "&D shortcut";
            // 
            // m_dtpDashboardDate
            // 
            this.m_dtpDashboardDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtpDashboardDate.EditValue = new System.DateTime(2008, 9, 16, 0, 0, 0, 0);
            this.m_dtpDashboardDate.Location = new System.Drawing.Point(649, 7);
            this.m_dtpDashboardDate.Name = "m_dtpDashboardDate";
            this.m_dtpDashboardDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.m_dtpDashboardDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpDashboardDate.Properties.ValidateOnEnterKey = true;
            this.m_dtpDashboardDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpDashboardDate.Size = new System.Drawing.Size(103, 20);
            this.m_dtpDashboardDate.TabIndex = 4;
            // 
            // m_lblCurrentTime
            // 
            this.m_lblCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCurrentTime.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblCurrentTime.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(133)))), ((int)(((byte)(111)))));
            this.m_lblCurrentTime.Appearance.Options.UseFont = true;
            this.m_lblCurrentTime.Appearance.Options.UseForeColor = true;
            this.m_lblCurrentTime.Location = new System.Drawing.Point(1025, 8);
            this.m_lblCurrentTime.Name = "m_lblCurrentTime";
            this.m_lblCurrentTime.Size = new System.Drawing.Size(77, 19);
            this.m_lblCurrentTime.TabIndex = 7;
            this.m_lblCurrentTime.Text = "13:00 AM";
            // 
            // m_btnPrevDate
            // 
            this.m_btnPrevDate.Location = new System.Drawing.Point(12, 7);
            this.m_btnPrevDate.Name = "m_btnPrevDate";
            this.m_btnPrevDate.Size = new System.Drawing.Size(39, 23);
            this.m_btnPrevDate.TabIndex = 50;
            this.m_btnPrevDate.TabStop = false;
            this.m_btnPrevDate.Text = "<";
            // 
            // m_btnNextDate
            // 
            this.m_btnNextDate.Location = new System.Drawing.Point(57, 7);
            this.m_btnNextDate.Name = "m_btnNextDate";
            this.m_btnNextDate.Size = new System.Drawing.Size(39, 23);
            this.m_btnNextDate.TabIndex = 51;
            this.m_btnNextDate.TabStop = false;
            this.m_btnNextDate.Text = ">";
            // 
            // m_lblDashboardDate
            // 
            this.m_lblDashboardDate.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblDashboardDate.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(133)))), ((int)(((byte)(111)))));
            this.m_lblDashboardDate.Appearance.Options.UseFont = true;
            this.m_lblDashboardDate.Appearance.Options.UseForeColor = true;
            this.m_lblDashboardDate.Location = new System.Drawing.Point(105, 8);
            this.m_lblDashboardDate.Name = "m_lblDashboardDate";
            this.m_lblDashboardDate.Size = new System.Drawing.Size(226, 19);
            this.m_lblDashboardDate.TabIndex = 4;
            this.m_lblDashboardDate.Text = "12 December, 2007 (Today)";
            // 
            // m_lblDayTotalAmount
            // 
            this.m_lblDayTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblDayTotalAmount.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblDayTotalAmount.Appearance.ForeColor = System.Drawing.Color.Black;
            this.m_lblDayTotalAmount.Appearance.Options.UseFont = true;
            this.m_lblDayTotalAmount.Appearance.Options.UseForeColor = true;
            this.m_lblDayTotalAmount.Location = new System.Drawing.Point(577, 8);
            this.m_lblDayTotalAmount.Name = "m_lblDayTotalAmount";
            this.m_lblDayTotalAmount.Size = new System.Drawing.Size(52, 16);
            this.m_lblDayTotalAmount.TabIndex = 57;
            this.m_lblDayTotalAmount.Text = "$560.00";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.m_lblFShortcut);
            this.panelControl3.Controls.Add(this.m_gridPendingVisits);
            this.panelControl3.Controls.Add(this.m_lblCShortcut);
            this.panelControl3.Controls.Add(this.m_btnNewVisit);
            this.panelControl3.Controls.Add(this.m_txtPendingVisitCustomerFilter);
            this.panelControl3.Controls.Add(this.m_cmbPendingVisitsFilter);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 478);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1114, 211);
            this.m_toolTipDashboard.SetSuperTip(this.panelControl3, null);
            this.panelControl3.TabIndex = 0;
            // 
            // m_lblFShortcut
            // 
            this.m_lblFShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblFShortcut.Location = new System.Drawing.Point(777, 9);
            this.m_lblFShortcut.Name = "m_lblFShortcut";
            this.m_lblFShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblFShortcut.TabIndex = 9;
            this.m_lblFShortcut.Text = "&F shortcut";
            // 
            // m_gridPendingVisits
            // 
            this.m_gridPendingVisits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridPendingVisits.EmbeddedNavigator.Name = "";
            this.m_gridPendingVisits.Location = new System.Drawing.Point(0, 32);
            this.m_gridPendingVisits.MainView = this.m_gridPendingVisitsView;
            this.m_gridPendingVisits.Name = "m_gridPendingVisits";
            this.m_gridPendingVisits.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.m_linkGridVisit,
            this.m_linkGridPendingPrint});
            this.m_gridPendingVisits.ShowOnlyPredefinedDetails = true;
            this.m_gridPendingVisits.Size = new System.Drawing.Size(1114, 179);
            this.m_gridPendingVisits.TabIndex = 12;
            this.m_gridPendingVisits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridPendingVisitsView});
            // 
            // m_gridPendingVisitsView
            // 
            this.m_gridPendingVisitsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_columnVisitNumber,
            this.m_columnReady,
            this.m_columnCustomer,
            this.m_columnMap,
            this.m_columnAddress,
            this.m_columnTaskType,
            this.m_columnServiceDate,
            this.m_columnTimeFrame,
            this.m_notes,
            this.m_colPendingMore});
            this.m_gridPendingVisitsView.GridControl = this.m_gridPendingVisits;
            this.m_gridPendingVisitsView.Name = "m_gridPendingVisitsView";
            this.m_gridPendingVisitsView.OptionsCustomization.AllowFilter = false;
            this.m_gridPendingVisitsView.OptionsCustomization.AllowGroup = false;
            this.m_gridPendingVisitsView.OptionsNavigation.UseTabKey = false;
            this.m_gridPendingVisitsView.OptionsView.ShowGroupPanel = false;
            // 
            // m_columnVisitNumber
            // 
            this.m_columnVisitNumber.Caption = "Visit";
            this.m_columnVisitNumber.ColumnEdit = this.m_linkGridVisit;
            this.m_columnVisitNumber.FieldName = "VisitNumber";
            this.m_columnVisitNumber.Name = "m_columnVisitNumber";
            this.m_columnVisitNumber.Visible = true;
            this.m_columnVisitNumber.VisibleIndex = 0;
            this.m_columnVisitNumber.Width = 42;
            // 
            // m_linkGridVisit
            // 
            this.m_linkGridVisit.AutoHeight = false;
            this.m_linkGridVisit.Name = "m_linkGridVisit";
            // 
            // m_columnReady
            // 
            this.m_columnReady.Caption = "Ready";
            this.m_columnReady.FieldName = "IsReady";
            this.m_columnReady.Name = "m_columnReady";
            this.m_columnReady.OptionsColumn.AllowEdit = false;
            this.m_columnReady.Visible = true;
            this.m_columnReady.VisibleIndex = 1;
            this.m_columnReady.Width = 38;
            // 
            // m_columnCustomer
            // 
            this.m_columnCustomer.Caption = "Customer";
            this.m_columnCustomer.FieldName = "CustomerName";
            this.m_columnCustomer.Name = "m_columnCustomer";
            this.m_columnCustomer.OptionsColumn.AllowEdit = false;
            this.m_columnCustomer.Visible = true;
            this.m_columnCustomer.VisibleIndex = 2;
            this.m_columnCustomer.Width = 83;
            // 
            // m_columnMap
            // 
            this.m_columnMap.Caption = "Map";
            this.m_columnMap.FieldName = "Map";
            this.m_columnMap.Name = "m_columnMap";
            this.m_columnMap.OptionsColumn.AllowEdit = false;
            this.m_columnMap.Visible = true;
            this.m_columnMap.VisibleIndex = 3;
            this.m_columnMap.Width = 40;
            // 
            // m_columnAddress
            // 
            this.m_columnAddress.Caption = "Address";
            this.m_columnAddress.FieldName = "ServiceAddressString";
            this.m_columnAddress.Name = "m_columnAddress";
            this.m_columnAddress.OptionsColumn.AllowEdit = false;
            this.m_columnAddress.Visible = true;
            this.m_columnAddress.VisibleIndex = 4;
            this.m_columnAddress.Width = 170;
            // 
            // m_columnTaskType
            // 
            this.m_columnTaskType.Caption = "Task Types";
            this.m_columnTaskType.FieldName = "TaskTypes";
            this.m_columnTaskType.Name = "m_columnTaskType";
            this.m_columnTaskType.OptionsColumn.AllowEdit = false;
            this.m_columnTaskType.Visible = true;
            this.m_columnTaskType.VisibleIndex = 5;
            this.m_columnTaskType.Width = 136;
            // 
            // m_columnServiceDate
            // 
            this.m_columnServiceDate.Caption = "Service Date";
            this.m_columnServiceDate.DisplayFormat.FormatString = "d";
            this.m_columnServiceDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.m_columnServiceDate.FieldName = "ServiceDate";
            this.m_columnServiceDate.Name = "m_columnServiceDate";
            this.m_columnServiceDate.OptionsColumn.AllowEdit = false;
            this.m_columnServiceDate.Visible = true;
            this.m_columnServiceDate.VisibleIndex = 6;
            this.m_columnServiceDate.Width = 65;
            // 
            // m_columnTimeFrame
            // 
            this.m_columnTimeFrame.Caption = "Pref. Time Frame";
            this.m_columnTimeFrame.FieldName = "TimeFrame";
            this.m_columnTimeFrame.Name = "m_columnTimeFrame";
            this.m_columnTimeFrame.OptionsColumn.AllowEdit = false;
            this.m_columnTimeFrame.Visible = true;
            this.m_columnTimeFrame.VisibleIndex = 7;
            this.m_columnTimeFrame.Width = 69;
            // 
            // m_notes
            // 
            this.m_notes.Caption = "Notes";
            this.m_notes.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.m_notes.FieldName = "Notes";
            this.m_notes.Name = "m_notes";
            this.m_notes.Visible = true;
            this.m_notes.VisibleIndex = 8;
            this.m_notes.Width = 36;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // m_colPendingMore
            // 
            this.m_colPendingMore.ColumnEdit = this.m_linkGridPendingPrint;
            this.m_colPendingMore.FieldName = "PrintLabel";
            this.m_colPendingMore.Name = "m_colPendingMore";
            this.m_colPendingMore.OptionsColumn.AllowEdit = false;
            this.m_colPendingMore.Visible = true;
            this.m_colPendingMore.VisibleIndex = 9;
            this.m_colPendingMore.Width = 26;
            // 
            // m_linkGridPendingPrint
            // 
            this.m_linkGridPendingPrint.AutoHeight = false;
            this.m_linkGridPendingPrint.Name = "m_linkGridPendingPrint";
            // 
            // m_lblCShortcut
            // 
            this.m_lblCShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblCShortcut.Location = new System.Drawing.Point(616, 9);
            this.m_lblCShortcut.Name = "m_lblCShortcut";
            this.m_lblCShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblCShortcut.TabIndex = 7;
            this.m_lblCShortcut.Text = "&C shortcut";
            // 
            // m_btnNewVisit
            // 
            this.m_btnNewVisit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnNewVisit.Location = new System.Drawing.Point(758, 4);
            this.m_btnNewVisit.Name = "m_btnNewVisit";
            this.m_btnNewVisit.Size = new System.Drawing.Size(67, 23);
            this.m_btnNewVisit.TabIndex = 6;
            this.m_btnNewVisit.Text = "&New Visit";
            // 
            // m_txtPendingVisitCustomerFilter
            // 
            this.m_txtPendingVisitCustomerFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtPendingVisitCustomerFilter.Location = new System.Drawing.Point(831, 6);
            this.m_txtPendingVisitCustomerFilter.Name = "m_txtPendingVisitCustomerFilter";
            this.m_txtPendingVisitCustomerFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.m_txtPendingVisitCustomerFilter.Size = new System.Drawing.Size(143, 20);
            this.m_txtPendingVisitCustomerFilter.TabIndex = 8;
            // 
            // m_cmbPendingVisitsFilter
            // 
            this.m_cmbPendingVisitsFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbPendingVisitsFilter.EditValue = 1;
            this.m_cmbPendingVisitsFilter.Location = new System.Drawing.Point(980, 6);
            this.m_cmbPendingVisitsFilter.Name = "m_cmbPendingVisitsFilter";
            this.m_cmbPendingVisitsFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbPendingVisitsFilter.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Current", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tomorrow", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Day After Tomorrow", 3, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Future", 4, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Unscheduled", 5, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("All", 6, -1)});
            this.m_cmbPendingVisitsFilter.Size = new System.Drawing.Size(130, 20);
            this.m_cmbPendingVisitsFilter.TabIndex = 10;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl2.AutoScroll = false;
            this.layoutControl2.Controls.Add(this.labelControl2);
            this.layoutControl2.Controls.Add(this.labelControl6);
            this.layoutControl2.Location = new System.Drawing.Point(543, 3);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(118, 27);
            this.m_toolTipDashboard.SetSuperTip(this.layoutControl2, null);
            this.layoutControl2.TabIndex = 58;
            this.layoutControl2.Text = "layoutControl1";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(7, 31);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(14, 13);
            this.labelControl2.StyleController = this.layoutControl2;
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "55";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(7, 7);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(28, 13);
            this.labelControl6.StyleController = this.layoutControl2;
            this.labelControl6.TabIndex = 4;
            this.labelControl6.Text = "Leads";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup1";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(118, 50);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Text = "layoutControlGroup1";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.labelControl2;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem8.Name = "layoutControlItem2";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem8.Size = new System.Drawing.Size(116, 24);
            this.layoutControlItem8.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem8.Text = "layoutControlItem2";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.labelControl6;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem1";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem5.Size = new System.Drawing.Size(116, 24);
            this.layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem5.Text = "layoutControlItem1";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // m_timerCurrentTime
            // 
            this.m_timerCurrentTime.Enabled = true;
            this.m_timerCurrentTime.Interval = 1000;
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1)});
            this.popupMenu1.Manager = this.m_barManager;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // m_appointmentImages
            // 
            this.m_appointmentImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_appointmentImages.ImageStream")));
            this.m_appointmentImages.TransparentColor = System.Drawing.Color.Magenta;
            this.m_appointmentImages.Images.SetKeyName(0, "phone7.bmp");
            this.m_appointmentImages.Images.SetKeyName(1, "tape.bmp");
            this.m_appointmentImages.Images.SetKeyName(2, "busy.bmp");
            this.m_appointmentImages.Images.SetKeyName(3, "CallOnTheWay.bmp");
            this.m_appointmentImages.Images.SetKeyName(4, "phone_blue.bmp");
            // 
            // m_menuPendingVisitAction
            // 
            this.m_menuPendingVisitAction.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuPendingVisitPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuPendingVisitSplit),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuPendingVisitNewVisit)});
            this.m_menuPendingVisitAction.Manager = this.m_barManager;
            this.m_menuPendingVisitAction.Name = "m_menuPendingVisitAction";
            // 
            // m_timerBlinking
            // 
            this.m_timerBlinking.Interval = 1000;
            // 
            // DashboardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "DashboardView";
            this.Size = new System.Drawing.Size(1114, 689);
            this.m_toolTipDashboard.SetSuperTip(this, null);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboardStorage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridPendingVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridPendingVisitsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridVisit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_linkGridPendingPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPendingVisitCustomerFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbPendingVisitsFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuPendingVisitAction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.Panel panel1;
        internal DevExpress.XtraScheduler.SchedulerStorage m_dashboardStorage;
        internal DevExpress.Utils.ToolTipController m_toolTipDashboard;
        internal DevExpress.XtraGrid.GridControl m_gridPendingVisits;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnVisitNumber;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnCustomer;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnMap;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridPendingVisitsView;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnAddress;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnTaskType;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnServiceDate;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal Scheduler m_dashboard;
        internal DevExpress.XtraEditors.LabelControl m_lblDashboardDate;
        internal DevExpress.XtraEditors.SimpleButton m_btnNextDate;
        internal DevExpress.XtraEditors.SimpleButton m_btnPrevDate;
        internal DevExpress.XtraEditors.LabelControl m_lblCurrentTime;
        internal System.Windows.Forms.Timer m_timerCurrentTime;
        private DevExpress.XtraGrid.Columns.GridColumn m_notes;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        internal DevExpress.XtraBars.BarManager m_barManager;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridVisit;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnTimeFrame;
        internal ImageComboBoxEdit m_cmbPendingVisitsFilter;
        internal DevExpress.XtraGrid.Columns.GridColumn m_columnReady;
        internal System.Windows.Forms.ImageList m_images;
        internal System.Windows.Forms.ImageList m_appointmentImages;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colPendingMore;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_linkGridPendingPrint;
        internal DevExpress.XtraBars.PopupMenu m_menuPendingVisitAction;
        internal DevExpress.XtraBars.BarButtonItem m_menuPendingVisitPrint;
        internal DevExpress.XtraBars.BarButtonItem m_menuPendingVisitSplit;
        internal SimpleButton m_btnNewVisit;
        internal ButtonEdit m_txtPendingVisitCustomerFilter;
        private PanelControl panelControl3;
        private PanelControl panelControl2;
        private SplitterControl splitterControl1;
        internal DateEdit m_dtpDashboardDate;
        private LabelControl m_lblFShortcut;
        private LabelControl m_lblCShortcut;
        private LabelControl m_lblDShortcut;
        internal LabelControl m_lblDayTotalAmount;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        internal DevExpress.XtraBars.BarButtonItem m_menuPendingVisitNewVisit;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private LabelControl labelControl3;
        private LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        internal LabelControl m_lblLeadsOpened;
        internal LabelControl m_lblLeadsPending;
        internal System.Windows.Forms.Timer m_timerBlinking;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        internal LabelControl labelControl2;
        private LabelControl labelControl6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        internal LabelControl m_lblProjectFeedbackCount;
        private LabelControl labelControl8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        internal LabelControl m_lblDispatcher;
    }
}