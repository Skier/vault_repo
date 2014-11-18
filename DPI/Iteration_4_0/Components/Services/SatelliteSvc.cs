using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;
using DPI.Interfaces.ServicePower;

namespace DPI.Services
{
	public class SatelliteSvc
	{
		public static void PreSave(IMap imap)
		{
			UOW uow = null; 
		
			try
			{
				uow = new UOW(imap, "SatelliteSvc.NewCard()"); 
	
				uow.BeginTransaction(); 
				uow.commit();   // pre-saves Bus objects.
			}
			finally
			{
				uow.close();
			}
		}
		public static IAppointmentSearchResponse[] GetAptSearch(IMap imap, IUser user, IPayInfo payInfo)
		{

			IWebSvcQueue logQue = null;

			try
			{
				logQue = WebServSvc.SetupEntry(imap, payInfo, user, "SatelliteSvc.GetAptSearch");
				logQue.Status = WebSvcQueueStatus.Error.ToString();

				
				logQue.Status = WebSvcQueueStatus.Completed.ToString();
				return null;				
			}
			finally
			{
				WebServSvc.SaveEntry(logQue);
			}
			
		}
		public static ISatelliteReceipt CallAssignment(IMap imap, IUser user, IPayInfo payInfo,
										IAppointmentSearchResponse resp)
		{
			return null;
		}
		
	}
}