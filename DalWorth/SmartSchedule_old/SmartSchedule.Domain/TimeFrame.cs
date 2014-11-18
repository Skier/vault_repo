using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain
{
    public class TimeFrame
    {
        #region Constructor

        public TimeFrame(DateTime timeStart, DateTime timeEnd)
        {
            m_timeStart = timeStart;
            m_timeEnd = timeEnd;
        }

        #endregion

        #region TimeStart

        private DateTime m_timeStart;
        public DateTime TimeStart
        {
            get { return m_timeStart; }
            set { m_timeStart = value; }
        }

        #endregion

        #region TimeEnd

        private DateTime m_timeEnd;
        public DateTime TimeEnd
        {
            get { return m_timeEnd; }
            set { m_timeEnd = value; }
        }

        #endregion

        #region Date

        public DateTime Date
        {
            get { return m_timeStart.Date; }
        }

        #endregion

        #region FormatTime

        private string FormatTime(DateTime time)
        {
            string result;

            if (time.Hour > 12)
                result = (time.Hour - 12).ToString();
            else
                result = time.Hour.ToString();

            if (time.Minute != 0)
                result += ":" + time.Minute.ToString("00");

            return result;
        }

        #endregion

        #region Text

        public string Text
        {
            get
            {
                return FormatTime(m_timeStart) + "-" + FormatTime(m_timeEnd);
            }
        }

        #endregion                

        #region GetTimeFrames

        public static List<TimeFrame> GetTimeFrames()
        {
            List<TimeFrame> result = new List<TimeFrame>();

            result.Add(new TimeFrame(new DateTime(1, 1, 1, 7, 30, 0), new DateTime(1, 1, 1, 8, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 8, 00, 0), new DateTime(1, 1, 1, 8, 30, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 9, 0, 0), new DateTime(1, 1, 1, 11, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 10, 0, 0), new DateTime(1, 1, 1, 12, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 11, 0, 0), new DateTime(1, 1, 1, 13, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 12, 0, 0), new DateTime(1, 1, 1, 14, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 13, 0, 0), new DateTime(1, 1, 1, 15, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 14, 0, 0), new DateTime(1, 1, 1, 16, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 15, 0, 0), new DateTime(1, 1, 1, 17, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 16, 0, 0), new DateTime(1, 1, 1, 18, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 17, 0, 0), new DateTime(1, 1, 1, 19, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 18, 0, 0), new DateTime(1, 1, 1, 20, 0, 0)));
            result.Add(new TimeFrame(new DateTime(1, 1, 1, 19, 0, 0), new DateTime(1, 1, 1, 21, 0, 0)));

            return result;
        }

        #endregion

        #region MinVisitStartTime

        public DateTime MinVisitStartTime
        {
            get { return TimeStart.AddMinutes(-30); }
        }

        #endregion

        #region MaxVisitStartTime

        public DateTime MaxVisitStartTime
        {
            get { return TimeEnd.AddMinutes(-60); }
        }

        #endregion

        #region GetMinVisitEndTime

        public DateTime GetMinVisitEndTime(double duration)
        {
            return MinVisitStartTime.AddMinutes(duration);
        }

        #endregion

        #region GetMaxVisitEndTime

        public DateTime GetMaxVisitEndTime(double duration)
        {
            return MaxVisitStartTime.AddMinutes(duration);
        }

        #endregion



        #region IsStartTimeWithinFrame

        public bool IsStartTimeWithinFrame(DateTime timeStart)
        {
            return timeStart >= MinVisitStartTime && timeStart <= MaxVisitStartTime;
        }

        #endregion

        #region IsEndTimeWithinFrame

        public bool IsEndTimeWithinFrame(DateTime timeEnd, double duration)
        {
            return timeEnd >= GetMinVisitEndTime(duration) && timeEnd <= GetMaxVisitEndTime(duration);
        }

        #endregion

        #region Rank

        private int m_rank;
        /// <summary>
        /// Ranks goes from 0. Higher rank - less priority        
        /// </summary>
        public int Rank
        {
            get { return m_rank; }
            set { m_rank = value; }
        }

        private int m_primaryRank;
        public int PrimaryRank
        {
            get { return m_primaryRank; }
            set { m_primaryRank = value; }
        }

        private int m_secondaryRank;
        public int SecondaryRank
        {
            get { return m_secondaryRank; }
            set { m_secondaryRank = value; }
        }

        #endregion

        #region SecondaryRankText

        public string SecondaryRankText
        {
            get
            {
                if (IsAllowed && m_secondaryRank >= 0)
                    return (m_secondaryRank + 1).ToString();
                return string.Empty;
            }
        }

        #endregion


        #region IsAllowed

        private bool m_isAllowed;
        public bool IsAllowed
        {
            get { return m_isAllowed; }
            set { m_isAllowed = value; }
        }

        #endregion        
    }

    public class TimeInterval
    {
        #region Constuctor

        public TimeInterval() {}

        public TimeInterval(DateTime start, DateTime end)
        {
            m_start = start;
            m_end = end;
        }

        #endregion


        #region Start

        private DateTime m_start;       
        public DateTime Start
        {
            get { return m_start; }
            set { m_start = value; }
        }

        #endregion

        #region End

        private DateTime m_end;
        public DateTime End
        {
            get { return m_end; }
            set { m_end = value; }
        }

        #endregion

        #region DurationMin

        public double DurationMin
        {
            get { return m_end.Subtract(m_start).TotalMinutes; }            
        }

        #endregion

        #region IsIntersectsOrConnects

        public bool IsIntersectsOrConnects(TimeInterval interval)
        {
            if ((interval.End >= Start && interval.End <= End)
                || (interval.Start >= Start && interval.Start <= End)
                || (interval.Start < Start && interval.End > End)
                || (interval.Start > Start && interval.End < End))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region IsIntersects

        public bool IsIntersects(TimeInterval interval)
        {
            if ((interval.End > Start && interval.End <= End)
                || (interval.Start >= Start && interval.Start < End)
                || (interval.Start < Start && interval.End > End)
                || (interval.Start > Start && interval.End < End))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region GetIntersectionDuration

        public double GetIntersectionDuration(TimeInterval interval)
        {
            if (!IsIntersects(interval))
                return 0;            

            if (interval.Start < Start && interval.End > End)
                return DurationMin;
            if (interval.Start > Start && interval.End < End)
                return interval.DurationMin;
            if (Start >= interval.Start && Start < interval.End)
                return interval.End.Subtract(Start).TotalMinutes;
            if (End > interval.Start && End <= interval.End)
                return End.Subtract(interval.Start).TotalMinutes;

            throw new DalworthException("GetIntersectionDuration method failed");
        }

        #endregion
    }
}
