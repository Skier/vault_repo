using System;
using System.Xml;
using System.Collections;
using System.Configuration;

using DPI.Interfaces;
 
namespace DPI.Components
{
	public class PhoneTechWSGateway  : IWebSvcProvider
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
		public PhoneTechWSGateway(string provider)
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
			SvcPlanDataResp resp = GetSvcPlanData(uow, ci, logQue);
			
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
			SvcPlanDataResp resp = GetSvcPlanData(uow, ci, logQue);
			
			if (resp == null)
				return false;
			
			if (resp.PlanStatus == null)
				return false;

			if (resp.StartDate > DateTime.Today)
				return false;

			if (resp.DueDate < DateTime.Today)
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
				return CheckActivation(uow, cellInfo, logQue);

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
		ICellPhoneReceipt ActivatePhone(ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
//			XmlNode xNode = new PhoneTechWS().ActivatePhone(ci.NewESN, ci.Pin, ci.Zip, "", Acct, PW); 
//			
//			logQue.Xml = new ActivatePhoneXml(ci.NewESN, ci.Pin, ci.Zip, Acct, PW).ToXmlDoc().InnerXml;
//			logQue.InitRespXml = xNode.InnerXml;
//			SimpleResp resp = new SimpleResp(xNode);

			//CellPhoneReceipt rct = new CellPhoneReceipt(resp.Pass, resp.ErrMessage);
			CellPhoneReceipt rct = new CellPhoneReceipt(true, "");
			rct.Pin = ci.Pin;
			
			return rct;
		}
		ICellPhoneReceipt CheckActivation(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			SvcPlanDataResp svcResp = GetSvcPlanData(uow, ci, logQue);
			ci.ControlNumber = svcResp.ControlNumber;

			XmlNode xNode = new PhoneTechWS().CheckActivation(ci.NewESN, Acct, PW); 
			logQue.Xml = new CheckActivationXml(ci.NewESN, Acct, PW).ToXmlDoc().InnerXml;
			logQue.InitRespXml = xNode.OuterXml;

			CheckActivationResp resp = new CheckActivationResp(xNode);

			ci.PhoneNumber = resp.PhoneNumber; // updates phone info
			CellPhoneReceipt rct = new CellPhoneReceipt(resp.Pass, resp.ErrMessage,  resp.Status, GetReceiptText(uow, ci, resp));

			SetReceiptAttrs(rct, ci, resp);			
			
			return rct;
		}
		ICellPhoneReceipt ReplenishServicePlan(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			ci.Pin = GetPIN(uow, ci.WireleesProduct); // reserve pin

			SvcPlanDataResp svcResp = GetSvcPlanData(uow, ci, logQue);
			ci.ControlNumber = svcResp.ControlNumber;

			XmlNode xNode = new PhoneTechWS().ReplenishServicePlan(ci.Pin, ci.PhoneNumber, Acct, PW); 
			
			logQue.Xml = new ReplenishServicePlanXml(ci.Pin, ci.PhoneNumber, Acct, PW).ToXmlDoc().InnerXml;
			logQue.InitRespXml = xNode.InnerXml;

			SimpleResp resp = new SimpleResp(xNode);
			// Remove receipt text
			CellPhoneReceipt rct = new CellPhoneReceipt(resp.Pass, resp.ErrMessage);
			rct.Pin = ci.Pin;
			rct.PhoneNumber = ci.PhoneNumber;
			rct.ControlNumber = ci.ControlNumber;

			return rct;
		}

		public SvcPlanDataResp GetSvcPlanData(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue)
		{
			string startDate = DateTime.Today.ToShortDateString();
			string endDate = DateTime.Today.AddMonths(-1).ToShortDateString();
		
			XmlNode xNode = new PhoneTechWS().GetServicePlanData(ci.PhoneNumber, ci.NewESN, startDate, endDate, Acct, PW); 

			logQue.Xml = new ReplenishServicePlanXml(string.Empty,ci.PhoneNumber, Acct, PW).ToXmlDoc().InnerXml;
			logQue.InitRespXml = xNode.InnerXml;

			return new SvcPlanDataResp(xNode);
		}
		public void ProcessQueue(IWebSvcQueue entry)
		{
			if (entry == null)
				throw new ArgumentNullException("Web queue entry is null");

		//	if (entry.QueType.Trim().ToLower() == WebSvcQueueType.Evergreen.ToString().Trim().ToLower())
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

			if (rct.PhoneNumber == null)
				return;

			rct.IsActivated = resp.PhoneNumber.Trim().Length > 0;
		}
	#endregion
	}
}