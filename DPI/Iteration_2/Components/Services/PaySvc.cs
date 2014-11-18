using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class PaySvc 
	{
		public static IPayInfo GetNewPayInfo(IMap imap, IDemand dmd, PayInfoClass pc)
		{
				IPayInfo pi  =  GetNewPayInfo(imap, pc);
				pi.ParDemand = dmd;
				return pi;
		}
		public static IPayInfo GetNewPayInfo(IMap imap, PayInfoClass pc)
		{
			return PayInfo.GetPayInfo(imap, pc.ToString());
		} 
		public static IPayInfo[] FindDmdPayInfo(IMap imap, int dmdId)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "PaySvc.FindDmdPayInfo");
				return PayInfo.getDmdPayInfo(uow, dmdId);
			}
			finally
			{
				uow.close();
			}
		}
		public static IPayInfo[] FindStorePayInfo(IMap imap, string storeCode, PaymentStatus paymentStatus)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "PaySvc.FindStorePayInfo");
				return PayInfo.getStorePayInfo(uow, storeCode, paymentStatus);
			}
			finally
			{
				uow.close();
				imap.ClearDomainObjs();
			}
		}
		public static IPayInfo FindPayInfo(IMap imap, int payInfo)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "PaySvc.FindPayInfo");
				return PayInfo.find(uow, payInfo);
			}
			finally
			{
				uow.close();
			}
		}
		public static PaymentType[] GetPymtTypes(IMap imap, string storeCode)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "PaySvc.GetPymtTypes");
				
				return PymtTypeRule.GetPymtTypes(uow, StoreStatsCol.GetCorporation(storeCode).PymtTypeRule);
			}
			finally
			{
				uow.close();
			}
		}
		public static PaymentType[] GetPymtTypes(string storeCode)
		{
			return 	GetPymtTypes( IMapFactory.getIMap(), storeCode);
		}
		public static string GetPaymentType(PaymentType pt)
		{
			switch(pt) 
			{
				case PaymentType.Cash :
					return "Cash";

				case PaymentType.Debit :
					return "Debit Card";		
				
				case PaymentType.Check :
					return "Check";

				case PaymentType.Credit :
					return "Credit Card";

				case PaymentType.TurboCash :
					return "Turbo Cash";

				case PaymentType.MoneyOrder :
					return "Money Order";

				default :
					throw new ArgumentException("Unknown payment type: " + pt.ToString());
			}			
		}
	}
}