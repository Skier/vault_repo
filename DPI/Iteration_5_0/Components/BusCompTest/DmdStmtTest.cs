//using System;
//using System.Data;
//using System.Collections;
//using System.Data.SqlClient;
//using NUnit.Framework;
//using DPI.Components;
//using DPI.Interfaces;
//
//namespace DPI.ComponentsTests
//{
//	[TestFixture]
//	public class DmdStmtTest
//	{
//		/*		Data		*/
//		int dmdId;
//
//        
//		/*		Constructors		*/
//		public DmdStmtTest()
//		{
//			// try { cleanup(); } 	catch {}
//			// Console.WriteLine("Cleanup completed");
//		}
//        
//		/*		Methods		*/
//		public static void Main()
//		{
//			DmdStmtTest test = new DmdStmtTest();
//			test.addDIs();
//			test.GetDis();
//            
//		}
//		public void GetDis()
//		{
//			UOW uow = new UOW();
//			uow.Service = "addDemand And Items";
//
//			Demand dmd = null;
//			try
//			{
//				dmd = Demand.find(uow, dmdId);
//				OrderSum sum = (OrderSum)dmd.OrderSummary(uow);
//				
//				int months = 3;
//
//				for (int i = 0; i < months; i++)
//					ReportMonth(dmd, sum, i + 1);
//
//                months++;
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine(e.Message);
//			}
//			finally
//			{
//				uow.close();
//			}
//		}
//		void ReportMonth(Demand dmd, OrderSum sum, int mon)
//		{
//			Console.WriteLine("");
//		    Console.WriteLine("---------------------------------------------");
//			Console.WriteLine("Order Number {1} for Month {0}", mon, dmd.Id);	
//			Console.WriteLine("");
//				
//			for (int i = 0; i < sum.Items.Length; i++)
//				ReportItem((DmdItem)sum.Items[i]);
//			
//			Console.WriteLine("Product Subtotal {0}",sum.GetProdSubTotal(mon));
//
//			IProdTax[] taxes = sum.GetTaxes(mon);
//			decimal fees = sum.GetTaxAmt(mon);
//			decimal tot  = sum.GetTotalAmtDue(mon);
//				
//			IProdTax[] combs = sum.GetTaxSummary(mon);
//				
//			Console.WriteLine("Tax summary");
//
//			for (int i = 0; i < combs.Length; i++)
//				Console.WriteLine("       {0} {1}", 
//					combs[i].Description, 
//					combs[i].TaxAmt.ToString("C")); 
//
//			Console.WriteLine("Total Taxes {0}",	sum.GetTaxAmt(mon));
//			Console.WriteLine("Total Fees  {0}",	sum.GetFeeAmt(mon));
//
//			Console.WriteLine("    Month {1} Amount Due  {0}", 
//				sum.GetTotalAmtDue(mon), mon);
//			Console.WriteLine("---------------------------------------------");
//			Console.WriteLine("");
//
//		}
//		void ReportItem(DmdItem di)
//		{
//			if (di.Components.Length > 0)
//				Console.WriteLine("Package {0} {1} {2} starts {3} ends {4}", 
//					di.Prod, 
//					ProdInfoCol.GetProd(di.Prod).BillText,
//					di.PriceAmt,
//					ProdInfoCol.GetProd(di.Prod).StartServMon,
//					ProdInfoCol.GetProd(di.Prod).EndServMon);
//			else
//				Console.WriteLine("Item: {0} {1} {2} starts {3} ends {4}", 
//					di.Prod, 
//					ProdInfoCol.GetProd(di.Prod).BillText,
//					di.PriceAmt,
//					ProdInfoCol.GetProd(di.Prod).StartServMon,
//					ProdInfoCol.GetProd(di.Prod).EndServMon);
//	
//			if (di.Components.Length > 0)
//			{
//				for (int i = 0; i < di.Components.Length; i++)
//                    Console.WriteLine("      Comp: {0} {1}", 
//						di.Components[i].Prod, 
//						ProdInfoCol.GetProd(di.Components[i].Prod).BillText);
//			}
//
//			if (di.TagAlongs.Length > 0)
//			{
//				for (int i = 0; i < di.TagAlongs.Length; i++)
//					Console.WriteLine("      Fee: {0} {1} {2} starts {3} ends {4}",  
//						di.TagAlongs[i].Prod,
//						ProdInfoCol.GetProd(di.TagAlongs[i].Prod).BillText,
//						di.TagAlongs[i].PriceAmt,
//					    ProdInfoCol.GetProd(di.TagAlongs[i].Prod).StartServMon,
//					    ProdInfoCol.GetProd(di.TagAlongs[i].Prod).EndServMon);
//						
//
//			}
//			Console.WriteLine("");
//		}
//		[Test]
//		public void addDIs()
//		{
//			UOW uow = new UOW();
//			uow.Service = "addDemand And Items";
//
//			Demand dmd = null;
//			try
//			{
//				dmd = CreateDmd(uow);
//				ProdPrice2[] prods =  GetProds(uow, dmd);
//				DmdItem[] dis      = new DmdItem[prods.Length];
//			
//				for (int i = 0; i <  dis.Length; i++)
//				{
//					dis[i] = new DmdItem(uow, prods[i], dmd);	
//					dis[i].BuildComps(uow);
//				}
//
//				uow.commit();
//				dmdId = dmd.Id;
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine(e.Message);
//			}
//			finally
//			{
//				uow.close();
//			}
//		}
//		ProdPrice2[] GetProds(UOW uow, Demand dmd)
//		{
//			ProdPrice2[] billable = ProdPrice2.GetBillableForZip(uow, dmd.Loc, dmd.Ilec);
//			
//			for (int i = 0; i < billable.Length; i++)		
//			{
//				billable[i].ProdSelState = ProdSelectionState.Unavailable;
//				if (billable[i].IsPreselectedWebOrderL2)
//					billable[i].ProdSelState = ProdSelectionState.Selected;
//			}
//
//			for (int i = 0; i < billable.Length; i++)		
//				if (billable[i].ProdSubclass.Trim().ToLower() == Const.LOCAL_SERVICE.ToLower())
//				{
//					billable[i].ProdSelState = ProdSelectionState.Selected;
//					break;
//				}
//					
//			ArrayList ar = new ArrayList();
//			for (int i = 0; i < billable.Length; i++)
//				if (billable[i].ProdSelState == ProdSelectionState.Selected)
//					ar.Add(billable[i]);
//
////			ProdPrice2[] res = new ProdPrice2[1];
////			res[0] = (ProdPrice2)ar[0];
//			ProdPrice2[] res = new ProdPrice2[ar.Count];
//			ar.CopyTo(res);
//			return res;
//		}
//		Demand CreateDmd(UOW uow)
//		{
//			Demand dmd = new Demand(uow, OrderType.New);
//			dmd.DmdType = "New";
//			dmd.Statement = 1;
//			dmd.Consumer = 4;
//			dmd.ConsumerAgent = 5;
//			dmd.Status = "WIP";
//			dmd.IsUnderWF = false;
//			dmd.Loc = 3746; // 75080
//			dmd.Ilec = 22; // SWB
//			
//			return dmd;
//		}
//	}
//}