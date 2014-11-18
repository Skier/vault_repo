using System;
using System.Data;
using System.Data.SqlClient;

using DPI.Components;
using DPI.Interfaces;
using NUnit.Framework;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class StoreWrapperTests
	{
		/*		Methods		*/
		public static void Main()
		{
			StoreWrapperTests test = new StoreWrapperTests();
            
			test.EndOfDayDTOTest();

		}

		[Test]
		public void EndOfDayDTOTest()
		{
			UOW uow = new UOW();

			EndOfDayDTO dto = StoreWrapper.Store_EndOfDay(uow, "NCRW0430RW", "123", new DateTime(2006, 3, 8));
			Assertion.Assert(dto.LocalRev > 0);
		}
	}
}