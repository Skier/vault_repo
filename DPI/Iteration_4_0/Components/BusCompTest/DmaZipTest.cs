using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class DmaZipTest
	{
		public static void Main()
		{
			DmaZipTest t = new DmaZipTest();
			t.FindState();
			t.checkZipFound();
			t.checkZipNotFound();
		}
		[Test]
		public void FindState()
		{
			UOW uow = new UOW();
			uow.Service = "addDmaZip";
			DmaZip cls = new DmaZip(uow);
			
			string zip = "75001";        
			string state = 	DmaZip.getState(uow, zip);
		
			Assertion.AssertNotNull(state);
		}
		[Test]
		public void checkZipFound()
		{	
			string zip = "75080";
			UOW uow = new UOW();

			try 
			{
				Assertion.Assert(DmaZip.checkZip(uow, zip));	
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
		[Test]
		public void checkZipNotFound()
		{	
			string zip = "77777";
			UOW uow = new UOW();

			try 
			{
				Assertion.Assert(!DmaZip.checkZip(uow, zip));	
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