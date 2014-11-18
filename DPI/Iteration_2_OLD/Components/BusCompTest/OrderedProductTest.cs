using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class OrderedProductTest
	{
		public static void Main()
		{
			OrderedProductTest t = new OrderedProductTest();
			t.testBasicService();
			t.testPackage();
			
		}

		public OrderedProductTest()
		{
			
		}

		[Test]
		public void testBasicService()
		{
			UOW uow = new UOW();
			uow.Service = "OrderedProductTest";

			OrderedProduct op;
			
			try{
				ProdPrice pp= getProdPrice(uow, "Local Service");
				op=makeOrderedProduct(uow, pp);
				Assertion.Assert("makeOrderedProduct: No taxes found.", op.Taxes.Length > 0);
				Assertion.Assert("makeOrderedProduct: No fees found.", op.Fees.Length > 0);
				Assertion.Assert("makeOrderedProduct: Product is free?.", op.TotalAmt > 0);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Assertion.Assert(e.Message, false);
			}
			finally
			{
				uow.close();
			}
		}

		[Test]
		public void testPackage()
		{

			UOW uow = new UOW();
			uow.Service = "OrderedProductTest";
			OrderedProduct op;
			try
			{
				ProdPrice pp= getProdPrice(uow, "PKG");
				op=makeOrderedProduct(uow, pp);
				Assertion.Assert("makeOrderedProduct: No taxes found.", op.Taxes.Length > 0);
//				Assertion.Assert("makeOrderedProduct: Product is free?.", op.TotalAmt > 0);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Assertion.Assert(e.Message, false);
			}
			finally
			{
				uow.close();
			}
		}


		public OrderedProduct makeOrderedProduct(UOW uow, ProdPrice pp)
		{
			string zip="75234";
			string ilec="SWB";  // swb, int, ver
			
//			ProdPrice pp;
			OrderedProduct op;
			

			Console.WriteLine("getprodprice");
			Console.WriteLine("prodprice #{0}", pp.ProdName);
			Console.WriteLine("create OP");
			op=new OrderedProduct(uow, pp, zip, ilec, OrderType.New,1); 
			Console.WriteLine("dumping");
//			op.Dump();// moved below

			return op;
		}

		ProdPrice getProdPrice(UOW uow, string subclass)
		{	
			ProdPrice pp=null;
//			int loc = 3746;
//			int ilec = 24; // dpi

			try 
			{
				ProdPrice[] availProd = ProdPrice.getAvaProdTest(uow);

				Assertion.Assert(availProd.Length > 0);	
				Console.WriteLine("Got avails");

				for (int i=0; i<availProd.Length; i++)
				{
					if (availProd[i].ProdSubclass.Equals(subclass))
					{
						pp=availProd[i];
						break;
					}
				}
				Assertion.Assert("subclass not found?", pp != null);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Assertion.Assert(e.Message, false);
			}
	//		Console.WriteLine("uow status");
	//		Console.WriteLine("uow status={0}", uow.Cn.State.ToString());
			return pp;
		}
	}


}

/*  // ripped out of OrderedProduct.cs
		public void Dump()
		{
			Console.WriteLine("Dumping OrderedProduct:");
			Console.WriteLine("product = {0}: {1}", prod.ProdId, prod.ProdName);
			for (int i=0; components!=null && i<components.Length; i++)
			{
				Console.WriteLine("component = {0}: {1}", components[i].priceInfo.ProdId, components[i].priceInfo.Description);
			}

			for (int i=0; fees!=null && i<fees.Length; i++)
			{
				Console.WriteLine("fee = {0}: {1}", fees[i].ProdId, fees[i].Description);
			}

			for (int i=0; taxes!=null && i< taxes.Length; i++)
			{
				Console.WriteLine("tax = ${0}: {1}", taxes[i].TaxAmt, taxes[i].taxDescription);
			}

			for (int i=0; taxesOnFees!=null && i< taxesOnFees.Length; i++)
			{
				Console.WriteLine("tax = ${0}: {1}", taxesOnFees[i].TaxAmt, taxesOnFees[i].taxDescription);
			}

			Console.WriteLine("taxesAmt: ${0}",this.taxesAmt);
			Console.WriteLine("taxesOnFeesAmt: ${0}",this.taxesOnFeesAmt);
			Console.WriteLine("totalAmt: ${0}",this.totalAmt);
			Console.WriteLine("action: ${0}",this.action);

			Console.WriteLine("Done dumping OrderedProduct.");
			Console.WriteLine("");
		}
*/