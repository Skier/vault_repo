using System;

namespace Dalworth.Common.SDK
{
    public interface ICounterField
    {
        int CounterValue { get;set;}
        String CounterName { get; }
    }
}
