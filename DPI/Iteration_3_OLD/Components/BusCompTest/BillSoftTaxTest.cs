using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class BillSoftTaxTests
	{
		public static void Main()
		{
			BillSoftTaxTests bst = new BillSoftTaxTests();
			bst.TaxTest();

			}
		[Test]
		public void TaxTest()
		{
			IdentityMap imap = new IdentityMap();
			UOW uow = new UOW();

			IDemand dmd = GenDmd(uow);
			IOrderSum sum = dmd.OrderSummary(uow);
			IDmdItem[] dis = sum.Items;
			
			decimal total = 0m;
			for (int i = 0; i < dis.Length; i++)
				for (int j = 0; j < dis[i].Taxes.Length; j++)
					total += dis[i].Taxes[j].TaxAmount;

			Console.Write("Total: {0}", total);
			
			Console.ReadLine();
		}

		IDemand GenDmd(UOW uow)
		{
			uow.Service = "BillSoft taxes test";

			Demand dmd = new Demand(uow, DemandType.New.ToString());
			dmd.DmdType = "New";
			dmd.Statement = 1;
			dmd.ConsId = 4;
			dmd.ConsumerAgent = "ConsumerAgent";
			dmd.Status = "WIP";
			dmd.IsUnderWF = false;
			dmd.Loc = 3746; // 75080
			dmd.Ilec = 22; // SWB
			

			
////			ProdPrice2[] billable = ProdPrice2.GetBillableForZip(uow, dmd.Loc, dmd.Ilec);
////			ProdPrice2[] billable = ProdPrice2.GetBillableForZip(uow, dmd.Loc, dmd.Ilec);
//			ProdPrice[] prods = ProdPrice.getAvaProdForZip(uow, dmd.Loc, dmd.Ilec);
//			IDemand demand = Demand.BuildDmd(uow, prods, "75080", ILECInfo.Find(uow, dmd.Ilec),  DemandType.New.ToString(), OrderType.New);
//			IDmdItem[]dis = demand.OrderSummary(uow).Items;
////			DmdItem[] dis = new DmdItem[billable.Length];
////
////			for (int i = 0; i <  dis.Length; i++)
////			{
////				dis[i] = new DmdItem(uow, billable[i], dmd);	
////				dis[i].BuildComps(uow);
////			}
////			dmd.AddDmdItems(dis);
			return dmd;
		}
	}
}