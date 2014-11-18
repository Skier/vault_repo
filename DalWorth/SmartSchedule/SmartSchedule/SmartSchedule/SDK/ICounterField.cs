using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule.SDK
{
	public interface ICounterField
	{
		int CounterValue { get;set;}
        String CounterName { get; }
	}
}
