using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraScheduler;
using SmartSchedule.Domain;
using SmartSchedule.Win32.VisitAdd;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.MainForm
{
    public class MainFormController : Controller<MainFormModel, MainFormView>
    {
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

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnAddVisit.Click += OnAddVisitClick;
            View.m_btnAddRandom.Click += OnAddRandomClick;
            View.m_btnAddRandomMany.Click += OnAddRandomManyClick;

            View.m_dashboard.AppointmentDrag += OnAppointmentDrag;
            View.m_dashboard.AppointmentResizing += OnAppointmentResizing;            

            View.m_btnSave.Click += OnSaveClick;
            View.m_btnRestore.Click += OnRestoreClick;
            View.m_btnClear.Click += OnClearClick;

            Model.BookingEngine.VisitsCountChanged += OnVisitsCountChanged;
            Model.BookingEngine.TechniciansLoadChanged += OnTechniciansLoadChanged;
            View.m_spinDaysCount.ValueChanged += OnDaysCountChanged;
            View.m_spinTechCount.ValueChanged += OnTechCountChanged;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_dashboard.Start = DateTime.Now;
            View.m_dashboardStorage.Resources.DataSource = Model.BookingEngine.Technicians;
            View.m_dashboardStorage.Appointments.DataSource = Model.BookingEngine.Visits;

            View.m_btnRestore.Enabled = false;
            OnTechniciansLoadChanged();
        }

        #endregion

        #region OnDaysCountTextChanged

        private void OnDaysCountChanged(object sender, EventArgs e)
        {
            Model.BookingEngine.FutureDaysCount = (int)View.m_spinDaysCount.Value;
        }

        #endregion

        #region OnTechCountChanged

        private void OnTechCountChanged(object sender, EventArgs e)
        {
            Model.BookingEngine.TechniciansCount = (int)View.m_spinTechCount.Value;
        }

        #endregion


        #region Appointment Drag & Resize

        private void OnAppointmentResizing(object sender, AppointmentResizeEventArgs e)
        {
            UpdateAppointmentStatus(e.EditedAppointment);            
        }

        private void OnAppointmentDrag(object sender, AppointmentDragEventArgs e)
        {
            UpdateAppointmentStatus(e.EditedAppointment);
        }

        private void UpdateAppointmentStatus(Appointment editedAppointment)
        {
            Visit visit = (Visit)editedAppointment.GetRow(View.m_dashboardStorage);
            visit.TimeStart = editedAppointment.Start;
            visit.TimeEnd = editedAppointment.End;

            editedAppointment.StatusId = visit.StatusId;
            editedAppointment.Description = visit.Caption;
        }

        #endregion

        #region Save & Restore

        private void OnVisitsCountChanged(int visitsCount)
        {
            View.m_btnSave.Enabled = visitsCount > 0;
        }

        private void OnRestoreClick(object sender, EventArgs e)
        {
            Model.RestoreVisits();            
        }        

        private void OnSaveClick(object sender, EventArgs e)
        {            
            Model.SaveVisits();
            View.m_btnRestore.Enabled = true;            
        }

        #endregion

        #region OnTechniciansLoadChanged

        private void OnTechniciansLoadChanged()
        {
            string result = string.Empty;

            foreach (Technician technician in Model.BookingEngine.Technicians)
                result += technician.TotalWorkDuration + "\n";

            View.m_lblTechLoad.Text = result;
        }

        #endregion

        #region OnClearClick

        private void OnClearClick(object sender, EventArgs e)
        {
            Model.BookingEngine.Visits.Clear();            
        }

        #endregion

        #region OnAddVisitClick

        private void OnAddVisitClick(object sender, EventArgs e)
        {
            using (VisitAddController controller = Prepare<VisitAddController>(Model.BookingEngine))
            {
                controller.Execute(false);
            }            
        }

        #endregion

        #region Add Random

        private void AddRandom(bool showFailNotification)
        {
            int maxDuration;

            try
            {
                maxDuration = int.Parse(View.m_txtMaxDuration.Text);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Please enter valid duration");
                return;
            }

            if (maxDuration == 0)
            {
                XtraMessageBox.Show("Duration should greater than 0");
                return;
            }

            if (maxDuration % 30 != 0)
            {
                XtraMessageBox.Show("Duration should be 30 X");
                return;
            }

            string result = Model.InsertRandomVisit(maxDuration);
            if (showFailNotification && result != string.Empty)
                MessageBox.Show(result);            
        }

        private void OnAddRandomClick(object sender, EventArgs e)
        {
            AddRandom(true);
        }

        private void OnAddRandomManyClick(object sender, EventArgs e)
        {
            for (int i = 0; i <= 1000; i++)
                AddRandom(false);
        }

        #endregion
    }
}
