using System;

using DPI.Interfaces;

namespace DPI.Components
{
	public class TranFactory
	{
		public static IPayInfoTran GetTran(int tranId, PayInfoSource source) 
		{
			if (tranId < 1)
				throw new  ArgumentException("Tran id must be a positive number");

			UOW uow = null;

			try
			{
				uow = new UOW();

				switch (source)
				{
					case PayInfoSource.FinProd :
						return FinancialProdTrans.find(uow, tranId);

					case PayInfoSource.Verifone:
						return Verifone_Transaction.find(uow, tranId);

					case PayInfoSource.Wireless :
						return Wireless_Transactions.find(uow, tranId);

					default :
						throw new ArgumentException("Uknown PayInfoSource: " + source.ToString());
				}
			}
			finally
			{
				uow.close();
			}
		}
	}
}