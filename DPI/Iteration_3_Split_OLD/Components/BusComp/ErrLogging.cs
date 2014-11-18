using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{
	public class ErrLogging 
	{
		public static void LogError(string subSys, string user, string message)  
		{
			UOW   uow = null; 
			IMap imap = null;
			try
			{
				imap = new IdentityMap();
				uow = new UOW(imap, "ErrLogging.LogError"); 

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
				//imap.ClearDomainObjs();
				uow.close();
			}					
		}
	}
}