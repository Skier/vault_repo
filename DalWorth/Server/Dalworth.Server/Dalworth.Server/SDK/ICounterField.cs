using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.SDK
{
	public interface ICounterField
	{
		int CounterValue { get;set;}
        String CounterName { get; }
	}
}
