using System;

namespace Dalworth.Server.Domain
{
    public class DateRange
    {
        public DateRange() : this(null, null) {}

        public DateRange(DateTime? startDate, DateTime? endDate)
        {
            m_startDate = startDate;
            m_endDate = endDate;
        }

        private DateTime? m_startDate;
        public DateTime? StartDate
        {
            get { return m_startDate; }
            set { m_startDate = value; }
        }

        private DateTime? m_endDate;
        public DateTime? EndDate
        {
            get { return m_endDate; }
            set { m_endDate = value; }
        }

        public bool IsNull
        {
            get
            {
                return !m_startDate.HasValue && !m_endDate.HasValue;
            }
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            DateRange dateRange = obj as DateRange;
            if (dateRange == null) return false;
            return Equals(m_startDate, dateRange.m_startDate) && Equals(m_endDate, dateRange.m_endDate);
        }

        public override int GetHashCode()
        {
            return m_startDate.GetHashCode() + 29*m_endDate.GetHashCode();
        }
    }
}
