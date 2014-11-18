using System;

namespace DPI.Interfaces
{
	public interface IProdPrice
	{
		// id and classification
		int     ProdId       { get; }	
		int     PackageId    { get; set;}
		string  ProdType     { get; }
		string  ProdSubclass { get; }

		// Descriptions
		string  Description  { get; }  
		string  ProdName     { get; }   
		string  BillText     { get; }   

		// Pricing
		int		StartServMon { get; }
		int		EndServMon   { get; }		
		decimal UnitPrice    { get; } // effective (discounted) price
		string  PriceRule    { get; }
		string  TaxCode      { get; }
		
		// Presentation options
		string  OrdSumryStartMon2       { get; } // prod starting after month 1 order summary option 
		bool    SuppressZeroPriceProd   { get; } // any month
	
		// Product selection
		bool    IsPreselectedWebOrderL2 { get; }
		bool    Locked                  { get; }   
		bool    DisplayUnclickMessage   { get; }
		bool    SuppressOnWebReceipt    { get; }
		ProdSelectionState ProdSelState { get ; set; }
	}
}