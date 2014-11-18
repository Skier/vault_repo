using System;
using System.Text;

using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class ProdPrice2Test
	{
		//	ProdPriceView data;
		LocLec[] locs = new LocLec[] 
		{ 
			new LocLec("75080", 22), 
			new LocLec("75087", 22), 
			new LocLec("63013", 22),   
			new LocLec("70056", 21),  
			new LocLec("32132", 21),  
			new LocLec("16117", 18),  
			new LocLec("75080", 18),  
			new LocLec("63090", 17),  
			new LocLec("22003", 17)  
		};

		public struct LocLec
		{
			public readonly string zip;
			public readonly int ilec;

			public LocLec(string zip, int ilec)
			{
				this.zip = zip;
				this.ilec = ilec;
			}
		}
		public static void Main()
		{
			ProdPrice2Test t = new ProdPrice2Test();
		//	t.GetZips();
			t.GetTopProds();
			t.GetFees();

		}
		[Test]
		public void GetFees()
		{
			UOW uow = null;

			int prod = 201; 
			int loc; 
			string lec = "SWB";
			string zip = "75080";
			int ilec = 22;

			ProdPrice[] pp = null;
			ProdPrice2[] pp2 = null;

			try
			{
				uow = new UOW();
				loc = Location.find(uow, zip).LocId;
				pp2 = ProdPrice2.GetFees(uow, prod, loc, ilec, OrderType.New);
				pp = ProductFee.getFeesForProd(uow, prod, zip, lec, OrderType.New);
				FindMismatch(pp, pp2);
			}
			finally
			{
				uow.close();
			}
		}
		[Test]
		public void GetZips()
		{
			UOW uow = null;

			try
			{
				uow = new UOW();

				for (int i = 0; i < locs.Length; i++)
				{
					Console.WriteLine("*** ILEC {0}, Location {1} ***",	locs[i].ilec,locs[i].zip); 
					getProdPrice(uow, Location.find(uow, locs[i].zip).LocId, locs[i].ilec);
				}
			}
			finally
			{
				uow.close();
			}
		}
		[Test]
		public void GetTopProds()
		{
			UOW uow = null;

			try
			{
				uow = new UOW();

				for (int i = 0; i < locs.Length; i++)
				{
					Console.WriteLine("*** ILEC {0}, Location {1} ***",	locs[i].ilec,locs[i].zip); 
					GetTopProds(uow, Location.find(uow, locs[i].zip).LocId, locs[i].ilec);
				}
			}
			finally
			{
				uow.close();
			}
		}
		void GetTopProds(UOW uow, int loc, int ilec)
		{	
			ProdPrice2[] tops = ProdPrice2.GetTopProducts(uow, loc, ilec);
			Assertion.Assert(tops.Length > 0);	
			
			for(int i = 0; i < tops.Length; i++)
				Console.WriteLine("Basic service: {0} {1}", tops[i].ProdId, tops[i].ProdName);
		}
		void getProdPrice(UOW uow, int loc, int ilec)
		{	
//			ProdPrice2[] billable = ProdPrice2.GetBillableForZip(uow, loc, ilec);
//			ProdPrice[] pps = ProdPrice.getAvaProdForZip(uow, loc, ilec);
//		
//			Assertion.Assert(billable.Length > 0);	
//	
//			for (int i = 0; i < billable.Length; i++)
//				Assertion.Assert(billable[i].IsBillable);
//			
//			FindMismatch(pps, billable);
		}
		
		void FindMismatch(ProdPrice[] one, ProdPrice2[] two)
		{
			for (int i = 0; i < one.Length; i++)
				if (!FindNew(one[i].ProdId, two))
				{
		//			Assertion.Assert(String.Format("Miising in the new {0} {1}", one[i].ProdId, one[i].ProdName), false);
					Console.WriteLine("Missing in the new {0} {1}", one[i].ProdId, one[i].ProdName);
				}
			for (int i = 0; i < two.Length; i++)
				if (!FindOld(two[i].ProdId, one))
					Console.WriteLine("Miising in the old {0} {1} start mon {2}",
						two[i].ProdId, two[i].ProdName, two[i].StartServMon);

		}
		bool FindOld(int newProd, ProdPrice[] oldProds)
		{
			for (int i = 0; i < oldProds.Length; i++)
				if (oldProds[i].ProdId == newProd)
					return true;
			
			return false; 
		}

		bool FindNew(int old, ProdPrice2[] newProds)
		{
			for (int i = 0; i < newProds.Length; i++)
				if (newProds[i].ProdId == old)
					return true;
			
			return false; 
		}
	}
}