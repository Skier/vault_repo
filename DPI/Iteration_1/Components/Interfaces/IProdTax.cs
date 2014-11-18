using System;

namespace DPI.Interfaces
{
	public interface IProdTax : ISummable
	{
		decimal TaxAmt      { get; set; }
		string  Description { get; } 
		string  TaxType     { get; }
		string  TaxCode     { get; }
		int     TaxedProd   { get; set; }
	}
}