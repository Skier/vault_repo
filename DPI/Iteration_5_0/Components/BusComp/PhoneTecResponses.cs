using System;
using System.Xml;
using System.Text;
using System.Collections;

using DPI.Interfaces;
using DPI.Components;

namespace DPI.Components
{
	public class DpiSvcPlanDataResp : ISvcPlanDataResp
	{
		#region Data
		bool pass;
		string errMessage;
		string planStatus;
		string esn;
		string controlNumber;
		DateTime customerSince;
		DateTime startDate;
		DateTime expirationDate;
		string mdn;
		decimal cashBalance;
		string planType;
		string planName;
		string pinItemNumber;
		string anytimeUsedMins;
		string nWUsedMins;
		string webUsedMins = null;
		string textUsedMins;
		string threeGWebUsedMins = null;
		string threeGPictureUsedMins = null;
		string threeGPTTUsedMins = null;		
		string usedMinutes;
		#endregion
		
		#region ISvcPlanDataResp Members
		public bool Pass { get { return pass; }}
		public string ErrMessage { get { return errMessage; }}
		public string PlanStatus    { get { return planStatus; }}
		public string Esn			{ get { return esn; }}
		public string ControlNumber { get { return controlNumber; }}
		public DateTime CustomerSince { get { return customerSince; }}
		public DateTime StartDate				{ get { return startDate; }}
		public DateTime ExpirationDate			{ get { return expirationDate; }}
		public string Mdn						
		{ 
			get { return mdn;  }
			set { mdn = value; }
		}
		public decimal CashBalance				{ get { return cashBalance; }}
		public string PlanType					{ get { return planType; }}
		public string PlanName					{ get { return planName; }}
		public string PinItemNumber				{ get { return pinItemNumber; }}
		public string AnytimeUsedMins			{ get { return anytimeUsedMins; }}
		public string NWUsedMins				{ get { return nWUsedMins; }}
		public string WebUsedMins				{ get { return webUsedMins; }}
		public string TextUsedMins				{ get { return textUsedMins; }}
		public string ThreeGWebUsedMins			{ get { return threeGWebUsedMins; }}
		public string ThreeGPictureUsedMins		{ get { return threeGPictureUsedMins; }}
		public string ThreeGPTTUsedMins			{ get { return threeGPTTUsedMins; }}
		#endregion

		#region Constractors
		public DpiSvcPlanDataResp(bool pass, string errMsg)
		{
			this.pass = pass;
			this.errMessage = errMsg;
		}
		public DpiSvcPlanDataResp(XmlNode xNode)
		{
			XmlTextReader xReader = new XmlTextReader(new System.IO.StringReader
				(XMLUtility.InsertLineBreaks(xNode.OuterXml))); 

			while (xReader.Read())
			{
				if (xReader.NodeType != XmlNodeType.Element)
					continue;

				switch (xReader.Name.ToLower())
				{
						
					case "usedminutes" :
					{
						if (usedMinutes == null || usedMinutes == "")
						{
							usedMinutes = xReader.ReadInnerXml().Trim();
							break;
						}
						usedMinutes = usedMinutes + "," + xReader.ReadInnerXml().Trim();
						break;
					}
					case "returnvalue" :
						pass = xReader.ReadInnerXml().Trim().ToLower() == "pass" ;
						break;
					
					case "errormessage" :
						errMessage = xReader.ReadInnerXml().Trim();
						break;
					
					case "planstatus" :
						planStatus = xReader.ReadInnerXml().Trim();
						break;
					
					case "esn" :
						esn = xReader.ReadInnerXml().Trim();
						break;
					
					case "controlnumber" :
						controlNumber = xReader.ReadInnerXml().Trim();
						break;
					
					case "customersince" :
						customerSince = ConvertDateTime(xReader.ReadInnerXml().Trim());
						break;
					
					case "startdate" :
						startDate = ConvertDateTime(xReader.ReadInnerXml());				
						break;
					
					case "expirationdate" :
						expirationDate = ConvertDateTime(xReader.ReadInnerXml());
						break;
					
					case "mdn" :
						mdn = xReader.ReadInnerXml().Trim();
						break;
						
					case "cashbalance" :
						cashBalance = ConverToDecimal(xReader.ReadInnerXml().Trim());
						break;

					case "plantype" :
						planType = xReader.ReadInnerXml().Trim();
						break;
					
					case "planname" :
						planName = xReader.ReadInnerXml().Trim();
						break;
					
					case "pinitemnumber" :
						pinItemNumber = xReader.ReadInnerXml().Trim();
						break;
				}
			}

			ProcessUsedMins(usedMinutes);
		}
		#endregion

		#region Methods
		static DateTime ConvertDateTime(string s)
		{
			return ConvertDateTime(s, DateTime.MinValue);
		}
		static decimal ConverToDecimal(string s)
		{
			try
			{
				return decimal.Parse(s);
			}
			catch (Exception)
			{
				return 0m;
			}
		}

		static DateTime ConvertDateTime(string s, DateTime def)
		{
			if (s.Trim().Length == 0)
				return def;

			return DateTime.Parse(s);	
		}
		
		void ProcessUsedMins(string mins)
		{
			if (mins == null)
				return;

			if (mins == "")
				return;

			string[] planMins = mins.Split(',');
			
			if (planMins.Length > 3)
			{
				anytimeUsedMins = planMins[0];
				nWUsedMins = planMins[1];
				//webUsedMins = planMins[2];
				textUsedMins = planMins[3];
			}

		}
		#endregion
	}
	public class WirelessDeviceDataResp : IWirelessDeviceData
	{
		#region Data
		bool   pass;
		string errMessage;
		string eSN;
		string eSNHex;
		string mDN;
		string mSID;
		string carrierMSL;
		string currentMSL;
		string cSA;
		string subscriberID;
		DpiWLPlanStatus planStatus;
		bool   statusPending;
		string carrierName;
		string provider;
		#endregion

		#region Properties		
		public bool   Pass	{ get { return pass; }}
		public string ErrMessage { get { return errMessage; }}
		public string ESN	{ get { return eSN; }}
		public string ESNHex { get { return eSNHex; }}
		public string MDN	{ get { return mDN; }}
		public string MSID	{ get { return mSID; }}
		public string CarrierMSL	{ get { return carrierMSL; }}
		public string CurrentMSL	{ get { return currentMSL; }}
		public string CSA	{ get { return cSA; }}
		public string SubscriberID	{ get { return subscriberID; }}
		public DpiWLPlanStatus PlanStatus	{ get { return planStatus; }}
		public bool   StatusPending	{ get { return statusPending; }}
		public string CarrierName	{ get { return carrierName; }}
		public string Provider
		{
			get { return provider; }
			set { provider = value; }
		}
		
		#endregion

		#region Constractors
		public WirelessDeviceDataResp(bool pass, string errMsg)
		{
			this.pass = pass;
			this.errMessage = errMsg;
		}
		public WirelessDeviceDataResp(XmlNode xNode)
		{
			XmlTextReader xReader = new XmlTextReader(new System.IO.StringReader
														(XMLUtility.InsertLineBreaks(xNode.OuterXml))); 

			while (xReader.Read())
			{
				if (xReader.NodeType != XmlNodeType.Element)
					continue;

				switch (xReader.Name.ToLower())
				{
					case "returnvalue" :
					{
						pass = xReader.ReadInnerXml().Trim().ToLower() == "pass" ;
						break;
					}
					case "errormessage" :
					{
						errMessage = xReader.ReadInnerXml().Trim();
						break;
					}
					case "esn" :
					{
						eSN = xReader.ReadInnerXml();
						break;
					}
					case "esnhex" :
					{
						eSNHex = xReader.ReadInnerXml();
						break;
					}
					case "mdn" :
					{
						mDN = xReader.ReadInnerXml();
						break;
					}
					case "msid" :
					{
						mSID = xReader.ReadInnerXml();
						break;
					}
					case "carriermsl" :
					{
						carrierMSL = xReader.ReadInnerXml();
						break;
					}
					case "currentmsl" :
					{
						currentMSL = xReader.ReadInnerXml();
						break;
					}
					
					case "csa" :
					{
						cSA = xReader.ReadInnerXml();
						break;
					}
					case "subscriberid" :
					{
						subscriberID = xReader.ReadInnerXml();
						break;
					}
					case "planstatus" :
					{
						planStatus = GetPlanStatus(xReader.ReadInnerXml().Trim().ToLower());
						break;
					}
					case "statuspending" :
					{
						statusPending = xReader.ReadInnerXml().Trim().ToLower() == "true";
						break;
					}
					case "carriername" :
					{
						carrierName = xReader.ReadInnerXml().Trim();
						break;
					}
				}
			}
		}

		#endregion

		#region Methods
		DpiWLPlanStatus GetPlanStatus(string status)
		{
			switch (status)
			{
				case "active" :
					return DpiWLPlanStatus.Active;
				
				case "deactivated" :
					return DpiWLPlanStatus.Deactivated;
				
				case "expended" :
					return DpiWLPlanStatus.Expended;
				
				case "expired" :
					return DpiWLPlanStatus.Expired;
				
				case "fail" :
					return DpiWLPlanStatus.Fail;
				
				case "inactive" :
					return DpiWLPlanStatus.Inactive;
				
				case "none" :
					return DpiWLPlanStatus.None;
				
				case "suspended" :
					return DpiWLPlanStatus.Suspended;
				
				default :
					return DpiWLPlanStatus.Suspended;
			}
		}
		
		public static bool IsReplanish(IWirelessDeviceData resp)
		{
			if (resp == null)
				return false;
			
			if (resp.PlanStatus == DpiWLPlanStatus.Suspended)
				return true;

			if (resp.PlanStatus == DpiWLPlanStatus.Expended)
				return true;

			if (resp.PlanStatus == DpiWLPlanStatus.Expired)
				return true;

			if (resp.PlanStatus == DpiWLPlanStatus.Active)
				return true;

			return false;
		}
		public static bool IsInActivationMode(IWirelessDeviceData resp)
		{
			if (resp == null)
				return false;
			
			if (resp.PlanStatus == DpiWLPlanStatus.Inactive)
				return true;

			if (resp.PlanStatus == DpiWLPlanStatus.Deactivated)
				return true;

			if (resp.PlanStatus == DpiWLPlanStatus.None)
				return true;

			if (resp.PlanStatus == DpiWLPlanStatus.Unknown)
				return true;

			return false;
		}
		#endregion
	}
	public class ServicePlanResp : IServicePlan
	{
		#region Data
		string pin;
		string controlNumber;
		string description;
		bool   currentlyInUse;
		DpiWLPlanStatus planStatus;
		DateTime loadDate;
		DateTime startDate;
		DateTime expirationDate;
		#endregion

		#region Properties
		public string Pin	{ get { return pin; }}
		public string ControlNumber { get { return controlNumber; }}
		public string Description { get { return description; }}
		public bool   CurrentlyInUse { get { return currentlyInUse; }}
		public DpiWLPlanStatus PlanStatus	{ get { return planStatus; }}
		public DateTime LoadDate { get { return loadDate; }}
		public DateTime StartDate { get { return startDate; }}
		public DateTime ExpirationDate { get { return expirationDate; }}
		#endregion

		#region Constractors		
		public ServicePlanResp(XmlNode xNode)
		{
			XmlTextReader xReader = new XmlTextReader(new System.IO.StringReader
				(XMLUtility.InsertLineBreaks(xNode.OuterXml))); 

			while (xReader.Read())
			{
				if (xReader.NodeType != XmlNodeType.Element)
					continue;

				switch (xReader.Name.ToLower())
				{
					case "pin" :
						pin = xReader.ReadInnerXml();
						break;

					case "controlnumber" :
						controlNumber = xReader.ReadInnerXml();
						break;

					case "description" :
						description = xReader.ReadInnerXml();
						break;

					case "currentlyinuse" :
						currentlyInUse = xReader.ReadInnerXml().Trim().ToLower() == "true";
						break;

					case "planstatus" :
						planStatus = GetPlanStatus(xReader.ReadInnerXml().Trim().ToLower());
						break;
					
					case "loaddate" :
						loadDate = ConvertDateTime(xReader.ReadInnerXml());
						break;
					
					case "startdate" :
						startDate = ConvertDateTime(xReader.ReadInnerXml());				
						break;
					
					case "expirationdate" :
						expirationDate = ConvertDateTime(xReader.ReadInnerXml());
						break;
				}
			}
		}

		#endregion

		#region Methods
		DpiWLPlanStatus GetPlanStatus(string status)
		{
			switch (status)
			{
				case "active" :
					return DpiWLPlanStatus.Active;
				
				case "deactivated" :
					return DpiWLPlanStatus.Deactivated;
				
				case "expended" :
					return DpiWLPlanStatus.Expended;
				
				case "expired" :
					return DpiWLPlanStatus.Expired;
				
				case "fail" :
					return DpiWLPlanStatus.Fail;
				
				case "inactive" :
					return DpiWLPlanStatus.Inactive;
				
				case "none" :
					return DpiWLPlanStatus.None;
				
				case "suspended" :
					return DpiWLPlanStatus.Suspended;
				
				default :
					return DpiWLPlanStatus.Suspended;
			}
		}
		
		static DateTime ConvertDateTime(string s)
		{
			return ConvertDateTime(s, DateTime.MinValue);
		}

		static DateTime ConvertDateTime(string s, DateTime def)
		{
			if (s.Trim().Length == 0)
				return def;

			return DateTime.Parse(s);	
		}		
		#endregion
	}
	public class ServicePlanListResp : IServicePlanList
	{
		#region Data
		bool   pass;
		string errMessage;
		IServicePlan[] svcPlans;
		#endregion

		#region Properties		
		public bool   Pass	{ get { return pass; }}
		public string ErrMessage { get { return errMessage; }}
		public IServicePlan[] SvcPlans	{ get { return svcPlans; }}
		#endregion

		#region Constractors
		public ServicePlanListResp(bool pass, string errMsg)
		{
			this.pass = pass;
			this.errMessage = errMsg;
		}
		public ServicePlanListResp(XmlNode xNode)
		{
			bool exit = false;

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xNode.OuterXml);
			
			this.svcPlans = GetSvcPlanList(doc.SelectNodes("/GetServicePlanList/ServicePlanList/ServicePlan"));

			XmlTextReader xReader = new XmlTextReader(new System.IO.StringReader
				(XMLUtility.InsertLineBreaks(xNode.OuterXml))); 

			while (xReader.Read() && exit == true)
			{
				if (xReader.NodeType != XmlNodeType.Element)
					continue;

				switch (xReader.Name.ToLower())
				{
					case "returnvalue" :
						pass = xReader.ReadInnerXml().Trim().ToLower() == "pass" ;
						break;
					
					case "errormessage" :
						errMessage = xReader.ReadInnerXml().Trim();
						exit = true;
						break;					
				}
			}

			
			

		}

		#endregion

		#region Constractors
		IServicePlan[] GetSvcPlanList(XmlNodeList list)
		{
			ArrayList ar = new ArrayList();

			foreach (XmlNode node in list)
				ar.Add(new ServicePlanResp(node));
			
			IServicePlan[] plans = new ServicePlanResp[ar.Count];
			ar.CopyTo(plans);

			return plans;
		}
		
		#endregion
	}
}