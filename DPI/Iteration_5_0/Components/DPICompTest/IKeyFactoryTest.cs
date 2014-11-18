using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.ClientComp;

namespace DPI.ClientComp
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
			IDomKey key = IKeyFactory.getKey(part1, part2);
			Assertion.AssertNotNull(key);
		}
	}
}