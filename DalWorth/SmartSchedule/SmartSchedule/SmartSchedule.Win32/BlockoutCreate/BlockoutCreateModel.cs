using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.BlockoutCreate
{
    public class BlockoutCreateModel : IModel
    {
        #region Technician

        private Technician m_technician;
        public Technician Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region InitialTimeStart

        private DateTime m_initialTimeStart;
        public DateTime InitialTimeStart
        {
            get { return m_initialTimeStart; }
            set { m_initialTimeStart = value; }
        }

        #endregion

        #region InitialTimeEnd

        private DateTime m_initialTimeEnd;
        public DateTime InitialTimeEnd
        {
            get { return m_initialTimeEnd; }
            set { m_initialTimeEnd = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
            if (m_visit != null)
            {
                m_technician = m_visit.Technician;
                m_initialTimeStart = m_visit.TimeStart;
                m_initialTimeEnd = m_visit.TimeEnd;
            }
        }

        #endregion

        #region CreateEditBlockout

        public string CreateEditBlockout(DateTime timeStart, DateTime timeEnd, string note)
        {
            DateTime timeStartNew = m_technician.ScheduleDate.Add(timeStart.TimeOfDay);
            DateTime timeEndNew = m_technician.ScheduleDate.Add(timeEnd.TimeOfDay);

            if (m_visit != null)
            {
                m_visit.TimeStart = timeStartNew;
                m_visit.TimeEnd = timeEndNew;
                m_visit.Note = note;
                return WcfClient.WcfClient.Instance.UpdateVisit(m_visit, true, false);
            }
            else
            {
                return WcfClient.WcfClient.Instance.CreateBlockout(m_technician.TechnicianDefaultId,
                    timeStartNew, timeEndNew, note);                
            }
        }

        #endregion
    }
}
