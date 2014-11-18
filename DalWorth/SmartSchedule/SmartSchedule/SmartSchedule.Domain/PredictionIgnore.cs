using System;
using System.Collections.Generic;

namespace SmartSchedule.Domain
{
    public partial class PredictionIgnore
    {
        public PredictionIgnore(){}

        private static List<DateTime> m_datesToIgnore;
        private static List<DateTime> IgnoredDates
        {
            get
            {
                if (m_datesToIgnore == null)
                {
                    List<PredictionIgnore> list = PredictionIgnore.Find();
                    m_datesToIgnore = new List<DateTime>();
                    foreach (PredictionIgnore ignoreItem in list)
                        m_datesToIgnore.Add(ignoreItem.IgnoreDate.Date);
                }

                return m_datesToIgnore;
            }
        }

        public static bool IsDateIgnored(DateTime dateTime)
        {
            return IgnoredDates.Contains(dateTime.Date);
        }

        public static void ModifyIgnoreDate(DateTime dateTime, bool isIgnore)
        {
            if (isIgnore)
            {
                m_datesToIgnore.Add(dateTime.Date);
                Insert(new PredictionIgnore(dateTime.Date));
            } 
            else
            {
                m_datesToIgnore.Remove(dateTime.Date);
                Delete(new PredictionIgnore(dateTime.Date));
            }
        }
    }
}
      