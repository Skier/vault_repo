using System;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;
using DPI.ClientComp;

namespace DPI.ClientCompTest
{
	[TestFixture]
	public class FormatPhoneTest
	{
		/*		Data		*/

		string phone = "1234567890"; 
		public static void Main()
		{
			FormatPhoneTest tests = new FormatPhoneTest();
			tests.NpaTest();
		}
		[Test]
		public void NpaTest()
		{
			Assertion.Assert(FormatPhone.ShowPhone(PhoneNumParts.Npa, phone) == "123");
		}
	}
}