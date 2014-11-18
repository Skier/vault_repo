using System;
using System.Reflection;
using CrystalDecisions.CrystalReports.Engine;

using DPI.Components;
using DPI.Interfaces;
using DPI.Ordering;
using DPI.Services;
using DPI.ClientComp;

namespace DPI.Reports 
{
	public class ReceiptFactory
	{
		const string app = "Reports";

		public static ReportClass GetReportClass(IMap imap, IWIP wip, IUser user)
		{
			IReceipt2 receipt =  GetReceipt(imap, wip, user);
			ReportClass rc = GetReport(receipt.CSharpName); 
			if (rc == null)
				throw new ArgumentException("Failed to Load " + receipt.CSharpName);
			
			return ReceiptDSFactory.SetDataSet(wip, user, rc, receipt);
		}
		public static string GetReceiptExportFilename(IWIP wip)
		{
			return Const.VIRTUAL_DIR_TEMP + ((IPayInfo)wip["PayInfo"]).Id.ToString() + ".pdf";
		}
		static IReceipt2 GetReceipt(IMap imap, IWIP wip, IUser user)
		{	
			return ReceiptSvc.GetReceipt(
				imap, user, wip.ProdGroup, wip.Prod, wip.WLProd, wip.Workflow.Name, (bool)wip["IsCompleted"]);
		}
		static ReportClass GetReport(string report)
		{
			return (ReportClass)CLoader.LoadObject(app, report);
		}
	}
}