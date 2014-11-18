using System;
 
namespace DPI.Interfaces
{	
	public interface IDelTax
	{
		int Id { get;  }
		int DlvId  { get; set; }
		string TaxId { get; set; }
		decimal TaxAmt  { get; set; }
	}
}