using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech
{
	public interface ICounterField
	{
		int CounterValue { get;set;}
        String CounterName { get; }
	}
}
