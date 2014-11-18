using System;
 
namespace DPI.Interfaces
{
	// Framework
	public enum RowState { Clean, Dirty, New, Remove, Deleted } 
	public enum PageDirection { First, Next, Previous, Last, PageNum} 
	
	// General
	public enum PhoneNumParts{ Npa, Nxx, Line }

	 
	// Web Servives
	public enum WebSvcQueueType { Post, Reversal, Query, Evergreen }
	public enum WebSvcQueueStatus { Open, Completed, Failed, Reversed, Error } 
	
	// Payment
	public enum PaymentType {Cash, Credit, Debit, Check, MoneyOrder, TurboCash }
	public enum PayInfoSource { Unknown, Verifone, Wireless, FinProd }
	public enum PayInfoClass { PayInfo, PayInfoLocal }
	public enum PaymentStatus {Incomplete, Pend, PendConfirm, Paid, Cancelled, PendWireless }

	// Product
	public enum ProdCategory { Internet, Wireless, Satellite, DpiWireless, DpiWirelessInt }
	public enum ProdSelectionState { Selected, Available, Unavailable }

	// Demand Group
	public enum DemandType 
	{
		New, NewPymt, Monthly, PriceLU, DebCardNew, DebCardReload, Reversal, Internet, 
		ReversalVoid, Wireless, LocalConv, Satellite, RecurringPymts, DpiWireless }	
	public enum OrderType {New, Add, Conv}
	public enum DemandStatus { Pend, PendCustInfo, PendConf, Submited, Cancelled, Approved, Denied }
	public enum PendingOrderPaymentInfoCriteria	{Corporation, Store }
	public enum DItemType { Selected, PackComp, TagAlong, Mapped  }
	public enum Transaction_Type_Id{Unknown, Monthly, New}	

	// Store
	public enum Store_OrderStatusCriteria 
	{
		All_PayDate, All_AccountStatus, Active_PayDate, Disconnected_PayDate, 
		Pending_PayDate, Active_DueDate }
	public enum Store_CustomerListCriteria { Active_ActiveDate, Disconnected_DueDate }
	public enum Store_CommissionCriteria {Local_Commission, Cell_Commission }
	public enum Store_DayTotalsStyle { Summary, Detail }	
	
	// Traning & Certification
	public enum CertResultCriteria	{Store, Corporation }
	public enum CertType { Debit_Card = 1, Basic_Web, Slingshot }

	// Customer group
	public enum AddressType{ Mailing, Service}
	public enum StatType { Active, New, Revenues }
	public enum CustInfoTypes { CardApp }
	public enum CustInfoStatus { Prospect, Active, Inactive }

	// Debit card
	public enum CardAppStatus { Pend, Approved, Denied }
	public enum FinProdTranStatus { Active, Invoiced, Cancelled }
	public enum FinProdTranType { DC_Enroll, DC_Reload, DC_CompEnroll }
	
	//Wireless
	public enum WirelessTranStatus { Unknown, Completed, Pend, Redeemed, Declined, Voided, Refund }
	public enum WirelessTranMethod { Verifone = 1, WU, IVR, Manual, ACE, CreditCard, EPP, WebService }

	// Reversal / Voids
	public enum VoidTranType { Unknown,  Verifone, Wireless, FinProd }
	
	// Agent registration
	public enum AgentTitle { Clerk, Manager, Owner }

	//Receipt2 items
	public enum ReceiptItemType { Title, Header, Footer, Body}

	// Rentway stuff
	public enum MapDomain { DPI = 1, Rentway }
	public enum MapCategory { PaymentType = 1, DmdType, TranType, ServType }
//	public enum RW_PayType {Cash, Credit, Debit, Check, Money_Order }
//	public enum RW_TranType { Revenue, Reversal }
//	public enum RW_SvcType { Dpi, Wir } // DPI = phone service, Wir = wireless

	//Dpi_Err_Log
	public enum ErrLogSubSystems { Logon, EZTaxWrapper, WQSpinner, UserAccount, Global, OperSvc, StoreStatCol, Wipper }

	//Dpi_Wireless
	public enum DpiWLProdSubCategory { NewActivation, CashCard, International }

}