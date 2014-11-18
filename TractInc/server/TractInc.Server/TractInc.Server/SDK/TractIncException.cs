using System;
using System.Collections.Generic;
using System.Text;
using d = System.Diagnostics;

namespace TractInc.Server.SDK
{
    public class TractIncException:Exception,IEventInfo
    {
        public TractIncException(String message):base(message)
        {
            Init();
        }

        public TractIncException(Exception e):base(e.Message,e)
        {
            Init();
        }

        public TractIncException(String message,Exception innerExeption)
            : base(message, innerExeption)
        {
            Init();
        }

        void Init()
        {
        }

        public override string ToString()
        {
            return String.Format("{0}",
                Message);
        }

        #region IEventInfo Members

        public string EventMessage
        {
            get 
            {
                return Message;
            }
        }

        public EventType EventType
        {
            get 
            {
                return EventType.Exception;
            }
        }

        #endregion
}
}
