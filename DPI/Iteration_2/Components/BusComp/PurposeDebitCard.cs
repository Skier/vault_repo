using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Xml;

using DPI.Interfaces;
 
namespace DPI.Components
{
	public class PurposeDebitCard : IDebCardProvider
	{
		const string Provider = "Purpose";

	#region Methods
		public IReceipt  Send(IUOW uow, IUser user, string action, IDomObj[] objects)
		{
			IPayInfo payInfo = null;
			ICardApp app = null;
			IWireless_Transactions tran = null;

			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] is IPayInfo)
					payInfo = (IPayInfo)objects[i];

				if (objects[i] is ICardApp)
					app = (ICardApp)objects[i];

				if (objects[i] is IWireless_Transactions)
					tran = (IWireless_Transactions)objects[i];
			}

			if (action == "Enroll")
				return Enroll(uow, payInfo, app, tran);

			if (action == "Reload")
				return Reload(uow, payInfo, app, tran);
			
			return null;
		}

		public IDebitCardReceipt Enroll(IUOW uow, IPayInfo payInfo, ICardApp app, IWireless_Transactions tran)
		{
			if (payInfo == null)
				throw new ArgumentNullException("PayInfo");

			string method = "PurposeDebitCard.Enroll";
			IWebSvcQueue wq = SetupWQ(payInfo, method);
			XElement xReq = null;
			PurposeDCResponse resp = null;

			try
			{
				resp = PurposeProxy.ApplyForNewCard((UOW)uow, app, payInfo, out xReq);
				SetRespAttr(wq, resp, xReq);

				IDebitCardReceipt rcpt = new DebitCardReceipt(payInfo.DmdId, payInfo.Id.ToString(), resp.RespCode == "00");
				payInfo.Status = rcpt.IsApproved ? PaymentStatus.Paid.ToString() : PaymentStatus.Cancelled.ToString();
				payInfo.ParDemand.Status  = rcpt.IsApproved ? DemandStatus.Submited.ToString() : DemandStatus.Cancelled.ToString();
				
				UpdateWirelessTran(rcpt, tran);

				if (rcpt.IsApproved)
				{
					DebitCardTran.Enroll((UOW)uow, payInfo, app, resp.TranId);
					return rcpt;
				}

				Reverse(wq, resp, payInfo, xReq, null, "PurposeDebitCard.EnrollReversal");
	
				return rcpt;
			}
			catch (Exception ex)
			{
				Reverse(wq, resp, payInfo, xReq,  ex.Message + ", Stack: " + ex.StackTrace, "PurposeDebitCard.EnrollReversal");
				throw ex;
			}
			finally
			{
				wq.SaveEntry();	
			}
		}	
		public IDebitCardReceipt Reload(IUOW uow, IPayInfo payInfo, ICardApp app, IWireless_Transactions tran)
		{
			string method = "PurposeDebitCard.Reload";
			IWebSvcQueue wq = SetupWQ(payInfo, method);
			XElement xReq = null;
			PurposeDCResponse resp = null;
			
			try
			{
				resp = PurposeProxy.ApplyForRefill((UOW)uow, app, payInfo, out xReq);
				SetRespAttr(wq, resp, xReq);
				IDebitCardReceipt rcpt = new DebitCardReceipt(payInfo.DmdId, payInfo.Id.ToString(), resp.RespCode == "00");

				payInfo.Status = rcpt.IsApproved ? PaymentStatus.Paid.ToString() : PaymentStatus.Cancelled.ToString();
				payInfo.ParDemand.Status  = rcpt.IsApproved ? DemandStatus.Submited.ToString() : DemandStatus.Cancelled.ToString();

				UpdateWirelessTran(rcpt, tran);
				
				if (rcpt.IsApproved)
				{
					DebitCardTran.Refill((UOW)uow, payInfo, app, resp.TranId);
					return rcpt;
				}

				Reverse(wq, resp, payInfo, xReq, null, "PurposeDebitCard.ReloadReversal");
				return rcpt;
			}
			catch (Exception ex)
			{
				Reverse(wq, resp, payInfo, xReq,  ex.Message + ", Stack: " + ex.StackTrace, "PurposeDebitCard.ReloadReversal");
				throw ex;
			}
			finally
			{
				wq.SaveEntry();	
			}
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
		
	#endregion

	#region Implementation
		void UpdateWirelessTran(IDebitCardReceipt rcpt, IWireless_Transactions tran)
		{
			if (tran == null)
				return;

			tran.Status = rcpt.IsApproved ? "Redeemed" : "Cancelled";
		}
		static void DoEntry(string method, IWebSvcQueue entry)
		{	
			switch (method.Trim().ToLower())
			{
				case "purposedebitcard.enrollreversal" : //PurposeDebitCard.EnrollReversal
				{
					PurposeProxy.ApplyForEnrollReversal(entry);
					return;
				}
				case "purposedebitcard.reloadreversal" : //PurposeDebitCard.ReloadReversal
				{
					PurposeProxy.ApplyForReloadReversal(entry);
					return;
				}
				case "purposedebitcard.reload" : //PurposeDebitCard.Reload
				{
					PurposeProxy.ApplyForRefill(entry);
					return;
				}
				case "purposedebitcard.enroll" : //PurposeDebitCard.Enroll
				{
					PurposeProxy.ApplyForNewCard(entry);
					return;
				}
				default :
					throw new ArgumentException("Unknown web method: " + method);
			}
		}
		IWebSvcQueue SetupWQ(IPayInfo payInfo, string method)
		{
			IWebSvcQueue wq = new WebSvcQueue();

			wq.WSProvider = Provider;
			wq.QueType    = WebSvcQueueType.Reversal.ToString();
			wq.ClerkId    = payInfo.ParDemand.ConsumerAgent;
			wq.StoreCode  = payInfo.ParDemand.StoreCode;
			wq.WebMethod  = method;
			wq.Status     = WebSvcQueueStatus.Completed.ToString();
			wq.BusObject  = payInfo.ToString();
			wq.BusObjId   = payInfo.Id.ToString();

			return wq;
		}
		void Reverse(IWebSvcQueue wq, PurposeDCResponse resp, IPayInfo payInfo, XElement xReq, string message, string revMethodType)
		{
			if (resp == null)
				return;

			if (resp.RespCode.Trim() != "60")  // Reverse on Timeouts only 8/15/05 per Barbara
				return;

			wq.Status = WebSvcQueueStatus.Open.ToString();
			if (xReq != null)
			{
				wq.Xml = xReq.ToString();
				wq.ReversalXml = new XmlEleRevReq(xReq, payInfo).ToString();
			}
	
			wq.ReversalMethod = revMethodType;
			wq.InitialMsg = message;
		}
		void SetRespAttr(IWebSvcQueue wq, PurposeDCResponse resp, XElement xReq)
		{
			wq.InitReasonCode = resp.RespCode;
			wq.InitialMsg = resp.RespText;	

			if (resp.Xnode != null)
				wq.InitRespXml = resp.Xnode;

//			if (xReq != null) //From now we will not store client information initially but in case of failure 
//				wq.Xml = xReq.ToString(); //i.e. in reverse call only ** 9/12/2006 (as per Don) **
		}		

	#endregion
		

	}	
}