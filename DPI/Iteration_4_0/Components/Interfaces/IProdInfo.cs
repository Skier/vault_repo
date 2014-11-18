using System;
  
namespace DPI.Interfaces
{
	public interface IProdInfo 
	{
		string ProdName       { get; }
		string BillText       { get; }
		string ProdType       { get; }
		string ProdSubClass   { get; }
		string Status		  { get; }
		string ProdCategory   { get; }

		bool IsComponentOnly  { get; }
		bool IsBillable       { get; }
		bool IsProvisionable  { get; }
		bool IsProvViaMapping { get; }
		
		string ProvCategory   { get; }
		int Supplier	      { get; }
		int Vendor            { get; }
		string TaxCode        { get; }
		int StartServMon      { get; }
		int EndServMon        { get; }
		int MappingProd       { get; }
		string Description    { get; }
		
		// Presentation options
		bool IsAgentVisible          { get; }
		string OrdSumryStartMon2     { get; }
		bool IsPreselectedWebOrderL2 { get; }
		bool SuppressZeroPriceProd   { get; }
		bool IsExcludedFromTotalL2   { get; }
	}
}