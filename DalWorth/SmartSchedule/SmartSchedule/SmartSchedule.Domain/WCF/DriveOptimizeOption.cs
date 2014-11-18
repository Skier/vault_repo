using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain.WCF
{
    [DataContract]
    public class DriveOptimizeOption
    {
        public DriveOptimizeOption(List<Visit> visitsToRemove, double newRpm)
        {
            m_visitsToRemove = visitsToRemove;
            m_newDrive = newRpm;
        }

        private List<Visit> m_visitsToRemove;
        [DataMember]
        public List<Visit> VisitsToRemove
        {
            get { return m_visitsToRemove; }
            set { m_visitsToRemove = value; }
        }

        private double m_newDrive;
        [DataMember]
        public double NewDrive
        {
            get { return m_newDrive; }
            set { m_newDrive = value; }
        }

        public string VisitsToRemoveText
        {
            get
            {
                string result = string.Empty;

                foreach (Visit visit in m_visitsToRemove)
                    result += visit.TicketNumber + ", ";

                if (result != string.Empty)
                    return result.Substring(0, result.Length - 2);
                return string.Empty;
            }
        }
    }
}
