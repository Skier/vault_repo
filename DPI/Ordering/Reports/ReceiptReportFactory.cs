using System;
using CrystalDecisions.CrystalReports.Engine;

using DPI.Components;
using DPI.Interfaces;
using DPI.Ordering;
using DPI.Services;
using DPI.ClientComp;


namespace DPI.Reports 
{
	public class ReceiptReportFactory
	{
	#region Methods
		public static string GetReceiptExportFilename(IWIP wip)
		{
			return Const.VIRTUAL_DIR_TEMP + ((IDemand)wip["Demand"]).Id.ToString() + ".pdf";
		}
		public static ReportClass GetReceiptReport(IWIP wip, IUser user)
		{
		#region Conversion
			if (wip is NewOrderConversionWip)
				return GetNewOrderConvRcpt(wip, user);
		#endregion

		#region New Payment
			if (wip.Workflow is NewPaymentWF)
				return GetNewPayment(wip, user);
		#endregion

		#region New Order
			if (wip.Workflow is NewOrderWF)
				return GetNewOrderReceipt(wip, user);

			if (wip.Workflow is NewOrderPendWF)
				return GetNewOrderReceipt(wip, user);

		#endregion

		#region Monthly Payment
			if (wip.Workflow is MonthlyPaymentWF)
				return GetMonthlyPaymentReceipt(wip, user);	

			if (wip.Workflow is MonthlyPymtPendWF)
				return GetMonthlyPaymentReceipt(wip, user);
		#endregion

		#region LD 
			if (wip.Workflow is LongDistanceWF)
			{				
				ReportClass rptReceipt = new MP_Receipt();
				rptReceipt.SummaryInfo.ReportTitle = "Long Distance Calling Card Receipt";
				if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
				{
					rptReceipt = new MP_Receipt_Narrow();
					rptReceipt.SummaryInfo.ReportTitle = "LD Card Receipt";
				}				

				rptReceipt.SetDataSource(ReportSvc.MP_GetReceipt(
								(IReceipt)wip["receipt"],
								(ICustInfo)((ICustInfoExt)wip["custinfoext"]).CustInfo,
								(IAcctInfo)wip["acctinfo"], 
								(IPayInfoLocal)wip["payinfo"], 
								user));
				
				return rptReceipt;
			}		
		#endregion
	
		#region Wireless
			if (wip.Workflow is WirelessWF)
				return GetWirelessReceipt(wip, user);
		#endregion

		#region Infinity
			if (wip.Workflow is WLActivationWF)
				return GetInfinityReceipt(wip, user);
		#endregion
	
		#region Internet
			if (wip.Workflow is InternetWF)
			return GetInternetReceipt(wip, user);

		#endregion

		#region Satellite
			if (wip.Workflow is SatelliteWF)
				return GetSatelliteReceipt(wip, user);
		#endregion

		#region DCTest
			if (wip.Workflow is DCTestWF)
				return GetInternetReceipt(wip, user);	

		#endregion
		
		#region Reprint receipt
			if (wip.Workflow is ReprintReceiptWF) 
				return GetReprintReceipt(wip, user);
		#endregion

		#region Debit Card New
			if (wip.Workflow is DebCardWF)
			{
				if(((IDebitCardReceipt)wip["Receipt"]).IsApproved) // bool to determine status of application
					return GetDebitCardNewReceipt(wip, user);
			
				return GetDebitCardNewReceiptDeny(wip, user);
			}			
		#endregion

		#region Debit Card Redeem New
			if (wip.Workflow is DebCardRedeemWF)
			{
				if(((IDebitCardReceipt)wip["Receipt"]).IsApproved) // bool to determine status of application
					return GetDebitCardNewReceipt(wip, user);
			
				return GetDebitCardNewReceiptDeny(wip, user);
			}
			
		#endregion

		#region Debit Card Reload
			if (wip.Workflow is DebCardReloadWF)
			{
				if(!((IDebitCardReceipt)wip["Receipt"]).IsApproved ) // bool to determine status of application
					return GetDebitCardReFillReceiptDeny(wip, user);

				return GetDebitCardReFillReceipt(wip, user);
			}
			throw new ArgumentException("Invalid workflow: " + wip.Workflow);
		}	
		#endregion		

	#endregion

	#region Implementation
		static ReportClass GetNewOrderReceipt(IWIP wip, IUser user)
		{
			switch ((bool)wip["IsHighTouch"])
			{
				case true :
				{
					ReportClass rptReceipt = null;

					if (wip is NewOrderConversionWip)
						return GetNewOrderConvRcpt(wip, user);					
								
					rptReceipt = new HI_Reciept();
					rptReceipt.SummaryInfo.ReportTitle = "New Order Confirmation";
	
					rptReceipt.SetDataSource(ReportSvc.HI_GetReceipt(
						(IReceipt)wip["receipt"], // // no cust number in the receipt
						(ICustInfo)wip["custinfo"],  
						(IOrderSum)wip["ordersummary"], 
						(IPayInfoLocal)wip["payinfo"], // use PayInfo.Id instead of AccNumber
						user,
						"To contact Customer Service Please call 1-800-350-4009 " 
						+ "or email us at customerservice@dpiteleconnect.com"));
				
					return rptReceipt;
					
				}
				default :
				{
					ReportClass rptReceipt = null;

					if (wip is NewOrderConversionWip)
						return GetNewOrderConvRcpt(wip, user);

					rptReceipt = new NO_Reciept();
					
					if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
						rptReceipt = new NO_Reciept_Narrow();

					rptReceipt.SummaryInfo.ReportTitle = "New Order Receipt";
	
					rptReceipt.SetDataSource(ReportSvc.NO_GetReceipt(
						(IReceipt)wip["receipt"], 
						(ICustInfo)wip["custinfo"], 
						(IOrderSum)wip["ordersummary"], 
						(IPayInfoLocal)wip["payinfo"], 
						user,
						"To contact Customer Service Please call 1-800-350-4009 " 
						+ "or email us at customerservice@dpiteleconnect.com"));
				
					return rptReceipt;
				}
			}
		}

		static ReportClass GetNewOrderConvRcpt(IWIP wip, IUser user)
		{
			ReportClass rptReceipt = new HI_ConvRecpt();
			rptReceipt.SummaryInfo.ReportTitle = "Customer Conversion";
	
			rptReceipt.SetDataSource(ReportSvc.HI_GetReceipt(
				(IReceipt)wip["receipt"], // // no cust number in the receipt
				(ICustInfo)wip["custinfo"],  
				(IOrderSum)wip["ordersummary"], 
				(IPayInfoLocal)wip["payinfo"], // use PayInfo.Id instead of AccNumber
				user,
				"To contact Customer Service Please call 1-800-350-4009 " 
				+ "or email us at customerservice@dpiteleconnect.com"));
				
			return rptReceipt;
		}
		static ReportClass GetNewPayment(IWIP wip, IUser user)
		{
			ReportClass rptReceipt = new NO_Reciept();
			if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
				rptReceipt = new NO_Reciept_Narrow();
			
			rptReceipt.SummaryInfo.ReportTitle = "New Payment Receipt";
	
			rptReceipt.SetDataSource(ReportSvc.NO_GetReceipt(
				(IReceipt)wip["receipt"], 
				(ICustInfo)wip["custinfo"], 
				(IOrderSum)wip["ordersummary"], 
				(IPayInfoLocal)wip["payinfo"], 
				user,
				"Please call 1-800-646-2111 now to order service!"));
			
			return rptReceipt;
		}
		static ReportClass GetMonthlyPaymentReceipt(IWIP wip, IUser user)
		{
			switch ((bool)wip["IsHighTouch"])
			{
				case true :
				{
					ReportClass rptReceipt = new HIMP_Receipt();
					rptReceipt.SummaryInfo.ReportTitle = "Monthly Payment Confirmation";

					rptReceipt.SetDataSource(ReportSvc.HIMP_GetReceipt(
						(IReceipt)wip["receipt"],
						(ICustInfo)((ICustInfoExt)wip["custinfoext"]).CustInfo, 
						(IAcctInfo)wip["acctinfo"], 
						(IPayInfoLocal)wip["payinfo"], 
						user));
					return rptReceipt;
				}
				default :
				{
					ReportClass rptReceipt = new MP_Receipt();
					rptReceipt.SummaryInfo.ReportTitle = "Monthly Payment Receipt";
					if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
					{
						rptReceipt = new MP_Receipt_Narrow();
						rptReceipt.SummaryInfo.ReportTitle = "Monthly Receipt";
					}

					rptReceipt.SetDataSource(ReportSvc.MP_GetReceipt(
						(IReceipt)wip["receipt"],
						(ICustInfo)((ICustInfoExt)wip["custinfoext"]).CustInfo, 
						(IAcctInfo)wip["acctinfo"], 
						(IPayInfoLocal)wip["payinfo"], 
						user));
					
					return rptReceipt;
				}
			}
		}

		static ReportClass GetDebitCardNewReceipt(IWIP wip, IUser user)
		{
			ReportClass rptReceipt = new DCNew_Receipt();
			rptReceipt.SummaryInfo.ReportTitle = "Purpose Prepaid MasterCard Receipt";
			if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
			{
				rptReceipt = new DCNew_Receipt_Narrow();
				rptReceipt.SummaryInfo.ReportTitle = "Purpose Receipt";
			}

			
			IDebitCardReceipt rcpt = (IDebitCardReceipt)wip["receipt"];
			
			rptReceipt.SetDataSource(ReportSvc.DCNew_Receipt
				(rcpt, (IProdPrice)wip["DebCard"],
				(IPayInfo)wip["PayInfo"], 
				(ICardApp)wip["DebCardApp"], user));

			return rptReceipt;
		}

		static ReportClass GetDebitCardNewReceiptDeny(IWIP wip, IUser user)
		{
			ReportClass rptReceipt = new DCNew_Receipt_Deny();
			rptReceipt.SummaryInfo.ReportTitle = "Purpose Prepaid MasterCard Receipt";
			if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
			{
				rptReceipt = new DCNew_Receipt_Deny_Narrow();
				rptReceipt.SummaryInfo.ReportTitle = "Purpose Receipt";
			}
			IDebitCardReceipt rcpt = (IDebitCardReceipt)wip["receipt"];
			ICustInfo2 ci = (ICustInfo2)wip["custinfo"];
			IPayInfo pi = (IPayInfo)wip["payinfo"];

			rptReceipt.SetDataSource(ReportSvc.DCNew_Receipt_Deny
				(rcpt,  
				(IProdPrice)wip["DebCard"],
				(IPayInfo)wip["PayInfo"], 
				user));

			return rptReceipt;
		}

		static ReportClass GetDebitCardReFillReceipt(IWIP wip, IUser user)
		{
			ReportClass rptReceipt = new DCReload_Receipt();
			rptReceipt.SummaryInfo.ReportTitle = "Purpose Prepaid MasterCard Receipt";
			if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
			{
				rptReceipt = new DCReload_Receipt_Narrow();
				rptReceipt.SummaryInfo.ReportTitle = "Purpose Receipt";
			}
			IDebitCardReceipt rcpt = (IDebitCardReceipt)wip["receipt"];

			rptReceipt.SetDataSource(ReportSvc.DCReload_Receipt
				(rcpt, 
				(ICardApp)wip["DebCardApp"], 
				(IProdPrice)wip["DebCard"], 
				(ICustInfo2)wip["CustInfo"], 
				(IPayInfo)wip["PayInfo"],
				user));
			
			return rptReceipt;
		}

		static ReportClass GetDebitCardReFillReceiptDeny(IWIP wip, IUser user)
		{
			ReportClass rptReceipt = new DCReload_Receipt_Deny();
			rptReceipt.SummaryInfo.ReportTitle = "Purpose Prepaid MasterCard Receipt";
			if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
			{
				rptReceipt = new DCReload_Receipt_Deny_Narrow();
				rptReceipt.SummaryInfo.ReportTitle = "Purpose Receipt";
			}

			IDebitCardReceipt rcpt = (IDebitCardReceipt)wip["receipt"];
			ICustInfo2 ci = (ICustInfo2)wip["custinfo"];
			IPayInfo pi = (IPayInfo)wip["payinfo"];

			rptReceipt.SetDataSource(ReportSvc.DCReload_Receipt_Deny(
				rcpt, 
				ci, 
				pi, 
				user, 
				wip));

			return rptReceipt;
		}
		static ReportClass GetInfinityReceipt(IWIP wip, IUser user)
		{
			ReportClass rptReceipt = new InfNewCell_Rcpt();
			rptReceipt.SummaryInfo.ReportTitle = "Cellular Payment Receipt";
			if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
			{
				rptReceipt = new InfNewCell_Rcpt_Narrow();
				rptReceipt.SummaryInfo.ReportTitle = "Cellular Receipt";
			}

			rptReceipt.SetDataSource(ReportSvc.Infinity_GetReceipt((ICellPhoneReceipt)wip["receipt"],
				(IPinProduct)wip["SelectedPinProduct"], 
				(IPayInfo)wip["payinfo"], 
				user));

			return rptReceipt;
		}
		static ReportClass GetInternetReceipt(IWIP wip, IUser user)
		{
			ReportClass rptReceipt = new Pin_Receipt();
			rptReceipt.SummaryInfo.ReportTitle = "Internet Payment Receipt";
			if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
			{
				rptReceipt = new Pin_Receipt_Narrow();
				rptReceipt.SummaryInfo.ReportTitle = "Internet Receipt";
			}
			rptReceipt.SetDataSource(ReportSvc.Pin_GetReceipt((IPinReceipt)wip["receipt"],
				(IPinProduct)wip["selectedpinproduct"], 
				(IPayInfo)wip["payinfo"], 
				user));

			return rptReceipt;
		}
		static ReportClass GetWirelessReceipt(IWIP wip, IUser user)
		{
			ReportClass rptReceipt = new Pin_Receipt();
			rptReceipt.SummaryInfo.ReportTitle = "Cellular Payment Receipt";
			
			if (StoreLocation.find(new UOW(), user.LoginStoreCode).IsNarrowPrinter)
			{
				rptReceipt = new Pin_Receipt_Narrow();
				rptReceipt.SummaryInfo.ReportTitle = "Cellular Receipt";
			}
				
			rptReceipt.SetDataSource(ReportSvc.Pin_GetReceipt((IPinReceipt)wip["receipt"],
				(IPinProduct)wip["selectedpinproduct"], 
				(IPayInfo)wip["payinfo"], 
				user));

			return rptReceipt;
		}
		static ReportClass GetSatelliteReceipt(IWIP wip, IUser user)
		{
			ReportClass rptReceipt = new Pin_Receipt();
			rptReceipt.SummaryInfo.ReportTitle = "Satellite Payment Receipt";
			
			rptReceipt.SetDataSource(ReportSvc.Pin_GetReceipt((IPinReceipt)wip["receipt"],
				(IPinProduct)wip["selectedpinproduct"], 
				(IPayInfo[])wip["payinfos"], 
				user));

			return rptReceipt;
		}
		static ReportClass GetReprintReceipt(IWIP wip, IUser user)
		{
			IDemand dmd = (IDemand)wip["Demand"];
				
			ICorporation corp = StoreStatsCol.GetCorporation(dmd.StoreCode);

			bool rac = corp.RAC_WF;
				
			if (dmd.DmdType.ToLower() == DemandType.Monthly.ToString().ToLower())
				return GetMonthlyPaymentReceipt(wip, user);

			if (dmd.DmdType.ToLower() == DemandType.NewPymt.ToString().ToLower())
				return GetNewPayment(wip, user);

			if (dmd.DmdType.ToLower() == DemandType.New.ToString().ToLower())
				return GetNewOrderReceipt(wip, user);

			throw new ArgumentException("Unknown Demand Type: " + dmd.DmdType);
		}
	#endregion
	}
}