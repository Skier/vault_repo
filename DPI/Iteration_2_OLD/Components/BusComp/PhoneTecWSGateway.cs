using System;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Threading;

using DPI.Interfaces;
 
namespace DPI.Components
{
	public class PhoneTecWSGateway  : IWebSvcProvider
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
		static string Acct { get { return ConfigurationSettings.AppSettings["PhoneTechWebSvcAcct"]; }}
		static string PW   { get { return ConfigurationSettings.AppSettings["PhoneTechWebSvcPwd"];  }}
		public	bool IsWirelessXactPosted { get { return false; }}
	#endregion

	#region Constructors
		public PhoneTecWSGateway(string provider)
		{
			this.provider = provider;
		}
	#endregion

	#region Methods
		public IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID, int areaCode, int prefix)
		{
			throw new ApplicationException("PhoneTechWSGateway.GetProducts() is not implemented");
		}
		public IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID)
		{
			throw new ApplicationException("PhoneTechWSGateway.GetProducts() is not implemented");
		}
		public bool IsActive(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			logQue.WSProvider = provider;
			ISvcPlanDataResp resp = GetSvcPlanData(uow, ci, logQue);
			
			if (resp == null)
				return false;
			
			if (resp.PlanStatus == null)
				return false;
			
			if (resp.PlanStatus.Trim().Length < 1)
				return false;
			
			if (resp.PlanStatus.Trim().ToLower() != "deactivated")
				return true;

//			if (resp.PlanStatus.Trim().ToLower() == "deactivated")
//			{
//				ci.Zip = resp.Zip;
//				ci.NewESN = resp.Esn;
//				return ActivatePhone(ci, logQue).Pass;				
//			}

			return false;
		}		
		public bool IsDateValid(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			logQue.WSProvider = provider;
			ISvcPlanDataResp resp = GetSvcPlanData(uow, ci, logQue);
			
			if (resp == null)
				return false;
			
			if (resp.PlanStatus == null)
				return false;

			if (resp.StartDate > DateTime.Today)
				return false;

			if (resp.ExpirationDate < DateTime.Today)
				return false;

			return true;

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

			if (action == Const.ACTIVATE_PHONE)
				return ActivatePhone(cellInfo, logQue);
			
			if (action == Const.CHECK_ACTIVATION)
				return CheckActivation(cellInfo, logQue);

			if (action == Const.REPLENISH_SERVICE_PLAN)
				return ReplenishServicePlan(uow, cellInfo, logQue);

			if (action == Const.ORDER_PRODUCT)
				return ReplenishServicePlan(uow, cellInfo, logQue);
			
			throw new ArgumentException("InfinityMobileWSGateway: Uknown action '" + action + "'");
		}
		string GetPIN(IUOW uow, int prod)
		{
			return AOL_PINs.ReservePIN((UOW)uow, prod).PIN;
		}
		public ICellPhoneReceipt ActivatePhone(ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			try
			{
			XmlNode xNode = new PhoneTecWS().ActivatePhone(ci.NewESN, ci.Pin, ci.Zip, "", Acct, PW); 
			
			logQue.Xml = new ActivatePhoneXml(ci.NewESN, ci.Pin, ci.Zip, Acct, PW).ToXmlDoc().InnerXml;
			logQue.InitRespXml = xNode.InnerXml;
			SimpleResp resp = new SimpleResp(xNode);

				Thread.Sleep(int.Parse(ConfigurationSettings.AppSettings["DpiWLActivationSleepTime"]));
				
				ICellPhoneReceipt rct = CheckActivation(ci, logQue);
				
				rct.Pass = false;
				
				if (rct.Msid != null && rct.Msid.Length > 0)
					rct.Pass = rct.IsActivated;
				
			rct.Pin = ci.Pin;
			
			return rct;
		}
			catch (Exception ex)
		{
				ICellPhoneReceipt rcpt = new CellPhoneReceipt(false, ex.Message + " PhoneTecWSGateway.ActivatePhone failed.");
				rcpt.Pin = ci.Pin;

				return rcpt;
			}
		}
		public ICellPhoneReceipt CheckActivation(ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			ISvcPlanDataResp svcResp = GetAvailableBalance(ci.PhoneNumber, ci.NewESN);
			ci.ControlNumber = svcResp.ControlNumber;

			XmlNode xNode = new PhoneTecWS().CheckActivation(ci.NewESN, Acct, PW); 
			//logQue.Xml = new CheckActivationXml(ci.NewESN, Acct, PW).ToXmlDoc().InnerXml;
			logQue.LastRespXml = xNode.OuterXml;

			CheckActivationResp resp = new CheckActivationResp(xNode);

			ci.PhoneNumber = resp.PhoneNumber; // updates phone info
			CellPhoneReceipt rct = new CellPhoneReceipt(resp.Pass, resp.ErrMessage,  resp.Status);

			SetReceiptAttrs(rct, ci, resp);			
			
			return rct;
		}
		public CheckActivationResp CheckActivation(string esn)
		{
			return new CheckActivationResp(new PhoneTecWS().CheckActivation(esn, Acct, PW));
		}
		public ICellPhoneReceipt ReplenishServicePlan(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			try
			{
//				ISvcPlanDataResp svcResp = GetAvailableBalance(ci.PhoneNumber, ci.NewESN);
//				ci.ControlNumber = svcResp.ControlNumber;


			XmlNode xNode = new PhoneTecWS().ReplenishServicePlan(ci.Pin, ci.PhoneNumber, Acct, PW); 
			
			logQue.Xml = new ReplenishServicePlanXml(ci.Pin, ci.PhoneNumber, Acct, PW).ToXmlDoc().InnerXml;
			logQue.InitRespXml = xNode.InnerXml;

			SimpleResp resp = new SimpleResp(xNode);
			
				CellPhoneReceipt rct = new CellPhoneReceipt(resp.ErrMessage == null || resp.ErrMessage.Length==0, resp.ErrMessage);
				
				Thread.Sleep(int.Parse(ConfigurationSettings.AppSettings["DpiWLActivationSleepTime"]));

				CheckActivation(ci, logQue);
			rct.Pin = ci.Pin;
			rct.PhoneNumber = ci.PhoneNumber;
			rct.ControlNumber = ci.ControlNumber;

			return rct;
		}
			catch (Exception ex)
			{
				ICellPhoneReceipt rcpt = new CellPhoneReceipt(false, ex.Message + " PhoneTecWSGateway.ReplenishServicePlan failed.");
				rcpt.Pin = ci.Pin;

				return rcpt;
			}
		}

		public ISvcPlanDataResp GetDpiSvcPlanData(string phone, string esn)
		{
			string startDate = DateTime.Today.ToShortDateString();
			string endDate = DateTime.Today.AddDays(-7).ToShortDateString();
		
			XmlNode xNode = new PhoneTecWS().GetServicePlanData(phone, esn, startDate, endDate, Acct, PW); 

			return new DpiSvcPlanDataResp(xNode);
		}
		public ISvcPlanDataResp GetAvailableBalance(string phone, string esn)
		{
			try
			{
				XmlNode xNode = new PhoneTecWS().GetAvailableBalance(phone, esn, Acct, PW); 

				ISvcPlanDataResp resp = new DpiSvcPlanDataResp(xNode);
				CheckActivationResp actResp = CheckActivation(esn);
				resp.Mdn = actResp.MDN;

				return resp;
			}
			catch (Exception ex)
			{
				return new DpiSvcPlanDataResp(false, ex.Message);
			}
		}
		public void ProcessQueue(IWebSvcQueue entry)
		{
			if (entry == null)
				throw new ArgumentNullException("Web queue entry is null");

		//	if (entry.QueType.Trim().ToLower() == WebSvcQueueType.Evergreen.ToString().Trim().ToLower())
			DoEntry(entry.WebMethod.Trim().ToLower(), entry);
		}	
		public ISvcPlanDataResp GetSvcPlanData(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			string startDate = DateTime.Today.ToShortDateString();
			string endDate = DateTime.Today.AddMonths(-1).ToShortDateString();
		
			XmlNode xNode = new PhoneTecWS().GetServicePlanData(ci.PhoneNumber, ci.NewESN, startDate, endDate, Acct, PW); 

			logQue.Xml = new ReplenishServicePlanXml(string.Empty,ci.PhoneNumber, Acct, PW).ToXmlDoc().InnerXml;
			logQue.InitRespXml = xNode.InnerXml;

			return new DpiSvcPlanDataResp(xNode);
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
		void CheckPinsLevels()
		{

		}
		DictionaryEntry[] GetResults( ICellPhoneInfo cell, SimpleResp resp)
		{
			return Combine(new DictionaryEntry[][] { resp.Entries, cell.Entries});
		}
		string GetReceiptText(IUOW uow, ICellPhoneInfo cell, SimpleResp resp)
		{
			return Wireless_Products.find((UOW)uow, cell.WireleesProduct)
				.GetReceipt(resp.Pass, Combine(new DictionaryEntry[][] { resp.Entries, cell.Entries}));
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
		static void SetReceiptAttrs(ICellPhoneReceipt rct, ICellPhoneInfo ci, CheckActivationResp resp)
		{
			rct.Pin = ci.Pin;
			rct.ControlNumber = ci.ControlNumber;
			rct.PhoneNumber = resp.PhoneNumber;
			rct.Mdn = resp.MDN;
			rct.Msid = resp.MSID;
			rct.Msl = resp.MSL;

			rct.IsActivated = resp.Status == "Success";
		}
	#endregion
	}
}