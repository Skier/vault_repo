using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TractInc.Server.SDK
{
    public class EventInfo:IEventInfo
    {
        String m_message;

        EventType m_eventType;
        
        public EventInfo(String message)
        {
            m_message = message;
            m_eventType = EventType.Log;

            Init();
        }

        public EventInfo(String message, EventType eventType)
        {

            if (eventType == EventType.Exception)
                throw new TractIncException("For exceptions use TractIncException class");

            m_message = message;
            m_eventType = eventType;

            Init();
        }



        void Init()
        {
        }



        #region IEventInfo Members



        public string EventMessage
        {
            get
            {
                return m_message;
            }
        }
        public EventType EventType
        {
            get
            {
                return m_eventType;
            }
        }

        #endregion


        public override string ToString()
        {
            return m_message;
        }
    }
}
