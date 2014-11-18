using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class ILECInfoTest
	{
		public static void Main()
		{
			ILECInfoTest t = new ILECInfoTest();
			t.getIlecs();
		}
		[Test]
		public void getIlecs()
		{	
			string zip = "75080";
			
			UOW uow = new UOW();
			ILECInfo[] ilecs;
			
			try 
			{
				ilecs = ILECInfo.getILECs(uow, zip);
				Assertion.Assert(ilecs.Length > 0);	
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