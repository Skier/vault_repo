using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Xml;

using DPI.Interfaces;
 
namespace DPI.Components
{
	public class SlingShotWSGateway  : IWebSvcProvider
	{
	#region Const
		const string GET_A_CODE_XML		= "getacodexml";
		const string VOID_A_CODE_XML	= "voidacodexml";
		const string UPC_FOR_PHONE_XML	= "upcforphonexml";
		string provider;
	#endregion		

	#region Properties
		static string SlingShotWebSvcURL 		{ get { return System.Configuration.ConfigurationSettings.AppSettings["SlingShotWebSvcURL"];  }}
		static string GetACodeXmlRequestXsd		{ get { return SlingShotWebSvcURL + "GetACodeXmlRequest.xsd";     }}
		static string VoidACodeXmlRequestXsd	{ get { return SlingShotWebSvcURL + "VoidACodeXmlRequest.xsd";    }}
		static string UpcForPhoneXmlXsd			{ get { return SlingShotWebSvcURL + "UpcForPhoneXmlRequest.xsd";  }}
		static string DynamicPosaXmlResponseXsd	{ get { return SlingShotWebSvcURL + "DynamicPosaXmlResponse.xsd"; }}		
		public string Provider 
		{
			get { return provider;  }
			set { provider = value; }
		}
		public	bool IsWirelessXactPosted { get { return true; }}
	#endregion

	#region Constructors
		public SlingShotWSGateway(string provider)
		{
			this.provider = provider;
		}
	#endregion

	#region Methods
		public	bool IsActive(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			return false;
		}
		public bool IsDateValid(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			return false;
		}
		public ISvcPlanDataResp GetAvailableBalance(string phone, string esn)
		{
			throw new ApplicationException("GetAvailableBalance method is not implemented in SlingshotWSGateway");
		}
		public IReceipt Send(IUOW uow, IUser user, string action, IDomObj[] objects)
		{
			IPayInfo payInfo			= null;
			IWireless_Products wp		= null;
			IDemand dmd					= null;
			IWebSvcQueue logQue			= null;
			IWireless_Transactions wt	= null;
	
			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] is IPayInfo)
					payInfo = (IPayInfo)objects[i];

				if (objects[i] is IWireless_Products)
					wp = (IWireless_Products)objects[i];

				if (objects[i] is IDemand)
					dmd = (IDemand)objects[i];

				if (objects[i] is IWebSvcQueue)
					logQue = (IWebSvcQueue)objects[i];

				if (objects[i] is IWireless_Transactions)
					wt = (IWireless_Transactions)objects[i];
			}

			if (action == Const.ORDER_PRODUCT)
				return  GetACodeXml(uow, user, payInfo, dmd, wp, logQue);
			
			if (action  == Const.VOID_PRODUCT)
				return  VoidACode(uow, user, logQue, wt);
			
			throw new ArgumentException("SlingShotWSGateway: Uknown action '" + action + "'");
		}
		
		public void ProcessQueue(IWebSvcQueue entry)
		{
			if (entry == null)
				throw new ArgumentNullException("Web queue entry is null");

			if (entry.QueType.Trim().ToLower() == WebSvcQueueType.Reversal.ToString().Trim().ToLower())
				DoEntry(entry.ReversalMethod, entry);
			
			if (entry.QueType.Trim().ToLower() == WebSvcQueueType.Post.ToString().Trim().ToLower())
				DoEntry(entry.WebMethod, entry);
		}	
		public IPinReceipt GetACodeXml(IUOW uow, IUser user, IPayInfo payInfo, IDemand dmd, IWireless_Products wp, IWebSvcQueue logQue)
		{
			ValidateParams(user, payInfo, dmd, wp);
			XmlDocument req = new GetACodeXml(new SSPostArgs(user, payInfo, wp)).ToXmlDoc();
			logQue.Xml = req.InnerXml;

//			if (!VerifyXsd(logQue, req, GetACodeXmlRequestXsd))
//				return null;

			XmlNode xNode = new SlingShotPinProxy().GetACodeXml(req);
			logQue.InitRespXml = xNode.InnerXml;
			SlingShotResp resp = new SlingShotResp(xNode);
			
			if (!CheckResp(resp))
				return VoidReceipt(payInfo);
			
			payInfo.ConfNumber = resp.ManId.ToString();
			CreateWirelessTran(uow, user, payInfo, wp, resp.ACode, resp.ManId);
			return new PinReceipt(resp.ManId.ToString(), 
								  FormatSSPin(resp.ACode), 0m, wp.Receipt_text, DateTime.Now);
		}
		static IPinReceipt VoidReceipt(IPayInfo payInfo)
		{
			payInfo.TotalAmountDue	= 0m;
			payInfo.AmountTendered	= 0m;
			payInfo.TotalAmountPaid	= 0m;

			return new PinReceipt("0000", "0000", 0m, 
				"\n\n\n\t\t\t\t\t Unable to get PIN. Please call Agent Relations at 1-800-383-9956", 
				DateTime.Now);
		}
		static bool CheckResp(ISlingShotResp resp)
		{
			if (resp == null)
				return false;

			if (resp.Code > 0)
				return false;

			if (resp.Action.Trim().ToLower() == VOID_A_CODE_XML)
				return true;

			if (resp.Action.Trim().ToLower() == UPC_FOR_PHONE_XML)
				return true;

			if (resp.ACode == null)
				return false;

			if (resp.ACode.Trim().Length == 0)
				return false;

			if (resp.ManId == 0)
				return false;
			
			return true;
		}		
		public IReceipt VoidACode(IUOW uow, IUser user, IWebSvcQueue logQue, IWireless_Transactions wt)
		{
			ValidateUser(user);
			XmlDocument req  = new VoidACodeXml(new SSPostArgs(user, 
				wt.Wireless_Transaction_ID, wt.TrConfirm.ToString(), wt.Pin, 
				int.Parse(wt.Supplier_tran), WirelessTranStatus.Refund.ToString())).ToXmlDoc();
			
			logQue.ReversalXml = req.OuterXml;
			
//			if (!VerifyXsd(logQue, req, VoidACodeXmlRequestXsd))
//				return null;

			XmlNode xNode = new SlingShotPinProxy().VoidACodeXml(req);
			logQue.LastRespXml = xNode.OuterXml;
			SlingShotResp resp = new SlingShotResp(xNode);
			
			wt.Status = WirelessTranStatus.Voided.ToString();
			if (!CheckResp(resp))
			{
				logQue.Status = WebSvcQueueStatus.Failed.ToString();	
				wt.Status = WirelessTranStatus.Declined.ToString();
				return null;
			}
			logQue.Status = WebSvcQueueStatus.Reversed.ToString();

			return new Receipt(wt.Supplier_tran, wt.Wireless_Transaction_ID);
		}
		
		public static void VoidACode(IWebSvcQueue entry)
		{
			XmlDocument xDoc = ConvertToXml(entry.ReversalXml);
			if (xDoc.OuterXml.Length == 0)
			{
				ReportErr("VoidACodeXml", entry, "VoidACode failed");
				return;
			}
			
			XMLUtility.ValidateXML(xDoc, VoidACodeXmlRequestXsd);
			XmlNode node = new SlingShotPinProxy().VoidACodeXml(xDoc);
			UpdateWebQue(entry, node, WebSvcQueueStatus.Reversed);
		}
		static void DoEntry(string method, IWebSvcQueue entry)
		{	
			// update entries only
			switch (method.Trim().ToLower())
			{
				case Const.VOID_PRODUCT : 
				{
					VoidACode(entry);
					return;
				}
				default :
					throw new ArgumentException("Unknown web method: " + method);
			}
		}
		public IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID)
		{
			return null;
		}
		public IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue logQue, int vendorID, int areaCode, int prefix)
		{
			ValidateUser(user);
			XmlDocument req = new UpcForPhoneXml(new SSPostArgs(areaCode, prefix)).ToXmlDoc();
			logQue.Xml = req.InnerXml;

//			if (!VerifyXsd(logQue, req, UpcForPhoneXmlXsd))
//				return null;

			XmlNode xNode = new SlingShotPinProxy().UpcForPhoneXml(req);
			logQue.InitRespXml = xNode.InnerXml;
			SlingShotResp resp = new SlingShotResp(xNode);
			
			if (!CheckResp(resp))
				throw new ApplicationException("Error getting Slingshot products");
			
			PinProduct.AddProdId((UOW)uow, resp.PinProducts, vendorID, user.LoginStoreCode);

			logQue.Status = WebSvcQueueStatus.Completed.ToString();			
			return resp.PinProducts;
		}
	#endregion

	#region Implementations
		void CreateWirelessTran(IUOW uow, IUser user, IPayInfo payInfo, IWireless_Products wp, string aCode, int manId)
		{
			IWireless_Transactions wt = new Wireless_Transactions((UOW)uow);
			wt.Clerkid					= user.ClerkId;
			wt.PayDateTime				= DateTime.Now;
			wt.TrConfirm				= new Random().Next(1, 1000000);
			wt.Pin						= aCode;
			wt.StoreCode				= user.LoginStoreCode;
			wt.Tran_Amount				= payInfo.TotalAmountPaid;
			wt.Transaction_Method_ID	= (int)WirelessTranMethod.WebService;
			wt.Supplier_tran			= manId.ToString();
			wt.Wireless_product_ID		= wp.Wireless_product_id;
			payInfo.Tran				= (IPayInfoTran)wt;		
			wt.Commission = GetCommission(wp, payInfo.TotalAmountPaid);	
		}
		decimal GetCommission(IWireless_Products wp, decimal tranAmt)
		{
			if (wp.Product_commission_percent > 0)
				return decimal.Round(tranAmt * wp.Product_commission_percent / 100, 2);

			return decimal.Round(wp.Product_commission_flat, 2);
		}
		void ValidateParams(IUser user, IPayInfo payInfo, IDemand dmd)
		{
			ValidateUser(user);
			
			if (payInfo == null)
				throw new ApplicationException("Payment is required");

			if (dmd == null)
				throw new ApplicationException("Demand is required");
		}
		void ValidateParams(IUser user, IPayInfo payInfo, IDemand dmd, IWireless_Products wp)
		{
			ValidateUser(user);

			if (payInfo == null)
				throw new ApplicationException("Payment is required");

			if (dmd == null)
				throw new ApplicationException("Demand is required");
			
			if (wp == null)
				throw new ApplicationException("Wireless Product is required");
		}
		void ValidateUser(IUser user)
		{
			if (user == null)
				throw new ApplicationException("User is required");
		}
		static void ReportErr(string method, IWebSvcQueue entry, string message)
		{
			DPI_Err_Log.AddLogEntry(method, "N/A",
				"Entry id = " + entry.Id.ToString() + "Message: " + message);

			entry.Status = WebSvcQueueStatus.Error.ToString();
		}
		static void UpdateWebQue(IWebSvcQueue entry, XmlNode result, WebSvcQueueStatus status)
		{
			entry.Status = status.ToString();

			if(!CheckResp(new SlingShotResp(result)))
				entry.Status = WebSvcQueueStatus.Failed.ToString();				
			
			entry.LastRespXml = result.OuterXml;
		}
		static XmlDocument ConvertToXml(string msg)
		{
			if (msg == null)
				return new XmlDocument();

			if (msg.Trim().Length == 0)
				return new XmlDocument();
			
			XmlDocument xDoc = new XmlDocument();
			xDoc.LoadXml(msg);
			
			return xDoc;
		}
		static bool VerifyXsd(IWebSvcQueue que, XmlDocument xDoc, string url)
		{
			int i = new Random().Next(1, 20);

			if (i != 10)
				return true;

			if (!XMLUtility.ValidateXML(xDoc,  url))
			{
				ReportErr("SlingShot.VerifyXsd", que, "Failed to validate against the Url:" + url);
				return false;
			}

			return true;
		}
		static string FormatSSPin(string pin)
		{
			if (pin.Trim().Length != 15)
				return pin;

			return pin.Substring(0, 1) + "-" + pin.Substring(1, 4) + "-" + pin.Substring(5, 4) +
				"-" + pin.Substring(9, 6); 
		}
	#endregion
	}
}