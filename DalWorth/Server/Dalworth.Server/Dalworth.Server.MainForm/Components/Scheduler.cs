using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;

namespace Dalworth.Server.MainForm.Components
{
    public class Scheduler : SchedulerControl
    {
        public new event KeyEventHandler KeyDown;

        #region NextControl

        private Control m_nextControl;        
        public Control NextControl
        {
            get { return m_nextControl; }
            set { m_nextControl = value; }
        }

        #endregion

        #region PreviousControl

        private Control m_previousControl;
        public Control PreviousControl
        {
            get { return m_previousControl; }
            set { m_previousControl = value; }
        }

        #endregion

        #region OnKeyDown

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!e.Shift && e.KeyCode == Keys.Tab)
            {
                if (m_nextControl != null)
                    m_nextControl.Select();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                if (m_previousControl != null)
                    m_previousControl.Select();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
         

            if (e.Shift && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if ((e.Alt || e.Shift)
                && (e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown
                    || e.KeyCode == Keys.Home || e.KeyCode == Keys.End))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;                
            }


            //Prevent switching to prev date
            if (e.KeyCode == Keys.Left && Storage.Resources.Count > 0
                && Storage.Resources[0] == SelectedResource)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            //Prevent switching to next date
            if (e.KeyCode == Keys.Right && Storage.Resources.Count > 0
                && Storage.Resources[Storage.Resources.Count - 1] == SelectedResource)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            //Navigate on selected appointment UP
            if (e.KeyCode == Keys.Up && SelectedAppointments.Count > 0)
            {
                List<Appointment> appointments = GetSortedAppointments((int) SelectedResource.Id);
                int currentIndex = appointments.IndexOf(SelectedAppointments[0]);

                if (currentIndex > 0)
                    ActiveView.SelectAppointment(appointments[currentIndex - 1]);
                else
                {
                    DateTime timeEndToSelect = Utils.RoundTo15Min(SelectedAppointments[0].Start);
                    DateTime timeStartToSelect = timeEndToSelect.AddMinutes(-15);

                    if (timeStartToSelect.Date == SelectedAppointments[0].Start.Date)
                        ActiveView.SetSelection(new TimeInterval(timeStartToSelect, timeEndToSelect), 
                            SelectedResource);                                                          
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }


            //Navigate on selected appointment DOWN
            if (e.KeyCode == Keys.Down && SelectedAppointments.Count > 0)
            {
                List<Appointment> appointments = GetSortedAppointments((int)SelectedResource.Id);
                int currentIndex = appointments.IndexOf(SelectedAppointments[0]);

                if (currentIndex < appointments.Count - 1)
                    ActiveView.SelectAppointment(appointments[currentIndex + 1]);
                else
                {
                    DateTime timeStartToSelect;
                    if (SelectedAppointments[0].End.Minute % 15 == 0)
                        timeStartToSelect = SelectedAppointments[0].End;
                    else
                        timeStartToSelect = Utils.RoundTo15Min(SelectedAppointments[0].End).AddMinutes(15);

                    DateTime timeEndToSelect = timeStartToSelect.AddMinutes(15);

                    if (timeEndToSelect.Date == SelectedAppointments[0].Start.Date)
                        ActiveView.SetSelection(new TimeInterval(timeStartToSelect, timeEndToSelect),
                            SelectedResource);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            //Navigate on selected appointment LEFT & RIGHT
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) && SelectedAppointments.Count > 0)
            {
                int newResourceIndex;
                if (e.KeyCode == Keys.Left)
                    newResourceIndex = Storage.Resources.Items.IndexOf(SelectedResource) - 1;
                else
                    newResourceIndex = Storage.Resources.Items.IndexOf(SelectedResource) + 1;

                Resource newResource = Storage.Resources[newResourceIndex];

                List<Appointment> appointments = GetSortedAppointments((int)newResource.Id);
                TimeInterval appointmentInterval = new TimeInterval(
                    SelectedAppointments[0].Start, SelectedAppointments[0].End);


                foreach (Appointment appointment in appointments)
                {
                    TimeInterval candidateAppointmentInterval = new TimeInterval(
                        appointment.Start, appointment.End);

                    if (candidateAppointmentInterval.IntersectsWithExcludingBounds(appointmentInterval))
                    {
                        ActiveView.SelectAppointment(appointment);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;
                    }
                }

                ActiveView.SetSelection(new TimeInterval(Utils.RoundTo15Min(appointmentInterval.Start),
                    Utils.RoundTo15Min(appointmentInterval.Start).AddMinutes(15)),
                    newResource);


                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (KeyDown != null)
                KeyDown.Invoke(this, e);

            if (e.Control && e.KeyCode == Keys.D)
                return;                

            base.OnKeyDown(e);
        }

        #endregion

        #region GetSortedAppointments

        public List<Appointment> GetSortedAppointments(int resourceId)
        {
            return GetSortedAppointmentsInternal(resourceId);
        }

        public List<Appointment> GetSortedAppointments()
        {
            return GetSortedAppointmentsInternal(null);
        }

        private List<Appointment> GetSortedAppointmentsInternal(int? resourceId)
        {
            List<Appointment> result = new List<Appointment>();

            for (int i = 0; i < Storage.Appointments.Count; i++)
            {                
                if (resourceId.HasValue)
                {
                    if (resourceId.Value == (int)Storage.Appointments[i].ResourceId)
                        result.Add(Storage.Appointments[i]);
                } else 
                    result.Add(Storage.Appointments[i]);
            }
                
            result.Sort(AppointmentComparison);
            return result;
        }

        private int AppointmentComparison(Appointment x, Appointment y)
        {
            if (x.Start != y.Start)
                return x.Start.CompareTo(y.Start);

            if (x.End != y.End)
                return x.End.CompareTo(y.End);

            return x.Subject.CompareTo(y.Subject);
        }

        #endregion
    }
}
