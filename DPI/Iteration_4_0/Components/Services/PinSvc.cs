using System;
using System.Xml;
using System.Collections;

using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{	
	public class PinSvc : ISvcProvider
	{
		const string provider =  "PinSvc";

	#region Get Vendors
		public static IPinVendor[] GetWirelessVendors(IMap imap)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetWirelessVendors");
				return PinVendor.GetWirelessVendors(uow);
			}
			finally
			{
				uow.close();
			}
		}		
		public static IPinVendor[] GetWirelessVendors(IMap imap, IUser user)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetWirelessVendors");
				return AddBlank(PinVendor.GetWirelessVendors(uow, Vendors.GetVendorsByStore(uow, user.LoginStoreCode)));
			}
			finally
			{
				uow.close();
			}
		}
		public static IPinVendor[] GetInternetVendors(IMap imap, IUser user)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetInternetVendors");
				return AddBlank(PinVendor.GetInternetVendors(uow, Vendors.GetVendorsByStore(uow, user.LoginStoreCode)));
			}
			finally
			{
				uow.close();
			}
		}
		public static IPinVendor[] GetDpiWLVendorsByProdCategory(IMap imap, IUser user, ProdCategory prodCategory)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetInternetVendors");
				return PinVendor.GetDpiWLVendorsByProdCategory(uow, Vendors.GetVendorsByStore(uow, user.LoginStoreCode), prodCategory);
			}
			finally
			{
				uow.close();
			}
		}
		public static IPinVendor[] GetDebitCardVendors(IMap imap, IUser user)
		{
			UOW uow = null;
			IWebSvcQueue webQue = null;
			XmlDocument doc = new XmlDocument();				
			
			try 
			{
				uow = new UOW(imap, "PinSvc.GetDebitCardVendors");

				doc.LoadXml(new WirelessProxy().request_vendors(Const.WS_USERNAME, Const.WS_PASSWORD, 
					user.LoginStoreCode, user.ClerkId));

				webQue = WebServSvc.SetupEntry(imap, null, user, "PinSvc.GetDebitCardVendors");
				webQue.Xml = doc.InnerXml;

				return PinVendor.GetDebitCardVendors(uow, doc);
			}
			catch (Exception e)
			{
				webQue.Status = WebSvcQueueStatus.Error.ToString();
				throw e;
			}
			finally
			{
				WebServSvc.SaveEntry(webQue);
				uow.close();
			}
		}
		
		public static IVendors GetVendor(IMap imap, int wirelessProdId)
		{
			UOW uow = null; 

			try
			{
				uow = new UOW(imap, "PinSvc.GetVendor");
			    return Vendors.find(uow, Wireless_Products.find(uow, wirelessProdId).Vendor_id);		
			}
			finally
			{
				uow.close();
			}
		}
		public static IVendors[] GetVendorsByStore(IMap imap, string storeCode)
		{
			UOW uow = null; 

			try
			{
				uow = new UOW(imap, "PinSvc.GetVendorsByStore");
				return Vendors.GetVendorsByStore(uow, storeCode);		
			}
			finally
			{
				uow.close();
			}
		}
	#endregion

	#region Get Products
		public static IWireless_Products FindProduct(IMap imap, int prod)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "PinSvc.FindProduct");
				return Wireless_Products.find(uow, prod);
			}
			finally
			{
				uow.close();
			}
		}
		public static IPinProduct[] GetProducts(IMap imap, IUser user, int vendorID, int areaCode, int prefix)
		{
			UOW uow = null;
			IWebSvcQueue webQue = null;

			try
			{
				uow = new UOW(imap, "PinSvc.GetProducts");
				webQue = WebServSvc.SetupEntry(imap, null, user, "PinSvc.GetProducts");
				
				return AddBlank(WSProviderFactory.GetProvider(uow, vendorID)
										.GetProducts(uow, user, webQue, vendorID, areaCode, prefix));	
			}
			catch (Exception e)
			{
				webQue.Status = WebSvcQueueStatus.Error.ToString();
				throw e;
			}
			finally
			{
				WebServSvc.SaveEntry(webQue);
				uow.close();
			}
		}
		public static IPinProduct[] GetProducts(IMap imap, IUser user, int vendorID)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetProducts");

				return AddBlank(
					PinProduct.GetPinProducts(
						Wireless_Products.GetAllByVendor(uow, vendorID, user.LoginStoreCode), vendorID));	
			}
			finally
			{
				uow.close();
			}
		}
		public static string GetProducts(IMap imap, IUser user, string prodCategory)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetProducts");

				return PinProduct.GetAllProducts(uow, user.LoginStoreCode, prodCategory);
			}
			finally
			{
				uow.close();
			}
		}		
		public static IPinProduct GetProduct(IMap imap, int prodId)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetProduct");

				IWireless_Products wp = Wireless_Products.find(uow, prodId);

				return new PinProduct(prodId, wp.Product_name, wp.Price, wp.Expiration, wp.ReqItems);				
			}
			finally
			{
				uow.close();
			}
		}
		public static IPinProduct[] GetWirelessProds(IMap imap, IUser user, int vendorID)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetWirelessProds");

				return AddBlank(
					PinProduct.GetPinProducts(
							Wireless_Products.GetAllByVendor(uow, vendorID, user.LoginStoreCode), vendorID));	
				}
			finally
			{
				uow.close();
			}
		}
		public static IWireless_Products[] GetWirelessProds(IMap imap, int vendorID, string storeCode)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetWirelessProds");

				return Wireless_Products.GetAllByVendor(uow, vendorID, storeCode);	
			}
			finally
			{
				uow.close();
			}
		}
	#endregion

	#region Orders and Activations

		public static IPinReceipt OrderProduct(IMap imap, IUser user, IWebSvcQueue logQue, IWireless_Products wp, string trNumber)
		{ 			
			UOW uow = null; 
			
			try
			{
				uow = new UOW(imap, "PinSvc.OrderProduct");
				
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				IPinReceipt rcpt = Order(uow, user, logQue, null, wp, null, trNumber, null);
				PostWirelessTran(uow, user, wp, null, rcpt);

				uow.commit();
				logQue.Status = WebSvcQueueStatus.Completed.ToString();
				return rcpt;
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}
		}
		

		public static IPinReceipt OrderProduct(IMap imap, IUser user, IPayInfo payInfo, IPinProduct pinProduct, IDemand dmd, string trNumber)
		{
			 return OrderProduct(imap, user, payInfo, pinProduct, dmd, trNumber, null); 
		}
		public static IPinReceipt OrderProduct(IMap imap, IUser user, IPayInfo payInfo, IPinProduct pinProduct, IDemand dmd, string trNumber, ICellPhoneInfo cell)
		{ 			
			UOW uow = null; 
			IWebSvcQueue logQue = null;

			try
			{
				uow = new UOW(imap, "PinSvc.OrderProduct");
				
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "PinSvc.OrderProduct");
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				IWireless_Products wp = Wireless_Products.find(uow, pinProduct.Product_Id);
				IPinReceipt rcpt = Order(uow, user, logQue, payInfo, wp, dmd, trNumber, cell);
				PostWirelessTran(uow, user, wp, payInfo, rcpt);

				uow.commit();
				logQue.Status = WebSvcQueueStatus.Completed.ToString();
				return rcpt;
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}
		}
		public static IPinReceipt Order(UOW uow, IUser user, IWebSvcQueue logQue, IPayInfo payInfo, IWireless_Products wp, IDemand dmd, string trNumber, ICellPhoneInfo cell)
		{
			if (IsWebService(uow, wp.Vendor_id))
				return OrderViaWebServ(uow, user, logQue, payInfo, wp, dmd, trNumber, cell);

			return  OrderDirect(uow, wp);
		}
		public static IPinReceipt OrderPin(IMap imap, IUser user, IWebSvcQueue logQue, IPayInfo payInfo, IWireless_Products wp, IDemand dmd, string trNumber, ICellPhoneInfo cell)
		{
			UOW uow = null; 
			
			try
			{
				uow = new UOW(imap, "PinSvc.OrderPin");
				if (IsWebService(uow, wp.Vendor_id))
					return OrderPinViaWebServ(uow, user, logQue, payInfo, wp, dmd, trNumber, cell);

				return  OrderDirect(uow, wp);				
			}
			finally
			{
				uow.close();
			}			
		}
		static IPinReceipt OrderDirect(UOW uow, IWireless_Products wp)
		{
			return new PinReceipt(GetPins(uow, wp), wp.CommissionAmt, wp.Receipt_text);
		}
		static IPinReceipt OrderViaWebServ(UOW uow, IUser user, IWebSvcQueue logQue, IPayInfo payInfo, IWireless_Products wp, IDemand dmd, string trNumber,	ICellPhoneInfo cell)
		{
			return (IPinReceipt)WSProviderFactory.GetProvider(uow, wp)
  				.Send(uow, user, Const.ORDER_PRODUCT, new IDomObj[] 
					{payInfo, (IDomObj)wp, (IDomObj)logQue, dmd, cell });
		}
		static IPinReceipt OrderPinViaWebServ(UOW uow, IUser user, IWebSvcQueue logQue, IPayInfo payInfo, IWireless_Products wp, IDemand dmd, string trNumber,	ICellPhoneInfo cell)
		{
			return (IPinReceipt)WSProviderFactory.GetProvider(uow, wp)
				.Send(uow, user, Const.ORDER_PIN, new IDomObj[] 
					{payInfo, (IDomObj)wp, (IDomObj)logQue, dmd, cell });
		}
		static bool IsWebService(UOW uow, int vendor)
		{
			IVendors vend = Vendors.find(uow, vendor);

			if (vend.DefaultWSProvider == null)
				return false;

			if (vend.DefaultWSProvider.Trim().Length == 0)
				return false;

			return true;
		}
		public static ICellPhoneReceipt ActivatePhone(IMap imap, IUser user, IPayInfo payInfo, ICellPhoneInfo cell, IPinProduct prod)
		{ 			
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			IWireless_Transactions tran = null;

			try
			{
				uow = new UOW(imap, "PinSvc.ActivatePhone()");
				
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "PinSvc.Activate");
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				cell.Pin = AOL_PINs.ReservePIN(uow, prod.Product_Id).PIN;
				
				IWireless_Products wp = Wireless_Products.find(uow, prod.Product_Id); 	
			
				string trNum = new Random().Next(1, 99900).ToString();
				
				IReceipt rct = WSProviderFactory.GetProvider(uow, wp)
 					.Send(uow, user, Const.ACTIVATE_PHONE, new IDomObj[] { payInfo, (IDomObj)wp, (IDomObj)logQue, cell });

				rct.ConfNum = "00";
				
				logQue.Status 
					=  ((ICellPhoneReceipt)rct).Pass ? 
								WebSvcQueueStatus.Completed.ToString()
								: WebSvcQueueStatus.Failed.ToString();

				if (((ICellPhoneReceipt)rct).Pass)
				{
					tran = Wireless_Transactions.PostWirelessTran(uow, 
											prod.Product_Id, 
											user.LoginStoreCode, 
											user.ClerkId, 
											cell.Pin,
											trNum);
					rct.ConfNum = tran.TrConfirm.ToString();
					uow.commit();
				}				

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

		public static ICellPhoneReceipt Check_Activation(IMap imap, IUser user, IPayInfo payInfo, ICellPhoneInfo cell)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;
			try
			{
				uow = new UOW(imap, "PinSvc.CheckActivation()");
				
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "PinSvc.CheckActivation");
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				IReceipt rct = new InfinityMobileWSGateway(Const.INFINITY_MOBILE)
					.Send(uow, user, Const.CHECK_ACTIVATION, new IDomObj[] { (IDomObj)logQue, payInfo, cell });
				
				logQue.Status = WebSvcQueueStatus.Completed.ToString();
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
				uow = new UOW(imap, "PinSvc.Replentish()");
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "PinSvc.Replenish");
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				IReceipt rct = new InfinityMobileWSGateway(Const.INFINITY_MOBILE)
					.Send(uow, user, Const.REPLENISH_SERVICE_PLAN, new IDomObj[] { (IDomObj)logQue, payInfo, cell });
				
				logQue.Status = WebSvcQueueStatus.Completed.ToString();
				return (ICellPhoneReceipt)rct;
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}
		}
		public static string GetReceipt(IMap imap, int prod, bool isActivated)
		{
			UOW uow = null; 

			try
			{
				uow = new UOW(imap, "PinSvc.GetReceipt()");
				return Wireless_Products.find(uow, prod).GetReceipt(isActivated,  new DictionaryEntry[0]);
			}
			finally
			{
				uow.close();
			}
		}

		public static IPinReceipt OrderSatellite(IMap imap, IUser user, IPayInfo[] payInfos, IPinProduct pinProduct, IDemand dmd, string trNumber)
		{ 			
			UOW uow = null; 
			try
			{
				uow = new UOW(imap, "PinSvc.OrderSatellite");
				
				IWireless_Products wp = Wireless_Products.find(uow, pinProduct.Product_Id);
				IPinReceipt rcpt = OrderDirect(uow, wp);
				PostSatelliteWLTran(uow, user, wp, payInfos, rcpt);

				uow.commit();				
				return rcpt;
			}
			finally
			{
				uow.close();				
			}
		
		
		}
	#endregion	

	#region Misc 

		public void FireAway(string action, string xml)
		{
			switch (action.Trim().ToLower())
			{
				case "checkavapins" :
				{
					CheckPins(xml);
					break;
				}

				default :
				throw new ArgumentException("PinSvc: Uknown action '" + action + "'");
			}
		}

		public string Provider { get { return provider; }}
		public static bool IsActive(IMap imap, IUser user, IPinProduct pinProduct, string phoneNumber)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;

			try
			{
				uow = new UOW(imap, "PinSvc.IsActive");

				IWireless_Products prod = Wireless_Products.find(uow, pinProduct.Product_Id);

				if (! WSProviderFactory.HasWSProvider(uow, prod)) // no provider - no validation
					return true;

				logQue = WebServSvc.SetupEntry(imap, null, user, "PinSvc.IsActive");
				logQue.Status = WebSvcQueueStatus.Error.ToString();
				
				bool isAct = WSProviderFactory
							.GetProvider(uow, prod)
							.IsActive(uow, new CellPhoneInfo(phoneNumber), logQue);
				logQue.Status = WebSvcQueueStatus.Completed.ToString();

				return isAct;
			}
			finally
			{
				uow.close();
				if (logQue != null)
					WebServSvc.SaveEntry(logQue);
			}
		}
		
		public static bool IsDateValid(IMap imap, IUser user, IPinProduct pinProduct, string phoneNumber)
		{
			UOW uow = null; 
			IWebSvcQueue logQue = null;

			try
			{

				uow = new UOW(imap, "PinSvc.IsDateValid");

				logQue = WebServSvc.SetupEntry(imap, null, user, "PinSvc.IsDateValid");
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				
				bool isDateValid = WSProviderFactory
					.GetProvider(uow, Wireless_Products.find(uow, pinProduct.Product_Id))
					.IsDateValid(uow, new CellPhoneInfo(phoneNumber), logQue);
				logQue.Status = WebSvcQueueStatus.Completed.ToString();

				return isDateValid;
			}
			finally
			{
				uow.close();
				WebServSvc.SaveEntry(logQue);
			}
		}
		public static string ReservePin(IMap imap, IPinProduct prod)
		{
			UOW uow = null; 

			try
			{
				uow = new UOW(imap, "PinSvc.GetPin");
				
				AOL_PINs apin = AOL_PINs.ReservePIN(uow, prod.Product_Id);  // Commit is in stored proc

				return apin.PIN;
			}
			finally
			{
				uow.close();	
			}
		}
		public static string ReservePin(IMap imap, int prodId)
		{
			UOW uow = null; 

			try
			{
				uow = new UOW(imap, "PinSvc.ReservePin");
				
				AOL_PINs apin = AOL_PINs.ReservePIN(uow, prodId);  // Commit is in stored proc

				return apin.PIN;
			}
			finally
			{
				uow.close();	
			}
		}
		public static int GetAvaPinsCnt(IMap imap, int prod)
		{
			UOW uow = null; 

			try
			{
				uow = new UOW(imap, "PinSvc.GetAvaPinsCnt");
				
				//AOL_PINs apin = AOL_PINs.ReservePIN(uow, prod.Product_Id);  // Commit is in stored proc

				return 0;
				
			}
			finally
			{
				uow.close();	
			}
		}
		public static ICellPhoneInfo GetCellInfo()
		{
			return new CellPhoneInfo();
		}
		public static void PreSave(IMap imap)
		{
			UOW uow = null; 
		
			try
			{
				uow = new UOW(imap, "PinSvc.PreSave"); 
	
				uow.BeginTransaction(); 
				uow.commit();   // pre-saves Bus objects.
			}
			finally
			{
				uow.close();
			}
		}
		public static bool IsPhoneReq(IMap imap, IPinProduct prod)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.IsPhoneReq");

				if (prod == null)
					return false;

				if (prod.Product_Id == 0)
					return false;				
				
				return Wireless_Products.find(uow, prod.Product_Id).IsPhoneReq;
			}
			finally
			{
				uow.close();
			}
		}
		public static bool IsPerValidationReq(IMap imap, IPinProduct prod)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.IsPerValidationReq");

				if (prod == null)
					return false;

				if (prod.Product_Id == 0)
					return false;				
				
				return Wireless_Products.find(uow, prod.Product_Id).IsPerValidationReq;
			}
			finally
			{
				uow.close();
			}
		}
		public static string GetReceiptText(IMap imap, ICellPhoneInfo info, ICellPhoneReceipt rct)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetReceiptText");
				return Wireless_Products.find(uow, info.WireleesProduct).
					GetReceipt(rct.Pass, Combine(new DictionaryEntry[][] { info.Entries, rct.Entries }));
			}
			finally
			{
				uow.close();
			}
			
		}
		public static IWireless_Products[] GetWLProdBySocPrice(IMap imap, string storeCode, string soc, decimal price)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "PinSvc.GetWLProdBySocPrice");
				
				return Wireless_Products.GetBySocPrice(uow, storeCode, soc, price);					
			}
			finally
			{
				uow.close();
			}
		}
	#endregion

	#region Implementation
		static void PostWirelessTran(UOW uow, IUser user, IWireless_Products wp,  IPayInfo payInfo, IPinReceipt rcpt)
		{
			if (WSProviderFactory.HasWSProvider(uow, wp))
				if (WSProviderFactory.GetProvider(uow, wp).IsWirelessXactPosted)
				{
					LinkPayInfo(uow, payInfo, rcpt, wp.Wireless_product_id);
					return;
				}

			IWireless_Transactions tran = Wireless_Transactions.PostWirelessTran(
				uow, 
				wp.Wireless_product_id,
				user.LoginStoreCode, 
				user.ClerkId, 
				rcpt.Pin,
				new Random().Next(1, 99900).ToString());

			payInfo.Tran = tran as IPayInfoTran;
			rcpt.ConfNum = tran.TrConfirm.ToString();
		}
		static void PostSatelliteWLTran(UOW uow, IUser user, IWireless_Products wp,  IPayInfo[] payInfos, IPinReceipt rcpt)
		{
			string[] pins = rcpt.Pin.Split(',');

			if (pins.Length != int.Parse(wp.ReqItems))
				throw new ApplicationException("No. of Pin must match with no. of required items");
			
			for (int i = 0; i < pins.Length; i++)
			{

				IWireless_Transactions tran = Wireless_Transactions.PostWirelessTran(
					uow, 
					wp.Wireless_product_id,
					user.LoginStoreCode, 
					user.ClerkId, 
					pins[i],
					new Random().Next(1, 99900).ToString());

				payInfos[i + 1].Tran = tran as IPayInfoTran; //PayInfo has an extra entry
				rcpt.ConfNum = tran.TrConfirm.ToString();
			}
		}
		static void LinkPayInfo(UOW uow, IPayInfo payInfo, IPinReceipt rcpt, int prodId)
		{
			IWireless_Transactions xact 
				= Wireless_Transactions.Locate(uow, rcpt.Pin, prodId, rcpt.ConfNum, payInfo.ParDemand.StoreCode);

			
			payInfo.Tran = xact as IPayInfoTran;
		}
		static void CheckPins(string xml)
		{
			IMap imap = IMapFactory.getIMap();
			IPinVendor[] vendors = GetWirelessVendors(imap);
		
			int cnt = 0;
			for (int i = 0; i < vendors.Length; i++)
				if (cnt > GetAvaPinsCnt(imap, int.Parse(vendors[i].Id)))
				{
					// do something (Create WebSvcQueue);
				}
		}

		static bool Failed(IPinReceipt rcpt)
		{
			if (!(rcpt is ICellPhoneReceipt))
				return false;

			return !((ICellPhoneReceipt)rcpt).Pass;
		}
		static DictionaryEntry[] Combine(DictionaryEntry[][] arrays)
		{
			ArrayList ar = new ArrayList();

			for (int i = 0; i < arrays.Length; i++)
				ar.AddRange(arrays[i]);

			DictionaryEntry[] entries = new DictionaryEntry[ar.Count];
			ar.CopyTo(entries);

			return entries;
		}
		static IPinProduct[] AddBlank(IPinProduct[] prods)
		{
			PinProduct blank = new PinProduct();

			blank.Product_Name = " ";
			blank.Product_Id = 0;
			//blank.Upc = "";
			
			IPinProduct[] pps = null;			

			if ((prods == null) || (prods.Length == 0))
			{
				pps = new IPinProduct[1];
				pps[0] = blank;
				return pps;
			}
			
			pps = new IPinProduct[prods.Length + 1];
			Array.Copy(prods, 0, pps, 1, prods.Length); 
			pps[0] = blank;
			return pps;
		}
		static IPinVendor[] AddBlank(IPinVendor[] vendors)
		{
			IPinVendor blank = new PinVendor();

			blank.Name = " ";
			
			blank.Id = "0";
			
			IPinVendor[] vends = null;			

			if ((vendors == null) || (vendors.Length == 0))
			{
				vends = new IPinVendor[1];
				vends[0] = blank;
				return vends;
			}
			
			vends = new IPinVendor[vendors.Length + 1];
			Array.Copy(vendors, 0, vends, 1, vendors.Length); 
			vends[0] = blank;
			return vends;
		}
		static string GetPins(UOW uow, IWireless_Products wp)
		{
			if (wp.ReqItems == null || wp.ReqItems == "0" || wp.ReqItems == "1")
				return AOL_PINs.ReservePIN(uow, wp.Wireless_product_id).PIN;

			string pin = "";
			for (int i = 0; i < int.Parse(wp.ReqItems); i++)
				if (i == 0)
					pin = AOL_PINs.ReservePIN(uow, wp.Wireless_product_id).PIN;
				else
					pin += ", " + AOL_PINs.ReservePIN(uow, wp.Wireless_product_id).PIN;

			return pin;
		}
		
	#endregion
	}
}