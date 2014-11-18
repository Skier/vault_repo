using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain
{
    [DataContract]
    public class TimeInterval : IEquatable<TimeInterval>
    {
        #region Constuctor

        public TimeInterval() { }

        public TimeInterval(DateTime start, DateTime end)
        {
            m_start = start;
            m_end = end;
        }

        #endregion


        #region Start

        private DateTime m_start;
        [DataMember]
        public DateTime Start
        {
            get { return m_start; }
            set { m_start = value; }
        }

        #endregion

        #region End

        private DateTime m_end;
        [DataMember]
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

        #region GetIntersectingInterval

        public TimeInterval GetIntersectingInterval(TimeInterval interval)
        {
            TimeInterval result = new TimeInterval();

            if (interval.Start > Start)
                result.Start = interval.Start;
            else
                result.Start = Start;

            if (interval.End < End)
                result.End = interval.End;
            else
                result.End = End;

            return result;
        }

        #endregion

        #region Union

        public List<TimeInterval> Union(TimeInterval interval)
        {
            List<TimeInterval> result = new List<TimeInterval>();

            if (IsIntersectsOrConnects(interval))
            {
                TimeInterval commonInterval = new TimeInterval(
                    Start < interval.Start ? Start : interval.Start,
                    End > interval.End ? End : interval.End);
                result.Add(commonInterval);
                return result;
            }

            if (Start < interval.Start)
            {
                result.Add(this);
                result.Add(interval);
            }
            else
            {
                result.Add(interval);
                result.Add(this);
            }

            return result;
        }

        #endregion

        #region Substract

        //Substract time interval from current, returns resulted intervals
        //returns empty collection if substraction erases current interval
        public List<TimeInterval> Substract(TimeInterval interval)
        {
            List<TimeInterval> result = new List<TimeInterval>();

            if (!IsIntersects(interval))
            {
                result.Add(this);
                return result;
            }
            else
            {
                if (interval.Start <= Start && interval.End >= End)
                    return result;

                if (interval.Start > Start && interval.End < End)
                {
                    result.Add(new TimeInterval(Start, interval.Start));
                    result.Add(new TimeInterval(interval.End, End));
                    return result;
                }

                if (interval.Start <= Start)
                {
                    result.Add(new TimeInterval(interval.End, End));
                    return result;
                }

                if (interval.End >= End)
                {
                    result.Add(new TimeInterval(Start, interval.Start));
                    return result;
                }
            }

            return result;
        }

        #endregion

        #region IsWithin

        //Checks if current interval completele within specified one
        public bool IsWithin(TimeInterval interval)
        {
            return m_start >= interval.Start && m_end <= interval.End;            
        }

        #endregion


        #region Equals & GetHashCode

        public bool Equals(TimeInterval timeInterval)
        {
            if (timeInterval == null) return false;
            return Equals(m_start, timeInterval.m_start) && Equals(m_end, timeInterval.m_end);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as TimeInterval);
        }

        public override int GetHashCode()
        {
            return m_start.GetHashCode() + 29 * m_end.GetHashCode();
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return string.Format("{0} - {1}", m_start.ToShortTimeString(), m_end.ToShortTimeString());
        }

        #endregion
    }
}
