using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.MainForm
{
    public class MainFormModel : IModel
    {
        #region BookingEngine

        private BookingEngine m_bookingEngine;
        public BookingEngine BookingEngine
        {
            get { return m_bookingEngine; }
        }

        #endregion

        #region SavedVisits

        private List<Visit> m_savedVisits;
        public List<Visit> SavedVisits
        {
            get { return m_savedVisits; }
        }

        #endregion

        #region Init

        public void Init()
        {               
            m_bookingEngine = new BookingEngine();
            m_savedVisits = new List<Visit>();
        }

        #endregion        

        #region Save & Restore

        public void SaveVisits()
        {
            m_savedVisits = m_bookingEngine.CloneVisits();
        }

        public void RestoreVisits()
        {
            m_bookingEngine.Visits.Clear();
            
            foreach (Visit visit in m_savedVisits)
                m_bookingEngine.Visits.Add((Visit)visit.Clone());            
        }

        #endregion

        #region InsertRandomVisit

        public string InsertRandomVisit(double maxDuration)
        {            
            Random random = new Random();

            IList<TimeFrame> timeFrames = m_bookingEngine.TimeFrames;

            double duration = random.Next(1, (int)(maxDuration / 30) + 1) * 30;
            TimeFrame timeFrame = timeFrames[random.Next(0, timeFrames.Count)];

            Visit newVisit = m_bookingEngine.GetNewVisit(timeFrame, duration,
                new Point(0, 0));
                //new Point(random.Next(0, 101), random.Next(0, 101)));

            if (!m_bookingEngine.InsertVisit(newVisit))
            {
                return "Unable to insert Visit. Time Frame = "
                       + newVisit.ConfirmedTimeFrame.Text + ", Duration = " + duration;
            }

            return string.Empty;
        }

        #endregion
    }
}
