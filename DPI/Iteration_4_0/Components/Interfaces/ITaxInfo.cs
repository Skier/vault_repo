using System;
 
namespace DPI.Interfaces
{	
	public interface ITaxInfo
	{
		string TaxId       { get; set; }
		string TaxCode     { get; }
		int TaxProd        { get; set; }
		decimal TaxAmount  { get; set; }
		string Description { get; }
	}
}