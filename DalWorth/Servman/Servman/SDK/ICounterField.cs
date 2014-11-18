using System;
using System.Collections.Generic;
using System.Text;

namespace Servman.SDK
{
    public interface ICounterField
    {
        int CounterValue { get;set;}
        String CounterName { get; }
    }
}
