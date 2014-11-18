using System;

using DPI.Interfaces;
using DPI.Components.EPSolutions;

namespace DPI.Components
{
	public abstract class EnergyProvider
	{
		public abstract IReceipt Send(IUOW uow, IUser user, string action, object[] objects);
		public abstract QuoteResponse GetQuote(IWebSvcQueue logQue, IQuoteReq quote);
		//public abstract FindLocationResponse FindLocationByAddress(IAddressReq address);
		public virtual FindLocationResponse FindLocationByAddress(IAddressReq address)
		{
			return null;
		}
		public virtual FindLocationResponse FindLocationBySiteCode(string siteCode)
		{
			return	null;
		}
		public virtual EnrollmentResponse EnrollPrepaidCustomer(IEnergy_CustData customer)
		{
			return null;
		}
		public virtual IEnergyRcpt RegisterCustomer(UOW uow, IWebSvcQueue logQue, IEnergy_CustData customer, IPaymentReq payReq)
		{
			return null;
		}
		public virtual BalanceResponse GetAccountBalance(string acctNum)
		{
			return null;
		}
		public virtual IEnergyRcpt MakePayment(IWebSvcQueue logQue, IPayInfo payInfo, string acctNumber)
		{
			return null;
		}
	}
}