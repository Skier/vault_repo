using System;
 
namespace DPI.Interfaces
{	
	public interface IDmdItem
	{
		/*		Properties		*/		
		int Id                { get; }
		string DmdItemType    { get; set; }
		int Supplier          { get; }
		IDemand ParDemand     { get; }
		IDmdItem Parent       { get; }
		IDmdItem[] Components { get; }
		IDmdItem[] TagAlongs  { get; }
		
		int PackageId         { get; }		
		int Prod              { get; }
		DateTime StartDate    { get; }
		DateTime EndDate      { get; }
		string UOM            { get; }
		int QT                { get; set; }	

		//	Prices
		string PriceRule      { get; }
		decimal PriceAmt      { get; }
		decimal PackDiscount  { get; }
		decimal EffPrice	  { get; }		
		
		string Status         { get; set; }

		//	Taxes
		IDmdTax[] Taxes       { get; }
		decimal TaxAmt        { get; }
		decimal PackageTaxAmt { get; }


		
		/*		Methods		*/
		//decimal EffPrice(int servMonth);
		void RemoveTagAlongs();
	    void AdjustPrice(decimal amt);
		void delete();
	}
}