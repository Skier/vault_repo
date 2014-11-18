
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
            this.m_spinTechCount = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.m_spinDaysCount = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dateNavigator1 = new DevExpress.XtraScheduler.DateNavigator();
            this.m_dashboard = new DevExpress.XtraScheduler.SchedulerControl();
            this.m_dashboardStorage = new DevExpress.XtraScheduler.SchedulerStorage(this.components);
            this.m_btnAddRandomMany = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblTechLoad = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.m_txtMaxDuration = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnAddRandom = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnRestore = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnAddVisit = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinTechCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinDaysCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboardStorage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMaxDuration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_spinTechCount);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.m_spinDaysCount);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.dateNavigator1);
            this.panelControl1.Controls.Add(this.m_btnAddRandomMany);
            this.panelControl1.Controls.Add(this.m_lblTechLoad);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_btnClear);
            this.panelControl1.Controls.Add(this.m_txtMaxDuration);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_btnAddRandom);
            this.panelControl1.Controls.Add(this.m_btnRestore);
            this.panelControl1.Controls.Add(this.m_btnSave);
            this.panelControl1.Controls.Add(this.m_dashboard);
            this.panelControl1.Controls.Add(this.m_btnAddVisit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(684, 678);
            this.panelControl1.TabIndex = 0;
            // 
            // m_spinTechCount
            // 
            this.m_spinTechCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_spinTechCount.EditValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.m_spinTechCount.Location = new System.Drawing.Point(606, 196);
            this.m_spinTechCount.Name = "m_spinTechCount";
            this.m_spinTechCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_spinTechCount.Properties.IsFloatValue = false;
            this.m_spinTechCount.Properties.Mask.EditMask = "N00";
            this.m_spinTechCount.Properties.MaxValue = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.m_spinTechCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_spinTechCount.Size = new System.Drawing.Size(75, 20);
            this.m_spinTechCount.TabIndex = 20;
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Location = new System.Drawing.Point(505, 199);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 13);
            this.labelControl4.TabIndex = 19;
            this.labelControl4.Text = "Tech Count";
            // 
            // m_spinDaysCount
            // 
            this.m_spinDaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_spinDaysCount.EditValue = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.m_spinDaysCount.Location = new System.Drawing.Point(606, 170);
            this.m_spinDaysCount.Name = "m_spinDaysCount";
            this.m_spinDaysCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_spinDaysCount.Properties.IsFloatValue = false;
            this.m_spinDaysCount.Properties.Mask.EditMask = "N00";
            this.m_spinDaysCount.Properties.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_spinDaysCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_spinDaysCount.Size = new System.Drawing.Size(75, 20);
            this.m_spinDaysCount.TabIndex = 18;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Location = new System.Drawing.Point(505, 173);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(56, 13);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Days Count";
            // 
            // dateNavigator1
            // 
            this.dateNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateNavigator1.Location = new System.Drawing.Point(502, 222);
            this.dateNavigator1.Name = "dateNavigator1";
            this.dateNavigator1.SchedulerControl = this.m_dashboard;
            this.dateNavigator1.Size = new System.Drawing.Size(179, 165);
            this.dateNavigator1.TabIndex = 15;
            this.dateNavigator1.View = DevExpress.XtraEditors.Controls.DateEditCalendarViewType.MonthInfo;
            // 
            // m_dashboard
            // 
            this.m_dashboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dashboard.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
            this.m_dashboard.Location = new System.Drawing.Point(0, 0);
            this.m_dashboard.Name = "m_dashboard";
            this.m_dashboard.OptionsCustomization.AllowAppointmentCopy = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.m_dashboard.OptionsCustomization.AllowAppointmentCreate = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.m_dashboard.OptionsCustomization.AllowAppointmentMultiSelect = false;
            this.m_dashboard.OptionsCustomization.AllowDisplayAppointmentForm = DevExpress.XtraScheduler.AllowDisplayAppointmentForm.Never;
            this.m_dashboard.OptionsCustomization.AllowInplaceEditor = DevExpress.XtraScheduler.UsedAppointmentType.None;
            schedulerColorSchema1.Cell = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(188)))));
            schedulerColorSchema1.CellBorder = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(228)))), ((int)(((byte)(177)))));
            schedulerColorSchema1.CellBorderDark = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(208)))), ((int)(((byte)(152)))));
            schedulerColorSchema1.CellLight = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(213)))));
            schedulerColorSchema1.CellLightBorder = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(199)))));
            schedulerColorSchema1.CellLightBorderDark = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(219)))), ((int)(((byte)(162)))));
            this.m_dashboard.ResourceColorSchemas.Add(schedulerColorSchema1);
            this.m_dashboard.Size = new System.Drawing.Size(499, 678);
            this.m_dashboard.Start = new System.DateTime(2009, 3, 18, 0, 0, 0, 0);
            this.m_dashboard.Storage = this.m_dashboardStorage;
            this.m_dashboard.TabIndex = 4;
            this.m_dashboard.Text = "schedulerControl1";
            this.m_dashboard.Views.DayView.Appearance.ResourceHeaderCaption.Options.UseTextOptions = true;
            this.m_dashboard.Views.DayView.Appearance.ResourceHeaderCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.m_dashboard.Views.DayView.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Always;
            this.m_dashboard.Views.DayView.AppointmentDisplayOptions.SnapToCellsMode = DevExpress.XtraScheduler.AppointmentSnapToCellsMode.Never;
            this.m_dashboard.Views.DayView.AppointmentDisplayOptions.StartTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Always;
            this.m_dashboard.Views.DayView.NavigationButtonVisibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never;
            this.m_dashboard.Views.DayView.ShowAllDayArea = false;
            this.m_dashboard.Views.DayView.ShowDayHeaders = false;
            this.m_dashboard.Views.DayView.TimeRulers.Add(timeRuler1);
            this.m_dashboard.Views.DayView.TimeScale = System.TimeSpan.Parse("00:15:00");
            this.m_dashboard.Views.DayView.VisibleTime.End = System.TimeSpan.Parse("21:00:00");
            this.m_dashboard.Views.DayView.VisibleTime.Start = System.TimeSpan.Parse("06:00:00");
            this.m_dashboard.Views.DayView.WorkTime.Start = System.TimeSpan.Parse("07:00:00");
            this.m_dashboard.Views.MonthView.Enabled = false;
            this.m_dashboard.Views.TimelineView.Enabled = false;
            this.m_dashboard.Views.WeekView.Enabled = false;
            this.m_dashboard.Views.WorkWeekView.Enabled = false;
            this.m_dashboard.Views.WorkWeekView.TimeRulers.Add(timeRuler2);
            // 
            // m_dashboardStorage
            // 
            this.m_dashboardStorage.Appointments.Mappings.Description = "Caption";
            this.m_dashboardStorage.Appointments.Mappings.End = "TimeEnd";
            this.m_dashboardStorage.Appointments.Mappings.ResourceId = "TechnicianId";
            this.m_dashboardStorage.Appointments.Mappings.Start = "TimeStart";
            this.m_dashboardStorage.Appointments.Mappings.Status = "StatusId";
            this.m_dashboardStorage.Resources.Mappings.Caption = "Caption";
            this.m_dashboardStorage.Resources.Mappings.Id = "Id";
            // 
            // m_btnAddRandomMany
            // 
            this.m_btnAddRandomMany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAddRandomMany.Location = new System.Drawing.Point(606, 96);
            this.m_btnAddRandomMany.Name = "m_btnAddRandomMany";
            this.m_btnAddRandomMany.Size = new System.Drawing.Size(75, 23);
            this.m_btnAddRandomMany.TabIndex = 14;
            this.m_btnAddRandomMany.Text = "Add Rnd 2";
            // 
            // m_lblTechLoad
            // 
            this.m_lblTechLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTechLoad.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblTechLoad.Appearance.Options.UseFont = true;
            this.m_lblTechLoad.Location = new System.Drawing.Point(630, 411);
            this.m_lblTechLoad.Name = "m_lblTechLoad";
            this.m_lblTechLoad.Size = new System.Drawing.Size(21, 39);
            this.m_lblTechLoad.TabIndex = 13;
            this.m_lblTechLoad.Text = "90\r\n120\r\n60";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(630, 392);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(49, 13);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "Tech Load";
            // 
            // m_btnClear
            // 
            this.m_btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClear.Location = new System.Drawing.Point(606, 3);
            this.m_btnClear.Name = "m_btnClear";
            this.m_btnClear.Size = new System.Drawing.Size(75, 23);
            this.m_btnClear.TabIndex = 11;
            this.m_btnClear.Text = "Clear";
            // 
            // m_txtMaxDuration
            // 
            this.m_txtMaxDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtMaxDuration.EditValue = "120";
            this.m_txtMaxDuration.Location = new System.Drawing.Point(606, 70);
            this.m_txtMaxDuration.Name = "m_txtMaxDuration";
            this.m_txtMaxDuration.Properties.Mask.EditMask = "\\d{1,6}";
            this.m_txtMaxDuration.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtMaxDuration.Size = new System.Drawing.Size(73, 20);
            this.m_txtMaxDuration.TabIndex = 10;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(505, 73);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Max Duration";
            // 
            // m_btnAddRandom
            // 
            this.m_btnAddRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAddRandom.Location = new System.Drawing.Point(505, 96);
            this.m_btnAddRandom.Name = "m_btnAddRandom";
            this.m_btnAddRandom.Size = new System.Drawing.Size(75, 23);
            this.m_btnAddRandom.TabIndex = 7;
            this.m_btnAddRandom.Text = "Add Rnd";
            // 
            // m_btnRestore
            // 
            this.m_btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRestore.Location = new System.Drawing.Point(606, 125);
            this.m_btnRestore.Name = "m_btnRestore";
            this.m_btnRestore.Size = new System.Drawing.Size(75, 23);
            this.m_btnRestore.TabIndex = 6;
            this.m_btnRestore.Text = "Restore";
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.Location = new System.Drawing.Point(505, 125);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(75, 23);
            this.m_btnSave.TabIndex = 5;
            this.m_btnSave.Text = "Save";
            // 
            // m_btnAddVisit
            // 
            this.m_btnAddVisit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAddVisit.Location = new System.Drawing.Point(505, 3);
            this.m_btnAddVisit.Name = "m_btnAddVisit";
            this.m_btnAddVisit.Size = new System.Drawing.Size(75, 23);
            this.m_btnAddVisit.TabIndex = 2;
            this.m_btnAddVisit.Text = "Add";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // MainFormView
            // 
            this.AcceptButton = this.m_btnAddVisit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 678);
            this.Controls.Add(this.panelControl1);
            this.Name = "MainFormView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainFormView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinTechCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinDaysCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dashboardStorage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMaxDuration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnAddVisit;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal DevExpress.XtraScheduler.SchedulerControl m_dashboard;
        internal DevExpress.XtraScheduler.SchedulerStorage m_dashboardStorage;
        internal DevExpress.XtraEditors.SimpleButton m_btnRestore;
        internal DevExpress.XtraEditors.SimpleButton m_btnSave;
        internal DevExpress.XtraEditors.SimpleButton m_btnAddRandom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.TextEdit m_txtMaxDuration;
        internal DevExpress.XtraEditors.SimpleButton m_btnClear;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.LabelControl m_lblTechLoad;
        internal DevExpress.XtraEditors.SimpleButton m_btnAddRandomMany;
        private DevExpress.XtraScheduler.DateNavigator dateNavigator1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.SpinEdit m_spinDaysCount;
        internal DevExpress.XtraEditors.SpinEdit m_spinTechCount;
        private DevExpress.XtraEditors.LabelControl labelControl4;

    }
}