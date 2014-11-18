using System;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class EventLog:ICounterField
    {
        public EventLog()
        {

        }

        public EventLog(IEventInfo eventInfo)
        {
            m_eventType = (int)eventInfo.EventType;
            m_message = eventInfo.EventMessage;
            m_createDate = DateTime.Now;
        }

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_eventLogId;
            }
            set
            {
                m_eventLogId = value;
            }
        }

        const String counterName = "eventlog";

        public string CounterName
        {
            get { return counterName; }
        }

        #endregion
    }
}
