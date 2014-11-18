using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class DmdItemTests
	{
		/*		Data		*/ 
		int id;
		int dmdId = 4;
		int prod = 4;
		string priceRule = "priceRule";
		decimal priceAmt = 15.25m;
		DateTime startDate = DateTime.Now;
		DateTime endDate = DateTime.Now;
		string uOM = "uOM";
		int qT = 9;
		string status = "Active";

		decimal taxAmount = 0m;
        
		/*		Constructors		*/
		public DmdItemTests()
		{
			// try { cleanup(); } 	catch {}
			// Console.WriteLine("Cleanup completed");
		}
        
		/*		Methods		*/
		public static void Main()
		{
			DmdItemTests test = new DmdItemTests();
			//
			//		for (int i = 0; i < 5; i++)
			//			new DmdItemTests().addDIs();

			//			PayInfoTests pit = new PayInfoTests();
			//			pit.addPayInfo();
			//			Console.ReadLine();
			//		test.RemoveFromIMap();
			test.GetBillable();
			test.addDIs();
			return;
			// UOW Tests
			//       test.addDmdItem();
			test.findDmdItem();
			test.saveDmdItem();
			test.findAllDmdItems();
            
			try
			{
				test.delDmdItem();
			}
			catch(ArgumentException ae)
			{
				Console.WriteLine("Expected exception: delDmdItem:" + ae.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine("Error: delDmdItem: " + e.Message);
			}
		}
		[Test]
		public void RemoveFromIMap()
		{	
			IdentityMap imap = new IdentityMap();
			UOW uow = new UOW(imap);
			uow.Service = "addDemand And Items";

			Demand dmd = new Demand(uow, DemandType.New.ToString());
			dmd.DmdType = "New";
			dmd.Statement = 1;
			dmd.ConsId = 4;
			dmd.ConsumerAgent = "ConsumerAgent";
			dmd.Status = "WIP";
			dmd.IsUnderWF = false;
			dmd.Loc = 3746; // 75080
			dmd.Ilec = 22; // SWB
			

			IDmdItem[] dis = new DmdItem[0];

			try
			{
				ProdPrice2[] billable = ProdPrice2.GetBillableForZip(uow, dmd.Loc, dmd.Ilec);
				IDemand demand = Demand.BuildDmd(uow, billable, "75080", ILECInfo.Find(uow, dmd.Ilec),  DemandType.New.ToString(), OrderType.New);

				dis = demand.OrderSummary(uow).Items;
				//				for (int i = 0; i <  dis.Length; i++)
				//				{
				//					dis[i] = new DmdItem(uow, billable[i], dmd);	
				//					dis[i].BuildComps(uow);
				//				}
				//				dmd.AddDmdItems(dis);

				imap.ClearDomainObjs();

				int begin = imap.Count; 

				dmd.removeFromIMap(uow);
				imap.Compress();

				int end = imap.Count; 
  
			}
			catch (Exception e)
			{
				string s = e.Message;
			}
			finally 
			{
				uow.close();
			}

			for (int i = 0; i < dis.Length; i++)		
			{
				IDmdItem[] comps = dis[i].Components;
				if(comps.Length == 0)
					continue;
			
				decimal compTotal = decimal.Zero;
				for (int j = 0; j < comps.Length; j++)
					compTotal += comps[j].PriceAmt;

				Assertion.Assert("Components total = package price?", 
					compTotal == dis[i].PriceAmt);
			}

			try
			{
				for (int i = 0; i < dis.Length; i++)		
				{
					//		Console.WriteLine(string.Empty);
					//		ShowDmdItem(dis[i]);
					//		ShowFees(dis[i]);	
					ShowTaxes(dis[i], "  ");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			Assertion.Assert(dis[0].Id == dmd.Id);

		}
		[Test]
		public void addDIs()
		{
			IMap imap = new IdentityMap();
			IOrderSum sum = GetOrderSum(imap);
			
			//IProdTax[] sumtax = sum.GetTaxSummary(1);
			ShowMonthsArray(sum);
			


			ShowTaxSumry(sum);
			IDmdItem[] dis = sum.Items;
			ShowOrderSum(sum);

			try
			{
				for (int i = 0; i < dis.Length; i++)		
				{
					//					Console.WriteLine(string.Empty);
					//					ShowDmdItem(dis[i]);
					//					ShowFees(dis[i]);	 
					ShowTaxes(dis[i], "");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			Console.WriteLine("****    Total prod taxes = {0}   *****", taxAmount.ToString("c")  );
			Console.ReadLine();
		}
		IOrderSum GetOrderSum(IMap imap)
		{
			UOW uow = new UOW(imap);
			uow.Service = "addDemand And Items";

			Demand dmd = new Demand(uow,  DemandType.New.ToString());
			dmd.DmdType = "New";
			dmd.Statement = 1;
			dmd.ConsId = 4;
			dmd.ConsumerAgent = "ConsumerAgent";
			dmd.Status = "WIP";
			dmd.IsUnderWF = false;
			//		dmd.Loc = 3746; // 75080
			dmd.Loc = 1764; // 90014
			string zip = "90014";

			// dmd.Ilec = 22; // SWB
			dmd.Ilec = 16; // pac bell

			IDemand demand;
			try
			{
//				ProdPrice[] billable = ProdQual.RemoveDuplicates(AvailProds.GetAvailProds(uow, dmd.Ilec, zip));
//				CheckDupsDmdItems(imap);
//				
//				ArrayList ar = new ArrayList();
//				for (int i = 0 ; i < billable.Length; i++)
//					if (IsIn(billable[i].ProdId))
//					{
//						billable[i].ProdSelState = ProdSelectionState.Selected;
//						ar.Add(billable[i]);
//
//					}
//				ProdPrice[] test = new ProdPrice[ar.Count];
//				ar.CopyTo(test);
//
//				CheckDupsDmdItems(imap);
//				demand = Demand.BuildDmd(uow, test, zip, ILECInfo.Find(uow, dmd.Ilec),  DemandType.New.ToString(), OrderType.New);
//				return demand.OrderSummary(uow);
				//			
				return null;
			}
			finally 
			{
				uow.close();
			}
		}
		void ShowMonthsArray(IOrderSum sum)
		{
			int months = 9;
			string[][] matrix = sum.GetMonthlySummary(months);
			Console.WriteLine(" ***********  Tax matrix ***********");

			for (int i = 0; i < matrix.Length; i++)
			{
				StringBuilder sb = new StringBuilder();
				for (int j = 0; j < matrix[i].Length; j++)
					sb.Append(matrix[i][j] + "    ");
				
				Console.WriteLine(sb.ToString());
			}
		}

		void ShowTaxSumry(IOrderSum sum)
		{
			string[] lines = sum.GetSumTaxDesc(1);
			for (int i = 0; i < lines.Length; i++)
				Console.WriteLine(lines[i]);

		}

		void ShowOrderSum(IOrderSum sum)
		{
			ShowMonth(sum, 1);
			ShowMonth(sum, 2);
		}
		void ShowMonth(IOrderSum sum, int month)
		{
			Console.WriteLine("Month {0}:", month.ToString());
			
			decimal subTotal = sum.GetProdSubTotal(month);         // Product + fees
			Console.WriteLine("   ProdSubTotal: {0}", subTotal);
			
			decimal tax = sum.GetTaxAmt(month);                    // taxes
			Console.WriteLine("   Tax amount:   {0}", tax); 
			
			decimal total = sum.GetTotalAmtDue(month);			   // Products + fees + taxes
			Console.WriteLine("   TotalAmtDue:  {0}", total); 
		}				

		bool IsIn(int prod)
		{
			int[] prods =  { 414, 450, 253, 340}; //,412 / 460};
			//			int[] prods =  { 253 };
			
			for (int i = 0; i < prods.Length; i++)
				if (prods[i] == prod)
					return true;
			return false;
		}
		void ShowTaxes(IDmdItem di, string indent)
		{
			if ( indent == "")
				if (taxAmount != decimal.Zero)
				{
					Console.WriteLine("   Total product taxes = {0}   ", taxAmount.ToString("c")  );
					Console.WriteLine("");
					taxAmount = 0m;
				}

			if (!ProdInfoCol.GetProd(di.Prod).IsBillable)
			{
				Console.WriteLine("Prod: {0} is not billable", di.Prod.ToString());	
				return;
			}
			string taxCode = "NONE";
			if (ProdInfoCol.GetProd(di.Prod).TaxCode != null)
				if (ProdInfoCol.GetProd(di.Prod).TaxCode.Trim().Length > 0)
					taxCode = ProdInfoCol.GetProd(di.Prod).TaxCode;
 
			Console.WriteLine(indent + "Prod: {0} Taxcode: {1}  {2} {3}",
				di.Prod.ToString(), taxCode, ProdInfoCol.GetProd(di.Prod).ProdName, (di.PriceAmt * di.PackDiscount).ToString("c"));

			Console.WriteLine(indent + "   Taxes:",
				di.Prod.ToString(), taxCode, ProdInfoCol.GetProd(di.Prod).ProdName);

			decimal sub = 0m;
			for (int i = 0; i < di.Taxes.Length; i++)
			{
				//				Console.WriteLine(indent + "    prod {2} tax  {0} amount {1} Start {3} Ends {4}", 
				//					di.Taxes[i].TaxType,
				//					di.Taxes[i].TaxAmt.ToString(),
				//					di.Prod, ProdInfoCol.GetProd(di.Prod).StartServMon,
				//					ProdInfoCol.GetProd(di.Prod).EndServMon);
					
					
				Console.WriteLine(indent + "        {0} {1}  {2}", 	
					di.Taxes[i].DmdItm,
					TaxDescription.TaxTypeToString(di.Taxes[i].DmdItm.ToString()),
					di.Taxes[i].TaxAmount.ToString("c")
					);
				
				sub += di.Taxes[i].TaxAmount;
				taxAmount += di.Taxes[i].TaxAmount;
			}
			//	if (sub != 0m)
			//		{
			Console.WriteLine(indent + "  --- Tax subtotal {0}", sub.ToString("c")); 
			sub = 0m;

			//		}
			if (indent != "")
				return;

			if (di.TagAlongs.Length > 0)
			{
				Console.WriteLine("   Tag-alongs taxes");
				for (int j = 0; j < di.TagAlongs.Length; j++)
					ShowTaxes(di.TagAlongs[j], indent + "      ");
			}
			
			if (di.Components.Length > 0)
			{
				Console.WriteLine("   Components taxes");
				for (int j = 0; j < di.Components.Length; j++)
					ShowTaxes(di.Components[j], indent + "      ");
			}
		} 
		
		void ShowFees(IDmdItem di)
		{
			for (int j = 0; j < di.TagAlongs.Length; j++)
				Console.WriteLine("  package {2}, fee: {0}, price={1} {3}", 
					di.TagAlongs[j].Prod, 
					di.TagAlongs[j].PriceRule, 
					di.TagAlongs[j].PackageId, 
					di.TagAlongs[j].PriceAmt);
		}

		void ShowDmdItem(IDmdItem di)
		{
			Console.WriteLine("{0}, price={1} {2}", di.Prod, di.PriceRule, di.PriceAmt);
			
			for (int j = 0; j < di.Components.Length; j++)
				Console.WriteLine("  package {2}, sub: {0}, price={1} {3}", 
					di.Components[j].Prod, 
					di.Components[j].PriceRule, 
					di.Components[j].PackageId,
					di.Components[j].PriceAmt);
		}

		[Test]
		public void addDmdItem()
		{
			UOW uow = new UOW();
			uow.Service = "addDmdItem";
			DmdItem cls = new DmdItem(uow);
            
			//    cls.DmdItemType = this.dmdItemType;
			cls.ParDemand  = Demand.find(uow, this.dmdId);
			cls.Prod = this.prod;
			cls.PriceRule = this.priceRule;
			cls.PriceAmt = this.priceAmt;
			cls.StartDate = this.startDate;
			cls.EndDate = this.endDate;
			cls.UOM = this.uOM;
			cls.QT = this.qT;
			cls.Status = this.status;
		
        
			uow.commit();
			this.id = cls.Id;
            
			uow = new UOW();
			uow.Service = "addDmdItem - assert";
			cls = DmdItem.find(uow, this.id);

			Assertion.Assert(cls.DmdId == this.dmdId);
			Assertion.Assert(cls.PriceAmt == this.priceAmt);
			Assertion.Assert(cls.PriceRule == this.priceRule);


			uow.close();
		}

		[Test]
		public void findDmdItem()
		{
			UOW uow = new UOW();
			uow.Service = "findDmdItem";
            
			DmdItem cls = DmdItem.find(uow, this.id);
			Assertion.Assert(cls.Id == this.id);
			uow.close();
		}

		[Test]
		public void saveDmdItem()
		{
			UOW uow = new UOW();
			uow.Service = "saveDmdItem";
			DmdItem cls = DmdItem.find(uow, this.id);
            
			//   cls.DmdItemType = this.dmdItemType;
			cls.ParDemand  = Demand.find(uow, this.dmdId);

			this.priceRule = "PriceRule 2";
			cls.PriceAmt = ++priceAmt;
			cls.PriceRule = this.priceRule;
			cls.StartDate = this.startDate;
			cls.EndDate = this.endDate;
			cls.UOM = this.uOM;
			cls.QT = this.qT;

			this.status = "Off";
			cls.Status = this.status;
			uow.commit();
            
			uow = new UOW();
			uow.Service = "saveDmdItem - assert";
            
			cls = DmdItem.find(uow, this.id);

			Assertion.Assert(cls.Status == this.status);
			Assertion.Assert(cls.PriceRule == this.priceRule);
			Assertion.Assert(cls.PriceAmt == this.priceAmt);
            
			uow.close();
		}

		[Test]
		public void findAllDmdItems()
		{
			UOW uow = new UOW();
			uow.Service = "findAllDmdItems";
			DmdItem[] objs = DmdItem.getAll(uow);
			Assertion.Assert(objs.Length > 0);
			uow.close();
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void delDmdItem()
		{
			UOW uow = new UOW();
			uow.Service = "delDmdItem";
			DmdItem cls = DmdItem.find(uow, this.id);
			cls.delete();
            
			uow.commit();
            
			uow = new UOW();
			uow.Service = "delDmdItem - assert";
			cls = DmdItem.find(uow, this.id);
			Assertion.Assert((cls.Id == 0));
			uow.close();
		}

		[Test]
		public void GetBillable()
		{
			IMap imap =  new IdentityMap();
			IOrderSum sum = GetOrderSum(imap);
			CheckDupsDmdItems(imap);
			int dups = 0;
			Hashtable htable = new Hashtable(25);
			for (int i = 0; i < sum.Items.Length; i++)
			{
				if (htable.ContainsKey(sum.Items[i].Prod))
				{
					Console.WriteLine("Duplicate product " + sum.Items[i].Prod.ToString());
					dups++;
					continue;
				}

				htable.Add(sum.Items[i].Prod, sum.Items[i]);
			}
			Assertion.Assert(dups == 0);
		}
		void CheckDupsDmdItems(IMap imap)
		{
			Hashtable htable = new Hashtable();

			IMapObj[] objs = imap.getObjets();
			for (int i = 0; i < objs.Length; i++)
			{
				if (!(objs[i] is IDmdItem))
					continue;

				if (htable.ContainsKey(((IDmdItem)objs[i]).Prod))
					throw new ApplicationException("Duplicate Demanded item " + ((IDmdItem)objs[i]).Prod.ToString());
				htable.Add(((IDmdItem)objs[i]).Prod, (IDmdItem)objs[i]);
			}
		}
	}
}