using System;
using System.Data;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.Reports
{
	public class PendingTransactionsByStoreRF
	{
		public static DataSet GetDataSource(int corpid, string storeNum, DateTime fromDate)
		{				
			return ReportSvc.PendingTransactionsByStore(corpid, storeNum, fromDate);
		}
		public static string GetTitle(PendingOrderPaymentInfoCriteria reportCriteria)
		{
			switch (reportCriteria)
			{
				case PendingOrderPaymentInfoCriteria.Corporation :
					return "Pending Transactions By Corporation";
				case PendingOrderPaymentInfoCriteria.Store :
					return "Pending Transactions By Store";
				default:
					throw new ApplicationException("Unknow certification result criteria: " + reportCriteria.ToString());
			}
		}
	}
}