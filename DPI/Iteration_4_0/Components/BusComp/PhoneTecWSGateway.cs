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

				if (objects[i] is ICellPhoneInfo)
					cellInfo = (ICellPhoneInfo)objects[i];
			}

			if (action == Const.ACTIVATE_PHONE)
				return ActivatePhone(cellInfo, logQue);
			
			if (action == Const.CHECK_ACTIVATION)
				return CheckActivation(cellInfo, logQue);

			if (action == Const.REPLENISH_SERVICE_PLAN)
				return ReplenishServicePlan(uow, cellInfo, logQue);

			if (action == Const.CHECK_PLAN_STATUS)
				return CheckPlanStatus(cellInfo, logQue);

			if (action == Const.ORDER_PIN)
				return OrderPin(uow, wp);
			
			throw new ArgumentException("PhoneTecWSGateway: Uknown action '" + action + "'");
		}
		IReceipt OrderPin(IUOW uow, IWireless_Products wp)
		{
			throw new ArgumentException("Product: " + wp.Wireless_product_id.ToString() + " is not supported.");
		}
		string GetPIN(IUOW uow, int prod)
		{
			return AOL_PINs.ReservePIN((UOW)uow, prod).PIN;
		}
		
		public bool ResetPin(IWebSvcQueue logQue, string pin)
		{
			try
			{
				XmlNode xNode = new PhoneTecWS().ResetPin(pin, Acct, PW);			
				
				logQue.InitRespXml = xNode.InnerXml;
				return new SimpleResp(xNode).Pass;				
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool ResetPinBySvcPlanList(IWebSvcQueue logQue, string esn)
		{
			XmlNode xNode = new PhoneTecWS().GetServicePlanList("", esn, Acct, PW);

			IServicePlanList svcPlanList = new ServicePlanListResp(xNode);

			for (int i = 0; i < svcPlanList.SvcPlans.Length; i++)
				if (svcPlanList.SvcPlans[i].PlanStatus == DpiWLPlanStatus.Inactive)
					if (!ResetPin(logQue, svcPlanList.SvcPlans[i].Pin))
						return false;
					else
					{
						Thread.Sleep(10000);
						continue;
					}

			return true;
		}
		bool ProcessForActivation(IWebSvcQueue logQue, string esn)
		{
			IWirelessDeviceData data = GetWLDeviceDataResp("", esn);

			if (data.PlanStatus != DpiWLPlanStatus.Inactive)
				return true;
			
			return ResetPinBySvcPlanList(logQue, esn);
		}
		public ICellPhoneReceipt ActivatePhone(ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			try
			{
				if (!ProcessForActivation(logQue, ci.NewESN))
					return new CellPhoneReceipt(false, "Can not process for activation");

				XmlNode xNode = new PhoneTecWS().ActivatePhone(ci.NewESN, ci.Pin, ci.Zip, "", Acct, PW);			
				logQue.Xml = new ActivatePhoneXml(ci.NewESN, ci.Pin, ci.Zip, Acct, PW).ToXmlDoc().InnerXml;
				logQue.InitRespXml = xNode.InnerXml;
				SimpleResp resp = new SimpleResp(xNode);				
				CellPhoneReceipt rct = new CellPhoneReceipt(resp.Pass, resp.ErrMessage);
				ProcessPlan(rct, ci, logQue);				
				rct.Pass = rct.IsActivated;

				return rct;
			}
			catch (Exception ex)
			{
				ICellPhoneReceipt rcpt = new CellPhoneReceipt(false, ex.Message + " PhoneTecWSGateway.ActivatePhone failed.");
				rcpt.Pin = ci.Pin;

				return rcpt;
			}
		}
		public ICellPhoneReceipt ReplenishServicePlan(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			try
			{
				XmlNode xNode = new PhoneTecWS().ReplenishServicePlan(ci.Pin, ci.PhoneNumber, Acct, PW); 
			
				logQue.Xml = new ReplenishServicePlanXml(ci.Pin, ci.PhoneNumber, Acct, PW).ToXmlDoc().InnerXml;
				logQue.InitRespXml = xNode.InnerXml;
				SimpleResp resp = new SimpleResp(xNode);			
				CellPhoneReceipt rct = new CellPhoneReceipt(resp.ErrMessage == null || resp.ErrMessage.Length==0, resp.ErrMessage);				
				ProcessPlan(rct, ci, logQue);

				if (rct.Pass)
					rct.Pass = rct.Msid != null && rct.Msid.Length > 0;				

				return rct;
			}
			catch (Exception ex)
			{
				ICellPhoneReceipt rcpt = new CellPhoneReceipt(false, ex.Message + " PhoneTecWSGateway.ReplenishServicePlan failed.");
				rcpt.Pin = ci.Pin;

				return rcpt;
			}
		}

//		public ICellPhoneReceipt GetWLDeviceDataResp(ICellPhoneInfo ci, IWebSvcQueue logQue)
//		{
//			try
//			{
//				CellPhoneReceipt rct = new CellPhoneReceipt(false, "");				
//				ProcessPlan(rct, ci, logQue);
//				rct.Pass = rct.IsActivated;
//
//				return rct;
//			}
//			catch (Exception ex)
//			{
//				ICellPhoneReceipt rcpt = new CellPhoneReceipt(false, ex.Message + " PhoneTecWSGateway.ActivatePhone failed.");
//				rcpt.Pin = ci.Pin;
//
//				return rcpt;
//			}
//		}
		void ProcessPlan(ICellPhoneReceipt rct, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			int sleepTime = 0;
			XmlNode xNode = null;
			IWirelessDeviceData resp = null;

			while (sleepTime < int.Parse(ConfigurationSettings.AppSettings["DpiWLActivationSleepTime"]))
			{
				Thread.Sleep(10000);
				xNode = new PhoneTecWS().GetWirelessDeviceData("", ci.NewESN, Acct, PW);
				logQue.LastRespXml = xNode.InnerXml;
				resp = new WirelessDeviceDataResp(xNode);
				sleepTime += 10000;
				
				if (!resp.StatusPending)
				{
					SetDeviceData(ci, rct, resp);
					break;				
				}
			}
		}
		void SetDeviceData(ICellPhoneInfo ci, ICellPhoneReceipt rct, IWirelessDeviceData resp)
		{
			rct.Mdn = resp.MDN;
			rct.Msid = resp.MSID;
			rct.Msl = resp.CurrentMSL;
			rct.PhoneNumber = resp.MDN;
			rct.IsActivated = resp.PlanStatus == DpiWLPlanStatus.Active;
			rct.Pin = ci.Pin;
			rct.ControlNumber = ci.ControlNumber;
			
		}
		public ICellPhoneReceipt CheckPlanStatus(ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			logQue.Xml = new CheckActivationXml(ci.NewESN, Acct, PW).ToXmlDoc().InnerXml;

			CellPhoneReceipt rct = new CellPhoneReceipt(true, "");
			ProcessPlan(rct, ci, logQue);
			rct.Pass = rct.IsActivated; //Both Activation and replenish will set the plan status to active.
			
			return rct;
		}
		public ICellPhoneReceipt CheckActivation(ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			ISvcPlanDataResp svcResp = GetAvailableBalance(ci.PhoneNumber, ci.NewESN);
			ci.ControlNumber = svcResp.ControlNumber;
			logQue.Xml = new CheckActivationXml(ci.NewESN, Acct, PW).ToXmlDoc().InnerXml;

			int sleepTime = 0;
			XmlNode xNode = null;
			CheckActivationResp resp = null;
			while (sleepTime < int.Parse(ConfigurationSettings.AppSettings["DpiWLActivationSleepTime"]))
			{
				Thread.Sleep(10000);
				xNode = new PhoneTecWS().CheckActivation(ci.NewESN, Acct, PW);
				resp = new CheckActivationResp(xNode);
				sleepTime += 10000;
				
				if (resp.MSID != null && resp.MSID.Length > 0)
					break;				
			}
			
			logQue.LastRespXml = xNode.OuterXml;

			ci.PhoneNumber = resp.PhoneNumber; // updates phone info
			CellPhoneReceipt rct = new CellPhoneReceipt(resp.Pass, resp.ErrMessage,  resp.Status);

			SetReceiptAttrs(rct, ci, resp);
			
			return rct;
		}
		public CheckActivationResp CheckActivation(string esn)
		{
			return new CheckActivationResp(new PhoneTecWS().CheckActivation(esn, Acct, PW));
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
				
				return resp;
			}
			catch (Exception ex)
			{
				return new DpiSvcPlanDataResp(false, ex.Message);
			}
		}
		public IWirelessDeviceData GetWLDeviceDataResp(string phone, string esn)
		{
			try
			{
				XmlNode xNode = new PhoneTecWS().GetWirelessDeviceData(phone, esn, Acct, PW); 

				IWirelessDeviceData resp = new WirelessDeviceDataResp(xNode);
				resp.Provider = Const.PHONETEC;

				return resp;
			}
			catch (Exception ex)
			{
				return new WirelessDeviceDataResp(false, ex.Message);
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