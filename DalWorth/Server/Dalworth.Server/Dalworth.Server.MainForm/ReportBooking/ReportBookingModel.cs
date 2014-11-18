using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportBooking
{
    public class ReportBookingModel : IModel
    {
        #region StartDate

        private DateTime m_startDate;
        public DateTime StartDate
        {
            get { return m_startDate; }
        }

        #endregion

        #region EndDate
        
        private DateTime m_endDate;
        public DateTime EndDate
        {
            get { return m_endDate; }
        }

        #endregion

        #region Bookings

        private BindingList<Booking> m_bookings;
        public BindingList<Booking> Bookings
        {
            get { return m_bookings; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion

        #region RefreshReportData

        public void RefreshReportData(DateTime startDate, DateTime endDate)
        {
            m_startDate = startDate;
            m_endDate = endDate;

            m_bookings = new BindingList<Booking>(
                Booking.Find(startDate, endDate));
        }

        #endregion
    }
}
