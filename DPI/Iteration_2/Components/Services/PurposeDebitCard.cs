//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Xml;
//
//using DPI.Interfaces;
// 
//namespace DPI.Components
//{
//	public class PurposeDebitCard : IDebCardProvider
//	{
//		const string Provider = "Purpose";
//
//		public  IDebitCardReceipt Enroll(IUOW uow, IPayInfo payInfo, ICardApp app, IUser user)
//		{
//			string method = "PurposeDebitCard.Enroll";
//			IWebSvcQueue wq = SetupWQ(user, payInfo, method);
//			XElement xReq = null;
//	
//			try
//			{
//				PurposeDCResponse resp = PurposeProxy.ApplyForNewCard((UOW)uow, app, payInfo, out xReq);
//				SetRespAttr(wq, resp, xReq);
//				IDebitCardReceipt rcpt = new DebitCardReceipt(payInfo.DmdId, payInfo.Id.ToString(), resp.RespCode == "00");
//
//				payInfo.Status = rcpt.IsApproved ? PaymentStatus.Paid.ToString()    : PaymentStatus.Cancelled.ToString();
//				payInfo.ParDemand.Status  = rcpt.IsApproved ? DemandStatus.Submited.ToString() : DemandStatus.Cancelled.ToString();
//
//				if (rcpt.IsApproved)
//				{
//					DebitCardTran.Enroll((UOW)uow, payInfo, app, user, resp.TranId);
//					return rcpt;
//				}
//
//				Reverse(wq, payInfo, xReq, null, "PurposeDebitCard.EnrollReversal");
//				return rcpt;
//			}
//			catch (Exception ex)
//			{
//				Reverse(wq, payInfo, xReq,  ex.Message, "PurposeDebitCard.EnrollReversal");
//				throw ex;
//			}
//			finally
//			{
//				WebServSvc.SaveEntry(wq);	
//			}
//		}
//		void SetRespAttr(IWebSvcQueue wq, PurposeDCResponse resp, XElement xReq)
//		{
//			wq.InitReasonCode = resp.RespCode;
//			wq.InitialMsg = resp.RespText;		
//			wq.InitRespXml = resp.Xnode;
//			if (xReq != null)
//				wq.Xml = xReq.ToString();
//		}
//		IWebSvcQueue SetupWQ(IUser user, IPayInfo payInfo, string method)
//		{
//			IWebSvcQueue wq = WebServSvc.GetEntry();
//
//			wq.WSProvider = Provider;
//			wq.QueType = WebSvcQueueType.Reversal.ToString();
//			wq.ClerkId = user.ClerkId;
//			wq.StoreCode = user.LoginStoreCode;
//			wq.WebMethod = method;
//			wq.Status = WebSvcQueueStatus.Completed.ToString();
//			wq.BusObject = payInfo.ToString();
//			wq.BusObjId = payInfo.Id.ToString();
//
//			return wq;
//		}
//		void Reverse(IWebSvcQueue wq, IPayInfo payInfo, XElement xReq, string message, string revMethodType)
//		{
//			wq.Status = WebSvcQueueStatus.Open.ToString();
//			if (xReq != null)
//			{
//				wq.Xml = xReq.ToString();
//				wq.ReversalXml = new XmlEleRevReq(xReq, payInfo).ToString();
//			}
//	
//			wq.ReversalMethod = revMethodType;
//			if (message != null)		
//				wq.InitialMsg += ", Exception: " + message;
//		}
//		public  IDebitCardReceipt Reload(IUOW uow, IPayInfo payInfo, ICardApp app, IUser user)
//		{
//			string method = "PurposeDebitCard.Reload";
//			IWebSvcQueue wq = SetupWQ(user, payInfo, method);
//			XElement xReq = null;
//	
//			try
//			{
//				PurposeDCResponse resp = PurposeProxy.ApplyForRefill((UOW)uow, app, payInfo, out xReq);
//				SetRespAttr(wq, resp, xReq);
//				IDebitCardReceipt rcpt = new DebitCardReceipt(payInfo.DmdId, payInfo.Id.ToString(), resp.RespCode == "00");
//
//				payInfo.Status = rcpt.IsApproved ? PaymentStatus.Paid.ToString()    : PaymentStatus.Cancelled.ToString();
//				payInfo.ParDemand.Status  = rcpt.IsApproved ? DemandStatus.Submited.ToString() : DemandStatus.Cancelled.ToString();
//
//				if (rcpt.IsApproved)
//				{
//					DebitCardTran.Refill((UOW)uow, payInfo, app, user, resp.TranId);
//					return rcpt;
//				}
//
//				Reverse(wq, payInfo, xReq, null, "PurposeDebitCard.ReloadReversal");
//				return rcpt;
//			}
//			catch (Exception ex)
//			{
//				Reverse(wq, payInfo, xReq,  ex.Message, "PurposeDebitCard.ReloadReversal");
//				throw ex;
//			}
//			finally
//			{
//				WebServSvc.SaveEntry(wq);	
//			}
//			//			PurposeDCResponse resp = PurposeProxy.ApplyForRefill((UOW)uow, app, payInfo);
//			//			IDebitCardReceipt rcpt = new DebitCardReceipt(payInfo.DmdId, payInfo.Id.ToString(), resp.RespCode == "00");
//			//
//			//			payInfo.Status = rcpt.IsApproved ? PaymentStatus.Paid.ToString()    : PaymentStatus.Cancelled.ToString();
//			//			payInfo.ParDemand.Status  = rcpt.IsApproved ? DemandStatus.Submited.ToString() : DemandStatus.Cancelled.ToString();
//			//
//			//			if (rcpt.IsApproved)
//			//				DebitCardTran.Refill((UOW)uow, payInfo, app, user, resp.Tran);
//			//
//			//			return rcpt;			
//		}
//	}
//}