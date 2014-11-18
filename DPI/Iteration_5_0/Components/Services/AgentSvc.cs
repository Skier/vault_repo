using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class AgentSvc
	{
		public static IAgentRegistration GetNewAgentReg(IMap imap)
		{
			return  new AgentRegistration(imap);	
		}
		public static void SaveRegistration(IMap imap)
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap);
				uow.commit();
			}
			finally
			{
				uow.close();
			}
		}

		public static string GetSalesRptByStoreData(string header, int corpId, string state, 
			string dma, DateTime startDate, DateTime endDate, string tranType)
		{
			return AgentExtranetRptWrapper.GetSalesRptByStoreData(header, corpId, state, dma, 
				startDate, endDate, tranType);
		}
		public static string GetSalesRptByStateData(string header, int corpId, string state, 
			string dma, DateTime startDate, DateTime endDate, string tranType)
		{
			return AgentExtranetRptWrapper.GetSalesRptByStateData(header, corpId, state, dma, 
				startDate, endDate, tranType);
		}


	}
}