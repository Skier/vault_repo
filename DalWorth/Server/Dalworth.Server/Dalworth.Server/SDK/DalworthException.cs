using System;
using System.Collections.Generic;
using System.Text;
using d = System.Diagnostics;

namespace Dalworth.Server.SDK
{
	public class DalworthException:Exception,IEventInfo
	{
		public DalworthException(String message):base(message)
		{
			Init();
		}

		public DalworthException(Exception e):base(e.Message,e)
		{
			Init();
		}

        public DalworthException(String message,Exception innerExeption)
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
