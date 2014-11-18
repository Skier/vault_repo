using System;
using System.Xml;
using System.Collections;

using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class DpiWirelessSvc : ISvcProvider
	{
		#region Const & Properties

		const string provider =  "DpiWirelessSvc";
		public string Provider { get { return provider; }}

		#endregion

		#region Vendor

		public static IPinVendor GetNewPinVendor(string vendId, string vendName)
		{
			return new PinVendor(vendId, vendName);
		}
		#endregion
		

		public static void SetAccountNotes(IMap imap, int accNumber, string user, string note)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.SetAccountNotes");
				new Account_Notes(uow, accNumber, user, note, "DpiWireless"); 
			}
			finally
			{
				uow.close();
			}
		}

		
		#region Dpi Wireless

		public static ISvcPlanDataResp GetDpiSvcPlanDataResp(string phone, string esn)
		{
				return new PhoneTecWSGateway(Const.PHONETEC).GetDpiSvcPlanData(phone, esn);			
		}
		public static ISvcPlanDataResp GetAvailableBalanceResp(string phone, string esn)
		{
			return new PhoneTecWSGateway(Const.PHONETEC).GetAvailableBalance(phone, esn);			
		}
		public static IWireless_Products[] GetDpiWLMainProds(IMap imap, bool isRecharge, bool isInternational)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.GetDpiWLMainProds");
				return (IWireless_Products[])Wireless_Products.GetDpiWLMainProds(uow, isRecharge, isInternational);
			}
			finally
			{
				uow.close();
			}
		}
		public static IWireless_Products[] GetDpiWLOptionalProds(IMap imap)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.GetDpiWLOptionalProds");
				return (IWireless_Products[])Wireless_Products.GetDpiWLOptionalProds(uow);
			}
			finally
			{
				uow.close();
			}
		}

		public static IWireless_Products[] GetPackageByProdList(IMap imap, string prodList)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.GetPackageByProdList");
				return (IWireless_Products[])Wireless_Products.GetPackageByProdList(uow, prodList);
			}
			finally
			{
				uow.close();
			}
		}

		public static IWireless_Products[] GetProdsBySoc(IMap imap, string soc, bool isActivation)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.GetProdsBySoc");
				return (IWireless_Products[])Wireless_Products.GetProdsBySoc(uow, soc, isActivation);
			}
			finally
			{
				uow.close();
			}
		}
		public static bool MatchZipCode(IMap imap, string zipCode)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.MatchZipCode");
				return WirelessZipCode.MatchZipCode(uow, zipCode);
			}
			finally
			{
				uow.close();
			}
		}
		//MatchZipCode
		public static ICellPhoneReceipt ActivatePhone(IMap imap, IUser user, IWireless_Custdata customer, IPayInfo payInfo, decimal taxAmt, ICellPhoneInfo cell, IWireless_Products wp)
		{ 			
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			//IWireless_Transactions tran = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.ActivatePhone()");
				
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "DpiWirelessSvc.ActivatePhone");
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				cell.Pin = AOL_PINs.ReservePIN(uow, wp.Wireless_product_id).PIN;
				IReceipt rct = new PhoneTecWSGateway(Const.PHONETEC).ActivatePhone(cell, logQue);		
				
				logQue.Status 
					=  ((ICellPhoneReceipt)rct).Pass ? 
					WebSvcQueueStatus.Completed.ToString()
					: WebSvcQueueStatus.Failed.ToString();

				CreateWirelessTran(uow, user, customer, payInfo, taxAmt, wp, cell);


				return (ICellPhoneReceipt)rct;
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}
		}

		public static ICellPhoneReceipt Replenish(IMap imap, IUser user, IWireless_Custdata customer, IPayInfo payInfo, decimal taxAmt, ICellPhoneInfo cell, IWireless_Products wp)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.Replentish()");
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "DpiWirelessSvc.Replenish");
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				cell.Pin = AOL_PINs.ReservePIN(uow, wp.Wireless_product_id).PIN;
				IReceipt rct = new PhoneTecWSGateway(Const.PHONETEC).ReplenishServicePlan(uow, cell, logQue);
				
				CreateWirelessTran(uow, user, customer, payInfo, taxAmt, wp, cell);

				logQue.Status = WebSvcQueueStatus.Completed.ToString();
				return (ICellPhoneReceipt)rct;
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}
		}
		#endregion

		#region Misc 

		public void FireAway(string action, string xml)
		{
			//do nothing for now
		}

//		static void CreateWirelessTran(UOW uow, IUser user, IPayInfo payInfo, string pin, int prodId, IReceipt rct)
//		{
//			IWireless_Transactions tran = null;
//			rct.ConfNum = "00";
//			int tranId = 0;
//			string trNum = new Random().Next(1, 99900).ToString();
//			
//			if (payInfo.Status == PaymentStatus.PendConfirm.ToString())
//			{
//				tranId = Wireless_Transactions.PostPendWirelessTran(
//					uow, 
//					prodId, 
//					user.LoginStoreCode, 
//					user.ClerkId, 
//					pin,
//					trNum);
//				
//				rct.ConfNum = tranId.ToString();
//			}
//			else
//			{
//				tran = Wireless_Transactions.PostWirelessTran(uow, 
//					prodId, 
//					user.LoginStoreCode, 
//					user.ClerkId,
//					pin,
//					trNum);
//				
//				rct.ConfNum = tran.Wireless_Transaction_ID.ToString();
//			}			
//		}
		static void CreateWirelessTran(UOW uow, IUser user, IWireless_Custdata customer, IPayInfo payInfo, decimal taxAmt, IWireless_Products wp, ICellPhoneInfo cell)
		{
			IWireless_Transactions wt = new Wireless_Transactions(uow);
			wt.TrConfirm				= new Random().Next(1, 1000000);
			wt.PayDateTime				= DateTime.Now;
			wt.Tran_Amount				= payInfo.TotalAmountPaid - taxAmt;
			wt.Transaction_Method_ID	= (int)WirelessTranMethod.WebService;			
			wt.StoreCode				= user.LoginStoreCode;
			wt.Clerkid					= user.ClerkId;
			wt.Wireless_product_ID		= wp.Wireless_product_id;
			wt.Pin						= cell.Pin;
			wt.ActivationFee			= cell.ActivationCharge;
			wt.Commission				= GetCommission(wp, payInfo.TotalAmountPaid);
			wt.Status					= payInfo.Status;
			wt.Customer					= customer;
			wt.TaxAmt					= taxAmt;
			
			payInfo.Tran				= (IPayInfoTran)wt;					
			}
		static decimal GetCommission(IWireless_Products wp, decimal tranAmt)
			{
			if (wp.Product_commission_percent > 0)
				return decimal.Round(tranAmt * wp.Product_commission_percent / 100, 2);

			return decimal.Round(wp.Product_commission_flat, 2);
		}
		#endregion

	}
}