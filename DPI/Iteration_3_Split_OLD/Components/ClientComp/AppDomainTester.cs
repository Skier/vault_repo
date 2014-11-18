using System;

namespace DPI.ClientComp
{	
	public class AppDomainTester
	{
		public static string staticString;
		public AppDomainTester() {}

		public static string StaticString
		{
			get	{ return staticString;		}
			set { staticString = value;		}
		}
	}
}
