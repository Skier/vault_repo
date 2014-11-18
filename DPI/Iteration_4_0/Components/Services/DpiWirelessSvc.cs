using System;
using System.Xml;
using System.Collections;
using System.Threading;

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

		#region AccountNotes

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

		#endregion

		#region Products & Zip
		
		public static IWireless_Products[] GetDpiWLMainProds(IMap imap, string provider, bool isRecharge, bool isInternational)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.GetDpiWLMainProds");
				return (IWireless_Products[])Wireless_Products.GetDpiWLMainProds(uow, provider, isRecharge, isInternational);
			}
			finally
			{
				uow.close();
			}
		}
		public static IWireless_Products[] GetDpiWLOptionalProds(IMap imap, string provider)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.GetDpiWLOptionalProds");
				return (IWireless_Products[])Wireless_Products.GetDpiWLOptionalProds(uow, provider);
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
		public static IWirelessZipCode GetZip(IMap imap, string zipCode)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.GetZip");
				return WirelessZipCode.find(uow, zipCode);
			}
			finally
			{
				uow.close();
			}
		}

		#endregion

		#region web service calls
		public static ISvcPlanDataResp GetAvailableBalanceResp(string provider, string phone, string esn)
		{
			return WSProviderFactory.GetGateway(provider).GetAvailableBalance(phone, esn);
		}
		
		public static IWirelessDeviceData GetWLDeviceDataResp(string phoneOrEsn)
		{
			return new WLDeviceDataPool(phoneOrEsn).GetWLDeviceDataResp();
		}		
		public static bool CancelWLTransaction(IMap imap, string clerkId, int tranId)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			bool resp = false;
			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.CancelWLTransaction()");
				
				logQue = WebServSvc.SetupEntry(imap, clerkId, "DpiWirelessSvc.CancelWLTransaction", "WLTranId", tranId.ToString());
				logQue.Status = WebSvcQueueStatus.Failed.ToString();

				Wireless_Transactions wt = Wireless_Transactions.find(uow, tranId);

				if (wt.ActivationFee > 0)
					resp = new TelispireWSGateway(Const.TELISPIRE).DeactivatePhone(logQue, Wireless_Custdata.find(uow, wt.AcctID).PhNumber);
				else
					resp = new TelispireWSGateway(Const.TELISPIRE).ResetPin(logQue, wt.Pin);

				if (resp)
				{
					logQue.Status = WebSvcQueueStatus.Completed.ToString();
					wt.Status = PaymentStatus.Cancelled.ToString();
					uow.commit();
				}
				
				return resp;
			}
			finally
			{
				uow.close();
				if (!resp)
					WebServSvc.SaveEntry(logQue);
			}
		}
		public static ICellPhoneReceipt ActivatePhone(IMap imap, IUser user, string provider, IWireless_Custdata customer, IPayInfo payInfo, decimal taxAmt, ICellPhoneInfo cell, IWireless_Products wp)
		{ 			
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.ActivatePhone()");
				
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "DpiWirelessSvc.ActivatePhone");
				logQue.Status = WebSvcQueueStatus.Failed.ToString();

				cell.Pin = AOL_PINs.ReservePIN(uow, wp.Wireless_product_id).PIN;
				
				IReceipt rct = WSProviderFactory.GetGateway(provider).Send(uow, user, Const.ACTIVATE_PHONE, 
					new IDomObj[] {cell, (IDomObj)logQue});

				//IReceipt rct = new PhoneTecWSGateway(Const.PHONETEC).ActivatePhone(cell, logQue);		
				
				if (((ICellPhoneReceipt)rct).Pass)
					logQue.Status = WebSvcQueueStatus.Completed.ToString();

				CreateWirelessTran(uow, user, customer, payInfo, taxAmt, wp, cell);
				
				return (ICellPhoneReceipt)rct;
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}
		}

		public static ICellPhoneReceipt Replenish(IMap imap, IUser user, string provider, IWireless_Custdata customer, IPayInfo payInfo, decimal taxAmt, ICellPhoneInfo cell, IWireless_Products wp)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.Replentish()");
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "DpiWirelessSvc.Replenish");
				logQue.Status = WebSvcQueueStatus.Failed.ToString();

				cell.Pin = AOL_PINs.ReservePIN(uow, wp.Wireless_product_id).PIN;
				IReceipt rct = WSProviderFactory.GetGateway(provider).Send(uow, user, Const.REPLENISH_SERVICE_PLAN, 
					new IDomObj[] {cell, (IDomObj)logQue});
				//IReceipt rct = new PhoneTecWSGateway(Const.PHONETEC).ReplenishServicePlan(uow, cell, logQue);
				
				if (((ICellPhoneReceipt)rct).Pass)
					logQue.Status = WebSvcQueueStatus.Completed.ToString();
				
				CreateWirelessTran(uow, user, customer, payInfo, taxAmt, wp, cell);
				
				return (ICellPhoneReceipt)rct;
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}
		}
		
				public static bool DeactivatePhone(IMap imap, IUser user, string provider, string phone)
				{
					UOW uow = null; 
					IWebSvcQueue logQue = null;
					try
					{
						if (phone == null)
							return false;
		
						uow = new UOW(imap, "DpiWirelessSvc.DeactivatePhone()");
						logQue = WebServSvc.SetupEntry(imap, user, provider, "DpiWirelessSvc.DeactivatePhone");
						logQue.Status = WebSvcQueueStatus.Failed.ToString();
						logQue.BusObject = "PhoneOrEsn";				
						logQue.BusObjId = phone;;
		
						if (provider.Trim().ToLower() == Const.TELISPIRE) 
							return new TelispireWSGateway(provider).DeactivatePhone(logQue, phone);
		
						return false;
					}
					finally
					{
						uow.close();
						WebServSvc.SaveEntry(logQue);
					}
				}
		
		public static ICellPhoneReceipt CheckPlanStatus(IMap imap, IUser user, string provider, IWireless_Custdata customer, IPayInfo payInfo, decimal taxAmt, ICellPhoneInfo cell, IWireless_Products wp)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.CheckPlanStatus()");
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "DpiWirelessSvc.CheckPlanStatus");
				logQue.Status = WebSvcQueueStatus.Failed.ToString();

				IReceipt rct = WSProviderFactory.GetGateway(provider).Send(uow, user, Const.CHECK_PLAN_STATUS, 
					new IDomObj[] {cell, (IDomObj)logQue});
				//IReceipt rct = new PhoneTecWSGateway(Const.PHONETEC).CheckPlanStatus(cell, logQue);
				
				if (((ICellPhoneReceipt)rct).Pass)
					logQue.Status = WebSvcQueueStatus.Completed.ToString();
				//CreateWirelessTran(uow, user, customer, payInfo, taxAmt, wp, cell);
				
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

		static void CreateWirelessTran(UOW uow, IUser user, IWireless_Custdata customer, IPayInfo payInfo, decimal taxAmt, IWireless_Products wp, ICellPhoneInfo cell)
		{
			IWireless_Transactions wt = new Wireless_Transactions(uow);
			wt.TrConfirm				= payInfo.Id;
			wt.PayDateTime				= DateTime.Now;
			wt.Tran_Amount				= payInfo.TotalAmountPaid - taxAmt;
			wt.Transaction_Method_ID	= (int)WirelessTranMethod.WebService;			
			wt.StoreCode				= user.LoginStoreCode;
			wt.Clerkid					= user.ClerkId;
			wt.Wireless_product_ID		= wp.Wireless_product_id;
			wt.Pin						= cell.Pin;
			wt.ActivationFee			= cell.ActivationCharge;
			wt.Commission				= GetCommission(wp, wt.Tran_Amount);
			
			if (payInfo.IsConfReq) 
				wt.Status				= PaymentStatus.PendWireless.ToString();
			else 
				wt.Status				= PaymentStatus.Paid.ToString();
			
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