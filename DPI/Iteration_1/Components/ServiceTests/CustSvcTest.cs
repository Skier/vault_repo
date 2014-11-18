using System;
using System.Collections;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;
using DPI.ClientComp;
using DPI.Services;

namespace DPI.Services
{
	[TestFixture]
	public class CustSvcTest
	{
		IMap imap;
		IILECInfo ilec; // = new DPI.Components.ILECInfoTest(17, "INT", "Sprint", false);
		string zipcode = "75234"; //"89060";
		IProdPrice[] products;
		IProdPrice selectedBasicService;
		
		public CustSvcTest()
		{
		}

		public static void Main()
		{
			CustSvcTest test = new CustSvcTest(); 
			//test.getEmptyError();
			//test.getNewOrderSummary();
			test.GetOrderSum();
			//test.CancelPendPayment();
			//			test.FindCustByPhone();
			//			test.FindCustById();
		}

		[Test]
		public void GetOrderSum()
		{
			imap = new IdentityMap();
			int ilec = 22;
			IOrderSum sum = GetOrderSummary(imap);
			Demand dmd = (Demand)sum.Demand;
			
			//			IAddr mailAddr = MakeAddress(AddressType.Mailing);
			//			IAddr svcAddr = MakeAddress(AddressType.Service); 
			//			ICustInfo custInfo = MakeCustInfo();
			
			IPayInfo  pymt = MakePayInfo(dmd);
			imap.add((IMapObj)pymt);

//			IReceipt receipt = CustSvc.SubmitNewXact( 
//				imap,
//				dmd, 
//				ilec, 
//				"SCRW0322RW", 
//				"Al2",
//				"1234", 
//				pymt, 
//				"1234",
//				null);
//			

			Console.WriteLine("New Payment submitted");

			IAddr2 sa = (IAddr2)CustFactory.GetAddress(imap);
			sa.Zipcode = "75080";
			sa.State = "TX";
			sa.Street = "Main";
			sa.StreetNum = "5";
			sa.City = "Plano";

			IAddr2 ma = CustFactory.GetAddress(imap);
			ma.Zipcode = "75024";
			ma.State = "TX";
			ma.Street = "Main";
			ma.StreetNum = "5";
			ma.City = "Plano";
			
			CustInfo ci = (CustInfo)CustFactory.GetCustInfo(imap);
//			ci.AccNumber = receipt.AccNumber;
			ci.FirstName = "Jane";
			ci.LastName  = "Smith";
			ci.Contact = "1234568765";

			imap.add((IMapObj)sa);
			imap.add((IMapObj)ma);
			imap.add(ci);

			CustInfoExt icx = new CustInfoExt(ci, ma, sa);
			UOW uow = new UOW();
			ILECInfo lec = ILECInfo.Find(uow, ilec);
			uow.close();
		
			CustSvc.PreSave(imap);
			dmd.BillPayer = icx.CustInfo.CustInfoID;

//			IReceipt receipt2 = CustSvc.SubmitNewOrder2(
//				imap,
//				OrderType.New,
//				ma, 
//				sa, 
//				ci,
//				dmd,
//				sa.Zipcode,
//				lec.ILECCode,
//				"123",
//				pymt,			
//				receipt,
//				"YONIXSYSTEM");

			Console.WriteLine("New Order submitted");

		}
		
		void CancelPendPayment()
		{
			int paymtId = 84;
			CustSvc.CancelPendPayment(imap, paymtId);
		}
		IPayInfo  MakePayInfo(IDemand dmd)
		{
			UOW uow = new UOW();
			PayInfo pymt = null;
			pymt = PayInfo.GetPayInfo(uow, PayInfoClass.PayInfo.ToString());
			pymt.AmountTendered = 100m;
			pymt.ParDemand = dmd;
			pymt.PayDate = DateTime.Today;
			pymt.LocalAmountDue = 100m;
			pymt.LocalAmountPaid = 100m;
			pymt.LdAmount = 0m;
			pymt.PaymentType = PaymentType.Credit;
			pymt.ConfNumber = "1233";
			pymt.VFConf = "5432";
	
			return pymt;
		}
		IAddr MakeAddress(AddressType addrType)
		{
			CustAddress addr = new CustAddress();

			addr.AddrType = addrType;

			addr.City = "Dallas";
			addr.Zipcode = "75080";
			addr.Unit = "14";
			addr.StreetNum = "101";
			addr.Street = "Main";
			addr.State = "TX";
		
			return addr;

		}
		ICustInfo MakeCustInfo()
		{
			CustInfo custinfo = null;
			return custinfo;
		}
		IOrderSum GetOrderSummary(IMap imap)
		{
            throw new NotImplementedException();
//			ilec = new ILECInfoTest(22, "SWB", "Southwestern Bell", false);
//			products = ProdSvc.GetTopProd(imap, ilec, zipcode);
//			selectedBasicService = products[0];
//			products = ProdSvc.GetDependentProds(imap, selectedBasicService, ilec, zipcode);
//
//			return CustSvc.GetOrderSummary(imap, products, zipcode, ilec, DemandType.New.ToString(), OrderType.New);
		}
		void PrintOrderSummary(IOrderSum sumry)
		{
			for (int i = 0; i < sumry.Items.Length; i++)
			{ 
				IDmdItem di = sumry.Items[i];
				Console.WriteLine("DmdItem {0}, type {1}",i, di.DmdItemType);
				for (int j = 0; j < di.Components.Length; j++)
					Console.WriteLine("      Component {0}, type {1}", j, di.Components[j].DmdItemType);

				for (int j = 0; j < di.TagAlongs.Length; j++)
					Console.WriteLine("      TagAlongs {0}, type {1}", j, di.TagAlongs[j].DmdItemType);

				Console.WriteLine(" ");
			}
			CustSvc.PreSave(imap);
		}

		//			Console.WriteLine("------ Order Summary --------");
		//			string indent;
		//			for (int i = 0; i < sumry.Products.Length; i++)
		//			{
		//				indent = "";
		//				if (sumry.Products[i].PackageId > 0)
		//					indent = "    ";
		//				Console.WriteLine("{4}Prod {0} {1}, price {2} Sel state {3}",  sumry.Products[i].ProdId.ToString(), sumry.Products[i].ProdName, 
		//					sumry.Products[i].UnitPrice, sumry.Products[i].ProdSelState.ToString(), indent); 
		//			}
		//			decimal subTotal = 0m;
		//			for (int i = 0; i < sumry.Products.Length; i++)
		//				subTotal += sumry.Products[i].UnitPrice;
		//
		//			Assertion.Assert(sumry.TotalAmtDue == sumry.TaxAmt + subTotal);
		

		//		[Test]
		//		[ExpectedException(typeof(ArgumentException))]
		//		public void getEmptyError()
		//		{
		//			IOrderSummary2 summ = CustSvc.GetNewOrderSummary(new IdentityMap(), null, null, null, OrderType.New);
		//		}
		[Test]
		public void FindCustByPhone()
		{
			imap = new IdentityMap();
			IAcctInfo ai = CustSvc.GetAcctInfo(imap, "1111111111");
			Assertion.Assert(ai != null);
		}
		[Test]
		public void FindCustById()
		{
			imap = new IdentityMap();
			IAcctInfo ai = CustSvc.GetAcctInfo(imap, "1111111111");
			Assertion.Assert(ai != null);
		}
		//		[Test]
		//		public void getNewOrderSummary()
		//		{
		//			imap = new IdentityMap();
		//
		//			BuildDummyOrder(imap);
		//
		//			IOrderSummary2 sumry = CustSvc.GetNewOrderSummary(imap, products, zipcode, ilec.ILECCode, OrderType.New);
		//
		//			Assertion.Assert(sumry.Products.Length > 0);
		//
		//			Console.WriteLine("------ Order Summary --------");
		//			string indent;
		//			for (int i = 0; i < sumry.Products.Length; i++)
		//			{
		//				indent = "";
		//				if (sumry.Products[i].PackageId > 0)
		//					indent = "    ";
		//				Console.WriteLine("{4}Prod {0} {1}, price {2} Sel state {3}",  sumry.Products[i].ProdId.ToString(), sumry.Products[i].ProdName, 
		//					sumry.Products[i].UnitPrice, sumry.Products[i].ProdSelState.ToString(), indent); 
		//			}
		//			decimal subTotal = 0m;
		//			for (int i = 0; i < sumry.Products.Length; i++)
		//				subTotal += sumry.Products[i].UnitPrice;
		//
		//			Assertion.Assert(sumry.TotalAmtDue == sumry.TaxAmt + subTotal);
		//		}
		
		//		[Test]
		//		public void submitNewOrder()
		//		{
		//			imap = new IdentityMap();
		//			AcctNotes an = new AcctNotes("notes", "YONIXSYSTEM");
		//
		//			BuildDummyOrder(imap);
		//
		//			IOrderSummary2 sumry = CustSvc.GetNewOrderSummary(imap, products, zipcode, ilec.ILECCode, OrderType.New);
		//
		//			ServAddr addr = new ServAddr();
		//			addr.Zipcode = "75234";//"75080";
		//			addr.City = "dallas";
		//			addr.State = "TX";
		//			addr.Street = "2997 LBJ Freeway";
		//
		//			CustInfo ci = new CustInfo();			
		//			ci.LastName = "Teleconnect";
		//			ci.FirstName = "dPi";
		//			ci.Contact = "1234567890";
		//
		//			PayInfo pi = 
		//				new PayInfo();
		//			pi.PayDate = DateTime.Now;
		//			pi.LocalAmountDue  = sumry.TotalAmtDue;
		//			pi.LocalAmountPaid = sumry.TotalAmtDue;
		//			pi.LdAmount = 0m;
		//			pi.PaymentType = PaymentType.Credit;
		//
		//			
		//			UserAccount ua= new UserAccount();
		//			
		//			IUser user = new User();
		//			user.DisplayName    = ua.DisplayName;
		//			user.AcctType       = ua.AcctType;
		//			user.ClerkId        =  "Clerk";
		//			user.LoginStoreCode = "DPI1234567";
		//			user.HasCertificate = false;
		//
		//			IReceipt r = CustSvc.SubmitNewXact(
		//				new IdentityMap(), ilec.ILECCode, user, "123", pi, "80", an);
		//
		//			Console.WriteLine("****  Accnumber = {0:G}   ****", r.AccNumber);
		//
		//			r = CustSvc.SubmitNewOrder(
		//				new IdentityMap(), OrderType.New, addr, addr, ci, products, 
		//				zipcode, ilec.ILECCode, "123", pi, r);
		//			
		////			IReceipt r = CustSvc.SubmitNewOrder(imap, OrderType.New, prods, zipcode, 
		////				ilec.ILECCode, "DPI1234567", "CLERK", "123", pi, an);
		//			//	ReceiptTest.DumpReceipt(r);// commented out to compile - alex
		//		//	ReceiptTest.DumpReceipt2(r);// commented out to compile - alex
		//			Console.ReadLine();
		//		}


//		void BuildDummyOrder(IMap imap)
//		{
//			//		IILECInfo ilec = new ILECInfoTest(22, "SWB", "Southwestern Bell", false);
//			//		string zipcode = "75287";
//
//			products = ProdSvc.GetTopProd(imap, ilec, zipcode);
//			IEnumerator enumerator = products.GetEnumerator();
//			while (enumerator.MoveNext()) 
//			{
//				ProdPrice element = (ProdPrice)enumerator.Current;
//				if (element.BillText.StartsWith("412"))
//					//				if (element.BillText.StartsWith("414") || element.BillText.StartsWith("372"))
//				{
//					Console.WriteLine("Basic service selected: {0:G}", element.ProdId, element.BillText);
//					IProdPrice[] deps = ProdSvc.GetDependentProds(imap, element, ilec, zipcode);
//
//					IEnumerator en = deps.GetEnumerator();
//					while (en.MoveNext()) 
//					{
//						IProdPrice el = (ProdPrice)en.Current;
//						if (el.BillText.StartsWith("191") || el.ProdName.StartsWith("402"))
//						{
//							if (el.ProdSelState == ProdSelectionState.Available)
//								((ProdPrice)en.Current).ProdSelState = ProdSelectionState.Selected;
//							//							ProdSvc.AddProd(imap, deps, el);
//							Console.WriteLine("Addl prod selected: {0:G}", el.ProdName);
//						}	
//					}
//					break;
//				}
//			}
//		}
	}
	public class ILECInfoTest : IILECInfo
	{
		/*        Data        */
		int orgId;
		string ilecCode;
		string ilecName;
		bool isDefault;
		/*        Properties        */
	
		public int OrgId
		{
			get { return orgId; }
		}
		public string ILECCode
		{
			get { return ilecCode; }
		}
		public string ILECName 
		{
			get { return ilecName; }
		}
		public bool IsDefault
		{
			get { return isDefault; }
		}
		/*        Constructors			*/
		public ILECInfoTest(int orgId, string ilecCode, string ilecName, bool isDefault) 
		{
			this.orgId = orgId;
			this.ilecCode = ilecCode;
			this.ilecName = ilecName;
			this.isDefault = isDefault; 
		}
	}
}