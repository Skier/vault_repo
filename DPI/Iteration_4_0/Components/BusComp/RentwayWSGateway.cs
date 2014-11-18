using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Xml;

using DPI.Interfaces;
 
namespace DPI.Components
{
	public class RentwayWSGateway : IPymtPostProvider
	{
	#region Const
		const string Provider			= "Rentway";
		const string PostDPIPymtReq		= "PostDPIPaymentRequest";
		const string RevDPIPymtReq		= "reverseDPIPaymentRequest";
		
	#endregion		

	#region URLs
		static string RWReqXsdURL				{ get { return System.Configuration.ConfigurationSettings.AppSettings["RWReqXsdURL"];  }}
		static string RWRespXsdURL				{ get { return System.Configuration.ConfigurationSettings.AppSettings["RWRespXsdURL"]; }}
		static string PostDPIPymtReqXsd			{ get { return RWReqXsdURL  + "postDPIPaymentRequest.xsd";				   }}
		static string RevDPIPymtReqXsd			{ get { return RWReqXsdURL  + "reverseDPIPaymentRequest.xsd";			   }}
		static string RetDailyDPITranReqXsd		{ get { return RWReqXsdURL  + "retrieveDailyDPITransactionsRequest.xsd"; }}
		static string PostDPIPymtRespXsd		{ get { return RWRespXsdURL + "postDPIPaymentResponse.xsd"; }}
		static string RevDPIPymtRespXsd			{ get { return RWRespXsdURL + "reverseDPIPaymentResponse.xsd"; }}
		static string RetDailyDPITranRespXsd	{ get { return RWRespXsdURL + "retrieveDailyDPITransactionsResponse.xsd"; }}	
	#endregion
	
	#region Methods
		public void PostReversal(IUOW uow, IUser user, int tranId)
		{
			IPayInfo payInfo = GetPayInfo(uow, tranId);
			ValidatePresetPymt(user, payInfo);
			IWebSvcQueue wq = SetupWQ((UOW)uow, payInfo, RevDPIPymtReq);
			wq.Xml = new ReverseDPIPaymentRequest(new RWPostArgs((UOW)uow, user, payInfo, "")).ToString();
		}
		public void PostPymt(IUOW uow, IUser user, IPayInfo payInfo, string receitId)
		{
			ValidatePresetPymt(user, payInfo);
			IWebSvcQueue wq = SetupWQ((UOW)uow, payInfo, PostDPIPymtReq);
			wq.Xml = new PostDPIPaymentRequest(new RWPostArgs((UOW)uow, user, payInfo, receitId)).ToString();
		}
		
	#endregion

	#region Implementations
		void ValidatePresetPymt(IUser user, IPayInfo payInfo)
		{
			if (user == null)
				throw new ApplicationException("User is required");

			if (user.Token == null)
				throw new ApplicationException("Token is required");
			
			if (user.Token.Trim().Length == 0)
				throw new ApplicationException("Token is required");

			if (payInfo == null)
				throw new ApplicationException("Payment is required");

			if (payInfo.ParDemand == null)
				throw new ApplicationException("Demand is required");

			if (payInfo.ParDemand.ConsumerAgent == null)
				throw new ApplicationException("Clerk is required");
	
			if (payInfo.ParDemand.StoreCode.Trim().Length == 0)
				throw new ApplicationException("Store Code is required");
		}
		static IWebSvcQueue SetupWQ(UOW uow, IPayInfo payInfo, string method)
		{
			IWebSvcQueue wq = new WebSvcQueue(uow);

			wq.Dom			  = payInfo;
			wq.QueType        = WebSvcQueueType.Post.ToString();
			wq.WSProvider	  = Provider;
			wq.WebMethod      = method;
			wq.StoreCode      = payInfo.ParDemand.StoreCode;
			wq.ClerkId        = payInfo.ParDemand.ConsumerAgent;
			wq.BusObject      = payInfo.ToString();
			wq.BusObjId       = payInfo.Id.ToString();
			wq.Status         = WebSvcQueueStatus.Open.ToString();
			payInfo.Status	  = PaymentStatus.Paid.ToString();
			payInfo.ParDemand.Status = DemandStatus.Submited.ToString();
			
			return wq;
		}
		public static void ProcessQueue(IWebSvcQueue entry)
		{
			if (entry == null)
				throw new ArgumentNullException("Web queue entry is null");

			if (entry.QueType.Trim().ToLower() == WebSvcQueueType.Reversal.ToString().Trim().ToLower())
				DoEntry(entry.ReversalMethod, entry);
			
			if (entry.QueType.Trim().ToLower() == WebSvcQueueType.Post.ToString().Trim().ToLower())
				DoEntry(entry.WebMethod, entry);
		}	
		static void DoEntry(string method, IWebSvcQueue entry)
		{	
			switch (method.Trim().ToLower())
			{
				case "postdpipaymentrequest" : 
				{
					PostPymntReq(entry);
					return;
				}
				case "reverseDPIPaymentRequest":
				{
					ReversePymntReq(entry);
					return;
				}
				default :
					throw new ArgumentException("Unknown web method: " + method);
			}
		}

		static void PostPymntReq(IWebSvcQueue entry)
		{
			
			if ((entry.Xml == null) || entry.Xml.Trim().Length == 0)
			{
				ReportErr("RentwayWSGateway.PostPymntReq", entry, "Xml not found");
				return;
			}
			if (!VerifyXsd(entry))
				return;
			
			object result = new RentwayPymtWebSvc().postDPIPayment(entry.Xml);

			UpdateWebQue(entry, result, WebSvcQueueStatus.Completed);			
		}
		static void ReversePymntReq(IWebSvcQueue entry)
		{
			if ((entry.Xml == null) || entry.Xml.Trim().Length == 0)
			{
				ReportErr("RentwayWSGateway.ReversePymntReq", entry, "Xml not found");
				return;
			}
			if (!VerifyXsd(entry))
				return;
			
			object result = new RentwayPymtWebSvc().reverseDPIPayment(entry.Xml);

			UpdateWebQue(entry, result, WebSvcQueueStatus.Completed);			
		}
		static void ReportErr(string method, IWebSvcQueue entry, string message)
		{
			DPI_Err_Log.AddLogEntry(method, "N/A",
				"Entry id = " + entry.Id.ToString() + "Message: " + message);

			entry.Status = WebSvcQueueStatus.Error.ToString();
		}
		static void UpdateWebQue(IWebSvcQueue entry, object result, WebSvcQueueStatus status)
		{
			entry.Status = status.ToString();

			if (entry.WebMethod == PostDPIPymtReq)
			{
				if(new PostDPIPaymentResponse(ConvertToXml(result.ToString())).Error != null)
					entry.Status = WebSvcQueueStatus.Failed.ToString();
			}
			if (entry.WebMethod == RevDPIPymtReq)
			{
				if(new ReverseDPIPaymentResponse(ConvertToXml(result.ToString())).Error != null)
					entry.Status = WebSvcQueueStatus.Failed.ToString();
			}

			entry.LastRespXml = result.ToString();
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
		static IPayInfo GetPayInfo(IUOW uow, int tranId)
		{
			IVerifone_Transaction vt = Verifone_Transaction.find((UOW)uow, tranId);//tranId is verifone tran Id 
			
			IDemand[] dmds = Demand.GetByBillPayer((UOW)uow, vt.AccNumber);

			if (dmds == null)
				throw new ArgumentException("Demand not found for BillPayer:" + vt.AccNumber);
			
			return GetOnePayInfo(uow, vt, dmds);
		}
		static PayInfo GetOnePayInfo(IUOW uow, IVerifone_Transaction vt, IDemand[] dmds)
		{
			PayInfo[] payInfos = null;

			if (dmds.Length == 1)
				return PayInfo.getDmdPayInfo((UOW)uow, dmds[0].Id)[0];

			for (int i = 0; i < dmds.Length; i++)
			{
				payInfos = PayInfo.getDmdPayInfo((UOW)uow, dmds[i].Id);
				for (int j = 0; j < payInfos.Length; j++) 
				{
					if (payInfos[j].VFConf.Trim() == vt.TrConfirm.Trim())
						return payInfos[j];
				}				
			}

			return payInfos[0];
		}
		static bool VerifyXsd(IWebSvcQueue entry)
		{
			int i = new Random().Next(1, 20);

			if (i != 10)
				return true;

			XmlNode xDoc = ConvertToXml(entry.Xml);
			
			string url = "";
			
			if (entry.WebMethod == PostDPIPymtReq)
				url = PostDPIPymtReqXsd;
			
			if (entry.WebMethod == RevDPIPymtReq)
				url = RevDPIPymtReqXsd;
			
			if (!XMLUtility.ValidateXML(xDoc,  url))
			{
				ReportErr("RentwayWSGateway.VerifyXsd", entry, "Failed to validate against the Url:" + url);
				return false;
			}

			return true;
		}
	#endregion
	}
}