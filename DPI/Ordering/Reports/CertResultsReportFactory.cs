using System;
using System.Data;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.Reports
{	
	public class CertResultsReportFactory
	{
		public static DataSet GetDataSource(IUser user)
		{				
			return ReportSvc.CertResultsGetByStore(user);			
		}
		public static DataSet GetDataSource(int corpid)
		{				
			return ReportSvc.CertResultsGetByCorp(corpid);
		}
		public static string GetTitle(CertResultCriteria reportCriteria)
		{
			switch (reportCriteria)
			{
				case CertResultCriteria.Store :
					return "DPI Certification Results by Store";
				case CertResultCriteria.Corporation :					
					return "DPI Certification Results by Corporation";
				default:
					throw new ApplicationException("Unknow certification result criteria: " + reportCriteria.ToString());
			}
		}
//		public static ArrayList GetInstructions(Store_OrderStatusCriteria reportCriteria)			
//		{				
//			ArrayList ar = new ArrayList();
//			switch (reportCriteria)
//			{
//				case Store_OrderStatusCriteria.All_PayDate :
//					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
//					ar.Add(@"Select today\'s date to get the most up to date list.");
//					return ar;
//				case Store_OrderStatusCriteria.All_AccountStatus :					
//					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
//					ar.Add(@"Select today\'s date to get the most up to date list.");
//					return ar;
//				case Store_OrderStatusCriteria.Active_PayDate :					
//					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
//					ar.Add(@"Select today\'s date to get the most up to date list.");
//					return ar;
//				case Store_OrderStatusCriteria.Disconnected_PayDate :
//					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
//					ar.Add(@"Select today\'s date to get the most up to date list.");
//					return ar;
//				case Store_OrderStatusCriteria.Pending_PayDate :
//					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
//					ar.Add(@"Select today\'s date to get the most up to date list.");
//					return ar;
//				case Store_OrderStatusCriteria.Active_DueDate :	
//					ar.Add(@"Select the date you want to start gathering customer information. For instance, if you want a list of \'all customers\', select your start date one month prior to taking dPi payments.");
//					ar.Add(@"Select today\'s date to get the most up to date list.");
//					return ar;
//				default:
//					throw new ApplicationException("Unknow store order status criteria: " + reportCriteria.ToString());
//			}
//		}
	}
}