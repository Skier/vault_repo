using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using Task=Dalworth.Server.Domain.Task;

namespace Dalworth.Server.MainForm.EndDay
{
    public class EndDayModel : IModel
    {
        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region Technician

        private Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_technician = Employee.FindByPrimaryKey(m_work.TechnicianEmployeeId);
        }

        #endregion

        #region IsVisitsExistsAfter

        public bool IsVisitsExistsAfter(DateTime date)
        {
            List<WorkDetail> workDetails = WorkDetail.FindBy(m_work);
            foreach (WorkDetail workDetail in workDetails)
            {
                if (workDetail.TimeEnd > date)
                    return true;

            }
            return false;
        }

        #endregion


        #region CompleteWork

        public void CompleteWork(DateTime? endDayDate)
        {
            Work.CompleteWork(m_work.ID, endDayDate, null);
        }

        #endregion

        #region GetLastVisitEndTime

        private DateTime? GetLastVisitEndTime()
        {
            List<WorkDetail> workDetails = WorkDetail.FindBy(m_work);
            if (workDetails.Count == 0)
                return null;

            DateTime result = workDetails[0].TimeEnd;

            foreach (WorkDetail detail in workDetails)
            {
                if (detail.TimeEnd > result)
                    result = detail.TimeEnd;
            }

            return result;
        }

        #endregion

        #region GetDefaultEndDayTime

        public DateTime GetDefaultEndDayTime()
        {
            if (m_work.EndDayDate.HasValue)
                return m_work.EndDayDate.Value;
     
            if (m_work.StartDate.Value.Date == DateTime.Now.Date)
            {
                return DateTime.Now;
            }
            else
            {
                DateTime? lastVisitTime = GetLastVisitEndTime();

                if (lastVisitTime.HasValue)
                    return lastVisitTime.Value;

                return new DateTime(
                    m_work.StartDate.Value.Year,
                    m_work.StartDate.Value.Month,
                    m_work.StartDate.Value.Day,
                    23, 59, 59);
            }
        }

        #endregion
    }
}
