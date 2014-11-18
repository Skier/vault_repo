using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech
{
	public enum EventType
	{
		Debug = 0,
		Log = 1,
		Warning = 2,
		Exception = 3
	}

	public interface IEventInfo
	{
		String EventMessage { get; }
#if WIN32
		String EventSource { get; }
#endif
		EventType EventType { get; }


#if WIN32
		String GetFileName();
		int GetFileLineNumber();
		String GetAssemblyName();
#endif
	}
}
