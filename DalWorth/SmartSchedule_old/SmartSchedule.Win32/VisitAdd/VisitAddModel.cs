using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.VisitAdd
{
    public class VisitAddModel : IModel
    {        
        #region BookingEngine

        private BookingEngine m_bookingEngine;
        public BookingEngine BookingEngine
        {
            get { return m_bookingEngine; }
            set { m_bookingEngine = value; }
        }

        #endregion

        #region TimeFrames

        private BindingList<TimeFrame> m_timeFrames;
        public BindingList<TimeFrame> TimeFrames
        {
            get { return m_timeFrames; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_timeFrames = m_bookingEngine.TimeFrames;
        }

        #endregion        
    }
}
