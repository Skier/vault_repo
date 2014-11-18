using System;
using System.Xml;
using System.Collections;

using DPI.Interfaces;
 
namespace DPI.Components
{
	public class PreSolutionsWSGateway  : IWebSvcProvider
	{
	#region Const
		string provider;
		const string url = "http://66.0.125.68:8080/PreSolutions/Purchase";
	#endregion		

	#region Properties
		public string Provider 
		{
			get { return provider;  }
			set { provider = value; }
		}
		
		public	bool IsWirelessXactPosted { get { return false; }}
	#endregion

	#region Constructors
		public PreSolutionsWSGateway(string provider)
		{
			this.provider = provider;
		}
	#endregion

	#region Methods
		public IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID, int areaCode, int prefix)
		{
			throw new ApplicationException("PreSolutionsWSGateway.GetProducts() is not implemented");
		}
		public IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID)
		{
			throw new ApplicationException("PreSolutionsWSGateway.GetProducts() is not implemented");
		}
		public bool IsActive(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			throw new ApplicationException("PreSolutionsWSGateway.IsActive() is not implemented");			
		}		
		public bool IsDateValid(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			throw new ApplicationException("PreSolutionsWSGateway.IsDateValid() is not implemented");
		}
		public ISvcPlanDataResp GetAvailableBalance(string phone, string esn)
		{
			throw new ApplicationException("GetAvailableBalance method is not implemented in PreSolutionsWSGateway");
		}
		public IReceipt Send(IUOW uow, IUser user, string action, IDomObj[] objects)
		{
			IWebSvcQueue logQue		= null;
			IWireless_Products wp = null;

			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] is IWebSvcQueue)
				{
					logQue = (IWebSvcQueue)objects[i];
					logQue.WSProvider = provider;
				}

				if (objects[i] is IWireless_Products)
					wp = (IWireless_Products)objects[i];
			}

			if (action == Const.ORDER_PRODUCT)
				return GetPinReceipt(uow, user, wp, logQue);
			
			throw new ArgumentException("PreSolutionsWSGateway: Uknown action '" + action + "'");
		}
		string GetPIN(IUOW uow, int prod)
		{
			return AOL_PINs.ReservePIN((UOW)uow, prod).PIN;
		}
		IPinReceipt GetPinReceipt(IUOW uow, IUser user, IWireless_Products wp, IWebSvcQueue logQue)
		{
			try
			{
				string xmlReq = new XMLPurchaseReq(wp.Soc, StoreLocation.find((UOW)uow, user.LoginStoreCode).StoreNumber).ToString();
			
				string resp = XMLUtility.GetHttpResponse(url, xmlReq);
			
				return null;
			}
			catch (Exception ex)
			{
				string s = ex.Message;
				return null;
			}
		}
		public void ProcessQueue(IWebSvcQueue entry)
		{
			if (entry == null)
				throw new ArgumentNullException("Web queue entry is null");

			DoEntry(entry.WebMethod.Trim().ToLower(), entry);
		}	
	#endregion

	#region Implementation

		static void DoEntry(string method, IWebSvcQueue entry)
		{
//			string item = null;
//
//			XmlNode xNode = new PaceWS().GeneratePINs(item, Acct, 1000.ToString(), Acct, PW); 
//			
//			//logQue.InitRespXml = xNode.InnerXml;
//			SimpleResp resp = new SimpleResp(xNode);
//			// set up new webservices queue entry with GetPins as method, ite
//			
//			//return rct;
//			//throw new ApplicationException("InfinityMobileWSGateway.DoEntry() is not implemented");
		}
		

	#endregion
	}
}