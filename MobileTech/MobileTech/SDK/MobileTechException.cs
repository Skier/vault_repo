using System;
using System.Collections.Generic;
using System.Text;
using d = System.Diagnostics;

namespace MobileTech
{
	public class MobileTechException:Exception,IEventInfo
	{

#if WIN32
		String m_fileName,m_assemblyName;
		int m_fileLineNumber;
#endif		

		public MobileTechException(String message):base(message)
		{
			Init();
		}

		public MobileTechException(Exception e):base(e.Message,e)
		{
			Init();
		}

        public MobileTechException(String message,Exception innerExeption)
            : base(message, innerExeption)
        {
            Init();
        }

		void Init()
		{
			// Frame index calculation
			// EventInfo.Init = 0
			// EventInfo.Constuctor = 1
			// Creator = 2
#if WIN32
			d.StackTrace stack = new System.Diagnostics.StackTrace();
			d.StackFrame frame = stack.GetFrame(2);

			m_fileLineNumber = frame.GetFileLineNumber();

			m_fileName = frame.GetFileName();

			m_assemblyName = frame.GetMethod().DeclaringType.AssemblyQualifiedName;
#endif
		}

		#region IEventInfo Members


#if WIN32
		public String GetFileName()
		{
			return m_fileName;
		}

		public int GetFileLineNumber()
		{
			return m_fileLineNumber;
		}

		public String GetAssemblyName()
		{
			return m_assemblyName;
		}
#endif
		#endregion


		public override string ToString()
		{
#if WIN32
			return String.Format("{0} {1}\\{2} {3}",
				Source,
				m_fileName,
				m_fileLineNumber,
				Message);
#else
			return String.Format("{0}",
				Message);
#endif
		}

		#region IEventInfo Members

		public string EventMessage
		{
			get 
			{
				return Message;
			}
		}
#if WIN32
		public string EventSource
		{
			get 
			{
				return Source;
			}
		}
#endif
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
