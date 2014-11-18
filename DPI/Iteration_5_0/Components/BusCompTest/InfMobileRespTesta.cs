using System;
using NUnit.Framework;

using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class InfinityMobileResponcesTests
	{
		/*		Data		*/

		string xml = 
			@"<?xml version=""1.0"" encoding=""utf-8""?>"
			+ "<GetPins>" 
				+ "<ReturnValue>Pass</ReturnValue>"
				+ "<ErrorMessage></ErrorMessage>"
				+ "<PinData>" 
					+ "<Pin>999284277248</Pin>"
					+ "<Pin>995423879168</Pin>"
					+ "<Pin>111344975872</Pin>"
				+ "</PinData>"
			+ "</GetPins>";

		
		/*		Methods		*/
		public static void Main()
		{
			InfinityMobileResponcesTests test = new InfinityMobileResponcesTests();
            
			// UOW Tests
			test. PINsTest();
			test. TestStats();
		}
		void TestStats()
		{
			DateTime prev = new DateTime(2006, 4, 9);
			
			if (DateTime.Now.AddMinutes(- Const.REF_INTERVAL) > prev)
				Console.WriteLine("Not Yet");
		}
		//				
		[Test]
		public void PINsTest()
		{
			PINs pins = new PINs(xml);

			Assertion.Assert(pins.GetPins().Length > 0);

		}
	}
}