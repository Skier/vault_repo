using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class WirelessTranSvc
	{
		public static IWireless_Transactions[] GetNonRedeemedTrans(IMap imap)
		{
			UOW uow = null;			

			try
			{
				uow = new UOW(imap, "WirelessTranSvc.GetNonRedeemedTrans");			
				return Wireless_Transactions.GetNonRedeemedTrans(uow);
			}
			finally
			{
				uow.close();
			}
		}
		public static IWireless_Transactions[] GetVdblTransByStore(IMap imap, string storeCode)
		{
			UOW uow = null;			

			try
			{
				uow = new UOW(imap, "WirelessTranSvc.GetNonVoidedTransByVendor");			
				return Wireless_Transactions.GetVdblTransByStore(uow, storeCode);
			}
			finally
			{
				uow.close();
			}
		}
		public static IWireless_Products GetWirelessProd(IMap imap, int prodId)
		{
			UOW uow = null;			

			try
			{
				uow = new UOW(imap, "WirelessTranSvc.GetWirelessProd");			
				return Wireless_Products.find(uow, prodId);
			}
			finally
			{
				uow.close();
			}
		}
		public static IWireless_Transactions GetWSTran(IMap imap, int id)
		{
			UOW uow = null;			

			try
			{
				uow = new UOW(imap, "WirelessTranSvc.GetWSTran");			
				return Wireless_Transactions.Locate(uow, id);
			}
			finally
			{
				uow.close();
			}
		}
		public static string GetWSProvider(IMap imap, int wpId)
		{
			UOW uow = null;			

			try
			{
				uow = new UOW(imap, "WirelessTranSvc.GetWSTran");	
				
				Wireless_Products wp = Wireless_Products.find(uow, wpId);

				IVendors ven =  Vendors.find(uow, wp.Vendor_id);
			
				if (wp.OverrideWSProvider == null)		
					return ven.DefaultWSProvider;
			
				if (wp.OverrideWSProvider.Trim().Length == 0)
					return ven.DefaultWSProvider;

				return wp.OverrideWSProvider;
			}
			finally
			{
				uow.close();
			}
		}
		public static void VoidTransaction(IMap imap, IUser user, int tran_id)
		{
			UOW uow = null;
			IWebSvcQueue webQue = null;

			try
			{
				uow = new UOW(imap, "WirelessTranSvc.VoidTransaction");
				uow.BeginTransaction();
				IWireless_Transactions wt = Wireless_Transactions.find(uow, tran_id);
				IWireless_Products wp = Wireless_Products.find(uow, wt.Wireless_product_ID);

				webQue = WebServSvc.SetupEntry(imap, user, WebSvcQueueType.Reversal, Const.VOID_PRODUCT, 
									WSProviderFactory.GetProvider(uow, wp.Vendor_id).Provider);
				
				WSProviderFactory.GetProvider(uow, wp.Vendor_id)
						.Send(uow, user, Const.VOID_PRODUCT, 
						new IDomObj[] { (IDomObj)wt, (IDomObj)webQue}); 								
				uow.commit();
			}
			catch (Exception e)
			{
				webQue.Status = WebSvcQueueStatus.Failed.ToString();
				throw e;
			}
			finally
			{
				WebServSvc.SaveEntry(webQue);
				uow.close();
			}
		}
		public static void ConfirmTransaction(IMap imap, int tran_id, string trConfirm)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "WirelessTranSvc.ConfirmTransaction");
				uow.BeginTransaction();
				
				IWireless_Transactions wt = Wireless_Transactions.Locate(uow, tran_id);
				
				if (wt.Status == null || wt.Status != PaymentStatus.PendWireless.ToString())
					throw new ArgumentException("Transaction is not in pending status");
				
				wt.Status = PaymentStatus.Paid.ToString();
				wt.TrNumber = trConfirm;

				uow.commit();
			}
			finally
			{
				uow.close();
			}
		}
		public static void CancelTransaction(IMap imap, int tran_id)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "WirelessTranSvc.ConfirmTransaction");
				uow.BeginTransaction();
				
				IWireless_Transactions wt = Wireless_Transactions.Locate(uow, tran_id);
				
				if (wt.Status == null || wt.Status != PaymentStatus.PendWireless.ToString())
					throw new ArgumentException("Transaction is not in pending status");
				
				wt.Status = PaymentStatus.Cancelled.ToString();
				
				uow.commit();
			}
			finally
			{
				uow.close();
			}
		}
		public static int PostPendWLTran(IMap imap, int prod, string storeCode, string clerkId, string pin)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "WirelessTranSvc.PostPendWLTran");
				return Wireless_Transactions.PostPendWirelessTran(uow, prod, storeCode, clerkId, pin, new Random().Next(1, 999999).ToString());
			}
			finally
			{
				uow.close();
			}
		}
		public static IWireless_Transactions[] GetPendingTransByStore(IMap imap, string storeCode)
		{
			UOW uow = null;			

			try
			{
				uow = new UOW(imap, "WirelessTranSvc.GetPendingTransByStore");			
				return Wireless_Transactions.GetPendingTransByStore(uow, storeCode);
			}
			finally
			{
				uow.close();
			}
		}		
	}
}