using System;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;
using DPI.ClientComp;

namespace DPI.ClientCompTest
{
	[TestFixture]
	public class WIPTest
	{
		/*		Data		*/

		const string part1 = "Wip";
		const string part2 = "Test";
 
		public static void Main()
		{
			WIPTest tests = new WIPTest();
			//tests.storeInImap();
		}
		[Test]
		public void NoTestCode()
		{
		 	Assertion.Assert(true);
		}

		/*  // commented so solution will compile.
		[Test]
		public void storeInImap()
		{
			string val = "some value";

			TestWip wip = new TestWip();
			wip.attr = val;
			IMap imap = IMapFactory.getIMap();
			imap.add(wip);

			wip = null;
			wip = (TestWip)imap.find(TestWip.WipKey);

			Assertion.Assert(wip.attr == val);
		}
		/*
		public class TestWip : WIP
		{
			public string attr;
			//public override IDomKey IKey
			//{	
			//	get 
			//	{
			//		return KeyFactory.getKey(part1, part2);
			//	}
			//}
			public static IDomKey WipKey
			{
				get { return new TestWip().IKey; }
			}
			public FirstStep{ get {}}
		}
		*/
	}
}