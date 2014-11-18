using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class OrgSvc 
	{
		public static string ZipNotFound(IMap imap, string zip)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "ProdSvc.GetILECsAtZip");
				if (DmaZip.checkZip(uow, zip))
					return "No services are available at the Zip " + zip;
				
				return "Zip " + zip + " is not found";
			}
			finally
			{
				uow.close();
			}
		}
		public static IILECInfo[] GetILECsAtZip(IMap imap, string zip)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "ProdSvc.GetILECsAtZip");
				return ILECInfo.getILECs(uow, zip);	
			}
			finally
			{
				uow.close();
			}
		}
		public static IDropDownListItem[] GetDMA()
		{	
			//wrapper do not use UOW and IMAP
			return AgentExtranetRptWrapper.GetDMA();	
		}
		public static IDropDownListItem[] GetDMA(string state)
		{	
			//wrapper do not use UOW and IMAP
			return AgentExtranetRptWrapper.GetDMA(state);	
		}
	}
}