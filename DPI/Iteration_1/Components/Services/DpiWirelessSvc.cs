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
		
		#region Dpi Wireless

		public static ISvcPlanDataResp GetDpiSvcPlanDataResp(string phone, string esn)
		{
				return new PhoneTecWSGateway(Const.PHONETEC).GetDpiSvcPlanData(phone, esn);			
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
		public static IWireless_Products[] GetDpiWLOptionalProds(IMap imap, int vendor)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.GetDpiWLOptionalProds");
				return (IWireless_Products[])Wireless_Products.GetDpiWLOptionalProds(uow, vendor);
			}
			finally
			{
				uow.close();
			}
		}

		public static ICellPhoneReceipt ActivatePhone(IMap imap, IUser user, IPayInfo payInfo, ICellPhoneInfo cell, IPinProduct prod)
		{ 			
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			IWireless_Transactions tran = null;

			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.ActivatePhone()");
				
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "DpiWirelessSvc.ActivatePhone");
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				//cell.Pin = AOL_PINs.ReservePIN(uow, prod.Product_Id).PIN;
				cell.Pin = "0809898809809";
				//IWireless_Products wp = Wireless_Products.find(uow, prod.Product_Id);				
				IReceipt rct = new PhoneTecWSGateway(Const.PHONETEC).ActivatePhone(cell, logQue);		
				
				logQue.Status 
					=  ((ICellPhoneReceipt)rct).Pass ? 
					WebSvcQueueStatus.Completed.ToString()
					: WebSvcQueueStatus.Failed.ToString();

				PostWirelessTran(uow, user, payInfo, cell.Pin, prod.Product_Id, rct);

				if (!((ICellPhoneReceipt)rct).Pass)
					uow.Rollback();

				return (ICellPhoneReceipt)rct;
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}
		}

		public static ICellPhoneReceipt Replenish(IMap imap, IUser user, IPayInfo payInfo, ICellPhoneInfo cell)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			try
			{
				uow = new UOW(imap, "DpiWirelessSvc.Replentish()");
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "DpiWirelessSvc.Replenish");
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				IReceipt rct = new PhoneTecWSGateway(Const.PHONETEC).ReplenishServicePlan(uow, cell, logQue);
				
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

		static void PostWirelessTran(UOW uow, IUser user, IPayInfo payInfo, string pin, int prodId, IReceipt rct)
		{
			IWireless_Transactions tran = null;
			rct.ConfNum = "00";
			int tranId = 0;
			string trNum = new Random().Next(1, 99900).ToString();
			
			if ((payInfo == null) || (payInfo.ParDemand == null) || (payInfo.ParDemand.Status != DemandStatus.PendConf.ToString()))
			{
				tran = Wireless_Transactions.PostWirelessTran(uow, 
					prodId, 
					user.LoginStoreCode, 
					user.ClerkId,
					pin,
					trNum);
				rct.ConfNum = tran.TrConfirm.ToString();
			}
			else
			{
				tranId = Wireless_Transactions.PostPendWirelessTran(
					uow, 
					prodId, 
					user.LoginStoreCode, 
					user.ClerkId, 
					pin,
					trNum);
				rct.ConfNum = tranId.ToString();
			}			
		}
		#endregion

	}
}