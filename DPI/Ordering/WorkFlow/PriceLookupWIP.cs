using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class PriceLookupWIP : WIP	
	{
		#region Data
		string         zip;
		IILECInfo      selectedilec;
		IILECInfo[]    avaIlecs;
		IProdPrice     selectedBasicService;
		IProdPrice[]   topProducts;
		IProdPrice[]   prods;
		IOrderSummary2 orderSummary;
		IDemand		   demand;
		string         criteria;
		int            source;
		bool		   allowLocalConv;
		IKeyVal[]	   discounts;

		#endregion	
	
		#region	Properties
		public override IWipStep FirstStep { get { return WorkflowFact.PriceLookupFirstStep(); }}
		#endregion		
	
		#region	Constructors
		public PriceLookupWIP(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
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

				case "ordersummary" :
					return orderSummary;

				case "demand" :
					return demand;
					
				case "salesidrequired" :
					return false;

				case "isconfreq" :
					return false;
				
				case "dmdtype" :
				{
					if (demand != null)
						return demand.DmdType;

					return DemandType.PriceLU.ToString();
				}
				
				case "acctnotes" :
					return null;
				
				case "criteria" :
					return criteria;
									
				case "source" :
					return source;

				case "showsource" :
					return false;

				case "allowlocalconv" :
					return false;

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
				{
					zip = (string)obj;
					break;
				}
				case "selectedilec" :
				{
					selectedilec = (IILECInfo)obj;
					break;
				}
				case "availecs" :
				{
					avaIlecs = (IILECInfo[])obj;
					break;
				}
				case "selectedbasicservice" :
				{
					selectedBasicService = (IProdPrice)obj;
					break;
				}
				case "topproducts" :
				{
					topProducts = (IProdPrice[])obj;
					break;
				}
				case "prods" :
				{
					prods = (IProdPrice[])obj;
					break;
				}
				case "ordersummary" :
				{
					orderSummary = (IOrderSummary2)obj;
					break;
				}
				case "demand" :
				{
					demand = (IDemand)obj;
					break;
				}
				case "criteria" :
				{
					criteria = (string)obj;
					break;
				}
				case "source" :
				{
					source = (int)obj;
					break;
				}
				case "allowlocalconv" :
				{
					allowLocalConv = (bool)obj;
					break;
				}
				case "discounts" :
				{
					discounts = (IKeyVal[])obj;
					break;
				}
				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
		#endregion
	}
}