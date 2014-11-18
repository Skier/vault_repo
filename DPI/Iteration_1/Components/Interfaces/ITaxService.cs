using System;
 
namespace DPI.Interfaces
{
	public interface ITaxService
	{
		IDmdTax[] ComputeTax(int prod, decimal priceAmt, string zip, DateTime date);
	}
}