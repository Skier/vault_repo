using System;
using System.Reflection;

using DPI.Components;
using DPI.Interfaces;
using NUnit.Framework;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class ReflectionTest
	{
		public static void Main()
		{
			ReflectionTest tst = new ReflectionTest();
			tst.LoadClass();
		}
		[Test]
		public void LoadClass()
		{
			string cardNumber = "12345";
			CardApp app = (CardApp)CLoader.LoadObject("BusComp", "DPI.Components.CardApp");
			app.CardNum = cardNumber;

			Assertion.AssertEquals(app.CardNum, cardNumber);
		}
	}
}