using System;
 
namespace DPI.Interfaces
{	
	public interface ITaxer
	{
		//IProdTax[] ComputeTax(IDmdItem[] dmdItems, string zip, DateTime date);
		IProdTax[] ComputeTax(IDmdItem[] dmdItems, string zip, string country, string st, DateTime date);
		//  DmdItem.Prod ->   (ProdInfoCol.GetProd(dmdttem.Prod)).TaxCode
		// taxCode.BillSoftTRan & taxCode.BillSoftServ 


		// Start EazyTax
		// for each prod in the dmdItems
		//  get tran & serv from taxcode
		// 
	}
}			