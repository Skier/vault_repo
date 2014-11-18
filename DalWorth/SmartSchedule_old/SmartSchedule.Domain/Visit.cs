using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule.Domain
{
    public class Point
    {
        private int m_x, m_y;

        #region Constructor

        public Point(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        #endregion        

        #region Properties

        public int X
        {
            get { return m_x; }
        }                

        public int Y
        {
            get { return m_y; }
        }

        #endregion

        #region Text

        public string Text
        {
            get { return string.Format("({0},{1})", X, Y); }
        }

        #endregion

        #region Distance

        public double Distance(Point point)
        {
            double dx = Math.Abs(X - point.X);
            double dy = Math.Abs(Y - point.Y);

            return Math.Sqrt(dx*dx + dy*dy);
        }

        #endregion
    }

    public class Visit : ICloneable
    {
        private const double SHORT_DISTANCE_THRESHOLD = 5;

        #region Constructor

        public Visit(int id, Technician technician, DateTime timeStart, 
            DateTime timeEnd, TimeFrame confirmedTimeFrame, Point location)
        {
            m_id = id;
            m_technician = technician;
            m_timeStart = timeStart;
            m_timeEnd = timeEnd;
            m_confirmedTimeFrame = confirmedTimeFrame;
            m_location = location;
        }

        #endregion

        #region Technician

        private Technician m_technician;
        public Technician Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
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

        #region TechnicianId

        public int TechnicianId
        {
            get { return m_technician.Id; }
            set
            {
                if (m_technician.Id != value)
                {
                    m_technician.TotalWorkDuration -= WorkDuration;
                    m_technician = Domain.Technician.GetTechnician(value);
                    m_technician.TotalWorkDuration += WorkDuration;
                }
                    
            }
        }

        #endregion

        #region TimeStart

        private DateTime m_timeStart;
        public DateTime TimeStart
        {
            get { return m_timeStart; }
            set
            {
                double prevWorkDuration = WorkDuration;
                m_timeStart = value;
                m_technician.TotalWorkDuration += WorkDuration - prevWorkDuration;
            }
        }

        #endregion

        #region TimeEnd

        private DateTime m_timeEnd;
        public DateTime TimeEnd
        {
            get { return m_timeEnd; }
            set
            {
                double prevWorkDuration = WorkDuration;
                m_timeEnd = value;
                m_technician.TotalWorkDuration += WorkDuration - prevWorkDuration;
            }
        }

        #endregion

        #region Location

        private Point m_location;
        public Point Location
        {
            get { return m_location; }
            set { m_location = value; }
        }

        #endregion

        #region Distance

        public double Distance(Visit visit)
        {
            return Location.Distance(visit.Location);
        }

        #endregion


        #region SetStartTimeFixDuration

        public void SetTimeStartFixDuration(DateTime timeStart)
        {
            double duration = DurationMin;
            m_timeStart = timeStart;
            m_timeEnd = m_timeStart.AddMinutes(duration);
        }

        #endregion

        #region SetTimeEndFixDuration

        public void SetTimeEndFixDuration(DateTime timeEnd)
        {
            double duration = DurationMin;
            m_timeEnd = timeEnd;
            m_timeStart = m_timeEnd.AddMinutes(-duration);
        }

        #endregion

        #region GetTimeStartAfterMove

        public DateTime GetTimeStartAfterMove(double minutes)
        {
            return m_timeStart.AddMinutes(minutes);
        }

        #endregion

        #region DurationMin

        public double DurationMin
        {
            get { return m_timeEnd.Subtract(m_timeStart).TotalMinutes; }
        }

        #endregion

        #region TimeArrival

        public DateTime TimeArrival
        {
            get { return m_timeStart.AddMinutes(30); }
        }

        #endregion

        #region Caption

        public string Caption
        {
            get
            {
                return "ID = " + m_id + ", Time Frame: " + m_confirmedTimeFrame.Text 
                    + ", ARR: " + TimeArrival.ToShortTimeString() + " Loc: " + Location.Text;
            }
        }

        #endregion

        #region StatusId

        public int StatusId
        {
            get { return IsWithinAllowedInterval ? 2 : 3; }
        }

        #endregion        

        #region ConfirmedTimeFrame

        private TimeFrame m_confirmedTimeFrame;
        public TimeFrame ConfirmedTimeFrame
        {
            get { return m_confirmedTimeFrame; }
            set { m_confirmedTimeFrame = value; }
        }

        #endregion

        #region IsWithinAllowedInterval

        public bool IsWithinAllowedInterval
        {
            get
            {
                TimeInterval interval = AllowedInterval;
                return m_timeStart >= interval.Start && m_timeEnd <= interval.End;
            }
        }

        #endregion

        #region AllowedInterval

        public TimeInterval AllowedInterval
        {
            get 
            {
                //TODO: Initially we don't know which technician will be assigned
                //Every technician could have its own EndOfDay

                DateTime start = m_confirmedTimeFrame.MinVisitStartTime;
                DateTime end = m_confirmedTimeFrame.MaxVisitStartTime.AddMinutes(DurationMin);

                DateTime technicianTimeEnd = m_confirmedTimeFrame.TimeStart.Date.Add(m_technician.EndOfDay);

                if (start > technicianTimeEnd)
                    start = technicianTimeEnd;

                if (end > technicianTimeEnd)
                    end = technicianTimeEnd;

                return new TimeInterval(start, end);
            }            
        }

        #endregion

        #region MaxTimeStart

        public DateTime MaxTimeStart
        {
            get
            {
                return AllowedInterval.End.AddMinutes(-DurationMin);
            }
        }

        #endregion

        #region MinTimeStart

        public DateTime MinTimeStart
        {
            get
            {
                return AllowedInterval.Start;
            }
        }

        #endregion

        #region MaxTimeEnd

        public DateTime MaxTimeEnd
        {
            get
            {
                return AllowedInterval.End;
            }
        }

        #endregion

        #region MinTimeEnd

        public DateTime MinTimeEnd
        {
            get
            {
                return AllowedInterval.Start.AddMinutes(DurationMin);
            }
        }

        #endregion

        #region CurrentInterval

        public TimeInterval CurrentInterval
        {
            get { return new TimeInterval(TimeStart, TimeEnd);}
        }

        #endregion

        #region WorkDuration

        public double WorkDuration
        {
            get
            {
                return DurationMin - 30;
            }
        }

        #endregion

        #region IsCloseDrive

        public bool IsCloseDrive(Visit visit)
        {
            return Distance(visit) <= SHORT_DISTANCE_THRESHOLD;
        }

        #endregion
       
        #region Clone

        public object Clone()
        {
            return new Visit(m_id, m_technician, m_timeStart, m_timeEnd, 
                m_confirmedTimeFrame, m_location);            
        }

        #endregion
    }
}
