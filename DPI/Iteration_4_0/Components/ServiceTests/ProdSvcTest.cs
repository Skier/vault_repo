using System;
using NUnit.Framework;

using DPI.Interfaces;
using DPI.Components;
using DPI.Services;

namespace DPI.Services
{
	[TestFixture]
	public class ProdSvcTest
	{
		/*		Data		*/
		//jb
		IMap imap;
		IProdPrice selectedBasicService;
		int cntProds;
		IProdPrice[] products;

		IILECInfo ilec;
		string zipcode = "75087";
		int zip = 3741;
		int lec = 22;
		
		/*		Constructors	*/
		public ProdSvcTest()
		{
		}
		/*		Methods		*/
		public static void Main()
		{
			ProdSvcTest test = new ProdSvcTest(); 

//			test.CompareTopProds(); // Requires ProdPrice2.GetTopProducts()...

//			test.getTopProds();
//			test.getDependentProds();
//			test.SuppressZeroPriceProducts();
//			test.AddProd();
//			test.RemProd();

		}
//		[Test]
//		public void CompareTopProds()
//		{
//			imap = new IdentityMap();
//			ilec = new ILECInfoTest(22, "SWB", "Southwestern Bell", false);
//
//			products = ProdSvc.GetTopProd(imap, ilec, zipcode);
//			IProdPrice[] pp2 = ProdPrice2.GetTopProducts(new UOW(imap), zip, lec); 
//			Assertion.Assert(CompProducts(products, pp2));
//		}
		bool CompProducts(IProdPrice[] pp1, IProdPrice[] pp2)
		{
			if (pp1.Length != pp2.Length)
			{
				DifferentLen(pp1, pp2);
				return false;
			}
			int[] keys = new int[pp1.Length];
			for(int i = 0; i < keys.Length; i++)
				keys[i] = pp1[i].ProdId;
		
			Array.Sort(keys, pp1);

			keys = new int[pp2.Length];
			for(int i = 0; i < keys.Length; i++)
				keys[i] = pp2[i].ProdId;

			Array.Sort(keys, pp2);

			bool ok = true;
			
			for(int i = 0; i < pp1.Length; i++)
				if (pp1[i].ProdId != pp2[i].ProdId)
				{
					Console.WriteLine("Mismatch: old prod {0}, new prod {1}", 
						pp1[i].ProdId, pp2[i].ProdId);
					ok = false;
				}
 
			return ok;
		}
		void DifferentLen(IProdPrice[] pp1, IProdPrice[] pp2) 
		{
			for (int i = 0; i < pp1.Length; i++)
				Console.WriteLine("Old prod: {0}", pp1[i].ProdId);

			Console.WriteLine("  ----");

			for (int i = 0; i < pp2.Length; i++)
					Console.WriteLine("New prod: {0}", pp2[i].ProdId);
		}
		[Test]
		public void getTopProds()
		{
//			imap = new IdentityMap();
//			ilec = new ILECInfoTest(22, "SWB", "Southwestern Bell", false);
//
//			products = ProdSvc.GetTopProd(imap, ilec, zipcode);
//			selectedBasicService = products[2];
//			cntProds = products.Length;
//
//			Assertion.Assert(products.Length > 0);
		}
		[Test]
		public void getDependentProds()
		{
//			products = ProdSvc.GetDependentProds(imap, selectedBasicService, ilec, zipcode);
//			for(int i = 0; i < products.Length; i++)
//			{
//				if (products[i].ProdSubclass != "Long Distance")
//					continue;
//
//			//	Console.WriteLine("{0}, ptype: {2} {1}", products[i].ProdName,
//			//		products[i].ProdSelState.ToString(), products[i].ProdType);
//
//				Assertion.Assert(products[i].ProdSelState == ProdSelectionState.Unavailable);
//			}
//			Assertion.Assert(products.Length > 0);
		}
		public void SuppressZeroPriceProducts()
		{
//			products = ProdSvc.GetDependentProds(imap, selectedBasicService, ilec, zipcode);
//			for(int i = 0; i < products.Length; i++)
//			{
//				if (products[i].PackageId != 0)
//					continue;
//
//				if (products[i].UnitPrice == 0)
//				{
//					Console.WriteLine("{0}, subclass: {1} {2}", products[i].ProdName,
//						 products[i].ProdSubclass, products[i].UnitPrice.ToString());
//
//				//	Assertion.Assert(products[i].ProdSelState == ProdSelectionState.Unavailable);
//				}
//			}
//			Assertion.Assert(products.Length > 0);
		}

		[Test]
		public void AddProd()
		{
			int cntBefore = 0;
			int cntAfter = 0;

			for (int i = 0; i < products.Length; i++)
				if (products[i].ProdSelState == ProdSelectionState.Selected)
					cntBefore++;

			products = ProdSvc.AddProd(imap, products, products[4]);

			for (int i = 0; i < products.Length; i++)
				if (products[i].ProdSelState == ProdSelectionState.Selected)
					cntAfter++;

			Assertion.Assert(cntBefore == cntAfter - 1);
		}
		[Test]
		public void RemProd()
		{
			int cntBefore = 0;
			int cntAfter = 0;

			for (int i = 0; i < products.Length; i++)
				if (products[i].ProdSelState == ProdSelectionState.Selected)
					cntBefore++;
		
			products = ProdSvc.RemoveProd(imap, products, products[4]);

			for (int i = 0; i < products.Length; i++)
				if (products[i].ProdSelState == ProdSelectionState.Selected)
					cntAfter++;

			Assertion.Assert(cntBefore == cntAfter + 1);
		}
	}
}