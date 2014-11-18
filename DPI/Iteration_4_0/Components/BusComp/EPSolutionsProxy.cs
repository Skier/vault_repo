using System.Diagnostics;
using System.Xml.Serialization;
using System;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Services;


namespace DPI.Components.EPSolutions
{
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="https://empower-test.epway.com/ews")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(EmpowerWebServicesResponse))]
	public class EPSolutionsProxy : System.Web.Services.Protocols.SoapHttpClientProtocol 
	{
    
		/// <remarks/>
		public EPSolutionsProxy() 
		{
			string urlSetting = System.Configuration.ConfigurationSettings.AppSettings["EPSolutionsWebSvcURL"];
			if ((urlSetting != null)) 
			{
				this.Url = urlSetting;
			}
			else 
			{
				this.Url = "https://empower-test.epway.com/ews/service.asmx";
			}
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/GetAccountBalance", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public BalanceResponse GetAccountBalance(string crDuns, string accountNumber, string userName) 
		{
			object[] results = this.Invoke("GetAccountBalance", new object[] {
																				 crDuns,
																				 accountNumber,
																				 userName});
			return ((BalanceResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginGetAccountBalance(string crDuns, string accountNumber, string userName, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("GetAccountBalance", new object[] {
																		  crDuns,
																		  accountNumber,
																		  userName}, callback, asyncState);
		}
    
		/// <remarks/>
		public BalanceResponse EndGetAccountBalance(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((BalanceResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/GetFoundOutCompanyBy", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public FoundOutByResponse GetFoundOutCompanyBy(string customerDuns, string userName) 
		{
			object[] results = this.Invoke("GetFoundOutCompanyBy", new object[] {
																					customerDuns,
																					userName});
			return ((FoundOutByResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginGetFoundOutCompanyBy(string customerDuns, string userName, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("GetFoundOutCompanyBy", new object[] {
																			 customerDuns,
																			 userName}, callback, asyncState);
		}
    
		/// <remarks/>
		public FoundOutByResponse EndGetFoundOutCompanyBy(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((FoundOutByResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/GetPaymentMethods", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public PaymentMethodResponse GetPaymentMethods(string customerDuns, string userName) 
		{
			object[] results = this.Invoke("GetPaymentMethods", new object[] {
																				 customerDuns,
																				 userName});
			return ((PaymentMethodResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginGetPaymentMethods(string customerDuns, string userName, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("GetPaymentMethods", new object[] {
																		  customerDuns,
																		  userName}, callback, asyncState);
		}
    
		/// <remarks/>
		public PaymentMethodResponse EndGetPaymentMethods(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((PaymentMethodResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/GetAdjustmentReasons", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public AdjustmentReasonResponse GetAdjustmentReasons(string customerDuns, string username) 
		{
			object[] results = this.Invoke("GetAdjustmentReasons", new object[] {
																					customerDuns,
																					username});
			return ((AdjustmentReasonResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginGetAdjustmentReasons(string customerDuns, string username, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("GetAdjustmentReasons", new object[] {
																			 customerDuns,
																			 username}, callback, asyncState);
		}
    
		/// <remarks/>
		public AdjustmentReasonResponse EndGetAdjustmentReasons(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((AdjustmentReasonResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/GetRatePlans", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public RatePlanResponse GetRatePlans(string customerDuns, string invoiceType, System.DateTime validFrom, System.DateTime validTo, string userName) 
		{
			object[] results = this.Invoke("GetRatePlans", new object[] {
																			customerDuns,
																			invoiceType,
																			validFrom,
																			validTo,
																			userName});
			return ((RatePlanResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginGetRatePlans(string customerDuns, string invoiceType, System.DateTime validFrom, System.DateTime validTo, string userName, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("GetRatePlans", new object[] {
																	 customerDuns,
																	 invoiceType,
																	 validFrom,
																	 validTo,
																	 userName}, callback, asyncState);
		}
    
		/// <remarks/>
		public RatePlanResponse EndGetRatePlans(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((RatePlanResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/MakePayment", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public PaymentResponse MakePayment(string crDuns, string accountNumber, int paymentMethod, System.Decimal paymentAmount, System.DateTime paymentDate, string identificationNumber, string institutionName, string userName) 
		{
			object[] results = this.Invoke("MakePayment", new object[] {
																		   crDuns,
																		   accountNumber,
																		   paymentMethod,
																		   paymentAmount,
																		   paymentDate,
																		   identificationNumber,
																		   institutionName,
																		   userName});
			return ((PaymentResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginMakePayment(string crDuns, string accountNumber, int paymentMethod, System.Decimal paymentAmount, System.DateTime paymentDate, string identificationNumber, string institutionName, string userName, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("MakePayment", new object[] {
																	crDuns,
																	accountNumber,
																	paymentMethod,
																	paymentAmount,
																	paymentDate,
																	identificationNumber,
																	institutionName,
																	userName}, callback, asyncState);
		}
    
		/// <remarks/>
		public PaymentResponse EndMakePayment(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((PaymentResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/CancelPayment", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public PaymentResponse CancelPayment(string crDuns, string accountNumber, int paymentMethod, System.Decimal paymentAmount, System.DateTime paymentDate, string userName) 
		{
			object[] results = this.Invoke("CancelPayment", new object[] {
																			 crDuns,
																			 accountNumber,
																			 paymentMethod,
																			 paymentAmount,
																			 paymentDate,
																			 userName});
			return ((PaymentResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginCancelPayment(string crDuns, string accountNumber, int paymentMethod, System.Decimal paymentAmount, System.DateTime paymentDate, string userName, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("CancelPayment", new object[] {
																	  crDuns,
																	  accountNumber,
																	  paymentMethod,
																	  paymentAmount,
																	  paymentDate,
																	  userName}, callback, asyncState);
		}
    
		/// <remarks/>
		public PaymentResponse EndCancelPayment(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((PaymentResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/MakeAdjustment", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public AdjustmentResponse MakeAdjustment(string crDuns, string accountNumber, int adjustmentType, System.Decimal adjustmentAmount, System.DateTime adjustmentDate, int adjustmentReason, string userName) 
		{
			object[] results = this.Invoke("MakeAdjustment", new object[] {
																			  crDuns,
																			  accountNumber,
																			  adjustmentType,
																			  adjustmentAmount,
																			  adjustmentDate,
																			  adjustmentReason,
																			  userName});
			return ((AdjustmentResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginMakeAdjustment(string crDuns, string accountNumber, int adjustmentType, System.Decimal adjustmentAmount, System.DateTime adjustmentDate, int adjustmentReason, string userName, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("MakeAdjustment", new object[] {
																	   crDuns,
																	   accountNumber,
																	   adjustmentType,
																	   adjustmentAmount,
																	   adjustmentDate,
																	   adjustmentReason,
																	   userName}, callback, asyncState);
		}
    
		/// <remarks/>
		public AdjustmentResponse EndMakeAdjustment(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((AdjustmentResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/FindLocationBySiteCode", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public FindLocationResponse FindLocationBySiteCode(string crDuns, string siteCode, string userName) 
		{
			object[] results = this.Invoke("FindLocationBySiteCode", new object[] {
																					  crDuns,
																					  siteCode,
																					  userName});
			return ((FindLocationResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginFindLocationBySiteCode(string crDuns, string siteCode, string userName, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("FindLocationBySiteCode", new object[] {
																			   crDuns,
																			   siteCode,
																			   userName}, callback, asyncState);
		}
    
		/// <remarks/>
		public FindLocationResponse EndFindLocationBySiteCode(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((FindLocationResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/FindLocationByAddress", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public FindLocationResponse FindLocationByAddress(string crDuns, string addressLineOne, string addressLineTwo, string city, string state, string zip, string plusFourOfZip, string userName) 
		{
			object[] results = this.Invoke("FindLocationByAddress", new object[] {
																					 crDuns,
																					 addressLineOne,
																					 addressLineTwo,
																					 city,
																					 state,
																					 zip,
																					 plusFourOfZip,
																					 userName});
			return ((FindLocationResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginFindLocationByAddress(string crDuns, string addressLineOne, string addressLineTwo, string city, string state, string zip, string plusFourOfZip, string userName, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("FindLocationByAddress", new object[] {
																			  crDuns,
																			  addressLineOne,
																			  addressLineTwo,
																			  city,
																			  state,
																			  zip,
																			  plusFourOfZip,
																			  userName}, callback, asyncState);
		}
    
		/// <remarks/>
		public FindLocationResponse EndFindLocationByAddress(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((FindLocationResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/GetQuote", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public QuoteResponse GetQuote(string crDuns, string ratePlanName, int dwellingType, int unitAge, int bedroomCount, int heatingType, int windowUnits, int acUnits, int hasPool, int hasGarage, System.DateTime startDate, System.Decimal monthlyRent, int squareFootage, string userName) 
		{
			object[] results = this.Invoke("GetQuote", new object[] {
																		crDuns,
																		ratePlanName,
																		dwellingType,
																		unitAge,
																		bedroomCount,
																		heatingType,
																		windowUnits,
																		acUnits,
																		hasPool,
																		hasGarage,
																		startDate,
																		monthlyRent,
																		squareFootage,
																		userName});
			return ((QuoteResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginGetQuote(
			string crDuns, 
			string ratePlanName, 
			int dwellingType, 
			int unitAge, 
			int bedroomCount, 
			int heatingType, 
			int windowUnits, 
			int acUnits, 
			int hasPool, 
			int hasGarage, 
			System.DateTime startDate, 
			System.Decimal monthlyRent, 
			int squareFootage, 
			string userName, 
			System.AsyncCallback callback, 
			object asyncState) 
		{
			return this.BeginInvoke("GetQuote", new object[] {
																 crDuns,
																 ratePlanName,
																 dwellingType,
																 unitAge,
																 bedroomCount,
																 heatingType,
																 windowUnits,
																 acUnits,
																 hasPool,
																 hasGarage,
																 startDate,
																 monthlyRent,
																 squareFootage,
																 userName}, callback, asyncState);
		}
    
		/// <remarks/>
		public QuoteResponse EndGetQuote(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((QuoteResponse)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://empower-test.epway.com/ews/EnrollPrepaidCustomer", RequestNamespace="https://empower-test.epway.com/ews", ResponseNamespace="https://empower-test.epway.com/ews", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public EnrollmentResponse EnrollPrepaidCustomer(
			string crDuns, 
			string siteCode, 
			string quoteId, 
			string userName, 
			string pin, 
			string billingAddressLineOne, 
			string billingAddressLineTwo, 
			string billingCity, 
			string billingState, 
			string billingZip, 
			string billingPlusFourOfZip, 
			string firstName, 
			string middleName, 
			string lastName, 
			string primaryPhone, 
			string secondaryPhone, 
			string email, 
			string fax, 
			int preferredContactMethod, 
			string socialSecurityNumber, 
			string driversLicenseNumber, 
			string driversLicenseState, 
			System.DateTime dateOfBirth, 
			string permitName, 
			string customerNumberReference, 
			string doingBusinessAs, 
			int specialNeedsRequired, 
			int lowIncomeCustomer, 
			int language) 
		{
			object[] results = this.Invoke("EnrollPrepaidCustomer", new object[] {
																					 crDuns,
																					 siteCode,
																					 quoteId,
																					 userName,
																					 pin,
																					 billingAddressLineOne,
																					 billingAddressLineTwo,
																					 billingCity,
																					 billingState,
																					 billingZip,
																					 billingPlusFourOfZip,
																					 firstName,
																					 middleName,
																					 lastName,
																					 primaryPhone,
																					 secondaryPhone,
																					 email,
																					 fax,
																					 preferredContactMethod,
																					 socialSecurityNumber,
																					 driversLicenseNumber,
																					 driversLicenseState,
																					 dateOfBirth,
																					 permitName,
																					 customerNumberReference,
																					 doingBusinessAs,
																					 specialNeedsRequired,
																					 lowIncomeCustomer,
																					 language});
			return ((EnrollmentResponse)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginEnrollPrepaidCustomer(
			string crDuns, 
			string siteCode, 
			string quoteId, 
			string userName, 
			string pin, 
			string billingAddressLineOne, 
			string billingAddressLineTwo, 
			string billingCity, 
			string billingState, 
			string billingZip, 
			string billingPlusFourOfZip, 
			string firstName, 
			string middleName, 
			string lastName, 
			string primaryPhone, 
			string secondaryPhone, 
			string email, 
			string fax, 
			int preferredContactMethod, 
			string socialSecurityNumber, 
			string driversLicenseNumber, 
			string driversLicenseState, 
			System.DateTime dateOfBirth, 
			string permitName, 
			string customerNumberReference, 
			string doingBusinessAs, 
			int specialNeedsRequired, 
			int lowIncomeCustomer, 
			int language, 
			System.AsyncCallback callback, 
			object asyncState) 
		{
			return this.BeginInvoke("EnrollPrepaidCustomer", new object[] {
																			  crDuns,
																			  siteCode,
																			  quoteId,
																			  userName,
																			  pin,
																			  billingAddressLineOne,
																			  billingAddressLineTwo,
																			  billingCity,
																			  billingState,
																			  billingZip,
																			  billingPlusFourOfZip,
																			  firstName,
																			  middleName,
																			  lastName,
																			  primaryPhone,
																			  secondaryPhone,
																			  email,
																			  fax,
																			  preferredContactMethod,
																			  socialSecurityNumber,
																			  driversLicenseNumber,
																			  driversLicenseState,
																			  dateOfBirth,
																			  permitName,
																			  customerNumberReference,
																			  doingBusinessAs,
																			  specialNeedsRequired,
																			  lowIncomeCustomer,
																			  language}, callback, asyncState);
		}
    
		/// <remarks/>
		public EnrollmentResponse EndEnrollPrepaidCustomer(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((EnrollmentResponse)(results[0]));
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class BalanceResponse : EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public Balance Balance;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class Balance 
	{
    
		/// <remarks/>
		public string AccountNumber;
    
		/// <remarks/>
		public string AccountName;
    
		/// <remarks/>
		public System.Decimal AccountBalance;
    
		/// <remarks/>
		public int Status;
    
		/// <remarks/>
		public string AccountNumberReference;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class Enrollment 
	{
    
		/// <remarks/>
		public string AccountNumber;
    
		/// <remarks/>
		public string AccountName;
    
		/// <remarks/>
		public string StartDate;
    
		/// <remarks/>
		public string ServiceProviderName;
    
		/// <remarks/>
		public string ServiceProviderPhone;
    
		/// <remarks/>
		public System.Decimal PrepaymentRequired;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class RatePlanChargeDetail 
	{
    
		/// <remarks/>
		public System.Decimal ChargeAmount;
    
		/// <remarks/>
		public string ChargeDescription;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class Quote 
	{
    
		/// <remarks/>
		public int Id;
    
		/// <remarks/>
		public System.Decimal PrepaymentRequired;
    
		/// <remarks/>
		public int EstimatedUsage;
    
		/// <remarks/>
		public System.Decimal RatePerKwh;
    
		/// <remarks/>
		public int Status;
    
		/// <remarks/>
		public string AccountNumberReference;
    
		/// <remarks/>
		public RatePlanChargeDetail[] RecurringCharges;
    
		/// <remarks/>
		public RatePlanChargeDetail[] NonRecurringCharges;
    
		/// <remarks/>
		public RatePlanChargeDetail[] EventCharges;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class ServiceLocation 
	{
    
		/// <remarks/>
		public string SiteCode;
    
		/// <remarks/>
		public string AddressLineOne;
    
		/// <remarks/>
		public string AddressLineTwo;
    
		/// <remarks/>
		public string City;
    
		/// <remarks/>
		public string State;
    
		/// <remarks/>
		public string Zip;
    
		/// <remarks/>
		public string PlusFourOfZip;
    
		/// <remarks/>
		public string Status;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class RatePlanEventCharge 
	{
    
		/// <remarks/>
		public string AppearsOnInvoiceAs;
    
		/// <remarks/>
		public System.Decimal FixedCharge;
    
		/// <remarks/>
		public System.Decimal PercentOffTotal;
    
		/// <remarks/>
		public int ChargePlanEvent;
    
		/// <remarks/>
		public bool Taxable;
    
		/// <remarks/>
		public bool CanBeWaived;
    
		/// <remarks/>
		public int MinimumUsageInKWH;
    
		/// <remarks/>
		public int Status;
    
		/// <remarks/>
		public string TaxItemCode;
    
		/// <remarks/>
		public int InvoiceGroupCode;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class RatePlanNonRecurringCharge 
	{
    
		/// <remarks/>
		public string AppearsOnInvoiceAs;
    
		/// <remarks/>
		public System.Decimal Amount;
    
		/// <remarks/>
		public bool Taxable;
    
		/// <remarks/>
		public bool CanBeWaived;
    
		/// <remarks/>
		public string InstructionsToApply;
    
		/// <remarks/>
		public int Status;
    
		/// <remarks/>
		public string TaxItemCode;
    
		/// <remarks/>
		public int InvoiceGroupCode;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class RatePlanRecurringCharge 
	{
    
		/// <remarks/>
		public string AppearsOnInvoiceAs;
    
		/// <remarks/>
		public System.Decimal Amount;
    
		/// <remarks/>
		public bool Taxable;
    
		/// <remarks/>
		public bool CanBeWaived;
    
		/// <remarks/>
		public int RecurringChargeIndex;
    
		/// <remarks/>
		public int Status;
    
		/// <remarks/>
		public string TaxItemCode;
    
		/// <remarks/>
		public int InvoiceGroupCode;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class RatePlanUsageCharge 
	{
    
		/// <remarks/>
		public int TimeOfUse;
    
		/// <remarks/>
		public string UnitOfMeasure;
    
		/// <remarks/>
		public System.Decimal ChargePerUnit;
    
		/// <remarks/>
		public int MinUnits;
    
		/// <remarks/>
		public int MaxUnits;
    
		/// <remarks/>
		public bool Taxable;
    
		/// <remarks/>
		public int UsageChargeIndex;
    
		/// <remarks/>
		public string AppearsOnInvoiceAs;
    
		/// <remarks/>
		public int Status;
    
		/// <remarks/>
		public string TaxItemCode;
    
		/// <remarks/>
		public int InvoiceGroupCode;
    
		/// <remarks/>
		public System.DateTime ValidFrom;
    
		/// <remarks/>
		public System.DateTime ValidTill;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class RatePlan 
	{
    
		/// <remarks/>
		public string Name;
    
		/// <remarks/>
		public int IsDefault;
    
		/// <remarks/>
		public System.DateTime ValidFrom;
    
		/// <remarks/>
		public System.DateTime ValidTo;
    
		/// <remarks/>
		public RatePlanUsageCharge[] UsageCharges;
    
		/// <remarks/>
		public RatePlanRecurringCharge[] RecurringCharges;
    
		/// <remarks/>
		public RatePlanNonRecurringCharge[] NonRecurringCharges;
    
		/// <remarks/>
		public RatePlanEventCharge[] EventCharges;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class AdjustmentReason 
	{
    
		/// <remarks/>
		public int Code;
    
		/// <remarks/>
		public string Description;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class PaymentMethod 
	{
    
		/// <remarks/>
		public string Code;
    
		/// <remarks/>
		public string Description;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class FoundOutBy 
	{
    
		/// <remarks/>
		public string Code;
    
		/// <remarks/>
		public string Description;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class Response 
	{
    
		/// <remarks/>
		public string Code;
    
		/// <remarks/>
		public string Description;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(EnrollmentResponse))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(QuoteResponse))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(FindLocationResponse))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(AdjustmentResponse))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(PaymentResponse))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(RatePlanResponse))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(AdjustmentReasonResponse))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(PaymentMethodResponse))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(FoundOutByResponse))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(BalanceResponse))]
	public class EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public Response[] Responses;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class EnrollmentResponse : EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public Enrollment Enrollment;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class QuoteResponse : EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public Quote Quote;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class FindLocationResponse : EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public ServiceLocation[] ServiceLocations;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class AdjustmentResponse : EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public Balance Balance;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class PaymentResponse : EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public Balance Balance;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class RatePlanResponse : EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public RatePlan[] RatePlans;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class AdjustmentReasonResponse : EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public AdjustmentReason[] AdjustmentReasons;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class PaymentMethodResponse : EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public PaymentMethod[] PaymentMethods;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="https://empower-test.epway.com/ews")]
	public class FoundOutByResponse : EmpowerWebServicesResponse 
	{
    
		/// <remarks/>
		public FoundOutBy[] FoundOutByList;
	}
}