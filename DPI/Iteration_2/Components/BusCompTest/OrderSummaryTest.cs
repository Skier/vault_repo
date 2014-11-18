using System;
using System.Data.SqlClient;
using System.Collections;
using NUnit.Framework;

using DPI.Interfaces;
using DPI.Components; 

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class OrderSummaryTest
	{
		public static void Main()
		{
			OrderSummaryTest t = new OrderSummaryTest();
			t.makeOrderSummary();
			t.makeOrderBadILEC();
			t.makeOrderBadZip();
		}
		[Test]
		public void makeOrderSummary()
		{
			UOW uow = new UOW();
			uow.Service = "OrderSummaryTest";

			string zip="75234";
			string ilec="SWB";  // swb, int, ver

			try
			{
				IOrderSummary summ 
					= OrderSummary.BuildOrderSummary(uow, 
						getProdPrices(uow, 3746, 22), zip, ilec, OrderType.New);
			
				Assertion.Assert("makeOrderedProduct: No taxes found.", summ.SubtotalTaxAmt > 0);
				Assertion.Assert("makeOrderedProduct: Products are free?.", summ.TotalAmountDue > 0);
			}
			finally
			{
				uow.close();
			}

		}
		[Test]
		public void makeOrderBadZip()
		{
			UOW uow = new UOW();
			uow.Service = "OrderSummaryTest";
			
			string zip="75x234";
			string ilec="SWB";  // swb, int, ver
	
			try
			{
				ProdPrice[] pp = getProdPrices(uow, 11111111, 22);
				IOrderSummary summ = OrderSummary.BuildOrderSummary(uow, pp, zip, ilec, OrderType.New);

				Assertion.Assert(summ.SubtotalTaxAmt == 0);
				Assertion.Assert(summ.TotalAmountDue == 0);	
			}
			finally
			{
				uow.close();
			}
		}
		[Test]
		public void makeOrderBadILEC()
		{
			UOW uow = new UOW();
			uow.Service = "OrderSummaryTest";

			string zip = "75z34";
			string ilec = "SWB";  // swb, int, ver

			try
			{
				IOrderSummary summ = OrderSummary.BuildOrderSummary(uow, getProdPrices(uow, 3746, 99999), zip, ilec, OrderType.New);
				Assertion.Assert(summ.SubtotalTaxAmt == 0);
				Assertion.Assert(summ.TotalAmountDue == 0);
			}
			finally 
			{
				uow.close();
			}
		}
		ProdPrice[] getProdPrices(UOW uow, int loc, int ilec)
		{	
//			ArrayList al = new ArrayList();
//			ProdPrice[] availProd = ProdPrice.getAvaProdForZip(uow, loc, ilec ); //   "75080", SWB 
////			ProdPrice[] availProd = ProdPrice.getAvaProdForZip(uow, 3746, 22 ); //   "75080", SWB 
//
//			for (int i=0; i<availProd.Length; i++)
//				if (availProd[i].ProdSubclass.Equals("Local Service"))
//				{
//					al.Add(availProd[i]);
//					break;
//				}
//
//			for (int i=0; i<availProd.Length && al.Count<4; i++)
//			{
//				if (availProd[i].ProdType.Equals("Call Features") 
//					&& availProd[i].ProvCategory == ((ProdPrice)al[0]).ProvCategory)
//						al.Add(availProd[i]);
//			}
//			
//
//			ProdPrice[] pp = new ProdPrice[al.Count];
//			al.CopyTo(pp);
//			return pp;

			return new ProdPrice[0];
		}
	}
}