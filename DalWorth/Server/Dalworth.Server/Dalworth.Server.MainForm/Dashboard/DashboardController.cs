using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.ArriveVisit;
using Dalworth.Server.MainForm.CompleteVisit;
using Dalworth.Server.MainForm.ConfirmVisit;
using Dalworth.Server.MainForm.CreateVisit;
using Dalworth.Server.MainForm.CreateWork;
using Dalworth.Server.MainForm.DashboardCustomize;
using Dalworth.Server.MainForm.DispatchVisit;
using Dalworth.Server.MainForm.EndDay;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.MainForm.Properties;
using Dalworth.Server.MainForm.SendMessage;
using Dalworth.Server.MainForm.StartDay;
using Dalworth.Server.MainForm.SubmitEtc;
using Dalworth.Server.MainForm.VisitMerge;
using Dalworth.Server.MainForm.VisitSplit;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using CustomDrawObjectEventArgs=DevExpress.XtraScheduler.CustomDrawObjectEventArgs;
using Message=Dalworth.Server.Domain.Message;
using Task=Dalworth.Server.Domain.Task;

namespace Dalworth.Server.MainForm.Dashboard
{    
    public class DashboardController : NestedController<DashboardModel, DashboardView>
    {
        private const int REFRESH_TIMER_INTERVAL = 5000;
        private bool m_isDashboardRefreshing = false;
        private bool m_isNewLeadsBlinking = false;
        private bool m_isPendingLeadsBlinking = false;
        private bool m_isProjectFeedbackCountBlinking = false;

        private MainFormController m_mainFormController;

        #region IsChangesTrackListenerActive

        public bool IsChangesTrackListenerActive
        {
            get { return View.m_timer.Enabled; }
            set { View.m_timer.Enabled = value; }
        }

        #endregion

        #region SelectedPendingVisit

        private VisitPackage SelectedPendingVisit
        {
            get
            {
                int[] selectedRows = View.m_gridPendingVisitsView.GetSelectedRows();
                if (selectedRows == null || selectedRows.Length == 0)
                    return null;
                return (VisitPackage)View.m_gridPendingVisitsView.GetRow(selectedRows[0]);
            }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.MainFormModel = (MainFormModel) data[0];
            m_mainFormController = (MainFormController)data[1];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_dashboard.Start = DateTime.Now;
            View.m_dashboardStorage.Appointments.DataSource = Model.Appointments;
            View.m_dashboardStorage.Resources.DataSource = Model.Resources;
            View.m_gridPendingVisits.DataSource = Model.PendingVisits;

            View.m_timerBlinking.Enabled = true;
            View.m_timerBlinking.Tick += OnTimerBlinkingTick;

            View.m_dashboard.PreparePopupMenu += OnPreparePopupMenu;
            View.m_dashboard.AllowAppointmentDrag += OnAllowAppointmentDragOrResize;
            View.m_dashboard.AllowAppointmentResize += OnAllowAppointmentDragOrResize;
            View.m_dashboard.AppointmentDrag += OnAppointmentDrag;

            View.m_dashboardStorage.AppointmentsChanged += OnAppointmentsChanged;
            View.m_dashboardStorage.AppointmentInserting += OnAppointmentInserting;
            View.m_dashboardStorage.AppointmentChanging += OnAppointmentChanging;
            View.m_toolTipDashboard.BeforeShow += OnBeforeToolTipShow;

            View.m_gridPendingVisits.MouseDown += OnGridPendingVisitsMouseDown;
            View.m_gridPendingVisits.MouseMove += OnGridPendingTasksMouseMove;
            View.m_gridPendingVisitsView.RowStyle += OnPendingVisitsRowStyle;
            View.m_gridPendingVisitsView.RowCellStyle += OnPendingVisitsRowCellStyle;
            View.m_gridPendingVisits.MouseMove += OnGridPendingVisitsMouseMove;

            View.m_linkGridVisit.Click += OnLinkGridVisitClick;
            View.m_gridPendingVisitsView.Click += OnLinkGridPendingPrintClick;

            View.m_menuVisitDetails.Click += OnDashboardVisitDetailsClick;            
            View.m_menuNewVisit.Click += OnMenuDashboardNewVisitClick;
            View.m_menuVisitPrint.Click += OnDashboardVisitPrintClick;
            View.m_menuVisitSend.Click += OnDashboardVisitSendClick;
            
            View.m_dashboard.VisibleIntervalChanged += OnDashboardVisibleIntervalChanged;
            View.m_btnPrevDate.Click += OnPrevDateClick;
            View.m_btnNextDate.Click += OnNextDateClick;

            View.m_timerCurrentTime.Tick += OnCurrentTimeTick;
            View.m_dashboard.CustomDrawDayViewTimeRuler += OnDashboardCustomDrawDayViewTimeRuler;

            View.m_dashboardStorage.AppointmentDeleting += OnDashboardAppointmentDeleting;

            View.m_menuDispatchVisit.Click += OnDispatchVisitClick;
            View.m_menuConfirmVisit.Click += OnConfirmVisitClick;
            View.m_menuArriveVisit.Click += OnArriveClick;
            View.m_menuSubmitEtcVisit.Click += OnSubmitEtcClick;
            View.m_menuCompleteVisit.Click += OnCompleteVisitClick;
            View.m_menuUnassignVisit.Click += OnUnassignVisitClick;
            View.m_menuRescheduleVisit.Click += OnRescheduleVisitClick;
            View.m_menuUndoVisit.Click += OnUndoVisitClick;

            View.m_menuCreateWork.Click += OnCreateWorkClick;
            View.m_menuStartDay.Click += OnStartDayClick;
            View.m_menuCompleteWork.Click += OnCompleteWorkClick;
            View.m_menuUndoWork.Click += OnUndoWorkClick;
            View.m_menuWorkDetails.Click += OnWorkDetailsClick;
            View.m_menuSendMessage.Click += OnSendMessageClick;

            View.m_dashboard.CustomDrawTimeCell += DrawDisabledCells;
            View.m_btnCustomize.Click += OnCustomizeClick;
            View.m_cmbPendingVisitsFilter.SelectedIndexChanged += OnPendingVisitsFilterChanged;

            View.m_dashboard.CustomDrawDayHeader += DrawTimeFrames;
            View.m_dashboard.MouseMove += OnDashboardMouseMove;
            View.m_dashboard.MouseDown += OnDashboardMouseDown;                        

            View.m_dashboard.CustomDrawAppointment += DashboardDrawAdditionalVisitGraphic;
            View.m_dashboard.MouseDoubleClick += OnDashboardMouseDoubleClick;
            View.m_btnNewVisit.Click += OnNewVisitClick;

            View.m_menuPendingVisitPrint.ItemClick += OnMenuPrintVisitClick;
            View.m_menuPendingVisitSplit.ItemClick += OnMenuSplitVisitClick;
            View.m_menuPendingVisitNewVisit.ItemClick += OnMenuPendingVisitNewVisitClick;
            View.m_txtPendingVisitCustomerFilter.TextChanged += OnPendingVisitCustomerFilterChanged;
            View.m_txtPendingVisitCustomerFilter.ButtonPressed += OnPendingVisitCustomerFilterButtonPressed;

            View.m_dtpDashboardDate.DateTimeChanged += OnDashboardDateChanged;

            InitializeDashboardDate();

            View.m_dashboard.SelectionChanged += OnDashboardSelectionChanged;
            View.m_dashboard.KeyDown += OnDashboardKeyDown;

            m_mainFormController.Form.KeyDown += OnKeyDown;
            View.m_gridPendingVisits.KeyDown += OnGridPendingVisitsKeyDown;

            View.m_lblDayTotalAmount.MouseHover += OnDayTotalAmountMouseHover;
            View.m_lblDayTotalAmount.MouseLeave += OnDayTotalAmountMouseLeave;

            View.m_timer.Enabled = false;
            View.m_timer.Tick += OnTimerTick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_dashboard.DayView.TopRowTime = TimeSpan.FromHours(7.5);
            View.m_dashboard.NextControl = View.m_btnNewVisit;
            View.m_dashboard.PreviousControl = View.m_dtpDashboardDate;
            View.m_dashboard.Focus();

            OnTimerTick(null, null);
            View.m_timer.Enabled = true;
        }

        #endregion

        #region OnKeyDown

        private void OnKeyDown(object sender, KeyEventArgs e)
        {            
            if (m_mainFormController.CurrentView != View)
                return;

            View.m_toolTipDashboard.HideHint();

            if (e.Alt && e.KeyCode == Keys.B)
            {
                if (View.m_dashboard.Focused)
                    View.m_gridPendingVisits.Focus();
                else
                    View.m_dashboard.Focus();

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (e.Alt && e.KeyCode == Keys.Left)
            {
                View.m_dashboard.Start = View.m_dashboard.Start.AddDays(-1);
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (e.Alt && e.KeyCode == Keys.Right)
            {
                View.m_dashboard.Start = View.m_dashboard.Start.AddDays(1);
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (e.Alt && e.KeyCode == Keys.T)
            {
                View.m_dashboard.Start = DateTime.Now;
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (e.Alt && e.KeyCode == Keys.M)
            {
                View.m_dashboard.Start = DateTime.Now.AddDays(1);
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (e.Alt && e.KeyCode == Keys.Y)
            {
                View.m_dashboard.Start = DateTime.Now.AddDays(-1);
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (e.Alt && e.KeyCode == Keys.A)
            {
                View.m_btnCustomize.DoClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

        }

        #endregion

        #region OnGridPendingVisitsKeyDown

        private void OnGridPendingVisitsKeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Return && View.m_gridPendingVisits.Focused
                && View.m_gridPendingVisitsView.RowCount > 0)
            {
                if (View.m_gridPendingVisitsView.FocusedColumn.Name != View.m_colPendingMore.Name)
                    OnLinkGridVisitClick(null, null);
                else
                    OnLinkGridPendingPrintClick(null, null);
            }
        }

        #endregion

        #region OnDayTotalAmountMouseHover

        private void OnDayTotalAmountMouseHover(object sender, EventArgs e)
        {
            if (!Model.MainFormModel.CurrentDispatch.SecurityPermissions.Contains(SecurityPermissionEnum.ViewDashboardTotal))
                return;

            ToolTipControllerShowEventArgs args = View.m_toolTipDashboard.CreateShowArgs();
            args.SelectedObject = "Totals";            
            View.m_toolTipDashboard.ShowHint(args,
                View.PointToScreen(View.m_lblDayTotalAmount.Location));
        }

        private void OnDayTotalAmountMouseLeave(object sender, EventArgs e)
        {
            View.m_toolTipDashboard.HideHint();
        }

        #endregion

        #region OnDashboardKeyDown

        private void OnDashboardKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.H)
            {
                if (View.m_dashboard.SelectedAppointments.Count > 0)
                {
                    Appointment appointment = View.m_dashboard.SelectedAppointments[0];
                    ToolTipControllerShowEventArgs args = View.m_toolTipDashboard.CreateShowArgs();                    
                    args.SelectedObject = appointment;

                    AppointmentViewInfoCollection viewInfos
                        = View.m_dashboard.ActiveView.ViewInfo.AppointmentViewInfoCollection;

                    for (int i = 0; i < viewInfos.Count; i++)
                    {
                        if (viewInfos[i].Appointment == appointment)
                        {
                            View.m_toolTipDashboard.ShowHint(args,
                                View.m_dashboard.PointToScreen(viewInfos[i].Bounds.Location));
                            break;
                        }
                    }                    
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (e.Control && !e.Shift && (
                e.KeyCode == Keys.F
                || e.KeyCode == Keys.D
                || e.KeyCode == Keys.A
                || e.KeyCode == Keys.E
                || e.KeyCode == Keys.C
                || e.KeyCode == Keys.L
                || e.KeyCode == Keys.N
                || e.KeyCode == Keys.P
                || e.KeyCode == Keys.S
                || e.KeyCode == Keys.R
                || e.KeyCode == Keys.Z))
            {
                if (View.m_dashboard.SelectedAppointments.Count == 0)
                {
                    XtraMessageBox.Show("Please select Visit to preform operation", "No Visit selected",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Appointment appointment = View.m_dashboard.SelectedAppointments[0];
                AppointmentWrapper wrapper = GetWrapper(appointment);                

                if (e.KeyCode == Keys.F)
                {
                    if (wrapper.IsConfirmReconfirmVisitAllowed)
                    {
                        View.m_menuConfirmVisit.Tag = appointment;
                        OnConfirmVisitClick(View.m_menuConfirmVisit, null);
                    }
                    else
                        ShowOperationDisabledError(wrapper.ConfirmReconfirmMenuText);
                }
                else if (e.KeyCode == Keys.D)
                {
                    if (wrapper.IsDispatchRedispatchVisitAllowed)
                    {
                        View.m_menuDispatchVisit.Tag = appointment;
                        OnDispatchVisitClick(View.m_menuDispatchVisit, null);
                    }
                    else
                        ShowOperationDisabledError(wrapper.DispatchRedispatchMenuText);                    
                }
                else if (e.KeyCode == Keys.A)
                {
                    if (wrapper.IsArriveRearriveVisitAllowed)
                    {
                        View.m_menuArriveVisit.Tag = appointment;
                        OnArriveClick(View.m_menuArriveVisit, null);
                    }
                    else
                        ShowOperationDisabledError(wrapper.ArriveRearriveMenuText);                    
                }
                else if (e.KeyCode == Keys.E)
                {
                    if (wrapper.IsSubmitEtcVisitAllowed)
                    {
                        View.m_menuSubmitEtcVisit.Tag = appointment;
                        OnSubmitEtcClick(View.m_menuSubmitEtcVisit, null);
                    }
                    else
                        ShowOperationDisabledError(View.m_menuSubmitEtcVisit.Caption);                    
                }
                else if (e.KeyCode == Keys.C)
                {
                    if (wrapper.IsCompleteRecompleteVisitAllowed)
                    {
                        View.m_menuCompleteVisit.Tag = appointment;
                        OnCompleteVisitClick(View.m_menuCompleteVisit, null);
                    }
                    else
                        ShowOperationDisabledError(wrapper.CompleteRecompleteMenuText);                    
                }
                else if (e.KeyCode == Keys.L)
                {                    
                    OnDashboardVisitDetailsClick(null, null);
                }
                else if (e.KeyCode == Keys.N)
                {                    
                    OnMenuDashboardNewVisitClick(null, null);
                }
                else if (e.KeyCode == Keys.P)
                {
                    OnDashboardVisitPrintClick(null, null);
                }
                else if (e.KeyCode == Keys.R)
                {
                    if (wrapper.IsRescheduleAllowed)
                    {
                        View.m_menuRescheduleVisit.Tag = appointment;
                        OnRescheduleVisitClick(View.m_menuRescheduleVisit, null);
                    }
                    else
                        ShowOperationDisabledError(View.m_menuRescheduleVisit.Caption);                                        
                }
                else if (e.KeyCode == Keys.S)
                {
                    ResourceWrapper resourceWrapper = GetResourceWrapperByAppointment(appointment);

                    if (Model.IsSendMessageAllowed(wrapper.Work, resourceWrapper.Technician))
                    {
                        View.m_menuVisitSend.Tag = appointment;
                        OnDashboardVisitSendClick(View.m_menuVisitSend, null);
                        XtraMessageBox.Show("Visit has been sent", "Success", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                    }
                    else
                        ShowOperationDisabledError(View.m_menuVisitSend.Caption);                    
                }
                else if (e.KeyCode == Keys.Z)
                {
                    if (wrapper.IsUndoVisitAllowed)
                    {
                        View.m_menuUndoVisit.Tag = appointment;
                        OnUndoVisitClick(View.m_menuUndoVisit, null);
                    }
                    else
                        ShowOperationDisabledError(wrapper.CurrentVisitUndoMenuText);                    
                }

                return;
            }


            if (e.Control && e.Shift && (
                e.KeyCode == Keys.C
                || e.KeyCode == Keys.D
                || e.KeyCode == Keys.O
                || e.KeyCode == Keys.L
                || e.KeyCode == Keys.S
                || e.KeyCode == Keys.Z))
            {
                if (View.m_dashboard.SelectedResource == null)
                {
                    XtraMessageBox.Show("Please select Technician to preform operation", "No Technician selected",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;                    
                }

                ResourceWrapper wrapper = GetWrapper(View.m_dashboard.SelectedResource);


                if (e.KeyCode == Keys.C)
                {
                    if (IsCreateRecreateWorkAllowed(wrapper))
                    {
                        View.m_menuCreateWork.Tag = wrapper;
                        OnCreateWorkClick(View.m_menuCreateWork, null);
                    }
                    else
                        ShowOperationDisabledError(wrapper.CreateRecreateWorkMenuText);
                } 
                else if (e.KeyCode == Keys.D)
                {
                    if (wrapper.IsStartRestartDayAllowed)
                    {
                        View.m_menuStartDay.Tag = wrapper;
                        OnStartDayClick(View.m_menuStartDay, null);
                    }
                    else
                        ShowOperationDisabledError(wrapper.StartRestartDayMenuText);                    
                }
                else if (e.KeyCode == Keys.O)
                {
                    if (wrapper.IsCompleteRecompleteWorkAllowed)
                    {
                        View.m_menuCompleteWork.Tag = wrapper;
                        OnCompleteWorkClick(View.m_menuCompleteWork, null);
                    }
                    else
                        ShowOperationDisabledError(wrapper.CompleteRecompleteWorkMenuText);                    
                }
                else if (e.KeyCode == Keys.L)
                {
                    if (wrapper.IsWorkExist)
                    {
                        View.m_menuWorkDetails.Tag = wrapper;
                        OnWorkDetailsClick(View.m_menuWorkDetails, null);
                    }
                    else
                        ShowOperationDisabledError(View.m_menuWorkDetails.Caption);                    
                }
                else if (e.KeyCode == Keys.S)
                {
                    if (Model.IsSendMessageAllowed(wrapper.Work, wrapper.Technician))
                    {
                        View.m_menuSendMessage.Tag = wrapper;
                        OnSendMessageClick(View.m_menuSendMessage, null);
                    }
                    else
                        ShowOperationDisabledError(View.m_menuSendMessage.Caption);                    
                }
                else if (e.KeyCode == Keys.Z)
                {
                    if (wrapper.IsUndoWorkAllowed)
                    {
                        View.m_menuUndoWork.Tag = wrapper;
                        OnUndoWorkClick(View.m_menuUndoWork, null);
                    }
                    else
                        ShowOperationDisabledError(wrapper.CurrentWorkUndoMenuText);                    
                }
                
                return;
            }


        }

        private void ShowOperationDisabledError(string operationName)
        {
            XtraMessageBox.Show(operationName + " is not allowed", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion

        #region OnDashboardDateChanged

        private void OnDashboardDateChanged(object sender, EventArgs e)
        {            
            Model.CurrentDate = View.m_dtpDashboardDate.DateTime;
            View.m_dashboard.Start = Model.CurrentDate;
            Host.TraceUserAction("Dashboard date switched to " + View.m_dashboard.Start.ToShortDateString());
        }

        #endregion

        #region OnDashboardSelectionChanged

        private bool m_isInSelectionHandler = false;
        private TimeInterval m_previousSelection = null;

        private void OnDashboardSelectionChanged(object sender, EventArgs e)
        {   
            if (m_isInSelectionHandler)
                return;            

            if (View.m_dashboard.SelectedAppointments.Count == 0)
            {
                TimeInterval selection = View.m_dashboard.SelectedInterval;
                
                if (!(View.m_dashboard.ActiveView.SelectedResource.Id is int))
                    return;

                List<Appointment> appointments = View.m_dashboard.GetSortedAppointments(
                    (int)View.m_dashboard.ActiveView.SelectedResource.Id);                

                if (m_previousSelection != null && m_previousSelection.Start > selection.Start)
                    appointments.Reverse();


                foreach (Appointment appointment in appointments)
                {
                    TimeInterval appointmentInterval = new TimeInterval(
                        appointment.Start, appointment.End);

                    if (appointmentInterval.IntersectsWithExcludingBounds(selection))
                    {
                        m_isInSelectionHandler = true;
                        View.m_dashboard.ActiveView.SelectAppointment(appointment);
                        m_isInSelectionHandler = false;
                        return;
                    }                    
                }
            }

            m_previousSelection = View.m_dashboard.SelectedInterval;
        }

        #endregion

        #region OnNewVisitClick

        private void OnNewVisitClick(object sender, EventArgs e)
        {
            using (CreateVisitController controller = Prepare<CreateVisitController>())
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {
                    RefreshPendingVisits(null);
                    TryToSelectPendingVisit(controller.AffectedVisit);
                }                    
            }
        }

        #endregion

        #region OnPendingVisitCustomerFilterChanged

        private void OnPendingVisitCustomerFilterChanged(object sender, EventArgs e)
        {
            RefreshPendingVisits(null);            
        }

        private void OnPendingVisitCustomerFilterButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            View.m_txtPendingVisitCustomerFilter.Text = string.Empty;
        }

        #endregion

        #region OnDashboardMouseDoubleClick

        private void OnDashboardMouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point position = new Point(e.X, e.Y);
            SchedulerViewInfoBase viewInfo = View.m_dashboard.ActiveView.ViewInfo;
            SchedulerHitInfo hitInfo = viewInfo.CalcHitInfo(position, false);

            if (hitInfo.HitTest == SchedulerHitTest.AppointmentContent)
            {
                AppointmentViewInfo appointmentViewInfo = (AppointmentViewInfo)hitInfo.ViewInfo;
                AppointmentWrapper wrapper = GetWrapper(appointmentViewInfo.Appointment);
                ShowVisitEditor(wrapper.Visit, true);
            }
        }

        #endregion

        #region ShowVisitEditor

        private void ShowVisitEditor(Visit visit, bool isFromDashboard)
        {
            using (CreateVisitController controller = Prepare<CreateVisitController>(visit))
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {
                    if (isFromDashboard)
                        RefreshDashboard();
                    else
                        RefreshPendingVisits(null);                        
                }                    
            }                            
        }

        #endregion

        #region GetWrapper

        private AppointmentWrapper GetWrapper(Appointment appointment)
        {            
            return (AppointmentWrapper)appointment.GetRow(View.m_dashboardStorage);            
        }

        private ResourceWrapper GetWrapper(Resource resource)
        {
            return (ResourceWrapper)resource.GetRow(View.m_dashboardStorage);
        }

        private ResourceWrapper GetWrapper(int resourceId)
        {
            Resource resource
                = View.m_dashboardStorage.Resources.Items.GetResourceById(resourceId);
            return GetWrapper(resource);
        }

        private ResourceWrapper GetResourceWrapperByAppointment(Appointment appointment)
        {
            return GetWrapper((int)appointment.ResourceId);
        }

        private void UpdateOnUI(ResourceWrapper resource)
        {
            Resource resourceStorage
                = View.m_dashboardStorage.Resources.Items.GetResourceById(resource.ID);
            resourceStorage.Caption = resource.Caption;
        }

        #endregion

        #region SelectVisit


        private bool TryToSelectPendingVisit(Visit visit)
        {
            for (int rowIndex = 0; rowIndex < Model.PendingVisits.Count; rowIndex++)
            {
                if (Model.PendingVisits[rowIndex].Visit.ID == visit.ID)
                {
                    int rowHandle = View.m_gridPendingVisitsView.GetRowHandle(rowIndex);
                    View.m_gridPendingVisitsView.FocusedRowHandle = rowHandle;
                    return true;
                }
            }

            return false;
        }

        public void SelectVisit(Visit visit)
        {
            SelectVisit(visit, true);
        }

        public void SelectVisit(Visit visit, bool needRefresh)
        {
            if (needRefresh)
            {
                RefreshDashboard();
                RefreshPendingVisits(null);
            }

            if (visit == null)
                return;

            View.m_dashboard.SelectedAppointments.Clear();

            if (visit.VisitStatus == VisitStatusEnum.Pending) // Select in pending grid
            {
                if (!TryToSelectPendingVisit(visit))
                {
                    View.m_cmbPendingVisitsFilter.SelectedIndex = (int) PendingVisitTypeEnum.All - 1;
                    View.m_txtPendingVisitCustomerFilter.Text = string.Empty;
                    TryToSelectPendingVisit(visit);
                }                

            } else
            {
                Work work = Work.FindByVisit(visit);

                if (!DashboardUserSettings.IsTechnicianVisitble(Configuration.CurrentDispatchId, work.TechnicianEmployeeId))
                {
                    DashboardUserSettings settings = DashboardUserSettings.FindByPrimaryKey(
                        Configuration.CurrentDispatchId,
                        work.TechnicianEmployeeId);
                    settings.IsVisible = true;
                    DashboardUserSettings.Update(settings);
                    RefreshDashboard();
                }
                
                if (View.m_dashboard.Start.Date != work.StartDate.Value.Date)
                    View.m_dashboard.Start = work.StartDate.Value.Date;                                

                foreach (Appointment appointment in View.m_dashboardStorage.Appointments.Items)
                {
                    AppointmentWrapper appointmentWrapper = GetWrapper(appointment);
                    if (appointmentWrapper.Visit.ID == visit.ID)
                    {                        
                        View.m_dashboard.Views[SchedulerViewType.Day].SelectAppointment(appointment);                        
                        return;
                    }
                }                
            }
        }

        #endregion

        #region OnDashboardCustomDrawDayViewTimeRuler

        private void OnDashboardCustomDrawDayViewTimeRuler(object sender, CustomDrawObjectEventArgs e)
        {
            TimeRulerViewInfo viewInfo = (TimeRulerViewInfo)e.ObjectInfo;

            foreach (ViewInfoItem item in viewInfo.CurrentTimeItems)
            {
                if (item is TimeRulerCurrentTimelineItem)
                {
                    TimeRulerCurrentTimelineItem timeLineItem = (TimeRulerCurrentTimelineItem)item;                    
                    Pen pen = e.Cache.GetPen(Color.FromArgb(152, 123, 92));
                    e.Graphics.DrawLine(pen, timeLineItem.Bounds.X, timeLineItem.Bounds.Y + 10,
                                        View.m_dashboard.Bounds.Right, timeLineItem.Bounds.Y + 10);

                    break;
                }
            }
        }

        #endregion

        #region DrawDisabledCells

        private void DrawDisabledCells(object sender, CustomDrawObjectEventArgs e)
        {
            if (View.m_dashboard.Start.Date > DateTime.Now.Date)
                return;

            SelectableIntervalViewInfo viewInfo = e.ObjectInfo as SelectableIntervalViewInfo;
            SchedulerViewCellBase cell = e.ObjectInfo as SchedulerViewCellBase;
            if (viewInfo == null || cell == null)
                return;

            ResourceWrapper resourceWrapper = GetWrapper(viewInfo.Resource);
            if (resourceWrapper == null || resourceWrapper.Work == null)
                return;

            if ((resourceWrapper.Work.StartDayDate.HasValue
                && viewInfo.Interval.End <= resourceWrapper.Work.StartDayDate.Value)
                ||
                (resourceWrapper.Work.EndDayDate.HasValue
                && viewInfo.Interval.Start >= resourceWrapper.Work.EndDayDate.Value))
            {
                
                if (viewInfo.Selected)
                    e.Cache.FillRectangle(e.Cache.GetSolidBrush(Color.FromArgb(49, 106, 197)), cell.Bounds);
                else
                    e.Cache.FillRectangle(e.Cache.GetSolidBrush(Color.Gray), cell.Bounds);

                

                if (resourceWrapper.Work.StartDayDate.HasValue
                    && viewInfo.Interval.End <= resourceWrapper.Work.StartDayDate.Value
                    && resourceWrapper.Work.StartDayDate.Value - viewInfo.Interval.End < new TimeSpan(0, 0, 15, 0))
                {
                    e.Cache.DrawString("Start Day - " + resourceWrapper.Work.StartDayDate.Value.ToShortTimeString(),
                        new Font("Arial", 10),
                        e.Cache.GetSolidBrush(Color.White),
                        e.Bounds,
                        StringFormat.GenericDefault);
                }
                else if (resourceWrapper.Work.EndDayDate.HasValue
                    && viewInfo.Interval.Start >= resourceWrapper.Work.EndDayDate.Value
                    && viewInfo.Interval.Start - resourceWrapper.Work.EndDayDate.Value < new TimeSpan(0, 0, 15, 0))
                {
                    e.Cache.DrawString("End Day - " + resourceWrapper.Work.EndDayDate.Value.ToShortTimeString(),
                        new Font("Arial", 10),
                        e.Cache.GetSolidBrush(Color.White),
                        e.Bounds,
                        StringFormat.GenericDefault);                    
                }

                e.Handled = true;                                                    
            }
        }

        #endregion

        #region OnPendingVisitsRowStyle

        private void OnPendingVisitsRowStyle(object sender, RowStyleEventArgs e)
        {
            VisitPackage visit = (VisitPackage)View.m_gridPendingVisitsView.GetRow(e.RowHandle);
            if (visit != null && !visit.IsReady)
                e.Appearance.BackColor = Color.LightGray;
        }

        #endregion

        #region OnPendingVisitsRowCellStyle

        private void OnPendingVisitsRowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (!(View.m_columnServiceDate.Name == e.Column.Name
                || View.m_columnReady.Name == e.Column.Name))
            {
                return;
            }                

            VisitPackage visit = (VisitPackage)View.m_gridPendingVisitsView.GetRow(e.RowHandle);
       
            if (visit != null && !visit.Visit.ServiceDate.HasValue)
            {
                 if (View.m_columnServiceDate.Name == e.Column.Name 
                     && (visit.TaskTypes.Contains("Monitoring") || visit.TaskTypes.Contains("Deflood")))
                 {
                     e.Appearance.BackColor = Color.Red;
                 }
            }

            if (visit != null && visit.Visit.ServiceDate.HasValue)
            {
                if (!visit.IsReady && View.m_columnReady.Name == e.Column.Name)
                {
                    int deadLineDays = visit.Visit.ServiceDate.Value.Date.Subtract(DateTime.Now.Date).Days;

                    if (deadLineDays <= 2 && deadLineDays > 1)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;                        
                    }
                    else if (deadLineDays <= 1 && deadLineDays > 0)
                    {
                        e.Appearance.BackColor = Color.Orange;
                        e.Appearance.ForeColor = Color.Black;                        
                    }
                    else if (deadLineDays <= 0)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;                        
                    }
                }


                if (View.m_columnServiceDate.Name == e.Column.Name 
                    && visit.Visit.ServiceDate.Value.Date < DateTime.Now.Date)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;                    
                }               
            }
        }

        #endregion

        #region OnCurrentTimeTick

        private void OnCurrentTimeTick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToShortTimeString() != View.m_lblCurrentTime.Text)
                View.m_lblCurrentTime.Text = DateTime.Now.ToShortTimeString();
        }

        #endregion

        #region Prev Next Buttons

        private void OnPrevDateClick(object sender, EventArgs e)
        {
            View.m_dashboard.Start = View.m_dashboard.Start.AddDays(-1);
        }

        private void OnNextDateClick(object sender, EventArgs e)
        {
            View.m_dashboard.Start = View.m_dashboard.Start.AddDays(1);
        }

        #endregion

        #region OnDashboardVisibleIntervalChanged

        private void OnDashboardVisibleIntervalChanged(object sender, EventArgs e)
        {
            InitializeDashboardDate();
            RefreshDashboard();
            UpdateDayClosedAmounts();
        }

        #endregion        

        #region InitializeDashboardDate

        private void InitializeDashboardDate()
        {
            string dashboardDate = View.m_dashboard.Start.Date.ToShortDateString();
            View.m_dtpDashboardDate.DateTime = View.m_dashboard.Start;

            if (View.m_dashboard.Start.Date == DateTime.Now.Date)
            {
                View.m_dashboard.DayView.TimeRulers[0].ShowCurrentTime = true;
                View.m_lblDashboardDate.Text = dashboardDate + " (&Today)";
            }
            else
            {
                View.m_dashboard.DayView.TimeRulers[0].ShowCurrentTime = false;
                View.m_lblDashboardDate.Text = dashboardDate;

                if (View.m_dashboard.Start.Date == DateTime.Now.Date.AddDays(-1))
                    View.m_lblDashboardDate.Text = dashboardDate + " (&Yesterday)";
                else if (View.m_dashboard.Start.Date == DateTime.Now.Date.AddDays(1))
                    View.m_lblDashboardDate.Text = dashboardDate + " (To&morrow)";
            }
        }

        #endregion 

        #region OnCustomizeClick

        private void OnCustomizeClick()
        {         
            using (DashboardCustomizeController controller
                = Prepare<DashboardCustomizeController>(View.m_dashboard.Start.Date))
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {
                    RefreshDashboard();
                    DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
                }
            }
        }

        #endregion

        #region Drag & Drop

        private GridHitInfo downHitInfo;

        private void OnGridPendingVisitsMouseDown(object sender, MouseEventArgs e)
        {
            downHitInfo = null;

            GridHitInfo hitInfo = View.m_gridPendingVisitsView.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None)
                return;
            if (e.Button == MouseButtons.Left && hitInfo.InRow && hitInfo.HitTest != GridHitTest.RowIndicator)
                downHitInfo = hitInfo;
        }

        private void OnGridPendingTasksMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {                    
                    VisitPackage visit = (VisitPackage) View.m_gridPendingVisitsView.GetRow(
                        View.m_gridPendingVisitsView.FocusedRowHandle);                    

                    SchedulerDragData dragData = GetDragData(View.m_gridPendingVisitsView);

                    if (visit != null && !visit.IsReady)
                        View.m_gridPendingVisitsView.GridControl.DoDragDrop(dragData, DragDropEffects.None);
                    else if (visit != null)
                    {
                        m_isAppointmentMoving = true;
                        m_currentDraggedVisit = visit.Visit;
                        DragDropEffects dragResult = View.m_gridPendingVisitsView.GridControl.DoDragDrop(dragData, DragDropEffects.All);
                        if (!m_isAppointmentInsertCancelled && dragResult != DragDropEffects.None)
                            MergeVisits(visit);
                    }                        

                    downHitInfo = null;
                }
            }
        }

        private SchedulerDragData GetDragData(GridView view)
        {
            int[] selection = view.GetSelectedRows();
            if (selection == null)
                return null;

            AppointmentBaseCollection appointments = new AppointmentBaseCollection();
            int count = selection.Length;
            for (int i = 0; i < count; i++)
            {
                int rowIndex = selection[i];
                Appointment appointment 
                    = View.m_dashboardStorage.CreateAppointment(AppointmentType.Normal);
               
                VisitPackage pendingVisit = (VisitPackage)view.GetRow(rowIndex);
                Visit visit = pendingVisit.Visit;
                visit.VisitStatus = VisitStatusEnum.Assigned;

                List<Task> tasks = new List<Task>();
                foreach (TaskPackage task in pendingVisit.Tasks)
                    tasks.Add(task.Task);

                AppointmentWrapper wrapper = new AppointmentWrapper(
                    visit, new WorkDetail(), new Work(), 
                    pendingVisit.Customer, 
                    pendingVisit.ServiceAddress,
                    tasks);

                appointment.CustomFields["AppointmentWrapper"] = wrapper;

                appointment.Subject = wrapper.Subject;
                appointment.LabelId = wrapper.Label;
                appointment.Duration = wrapper.DurationCorrected;

                appointments.Add(appointment);
            }

            return new SchedulerDragData(appointments, 0);
        }       

        #endregion

        #region MergeVisits

        private void MergeVisits(VisitPackage visitPackage)
        {
            foreach (TaskPackage task in visitPackage.Tasks)
            {
                if (task.Task.TaskType == TaskTypeEnum.Help)
                    return;
            }

            Visit visit = visitPackage.Visit;
            bool isMergePerformed = false;
            VisitSummaryPackage initialVisitSummary = new VisitSummaryPackage(visit);

            //Merge with dashboard visits
            List<Visit> mergableDashboardVisits = Visit.FindMergableDashboardVisits(
                visit, View.m_dashboard.Start.Date);

            if (mergableDashboardVisits.Count == 1)
            {
                Work work = Work.FindByVisit(mergableDashboardVisits[0]);
                Employee technician = Employee.FindByPrimaryKey(work.TechnicianEmployeeId);

                if (XtraMessageBox.Show(
                    string.Format("There is visit for {0} already placed on dashboard and assigned to {1}.\r\nWould you like to merge current visit with existing one?", 
                    visitPackage.Customer.DisplayName, technician.DisplayName),
                    "Assigned Visits Merge", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Visit tempDraggedVisit = visit;
                    visit = mergableDashboardVisits[0];
                    mergableDashboardVisits.Clear();
                    mergableDashboardVisits.Add(tempDraggedVisit);

                    try
                    {
                        Database.Begin();
                        Visit.MergeVisits(visit, mergableDashboardVisits);
                        Database.Commit();
                        isMergePerformed = true;
                    }
                    catch (Exception)
                    {
                        Database.Rollback();
                        throw;
                    }
                    
                }
            } else if (mergableDashboardVisits.Count > 0)
            {
                using (VisitMergeController controller = Prepare<VisitMergeController>(
                    visit, mergableDashboardVisits))
                {
                    controller.Execute(false);
                    if (!controller.IsCancelled)
                    {
                        List<Visit> mergableVisits = new List<Visit>();
                        mergableVisits.Add(visit);
                        visit = controller.SelectedVisits[0];

                        try
                        {
                            Database.Begin();
                            Visit.MergeVisits(visit, mergableVisits);
                            Database.Commit();
                            isMergePerformed = true;
                        }
                        catch (Exception)
                        {
                            Database.Rollback();
                            throw;
                        }
                    }
                }                
            }


            //Merge with pending visits
            List<Visit> mergablePendingVisits = Visit.FindMergablePendingVisits(
                visit, View.m_dashboard.Start.Date);

            if (mergablePendingVisits.Count > 0)
            {
                using (VisitMergeController controller = Prepare<VisitMergeController>(
                    visit, mergablePendingVisits))
                {
                    controller.Execute(false);
                    if (!controller.IsCancelled)
                    {
                        try
                        {
                            Database.Begin();
                            Visit.MergeVisits(visit, controller.SelectedVisits);
                            Database.Commit();
                            isMergePerformed = true;
                        }
                        catch (Exception)
                        {
                            Database.Rollback();
                            throw;
                        }
                    }
                }
            }

            if (isMergePerformed)
            {
                if (visit.IsNeedToPrint(initialVisitSummary, false))
                {
                    try
                    {
                        using (new WaitCursor())
                        {
                            VisitSummaryPackage visitSummary = new VisitSummaryPackage(visit);
                            visitSummary.Print();                        
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "Unable to print visit",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                RefreshDashboard();
                RefreshPendingVisits(null);
                DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
                PendingTaskGridState.MakePendingTaskGridDirty(Model.MainFormModel.CurrentDispatch.ID);        
            }
        }        

        #endregion

        #region Jumps to Visit details

        private void OnLinkGridVisitClick(object sender, EventArgs e)
        {
            VisitPackage package = SelectedPendingVisit;
            ShowVisitEditor(package.Visit, false);            
        }

        private void OnDashboardVisitDetailsClick(object sender, EventArgs e)
        {
            if (View.m_dashboard.SelectedAppointments.Count == 1)
            {
                Appointment appointment = View.m_dashboard.SelectedAppointments[0];                
                AppointmentWrapper appointmentWrapper = GetWrapper(appointment);
                ShowVisitEditor(appointmentWrapper.Visit, true);
            }
        }

        #endregion

        #region OnMenuDashboardNewVisitClick

        private void OnMenuDashboardNewVisitClick(object sender, EventArgs e)
        {
            if (View.m_dashboard.SelectedAppointments.Count == 1)
            {
                Appointment appointment = View.m_dashboard.SelectedAppointments[0];
                AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

                using (CreateVisitController editVisitcontroller = Prepare<CreateVisitController>(
                    null, null, null, appointmentWrapper.Visit))
                {
                    editVisitcontroller.Execute(false);
                    if (!editVisitcontroller.IsCancelled)
                    {
                        RefreshPendingVisits(null);
                        TryToSelectPendingVisit(editVisitcontroller.AffectedVisit);
                    }  
                }                    
            }            
        }

        #endregion

        #region OnLinkGridPendingPrintClick

        private void OnLinkGridPendingPrintClick(object sender, EventArgs e)
        {
            if (View.m_gridPendingVisits.Focused
                && View.m_gridPendingVisitsView.FocusedColumn.Name == View.m_colPendingMore.Name)
            {
                GridViewInfo info = (GridViewInfo)View.m_gridPendingVisitsView.GetViewInfo();
                GridCellInfo cell = info.GetGridCellInfo(View.m_gridPendingVisitsView.FocusedRowHandle,
                    View.m_colPendingMore.AbsoluteIndex);

                if (cell != null)
                {
                    Rectangle rectangle = cell.Bounds;
                    Point position = new Point(rectangle.Location.X, rectangle.Location.Y + rectangle.Height);
                    View.m_menuPendingVisitAction.ShowPopup(View.m_gridPendingVisits.PointToScreen(position));
                    View.m_menuPendingVisitAction.ItemLinks[0].Focus();
                }                  
            }            
        }

        private void OnMenuPrintVisitClick(object sender, ItemClickEventArgs e)
        {
            VisitPackage package = SelectedPendingVisit;

            try
            {
                using (new WaitCursor())
                {
                    VisitSummaryPackage summaryPackage = new VisitSummaryPackage(package.Visit);
                    summaryPackage.Print();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Unable to print visit",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }               
        }

        private void OnMenuSplitVisitClick(object sender, ItemClickEventArgs e)
        {
            VisitPackage package = SelectedPendingVisit;
            if (!Visit.IsSplitPossible(package.Visit))
            {
                XtraMessageBox.Show("Visit split impossible. Visit should contain at least 2 tasks",
                    "Split impossible", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (VisitSplitController controller = Prepare<VisitSplitController>(package.Visit))
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                    RefreshPendingVisits(null);                    
            }
        }

        private void OnMenuPendingVisitNewVisitClick(object sender, ItemClickEventArgs e)
        {
            VisitPackage package = SelectedPendingVisit;

            using (CreateVisitController controller = Prepare<CreateVisitController>(
                null, null, null, package.Visit))
            {
                controller.Execute(false);
            }
        }

        #endregion


        #region OnDashboardVisitPrintClick

        private void OnDashboardVisitPrintClick(object sender, EventArgs e)
        {
            if (View.m_dashboard.SelectedAppointments.Count == 1)
            {
                Appointment appointment = View.m_dashboard.SelectedAppointments[0];                
                AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

                try
                {
                    using (new WaitCursor())
                    {
                        VisitSummaryPackage summaryPackage = new VisitSummaryPackage(appointmentWrapper.Visit);
                        summaryPackage.Print();                        
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Unable to print visit",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        #endregion

        #region OnDashboardVisitSendClick

        private void OnDashboardVisitSendClick(object sender, EventArgs e)
        {
            Appointment appointment = (Appointment)((DXMenuItem)sender).Tag;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);
            ResourceWrapper resourceWrapper = GetResourceWrapperByAppointment(appointment);

            using (new WaitCursor())
                Model.SendVisitToPager(resourceWrapper.Work, appointmentWrapper.Visit);
        }

        #endregion

        #region OnBeforeToolTipShow

        private void OnBeforeToolTipShow(object sender, ToolTipControllerShowEventArgs e)
        {   
            if (e.SelectedObject == null)
            {
                e.Show = false;
                return;
            }

            if (e.SelectedObject is string && (string)e.SelectedObject == "Totals")
            {
                e.Title = "Closed Amounts";
                e.IconType = ToolTipIconType.Information;

                DepartmentClosedAmounts amounts = Work.FindDepartmentClosedAmounts(
                    View.m_dashboard.Start.Date);

                string toolTipText = string.Empty;

                toolTipText += "Rug Cleaning Department    " + amounts.RugCleaningAmount.ToString("C");
                toolTipText += "\r\nRestoration Depatrment      " + amounts.RestorationAmount.ToString("C");

                e.ToolTip = toolTipText;
                return;
            }

            if (e.SelectedObject is VerticalAppointmentViewInfo || e.SelectedObject is Appointment)
            {
                Appointment appointment;

                if (e.SelectedObject is VerticalAppointmentViewInfo)
                    appointment = (e.SelectedObject as VerticalAppointmentViewInfo).Appointment;
                else
                    appointment = (Appointment) e.SelectedObject;

                AppointmentWrapper wrapper = GetWrapper(appointment);

                e.Title = wrapper.ToolTipTitle;
                e.IconType = wrapper.ToolTipIcon;
                e.ToolTip = wrapper.ToolTipText;
            }
            else if (!(e.SelectedObject is NavigatorButton || e.SelectedObject is NavigatorCustomButton2))
                e.Show = false;
        }

        #endregion        

        #region OnAllowAppointmentDragOrResize

        private void OnAllowAppointmentDragOrResize(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = GetWrapper(e.Appointment).Visit.IsEditAllowed;
        }

        #endregion        

        #region OnAppointmentDrag

        private void OnAppointmentDrag(object sender, AppointmentDragEventArgs e)
        {            
            if (e.EditedAppointment.Start.Minute % 15 != 0)
                e.EditedAppointment.Start = Utils.RoundTo15Min(e.EditedAppointment.Start);

            AppointmentWrapper resultWrapper = UpdateDraggedAppointment(e.EditedAppointment, false);

            if (!resultWrapper.IsTimeAssignmentAllowed)
            {
                e.Handled = true;
                e.Allow = false;
                return;
            } 
        }

        private AppointmentWrapper UpdateDraggedAppointment(Appointment appointment, bool isResize)
        {
            AppointmentWrapper wrapper = GetWrapper(appointment);

            if (wrapper == null)
                wrapper = (AppointmentWrapper)appointment.CustomFields["AppointmentWrapper"];

            AppointmentWrapper clonedWrapper = (AppointmentWrapper)wrapper.Clone();

            clonedWrapper.WorkDetail.TimeBegin = appointment.Start;
            clonedWrapper.WorkDetail.TimeEnd = appointment.End;
            wrapper.WorkDetail.TimeBegin = clonedWrapper.WorkDetail.TimeBegin;
            wrapper.WorkDetail.TimeEnd = clonedWrapper.WorkDetail.TimeEnd;

            appointment.Description = clonedWrapper.Description;
            appointment.StatusId = clonedWrapper.Status;
            appointment.Subject = clonedWrapper.Subject;

            if (!isResize && (int)appointment.ResourceId != clonedWrapper.ResourceId)
            {
                ResourceWrapper resourceWrapper = GetWrapper((int)appointment.ResourceId);
                clonedWrapper.Work = resourceWrapper.Work;
            }

            return clonedWrapper;
        }

        #endregion

        #region OnAppointmentsChanged

        private void OnAppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {
            if (m_isDashboardRefreshing)
                return;
            
            foreach (object o in e.Objects)
            {
                Appointment appointment = (Appointment) o;
                AppointmentWrapper wrapper = GetWrapper(appointment);
                appointment.Subject = wrapper.Subject;

                List<ResourceWrapper> modifiedResources;
                try
                {
                    modifiedResources = wrapper.Save(Model.MainFormModel.CurrentDispatch.ID);
                }
                catch (DataOutdatedException)
                {
                    DashboardState.MakeDashboardDirty();
                    View.m_timer.Interval = 1;                    
                    return;
                }
                
                //manual resource captions update
                foreach (ResourceWrapper modifiedResource in modifiedResources)
                {
                    Resource resource
                        = View.m_dashboardStorage.Resources.Items.GetResourceById(modifiedResource.ID);
                    ResourceWrapper resourceWrapper = GetWrapper(resource);
                    modifiedResource.ResourceIndex = resourceWrapper.ResourceIndex;
                    resourceWrapper.Work = modifiedResource.Work;
                    resourceWrapper.Technician = modifiedResource.Technician;
                    resource.Caption = modifiedResource.Caption;
                }                
            }                        

            DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
        }

        #endregion        

        #region OnDashboardAppointmentDeleting

        private void OnUnassignVisitClick(object sender, EventArgs e)
        {
            Appointment appointment = (Appointment)((DXMenuItem)sender).Tag;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

            appointmentWrapper.Refresh();
            if (!appointmentWrapper.IsUnassignAllowed)
            {
                RefreshDashboard();
                return;
            }

            View.m_dashboard.DeleteSelectedAppointments();
        }

        private void UnassignVisit(AppointmentWrapper wrapper)
        {
            try
            {
                Database.Begin();
                WorkDetail.Delete(wrapper.WorkDetail);
                wrapper.Visit.VisitStatus = VisitStatusEnum.Pending;
                Visit.Update(wrapper.Visit);
                Database.Commit();
                Host.TraceUserAction(string.Format("Visit {0} unassigned", wrapper.Visit.ID));
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            bool isWorkDeleted = false;
            try
            {
                if (wrapper.Work.WorkStatus == WorkStatusEnum.Pending)
                {
                    Work.Delete(wrapper.Work);
                    isWorkDeleted = true;
                }
            }
            catch (Exception) { }

            RefreshPendingVisits(null);

            if (isWorkDeleted)
            {
                RefreshDashboardResources();
                Resource resource
                    = View.m_dashboardStorage.Resources.Items.GetResourceById(wrapper.ResourceId);
                ResourceWrapper resourceWrapper = GetWrapper(resource);
                resource.Caption = resourceWrapper.Caption;
            }


            DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
            PendingTaskGridState.MakePendingTaskGridDirty(Model.MainFormModel.CurrentDispatch.ID);            
        }

        private void OnDashboardAppointmentDeleting(object sender, PersistentObjectCancelEventArgs e)
        {
            Appointment appointment = (Appointment)e.Object;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

            if (appointmentWrapper.IsUnassignAllowed)
            {
                if (XtraMessageBox.Show("Do you really want to unassign this visit?", "Visit unassign", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UnassignVisit(appointmentWrapper);
                    return;
                }
            }

            e.Cancel = true;
        }

        #endregion

        #region OnRescheduleVisitClick

        private void OnRescheduleVisitClick(object sender, EventArgs e)
        {
            Appointment appointment = (Appointment)((DXMenuItem)sender).Tag;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

            using (CreateVisitController controller = Prepare<CreateVisitController>(
                appointmentWrapper.Visit, false, true))
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {
                    appointmentWrapper.Visit = controller.AffectedVisit;

                    try
                    {
                        Database.Begin();                            
                        Model.UndoAllVisitOperations(appointmentWrapper);
                        Database.Commit();                        
                        UnassignVisit(appointmentWrapper);
                        RefreshDashboard();
                        DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
                    }
                    catch (DataOutdatedException)
                    {
                        RefreshDashboard();
                        Database.Rollback();
                    }                    
                    catch (Exception)
                    {
                        Database.Rollback();
                        throw;
                    }                    
                }
            }
        }

        #endregion


        #region OnAppointmentChanging

        private void OnAppointmentChanging(object sender, PersistentObjectCancelEventArgs e)
        {
            if (m_isDashboardRefreshing)
                return;

            Appointment appointment = (Appointment)e.Object;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);
            ResourceWrapper targetResource = GetResourceWrapperByAppointment(appointment);
            Host.TraceUserAction(string.Format("Visit {0} moved to {1}",
                appointmentWrapper.Visit.ID, targetResource.Caption));

            if (targetResource.ID != appointmentWrapper.ResourceId
                && targetResource.IsWorkExist
                && appointmentWrapper.Work.StartDate.Value.Date == DateTime.Now.Date
                && targetResource.Work.WorkStatus == WorkStatusEnum.StartDayDone
                && appointmentWrapper.IsContainsRugDelivery())
            {                            
                if (XtraMessageBox.Show("You are about to assign Rug Delivery visit to technician who has already started work day.\nAre you sure "
                    + targetResource.Technician.DisplayName.Replace(",", string.Empty) 
                    + " has all the rugs to be delivered?", "Confirmation", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    UndoUserAppointmentChanges(appointmentWrapper, appointment);
                }                                                                    
            }                        
        }

        #endregion


        #region UndoUserAppointmentChanges

        private void UndoUserAppointmentChanges(AppointmentWrapper wrapper, Appointment appointment)
        {
            Work work = Work.FindByPrimaryKey(wrapper.Work.ID);
            WorkDetail workDetail = WorkDetail.FindByPrimaryKey(wrapper.WorkDetail.ID);
            VisitPackage visitPackage = VisitPackage.GetVisit(wrapper.Visit.ID, null);

            List<Task> tasks = new List<Task>();
            foreach (TaskPackage task in visitPackage.Tasks)
                tasks.Add(task.Task);

            AppointmentWrapper temp = new AppointmentWrapper(
                visitPackage.Visit,
                workDetail,
                work,
                visitPackage.Customer,
                visitPackage.ServiceAddress,
                tasks);

            m_isDashboardRefreshing = true;
            appointment.ResourceId = temp.ResourceId;
            appointment.Start = temp.Start;
            appointment.End = temp.End;
            appointment.StatusId = temp.Status;
            appointment.Description = temp.Description;
            m_isDashboardRefreshing = false;
        }

        #endregion

        #region OnAppointmentInserting

        private bool m_isAppointmentInsertCancelled = false;
        private void OnAppointmentInserting(object sender, PersistentObjectCancelEventArgs e)
        {
            if (m_isDashboardRefreshing)
                return;

            m_isAppointmentInsertCancelled = false;

            Appointment appointment = (Appointment) e.Object;
            AppointmentWrapper appointmentWrapper 
                = (AppointmentWrapper) appointment.CustomFields["AppointmentWrapper"];
            ResourceWrapper resourceWrapper = GetResourceWrapperByAppointment(appointment);
            Host.TraceUserAction(string.Format("Visit {0} placed on {1}", 
                appointmentWrapper.Visit.ID, resourceWrapper.Caption));

            foreach (AppointmentWrapper wrapper in Model.Appointments)
            {
                if (wrapper.Visit.ID == appointmentWrapper.Visit.ID)
                {
                    e.Cancel = true;
                    m_isAppointmentInsertCancelled = true;
                    return;
                }                    
            }

            if (appointmentWrapper.Visit.ServiceDate.HasValue
                && appointmentWrapper.Visit.ServiceDate.Value.Date != View.m_dashboard.Start.Date)
            {
                if (XtraMessageBox.Show("You are about to assign Visit on date different from requested Service Date.\r\nDo you want to continue?",
                        "Service Date mismatch", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    m_isAppointmentInsertCancelled = true;
                    return;
                }
            }

            if (resourceWrapper.IsWorkExist
                && resourceWrapper.Work.StartDate.Value.Date == DateTime.Now.Date
                && resourceWrapper.Work.WorkStatus == WorkStatusEnum.StartDayDone
                && appointmentWrapper.IsContainsRugDelivery())
            {
                Employee technician = Employee.FindByPrimaryKey(resourceWrapper.ID);

                if (XtraMessageBox.Show("You are about to assign Rug Delivery visit to technician who has already started work day.\nAre you sure "
                    + technician.DisplayName.Replace(",", string.Empty) + " has all the rugs to be delivered?", "Confirmation", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    m_isAppointmentInsertCancelled = true;
                    return;
                }
            } 

            if (!resourceWrapper.IsWorkExist) //Create Work
            {
                WorkPackage package = new WorkPackage();
                package.Dispatch = Model.MainFormModel.CurrentDispatch;
                package.Technician = resourceWrapper.Technician;
                package.Van = null;
                package.Work = new Work();
                package.Work.StartDate = appointment.Start;
                package.Work.WorkStatus = WorkStatusEnum.Pending;                   
                package.Work.StartMessage = string.Empty;
                package.Work.EndMessage = string.Empty;
                package.Work.EquipmentNotes = string.Empty;

                WorkDetail workDetail = new WorkDetail();
                workDetail.VisitId = appointmentWrapper.Visit.ID;
                workDetail.Sequence = 0;
                workDetail.WorkDetailStatusId = null;
                workDetail.TimeBegin = appointment.Start;
                workDetail.TimeEnd = appointment.End;                

                package.WorkDetails = new List<WorkDetail>();
                package.WorkDetails.Add(workDetail);                

                package.WorkEquipments = new List<WorkEquipment>();

                try
                {
                    Database.Begin(IsolationLevel.RepeatableRead);
                    WorkPackage.CreateWork(package);                    
                    Database.Commit();
                    DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);

                    appointmentWrapper.Work = package.Work;
                    appointmentWrapper.WorkDetail = package.WorkDetails[0];
                    resourceWrapper.Work = package.Work;

                    Model.Appointments.Add(appointmentWrapper);
                    Model.Appointments.ResetBindings();
                    UpdateOnUI(resourceWrapper);
                }
                catch (Exception ex)
                {
                    Host.Trace("DashboardController", ex.ToString());
                    Database.Rollback();
                    throw ex;
                }                
            } else //Add visit to existing work
            {
                if (WorkDetail.Exists(resourceWrapper.Work, appointmentWrapper.Visit, null))
                {
                    e.Cancel = true;
                    m_isAppointmentInsertCancelled = true;
                    return;
                }

                WorkDetail detail = new WorkDetail();
                detail.TimeBegin = appointment.Start;
                detail.TimeEnd = appointment.End;

                resourceWrapper.Work.AddVisit(appointmentWrapper.Visit, detail);
                appointmentWrapper.Work = resourceWrapper.Work;
                appointmentWrapper.WorkDetail = detail;
                resourceWrapper.Work = resourceWrapper.Work;

                Model.Appointments.Add(appointmentWrapper);
                Model.Appointments.ResetBindings();
                UpdateOnUI(resourceWrapper);
            }

            RefreshPendingVisits(null);
            DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
            PendingTaskGridState.MakePendingTaskGridDirty(Model.MainFormModel.CurrentDispatch.ID);
        }

        #endregion       

        #region OnPreparePopupMenu

        private void OnPreparePopupMenu(object sender, PreparePopupMenuEventArgs e)
        {
            Point pt = View.m_dashboard.PointToClient(Control.MousePosition);
            SchedulerHitInfo hitInfo = View.m_dashboard.ActiveView.ViewInfo.CalcHitInfo(pt, false);

            if (e.Menu.Id == SchedulerMenuItemId.AppointmentMenu                
                && hitInfo.NextHitInfo.HitTest != SchedulerHitTest.ResourceHeader
                && View.m_dashboard.SelectedAppointments.Count == 1) //appointment popup menu
            {
                e.Menu = View.m_menuPopupAppointment;

                Appointment appointment = View.m_dashboard.SelectedAppointments[0];
                AppointmentWrapper appointmentWrapper = GetWrapper(appointment);
                ResourceWrapper resourceWrapper = GetResourceWrapperByAppointment(appointment);

                View.m_menuUnassignVisit.Enabled = appointmentWrapper.IsUnassignAllowed;
                View.m_menuUnassignVisit.Tag = appointment;

                View.m_menuRescheduleVisit.Enabled = appointmentWrapper.IsRescheduleAllowed;
                View.m_menuRescheduleVisit.Tag = appointment;

                View.m_menuDispatchVisit.Enabled = appointmentWrapper.IsDispatchRedispatchVisitAllowed;
                View.m_menuDispatchVisit.Caption = appointmentWrapper.DispatchRedispatchMenuText;
                View.m_menuDispatchVisit.Tag = appointment;

                View.m_menuConfirmVisit.Enabled = appointmentWrapper.IsConfirmReconfirmVisitAllowed;
                View.m_menuConfirmVisit.Caption = appointmentWrapper.ConfirmReconfirmMenuText;
                View.m_menuConfirmVisit.Tag = appointment;

                View.m_menuArriveVisit.Enabled = appointmentWrapper.IsArriveRearriveVisitAllowed;
                View.m_menuArriveVisit.Caption = appointmentWrapper.ArriveRearriveMenuText;
                View.m_menuArriveVisit.Tag = appointment;

                View.m_menuSubmitEtcVisit.Enabled = appointmentWrapper.IsSubmitEtcVisitAllowed;
                View.m_menuSubmitEtcVisit.Tag = appointment;

                View.m_menuCompleteVisit.Enabled = appointmentWrapper.IsCompleteRecompleteVisitAllowed;
                View.m_menuCompleteVisit.Caption = appointmentWrapper.CompleteRecompleteMenuText;
                View.m_menuCompleteVisit.Tag = appointment;

                View.m_menuUndoVisit.Enabled = appointmentWrapper.IsUndoVisitAllowed;
                View.m_menuUndoVisit.Caption = appointmentWrapper.CurrentVisitUndoMenuText;
                View.m_menuUndoVisit.Tag = appointment;

                View.m_menuVisitSend.Enabled =
                    Model.IsSendMessageAllowed(resourceWrapper.Work, resourceWrapper.Technician);
                View.m_menuVisitSend.Tag = appointment;                
                
            } else 
            {
                e.Menu.Items.Clear();

                if (hitInfo.HitTest == SchedulerHitTest.ResourceHeader
                    || hitInfo.NextHitInfo.HitTest == SchedulerHitTest.ResourceHeader) //Resource popup menu
                {
                    e.Menu = View.m_menuPopupResource;

                    ResourceWrapper resourceWrapper;

                    if (hitInfo.HitTest == SchedulerHitTest.ResourceHeader)
                        resourceWrapper = GetWrapper(hitInfo.ViewInfo.Resource);
                    else
                        resourceWrapper = GetWrapper(hitInfo.NextHitInfo.ViewInfo.Resource);

                    View.m_menuCreateWork.Caption = resourceWrapper.CreateRecreateWorkMenuText;
                    View.m_menuCreateWork.Enabled = IsCreateRecreateWorkAllowed(resourceWrapper);
                    View.m_menuCreateWork.Tag = resourceWrapper;

                    View.m_menuStartDay.Caption = resourceWrapper.StartRestartDayMenuText;
                    View.m_menuStartDay.Enabled = resourceWrapper.IsStartRestartDayAllowed;
                    View.m_menuStartDay.Tag = resourceWrapper;

                    View.m_menuCompleteWork.Caption = resourceWrapper.CompleteRecompleteWorkMenuText;
                    View.m_menuCompleteWork.Enabled = resourceWrapper.IsCompleteRecompleteWorkAllowed;
                    View.m_menuCompleteWork.Tag = resourceWrapper;

                    View.m_menuUndoWork.Enabled = resourceWrapper.IsUndoWorkAllowed;
                    View.m_menuUndoWork.Caption = resourceWrapper.CurrentWorkUndoMenuText;
                    View.m_menuUndoWork.Tag = resourceWrapper;

                    View.m_menuWorkDetails.Enabled = resourceWrapper.IsWorkExist;
                    View.m_menuWorkDetails.Tag = resourceWrapper;

                    View.m_menuSendMessage.Enabled 
                        = Model.IsSendMessageAllowed(resourceWrapper.Work, resourceWrapper.Technician);
                    View.m_menuSendMessage.Tag = resourceWrapper;
                }
            }
        }

        #endregion

        #region IsCreateRecreateWorkAllowed

        private bool IsCreateRecreateWorkAllowed(ResourceWrapper resourceWrapper)
        {
            return resourceWrapper.IsCreateRecreateWorkAllowed
                   && View.m_dashboard.Start.Date <= DateTime.Now.Date;
        }

        #endregion


        #region OnCreateWorkClick

        private void OnCreateWorkClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = (DXMenuItem)sender;            
            ResourceWrapper resourceWrapper = (ResourceWrapper)menuItem.Tag;

            if (resourceWrapper.Work == null)
            {
                resourceWrapper.Work = new Work();
                resourceWrapper.Work.DispatchEmployeeId = Configuration.CurrentDispatchId;
                resourceWrapper.Work.TechnicianEmployeeId = resourceWrapper.Technician.ID;
                resourceWrapper.Work.StartDate = View.m_dashboard.Start.Date;
                resourceWrapper.Work.WorkStatus = WorkStatusEnum.Pending;
                resourceWrapper.Work.CreateDate = DateTime.Now;                
            }

            using (CreateWorkController controller
                = Prepare<CreateWorkController>(Model.MainFormModel.CurrentDispatch, resourceWrapper.Work))
            {
                controller.Execute(false);
            }
            RefreshDashboard();
            DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
        }

        #endregion        

        #region OnStartDayClick

        private void OnStartDayClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = (DXMenuItem)sender;
            ResourceWrapper resourceWrapper = (ResourceWrapper)menuItem.Tag;

            using (StartDayController controller = Prepare<StartDayController>(resourceWrapper.Work))
            {
                controller.Execute(false);

                if (!controller.IsCancelled)
                {
                    RefreshDashboard();
                    DashboardState.MakeDashboardDirty(Configuration.CurrentDispatchId);
                }
            }
        }

        #endregion

        #region OnCompleteWorkClick

        private void OnCompleteWorkClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = (DXMenuItem)sender;
            ResourceWrapper resourceWrapper = (ResourceWrapper)menuItem.Tag;

            using (EndDayController controller = Prepare<EndDayController>(resourceWrapper.Work))
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {
                    RefreshDashboard();
                    DashboardState.MakeDashboardDirty(Configuration.CurrentDispatchId);
                }
            }            
        }

        #endregion

        #region OnUndoWorkClick

        private void OnUndoWorkClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = (DXMenuItem)sender;
            ResourceWrapper resourceWrapper = (ResourceWrapper)menuItem.Tag;

            if (Work.GetWorkUndoAllowance(resourceWrapper.Work) == WorkUndoAllwanceEnum.NotAllowedProcessedVisitsExist)
            {
                XtraMessageBox.Show(
                    "Undo operation is not allowed. Processed visits exists on this work day. In order to perform Undo you need to have undone all the processed visits",
                    "Undo is not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string undoOperationName = string.Empty;
            WorkUndoOperationEnum operation = resourceWrapper.CurrentWorkUndoOperation;
            if (operation == WorkUndoOperationEnum.CreateWork)
                undoOperationName = "Create Work";
            else if (operation == WorkUndoOperationEnum.StartDay)
                undoOperationName = "Start Day";
            else if (operation == WorkUndoOperationEnum.CompleteWork)
                undoOperationName = "Complete Work";

            if (XtraMessageBox.Show(
                    "Do really want to Undo " + undoOperationName + "? If yes you will loose all the data entered on " + undoOperationName,
                    "Confirm Undo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                Database.Begin();
                Work.UndoLastOperation(resourceWrapper.Work);
                Database.Commit();
                RefreshDashboard();
                DashboardState.MakeDashboardDirty(Configuration.CurrentDispatchId);
                PendingTaskGridState.MakePendingTaskGridDirty(Model.MainFormModel.CurrentDispatch.ID);
                Host.TraceUserAction(string.Format("Undo {0} for {1}", 
                    undoOperationName, resourceWrapper.Caption));
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region OnWorkDetailsClick

        private void OnWorkDetailsClick(object sender, EventArgs e)
        {
            ResourceWrapper resourceWrapper = (ResourceWrapper)((DXMenuItem)sender).Tag;
            m_mainFormController.ShowWorksForm(resourceWrapper.Work);
        }

        #endregion

        #region OnSendMessageClick

        private void OnSendMessageClick(object sender, EventArgs e)
        {
            ResourceWrapper resourceWrapper = (ResourceWrapper)((DXMenuItem)sender).Tag;

            using (SendMessageController controller = Prepare<SendMessageController>(resourceWrapper.Work))
            {
                controller.Execute(false);
            }            
        }

        #endregion

        #region OnUndoVisitClick

        private void OnUndoVisitClick(object sender, EventArgs e)
        {
            Appointment appointment = (Appointment)((DXMenuItem)sender).Tag;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

            appointmentWrapper.Refresh();
            if (!appointmentWrapper.IsUndoVisitAllowed)
            {
                RefreshDashboard();
                return;
            }
            
            string undoOperationName = string.Empty;
            VisitUndoOperationEnum operation = appointmentWrapper.CurrentVisitUndoOperation;

            if (operation == VisitUndoOperationEnum.Complete)
                undoOperationName = "Complete";
            else if (operation == VisitUndoOperationEnum.Arrive)
                undoOperationName = "Arrive";
            else if (operation == VisitUndoOperationEnum.Dispatch)
                undoOperationName = "Dispatch";
            else if (operation == VisitUndoOperationEnum.Confirm)
                undoOperationName = "Confirm";

            List<Task> modifiedTasks = Task.FindByVisit(appointmentWrapper.Visit);
            if (operation == VisitUndoOperationEnum.Complete)
                modifiedTasks.AddRange(Task.FindBookedTasksOnCompletion(appointmentWrapper.Visit));

            string allowanceError = Visit.GetVisitEditUndoAllowance(
                appointmentWrapper.Visit, modifiedTasks, true, false);

            if (allowanceError != string.Empty)
            {
                XtraMessageBox.Show(
                    allowanceError, "Undo is not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;                
            }

            if (XtraMessageBox.Show(
                    "Do really want to Undo " + undoOperationName + "? If yes you will loose all the data entered on " + undoOperationName,
                    "Confirm Undo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                Database.Begin();
                Visit.UndoLastOperation(appointmentWrapper.Visit, appointmentWrapper.Work);
                DashboardState.MakeDashboardDirty(Configuration.CurrentDispatchId);
                Database.Commit();
                Host.TraceUserAction(string.Format("Undo {0} for Visit {1}",
                    undoOperationName, appointmentWrapper.Visit.ID));
                RefreshDashboard();
                RefreshPendingVisits(null);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            if (operation == VisitUndoOperationEnum.Complete)
                UpdateDayClosedAmounts();
        }

        #endregion

        #region Complete Visit

        private void OnCompleteVisitClick(object sender, EventArgs e)
        {
            Appointment appointment = (Appointment) ((DXMenuItem) sender).Tag;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

            appointmentWrapper.Refresh();
            if (!appointmentWrapper.IsCompleteRecompleteVisitAllowed)
            {
                RefreshDashboard();
                return;
            }

            CompleteVisit(appointmentWrapper);                
        }

        #region CompleteVisit

        private void CompleteVisit(AppointmentWrapper appointmentWrapper)
        {
            if (appointmentWrapper.Start > DateTime.Now)
            {
                XtraMessageBox.Show("Unable to complete visit scheduled in the future", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (CompleteVisitController controller = Prepare<CompleteVisitController>(
                appointmentWrapper.Work, appointmentWrapper.Visit))
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {
                    Message.RemoveMessage(
                        appointmentWrapper.Work.TechnicianEmployeeId,
                        appointmentWrapper.Visit.ID, MessageTypeEnum.VisitIncome);
                    RefreshDashboard();
                    RefreshPendingVisits(null);
                    DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
                    UpdateDayClosedAmounts();
                }
            }
        }

        #endregion        

        #endregion

        #region OnEtcClick

        private void OnSubmitEtcClick(object sender, EventArgs e)
        {
            Appointment appointment = (Appointment)((DXMenuItem)sender).Tag;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

            appointmentWrapper.Refresh();
            if (!appointmentWrapper.IsSubmitEtcVisitAllowed)
            {
                RefreshDashboard();
                return;
            }

            using (SubmitEtcController controller = Prepare<SubmitEtcController>(
                appointmentWrapper.Work, appointmentWrapper.Visit))
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {                    
                    RefreshDashboard();
                    DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
                }
            }
        }

        #endregion        

        #region OnArriveClick

        private void OnArriveClick(object sender, EventArgs e)
        {
            Appointment appointment = (Appointment)((DXMenuItem)sender).Tag;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

            DateTime arriveTime;

            using (ArriveVisitController controller
                = Prepare<ArriveVisitController>(appointmentWrapper.WorkDetail))
            {
                controller.Execute(false);
                if (controller.IsCancelled)
                    return;

                arriveTime = controller.SelectedTime;
            }

            appointmentWrapper.Refresh();
            if (!appointmentWrapper.IsArriveRearriveVisitAllowed)
            {
                RefreshDashboard();
                return;
            }

            try
            {
                Database.Begin();
                Visit.Arrive(appointmentWrapper.Work.TechnicianEmployeeId,
                    appointmentWrapper.Visit.ID, arriveTime);
                Database.Commit();
                RefreshDashboard();
                DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }            
        }            
        
        #endregion        

        #region OnConfirmVisitClick

        private void OnConfirmVisitClick(object sender, EventArgs e)
        {
            Appointment appointment = (Appointment)((DXMenuItem)sender).Tag;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

            appointmentWrapper.Refresh();
            if (!appointmentWrapper.IsConfirmReconfirmVisitAllowed)
            {
                RefreshDashboard();
                return;
            }

            ConfirmVisitController controller = Prepare<ConfirmVisitController>(
                appointmentWrapper, Model.MainFormModel.CurrentDispatch);
            controller.Execute(false);
            if (controller.Result != ConfirmVisitResultEnum.Cancel)
            {
                DashboardState.MakeDashboardDirty(Configuration.CurrentDispatchId);
                RefreshDashboard();
            }
        }

        #endregion

        #region OnDispatchVisitClick

        private void OnDispatchVisitClick(object sender, EventArgs e)
        {
            Appointment appointment = (Appointment) ((DXMenuItem) sender).Tag;
            AppointmentWrapper appointmentWrapper = GetWrapper(appointment);

            DateTime dispatchTime;            

            using (DispatchVisitController controller
                = Prepare<DispatchVisitController>(appointmentWrapper.WorkDetail, appointmentWrapper.Start))
            {
                controller.Execute(false);
                if (controller.IsCancelled)
                    return;

                dispatchTime = controller.SelectedTime;
            }                                    

            if (View.m_dashboard.Start.Date == DateTime.Now.Date
                && appointmentWrapper.Visit.VisitStatus == VisitStatusEnum.Assigned
                && (DateTime.Now - dispatchTime).TotalMinutes < 60
                && appointmentWrapper.Visit.GetConfirmationReasons(dispatchTime).Count > 0)
            {
                using (ConfirmVisitController confirmController = Prepare<ConfirmVisitController>(appointmentWrapper,
                    Model.MainFormModel.CurrentDispatch, dispatchTime))
                {
                    confirmController.Execute(false);
                    if (confirmController.Result == ConfirmVisitResultEnum.Confirm)
                    {
                        m_isDashboardRefreshing = true;
                        Model.Appointments.ResetItem(Model.Appointments.IndexOf(appointmentWrapper));
                        m_isDashboardRefreshing = false;
                    }
                    else if (confirmController.Result == ConfirmVisitResultEnum.Cancel)
                        return;

                }
            }

            appointmentWrapper.Refresh();
            if (!appointmentWrapper.IsDispatchRedispatchVisitAllowed)
            {
                RefreshDashboard();
                return;
            }

            try
            {
                Database.Begin(IsolationLevel.RepeatableRead);
                Visit.AssignVisitExecution((int)View.m_dashboard.SelectedResource.Id,
                    appointmentWrapper.Visit.ID, dispatchTime);                
                Database.Commit();
                DashboardState.MakeDashboardDirty(Model.MainFormModel.CurrentDispatch.ID);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            if (Model.IsSendMessageAllowed(appointmentWrapper.ResourceId) 
                && (DateTime.Now - dispatchTime).TotalHours < 2)
            {
                using (new WaitCursor())
                    Model.SendVisitToPager(appointmentWrapper.Work, appointmentWrapper.Visit);
            }

            RefreshDashboard();
        }

        #endregion                

        #region RefreshDashboard

        private void RefreshDashboard(IDbConnection connection)
        {
            View.m_lblDispatcher.Text = Model.MainFormModel.CurrentDispatch.FirstName
                + " " + Model.MainFormModel.CurrentDispatch.LastName;
            View.m_lblDayTotalAmount.Visible =
                Model.MainFormModel.CurrentDispatch.SecurityPermissions.Contains(
                    SecurityPermissionEnum.ViewDashboardTotal);

            View.m_dashboard.BeginUpdate();            
            Model.RefreshAppointments(connection);

            AppointmentWrapper selectedAppointment = null;
            if (View.m_dashboard.SelectedAppointments.Count > 0)
                selectedAppointment = GetWrapper(View.m_dashboard.SelectedAppointments[0]);

            m_isDashboardRefreshing = true;

            if (Model.IsCurrentDateChanged())
            {
                View.m_dashboardStorage.Resources.DataSource = Model.Resources;
                View.m_dashboardStorage.Appointments.DataSource = Model.Appointments;
                Model.ResetCurrentDateChanged();
            }

            m_isDashboardRefreshing = false;
            View.m_dashboard.EndUpdate();
            View.m_dashboard.RefreshData();

            if (selectedAppointment != null)
            {
                foreach (Appointment appointment in View.m_dashboardStorage.Appointments.Items)
                {
                    AppointmentWrapper appointmentWrapper = GetWrapper(appointment);
                    if (appointmentWrapper.Visit.ID == selectedAppointment.Visit.ID)
                    {
                        View.m_dashboard.ActiveView.SelectAppointment(appointment);                        
                        return;
                    }
                }                                
            }

            UpdateDayClosedAmounts();
            RefreshLeadsAndFeedbacks(connection);
        }

        private void RefreshDashboard()
        {            
            using (new WaitCursor())
                RefreshDashboard(null);
        }

        #endregion

        #region RefreshDashboardResources

        private void RefreshDashboardResources()
        {
            Model.RefreshResources(View.m_dashboard.Start);

            m_isDashboardRefreshing = true;
            View.m_dashboardStorage.Resources.DataSource = Model.Resources;
            m_isDashboardRefreshing = false;
        }

        #endregion

        #region RefreshPendingVisits

        private void RefreshPendingVisits(IDbConnection connection)
        {
            VisitPackage selectedVisit = (VisitPackage) View.m_gridPendingVisitsView.GetRow(
                View.m_gridPendingVisitsView.FocusedRowHandle);                        

            Model.RefreshPendingVisits(
                (PendingVisitTypeEnum) (int) View.m_cmbPendingVisitsFilter.EditValue,
                View.m_txtPendingVisitCustomerFilter.Text,
                connection);
            View.m_gridPendingVisits.DataSource = Model.PendingVisits;

            if (selectedVisit != null)
                TryToSelectPendingVisit(selectedVisit.Visit);
        }
        #endregion

        #region RefreshLeadsAndFeedbacks

        private void RefreshLeadsAndFeedbacks(IDbConnection connection)
        {
            Model.RefreshProjectFeedbackCount(connection);
            Model.RefreshLeads(connection);

            m_isProjectFeedbackCountBlinking = Model.ProjectFeedbackCount > 0 ? true : false;
            m_isNewLeadsBlinking = Model.LeadsInfo.OpenedLeads > 0 ? true : false;
            m_isPendingLeadsBlinking = Model.LeadsInfo.PendingAlertLeads > 0 ? true : false;     
            
            View.m_lblLeadsOpened.Text = Model.LeadsInfo.OpenedLeads.ToString();
            View.m_lblLeadsPending.Text = Model.LeadsInfo.PendingLeads.ToString();
            View.m_lblProjectFeedbackCount.Text = Model.ProjectFeedbackCount.ToString();
        }

        #endregion

        #region OnPendingVisitsFilterChanged

        private void OnPendingVisitsFilterChanged(object sender, EventArgs e)
        {
            RefreshPendingVisits(null);
        }

        #endregion

        #region OnTimerTick

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (View.m_timer.Interval != REFRESH_TIMER_INTERVAL)
                View.m_timer.Interval = REFRESH_TIMER_INTERVAL;

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                if (DashboardState.IsDashboardDirty(Model.MainFormModel.CurrentDispatch.ID, connection))
                {
                    RefreshDashboard(connection);
                }

                if (PendingTaskGridState.IsPendingTaskGridDirty(Model.MainFormModel.CurrentDispatch.ID, connection))
                {
                    RefreshPendingVisits(connection);
                }

                RefreshLeadsAndFeedbacks(connection);
            }    
        }

        #endregion

        #region OnTimerBlinkingTick

        private void OnTimerBlinkingTick(object sender, EventArgs e)
        {
            if (m_isNewLeadsBlinking)
                BlinkControl(View.m_lblLeadsOpened);
        }

        private static void BlinkControl(Control l)
        {
            Color foreColor = l.ForeColor;
            Color backColor = l.BackColor;

            if (backColor == Color.Transparent)
            {
                l.BackColor = foreColor;
                l.ForeColor = Color.White;
            } else
            {
                l.BackColor = Color.Transparent;
                l.ForeColor = backColor;
            }
        }

        #endregion

        #region Draw time frame suggestions

        private bool m_isAppointmentMoving = false;
        private Visit m_currentDraggedVisit = null;
        private Appointment m_currentDraggedAppointment = null;

        private void DrawTimeFrames(object sender, CustomDrawObjectEventArgs e)
        {            
            //draw only on time for 1 technician
            if (e.ObjectInfo.Bounds.X > 57)
                return;

            if (m_isAppointmentMoving && View.m_dashboard.Start.Date >= DateTime.Now.Date)
            {
                if (m_currentDraggedVisit == null)
                {
                    SchedulerHitInfo hitInfo = View.m_dashboard.ActiveView.ViewInfo.CalcHitInfo(
                        View.m_dashboard.PointToClient(Control.MousePosition), false);                    
                    if (hitInfo.ViewInfo is AppointmentViewInfo)
                    {
                        AppointmentViewInfo appointmentViewInfo = (AppointmentViewInfo)hitInfo.ViewInfo;
                        m_currentDraggedVisit = GetWrapper(appointmentViewInfo.Appointment).Visit;
                        m_currentDraggedAppointment = appointmentViewInfo.Appointment;
                    }
                }

                if (m_currentDraggedVisit != null)
                {
                    bool isAtLeastOneTimeFrameDrawed = false;

                    if (m_currentDraggedVisit.PreferedTimeFrom.HasValue
                        || m_currentDraggedVisit.PreferedTimeTo.HasValue)
                    {
                        DateTime intervalStart;
                        DateTime intervalEnd;

                        if (!m_currentDraggedVisit.PreferedTimeFrom.HasValue)
                            intervalStart = new DateTime(
                                View.m_dashboard.Start.Year,
                                View.m_dashboard.Start.Month,
                                View.m_dashboard.Start.Day, 0, 0, 0);
                        else 
                            intervalStart = new DateTime(
                                View.m_dashboard.Start.Year,
                                View.m_dashboard.Start.Month,
                                View.m_dashboard.Start.Day,
                                m_currentDraggedVisit.PreferedTimeFrom.Value.Hour,
                                m_currentDraggedVisit.PreferedTimeFrom.Value.Minute, 0);

                        if (!m_currentDraggedVisit.PreferedTimeTo.HasValue)
                            intervalEnd = new DateTime(
                                View.m_dashboard.Start.Year,
                                View.m_dashboard.Start.Month,
                                View.m_dashboard.Start.Day, 23, 59, 59);
                        else 
                            intervalEnd = new DateTime(
                                View.m_dashboard.Start.Year,
                                View.m_dashboard.Start.Month,
                                View.m_dashboard.Start.Day,
                                m_currentDraggedVisit.PreferedTimeTo.Value.Hour,
                                m_currentDraggedVisit.PreferedTimeTo.Value.Minute, 0);

                        DrawTimeFrame(e, Color.FromArgb(TIME_FRAME_TRANSPARENCY, 0, 255, 0), intervalStart, intervalEnd);
                        
                        isAtLeastOneTimeFrameDrawed = true;
                    }

                    if (m_currentDraggedVisit.IsConfirmed
                        && m_currentDraggedVisit.ConfirmDateTime.Value.Date == View.m_dashboard.Start.Date
                        && (m_currentDraggedVisit.ConfirmedFrameBegin.HasValue
                            || m_currentDraggedVisit.ConfirmedFrameEnd.HasValue))
                    {
                        DrawTimeFrame(e, Color.FromArgb(TIME_FRAME_TRANSPARENCY, 0, 0, 255),
                            m_currentDraggedVisit.ConfirmedFrameBegin ?? new DateTime(
                                View.m_dashboard.Start.Year,
                                View.m_dashboard.Start.Month,
                                View.m_dashboard.Start.Day, 0, 0, 0),
                            m_currentDraggedVisit.ConfirmedFrameEnd ?? new DateTime(
                                View.m_dashboard.Start.Year,
                                View.m_dashboard.Start.Month,
                                View.m_dashboard.Start.Day, 23, 59, 59));
                        isAtLeastOneTimeFrameDrawed = true;
                    }

                    if (isAtLeastOneTimeFrameDrawed)
                    {
                        int? arriveY = null;

                        SchedulerHitInfo hitInfo = View.m_dashboard.ActiveView.ViewInfo.CalcHitInfo(
                            View.m_dashboard.PointToClient(Control.MousePosition), false);
                        if (hitInfo.ViewInfo is AppointmentViewInfo)
                        {
                            AppointmentViewInfo appointmentViewInfo = (AppointmentViewInfo)hitInfo.ViewInfo;
                            TimeRulerYPoint yPointResult = GetTimeRulerYPoint(
                                appointmentViewInfo.Appointment.Start.AddMinutes(30));
                            if (yPointResult.Location == TimeRulerYPointLocationEnum.Within)
                                arriveY = yPointResult.Y;
                        }

                        if (arriveY.HasValue)
                        {
                            e.Graphics.DrawLine(e.Cache.GetPen(
                                Color.FromArgb(ARRIVAL_LINE_TRANSPARENCY, Color.Black)),
                                0, arriveY.Value, TIME_RULER_WIDTH, arriveY.Value);
                            if (arriveY.Value - 10 > TIME_CELL_FIRST_TOP_POINT_Y)
                            {
                                e.Graphics.DrawString("ARRIVAL", new Font("Tahoma", 6),
                                    e.Cache.GetSolidBrush(
                                    Color.FromArgb(ARRIVAL_LINE_TRANSPARENCY, Color.Black)),
                                    4, arriveY.Value - 10);                                                            
                            }
                        }                        
                    }
                }
            }
        }

        private int GetY(TimeRulerYPoint result)
        {
            if (result.Location == TimeRulerYPointLocationEnum.Above)
                return TIME_CELL_FIRST_TOP_POINT_Y;
            else if (result.Location == TimeRulerYPointLocationEnum.Under)
                return TimeCellLastBottomPointY;
            else
                return result.Y;
        }

        private void DrawTimeFrame(CustomDrawObjectEventArgs e, Color color, DateTime start, DateTime end)
        {
            TimeRulerYPoint y1Result = GetTimeRulerYPoint(start);
            TimeRulerYPoint y2Result = GetTimeRulerYPoint(end);

            int y1 = GetY(y1Result);
            int y2 = GetY(y2Result);

            if (y1Result.Location == TimeRulerYPointLocationEnum.Within 
                || y2Result.Location == TimeRulerYPointLocationEnum.Within
                || (y1Result.Location == TimeRulerYPointLocationEnum.Above
                && y2Result.Location == TimeRulerYPointLocationEnum.Under))
            {
                e.Cache.FillRectangle(color, new Rectangle(0, y1, TIME_RULER_WIDTH, y2 - y1 + 1));            
            }
                
        }

        private const int TIME_FRAME_TRANSPARENCY = 60;
        private const int ARRIVAL_LINE_TRANSPARENCY = 100;
        private const int TIME_CELL_FIRST_TOP_POINT_Y = 35;
        private const int TIME_RULER_WIDTH = 55;
        
        private int TimeCellLastBottomPointY
        {
            get { return View.m_dashboard.Height - 23; }
        }

        private enum TimeRulerYPointLocationEnum
        {
            Above,
            Under,
            Within
        }

        private class TimeRulerYPoint
        {
            private TimeRulerYPointLocationEnum m_location;
            private int m_Y;

            public TimeRulerYPoint(TimeRulerYPointLocationEnum location, int y)
            {
                m_location = location;
                m_Y = y;
            }

            public TimeRulerYPointLocationEnum Location
            {
                get { return m_location; }
            }

            public int Y
            {
                get { return m_Y; }
            }
        }

        private TimeRulerYPoint GetTimeRulerYPoint(DateTime time)
        {
            DateTime visibleTimeStart = new DateTime(
                View.m_dashboard.Start.Year,
                View.m_dashboard.Start.Month,
                View.m_dashboard.Start.Day,
                View.m_dashboard.DayView.TopRowTime.Hours,
                View.m_dashboard.DayView.TopRowTime.Minutes,
                View.m_dashboard.DayView.TopRowTime.Seconds);

            SchedulerHitInfo lastCellHitInfo = View.m_dashboard.ActiveView.ViewInfo.CalcHitInfo(
                    new Point(TIME_RULER_WIDTH + 2, TimeCellLastBottomPointY - 1),
                    true);

            DateTime visibleTimeEnd = lastCellHitInfo.ViewInfo.Interval.End;
            if (visibleTimeEnd.Hour == 23 && visibleTimeEnd.Minute == 59)
                visibleTimeEnd = visibleTimeStart.Date.AddDays(1);

            if (time < visibleTimeStart)
                return new TimeRulerYPoint(TimeRulerYPointLocationEnum.Above, 0);
            if (time > visibleTimeEnd)
                return new TimeRulerYPoint(TimeRulerYPointLocationEnum.Under, 0);

            if (time == visibleTimeEnd)
                return new TimeRulerYPoint(TimeRulerYPointLocationEnum.Within, 
                    TimeCellLastBottomPointY - 1);

            int totalVisiblePoints = TimeCellLastBottomPointY - TIME_CELL_FIRST_TOP_POINT_Y;
            int cellsCount = (int)(visibleTimeEnd - visibleTimeStart).TotalMinutes / 15;
            if (cellsCount == 0)
                return new TimeRulerYPoint(TimeRulerYPointLocationEnum.Above, -1);
            int cellSize = totalVisiblePoints / cellsCount;
            int remainingPixels = totalVisiblePoints % cellsCount;

            DateTime truncatedTime = new DateTime(
                    time.Year, time.Month, time.Day, time.Hour, 
                    time.Minute < 30 ? 0 : 30, 0);;

            // soughtCellNumber - zero based
            int soughtCellNumber = (int)(truncatedTime - visibleTimeStart).TotalMinutes / 15;
            int soughtCellStartY = soughtCellNumber * cellSize + TIME_CELL_FIRST_TOP_POINT_Y;
            soughtCellStartY += Math.Min(soughtCellNumber, remainingPixels);
            if (truncatedTime == time)
                return new TimeRulerYPoint(TimeRulerYPointLocationEnum.Within, soughtCellStartY);
            int soughtCellEndY = soughtCellStartY + cellSize;
            if (remainingPixels >= soughtCellNumber)
                soughtCellEndY++;

            double restSeconds = (time - truncatedTime).TotalSeconds;

            int additionalPixels = (int)restSeconds * (soughtCellEndY - soughtCellStartY) / 1800;
            return new TimeRulerYPoint(TimeRulerYPointLocationEnum.Within,
                soughtCellStartY + additionalPixels);                    
        }

        private void OnDashboardMouseMove(object sender, MouseEventArgs e)
        {
            if (!m_isAppointmentMoving && IsAppointmentMoving(e))
            {
                m_isAppointmentMoving = true;
            }                
            else if (e.Button != MouseButtons.Left && m_isAppointmentMoving)
            {
                m_isAppointmentMoving = false;
                m_currentDraggedVisit = null;
                m_currentDraggedAppointment = null;
                View.m_dashboard.Invalidate();                
            }

            if (m_isAppointmentMoving && m_currentDraggedAppointment != null)
            {
                UpdateDraggedAppointment(m_currentDraggedAppointment, true);
            }
        }

        private void OnGridPendingVisitsMouseMove(object sender, MouseEventArgs e)
        {
            if (m_isAppointmentMoving)
            {
                m_isAppointmentMoving = false;
                m_currentDraggedVisit = null;
                m_currentDraggedAppointment = null;
                View.m_dashboard.Invalidate();                                
            }
        }

        private void OnDashboardMouseDown(object sender, MouseEventArgs e)
        {
            if (!m_isAppointmentMoving && IsAppointmentMoving(e))
                m_isAppointmentMoving = true;
        }

        private bool IsAppointmentMoving(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SchedulerHitInfo hitInfo = View.m_dashboard.ActiveView.ViewInfo.CalcHitInfo(
                    View.m_dashboard.PointToClient(Control.MousePosition), false);

                if (hitInfo.HitTest == SchedulerHitTest.AppointmentContent
                    || hitInfo.HitTest == SchedulerHitTest.AppointmentMoveEdge
                    || hitInfo.HitTest == SchedulerHitTest.AppointmentResizingBottomEdge
                    || hitInfo.HitTest == SchedulerHitTest.AppointmentResizingRightEdge)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region DashboardDrawAdditionalVisitGraphic

        private void DashboardDrawAdditionalVisitGraphic(object sender, CustomDrawObjectEventArgs e)
        {
            if (e.ObjectInfo is VerticalAppointmentViewInfo)
            {
                Appointment appointment = (e.ObjectInfo as VerticalAppointmentViewInfo).Appointment;
                AppointmentWrapper wrapper = GetWrapper(appointment);

                Visit visit;
                if (wrapper != null)
                    visit = wrapper.Visit;
                else
                    visit = m_currentDraggedVisit;

                if (visit.IsConfirmed)
                {
                    if (visit.IsCallOnYourWay)
                    {
                        e.Graphics.DrawImage(View.m_appointmentImages.Images[3],
                             e.Bounds.X + e.Bounds.Width - 34,
                             e.Bounds.Y + e.Bounds.Height - 15);                                                
                    }

                    if (visit.IsWillCall && !visit.IsCallOnYourWay)
                    {
                        e.Graphics.DrawImage(View.m_appointmentImages.Images[4],
                             e.Bounds.X + e.Bounds.Width - 34,
                             e.Bounds.Y + e.Bounds.Height - 15);
                    }
                    else if (visit.IsWillCall)
                    {
                        e.Graphics.DrawImage(View.m_appointmentImages.Images[4],
                             e.Bounds.X + e.Bounds.Width - 48,
                             e.Bounds.Y + e.Bounds.Height - 15);                        
                    }


                    if (visit.ConfirmLeftMessage)
                    {
                        e.Graphics.DrawImage(Resources.Tape,
                             e.Bounds.X + e.Bounds.Width - 19, 
                             e.Bounds.Y + e.Bounds.Height - 15);
                    } else if (visit.ConfirmBusy)
                    {
                        e.Graphics.DrawImage(View.m_appointmentImages.Images[2],
                             e.Bounds.X + e.Bounds.Width - 20,
                             e.Bounds.Y + e.Bounds.Height - 15);                        
                    }
                    else
                    {
                        e.Graphics.DrawImage(View.m_appointmentImages.Images[0],
                             e.Bounds.X + e.Bounds.Width - 20,
                             e.Bounds.Y + e.Bounds.Height - 15);
                    }
                }
            }
        }

        #endregion

        #region UpdateDayClosedAmounts

        private void UpdateDayClosedAmounts()
        {
            View.m_lblDayTotalAmount.Text = Work.FindDayClosedAmount(
                View.m_dashboard.Start.Date).ToString("C");
        }

        #endregion
    }    
}
