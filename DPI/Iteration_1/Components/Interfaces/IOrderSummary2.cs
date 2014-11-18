using System;

namespace DPI.Interfaces
{
	public interface IOrderSummary2
	{
		//IProdPrice[] Products { get; }
		decimal ProdSubTotal  { get; } // Products & fees
		decimal TotalAmtDue	  { get; } // Products, fees,and taxes
		decimal TaxAmt		  { get; } // Taxes 
	}
	public interface IOrderSum // refactored   
	{
		IDmdItem[] Items  { get; }
		IDemand    Demand { get; }
		IProdPrice[] Products { get; }

		// month-specific numbers
		decimal    GetFeeAmt      (int month); // Fees
		decimal    GetProdAmt     (int month); // Products only
		decimal    GetProdSubTotal(int month); // Product + fees
		decimal    GetTotalAmtDue (int month); // Products + fees + taxes
		decimal    GetTaxAmt      (int month); // Taxes 
		ITaxInfo[] GetTaxes       (int month); 
		ITaxInfo[] GetTaxSummary  (int month);
		string[]   GetSumTaxDesc  (int month);
	    string[][] GetMonthlySummary(int months);
		IDmdItem[] GetProducts(int month);
		decimal GetTaxAmt(int prod, int month);
       
		// without args 
		decimal    GetProdSubTotal();// products + fees
		decimal    GetProdAmt(); // products only
		decimal    GetTotalAmtDue();
		decimal    GetTaxAmt();
		decimal    GetFeeAmt();
		ITaxInfo[] GetTaxes();
		ITaxInfo[] GetTaxSummary();
		string[]   GetSumTaxDesc();
		string[][] GetMonthlySummary();
		IDmdItem[] GetProducts();

	}
}