using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class StoreOrder
	{
		public static IOrder[] GetOrderByStore(UOW uow, IUser user, DateTime from, DateTime to)
		{
			ArrayList ar = new ArrayList();
			IDemand[] dmds = Demand.GetForStoreCode(uow, user.LoginStoreCode, from, to);
			
			for (int i = 0; i < dmds.Length; i++)
			{	 
				if ((dmds[i].BillPayer) < 1)
					continue;
				
				IPayInfo[] pis = PayInfo.getDmdPayInfo(uow, dmds[i].Id);
				
				for (int j = 0; j < pis.Length; j++)
				{
					IOrder ord = BuildOrder(uow, user, dmds[i],  pis[j]);
					if (ord != null)
						ar.Add(ord);
				}
			}	
	
			IOrder[] orders = new IOrder[ar.Count];
			ar.CopyTo(orders);
			return orders;
		}
		static IOrder BuildOrder(UOW uow, IUser user, IDemand demand, IPayInfo payInfo)
		{
			Order order = new Order(demand.Id, demand.DmdType, payInfo.Id, payInfo.PayDate);	
			order.AccNumber = demand.BillPayer;
		
			order.ConfNumber = payInfo.VFConf;
			if (payInfo.IsConfReq)
				order.ConfNumber = payInfo.Id.ToString();	
				
			if (demand.DmdType.ToLower() == DemandType.Monthly.ToString().ToLower())
			{
				if (demand.BillPayer < 1)
				{
					DPI_Err_Log.AddLogEntry(uow, "StoreOrder.BuildOrder()",  
						"Store code: " + user.LoginStoreCode + ", Clerk id: " + user.ClerkId, 
						"No billpayer found for demand: " + demand.Id.ToString());
					return null;
				}
				order.Setup(CustData.find(uow, demand.BillPayer));
				return order; 
			}
			if (demand.DmdType.ToLower() == DemandType.NewPymt.ToString().ToLower())
				return order;

			if (demand.DmdType.ToLower() == DemandType.New.ToString().ToLower())
			{
				if (demand.ConsId < 1)
				{
					DPI_Err_Log.AddLogEntry(uow, "StoreOrder.BuildOrder()", 
						"Store code: " + user.LoginStoreCode + ", Clerk id: " + user.ClerkId,  
						"No custInfo found for demand: " + demand.Id.ToString());
					return null;
				}
				order.Setup(CustInfo.find(uow, demand.ConsId));
				return order;
			}

			DPI_Err_Log.AddLogEntry(uow, "StoreOrder.BuildOrder()",  
				"Store code: " + user.LoginStoreCode + ", Clerk id: " + user.ClerkId, 
				"Unkown Demand type: '" + demand.DmdType + "'. Demand id: " + demand.Id.ToString());
			
			return null;
		}
	}
}