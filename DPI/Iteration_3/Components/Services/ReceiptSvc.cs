using System;
using System.Text;

using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class ReceiptSvc 
	{
		public static IReceipt2 GetReceipt(IMap imap, IUser user, string prodGroup, int prod, int wlProd, string workflow, bool isCompleted)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "ReceiptSvc.FindReceipt");
				
				return Receipt2.find(uow, 
					prodGroup, 
					prod, 
					wlProd, 
					StoreStatsCol.GetCorporation(user.LoginStoreCode).CorpID, 
					user.LoginStoreCode,
					workflow,
					isCompleted);
			}
			finally
			{
				uow.close();
			}
		}
		public static string ReplaceVars(IWIP wip, string text)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(text);
			// magic takes place here
			return sb.ToString();
		}
	}
}