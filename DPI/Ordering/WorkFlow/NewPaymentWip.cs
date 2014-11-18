using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class NewPaymentWip : WIP	
	{
		/*		Data		*/
		string         zip;
		IILECInfo      selectedilec;
		IILECInfo[]    avaIlecs;
		IProdPrice     selectedBasicService;
		IProdPrice[]   topProducts;
		IProdPrice[]   prods;
		IPayInfo  payInfo;
		ICustInfo      custInfo;
		IOrderSum      orderSummary;
		IAcctNotes     acctNotes;
		IReceipt       receipt;
		IDemand		   demand;
		bool		   isHighTouch;
		bool           isHighTouchDisallowed = true;
		bool           isConfReq; // this particular order is pending
		string		   phNumber;
		string         criteria;
		int            source;
		bool		   allowLocalConv;
		string		   pin;
		IKeyVal[]	   discounts;

		public override IWipStep FirstStep { get { return WorkflowFact.GetNewPaymentFirstStep(); }}
		/*		Constructors		*/
		public NewPaymentWip(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
		{
			CurrStep = (WIP.WipStep)FirstStep;
		}
		/*		Methods		*/
		protected override object find(string attr)
		{
			if (attr == null)
				return null;

			switch(attr.ToLower())
			{
				case "zip" :
					return zip;

				case "ordertype" :
					return OrderType.New;

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

				case "payinfo" :
					return payInfo;
				
				case "custinfo" :
					return custInfo;

				case "ordersummary" :
					return orderSummary;

				case "acctnotes" :
					return acctNotes;

				case "receipt" :
					return receipt;

				case "servaddr" :
					return null;
				
				case "mailaddr" :
					return null;
 
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

					return DemandType.NewPymt.ToString();
				}

				case "phnumber" :
					return phNumber;

				case "criteria" :
					return criteria;
					
				case "source" :
					return source;

				case "showsource" :
					return false;

				case "allowlocalconv" :
					return false;

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
					custInfo = (ICustInfo)obj;
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
				
				case "ordertype" :
					throw new ArgumentException("Order type cannot be changed");

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
	}
}