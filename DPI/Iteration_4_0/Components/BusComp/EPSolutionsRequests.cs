using System;

using DPI.Interfaces;

namespace DPI.Components.EPSolutions
{	
	public class QuoteReq : IQuoteReq
	{
		#region Data
		private string crDuns;
		private string ratePlanName;
		private int dwellingType;
		private int unitAge;
		private int bedroomCount;
		private int heatingType;
		private int windowUnits;
		private int acUnits;
		private int hasPool;
		private int hasGarage;
		private DateTime startDate;
		private decimal monthlyRent;
		private int squareFootage;
		private string userName;
		#endregion

		#region Properties
		public string CrDuns
		{
			get { return crDuns;  }
			set { crDuns = value; }
		}
		public string RatePlanName
		{
			get { return ratePlanName;  }
			set { ratePlanName = value; }
		}
		public int DwellingType
		{
			get { return dwellingType;  }
			set { dwellingType = value; }
		}
		public int UnitAge
		{
			get { return unitAge;  }
			set { unitAge = value; }
		}
		public int BedroomCount
		{
			get { return bedroomCount;  }
			set { bedroomCount = value; }
		}
		public int HeatingType
		{
			get { return heatingType;  }
			set { heatingType = value; }
		}
		public int WindowUnits
		{
			get { return windowUnits;  }
			set { windowUnits = value; }
		}
		public int AcUnits
		{
			get { return acUnits;  }
			set { acUnits = value; }
		}
		public int HasPool
		{
			get { return hasPool;  }
			set { hasPool = value; }
		}
		public int HasGarage
		{
			get { return hasGarage;  }
			set { hasGarage = value; }
		}
		public DateTime StartDate
		{
			get { return startDate;  }
			set { startDate = value; }
		}
		public decimal MonthlyRent
		{
			get { return monthlyRent;  }
			set { monthlyRent = value; }
		}
		public int SquareFootage
		{
			get { return squareFootage;  }
			set { squareFootage = value; }
		}
		public string UserName
		{
			get { return userName;  }
			set { userName = value; }
		}
		#endregion

		#region Constructors
		public QuoteReq(){}
		public QuoteReq(string crDuns, string userName)
		{
			this.crDuns = crDuns;
			this.userName = userName;
		}

		#endregion


	}

	public class AddressReq : IAddressReq
	{
		private string crDuns = "";
		private string address1 = "";
		private string  address2 = "";
		private string city = "";
		private string state = "";
		private string zip = "";
		private string zip4 = "";
		private string userName = "";

		public string CrDuns
		{
			get { return crDuns;  }
			set { crDuns = value; }
		}
		public string Address1
		{ 
			get { return address1;  }
			set { address1 = value; }
		}
		public string Address2
		{ 
			get { return address2;  }
			set { address2 = value; }
		}
		public string City
		{ 
			get { return city;  }
			set { city = value; }
		}
		public string State
		{ 
			get { return state;  }
			set { state = value; }
		}
		public string Zip
		{ 
			get { return zip;  }
			set { zip = value; }
		}
		public string Zip4
		{ 
			get { return zip4;  }
			set { zip4 = value; }
		}
		public string UserName
		{
			get { return userName;  }
			set { userName = value; }
		}
		public AddressReq(){}
		public AddressReq(string crDuns, string userName)
		{
			this.crDuns = crDuns;
			this.userName = userName;
		}
	}
	public class PaymentReq : IPaymentReq
	{
		#region Data
		string accountNumber;
		int paymentMethod;
		decimal paymentAmount;
		DateTime paymentDate;
		string identificationNumber;
		string institutionName;
		#endregion

		#region Properties
		public string AccountNumber			
		{ 
			get { return accountNumber;  } 
			set { accountNumber = value; }	
		}
		public int PaymentMethod
		{ 
			get { return paymentMethod;  }
			set { paymentMethod = value; }
		}
		public decimal PaymentAmount
		{ 
			get { return paymentAmount;  }
			set { paymentAmount = value; }
		}
		public DateTime PaymentDate
		{ 
			get { return paymentDate;  }
			set { paymentDate = value; }
		}
		public string IdentificationNumber
		{ 
			get { return identificationNumber;  }
			set { identificationNumber = value; }
		}
		public string InstitutionName
		{ 
			get { return institutionName;  }
			set { institutionName = value; }
		}
		#endregion

		#region Constractors
		public PaymentReq() {}
		public PaymentReq(string accountNumber)
		{
			this.accountNumber = accountNumber;
		}
		public PaymentReq(string accountNumber,	int paymentMethod, decimal paymentAmount) : this(accountNumber)
		{
			this.paymentMethod = paymentMethod;
			this.paymentAmount = paymentAmount;
		}
		public PaymentReq(IPayInfo payInfo, string accountNumber) : this(accountNumber)
		{
			this.paymentMethod = ConvertToEPPayMethod(payInfo.PaymentType);
			this.paymentAmount = payInfo.TotalAmountPaid;
			this.paymentDate   = payInfo.PayDate;
		}
	
		#endregion
		private int ConvertToEPPayMethod(PaymentType payType)
		{
			switch (payType)
			{
				
				case PaymentType.Cash :
					return 1;
				
				case PaymentType.Check :
					return 2;
				
				case PaymentType.Credit :
					return 3;				

				case PaymentType.Debit :
					return 4;

				case PaymentType.MoneyOrder:
					return 5;

				default :
					return 1;
			}

		}
	}
	public class EnrollPrepaidCustomerXml : XElement
	{
		public EnrollPrepaidCustomerXml(string CrDuns, string UserName, IEnergy_CustData customer)
		{
			name = "Request";
			SetupElmnts(CrDuns, UserName, customer);
		} 

		
		void SetupElmnts(string CrDuns, string UserName, IEnergy_CustData customer)
		{
			
			children = new XElement[33];
			int i = 0;

			children[i++] = new XElement("CrDuns",   CrDuns);
			children[i++] = new XElement("UserName", UserName);
			
			if (customer == null)
				return;

			children[i++] = new XElement("Address1",   customer.Address1);
			children[i++] = new XElement("Address2",   customer.Address2);
			children[i++] = new XElement("City",   customer.City);
			children[i++] = new XElement("CustomerNumberRef",   customer.CustomerNumberRef);
			children[i++] = new XElement("DateInserted",   customer.DateInserted.ToShortDateString());
			children[i++] = new XElement("DateModified",   customer.DateModified.ToShortDateString());
			children[i++] = new XElement("DL",   customer.DL);
			children[i++] = new XElement("DlState",   customer.DlState);
			children[i++] = new XElement("DOB",   customer.DOB.ToShortDateString());
			children[i++] = new XElement("DoingBusAs",   customer.DoingBusAs);
			children[i++] = new XElement("Email",   customer.Email);
			children[i++] = new XElement("Fax",   customer.Fax);
			children[i++] = new XElement("ID",   customer.ID.ToString());
			children[i++] = new XElement("Language",   customer.Language);
			children[i++] = new XElement("LowIncomeCustomer",   customer.LowIncomeCustomer.ToString());
			children[i++] = new XElement("NameFirst",   customer.NameFirst);
			children[i++] = new XElement("NameLast",   customer.NameLast);
			children[i++] = new XElement("NameMiddle",   customer.NameMiddle);
			children[i++] = new XElement("PermitName",   customer.PermitName);
			children[i++] = new XElement("Ph1",   customer.Ph1);
			children[i++] = new XElement("Ph2",   customer.Ph2);
			children[i++] = new XElement("Pin",   customer.Pin);
			children[i++] = new XElement("PreferedContactMethod",   customer.PreferedContactMethod);
			children[i++] = new XElement("QuoteId",   customer.QuoteId);
			children[i++] = new XElement("SiteCode",   customer.SiteCode);
			children[i++] = new XElement("SpecialNeedsReq",   customer.SpecialNeedsReq.ToString());
			children[i++] = new XElement("Ssn",   customer.Ssn);
			children[i++] = new XElement("State",   customer.State);
			children[i++] = new XElement("Status",   customer.Status);
			children[i++] = new XElement("Zip",   customer.Zip);
			children[i++] = new XElement("Zip4",   customer.Zip4);			
		}		
	}

	public class MakePaymentXml : XElement
	{
		public MakePaymentXml(string CrDuns, string UserName, IPaymentReq payReq)
		{
			name = "request";
			SetupElmnts(CrDuns, UserName, payReq);
		} 

		
		void SetupElmnts(string CrDuns, string UserName, IPaymentReq payReq)
		{
			children = new XElement[8];
			int i = 0;

			children[i++] = new XElement("CrDuns",   CrDuns);
			children[i++] = new XElement("UserName", UserName);
			
			if (payReq == null)
				return;

			children[i++] = new XElement("AccountNumber",   payReq.AccountNumber);
			children[i++] = new XElement("IdentificationNumber",   payReq.IdentificationNumber);
			children[i++] = new XElement("InstitutionName",   payReq.InstitutionName);
			children[i++] = new XElement("PaymentAmount",   payReq.PaymentAmount.ToString());
			children[i++] = new XElement("PaymentDate",   payReq.PaymentDate.ToShortDateString());
			children[i++] = new XElement("PaymentMethod",   payReq.PaymentMethod.ToString());
		}		
	}

	public class EnrollmentResponseXml : XElement
	{
		public EnrollmentResponseXml(string CrDuns, string UserName, EnrollmentResponse er)
		{
			name = "response";
			SetupElmnts(CrDuns, UserName, er);
		} 
		
		
		void SetupElmnts(string CrDuns, string UserName, EnrollmentResponse er)
		{
			if (er == null)
				return;
			
			if (er.Responses != null && er.Responses.Length > 0 && er.Responses[0].Code != "MP000")
			{
				AddErros(CrDuns, UserName, er.Responses[0]);
				return;
			}

			AddERChilds(CrDuns, UserName, er.Enrollment);
		}
			
		void AddERChilds(string CrDuns, string UserName, Enrollment enrm)
		{
			if (enrm == null)
				return;

			children = new XElement[8];
			int i = 0;

			children[i++] = new XElement("CrDuns",   CrDuns);
			children[i++] = new XElement("UserName", UserName);
			children[i++] = new XElement("AccountName",   enrm.AccountName);
			children[i++] = new XElement("AccountNumber",   enrm.AccountNumber);
			children[i++] = new XElement("AccountName",   enrm.PrepaymentRequired.ToString());
			children[i++] = new XElement("ServiceProviderName",   enrm.ServiceProviderName);
			children[i++] = new XElement("ServiceProviderPhone",   enrm.ServiceProviderPhone);
			children[i++] = new XElement("AccountName",   enrm.StartDate.ToString());
		}
		void AddErros(string CrDuns, string UserName, Response resp)
		{
			children = new XElement[4];
			int i = 0;
			
			children[i++] = new XElement("CrDuns",   CrDuns);
			children[i++] = new XElement("UserName", UserName);
			children[i++] = new XElement("Code", resp.Code);
			children[i++] = new XElement("Description", resp.Description);			
		}

	}

	public class PaymentResponseXml : XElement
	{
		public PaymentResponseXml(string CrDuns, string UserName, PaymentResponse pr)
		{
			name = "response";
			SetupElmnts(CrDuns, UserName, pr);
		}		
		void SetupElmnts(string CrDuns, string UserName, PaymentResponse pr)
		{
			if (pr == null)
				return;
			
			if (pr.Responses != null && pr.Responses[0].Code != "MP000")
			{
				AddErros(CrDuns, UserName, pr.Responses[0]);
				return;
			}
			AddPRChilds(CrDuns, UserName, pr.Balance);			
		}		

		void AddPRChilds(string CrDuns, string UserName, Balance blnce)
		{
			if (blnce == null)
				return;

			children = new XElement[5];
			int i = 0;
			
			children[i++] = new XElement("CrDuns",   CrDuns);
			children[i++] = new XElement("UserName", UserName);
			children[i++] = new XElement("AccountName",   blnce.AccountName);
			children[i++] = new XElement("AccountNumber",   blnce.AccountNumber);
			children[i++] = new XElement("AccountBalance",   blnce.AccountBalance.ToString("C"));
		}

		void AddErros(string CrDuns, string UserName, Response resp)
		{
			children = new XElement[4];
			int i = 0;
			
			children[i++] = new XElement("CrDuns",   CrDuns);
			children[i++] = new XElement("UserName", UserName);
			children[i++] = new XElement("Code", resp.Code);
			children[i++] = new XElement("Description", resp.Description);
		}
	}
}