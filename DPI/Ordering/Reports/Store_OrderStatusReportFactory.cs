using System;
using System.Data;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Reports
{	
	public class Store_OrderStatusReportFactory
	{
		public static DataSet GetDataSource(Store_OrderStatusCriteria reportCriteria, 
			IUser user, DateTime startDate, DateTime endDate)
		{				
			switch (reportCriteria)
			{
				case Store_OrderStatusCriteria.All_PayDate :
					return ReportSvc.Store_GetOrderStatus(user, startDate, endDate, "PayDate");					
				
				case Store_OrderStatusCriteria.All_AccountStatus :					
					return ReportSvc.Store_GetOrderStatus(user, startDate, endDate, "AccountStatus");					
				
				case Store_OrderStatusCriteria.Active_PayDate :					
					return ReportSvc.Store_GetOrderStatus(user, startDate, endDate, "PayDate", "AccountStatus = 'Active'");					
				
				case Store_OrderStatusCriteria.Disconnected_PayDate :
					return ReportSvc.Store_GetOrderStatus(user, startDate, endDate, "PayDate", "AccountStatus = 'Disconnected'");					
				
				case Store_OrderStatusCriteria.Pending_PayDate :
					return ReportSvc.Store_GetOrderStatus(user, startDate, endDate, "PayDate", "AccountStatus = 'Pending Order'");					
				
				case Store_OrderStatusCriteria.Active_DueDate :	
					return ReportSvc.Store_GetActiveDueDate(user, startDate, endDate, "Due_Date", "AccountStatus = 'Active'");
				
				default:
					throw new ApplicationException("Unknow store order status criteria: " + reportCriteria.ToString());
			}
		}
		public static string GetTitle(Store_OrderStatusCriteria reportCriteria)
		{
			switch (reportCriteria)
			{
				case Store_OrderStatusCriteria.All_PayDate :
					return "Customers by Order Date";
				
				case Store_OrderStatusCriteria.All_AccountStatus :					
					return "Customers by Account Status";
				
				case Store_OrderStatusCriteria.Active_PayDate :					
					return "Active Customers by Order Date";
				
				case Store_OrderStatusCriteria.Disconnected_PayDate :
					return "Disconnected Customers by Order Date";
				
				case Store_OrderStatusCriteria.Pending_PayDate :
					return "Pending Customers by Order Date";					
				
				case Store_OrderStatusCriteria.Active_DueDate :	
					return "Active Customers by Due Date";
				
				default:
					throw new ApplicationException("Unknow store order status criteria: " + reportCriteria.ToString());
			}
		}
		public static ArrayList GetInstructions(Store_OrderStatusCriteria reportCriteria)			
		{				
			ArrayList ar = new ArrayList();
			switch (reportCriteria)
			{
				case Store_OrderStatusCriteria.All_PayDate :
				{
					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
					ar.Add(@"Select today\'s date to get the most up to date list.");
					return ar;
				}
				case Store_OrderStatusCriteria.All_AccountStatus :
				{
					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
					ar.Add(@"Select today\'s date to get the most up to date list.");
					return ar;
				}
				case Store_OrderStatusCriteria.Active_PayDate :	
				{
					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
					ar.Add(@"Select today\'s date to get the most up to date list.");
					return ar;
				}
				case Store_OrderStatusCriteria.Disconnected_PayDate :
				{
					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
					ar.Add(@"Select today\'s date to get the most up to date list.");
					return ar;
				}
				case Store_OrderStatusCriteria.Pending_PayDate :
				{
					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
					ar.Add(@"Select today\'s date to get the most up to date list.");
					return ar;
				}
				case Store_OrderStatusCriteria.Active_DueDate :	
				{
					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
					ar.Add(@"Select today\'s date to get the most up to date list.");
					return ar;
				}
				default:
					throw new ApplicationException("Unknow store order status criteria: " + reportCriteria.ToString());
			}
		}
	}
}