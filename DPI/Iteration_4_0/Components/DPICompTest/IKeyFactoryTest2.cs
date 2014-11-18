using System;
using NUnit.Framework;
using DPI.Interfaces;

namespace DPI.Components
{
	[TestFixture]
	public class IKeyFactoryTest
	{
		/*		Data		*/

		const string part1 = "IKeyFactory";
		const string part2 = "Test";
 
		public static void Main()
		{
			IKeyFactoryTest tests = new IKeyFactoryTest();
			tests.makeKey();
		}
		[Test]
		public void makeKey()
		{
			IDomKey key = KeyFactory.getKey(part1, part2);
			Assertion.AssertNotNull(key);
		}
	}
}