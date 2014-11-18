using System;
using System.Xml; 

using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class ColdFusionWSGateway : IWebSvcProvider
	{
		string provider;

		public string Provider 
		{
			get { return provider;  }
			set { provider = value; }
		}
		public	bool IsWirelessXactPosted { get { return true; }}
		public	bool IsActive(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			return false;
		}
		public bool IsDateValid(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			return false;
		}
		public ColdFusionWSGateway(string provider)
		{
			this.provider = provider;
		}
		public void ProcessQueue(IWebSvcQueue entry)
		{
			throw new ApplicationException("ProcessQueue method is not implemented in ColdFusionWSGateway");
		}
		public IReceipt Send(IUOW uow, IUser user, string action, IDomObj[] objects)
		{
			IPayInfo payInfo      = null;
			IWireless_Products wp = null;
			IWebSvcQueue logQue   = null;
	
			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] is IPayInfo) 
					payInfo = (IPayInfo)objects[i];

				if (objects[i] is IWireless_Products)
					wp = (IWireless_Products)objects[i];

				if (objects[i] is IWebSvcQueue)
					logQue = (IWebSvcQueue)objects[i];
			}

			if (action == Const.ORDER_PRODUCT)
				return  Order_Product(uow, user, payInfo, wp, logQue);
			
			if (action == Const.ORDER_PIN)
				return  Order_Product(uow, user, payInfo, wp, logQue);
			
			throw new ArgumentException("ColdFusionProxy: Uknown action '" + action + "'");
		}
		IReceipt Order_Product(IUOW uow, IUser user, IPayInfo payInfo, IWireless_Products wp, IWebSvcQueue logQue)
		{
			XmlDocument doc = new XmlDocument();

			string trNumber = new Random().Next(1, 100000).ToString();

			doc.LoadXml(new WirelessProxy().order_product(Const.WS_USERNAME, Const.WS_PASSWORD, user.LoginStoreCode, 
				user.ClerkId, wp.Wireless_product_id.ToString(), trNumber));
				
			IReceipt rcpt = new PinReceipt(doc, wp);				
				
			//logQue.BusObjId = payInfo.Id.ToString();
			logQue.Xml = doc.InnerXml;
				
			return rcpt; 
		}
		public IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID)
		{
			throw new ApplicationException("Products must be accessed directly instead of through ColdFusion");
		}
		public IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID, int areaCode, int prefix)
		{
			throw new ApplicationException("This method is not implemented in ColdFusionWSGateway.GetProducts");
		}
		public ISvcPlanDataResp GetAvailableBalance(string phone, string esn)
		{
			throw new ApplicationException("GetAvailableBalance method is not implemented in ColdFusionWSGateway");
		}
	}
}