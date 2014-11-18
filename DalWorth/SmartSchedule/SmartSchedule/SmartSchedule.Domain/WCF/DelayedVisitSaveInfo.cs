using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain.WCF
{
    [DataContract]
    public class DelayedVisitSaveInfo
    {
        public DelayedVisitSaveInfo(Visit delayedVisit, List<int> doNotCallVisitIds)
        {
            DelayedVisit = delayedVisit;
            DoNotCallVisitIds = doNotCallVisitIds;
        }

        [DataMember]
        public Visit DelayedVisit { get; set; }
        [DataMember]
        public List<int> DoNotCallVisitIds { get; set; }
    }
}
