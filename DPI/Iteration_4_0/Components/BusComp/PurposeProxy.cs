using System; 
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.ComponentModel;

using System.Web.Services;
using System.Xml;
using System.Xml.Schema;

using DPI.Interfaces;
using DPI.Components;

namespace DPI.Components
{
	public class PurposeProxy
	{
	#region URLs
		static string PurposeUrl       { get { return ApplSetting.GetPurposeXsdUrl(); }}
		
		static string urlEnrollReq     { get { return PurposeUrl + @"Enroll_Request.xsd";  }}
		static string urlEnrollResp	   { get { return PurposeUrl + @"Enroll_Response.xsd"; }}
		static string urlRefillReq	   { get { return PurposeUrl + @"Reload_Request.xsd";  }}
		static string urlRefillResp	   { get { return PurposeUrl + @"Reload_Response.xsd"; }}
	
		static string urlEnrollRevReq  { get { return PurposeUrl + @"Enroll_Reversal_Request.xsd";  }}
		static string urlEnrollRevResp { get { return PurposeUrl + @"Enroll_Reversal_Response.xsd"; }}	
		static string urlReloadRevReq  { get { return PurposeUrl + @"Reload_Reversal_Request.xsd";  }}
		static string urlReloadRevResp { get { return PurposeUrl + @"Reload_Reversal_Response.xsd"; }}	
	#endregion

	#region Post Methods
		public static PurposeDCResponse ApplyForNewCard(UOW uow, ICardApp app, IPayInfo payInfo, out XElement req)
		{
			ValidateNewCard(uow, app, payInfo); 
			req = new XmlEleEnrollReq(payInfo, app);
			
			if (!XMLUtility.ValidateXML(req.ToXmlDoc(), urlEnrollReq))
				return  PurposeDCResponse.XmlErrorResp("Before");

			XmlNode node = new PurposeTransactionService().EnrollRequest(req.ToXmlDoc(), 20000);

			if (!XMLUtility.ValidateXML(node, urlEnrollResp))
				return  PurposeDCResponse.XmlErrorResp("After");
			
			return new PurposeDCResponse(node); 
		}
		public static void ApplyForNewCard(IWebSvcQueue entry)
		{
			XmlDocument xDoc = ConvertToXml(entry.Xml);
			if (xDoc.OuterXml.Length == 0)
			{
				ReportErr("PurposeProxy.ApplyForEnroll", entry);
				return;
			}
			XMLUtility.ValidateXML(xDoc,  urlEnrollReq);
			XmlNode node = new PurposeTransactionService().EnrollRequest(xDoc, 20000);

			UpdateWebQue(entry, node, WebSvcQueueStatus.Completed);
			XMLUtility.ValidateXML(node, urlEnrollResp);
		}
		public static PurposeDCResponse ApplyForRefill(UOW uow, ICardApp app, IPayInfo payInfo, out XElement req)
		{
			ValidateRefill(uow, app, payInfo);
			req = new XmlEleReloadReq(payInfo, app);
			if (!XMLUtility.ValidateXML(req.ToXmlDoc(), urlRefillReq))
				return  PurposeDCResponse.XmlErrorResp("Before");
			
			XmlNode node = new PurposeTransactionService().ReloadRequest(req.ToXmlDoc(), 20000);

			if (!XMLUtility.ValidateXML(node, urlRefillResp))
				return  PurposeDCResponse.XmlErrorResp("After");

			return new PurposeDCResponse(node);
		}
		public static void ApplyForRefill(IWebSvcQueue entry)
		{
			XmlDocument xDoc = ConvertToXml(entry.Xml);
			if (xDoc.OuterXml.Length == 0)
			{
				ReportErr("PurposeProxy.ApplyForRefill", entry);
				return;
			}

			XMLUtility.ValidateXML(xDoc,  urlRefillReq);
			XmlNode node = new PurposeTransactionService().ReloadRequest(xDoc, 20000);

			UpdateWebQue(entry, node, WebSvcQueueStatus.Completed);
			XMLUtility.ValidateXML(node, urlRefillResp);
		}

	#endregion
	
	#region Reversal Methods
		public static void ApplyForEnrollReversal(IWebSvcQueue entry)
		{
			XmlDocument xDoc = ConvertToXml(entry.ReversalXml);
			if (xDoc.OuterXml.Length == 0)
			{
				ReportErr("PurposeProxy.ApplyForEnrollReversal", entry);
				return;
			}

			XMLUtility.ValidateXML(xDoc, urlEnrollRevReq);
			XmlNode node = new PurposeTransactionService().EnrollReversalRequest(xDoc, 20000);
			UpdateWebQue(entry, node, WebSvcQueueStatus.Reversed);
			XMLUtility.ValidateXML(node, urlEnrollRevResp);
		}
		public static void ApplyForReloadReversal(IWebSvcQueue entry)
		{
			XmlDocument xDoc = ConvertToXml(entry.ReversalXml);
			if (xDoc.OuterXml.Length == 0)
			{
				ReportErr("PurposeProxy.ApplyForEnrollReversal", entry);
				return;
			}

			XMLUtility.ValidateXML(xDoc, urlReloadRevReq);
			XmlNode node = new PurposeTransactionService().ReloadReversalRequest(xDoc, 20000);
			UpdateWebQue(entry, node, WebSvcQueueStatus.Reversed);
			XMLUtility.ValidateXML(node, urlReloadRevResp);
		}
	#endregion

	#region Implementation
		static void ReportErr(string method, IWebSvcQueue entry)
		{
			DPI_Err_Log.AddLogEntry(method, "N/A",
				"Entry id = " + entry.Id.ToString() + " does not contains reversal xml");

			entry.Status = WebSvcQueueStatus.Error.ToString();
		}
		static void UpdateWebQue(IWebSvcQueue entry, XmlNode node, WebSvcQueueStatus status)
		{
			PurposeDCResponse pr =  new PurposeDCResponse(node);	
			
			entry.LastRespXml = node.OuterXml;
			entry.LastReasonCode = pr.RespCode;	
			entry.Status = WebSvcQueueStatus.Failed.ToString();

			if (pr.RespCode == "00")
				entry.Status = status.ToString();
		}
	
		static void ValidateNewCard(UOW uow, ICardApp app, IPayInfo payInfo)
		{
			if (payInfo == null)
				throw new ApplicationException("Payment is required");

			if (payInfo.ParDemand == null)
				throw new ApplicationException("Demand is required");
	
			if (payInfo.ParDemand.ConsId == 0)
				throw new ApplicationException("Customer is required");

			if (CustInfo.find(uow, payInfo.ParDemand.ConsId) == null)
				throw new ApplicationException("Customer is required");

			if (app == null)
				throw new ApplicationException("Debit Card Application is required");
		}
		static void ValidateRefill(UOW uow, ICardApp app, IPayInfo payInfo)
		{	
			if (payInfo == null)
				throw new ApplicationException("Payment is required");
			
			if (payInfo.ParDemand == null)
				throw new ApplicationException("Demand is required");

			if (app == null)
				throw new ApplicationException("Debit Card Application is required");
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
	#endregion
	}
}