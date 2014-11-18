using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartSchedule
{
    public class Profiler
    {
        private static Stack<DateTime> m_dates;

        static Profiler()
        {
            m_dates = new Stack<DateTime>();
        }

        public static void Start()
        {
            m_dates.Push(DateTime.Now);
        }

        public static void Stop(string logMessage)
        {
            DateTime startTime = m_dates.Pop();
            Host.Trace(logMessage, string.Format("Time taken: {0} sec",
                DateTime.Now.Subtract(startTime).TotalSeconds.ToString("0.00")));
        }
    }
}
