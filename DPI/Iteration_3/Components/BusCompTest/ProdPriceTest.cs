using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class ProdPriceTest
	{
		//	ProdPriceView data;

		public static void Main()
		{
			ProdPriceTest t = new ProdPriceTest();
			t.getProdPrice();

		}

		public ProdPriceTest()
		{
			
			//ProdPriceTestView pptv = new ProdPriceTestView();

		}
		[Test]
		public void getProdPrice()
		{	

			int loc = 3746;
			int ilec = 24; // dpi

			UOW uow = new UOW();

			try 
			{
//				ProdPrice[] availProd = ProdPrice.getAvaProdForZip(uow, loc, ilec);
////				for (int i = 0; i < availProd.Length; i++)
////				{
////					if (availProd[i].IsPreselectedWebOrderL2)
////						Console.WriteLine("Preselected {0} {1}", availProd[i].ProdId, availProd[i].ProdName);
////				}
//				Assertion.Assert(availProd.Length > 0);	
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