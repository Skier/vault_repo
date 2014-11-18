using System;
using DPI.Interfaces;
using DPI.Components;
using DPI.Services;

namespace DPI.Services
{
	public class LocalTransactionSvc
	{
		public static ILocalTransactionInfo[] GetVoidableTransactions(IMap imap, string storecode, DateTime paydate)
		{
			UOW uow = null;			

			try
			{
				uow = new UOW(imap, "LocalTransactionSvc.GetVoidableTransactions");			
				return LocalTransaction.ToInfo(LocalTransaction.GetVoidableTransactions(uow, storecode, paydate));
			}
			finally
			{
				uow.close();
			}
		}
		public static ILocalTransactionInfo FindTran(IMap imap, int tran)
		{
			UOW uow = null;

			try
			{	
				uow = new UOW(imap, "Find xact");
				return LocalTransaction.find(uow, tran).LocalTransactionInfo;
			}
			finally
			{
				uow.close();
			}
		}
		public static bool VoidTransaction(IMap imap, IUser user, int tran_id)
		{
			UOW uow = null;

			try
			{	
				uow = new UOW(imap, "LocalTransactionSvc.VoidTransaction");

				if (tran_id == 0)
					throw new ArgumentException("Trans id is required");		
				
				bool ret = LocalTransaction.VoidTransaction(uow, tran_id, 1);
				
				WebServSvc.PresetVoid(uow, user, tran_id); 
				
				uow.commit();
				return ret;
			}
			finally
			{
				uow.close();
			}
		}
	}
}
