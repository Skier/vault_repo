using System;
using DPI.Interfaces;
using DPI.Services;
 
namespace DPI.ClientComp
{
	public class WipOrderSummary
	{
		public static void CheckOrderSummary(IMap imap)
		{
			WIP wip = (WIP)imap.find(WIP.IKeyS);
           
			if (wip["ordersummary"] != null)
				return;
	
			wip["ordersummary"] = CustSvc.GetOrderSummary(imap, 
				(IProdPrice[])wip["prods"], 
				(string)wip["Zip"],
				(IILECInfo)wip["SelectedIlec"],
				(string)wip["DmdType"], 
				(OrderType)wip["ordertype"]);
		
			if (wip["ordersummary"] == null)
				throw new ApplicationException("Order summary is not found");

			wip["Demand"] = ((IOrderSum)wip["ordersummary"]).Demand;
			((IDemand)wip["Demand"]).ConsumerAgent = wip.ClerkId;
			((IDemand)wip["Demand"]).StoreCode = wip.StoreCode;
		}
	}
}