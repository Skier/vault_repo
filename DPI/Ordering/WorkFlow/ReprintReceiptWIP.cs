using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class ReprintReceiptWIP : WIP	
	{
	/*
		wip["IsHighTouch"]) - use storecode of demand to get corporation and use RACWF
		(IReceipt)wip["receipt"] - see how code constructs receipt in each workflow involved
		(ICustInfo)wip["custinfo"] - use Consumer of demand
		--(IOrderSummary2)wip["ordersummary"] - use demand.ordersummary
		(IPayInfo)wip["payinfo"] - use payinfo of demand
	     (IAcctInfo)wip["acctinfo"] - use billpayer of demand  for monthly payment
	 */

	   /*		Data		*/
		int				tran;
		DateTime		toDate;
		DateTime		fromDate;
		IDemand			demand;
	    bool			isHighTouch;
		IReceipt		receipt;
		ICustInfo		custInfo;
		IPayInfo	payInfo;
		IOrder[]		orders;
		IOrder          order;
		IAcctInfo		acctInfo;
		IOrderSum		ordSum;
		ICustInfoExt    custInfoExt;
		bool		    allowLocalConv;

	#region	Properties
		public override IWipStep FirstStep { get { return WorkflowFact.ReprintReceiptFirstStep(); }}
	#endregion	
	
		/*		Constructors		*/
		public ReprintReceiptWIP(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
		{
			CurrStep = (WIP.WipStep)FirstStep;
		}

		protected override object find(string attr)
		{
			if (attr == null)
				return null;

			switch(attr.ToLower())
			{
				case "tran" :
					return tran;

				case "todate" :
					return toDate;

				case "fromdate" :
					return fromDate;

				case "demand" :
					return demand;

				case "ishightouch" :
					return isHighTouch;

				case "receipt" :
					return receipt;

				case "custinfo" :
					return custInfo;

				case "payinfo" :
					return payInfo;

				case "orders" :
					return orders;

				case "acctinfo" :
					return acctInfo;

				case "ordersummary" : 
					return ordSum;
					
				case "custinfoext" : 
					return custInfoExt;

				case "order" :
					return order;

				case "allowlocalconv" :
					return false;

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
				case "tran":
				{
					tran = (int)obj;
					break;
				}
				case "todate":
				{
					toDate = (DateTime)obj;
					break;
				}
				case "fromdate":
				{
					fromDate = (DateTime)obj;
					break;
				}
				case "demand" :
				{
					demand = (IDemand)obj;
					break;
				}
				case "ishightouch" :
				{
					isHighTouch = (bool)obj;
					break;
				}
				case "receipt" :
				{
					receipt = (IReceipt)obj;
					break;
				}
				case "custinfo" :
				{
					custInfo = (ICustInfo)obj;
					break;
				}
				case "payinfo"	:
				{
					payInfo = (IPayInfo)obj;
					break;
				}
				case "orders"	:
				{
					orders = (IOrder[])obj;
					break;
				}
				case "acctinfo" :
				{
					acctInfo = (IAcctInfo)obj;
					break;
				}
				case "ordersummary" :
				{
					ordSum = (IOrderSum)obj;
					break;
				}
				case "custinfoext" : 
				{
					custInfoExt = (ICustInfoExt)obj;
					break;
				}
				case "order" :
				{
					order = (IOrder)obj;
					break;
				}
				case "allowlocalconv" :
				{
					allowLocalConv = (bool)obj;
					break;
				}
				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
	}
}