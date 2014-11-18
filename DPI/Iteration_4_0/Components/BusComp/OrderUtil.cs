using System;
using System.Collections;
using DPI.Interfaces;
using DPI.Components;


namespace DPI.Components
{
	public class OrderUtil
	{
	#region Methods
		public static int CreateOrder(UOW uow, int accnumber, string user)
		{
			spCustomerCare_CreateCustOrderWrapper co = new spCustomerCare_CreateCustOrderWrapper(uow);
			co.AccNumber = accnumber;
			co.Userid = user;
			return co.CreateCustOrder();  // returns ordernumber
		}
		public static void CreateCustomerProducts2(UOW uow, IDemand dmd, CustData cd, int orderNum,  string transnum, OrderType otype)
		{
			int prodIdx, compIdx, feeIdx, mapIdx;
			int detailId;
			ProdComposition[] mc;

			ILECInfo ilec = ILECInfo.Find(uow, cd.Ilec);
			int supplierId = ilec.OrgId;

			IOrderSum sumry = dmd.OrderSummary(uow);
			for (prodIdx = 0 ; prodIdx < sumry.Items.Length; prodIdx++)
			{
				// for every product ordered, need a detail entry
				spCustomerCare_CreateCustOrderDetailWrapper od = new spCustomerCare_CreateCustOrderDetailWrapper(uow);
				od.DpiProdId = sumry.Items[prodIdx].Prod;
				od.OrderId = orderNum;
				od.Qty = 1;
				detailId = od.CreateOrderDetail();

				// Write record to customerproduct for ordered product
				spCustomerCare_CreateCustomerProductWrapper cp = new spCustomerCare_CreateCustomerProductWrapper(uow);
				cp.AccNumber = cd.AccNumber;
				cp.BillingStatus = "OFF";
				cp.IsFee = false;
				cp.IsOrderedProduct = true;
				cp.OrderDetailId = detailId;
				cp.OrderType = otype;//dmd.DmdType;
				cp.PriceRule = sumry.Items[prodIdx].PriceRule;
				cp.ProvProdId = sumry.Items[prodIdx].Prod;
				cp.ProvStatus = "NEW";  // probably should use enum here...
				cp.VerTransId = int.Parse(transnum);
				cp.CreateCustomerProduct(ilec.OrgId);
		
				DoLD_Purchases(uow, sumry.Items[prodIdx], cd.AccNumber); 

				// Now we need to attach any components.  
				// ASSUMPTION:  No packages within packages!!!
				IDmdItem[] comps = sumry.Items[prodIdx].Components;
				for (compIdx = 0; compIdx < comps.Length; compIdx++ )
				{
					// Write record to customerproduct
					cp = new spCustomerCare_CreateCustomerProductWrapper(uow);
					cp.AccNumber = cd.AccNumber;
					cp.BillingStatus = "OFF";
					cp.IsFee = false;
					cp.IsOrderedProduct = false;
					cp.OrderDetailId = detailId;
					cp.OrderType = otype;
					cp.PriceRule = comps[compIdx].PriceRule;
					cp.ProvProdId = comps[compIdx].Prod;
					cp.ProvStatus = "NEW";  // probably should use enum here...
					DoLD_Purchases(uow, comps[compIdx], cd.AccNumber); 

					//				cp.VerTransId = CustData.find(uow, accnumber)... ;  // TODO: Need transaction number? (For voids)
					cp.CreateCustomerProduct(ilec.OrgId);

					// Need to attach mapped product for each component
					mc = ProdComposition.getMapProd(uow, cp.ProvProdId);
					for (mapIdx=0; mapIdx < mc.Length; mapIdx++)
					{
						if (Product.find(uow, mc[mapIdx].SubProd).Supplier != supplierId)
							continue;
						// Write record to customerproduct
						cp = new spCustomerCare_CreateCustomerProductWrapper(uow);
						cp.AccNumber = cd.AccNumber;
						cp.BillingStatus = "OFF";
						cp.IsFee = false;
						cp.IsOrderedProduct = false;
						cp.OrderDetailId = detailId;
						cp.OrderType = otype;
						cp.ProvProdId = mc[mapIdx].SubProd;
						cp.ProvStatus = "NEW";  // probably should use enum here...
						cp.Supplier = 0;  // supplier in this case is the PRICE supplier.  Provisionable products aren't priced.
						cp.CreateCustomerProduct(ilec.OrgId);
					}
				}

				// Now we need to attach any mapped products
				mc = ProdComposition.getMapProd(uow, sumry.Items[prodIdx].Prod);
				for (mapIdx=0; mapIdx < mc.Length; mapIdx++ )
				{
					if (Product.find(uow, mc[mapIdx].SubProd).Supplier != supplierId)
						continue;
					// write record to customerproduct
					cp = new spCustomerCare_CreateCustomerProductWrapper(uow);
					cp.AccNumber = cd.AccNumber;
					cp.BillingStatus = "OFF";
					cp.IsFee = false;
					cp.IsOrderedProduct = false;
					cp.OrderDetailId = detailId;
					cp.OrderType = otype;
					cp.ProvProdId = mc[mapIdx].SubProd;
					cp.ProvStatus = "NEW";  // probably should use enum here...
					cp.CreateCustomerProduct(ilec.OrgId);
				}

				// Now we need to attach any fees
				//IProdPrice[] fees;
				//fees=summ.Prods[prodIdx].Fees;
				IDmdItem[] fees = sumry.Items[prodIdx].TagAlongs;
				for (feeIdx=0; feeIdx < fees.Length; feeIdx++)
				{
					//ProdPrice fee = ProdPrice.getPriceForProd(uow, fees[feeIdx].ProdId, Location.find(uow,cd.AdrZip).LocId, ILECInfo.Find(uow,cd.Ilec).OrgId);
					// write record to customerproduct
					cp = new spCustomerCare_CreateCustomerProductWrapper(uow);
					cp.AccNumber = cd.AccNumber;
					cp.BillingStatus = "OFF";
					cp.IsFee = true;
					cp.IsOrderedProduct = false;
					cp.OrderDetailId = detailId;
					cp.OrderType = otype;
					cp.ProvProdId = fees[feeIdx].Prod;
					cp.ProvStatus = "NEW";  // probably should use enum here...
					cp.PriceRule = fees[feeIdx].PriceRule;
					//					cp.CreateCustomerProduct(fee.ExclusiveSupplier);
					cp.CreateCustomerProduct(ilec.OrgId);
				}
			}
		}
		public static void DoLD_Purchases(UOW uow, IDmdItem di, int accNumber)
		{
			IProdType pt = ProdTypeCol.GetProdType(ProdInfoCol.GetProd(di.Prod).ProdType);

			if (pt.PrdType == ProdCategory.Internet.ToString())
				DoIntLDPurchase(uow, di, accNumber);

			if (pt.FulfillMethod == null)
				return;

			if (pt.FulfillMethod.Trim().ToLower() != Const.LD_FULLFILL_METHOD.Trim().ToLower())
				return;

			Price price = Price.getPriceRule(uow, di.PriceRule);
			if (price == null)
				throw new ApplicationException("No price found for Product " + di.Prod.ToString());

			LDPurchases ld = new LDPurchases(uow);
			ld.AccNumber = accNumber;
			ld.LD_Type = Const.LD_PROD_TYPE;
			ld.LD_Product = Const.LD_PROD_ID;
			ld.Amount = price.UnitQuantity;
			ld.Ordered = false;
			ld.Purchase_Date = DateTime.Now;
			ld.ProductId = di.Prod;
		}

		public static int AssignIlec(UOW uow, string userid, int AccNumber, string Ilec)
		{
			spOrder_AssignIlecWrapper ai = new spOrder_AssignIlecWrapper(uow);
			ai.AccNumber = AccNumber;
			ai.Userid = userid;
			ai.Ilec = Ilec;
			return ai.AssignIlec();
		}
		public static void DoIntLDPurchase(UOW uow, IDmdItem di, int accNumber)
		{
			//AOL_PINs apin = AOL_PINs.ReservePIN(uow, di.Prod);

			LDPurchases ld = new LDPurchases(uow);
			ld.AccNumber = accNumber;
			ld.Amount = di.EffPrice;
			ld.Purchase_Date = DateTime.Now;
			ld.Ordered = false;
			ld.LD_Type = "P"; //Stands for purchase; it doesn't matter for this method.
			ld.LD_Product = ProdCategory.Internet.ToString();			
			ld.ProductId = di.Prod;
		}

	#endregion

	#region Implementation
		static bool IsMonth1(IDmdItem pp)
		{
			const int month = 1;

			int startMon = (ProdInfoCol.GetProd(pp.Prod)).StartServMon;
			int endMon   = (ProdInfoCol.GetProd(pp.Prod)).EndServMon;

			if (endMon == 0)
				endMon = int.MaxValue;

			if (startMon > month)
				return false;

			if (endMon < month)
				return false;

			return true;
		}	
	#endregion
	#region Dan's stuff
		//		public static void UpdateAccountLog2(UOW uow, CustData cd, IDemand dmd, decimal startingBalance)
//		{
//			Account_PaymentLog.AddEntries(uow, cd, dmd, startingBalance);
////			decimal balance = startingBalance;
////			IOrderSum sumry = dmd.OrderSummary(uow);
////
////			for (int i = 0; i < sumry.Items.Length; i++)
////			{	
////				AddAcctPayLogDmdItem(uow, cd, sumry.Items[i], ref balance); // product
////
////				for (int j = 0; j < sumry.Items[i].TagAlongs.Length; j++ )   // TagAlongs
////					AddAcctPayLogDmdItem(uow, cd, sumry.Items[i].TagAlongs[j], ref balance);
////			}
//		}
//		static void AddAcctPayLogDmdItem(UOW uow, CustData cd, IDmdItem dmdItem, ref decimal balance)
//		{
//			if (!IsMonth1(dmdItem))
//				return;
//
//			string prodName = (ProdInfoCol.GetProd(dmdItem.Prod)).ProdName;
//			decimal amount = decimal.Round(dmdItem.PriceAmt * dmdItem.PackDiscount, 2);
//
//			AddPaymtLog(uow, cd, prodName, amount, ref balance);					// Item
//			AddPaymtLog(uow, cd, "Tax:" + prodName, dmdItem.TaxAmt, ref balance);   // Item taxes
//		}

//		static void AddPaymtLog(UOW uow, CustData cd, string prodName, decimal amt, ref decimal balance)
//		{
//			if (amt == 0m)
//				return;
//
//			Account_PaymentLog pl = new Account_PaymentLog(uow);
//
//			pl.AccNumber   = cd.AccNumber;
//			pl.Amount      = -amt;
//			balance       += pl.Amount;
//			pl.Balance     = balance;
//			pl.Date        = DateTime.Now;
//			pl.Description = prodName;
//			
//			pl.add();
//		}	

//		static void AddPaymtLogTax(UOW uow, CustData cd, IProdInfo prod, decimal amt, ref decimal balance)
//		{
//			Account_PaymentLog pl = new Account_PaymentLog(uow);
//
//			pl.AccNumber   = cd.AccNumber;
//			pl.Amount      = -amt;
//			balance       += pl.Amount;
//			pl.Balance     = balance;
//			pl.Date        = DateTime.Now;
//			pl.Description = "Tax:" + prod.ProdName;
//			
//			pl.add();
//		}	
		/************** end *******************/
		//	create servicetransactions (taxes)
		//		public static void CreateServiceTransactions(UOW uow, int accnumber, OrderSummary summ)
		//		{
		////			Service_Transaction st;
		//			CustData cd = CustData.find(uow, accnumber);
		//	
		//			for (int i = 0; i < summ.Prods.Length; i++)
		//			{	
		//				OrderedProduct oProd = summ.orderedProducts[i];
		//
		//				if (oProd.components != null && oProd.components.Length > 0) // is this a package?
		//				{
		//					for (int j=0; j<oProd.components.Length; j++)
		//					{
		//						Component comp = oProd.components[j];
		//						
		//						//ProdTax[] tx = Tax.findTaxes(uow, cd.AdrZip, comp.priceInfo.TaxCode));
		//						if (comp.priceInfo.TaxCode != null)
		//							WriteSvcTransactions(uow, accnumber, comp.priceInfo.ProdId, 
		//								comp.priceInPackage, Int32.Parse(comp.priceInfo.TaxCode) );
		//					}
		//				}
		//				else
		//				{
		//					//ProdTax[] tx = Tax.findTaxes(uow, cd.AdrZip, oProd.prod.TaxCode);
		//					if (oProd.prod.TaxCode != null)
		//						WriteSvcTransactions(uow, accnumber, oProd.prod.ProdId, oProd.prod.UnitPrice, 
		//							Int32.Parse(oProd.prod.TaxCode));
		//				}
		//
		//				// must allow for taxing of fees.
		//				for (int j = 0; j < oProd.fees.Length; j++)
		//				{
		//					ProdPrice fee = oProd.fees[j];
		//
		//					if (fee.TaxCode!=null)
		//						WriteSvcTransactions(uow, accnumber, fee.ProdId, fee.PriceAmt, 
		//							Int32.Parse(fee.TaxCode));
		//
		//				}
		//			}
		//		}

//
//		public static void UpdateAccountLog(UOW uow, int accnumber, OrderSummary summ, decimal startingBalance)
//		{
//			Account_PaymentLog pl;
//			decimal workingBalance = startingBalance;
//
//			/* 
//			 * Right now we loop through the order summary, making one entry
//			 * in the log for each item ordered.  The amount is the total of the 
//			 * product's price, its fees, and its taxes.
//			 * It can be easily changed to make one entry for each line item's 
//			 * price, another for the fees, and another for the taxes.  
//			 * With a little additional work, an entry could be made at whatever 
//			 * level of granularity makes the best business sense.  -DP
//			*/
//			for (int i = 0; i < summ.Prods.Length; i++)
//			{	
//				// write records for:
//				// ordered product
//				// total taxes on product
//				// each fee w/total taxes for fee
//
//				OrderedProduct oProd = summ.orderedProducts[i];
//
//				if (oProd.prod.StartServMon > 1)
//					continue;
//
//				// record for ordered product
//				pl = new Account_PaymentLog(uow);
//				pl.AccNumber = accnumber;
//				pl.Amount = -oProd.prod.PriceAmt; //.TotalAmt;
//				workingBalance = workingBalance + pl.Amount;
//				pl.Balance = workingBalance;
//				pl.Date = DateTime.Now;
//				pl.Description = oProd.Prod.ProdName.ToString();
//				pl.add();
//
//				// record for taxes on ordered product
//				pl = new Account_PaymentLog(uow);
//				pl.AccNumber = accnumber;
//				pl.Amount = -oProd.taxesAmt;
//				workingBalance = workingBalance + pl.Amount;
//				pl.Balance = workingBalance;
//				pl.Date = DateTime.Now;
//				pl.Description = "Tax:" + oProd.Prod.ProdName.ToString();
//				pl.add();
//
//				// now for each fee...
//				for (int j = 0; j < oProd.fees.Length; j++ )
//				{
//					ProdPrice fee = oProd.fees[j];
//
//					if (fee.StartServMon > 1)
//						continue;
//
//					// record for fee
//					pl = new Account_PaymentLog(uow);
//					pl.AccNumber = accnumber;
//					pl.Amount = -fee.PriceAmt; //.TotalAmt;
//					workingBalance = workingBalance + pl.Amount;
//					pl.Balance = workingBalance;
//					pl.Date = DateTime.Now;
//					pl.Description = fee.ProdName.ToString();
//					pl.add();
//
//					/*  -- don't have taxes stored on each fee
//										// record for tax on fee
//										pl = new Account_PaymentLog(uow);
//										pl.AccNumber = accnumber;
//										pl.Amount = oProd.taxesAmt;
//										workingBalance = workingBalance - pl.Amount;
//										pl.Balance = workingBalance;
//										pl.Date = DateTime.Now;
//										pl.Description = "Tax:" + oProd.Prod.ProdName.ToString();
//										pl.add();
//					*/
//				}
//				// record for taxes on fees of ordered product, if any fees exist
//				if (oProd.fees.Length > 0)
//				{
//					/*
//										pl = new Account_PaymentLog(uow);
//										pl.AccNumber = accnumber;
//										pl.Amount = -oProd.taxesOnFeesAmt;
//										workingBalance = workingBalance + pl.Amount;
//										pl.Balance = workingBalance;
//										pl.Date = DateTime.Now;
//										pl.Description = "Taxes on Fees for " + oProd.Prod.ProdName.ToString();
//										pl.add();
//					*/
//					decimal runningTotal=0;
//					for (int k=0; k<oProd.taxesOnFees.Length; k++)
//					{
//						ProdTax pt = oProd.taxesOnFees[k];
//
//						if (pt.TaxedProdId == 0)
//							continue;
//							
//						if (k < oProd.taxesOnFees.Length-1 &&
//							oProd.taxesOnFees[k].TaxedProdId == oProd.taxesOnFees[k+1].TaxedProdId)
//						{
//							runningTotal += pt.TaxAmt;
//						}
//						else
//						{
//							runningTotal += pt.TaxAmt;
//
//							pl = new Account_PaymentLog(uow);
//							pl.AccNumber = accnumber;
//							pl.Amount = -runningTotal;
//							workingBalance = workingBalance + pl.Amount;
//							pl.Balance = workingBalance;
//							pl.Date = DateTime.Now;
//							Product taxedFee=Product.find(uow, pt.TaxedProdId);
//							pl.Description = "Tax:" + taxedFee.ProdName;
//							pl.add();
//
//							runningTotal = 0;
//						}
//					}
//				}
//			}
//		}		//  create customerproduct
//		public static void CreateCustomerProducts(UOW uow, int accnumber, int orderNum, OrderSummary summ, string transnum)
//		{
//			int prodIdx, compIdx, feeIdx, mapIdx;
//			int detailId;
//			ProdComposition[] pc, mc;
//
//			CustData cd = CustData.find(uow, accnumber);
//			ILECInfo ilec = ILECInfo.Find(uow, cd.Ilec);
//			int supplierId = ilec.OrgId;
//
//			for (prodIdx = 0 ; prodIdx < summ.orderedProducts.Length; prodIdx++)
//			{
//				ProdPrice orderedprod;
//
//				orderedprod=summ.orderedProducts[prodIdx].prod;  // ordered product
//				// for every product ordered, need a detail entry
//				spCustomerCare_CreateCustOrderDetailWrapper od = new spCustomerCare_CreateCustOrderDetailWrapper(uow);
//				od.DpiProdId = orderedprod.ProdId;
//				od.OrderId = orderNum;
//				od.Qty = 1;
//				detailId = od.CreateOrderDetail();
//
//				// Write record to customerproduct for ordered product
//				spCustomerCare_CreateCustomerProductWrapper cp = new spCustomerCare_CreateCustomerProductWrapper(uow);
//				cp.AccNumber = accnumber;
//				cp.BillingStatus = "OFF";
//				cp.IsFee = false;
//				cp.IsOrderedProduct = true;
//				cp.OrderDetailId = detailId;
//				cp.OrderType = summ.OrderType;
//				cp.PriceRule = orderedprod.priceRule;
//				cp.ProvProdId = orderedprod.ProdId;
//				cp.ProvStatus = "NEW";  // probably should use enum here...
//				cp.VerTransId = int.Parse(transnum);
//				cp.CreateCustomerProduct(ilec.OrgId);
//				
//				
//				pc = ProdComposition.getPackageComp(uow, orderedprod.ProdId, false);
//
//				// Now we need to attach any components.  
//				// ASSUMPTION:  No packages within packages!!!
//				for (compIdx=0; compIdx < pc.Length; compIdx++ )
//				{
//					ProdPrice comp = ProdPrice.getPriceForProd(uow, pc[compIdx].SubProd, Location.find(uow,cd.AdrZip).LocId, ILECInfo.Find(uow,cd.Ilec).OrgId);
//
//					// Write record to customerproduct
//					cp = new spCustomerCare_CreateCustomerProductWrapper(uow);
//					cp.AccNumber = accnumber;
//					cp.BillingStatus = "OFF";
//					cp.IsFee = false;
//					cp.IsOrderedProduct = false;
//					cp.OrderDetailId = detailId;
//					cp.OrderType = summ.OrderType;
//					cp.PriceRule = comp.priceRule;
//					cp.ProvProdId = comp.ProdId;
//					cp.ProvStatus = "NEW";  // probably should use enum here...
//
//					//				cp.VerTransId = CustData.find(uow, accnumber)... ;  // TODO: Need transaction number? (For voids)
//					cp.CreateCustomerProduct(ilec.OrgId);
//
//					// Need to attach mapped product for each component
//					mc = ProdComposition.getMapProd(uow, pc[compIdx].SubProd);
//					for (mapIdx=0; mapIdx < mc.Length; mapIdx++)
//					{
//						if (Product.find(uow, mc[mapIdx].SubProd).Supplier != supplierId)
//							continue;
//						// Write record to customerproduct
//						cp = new spCustomerCare_CreateCustomerProductWrapper(uow);
//						cp.AccNumber = accnumber;
//						cp.BillingStatus = "OFF";
//						cp.IsFee = false;
//						cp.IsOrderedProduct = false;
//						cp.OrderDetailId = detailId;
//						cp.OrderType = summ.OrderType;
//						cp.ProvProdId = mc[mapIdx].SubProd;
//						cp.ProvStatus = "NEW";  // probably should use enum here...
//						cp.Supplier = 0;  // supplier in this case is the PRICE supplier.  Provisionable products aren't priced.
//						cp.CreateCustomerProduct(ilec.OrgId);
//					}
//				}
//				// Now we need to attach any mapped products
//				mc = ProdComposition.getMapProd(uow,orderedprod.ProdId);
//				for (mapIdx=0; mapIdx < mc.Length; mapIdx++ )
//				{
//					if (Product.find(uow, mc[mapIdx].SubProd).Supplier != supplierId)
//						continue;
//					// write record to customerproduct
//					cp = new spCustomerCare_CreateCustomerProductWrapper(uow);
//					cp.AccNumber = accnumber;
//					cp.BillingStatus = "OFF";
//					cp.IsFee = false;
//					cp.IsOrderedProduct = false;
//					cp.OrderDetailId = detailId;
//					cp.OrderType = summ.OrderType;
//					cp.ProvProdId = mc[mapIdx].SubProd;
//					cp.ProvStatus = "NEW";  // probably should use enum here...
//					cp.CreateCustomerProduct(ilec.OrgId);
//				}
//
//				// Now we need to attach any fees
//				IProdPrice[] fees;
//				fees=summ.Prods[prodIdx].Fees;
//				for (feeIdx=0; feeIdx < fees.Length; feeIdx++)
//				{
//					ProdPrice fee = ProdPrice.getPriceForProd(uow, fees[feeIdx].ProdId, Location.find(uow,cd.AdrZip).LocId, ILECInfo.Find(uow,cd.Ilec).OrgId);
//					// write record to customerproduct
//					cp = new spCustomerCare_CreateCustomerProductWrapper(uow);
//					cp.AccNumber = accnumber;
//					cp.BillingStatus = "OFF";
//					cp.IsFee = true;
//					cp.IsOrderedProduct = false;
//					cp.OrderDetailId = detailId;
//					cp.OrderType = summ.OrderType;
//					cp.ProvProdId = fee.ProdId;
//					cp.ProvStatus = "NEW";  // probably should use enum here...
//					cp.PriceRule = fee.priceRule;
//					//					cp.CreateCustomerProduct(fee.ExclusiveSupplier);
//					cp.CreateCustomerProduct(ilec.OrgId);
//				}
//			}
//		}
#endregion
	}
}