using System;
using System.Data;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Reports
{
	public class Store_CommissionReportFactory
	{
		public Store_CommissionReportFactory()	{}

		public static DataSet GetDataSource(Store_CommissionCriteria reportCriteria, 
			IUser user, DateTime startDate, DateTime endDate)
		{				
			switch (reportCriteria)
			{
				case Store_CommissionCriteria.Local_Commission :
					return ReportSvc.Store_GetCommission(user, startDate, endDate, "Local", null);					
				
				case Store_CommissionCriteria.Cell_Commission :					
					return ReportSvc.Store_GetCommission(user, startDate, endDate, "Cell", null);					
				
				default:
					throw new ApplicationException("Unknow commission criteria: " + reportCriteria.ToString());
			}
		}


		public static string GetTitle(Store_CommissionCriteria reportCriteria)
		{
			switch (reportCriteria)
			{
				case Store_CommissionCriteria.Local_Commission:
					return "Customer Local Phone Commission List";
				case Store_CommissionCriteria.Cell_Commission:					
					return "Customers Cell Phone Commission List";				
				default:
					throw new ApplicationException("Unknow commission criteria: " + reportCriteria.ToString());
			}
		}

		public static ArrayList GetInstructions(Store_CommissionCriteria reportCriteria)			
		{				
			ArrayList ar = new ArrayList();
			switch (reportCriteria)
			{
				case Store_CommissionCriteria.Local_Commission:
					ar.Add(@"Select the date you want to start gathering customer local phone commission information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
					ar.Add(@"Select today\'s date to get the most up to date list.");
					return ar;
				case Store_CommissionCriteria.Cell_Commission:					
					ar.Add(@"Select the date you want to start gathering customer cell phone phone commission information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
					ar.Add(@"Select today\'s date to get the most up to date list.");
					return ar;				
				default:
					throw new ApplicationException("Unknow commission criteria: " + reportCriteria.ToString());
			}
		}
	}
}
