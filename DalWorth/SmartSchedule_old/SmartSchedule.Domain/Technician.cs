using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmartSchedule.Domain
{
    public class Technician
    {
        public delegate void LoadChangedHandler();
        public event LoadChangedHandler LoadChanged;

        #region Technician

        public Technician(int id, string name, TimeSpan endOfDay)
        {
            m_id = id;
            m_name = name;
            m_endOfDay = endOfDay;
        }

        #endregion

        #region Id

        private int m_id;
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        #endregion

        #region Name

        private string m_name;
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        #endregion

        #region Caption

        public string Caption
        {
            get
            {
                return Name;
            }
        }

        #endregion

        #region EndOfDay

        private TimeSpan m_endOfDay;
        public TimeSpan EndOfDay
        {
            get { return m_endOfDay; }
            set { m_endOfDay = value; }
        }

        #endregion

        #region TotalWorkDuration

        private double m_totalWorkDuration;
        public double TotalWorkDuration
        {
            get { return m_totalWorkDuration; }
            set
            {
                if (m_totalWorkDuration != value)
                {
                    m_totalWorkDuration = value;
                    if (LoadChanged != null)
                        LoadChanged.Invoke();
                }                
            }
        }

        #endregion

        #region Technicians

        private static Dictionary<int, Technician> m_technicians;
        public static Dictionary<int, Technician> Technicians
        {
            get
            {
                if (m_technicians == null)
                {
                    m_technicians = new Dictionary<int, Technician>();
                    m_technicians.Add(1, new Technician(1, "Hedger, Terry",
                        DateTime.Now.Date.AddHours(18).TimeOfDay));                   
                    m_technicians.Add(2, new Technician(2, "Taylor, Garry",
                        DateTime.Now.Date.AddHours(18).TimeOfDay));
                    m_technicians.Add(3, new Technician(3, "Franklin, Steve",
                        DateTime.Now.Date.AddHours(18).TimeOfDay));
                    m_technicians.Add(4, new Technician(4, "Mitchell, Mike",
                        DateTime.Now.Date.AddHours(18).TimeOfDay));
                }

                return m_technicians;                
            }
        }

        public static Technician GetTechnician(int id)
        {
            return Technicians[id];
        }

        #endregion

    }


}
