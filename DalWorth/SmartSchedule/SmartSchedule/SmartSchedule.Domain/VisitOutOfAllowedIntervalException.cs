using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain
{
    public class VisitOutOfAllowedIntervalException : Exception
    {
        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
        }

        public VisitOutOfAllowedIntervalException(Visit visit)
        {
            m_visit = visit;
        }
    }
}
