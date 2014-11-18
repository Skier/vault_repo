using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QuickBooksAgent
{
	public class EventInfo:IEventInfo
	{
		String m_message;

		EventType m_eventType;
		
#if WIN32		
		String m_fileName;
		int m_fileLineNumber;
		String m_source, m_assemblyName;
#endif

		public EventInfo(String message)
		{
			m_message = message;
			m_eventType = EventType.Log;

			Init();
		}

		public EventInfo(String message, EventType eventType)
		{

			if (eventType == EventType.Exception)
				throw new QuickBooksAgentException("For exceptions use QuickBooksAgentException class");

			m_message = message;
			m_eventType = eventType;

			Init();
		}



		void Init()
		{
			// Frame index calculation
			// EventInfo.Init = 0
			// EventInfo.Constuctor = 1
			// Creator = 2

#if WIN32
			StackTrace stack = new System.Diagnostics.StackTrace();
			StackFrame frame = stack.GetFrame(2);

			m_fileLineNumber = frame.GetFileLineNumber();

			m_fileName = frame.GetFileName();

			m_source = String.Format("{0}.{1}.{2}",
				frame.GetMethod().DeclaringType.Namespace,
				frame.GetMethod().DeclaringType.Name,
				frame.GetMethod().Name);

			m_assemblyName = frame.GetMethod().DeclaringType.AssemblyQualifiedName;
#endif
		}



		#region IEventInfo Members



		public string EventMessage
		{
			get
			{
				return m_message;
			}
		}
#if WIN32
		public string EventSource
		{
			get
			{

				return m_source;


			}
		}
#endif
		public EventType EventType
		{
			get
			{
				return m_eventType;
			}
		}

#if WIN32
		public string GetFileName()
		{
			return m_fileName;
		}

		public int GetFileLineNumber()
		{
			return m_fileLineNumber;
		}

		public string GetAssemblyName()
		{
			return m_assemblyName;
		}
#endif
		#endregion


		public override string ToString()
		{
#if WIN32

			return String.Format("{0} {1}\\{2} {3}",
				m_source,
				m_fileName,
				m_fileLineNumber,
				m_message);
#else
			return m_message;
#endif

		}
	}
}
