using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SmartSchedule.SDK
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
                throw new DalworthException("For exceptions use DalworthException class");

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
