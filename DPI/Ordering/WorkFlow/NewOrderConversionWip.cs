using System;
using System.Collections;

using DPI.Interfaces;
using DPI.Components;
using DPI.Services;
using DPI.ClientComp;

namespace DPI.Ordering
{
	[Serializable]
	public class NewOrderConversionWip : WIP
	{
		#region Data
		string         zip;
		IILECInfo      selectedilec;
		IILECInfo[]    avaIlecs;
		IProdPrice     selectedBasicService;
		IProdPrice[]   topProducts;
		IProdPrice[]   prods;
		IPayInfo	   payInfo;
		ICustInfo2     custInfo;
		IAddr2	       servAddr; 
		IAddr2         mailAddr;
		IAcctNotes     acctNotes;
		IReceipt       receipt;
		IOrderSum      orderSummary;
		IDemand		   demand;
		bool		   isHighTouch;
		bool		   isHighTouchDisallowed = false;
		bool           isConfReq; // this particular order is pending
		string		   phNumber;
		string         criteria;
		int            source;
		bool		   allowLocalConv;
		string		   pin;
		IKeyVal[]	   discounts;

		#endregion

		#region Properties
		protected override object find(string attr)
		{
			if (attr == null)
				return null;

			switch(attr.ToLower())
			{
				case "zip" :
					return zip;

				case "ordertype" :
					return OrderType.Conv;

				case "ileccode" :
					return selectedilec.ILECCode;

				case "selectedilec" :
					return selectedilec;

				case "availecs" :
					return avaIlecs;
				
				case "selectedbasicservice" :
					return selectedBasicService;
				
				case "topproducts" :
					return topProducts;

				case "prods" :
					return prods;

				case "demand" :
					return demand;

				case "servaddr" :
					return servAddr;

				case "payinfo" :
					return payInfo;
				
				case "mailaddr" :
					return mailAddr;

				case "custinfo" :
					return custInfo;

				case "ordersummary" :
					return orderSummary;

				case "acctnotes" :
					return acctNotes;

				case "receipt" :
					return receipt;
 
				case "ishightouchdisallowed" :
					return isHighTouchDisallowed;

				case "ishightouch" :
					return isHighTouch;

				case "salesidrequired" :
					return true;
					
				case "isconfreq" :
					return isConfReq;

				case "dmdtype" :
				{
					if (demand != null)
						return demand.DmdType;

					return DemandType.LocalConv.ToString();
				}

				case "phnumber" :
					return phNumber;

				case "criteria" :
					return criteria;
					
				case "source" :
					return source;

				case "showsource" :
					return true;

				case "allowlocalconv" :
					return allowLocalConv;

				case "pin" :
					return pin;

				case "discounts" :
					return discounts;

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
				case "zip" :
					zip = (string)obj;
					break;
				
				case "selectedilec" :
					selectedilec = (IILECInfo)obj;
					break;
				
				case "availecs" :
					avaIlecs = (IILECInfo[])obj;
					break;
				
				case "selectedbasicservice" :
					selectedBasicService = (IProdPrice)obj;
					break;
				
				case "topproducts" :
					topProducts = (IProdPrice[])obj;
					break;
				
				case "prods" :
					prods = (IProdPrice[])obj;
					break;
				
				case "demand" :
					demand = (IDemand)obj;
					break;
				
				case "payinfo" :
					payInfo = (IPayInfo)obj;
					break; 
				
				case "custinfo" :
					custInfo = (ICustInfo2)obj;
					break;
				
				case "servaddr" :
					servAddr = (IAddr2)obj;
					break;
				
				case "mailaddr" :
					mailAddr = (IAddr2)obj;
					break;
				
				case "ordersummary" :
					orderSummary = (IOrderSum)obj;
					break;
				
				case "acctnotes" :
					acctNotes = (IAcctNotes)obj;
					break;
				
				case "receipt" :
					receipt = (IReceipt)obj;
					break;
				
				case "ishightouch" :
					isHighTouch = (bool)obj;
					break;
				
				case "isconfreq" :
					isConfReq = (bool)obj;
					break;
				
				case "phnumber" :
					phNumber = (string)obj;
					break;
				
				case "criteria" :
					criteria = (string)obj;
					break;
				
				case "source" :
					source = (int)obj;
					break;
				
				case "allowlocalconv" :
					allowLocalConv = (bool)obj;
					break;
				
				case "pin" :
					pin = (string)obj;
					break;

				case "discounts" :
					discounts = (IKeyVal[])obj;
					break;
				
				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
		public override IWipStep FirstStep { get { return WorkflowFact.GetNewOrderFirstStep(isConfReq); }}
		#endregion

		#region Constructors
		public NewOrderConversionWip(IUser user) : base(user.DisplayName, user.ClerkId, user.LoginStoreCode)
		{
			isConfReq = isHighTouch = StoreSvc.IsRac_WF(user);
			CurrStep = (WIP.WipStep)FirstStep;
		}
		#endregion

		#region	Methods	

		#endregion

		#region	Implementations
		#endregion	
	}
}