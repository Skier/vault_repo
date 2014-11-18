using System;
 
namespace DPI.Interfaces
{
	public interface ITaxService
	{
		IDmdTax[] ComputeTax(int prod, decimal priceAmt, string zip, DateTime date);
		IDmdTax[] ComputeTax(int prod, decimal priceAmt, bool isWireless, string zip, DateTime date);
	}
}