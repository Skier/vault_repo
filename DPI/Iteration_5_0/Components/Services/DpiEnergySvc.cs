using System;
using System.Xml;
using System.Collections;
using System.Text;

using DPI.Components;
using DPI.Interfaces;
using DPI.Components.EPSolutions;


namespace DPI.Services
{
	public class DpiEnergySvc : ISvcProvider
	{
		#region Const & Properties

		const string provider =  "DpiEnergySvc";
		public string Provider { get { return provider; }}

		#endregion

		#region Direct Web Calls 
		public static FindLocationResponse FindLocationByAddress(string crDuns, string address1, string address2, string city, string state, string zip, string zip4, string userName)
		{
			return	new EPSolutionsProxy().FindLocationByAddress(crDuns, address1, address2, city, state, zip, zip4, userName);
		}

		public static FindLocationResponse FindLocationBySiteCode(string crDuns, string siteCode, string userName)
		{
			return	new EPSolutionsProxy().FindLocationBySiteCode(crDuns, siteCode, userName);
		}

		public static EnrollmentResponse EnrollPrepaidCustomer(string crDuns, string siteCode, string quoteId, string userName, string pin, string address1, string address2, string city, string state, string zip, string zip4, string fName, string mName, string lName, string ph1, string ph2, string email, string fax, int preferedContactMethod, string ssn, string dl, string dlState, DateTime dob, string permitName, string customerNumberRef, string doingBusAs, int specialNeedsReq, int lowIncomeCustomer, int language)
		{
			return	new EPSolutionsProxy().EnrollPrepaidCustomer(crDuns, siteCode, quoteId, userName, pin, address1, address2, city, state, zip, zip4, fName, mName, lName, ph1, ph2, email, fax, preferedContactMethod, ssn, dl, dlState, dob, permitName, customerNumberRef, doingBusAs, specialNeedsReq, lowIncomeCustomer, language);
		}

		public static BalanceResponse GetAccountBalance(string crDuns, string acctNum, string userName)
		{
			return new EPSolutionsProxy().GetAccountBalance(crDuns, acctNum, userName);
		}
		public static AdjustmentReasonResponse GetAdjustmentReasons(string crDuns, string userName)
		{
			return new EPSolutionsProxy().GetAdjustmentReasons(crDuns, userName);
		}
		
		public static QuoteResponse GetQuote(string crDuns, string ratePlanName, int dwellingType, int unitAge, int bedroomCount, int heatingType, int windowUnits, int acUnits, int hasPool, int hasGarage, DateTime startDate, decimal monthlyRent, int squareFootage, string userName)
		{
			return new EPSolutionsProxy().GetQuote(crDuns, ratePlanName, dwellingType, unitAge, bedroomCount, heatingType, windowUnits, acUnits, hasPool, hasGarage, startDate, monthlyRent, squareFootage, userName);
		}
		
		public static PaymentResponse CancelPayment(string crDuns, string accountNumber, int paymentMethod, System.Decimal paymentAmount, DateTime paymentDate, string userName)
		{
			return new EPSolutionsProxy().CancelPayment(crDuns, accountNumber, paymentMethod, paymentAmount, paymentDate, userName);
		}
		public static AdjustmentResponse MakeAdjustment(string crDuns, string accountNumber, int adjustmentType, decimal adjustmentAmount, DateTime adjustmentDate, int adjustmentReason, string userName)
		{
			return new EPSolutionsProxy().MakeAdjustment(crDuns, accountNumber, adjustmentType, adjustmentAmount, adjustmentDate, adjustmentReason, userName);
		}
		public static PaymentResponse MakePayment(string customerDuns, string accountNumber, int paymentMethod, System.Decimal paymentAmount, System.DateTime paymentDate, string identificationNumber, string institutionName, string userName)
		{
			return new EPSolutionsProxy().MakePayment(customerDuns, accountNumber, paymentMethod, paymentAmount, paymentDate, identificationNumber,  institutionName, userName);
		}
		public static PaymentMethodResponse GetPaymentMethods(string customerDuns, string userName)
		{
			return new EPSolutionsProxy().GetPaymentMethods(customerDuns, userName);
		}
		public static RatePlanResponse GetRatePlans(string customerDuns, string invoiceType, DateTime validFrom, DateTime validTo, string userName)
		{
			return new EPSolutionsProxy().GetRatePlans(customerDuns, invoiceType, validFrom, validTo, userName);
		}
//		public static RatePlanResponse MakeAdjustment(string customerDuns, string accountNumber, int adjustmentType, decimal adjustmentAmount, DateTime adjustmentDate, int adjustmentReason, string userName)
//		{
//			return new EPSolutionsProxy().MakeAdjustment(customerDuns, accountNumber, adjustmentType, adjustmentAmount, adjustmentDate, adjustmentReason, userName);
//		}
		#endregion

		#region Service Methods
		public static IWireless_Products[] GetDpiEnergyProducts(IMap imap, string storeCode, string provider, bool isNew)
		{
			UOW uow = null;
			try 
			{
				uow = new UOW(imap, "DpiEnergySvc.GetEnergyProduct");

				return Wireless_Products.GetDpiEnergyProducts(uow, storeCode, provider, isNew);
			}
			finally 
			{
				uow.close();				
			}
		}
		public static IEnergy_CustData GetEnergyCustData(IMap imap) 
		{
			UOW uow = null;
			try 
			{
				uow = new UOW(imap, "DpiEnergySvc.GetEnergyCustData");

				return new Energy_CustData(uow);
			}
			finally 
			{
				uow.close();				
			}
		}
		public static string GetDpiEnergyProvider(string storeCode)
		{
			UOW uow = null; 
			try
			{
				uow = new UOW("DpiEnergySvc.GetDpiEnergyVendor");

				return SelectEnergyProvider(Vendors.GetVendorsByStore(uow, storeCode));			
				
			}
			finally
			{
				uow.close();
			}			

		}
		public static IQuoteReq GetQouteReq()
		{
			return new QuoteReq();
		}
		public static IAddressReq GetAddressReq()
		{
			return new AddressReq();
		}
		public static IPaymentReq GetPaymentReq()
		{
			return new PaymentReq();
		}
		public static Balance GetAccountBalance(string provider, string acctNum)
		{
			return EnergyProviderFactory.GetGateway(provider).GetAccountBalance(acctNum).Balance;
		}
		public static FindLocationResponse FindLocationByAddress(IMap imap, string provider, IAddressReq address)
		{
			UOW uow = null; 
			try
			{
				uow = new UOW(imap, "DpiEnergySvc.FindLocationByAddress");				
				
				return EnergyProviderFactory.GetGateway(provider).FindLocationByAddress(address);
			}
			finally
			{
				uow.close();
			}			
		}
		
		public static FindLocationResponse FindLocationBySiteCode(string provider, string siteCode)
		{
			return EnergyProviderFactory.GetGateway(provider).FindLocationBySiteCode(siteCode);
		}
		public static IEnergy_CustData[] SearchCustomer(IMap imap, string phNumber, string lastName, string address, string city, string state, string zip, int accNumber)
		{
			UOW uow = null; 
			try
			{
				uow = new UOW(imap, "DpiEnergySvc.SearchCustomer");				
				
				return Energy_CustData.SearchCustomer(uow, phNumber, lastName, address, city, state, zip, accNumber);
			}
			finally
			{
				uow.close();				
			}			
		}
		public static QuoteResponse GetQuote(IMap imap, IUser user, IDemand dmd, string provider, IQuoteReq quote)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			try
			{
				uow = new UOW(imap, "DpiEnergySvc.GetQuote");				
				logQue = WebServSvc.SetupEntry(imap, user, dmd, "DpiEnergySvc.GetQuote");
				
				return EnergyProviderFactory.GetGateway(provider).GetQuote(logQue, quote);
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}			
			
		}
		public static EnrollmentResponse EnrollPrepaidCustomer(IMap imap, IUser user, IPayInfo pi, string provider, IEnergy_CustData customer)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			try
			{
				uow = new UOW(imap, "DpiEnergySvc.EnrollPrepaidCustomer");				
				logQue = WebServSvc.SetupEntry(imap, pi, user, "DpiEnergySvc.GetQuote");
				
				return EnergyProviderFactory.GetGateway(provider).EnrollPrepaidCustomer(customer);
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}			
			
		}	
		public static IEnergyRcpt RegisterCustomer(IMap imap, IUser user, IPayInfo pi, string provider, IEnergy_CustData customer, IPaymentReq payReq)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			try
			{
				uow = new UOW(imap, "DpiEnergySvc.RegisterCustomer");				
				logQue = WebServSvc.SetupEntry(imap, pi, user, "DpiEnergySvc.RegisterCustomer", provider);
				
				if (StoreStatsCol.GetCorporation(user.LoginStoreCode).IsDpiEngInstantPay)
				{
					CreateEnergyTran(uow, user, customer, pi);
					uow.commit();
				}
				
				return EnergyProviderFactory.GetGateway(provider).RegisterCustomer(uow, logQue, customer, payReq);
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}			
		}

		public static IEnergyRcpt MakePayment(IMap imap, IUser user, IPayInfo pi, string provider, IEnergy_CustData customer)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			try
			{
				uow = new UOW(imap, "DpiEnergySvc.MakePayment");				
				logQue = WebServSvc.SetupEntry(imap, pi, user, "DpiEnergySvc.MakePayment", provider);
				
				CreateEnergyTran(uow, user, customer, pi);
				uow.commit();
				
				return EnergyProviderFactory.GetGateway(provider).MakePayment(logQue, pi, customer.AccountNumber.ToString());
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}			
		}

		#endregion

		#region Misc 

		static IEnergy_Transactions CreateEnergyTran(UOW uow, IUser user, IEnergy_CustData customer, IPayInfo payInfo)
		{
			IEnergy_Transactions et = new Energy_Transactions(uow);
			et.ConfirmNum 				= payInfo.Id;
			et.PayDateTime				= DateTime.Now;
			et.Tran_Amount				= payInfo.TotalAmountPaid;
			et.StoreCode				= user.LoginStoreCode;
			et.Clerkid					= user.ClerkId;
			et.Pin						= customer.Pin;
			
			if (payInfo.IsConfReq) 
				et.Status				= PaymentStatus.PendWireless.ToString();
			else 
				et.Status				= PaymentStatus.Paid.ToString();
			
			et.AcctID 					= customer.ID;
			payInfo.Tran				= (IPayInfoTran)et;

			return et;
		}
		public void FireAway(string action, string xml)
		{
			//do nothing for now
		}
		private static string SelectEnergyProvider(IVendors[] vendors)
		{
			for (int i = 0; i < vendors.Length; i++)
				if ( vendors[i].ProdCategory == ProdCategory.DpiEnergy.ToString())
					return vendors[i].DefaultWSProvider;

			return "";				
		}
		#endregion
	}
}
			