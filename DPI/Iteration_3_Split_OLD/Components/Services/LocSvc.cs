using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class LocSvc 
	{
		public static string FindState(IMap imap, string zip)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "LocSvc.FindState");
				return DmaZip.getState(uow, zip);
			}
			finally
			{
				uow.close();
				//imap.ClearDomainObjs();
			}
		}
		public static int FindStoreZipId(IMap imap, string storeCode)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "LocSvc.FindStoreZipId");
				StoreLocation sl = StoreLocation.find(uow, storeCode);
				return ((Location)Location.find(uow, sl.Zip)).LocId;
			}
			finally
			{
				uow.close();
			}

		}
		public static int FindLocId(IMap imap, string loc)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "LocSvc.FindLocByNmae");
				return ((Location)Location.find(uow, loc)).LocId;
			}
			finally
			{
				uow.close();
			}
		}
	}
}