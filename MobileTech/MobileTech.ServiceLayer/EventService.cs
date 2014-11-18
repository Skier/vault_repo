using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Domain = MobileTech.Domain;
using System.Diagnostics;
using MobileTech.Data;


namespace MobileTech.ServiceLayer
{
	/// <summary>
	/// 
	/// </summary>
	public class EventService
	{
		public event EventInfoEventHandler Events;

		private EventService()
		{
			Host.Instance.Events += new EventInfoEventHandler(OnHostEvents);
		}

        void OnHostEvents(IEventInfo eventInfo)
		{
			_AddEvent(eventInfo);
		}

		private static EventService m_eventService;
		public static EventService Instance
		{
			get
			{
				if (m_eventService == null)
					m_eventService = new EventService();

				return m_eventService;
			}
		}

		protected void _AddEvent(IEventInfo eventInfo)
		{

            //Reflecting event
            if (Events != null)
                Events.Invoke(eventInfo);

            //Save to database;

		    StoreEvent(eventInfo);

		}

		private void StoreEvent(IEventInfo eventInfo)
		{

            if (eventInfo.EventType >= Configuration.EventStoreLevel)
            {
                if (Database.IsDatabaseExist())
                {

                    try
                    {
                        Domain.EventLog eventLog = new Domain.EventLog(eventInfo);
                        eventLog.SessionId = Domain.Session.FindCurrent().SessionId;

                        Domain.Counter.Assign(eventLog);

                        Domain.EventLog.Insert(eventLog);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message + e.StackTrace);
                    }
                }
            }
            
		}

		public static void AddEvent(IEventInfo eventInfo)
		{
			Instance._AddEvent(eventInfo);
		}

		public static void AddLogEvent(string message)
		{
			Instance._AddEvent(new EventInfo(message));
		}
	}
}
