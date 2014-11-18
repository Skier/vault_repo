using System;
using System.Collections;
using System.Configuration;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class StoreSvc
	{
		public static ICorp GetCorp(int id)
		{
			return StoreStatsCol.GetCorp(id);  
		}
		public static bool IsRac_WF(IUser user)
		{
			return GetCorporation(user.LoginStoreCode).RAC_WF;
		}
		public static bool ShowEndOfDayRpt(IUser user)
		{
			string[] corps = ConfigurationSettings.AppSettings["EndOfDayRptCorps"]
				.Split(new char[] {',', ';'});
			
			for (int i = 0; i < corps.Length; i++)
				if (GetCorporation(user.LoginStoreCode).CorpID == int.Parse(corps[i].Trim()))
					return true;
			
			return false;
		}
		public static ICorporation GetCorporation(int id)
		{
			return StoreStatsCol.GetCorporation(id);
		}
		public static ICertResult[] GetCertResultStore(IMap imap, string storeCode)
		{
			if (storeCode == null)
				throw new ArgumentException("StoreCode is required");
			
			UOW uow = null;
		
			try
			{
				uow = new UOW(imap);
				uow.Service = "StoreSvc.GetCertResultStore";
				return CertResult.getAll_Store(uow, storeCode);
			}
			finally
			{	
				uow.close();
			}
		}
		public static ICertResult[] GetCertResultCorp(IMap imap, int corpId)
		{
			if (corpId == 0)
				throw new ArgumentException("CorpId is required");
			
			UOW uow = null;
		
			try
			{
				uow = new UOW(imap);
				uow.Service = "StoreSvc.GetCertResultCorp";
				return CertResult.getAll_Corp(uow, corpId);
			}
			finally
			{	
				uow.close();
			}
		}
		public static IDemand[] GetDemandByStore(IMap imap, string storeCode, DateTime from, DateTime to)
		{
			if (storeCode.Trim().Length < 1)
				throw new ArgumentException("Store Code is required");

			UOW uow = null;

			try
			{
				uow = new UOW(imap);
				uow.Service = "StoreSvc.GetDemandByStore";
				return Demand.GetForStoreCode(uow, storeCode, from, to);
			}
			finally
			{
				uow.close();
			}
		}
		
		public static IOrder[]  GetOrderByStore(IMap imap, IUser user, DateTime from, DateTime to)
		{
			if (user.LoginStoreCode == null)
				throw new ArgumentException("Store Code is required");

			if (user.LoginStoreCode.Trim().Length < 1)
				throw new ArgumentException("Store Code is required");

			UOW uow = new UOW(imap);
			uow.Service = "StoreSvc.GetOrderByStore";
			
			try
			{
				IOrder[] orders = StoreOrder.GetOrderByStore(uow, user, from, to);
				return orders;					
			}
			finally
			{
				imap.ClearDomainObjs();
				uow.close();
			}
		}
		public static void SaveCertResult(IMap imap, string coWorker, bool passed, string name, string storeCode, int type)
		{
			UOW uow = null;
			CertResult cr = null;

			try
			{	
				uow = new UOW(imap);	
				uow.BeginTransaction();
	
				cr = new CertResult(uow);

				switch (type)
				{
					case 1:
						cr.Type = "Debit Card";
						break;

					case 2:
						cr.Type = "Basic Web";
						break;

					case 3:
						cr.Type = "Slingshot";
						break;

					default :
						throw new ArgumentException("Uknown Quiz Type: " + type.ToString());
				}	
				
				cr.CertDate = DateTime.Now;
				cr.Coworker = coWorker;
				cr.Name = name;
				cr.Status = passed ? "Passed" : "Failed";
				cr.StoreCode = storeCode;

				uow.commit();
			}
			finally
			{
				uow.close();
			}
		}
		public static ICorporation GetCorporation(string storeCode)
		{
			return StoreStatsCol.GetCorporation(storeCode);
		}
		public static IStoreStats GetStoreStats(string storeCode)
		{
			return StoreStatsCol.GetStoreStat(storeCode);
		}
		public static bool IsActiveStore(IMap imap, string storeCode)
		{
			if (storeCode == null)
				throw new ArgumentException("StoreCode is required");
			
			UOW uow = null;
		
			try
			{
				uow = new UOW(imap);
				uow.Service = "StoreSvc.IsActiveStore";

				StoreLocation storeLocation = StoreLocation.find(uow, storeCode);
				if (storeLocation == null)
					return false;
				return storeLocation.Active;
			}
			finally
			{	
				imap.ClearDomainObjs();	
				uow.close();
			}
		}

		public static string GetStoreCode(IMap imap, int corpId, string storeNumber)
		{
			UOW uow = new UOW(imap);
			uow.Service = "StoreSvc.GetStoreCode";

			if (corpId == 0)
				throw new ArgumentException("CorpId is required.");

			if (storeNumber == "")
				throw new ArgumentException("Store number is required.");
		
			try
			{				
				StoreLocation storeLocation = StoreLocation.find(uow, corpId, storeNumber);
				if (storeLocation == null)
					return "";
				return storeLocation.StoreCode;
			}
			finally
			{	
				imap.ClearDomainObjs();	
				uow.close();
			}			
		}
		public static string GetStoreCode(int corpId, string storeNumber)
		{
			IMap imap = IMapFactory.getIMap();
			UOW uow = new UOW(imap);
			uow.Service = "StoreSvc.GetStoreCode";

			if (corpId == 0)
				throw new ArgumentException("CorpId is required.");

			if (storeNumber == "")
				throw new ArgumentException("Store number is required.");
		
			try
			{				
				StoreLocation storeLocation = StoreLocation.find(uow, corpId, storeNumber);
				if (storeLocation == null)
					return "";
				return storeLocation.StoreCode;
			}
			finally
			{	
				imap.ClearDomainObjs();	
				uow.close();
			}			
		}
		// reprint receipt methods //
//		public static IPayInfo[] FindPayInfoByDmd(IMap imap, IDemand dmd)
//		{
//			UOW uow = null;
//
//			try
//			{
//				uow = new UOW(imap);
//				uow.Service = "StoreSvc.FindPayInfo";
//				IPayInfo[] payinfo = null;
//				for (int i = 0; i < payinfo.Length; i++)
//				{
//					if(i == dmd.Id)
//					{
//						payinfo = PayInfo.getDmdPayInfo(uow, dmd.Id);				
//					}
//				}
//				return payinfo;
//			}
//			finally
//			{
//				imap.ClearDomainObjs();
//				uow.close();
//			}
//		}

		public static ICustInfo FindCustInfoByDmd(IMap imap, IOrder[] ord, IDemand dmd)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap);
				uow.Service = "StoreSvc.FindCustInfo";
				ICustInfo ci = null;
				for (int i = 0; i < ord.Length; i++)
				{
					if(i == dmd.Id)
					{
						ci =  CustSvc.GetCustInfoExtPend(imap, dmd);
					}
				}
				return  ci ;
			}
			finally
			{
				uow.close();
			}
		}
		public static bool IsHighTouch(IMap imap, IUser user)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap);
				uow.Service = "StoreSvc.IsHighTouch";
				bool ht = StoreSvc.IsRac_WF(user);
				return ht;
			}
			finally
			{
				uow.close();
			}
		}

		public static IDemand FindDemand(IMap imap, int dmdId)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap);
				uow.Service = "StoreSvc.FindDemand";
				return Demand.find(uow, dmdId);
			}
			finally
			{
				uow.close();
			}
		}

		public static bool IsAllowedLocalConv(IMap imap, string storeCode)
		{
			return StoreStatsCol.GetCorporation(storeCode).AllowLocalConv;
		}
		public static bool ShowSource(IMap imap, string storeCode)
		{
			if (storeCode == null)
				throw new ArgumentException("StoreCode is required");
			
			UOW uow = null;
		
			try
			{
				uow = new UOW(imap);
				uow.Service = "StoreSvc.ShowSource";

				StoreLocation sl = StoreLocation.find(uow, storeCode);
				if (sl == null)
					return false;
				return sl.ShowSource;
			}
			finally
			{	
				uow.close();
			}
		}

	}
}