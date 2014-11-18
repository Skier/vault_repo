using System;
using System.Xml;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class PreSolutionsGateway : IWebSvcProvider
	{
		#region Const
		string provider;
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
		public PreSolutionsGateway(string provider)
		{
			this.provider = provider;
		}
		#endregion

		#region Methods
		public IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID, int areaCode, int prefix)
		{
			throw new ApplicationException("PreSolutionsGateway.GetProducts() is not implemented");
		}
		public IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID)
		{
			throw new ApplicationException("PreSolutionsGateway.GetProducts() is not implemented");
		}
		public bool IsActive(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			throw new ApplicationException("PreSolutionsGateway.IsActive() is not implemented");
		}		
		public bool IsDateValid(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			throw new ApplicationException("PreSolutionsGateway.IsActive() is not implemented");

		}
		public ISvcPlanDataResp GetAvailableBalance(string phone, string esn)
		{
			throw new ApplicationException("GetAvailableBalance method is not implemented in PreSolutionsGateway");
		}
		public IReceipt Send(IUOW uow, IUser user, string action, IDomObj[] objects)
		{
			IWebSvcQueue logQue		= null;
			ICellPhoneInfo cellInfo	= null;

			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] is IWebSvcQueue)
				{
					logQue = (IWebSvcQueue)objects[i];
					logQue.WSProvider = provider;
				}

				if (objects[i] is ICellPhoneInfo)
					cellInfo = (ICellPhoneInfo)objects[i];
			}

			if (action == Const.ORDER_PRODUCT)
				return HealthCheck();
			
			throw new ArgumentException("PreSolutionsGateway: Uknown action '" + action + "'");
		}
		string GetPIN(IUOW uow, int prod)
		{
			return AOL_PINs.ReservePIN((UOW)uow, prod).PIN;
		}
		IReceipt HealthCheck()
		{
			//string s = "<?xml version='1.0' encoding='UTF-8' ?>";
			//s += "<message (PartnerDescription, DateTimeStamp)>
			//<!ATTLIST message  id     CDATA  #REQUIRED
			//                   name   (status.inquiry)  #REQUIRED
			//                   source CDATA  #REQUIRED
			// storenumber CDATA #REQUIRED
			//                   version (1, 2,3,4,5) “1”  >
			//<!ELEMENT PartnerDescription (#PCDATA)>
			//<!ELEMENT DateTimeStamp (#PCDATA)>

			return null;

		}

		public void ProcessQueue(IWebSvcQueue entry)
		{
			if (entry == null)
				throw new ArgumentNullException("Web queue entry is null");
			
			DoEntry(entry.WebMethod.Trim().ToLower(), entry);
		}	
		#endregion

		#region Implementation

//		public void PostXml(string url, string xml, Encoding enc) 
//		{ 
//		    byte[] bytes = enc.GetBytes(xml); 
//		    HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url); 
//		    request.Method = "POST"; 
//		    request.ContentLength = bytes.Length; 
//		    request.ContentType = "text/xml"; 
//		    using (Stream requestStream = request.GetRequestStream()) 
//		    { 
//		        requestStream.Write(bytes, 0, bytes.Length); 
//		    } 
//		
//		
//		    using (HttpWebResponse response = 
//		        (HttpWebResponse) request.GetResponse()) 
//		    { 
//		        if (response.StatusCode != HttpStatusCode.OK) 
//		        { 
//		           string message = String.Format( 
//		               "POST failed. Received HTTP {0}", 
//		               response.StatusCode); 
//		           throw new ApplicationException(message); 
//		        } 
//		    } 
//		
//		
//		
//		} 

		static void DoEntry(string method, IWebSvcQueue entry)
		{
			
		}		
		#endregion
	}
}
