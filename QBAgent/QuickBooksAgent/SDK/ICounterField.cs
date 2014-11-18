using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBooksAgent
{
	public interface ICounterField
	{
		int CounterValue { get;set;}
        String CounterName { get; }
	}
}
