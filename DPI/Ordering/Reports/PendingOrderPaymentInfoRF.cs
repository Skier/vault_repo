using System;
using System.Data;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.Reports
{
	public class PendingOrderPaymentInfoRF
	{
//		public static DataSet GetDataSource(IUser user)
//		{				
//			return ReportSvc.CertResultsGetByStore(user);			
//		}
		public static DataSet GetDataSource(int corpid)
		{				
			return ReportSvc.PendingOrderPaymentInfoByCorp(corpid);
		}
		public static string GetTitle(PendingOrderPaymentInfoCriteria reportCriteria)
		{
			switch (reportCriteria)
			{
				case PendingOrderPaymentInfoCriteria.Corporation :
					return "Pending Order Payment Info by Corporation";
				case PendingOrderPaymentInfoCriteria.Store :
					return "Pending Order Payment Info by Store";
				default:
					throw new ApplicationException("Unknow certification result criteria: " + reportCriteria.ToString());
			}
		}
	}
}