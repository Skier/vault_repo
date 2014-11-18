using System;
using System.Data;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Reports
{	
	public class Store_CustomerListReportFactory
	{		
		public static DataSet GetDataSource(Store_CustomerListCriteria reportCriteria, 
			IUser user, DateTime startDate, DateTime endDate)
		{				
			switch (reportCriteria)
			{
				case Store_CustomerListCriteria.Active_ActiveDate :
					return ReportSvc.Store_GetCustomerList(user, startDate, endDate, "ActivDate", 
						"ActivDate >= '" + startDate.ToShortDateString() + "' AND ActivDate <= '" + endDate.ToShortDateString() + "' AND AccountStatus = 'Active'");
				case Store_CustomerListCriteria.Disconnected_DueDate :
					return ReportSvc.Store_GetCustomerList(user, startDate, endDate, "Due_Date DESC", 
						"Due_Date >= '" + startDate.ToShortDateString() + "' AND Due_Date <= '" + endDate.ToShortDateString() + "' AND AccountStatus = 'Disconnected'");
				default:
					throw new ApplicationException("Unknow store customer list criteria: " + reportCriteria.ToString());
			}
		}
		public static string GetTitle(Store_CustomerListCriteria reportCriteria)
		{
			switch (reportCriteria)
			{
				case Store_CustomerListCriteria.Active_ActiveDate :
					return "Active Customers by Active Date";
				case Store_CustomerListCriteria.Disconnected_DueDate :
					return "Disconnected Customers by Due Date";
				default:
					throw new ApplicationException("Unknow store customer list criteria: " + reportCriteria.ToString());
			}
		}
		public static ArrayList GetInstructions(Store_CustomerListCriteria reportCriteria)			
		{				
			ArrayList ar = new ArrayList();
			switch (reportCriteria)
			{
				case Store_CustomerListCriteria.Active_ActiveDate :
					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
					ar.Add(@"Select today\'s date to get the most up to date list.");
					return ar;
				case Store_CustomerListCriteria.Disconnected_DueDate :
					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
					ar.Add(@"Select today\'s date to get the most up to date list.");
					return ar;
				default:
					throw new ApplicationException("Unknow store customer list criteria: " + reportCriteria.ToString());
			}
		}
	}
}