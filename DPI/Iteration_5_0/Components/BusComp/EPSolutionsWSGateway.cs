using System;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Threading;

using DPI.Interfaces;
using DPI.Components.EPSolutions;
 
namespace DPI.Components
{
	public class EPSolutionsWSGateway  : EnergyProvider
	{
	#region Const
		string provider;
		const string crDuns = "785092292"; 
	#endregion		

	#region Properties
		public string Provider 
		{
			get { return provider;  }
			set { provider = value; }
		}
		static string UserName { get { return ConfigurationSettings.AppSettings["EPSolutionsWebSvcUserName"]; }}		
	#endregion

	#region Constructors
		public EPSolutionsWSGateway(string provider)
		{
			this.provider = provider;
		}
	#endregion

	#region Methods
		public override IReceipt Send(IUOW uow, IUser user, string action, object[] objects)
		{
			IWebSvcQueue logQue		= null;
			ICellPhoneInfo cellInfo	= null;
			IWireless_Products wp = null;

			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] is IWebSvcQueue)
				{
					logQue = (IWebSvcQueue)objects[i];
					logQue.WSProvider = provider;
				}

				if (objects[i] is IWireless_Products)
					wp = (IWireless_Products)objects[i];

				if (objects[i] is ICellPhoneInfo)
					cellInfo = (ICellPhoneInfo)objects[i];
			}
		
			throw new ArgumentException("EPSolutionsWSGateway: Uknown action '" + action + "'");
		}
		public override QuoteResponse GetQuote(IWebSvcQueue logQue, IQuoteReq q)
		{
			logQue.WSProvider = provider;
			logQue.Status = WebSvcQueueStatus.Completed.ToString(); //Pretty much no chance to fail after this.
			QuoteResponse  resp = new EPSolutionsProxy().GetQuote(crDuns, q.RatePlanName, q.DwellingType, q.UnitAge, q.BedroomCount, q.HeatingType, q.WindowUnits, q.AcUnits, q.HasPool, q.HasGarage, q.StartDate, q.MonthlyRent, q.SquareFootage, UserName);

			//new EnergyQuotation(
			return resp;
		}
		public override FindLocationResponse FindLocationByAddress(IAddressReq a)
		{
			return new EPSolutionsProxy().FindLocationByAddress(crDuns, a.Address1, a.Address2, a.City, a.State, a.Zip, a.Zip4, UserName);			
		}
		public override FindLocationResponse FindLocationBySiteCode(string siteCode)
		{
			return	new EPSolutionsProxy().FindLocationBySiteCode(crDuns, siteCode, UserName);
		}
		public override BalanceResponse GetAccountBalance(string acctNum)
		{
			return	new EPSolutionsProxy().GetAccountBalance(crDuns, acctNum, UserName);
		}
		public EnrollmentResponse EnrollPrepaidCustomer(IWebSvcQueue logQue, IEnergy_CustData c)
		{			
			logQue.InitialMsg = new EnrollPrepaidCustomerXml(crDuns, UserName, c).ToXmlDoc().InnerXml;					
			
			EnrollmentResponse resp = new EPSolutionsProxy().EnrollPrepaidCustomer(crDuns, c.SiteCode, c.QuoteId, UserName, c.Pin, c.Address1, c.Address2, c.City, c.State, c.Zip, c.Zip4, c.NameFirst, c.NameMiddle, c.NameLast, c.Ph1, c.Ph2, c.Email, c.Fax, ConvPCM(c.PreferedContactMethod), c.Ssn, c.DL, c.DlState, c.DOB, c.PermitName, c.CustomerNumberRef, c.DoingBusAs, ConvSNR(c.SpecialNeedsReq), ConvLIC(c.LowIncomeCustomer), ConvLan(c.Language));
			logQue.InitRespXml = new EnrollmentResponseXml(crDuns, UserName, resp).ToXmlDoc().InnerXml;

			if (resp != null && resp.Responses != null && resp.Responses.Length > 0 && resp.Responses[0].Code != "SP000")
				throw new ApplicationException("Enroll customer failed. Please try later.");
			
			return resp;
		}

		public PaymentResponse MakePayment(IWebSvcQueue logQue, IPaymentReq payReq)
		{
			logQue.Xml = new MakePaymentXml(crDuns, UserName, payReq).ToXmlDoc().InnerXml;	
			
			PaymentResponse resp = new EPSolutionsProxy().MakePayment(crDuns, payReq.AccountNumber, payReq.PaymentMethod, payReq.PaymentAmount, payReq.PaymentDate, payReq.IdentificationNumber,  payReq.InstitutionName, UserName);			
			logQue.LastRespXml = new PaymentResponseXml(crDuns, UserName, resp).ToXmlDoc().InnerXml;

			if (resp!= null && resp.Responses != null && resp.Responses.Length > 0 && resp.Responses[0].Code != "MP000")
				throw new ArgumentException("Payment failed. Please try to make payment later");

			return resp;
		}
		public override IEnergyRcpt MakePayment(IWebSvcQueue logQue, IPayInfo payInfo, string acctNumber)
		{
			
			IPaymentReq payReq = new PaymentReq(payInfo, acctNumber);

			PaymentResponse pr = MakePayment(logQue, payReq);
						
			IEnergyRcpt rcpt = new EnergyRcpt(new Random().Next(1, 9999999).ToString(), pr);			
			logQue.Status = WebSvcQueueStatus.Completed.ToString();
			
			return rcpt;
		}
		public PaymentResponse CancelPayment(IPaymentReq payReq)
		{
			PaymentResponse resp = new EPSolutionsProxy().CancelPayment(crDuns, payReq.AccountNumber, payReq.PaymentMethod, payReq.PaymentAmount, payReq.PaymentDate, UserName);
			return resp;
		}
		public override IEnergyRcpt RegisterCustomer(UOW uow, IWebSvcQueue logQue, IEnergy_CustData customer, IPaymentReq payReq)
		{
			PaymentResponse pr = null;
			
			EnrollmentResponse er = EnrollPrepaidCustomer(logQue, customer);			
			SetPayReqEnroll(uow, customer, er, payReq);		
			
			if (StoreStatsCol.GetCorporation(logQue.StoreCode).IsDpiEngInstantPay)
				pr = MakePayment(logQue, payReq);
						
			IEnergyRcpt rcpt = new EnergyRcpt(new Random().Next(1, 9999999).ToString(), er, pr);			
			logQue.Status = WebSvcQueueStatus.Completed.ToString();
			
			return rcpt;
		}
	#endregion

	#region Implementation

		private void SetPayReqEnroll(UOW uow, IEnergy_CustData customer, EnrollmentResponse er, IPaymentReq payReq)
		{
			EnergyEnrollment een = new EnergyEnrollment(uow, er);
			een.EngCustomer		 = customer; 
			customer.AccountNumber = int.Parse(er.Enrollment.AccountNumber);
			payReq.AccountNumber = er.Enrollment.AccountNumber;
			payReq.PaymentAmount = er.Enrollment.PrepaymentRequired;
			payReq.PaymentDate   = DateTime.Now;
		}
		static void DoEntry(string method, IWebSvcQueue entry)
		{
		}		
		private int ConvSNR(bool specialNeedsReq)
		{
			if (specialNeedsReq)
				return 1;

			return 0;
		}
		private int ConvLIC(bool lowIncomeCustomer)
		{
			if (lowIncomeCustomer)
				return 1;

			return 0;
		}
		private int ConvLan(string language)
		{
			if (language == null)
				return 1;

			if (language.Length == 0)
				return 1;

			if (language.Trim().ToLower() == "spanish")
				return 2;

			return 1;
		}
		private int ConvPCM(string preferedContactMethod)
		{
			if (preferedContactMethod == null)
				return 1;

			if (preferedContactMethod.Length == 0)
				return 1;

			if (preferedContactMethod.Trim().ToLower() == "secondaryphone")
				return 2;

			if (preferedContactMethod.Trim().ToLower() == "email")
				return 3;

			if (preferedContactMethod.Trim().ToLower() == "fax")
				return 4;

			return 1;
		}
	#endregion
	}
}