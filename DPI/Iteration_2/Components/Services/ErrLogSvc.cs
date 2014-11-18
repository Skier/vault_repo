using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class ErrLogSvc 
	{
		public static void LogError(IMap imap, string subSys, string user, string message)  
		{
			UOW uow = null; 
			try
			{
				if (imap == null)
					uow = new UOW(); 
				else
					uow = new UOW(imap, "ErrLogSvc.LogError"); 

				if (message == null)
					throw new ApplicationException("Error message is required");

				DPI_Err_Log eLog = new DPI_Err_Log(uow);
            
				eLog.Subsys = subSys;
				eLog.DPI_User = user;
				eLog.DateTime = DateTime.Now;
				eLog.Message = message;
				eLog.add();
			}
			catch (Exception e) 
			{
				string s = e.Message;
			}
			finally
			{	
				uow.close();
			}					
		}
		public static void LogError(string subSys, string user, string message)  
		{
			UOW uow = null; 
			try
			{
				uow = new UOW(); 

				if (message == null)
					throw new ApplicationException("Error message is required");

				if (message.Trim().Length == 0)
					throw new ApplicationException("Error message is required");

				DPI_Err_Log eLog = new DPI_Err_Log(uow);
            
				eLog.Subsys = subSys;
				eLog.DPI_User = user;
				eLog.DateTime = DateTime.Now;
				eLog.Message = message;
				eLog.add();
			}
			catch (Exception e) 
			{
				string s = e.Message;
			}
			finally
			{	
				uow.close();
			}					
		}
	}
}