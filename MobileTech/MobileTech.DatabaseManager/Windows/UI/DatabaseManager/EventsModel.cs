using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Data;

using MobileTech.Domain;


namespace MobileTech.Windows.UI.DatabaseManager
{
	public class EventsModel:IModel
	{
		EventInfoTableModel m_events;

		public EventInfoTableModel Events
		{
			get
			{
				return m_events;
			}
		}

        public void Init()
        {
            List<EventLog> list = EventLog.Find();

            m_events = new EventInfoTableModel(list);
        }

    }
}
