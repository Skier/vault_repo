using System;
 
namespace DPI.Interfaces
{	
	public interface IProductCategory
	{
		string ProdCategory { get; }
		bool IsLocal        { get; }
		bool IsWireless     { get; }
		bool IsInternet     { get; }
		bool IsDebitCard    { get; }
		bool IsSatellite	{ get; }

		bool Compare(IProductCategory pcat);
	}
}