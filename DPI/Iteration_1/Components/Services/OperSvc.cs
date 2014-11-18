using System;
using System.Collections;

using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class OperSvc 
	{
		public static void StartThreads(string sender)
		{
			try
			{
				WQSpinner.Load();
				OperationMessenger.OnRestart();
				
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.OperSvc.ToString(), sender, "Threads started " + DateTime.Now.ToString());
			}
			catch (Exception ex) 
			{	
				LogError("OperSvc.StartThreads", ex, sender);
			} 
		}
		public static void StopThreads(string sender)
		{
			try
			{
				OperationMessenger.OnStop();
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.OperSvc.ToString(), sender, "Threads stopped " + DateTime.Now.ToString());
			}
			catch (Exception ex) 
			{	
				LogError("OperSvc.StopThreads", ex, sender);
			}
		}
		public static void RefreshData(string sender)  
		{
			try
			{
				object o = ProdInfoCol.Comps;
				o = PermissionCol.GetPerms();
				o = ProdSubClassCol.GetSubClasses();
				o = ProdTypeCol.GetProdTypes();
				o = StoreStatsCol.GetCorp();
				
				OperationMessenger.OnRefreshData();			
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.OperSvc.ToString(), sender, "DataRefreshed " + DateTime.Now.ToShortDateString());
			}
			catch (Exception ex) 
			{	
				LogError("OperSvc.RefreshData", ex, sender);
			}
		}
		public static void StartEazyTax()
		{
			object o = TaxWrapper.EZTax;
		}
		public static void DisposeEazyTax()
		{
			TaxWrapper.Dispose();
		}
		static void LogError(string method, Exception ex, string sender)
		{
			ErrLogSvc.LogError(method, sender, "Error: " + ex.Message + ", Stack trace: " + ex.StackTrace);
		}
		public static void SetAcctActivityLog(IMap imap, int accNumber, string activity)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap, "Account Activity log");
				Account_ActivityLog log = new Account_ActivityLog(uow, Const.YONIX_USERID, accNumber, activity, Const.YONIX_DEPARTMENT);
			}
			finally 
			{
				uow.close();
			}
		}
	}
}