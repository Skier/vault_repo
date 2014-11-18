using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class CustomerInquiryWIP : WIP
	{
	#region Member Variables
		string phone;
		IAcctInfo acctInfo;
		ICustInfoExt custInfoExt;
		IDemand demand;
		ICustomerRecurringPayment[] customerROPs;
		int recurringId;
		string operationMode;
	#endregion	
	
	#region Properties
		public override IWipStep FirstStep { get { return WorkflowFact.CustomerInquiryFirstStep(); }}
	#endregion
	
	#region Constructors
		public CustomerInquiryWIP(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
		{
			CurrStep = (WIP.WipStep)FirstStep;
		}
	#endregion
	
	#region	Methods
		protected override object find(string attr)
		{
			if (attr == null)
				return null;

			switch(attr.ToLower())
			{
				case "phone" :
					return phone;

				case "acctinfo" :
					return acctInfo;

				case "custinfoext" :
					return custInfoExt;
					
				case "salesidrequired" :
					return false;
				
				case "demand" :
					return demand;

				case "recurringid" :
					return recurringId;

				case "customerrops" :
					return customerROPs;

				case "operationmode" :
					return operationMode;

				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
		protected override void load(string attr, object obj)
		{
			if (attr == null)
				return;

			switch(attr.ToLower())
			{
				case "phone" :
					phone = (string)obj;
					break;
				
				case "acctinfo" :
					acctInfo = (IAcctInfo)obj;
					break;
				
				case "custinfoext" :
					custInfoExt = (ICustInfoExt)obj;
					break;

				case "demand" :
					demand = (IDemand)obj;
					break;
				
				case "recurringid" :
					recurringId = (int)obj;
					break;

				case "customerrops" :
					customerROPs = (ICustomerRecurringPayment[])obj;
					break;

				case "operationmode" :
					operationMode = (string)obj;
					break;

				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
	#endregion
	}
}