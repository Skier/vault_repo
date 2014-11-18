using System;
using System.Collections.Generic;
using System.Text;

namespace Servman.SDK
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
        EventType EventType { get; }
    }
}
