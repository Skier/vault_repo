using System;
 
namespace DPI.Interfaces
{
	public interface IBillSoftTax
	{
		void ReleaseSession();
		IDmdTax[] ComputeTax(IUOW uow, int prod, decimal priceAmt, string zip, DateTime date);
	}
}