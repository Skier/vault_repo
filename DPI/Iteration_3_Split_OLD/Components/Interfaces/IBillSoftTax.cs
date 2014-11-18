using System;
 
namespace DPI.Interfaces
{
	public interface IBillSoftTax
	{
		void ReleaseSession();
		IDmdTax[] ComputeTax(IUOW uow, int prod, decimal priceAmt, string zip, DateTime date);
		IDmdTax[] ComputeTax(IUOW uow, int prod, decimal priceAmt, bool isWireless, string zip, DateTime date);
	}
}