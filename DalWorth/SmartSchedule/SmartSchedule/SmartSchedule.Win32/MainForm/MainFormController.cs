using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net.Mime;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraTab;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.Servman;
using SmartSchedule.Domain.Sync;
using SmartSchedule.Domain.WCF;
using SmartSchedule.SDK;
using SmartSchedule.Win32.ActionReport;
using SmartSchedule.Win32.Authenticate;
using SmartSchedule.Win32.BlockoutCreate;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Win32.DelayedVisitProcess;
using SmartSchedule.Win32.HistoricalOrders;
using SmartSchedule.Win32.RouteAnalyze;
using SmartSchedule.Win32.SnapshotChange;
using SmartSchedule.Win32.TechnicianArrangement;
using SmartSchedule.Win32.TechnicianEdit;
using SmartSchedule.Win32.UserManage;
using SmartSchedule.Win32.VisitAdd;
using SmartSchedule.Win32.WcfClient;
using SmartSchedule.Windows;
using CustomDrawObjectEventArgs=DevExpress.XtraScheduler.CustomDrawObjectEventArgs;
using Point=System.Drawing.Point;
using TimeInterval=SmartSchedule.Domain.TimeInterval;
using System.Linq;

namespace SmartSchedule.Win32.MainForm
{
    public class MainFormController : Controller<MainFormModel, MainFormView>
    {
        private List<Visit> m_visitsToRefreshStatuses;
        private bool m_isUserChangingAppointment;
        private TechnicianSearchInfo m_lastSearchInfo;

        #region Form

        public Form Form
        {
            get { return View; }
        }

        #endregion

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region BucketVisits

        private List<Visit> BucketVisits
        {
            get
            {
                return ((BindingList<Visit>) View.m_gridDelayedVisits.DataSource).ToList();
            }
        }

        #endregion
       
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnTechnicianReorder.Click += OnTechnicianArrangeClick;
            
            View.m_dashboard.AppointmentDrag += OnAppointmentDrag;            
            View.m_dashboardStorage.AppointmentsChanged += OnDashboardAppointmentsChanged;
            View.m_dashboardStorage.AppointmentDeleting += OnDashboardAppointmentDeleting;      
            View.m_dashboard.AppointmentResized += OnDashboardAppointmentResized;
            View.m_dashboard.AppointmentResizing += OnDashboardAppointmentResizing;
            View.m_dashboard.CustomDrawTimeCell += OnDashboardCustomDrawTimeCell;
            View.m_dashboard.DoubleClick += OnDashboardDoubleClick;

            View.m_tooltipController.BeforeShow += OnTooltipBeforeShow;
            View.m_tooltipController.GetActiveObjectInfo += OnTooltipControllerGetActiveObjectInfo;

            View.m_btnFindTechnician.Click += OnFindTechnicianClick;
            View.m_btnFindBadRoute.Click += OnFindBadRouteClick;
            View.m_btnFindZip.Click += OnFindZipClick;
            View.m_btnFindVisit.Click += OnFindVisitClick;
            View.m_cmbTechnician.KeyPress += OnTechnicianKeyPress;
            View.m_txtZip.KeyPress += OnZipKeyPress;
            View.m_txtVisitNumber.KeyPress += OnVisitNumberKeyPress;

            View.m_dashboard.PreparePopupMenu += OnDashboardPreparePopupMenu;
            View.m_gridDelayedVisitsView.RowCellStyle += OnGridRowCellStyle;
            View.m_gridTempAssignmentView.RowCellStyle += OnGridRowCellStyle;

            View.m_gridDelayedVisitsView.DoubleClick += OnGridVisitsDoubleClick;
            View.m_gridTempAssignmentView.DoubleClick += OnGridVisitsDoubleClick;

            View.m_lblBucketTempAssignmentCount.DoubleClick += OnBucketTempAssignmentCountDoubleClick;
            View.m_lblVisitsCountLabel.DoubleClick += OnOrdersClick;

            View.KeyDown += OnViewKeyDown;

            View.m_menuTechnicianDayOrder.ItemClick += OnMenuTechnicianArrangeClick;
            View.m_menuTechnicianDefaultOrder.ItemClick += OnMenuTechnicianArrangeClick;

            View.m_menuSetAsWorkingTime.Click += OnMenuSetAsWorkingTimeClick;
            View.m_menuSetAsFreeTime.Click += OnMenuSetAsFreeTimeClick;
            View.m_menuBlockOutTime.Click += OnMenuBlockOutTimeClick;

            View.m_menuEditTechnicianDaySettings.Click += OnMenuEditTechnicianSettingsClick;
            View.m_menuEditTechnicianDefaultSettings.Click += OnMenuEditTechnicianSettingsClick;

            View.m_menuVisitPrint.Click += OnMenuVisitPrintClick;
            View.m_menuTechnicianPrint.Click += OnMenuTechnicianPrintClick;    
            View.m_menuTechnicianShowRoute.Click += OnMenuTechnicianShowRouteClick;
        
            View.m_timer.Tick += OnPendingChangesTimerTick;
            View.m_timerKeepAlive.Tick += OnTimerKeepAliveTick;      
            View.m_dtpDashboardDate.DateTimeChanged += OnDashboardDateChanged;
            View.m_dtpDashboardDate.DoubleClick += OnDashboardDateDoubleClick;
            View.m_menuPrintAll.ItemClick += OnPrintAllClick;
            View.m_chkSuspendRecommendations.CheckedChanged += OnSuspendRecommendationsChenged;
            View.m_chkSuspendRecommendations.EditValueChanging += OnSuspendRecommendationsChanging;

            View.m_splitContainer.Panel2.SizeChanged += OnPanel2SizeChanged;
            View.SizeChanged += OnPanel2SizeChanged;
            View.m_tabs.SelectedPageChanged += OnSelectedTabPageChanged;
            View.m_chkBucketShowAllDates.CheckedChanged += OnBucketShowAllDatesChanged;

            View.m_btnLogOut.Click += OnLogOutClick;
            User.CurrentUserChanged += OnCurrentUserChanged;

            View.m_btnMisc.Click += OnMiscClick;
            View.m_menuUserActionReport.ItemClick += OnUserActionReportClick;
            View.m_menuUserManagement.ItemClick += OnUserManagementItemClick;
            View.m_menuAddTechnician.ItemClick += OnAddTechnicianClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {            
            m_visitsToRefreshStatuses = new List<Visit>();

            if (Configuration.IsRealtimeMode)
            {
                View.m_dtpDashboardDate.DateTime = DateTime.Now.Date;
                View.m_dtpDashboardDate.Properties.MinValue = DateTime.Now.Date;
            }                
            else
                View.m_dtpDashboardDate.DateTime = new DateTime(2009, 7, 13);            

            ResetUI();

            foreach (Technician technician in Model.GetTechnicians(true))
            {
                View.m_cmbTechnician.Properties.Items.Add(
                    new ImageComboBoxItem(technician.Name, (object)technician.TechnicianDefaultId));
            }

            View.m_timer.Start();
            OnCurrentUserChanged(User.Current);
        }

        #endregion

        #region OnTooltipControllerGetActiveObjectInfo

        private void OnTooltipControllerGetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs args)
        {
            if (args.Info == null && (args.SelectedControl == View.m_gridDelayedVisits
                    || args.SelectedControl == View.m_gridTempAssignment))
            {
                GridView gridView = (GridView)((GridControl)args.SelectedControl).FocusedView;
                GridHitInfo hitInfo = gridView.CalcHitInfo(args.ControlMousePosition);

                if (hitInfo.InRowCell)
                {
                    Visit visit = (Visit)hitInfo.Column.View.GetRow(hitInfo.RowHandle);
                    if (visit != null)
                    {
                        args.Info = new ToolTipControlInfo(
                            new CellToolTipInfo(hitInfo.RowHandle, hitInfo.Column, "cell"), visit.ToolTipText);                    
                    }                    
                }                    
            }
        }

        #endregion


        #region OnTooltipBeforeShow

        private void OnTooltipBeforeShow(object sender, ToolTipControllerShowEventArgs e)
        {
            if (e.SelectedObject == null)
            {
                e.Show = false;
                return;
            }

            if (e.SelectedObject is VerticalAppointmentViewInfo || e.SelectedObject is Appointment)
            {
                Appointment appointment;

                if (e.SelectedObject is VerticalAppointmentViewInfo)
                    appointment = (e.SelectedObject as VerticalAppointmentViewInfo).Appointment;
                else
                    appointment = (Appointment)e.SelectedObject;

                Visit visit = (Visit)appointment.GetRow(View.m_dashboardStorage);


                if (visit != null)
                {
                    e.IconType = ToolTipIconType.Information;
                    e.ToolTip = visit.ToolTipText;                    
                }
            }
            else if (e.SelectedObject is ResourceHeader)
            {
                ResourceHeader header = (ResourceHeader) e.SelectedObject;
                Technician technician = (Technician)header.Resource.GetRow(View.m_dashboardStorage);

                e.IconType = ToolTipIconType.Information;
                e.ToolTip = technician.GetToolTipText(GetCurrentCost(technician));
            }
        }

        #endregion

        #region Appointment Drag

        private void OnAppointmentDrag(object sender, AppointmentDragEventArgs e)
        {                
            Visit visit = (Visit)e.EditedAppointment.GetRow(View.m_dashboardStorage);

            if (!AuthenticateController.IsAccessAllowed(
                visit.IsBlockout ? UserRoleEnum.Supervisor : UserRoleEnum.Dispatrcher))
            {
                e.Allow = false;
                e.Handled = true;
                return;
            }

            if (!UpdateAppointmentStatus(e.EditedAppointment))
            {
                e.Allow = false;
                e.Handled = true;
            }
        }
        
        private Visit GetCloseNeighbour(Visit visit, bool searchUp)
        {
            if (!visit.TechnicianId.HasValue)
                return null;

            IList<Visit> technicianVisits = GetTechnicianVisits(visit.TechnicianId.Value);
            int currentVisitIndex = technicianVisits.IndexOf(visit);
            if (searchUp && currentVisitIndex > 0)
            {
                Visit previousVisit = technicianVisits[currentVisitIndex - 1];
                if (previousVisit.TimeEnd == visit.TimeStart && visit.IsCloseDrive(previousVisit))
                    return previousVisit;
            }
            else if (!searchUp && currentVisitIndex < technicianVisits.Count - 1)
            {
                Visit nextVisit = technicianVisits[currentVisitIndex + 1];
                if (nextVisit.TimeStart == visit.TimeEnd && visit.IsCloseDrive(nextVisit))
                    return nextVisit;
            }

            return null;
        }

        private IList<Visit> GetTechnicianVisits(int technicianId)
        {
            return Model.Visits.GetTechnicianVisits(technicianId);
        }

        private bool HasTechnicianNonBlockoutVisits(int technicianId)
        {
            IList<Visit> visits = GetTechnicianVisits(technicianId);
            foreach (var visit in visits)
            {
                if (!visit.IsBlockout)
                    return true;
            }

            return false;
        }

        private int GetVisitStatusId(Visit visit)
        {
            if (!visit.IsWithinAllowedInterval)
                return 3;

            if (GetCloseNeighbour(visit, false) != null || GetCloseNeighbour(visit, true) != null)
                return 2;

            return 1;            
        }

        //returns false if visit cannot be placed on particular technician
        private bool UpdateAppointmentStatus(Appointment editedAppointment)
        {
            Visit visit = (Visit)editedAppointment.GetRow(View.m_dashboardStorage);

            if (!m_isUserChangingAppointment)
            {
                m_isUserChangingAppointment = true;
                Visit prevClose = GetCloseNeighbour(visit, true);
                Visit nextClose = GetCloseNeighbour(visit, false);

                if (prevClose != null)
                    m_visitsToRefreshStatuses.Add(prevClose);
                if (nextClose != null)
                    m_visitsToRefreshStatuses.Add(nextClose);
            }

            int newTechnicianId;
            try
            {
                newTechnicianId = (int) editedAppointment.ResourceId;
            }
            catch (Exception)
            {
                return false;
            }

            if (newTechnicianId != visit.TechnicianId)
            {
                if (!visit.IsTechnicianAllowed(Technician.GetTechnician(newTechnicianId)))
                    return false;
            }

            visit.TimeStart = editedAppointment.Start;
            visit.TimeEnd = editedAppointment.End;
            visit.TechnicianId = newTechnicianId;
            visit.UpdateTimeEndByTechnician(Technician.GetTechnician(newTechnicianId));

            editedAppointment.StatusId = GetVisitStatusId(visit);
            editedAppointment.Description = visit.Caption;
            editedAppointment.LabelId = visit.LabelId;

            return true;
        }

        #endregion

        #region OnDashboardCustomDrawTimeCell

        private void OnDashboardCustomDrawTimeCell(object sender, CustomDrawObjectEventArgs e)
        {
            SelectableIntervalViewInfo viewInfo = e.ObjectInfo as SelectableIntervalViewInfo;
            SchedulerViewCellBase cell = e.ObjectInfo as SchedulerViewCellBase;
            if (viewInfo == null || cell == null)
                return;

            Technician technician = (Technician) viewInfo.Resource.GetRow(View.m_dashboardStorage);
            if (technician == null)
                return;
            TimeInterval cellInterval = new TimeInterval(viewInfo.Interval.Start, viewInfo.Interval.End);

            bool isWithinWorkingTime = false;
            foreach (TechnicianWorkTime workingInterval in technician.WorkingIntervals)
            {
                if (workingInterval.GetInterval().IsIntersects(cellInterval))
                {
                    isWithinWorkingTime = true;
                    break;
                }
            }
            
            if (!isWithinWorkingTime)
            {
                if (viewInfo.Selected)
                    e.Cache.FillRectangle(e.Cache.GetSolidBrush(Color.FromArgb(49, 106, 197)), cell.Bounds);
                else
                    e.Cache.FillRectangle(e.Cache.GetSolidBrush(Color.FromArgb(190, 174, 124)), cell.Bounds);

                e.Handled = true;
            } 
        }

        #endregion

        #region OnDashboardPreparePopupMenu

        private void OnDashboardPreparePopupMenu(object sender, PreparePopupMenuEventArgs e)
        {
            Point pt = View.m_dashboard.PointToClient(Control.MousePosition);
            SchedulerHitInfo hitInfo = View.m_dashboard.ActiveView.ViewInfo.CalcHitInfo(pt, false);

            e.Menu.RemoveMenuItem(SchedulerMenuItemId.OpenAppointment);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoToday);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoDate);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.SwitchViewMenu);
        
            if (hitInfo.HitTest == SchedulerHitTest.ResourceHeader
                || hitInfo.NextHitInfo.HitTest == SchedulerHitTest.ResourceHeader)
            {
                e.Menu.RemoveMenuItem(SchedulerMenuItemId.DeleteAppointment);

                View.m_menuEditTechnicianDaySettings.Tag = hitInfo.ViewInfo.Resource;
                View.m_menuEditTechnicianDefaultSettings.Tag = hitInfo.ViewInfo.Resource;
                View.m_menuTechnicianPrint.Tag = hitInfo.ViewInfo.Resource;
                View.m_menuTechnicianShowRoute.Tag = hitInfo.ViewInfo.Resource;

                e.Menu.Items.Add(View.m_menuEditTechnicianDaySettings);
                e.Menu.Items.Add(View.m_menuEditTechnicianDefaultSettings);
                View.m_menuTechnicianShowRoute.BeginGroup = true;
                e.Menu.Items.Add(View.m_menuTechnicianShowRoute);
                e.Menu.Items.Add(View.m_menuTechnicianPrint);

                View.m_menuTechnicianPrint.Enabled = HasTechnicianNonBlockoutVisits((int) hitInfo.ViewInfo.Resource.Id);
                View.m_menuTechnicianShowRoute.Enabled = View.m_menuTechnicianPrint.Enabled;
            }
            else if (e.Menu.Id == SchedulerMenuItemId.DefaultMenu
                && hitInfo.HitTest == SchedulerHitTest.Cell)            
            {                
                e.Menu.Items.Add(View.m_menuSetAsWorkingTime);
                e.Menu.Items.Add(View.m_menuSetAsFreeTime);                
                e.Menu.Items.Add(View.m_menuBlockOutTime);

                if (View.m_dashboardStorage.Resources.Count == 0
                    || View.m_dashboard.SelectedAppointments.Count > 0
                    || View.m_dashboard.SelectedInterval.Duration.TotalMinutes == 0)
                {
                    View.m_menuSetAsWorkingTime.Enabled = false;
                    View.m_menuSetAsFreeTime.Enabled = false;
                    View.m_menuBlockOutTime.Enabled = false;
                } else
                {
                    View.m_menuSetAsWorkingTime.Enabled = true;
                    View.m_menuSetAsFreeTime.Enabled = true;
                    View.m_menuBlockOutTime.Enabled = true;
                }
            }
            else if (e.Menu.Id == SchedulerMenuItemId.AppointmentMenu
                && hitInfo.HitTest == SchedulerHitTest.AppointmentContent)
            {
                Appointment appointment = View.m_dashboard.SelectedAppointments[0];
                Visit visit = (Visit)appointment.GetRow(View.m_dashboardStorage);

                View.m_menuVisitPrint.Enabled = !visit.IsBlockout;
                e.Menu.Items.Add(View.m_menuVisitPrint);
            }

        }

        private void OnMenuSetAsWorkingTimeClick(object sender, EventArgs e)
        {
            if (!AuthenticateController.IsAccessAllowed(UserRoleEnum.Supervisor))
                return;

            try
            {
                Model.MarkTimeAs(
                    new TimeInterval(View.m_dashboard.SelectedInterval.Start, View.m_dashboard.SelectedInterval.End),
                    (Technician)View.m_dashboard.SelectedResource.GetRow(View.m_dashboardStorage), true);
            }
            catch (FaultException<WcfServiceBusinessException> ex)
            {
                XtraMessageBox.Show(ex.Detail.Message, ex.Detail.Caption, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }                
        }

        private void OnMenuSetAsFreeTimeClick(object sender, EventArgs e)
        {
            if (!AuthenticateController.IsAccessAllowed(UserRoleEnum.Supervisor))
                return;

            try
            {
                Model.MarkTimeAs(
                    new TimeInterval(View.m_dashboard.SelectedInterval.Start, View.m_dashboard.SelectedInterval.End),
                    (Technician)View.m_dashboard.SelectedResource.GetRow(View.m_dashboardStorage), false);
            }
            catch (FaultException<WcfServiceBusinessException> ex)
            {
                XtraMessageBox.Show(ex.Detail.Message, ex.Detail.Caption, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }                
        }

        private void OnMenuBlockOutTimeClick(object sender, EventArgs eventArgs)
        {
            if (!AuthenticateController.IsAccessAllowed(UserRoleEnum.Supervisor))
                return;

            Technician technician = (Technician)View.m_dashboard.SelectedResource.GetRow(View.m_dashboardStorage);

            using (BlockoutCreateController controller = Prepare<BlockoutCreateController>(null,
                technician, View.m_dashboard.SelectedInterval.Start, View.m_dashboard.SelectedInterval.End))
            {
                controller.Execute(false);
            }
        }

        private void OnMenuEditTechnicianSettingsClick(object sender, EventArgs args)
        {
            Resource resource = (Resource) ((DXMenuItem)sender).Tag;
            Technician technician = (Technician)resource.GetRow(View.m_dashboardStorage);

            bool isDefaultSettings = sender == View.m_menuEditTechnicianDefaultSettings;

            using (TechnicianEditController controller = Prepare<TechnicianEditController>(
                technician, isDefaultSettings))
            {
                controller.Execute(false);
            }
        }

        private void OnMenuVisitPrintClick(object sender, EventArgs args)
        {
            Appointment appointment = View.m_dashboard.SelectedAppointments[0];
            Visit visit = (Visit) appointment.GetRow(View.m_dashboardStorage);

            try
            {
                using(new WaitCursor())
                    visit.Print();
            }
            catch (Exception ex)
            {
                Host.Trace("OnMenuVisitPrintClick", ex.ToString());
                MessageBox.Show(ex.Message, "Printing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void OnMenuTechnicianPrintClick(object sender, EventArgs args)
        {
            Resource resource = (Resource)((DXMenuItem)sender).Tag;

            try
            {
                using(new WaitCursor())
                {
                    foreach (Visit visit in GetTechnicianVisits((int)resource.Id))
                        visit.Print();
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Printing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }            
        }

        private void OnMenuTechnicianShowRouteClick(object sender, EventArgs args)
        {
            Resource resource = (Resource)((DXMenuItem)sender).Tag;
            Technician technician = (Technician)resource.GetRow(View.m_dashboardStorage);

            Process.Start(Model.GetGoogleMapRouteUrl(technician, GetTechnicianVisits(technician.ID)));
        }

//        private void OnTechnicianOptimizeRpmClick(object sender, EventArgs args)
//        {
//            Resource resource = (Resource)((DXMenuItem)sender).Tag;
//            Technician technician = (Technician)resource.GetRow(View.m_dashboardStorage);
//
//            using (RouteAnalyzeController controller = Prepare<RouteAnalyzeController>(
//                (int)resource.Id, GetCurrentCost(technician)))
//            {
//                controller.Execute(false);
//            }            
//        }

        #endregion        

        #region DB update on change

        private void OnDashboardAppointmentResized(object sender, AppointmentResizeEventArgs e)
        {
            Visit visit = (Visit)View.m_dashboardStorage.GetObjectRow(e.EditedAppointment);            

            if (!AuthenticateController.IsAccessAllowed(
                visit.IsBlockout ? UserRoleEnum.Supervisor : UserRoleEnum.Dispatrcher))
            {
                e.Allow = false;
                e.Handled = true;
                return;
            }            
            
            visit.UpdateDurationCostByDuration(
                e.EditedAppointment.End.Subtract(e.EditedAppointment.Start).TotalMinutes);
        }

        private void OnDashboardAppointmentResizing(object sender, AppointmentResizeEventArgs e)
        {
            if (!UpdateAppointmentStatus(e.EditedAppointment))
            {
                e.Allow = false;
                e.Handled = true;
            }
        }

        private void OnDashboardAppointmentDeleting(object sender, PersistentObjectCancelEventArgs e)
        {
            Visit visit = (Visit)View.m_dashboardStorage.GetObjectRow(e.Object);

            if (visit.IsBlockout && XtraMessageBox.Show("Deleting blockout time interval. Are you sure?", "Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            if (!AuthenticateController.IsAccessAllowed(
                visit.IsBlockout ? UserRoleEnum.Supervisor : UserRoleEnum.Dispatrcher))
            {
                e.Cancel = true;
                return;
            }

            using (new WaitCursor())
                Model.UnscheduleVisit(visit);

            //Workaround to not let dashboard affect collection by itself
            e.Cancel = true;
        }

        private void OnDashboardDoubleClick(object sender, EventArgs eventArgs)
        {
            if (View.m_dashboard.SelectedAppointments.Count == 0)
                return;

            Visit visit = (Visit) View.m_dashboardStorage.GetObjectRow(View.m_dashboard.SelectedAppointments[0]);            
            if (visit.IsBlockout)
            {
                using (BlockoutCreateController controller = Prepare<BlockoutCreateController>(visit.Clone()))
                    controller.Execute(false);                
            }
        }

        private void OnDashboardAppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {   
            foreach (PersistentObject appointment in e.Objects)
            {
                Visit visit = (Visit)View.m_dashboardStorage.GetObjectRow(appointment);
                Technician technician = Technician.GetTechnician(visit.TechnicianId.Value);
                DateTime oldTimeEnd = visit.TimeEnd;
                visit.UpdateTimeEndByTechnician(technician);

                if (oldTimeEnd != visit.TimeEnd)
                    View.m_dashboard.RefreshData();
                Model.UpdateVisit(visit);
                UpdateAppointmentStatus((Appointment) appointment);

                if (m_isUserChangingAppointment)
                {
                    Visit prevClose = GetCloseNeighbour(visit, true);
                    Visit nextClose = GetCloseNeighbour(visit, false);

                    if (prevClose != null && !m_visitsToRefreshStatuses.Contains(prevClose))
                        m_visitsToRefreshStatuses.Add(prevClose);
                    if (nextClose != null && !m_visitsToRefreshStatuses.Contains(nextClose))
                        m_visitsToRefreshStatuses.Add(nextClose);

                    foreach (Appointment app in View.m_dashboardStorage.Appointments.Items)
                    {
                        Visit updateCandidate = (Visit) app.GetRow(View.m_dashboardStorage);

                        if (m_visitsToRefreshStatuses.Contains(updateCandidate))
                            app.StatusId = GetVisitStatusId(updateCandidate);
                    }

                    m_visitsToRefreshStatuses.Clear();
                    m_isUserChangingAppointment = false;
                }
            }                
        }

        #endregion      

        #region OnTechnicianArrangeClick

        private void OnTechnicianArrangeClick(object sender, EventArgs e)
        {
            Point menuPoint = View.PointToScreen(View.m_btnTechnicianReorder.Location);
            menuPoint.Offset(0, View.m_btnTechnicianReorder.Height);
            View.m_menuTechnicianArrange.ShowPopup(menuPoint);
        }

        private void OnMenuTechnicianArrangeClick(object sender, ItemClickEventArgs args)
        {
            using (TechnicianArrangementController controller = Prepare<TechnicianArrangementController>(
                args.Item == View.m_menuTechnicianDefaultOrder))
            {
                controller.Execute(false);
            }
        }

        #endregion

        #region OnMiscClick

        private void OnMiscClick(object sender, EventArgs eventArgs)
        {
            Point menuPoint = View.PointToScreen(View.m_btnMisc.Location);
            menuPoint.Offset(0, View.m_btnMisc.Height);
            View.m_menuMisc.ShowPopup(menuPoint);
        }

        private void OnUserActionReportClick(object sender, ItemClickEventArgs itemClickEventArgs)
        {
            if (!AuthenticateController.IsAccessAllowed(UserRoleEnum.Supervisor))
                return;

            foreach (Form form in Application.OpenForms)
            {
                if (form is ActionReportView)
                {
                    form.BringToFront();
                    return;
                }
            }

            ActionReportController controller = Prepare<ActionReportController>();
            controller.Execute(true);
        }

        private void OnUserManagementItemClick(object sender, ItemClickEventArgs itemClickEventArgs)
        {
            if (!AuthenticateController.IsAccessAllowed(UserRoleEnum.Supervisor))
                return;

            using (UserManageController controller = Prepare<UserManageController>())
                controller.Execute(false);
        }

        #endregion

        #region OnAddTechnicianClick

        private void OnAddTechnicianClick(object sender, ItemClickEventArgs itemClickEventArgs)
        {
            if (!AuthenticateController.IsAccessAllowed(UserRoleEnum.Administrator))
                return;

            using (TechnicianEditController controller = Prepare<TechnicianEditController>(null, true))
                controller.Execute(false);
        }

        #endregion


        #region OnOrdersClick

        private void OnOrdersClick(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is HistoricalOrdersView)
                {
                    form.BringToFront();
                    return;
                }
            }

//            HistoricalOrdersController controller 
//                = Prepare<HistoricalOrdersController>(Model.BookingEngine);            
//            controller.Execute();
        }

        #endregion        

        #region OnFindTechnicianClick

        private void OnFindTechnicianClick(object sender, EventArgs e)
        {
            if (View.m_cmbTechnician.SelectedIndex < 0)
                return;

            int technicianDefaultId = (int) ((ImageComboBoxItem) View.m_cmbTechnician.SelectedItem).Value;
            string technicianName = ((ImageComboBoxItem) View.m_cmbTechnician.SelectedItem).Description;

            foreach (Resource resource in View.m_dashboardStorage.Resources.Items)
            {
                Technician technician = (Technician)resource.GetRow(View.m_dashboardStorage);
                if (technician.TechnicianDefaultId == technicianDefaultId)
                {                    
                    View.m_dashboard.ActiveView.SetSelection(
                        new DevExpress.XtraScheduler.TimeInterval(View.m_dashboard.Start.Date, new TimeSpan(23, 0, 0)), 
                        resource);
                    return;
                }
            }

            Technician fakeTechnician = new Technician(0);
            fakeTechnician.TechnicianDefaultId = technicianDefaultId;
            fakeTechnician.ScheduleDate = View.m_dashboard.Start.Date;

            if (XtraMessageBox.Show("Technician " + technicianName + " is removed from current schedule. Would you like to edit his daily settings?",
                "Technician not found", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                using (TechnicianEditController controller = Prepare<TechnicianEditController>(
                    fakeTechnician, false))
                {
                    controller.Execute(false);
                }                
            }
        }

        #endregion

        #region OnFindBadRouteClick

        private void OnFindBadRouteClick(object sender, EventArgs args)
        {
            if (m_lastSearchInfo != null && !m_lastSearchInfo.IsLastSearchBadRoute )
                m_lastSearchInfo = null;

            int startIndex = 0;
            if (m_lastSearchInfo != null)
            {
                foreach (Resource resource in View.m_dashboardStorage.Resources.Items)
                {
                    if ((int)resource.Id == (int)m_lastSearchInfo.FoundResource.Id)
                    {
                        startIndex = View.m_dashboardStorage.Resources.Items.IndexOf(resource) + 1;
                        break;
                    }
                }
            }
                

            for (int i = startIndex; i < View.m_dashboardStorage.Resources.Items.Count; i++)
            {
                Resource resource = View.m_dashboardStorage.Resources.Items[i];
                Technician technician = (Technician)View.m_dashboardStorage.GetObjectRow(resource);

                double penalty;
                IList<Visit> visits = GetTechnicianVisits(technician.ID);
                double drive = MainFormModel.GetTechnicianDriveDistance(technician, visits, out penalty);

                if (drive > 0 && penalty > 0)
                {
                    View.m_dashboard.ActiveView.SetSelection(
                        new DevExpress.XtraScheduler.TimeInterval(View.m_dashboard.Start.Date, new TimeSpan(23, 0, 0)),
                        resource);
                    m_lastSearchInfo = new TechnicianSearchInfo(string.Empty, true, resource);
                    return;
                }
            }

            if (startIndex == 0)
            {
                XtraMessageBox.Show("No unfeasible routes found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (XtraMessageBox.Show("Search from the beginning?", "Question", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                m_lastSearchInfo = null;
                OnFindBadRouteClick(null, null);
            } 
        }        

        private double GetCurrentCost(Technician technician)
        {
            IList<Visit> visits = GetTechnicianVisits(technician.ID);
            double penalty;
            double drive = MainFormModel.GetTechnicianDriveDistance(technician, visits, out penalty);
            return drive + penalty*Utils.PENALTY_MULTIPLIER;
        }

        #endregion


        #region OnFindZipClick

        private void OnFindZipClick(object sender, EventArgs e)
        {
            if (View.m_txtZip.Text == string.Empty)
                return;

            if (m_lastSearchInfo != null && 
                (m_lastSearchInfo.IsLastSearchBadRoute || m_lastSearchInfo.SearchedZip != View.m_txtZip.Text))
            {
                m_lastSearchInfo = null;                
            }

            int startIndex = 0;
            if (m_lastSearchInfo != null)
                startIndex = View.m_dashboardStorage.Resources.Items.IndexOf(m_lastSearchInfo.FoundResource) + 1;            

            for (int i = startIndex; i < View.m_dashboardStorage.Resources.Items.Count; i++)
            {
                Resource resource = View.m_dashboardStorage.Resources.Items[i];
                Technician technician = (Technician)View.m_dashboardStorage.GetObjectRow(resource);

                if (technician.PrimaryZipCodes.Contains(View.m_txtZip.Text)
                    || technician.SecondaryZipCodes.Contains(View.m_txtZip.Text))
                {
                    View.m_dashboard.ActiveView.SetSelection(
                        new DevExpress.XtraScheduler.TimeInterval(View.m_dashboard.Start.Date, new TimeSpan(23, 0, 0)),
                        resource);
                    m_lastSearchInfo = new TechnicianSearchInfo(View.m_txtZip.Text, false, resource);
                    return;
                }
            }

            if (startIndex == 0)
            {
                XtraMessageBox.Show(string.Format("No technicians which service zip {0} were found", View.m_txtZip.Text), 
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            else if (XtraMessageBox.Show("Search from the beginning?", "Question", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                m_lastSearchInfo = null;
                OnFindZipClick(null, null);
            }
        }

        #endregion

        #region OnFindVisitClick

        private void OnFindVisitClick(object sender, EventArgs args)
        {
            if (View.m_txtVisitNumber.Text == string.Empty)
                return;

            foreach (Appointment appointment in View.m_dashboardStorage.Appointments.Items)
            {
                Visit visit = (Visit) appointment.GetRow(View.m_dashboardStorage);
                if (visit.TicketNumber == View.m_txtVisitNumber.Text)
                {
                    View.m_dashboard.ActiveView.SelectAppointment(appointment);
                    return;
                }
            }

            BindingList<Visit> delayedVisits = (BindingList<Visit>) View.m_gridDelayedVisits.DataSource;
            foreach (Visit visit in delayedVisits)
            {
                if (visit.TicketNumber == View.m_txtVisitNumber.Text)
                {
                    View.m_tabs.SelectedTabPageIndex = 0;
                    View.m_gridDelayedVisitsView.FocusedRowHandle
                        = View.m_gridDelayedVisitsView.GetRowHandle(delayedVisits.IndexOf(visit));
                    return;
                }
            }

            XtraMessageBox.Show("Visit " + View.m_txtVisitNumber.Text + " not found", "No Visit",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Search on enter

        private void OnTechnicianKeyPress(object sender, KeyPressEventArgs args)
        {
            if (args.KeyChar == '\r')
            {
                OnFindTechnicianClick(null, null);
                args.Handled = true;
            }
        }

        private void OnZipKeyPress(object sender, KeyPressEventArgs args)
        {
            if (args.KeyChar == '\r')
            {
                OnFindZipClick(null, null);
                args.Handled = true;
            }
        }

        private void OnVisitNumberKeyPress(object sender, KeyPressEventArgs args)
        {
            if (args.KeyChar == '\r')
            {
                OnFindVisitClick(null, null);
                args.Handled = true;
            }
        }

        #endregion


        #region OnGridRowCellStyle

        private void OnGridRowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gridView = (GridView) sender;

            Visit visit = (Visit)gridView.GetRow(e.RowHandle);
           
            if (visit != null && visit.CanBook)
            {
                if (gridView.FocusedRowHandle == e.RowHandle)
                {
                    if (gridView.FocusedColumn == e.Column)
                        e.Appearance.BackColor = Color.White;
                    else                    
                        e.Appearance.BackColor = Color.Green;
                } else 
                    e.Appearance.BackColor = Color.LightGreen;
            }            
        }

        #endregion        

        #region OnGridVisitsDoubleClick

        private void OnGridVisitsDoubleClick(object sender, EventArgs e)
        {
            GridView gridView = (GridView)sender;
            Visit visit = (Visit)gridView.GetRow(gridView.FocusedRowHandle);

            if (visit != null)
            {
                if (visit.CanBook)
                {
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        RecommendationRequest request = Model.SendRecommendationRequest(visit);
                        using (VisitAddController controller = Prepare<VisitAddController>(request))
                        {
                            controller.Execute(false);
                            if (controller.SelectedItem != null)
                            {
                                if (!Model.BookDelayedVisit(visit.ID, controller.SelectedItem))
                                    XtraMessageBox.Show("Visit cannot be booked", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                                
                            }
                        }                            
                        
                        return;
                    }

                    if (XtraMessageBox.Show("Do you really want to book this visit?", "Confirmation",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (!AuthenticateController.IsAccessAllowed(UserRoleEnum.Dispatrcher))
                            return;

                        bool isSuccess = false;
                        using (new WaitCursor())
                            isSuccess = Model.BookDelayedVisit(visit.ID, null);

                        if (!isSuccess)
                            XtraMessageBox.Show("Visit cannot be booked", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (gridView == View.m_gridDelayedVisitsView)
                {
                    using (DelayedVisitProcessController controller = Prepare<DelayedVisitProcessController>(visit))
                        controller.Execute(false);
                }
                else
                {
                    string targetCaption = visit.Caption;
                    foreach (Appointment appointment in View.m_dashboardStorage.Appointments.Items)
                    {
                        if (appointment.Description == targetCaption)
                        {
                            View.m_dashboard.DayView.SelectAppointment(appointment);
                            break;
                        }
                    }                    
                }
            }            
        }

        #endregion

        #region OnPrintAllClick

        private void OnPrintAllClick(object sender, EventArgs args)
        {
            if (XtraMessageBox.Show("Do you really want to print all the tickets?", "Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            try
            {
                using (new WaitCursor())
                {
                    foreach (var resource in View.m_dashboardStorage.Resources.Items)
                    {
                        foreach (Visit visit in GetTechnicianVisits((int)resource.Id))
                            visit.Print();                        
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Printing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }

        #endregion

        #region OnBucketTempAssignmentCountDoubleClick

        private void OnBucketTempAssignmentCountDoubleClick(object sender, EventArgs args)
        {
            DashboardStatisticInfo info = new DashboardStatisticInfo(Model, BucketVisits);
            XtraMessageBox.Show(info.StatisticsText + info.ErrorsText);
        }

        #endregion

        #region OnViewKeyDown

        private void OnViewKeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Escape)
            {
                args.SuppressKeyPress = true;
                args.Handled = true;
            }
        }

        #endregion

        #region OnPendingChangesTimerTick

        private void OnPendingChangesTimerTick(object sender, EventArgs args)
        {
            if (!WcfCallbackListener.HasPendingChanges())
                return;

            if (!WcfCallbackListener.IsSequenceValid())
            {
                ResetUI();
                return;
            }

            CallbackInfo change = WcfCallbackListener.Read();

            if (change.ChangeType == CallbackType.TechnicianWorkHours)
            {
                TechnicianWorkHoursChangeDetail detail = change.TechnicianWorkHoursChange;
                Technician technician = Technician.GetTechnician(detail.TechnicianId);
                technician.WorkingIntervals = detail.WorkingIntervals;
                Technician.CacheClient(technician);

                Technician boardTechnician = (Technician)View.m_dashboardStorage.Resources
                    .Items.GetResourceById(technician.ID).GetRow(View.m_dashboardStorage);

                View.m_dashboard.BeginUpdate();
                boardTechnician.WorkingIntervals = technician.WorkingIntervals;
                View.m_dashboard.EndUpdate();
                View.m_dashboard.Invalidate();
            }
            else if (change.ChangeType == CallbackType.Technician)
            {
                Technician modifiedTechnician = change.TechnicianChange;
                Technician.CacheClient(modifiedTechnician);

                Technician boardTechnician = (Technician)View.m_dashboardStorage.Resources
                    .Items.GetResourceById(modifiedTechnician.ID).GetRow(View.m_dashboardStorage);
                int index = Model.Technicians.IndexOf(boardTechnician);

                View.m_dashboard.BeginUpdate();
                Model.Technicians[index] = modifiedTechnician;
                Model.Technicians.ResetItem(index);
                View.m_dashboard.EndUpdate();                            
            }
            else if (change.ChangeType == CallbackType.Technicians)
            {
                Technician.CacheClient(change.TechniciansChange);                
                ShowTechnicians(change.TechniciansChange);
            }
            else if (change.ChangeType == CallbackType.VisitsFull)
            {
                RefreshUI(change.VisitsFullChange);
                CheckErrors();
            }
            else if (change.ChangeType == CallbackType.Visit)
            {
                View.m_dashboard.BeginUpdate();
                RefreshModifiedVisit(change.VisitChange);
                View.m_dashboard.RefreshData();
                View.m_dashboard.EndUpdate();
                CheckErrors();
            }
            else if (change.ChangeType == CallbackType.Visits)
            {
                RefreshUI(change.VisitsChange);
                CheckDashboardEstimate();
                CheckErrors();
            }
        }

        private void CheckErrors()
        {
            if (!Configuration.IsRealtimeMode)
            {
                DashboardStatisticInfo newStatistics = new DashboardStatisticInfo(Model, BucketVisits);
                if (newStatistics.ErrorsText != string.Empty)
                    Debug.Fail(newStatistics.ErrorsText);
            }
        }

        private void CheckDashboardEstimate()
        {
            if (!Configuration.IsRealtimeMode && Model.EstimatedDashboardInfo != null)
            {
                DashboardStatisticInfo newStatistics = new DashboardStatisticInfo(Model, BucketVisits);

                if (Model.EstimatedDashboardInfo.SecondaryAreaVisits != newStatistics.SecondaryAreaVisits)
                    Debug.Fail(string.Format("Secondary visits count error. Should be {0} instead of {1}",
                        Model.EstimatedDashboardInfo.SecondaryAreaVisits, newStatistics.SecondaryAreaVisits));

                if (Math.Round(Model.EstimatedDashboardInfo.Cost, 1) != Math.Round(newStatistics.Cost, 1))
                    Debug.Fail(string.Format("Cost change error. Should be {0} instead of {1}",
                        Model.EstimatedDashboardInfo.Cost.ToString("0.00"), newStatistics.Cost.ToString("0.00")));

                Model.EstimatedDashboardInfo = null;
            }            
        }

        #endregion

        #region OnTimerKeepAliveTick

        private void OnTimerKeepAliveTick(object sender, EventArgs args)
        {
           Model.KeepAliveDummy();
        }

        #endregion

        #region Refresh UI

        public void ResetUI()
        {
            VisitsFullChangeDetail fullChangeDetail = Model.GetFullViewInfo();
            WcfCallbackListener.Reset(fullChangeDetail.LastProcessedCallbackId);                
            ShowTechnicians(Model.GetTechnicians(false));
            RefreshUI(fullChangeDetail);            
        }

        private void ShowTechnicians(List<Technician> technicians)
        {
            View.m_dashboard.BeginUpdate();
            Model.Technicians = new BindingList<Technician>(technicians);
            View.m_dashboardStorage.Resources.DataSource = Model.Technicians;                
            View.m_dashboard.EndUpdate();
        }

        private void RefreshUI(List<Visit> delayedVisits, List<Visit> tempAssignedVisits)
        {
            if (delayedVisits != null)
            {
                View.m_gridDelayedVisits.BeginUpdate();
                View.m_gridDelayedVisits.DataSource = new BindingList<Visit>(delayedVisits);
                View.m_gridDelayedVisits.EndUpdate();                
            }

            if (tempAssignedVisits != null)
            {
                View.m_gridTempAssignment.BeginUpdate();
                View.m_gridTempAssignment.DataSource = new BindingList<Visit>(tempAssignedVisits);
                View.m_gridTempAssignment.EndUpdate();                
            }

            View.m_lblVisitsCount.Text = Model.Visits.Count.ToString();
            View.m_lblBucketTempAssignmentCount.Text = ((BindingList<Visit>)View.m_gridDelayedVisits.DataSource).Count
                + "/" + ((BindingList<Visit>)View.m_gridTempAssignment.DataSource).Count;
            View.m_lblTechniciansCount.Text = Model.Technicians.Count.ToString();
        }

        private bool m_isSuspendRecommendationsStateLoading;
        private void RefreshUI(VisitsFullChangeDetail fullChangeDetail)
        {
            BindingListVisit visits = new BindingListVisit(fullChangeDetail.DashboardVisits);

            View.m_dashboard.BeginUpdate();
            Model.Visits = visits;
            View.m_dashboardStorage.Appointments.DataSource = Model.Visits;
            foreach (var visit in visits)
                UpdateVisitStatus(visit, false);
            View.m_dashboardStorage.RefreshData();
            View.m_dashboard.EndUpdate();

            m_isSuspendRecommendationsStateLoading = true;
            View.m_chkSuspendRecommendations.Checked = fullChangeDetail.IsRecommendationsSuspended;
            m_isSuspendRecommendationsStateLoading = false;

            RefreshUI(fullChangeDetail.BucketVisits, fullChangeDetail.TemporaryAssignedVisits);
        }

        private void RefreshUI(VisitsChangeDetail changeDetail)
        {           
            View.m_dashboard.BeginUpdate();
            foreach (var visit in changeDetail.DashboardRemovedVisits)
            {
                Visit dashboardVisit = Model.Visits.GetVisitByTicketNumber(visit.TicketNumber);

                Visit top = GetCloseNeighbour(dashboardVisit, true);
                Visit bottom = GetCloseNeighbour(dashboardVisit, false);
                Model.Visits.Remove(dashboardVisit);
                UpdateVisitStatus(top, false);
                UpdateVisitStatus(bottom, false);
            }

            foreach (var visit in changeDetail.DashboardAddedVisits)
            {
                Model.Visits.Add(visit);
                UpdateVisitStatus(visit, true);
            }

            foreach (var visit in changeDetail.DashboardModifiedVisits)
                RefreshModifiedVisit(visit);

            View.m_dashboardStorage.RefreshData();
            View.m_dashboard.EndUpdate();

            RefreshUI(changeDetail.BucketVisits, changeDetail.TemporaryAssignedVisits);
        }

        private void RefreshModifiedVisit(Visit callbackModifiedVisit)
        {
            Visit dashboardVisit = Model.Visits.GetVisitByTicketNumber(callbackModifiedVisit.TicketNumber);

            Visit top = GetCloseNeighbour(dashboardVisit, true);
            Visit bottom = GetCloseNeighbour(dashboardVisit, false);
            Model.Visits.Remove(dashboardVisit);
            UpdateVisitStatus(top, false);
            UpdateVisitStatus(bottom, false);
            Model.Visits.Add(callbackModifiedVisit);
            UpdateVisitStatus(callbackModifiedVisit, true);            
        }

        #endregion

        #region UpdateVisitStatus

        private void UpdateVisitStatus(Visit visit, bool updateNeighbors)
        {
            if (updateNeighbors)
            {
                Visit top = GetCloseNeighbour(visit, true);
                Visit bottom = GetCloseNeighbour(visit, false);

                if (top != null)
                    top.StatusId = GetVisitStatusId(top);
                if (bottom != null)
                    bottom.StatusId = GetVisitStatusId(bottom);
            }

            if (visit != null)
                visit.StatusId = GetVisitStatusId(visit);
        }

        #endregion

        #region OnDashboardDateChanged

        private void OnDashboardDateChanged(object sender, EventArgs args)
        {
            View.m_dashboard.Start = View.m_dtpDashboardDate.DateTime.Date;
            WcfClient.WcfClient.SetClientProperties(View.m_dashboard.Start);
            ResetUI();            
        }

        #endregion

        #region OnDashboardDateDoubleClick

        private void OnDashboardDateDoubleClick(object sender, EventArgs args)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
//                if (XtraMessageBox.Show("Do you really want to import working hours", "Confirmation",
//                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
//                {
//                    return;
//                }
//
//
//                using (new WaitCursor())
//                {
//                    Model.ImportSettings();
//                }
//
//                XtraMessageBox.Show("Working hours were successfully imported", "Done",
//                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                WcfClient.WcfClient.Instance.EnqueueOptimization(View.m_dtpDashboardDate.DateTime.Date);                            
            }                
            else
            {
                using (new WaitCursor())
                {
                    Model.ImportTickets();
                }

                XtraMessageBox.Show("Tickets were successfully imported", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        #endregion

        #region OnSuspendRecommendationsClick

        private void OnSuspendRecommendationsChenged(object sender, EventArgs eventArgs)
        {
            if (m_isSuspendRecommendationsStateLoading)
                return;
            Model.ModifyPredictionIgnoreDate(View.m_chkSuspendRecommendations.Checked);
        }

        private void OnSuspendRecommendationsChanging(object sender, ChangingEventArgs changingEventArgs)
        {
            if (m_isSuspendRecommendationsStateLoading)
                return;

            if (Control.ModifierKeys != Keys.Shift)
                changingEventArgs.Cancel = true;
            else
            {
                if (!View.m_chkSuspendRecommendations.Checked
                    && XtraMessageBox.Show(string.Format("You are going to close booking for {0}. Are you sure?", WcfClient.WcfClient.ClientDate.ToShortDateString()),
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    changingEventArgs.Cancel = true;
                    return;
                }

                if (!AuthenticateController.IsAccessAllowed(UserRoleEnum.Supervisor))
                {
                    changingEventArgs.Cancel = true;
                    return;
                }
            }
        }

        #endregion

        #region OnPanel2SizeChanged

        private void OnPanel2SizeChanged(object sender, EventArgs eventArgs)
        {
            View.m_chkBucketShowAllDates.Location = new Point(View.m_tabs.Width - 100,
                View.m_splitContainer.Panel2.Location.Y + 2);
        }

        #endregion

        #region OnSelectedTabPageChanged

        private void OnSelectedTabPageChanged(object sender, TabPageChangedEventArgs tabPageChangedEventArgs)
        {
            View.m_chkBucketShowAllDates.Visible = View.m_tabs.TabPages.IndexOf(tabPageChangedEventArgs.Page) == 0;
        }

        #endregion

        #region OnBucketShowAllDatesChanged

        private void OnBucketShowAllDatesChanged(object sender, EventArgs eventArgs)
        {
            WcfClient.WcfClient.SetClientProperties(View.m_chkBucketShowAllDates.Checked);
            ResetUI();
        }

        #endregion

        #region LogOut

        private void OnCurrentUserChanged(User newUser)
        {
            View.m_lblCurrentUser.Text = newUser.Login;
            View.m_btnLogOut.Enabled = newUser.UserRole != UserRoleEnum.Anonymous;
        }

        private void OnLogOutClick(object sender, EventArgs eventArgs)
        {
            User.LogOut();
        }

        #endregion
    }

    internal class TechnicianSearchInfo
    {
        #region Constructor

        public TechnicianSearchInfo(string searchedZip, bool isLastSearchBadRoute, Resource foundResource)
        {
            m_searchedZip = searchedZip;
            m_isLastSearchBadRoute = isLastSearchBadRoute;
            m_foundResource = foundResource;
        }

        #endregion

        #region SearchedZip

        private string m_searchedZip;       
        public string SearchedZip
        {
            get { return m_searchedZip; }
            set { m_searchedZip = value; }
        }

        #endregion

        #region IsLastSearchBadRoute

        private bool m_isLastSearchBadRoute;
        public bool IsLastSearchBadRoute
        {
            get { return m_isLastSearchBadRoute; }
            set { m_isLastSearchBadRoute = value; }
        }

        #endregion

        #region FoundResource

        private Resource m_foundResource;
        public Resource FoundResource
        {
            get { return m_foundResource; }
            set { m_foundResource = value; }
        }

        #endregion
    }
}
