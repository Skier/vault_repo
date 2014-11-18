using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class LocationTest
	{
		public static void Main()
		{
			LocationTest t = new LocationTest();
			t.findByName();

		}
		[Test]
		public void findByName()
		{	

			string zip = "75080";

			UOW uow = new UOW();

			try 
			{
				Location loc = Location.find(uow, zip);
				Assertion.Assert(loc.Name == zip);	
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				uow.close();
			}
		}
	}
}