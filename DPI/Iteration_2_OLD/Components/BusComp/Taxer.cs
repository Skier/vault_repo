/// <summary>
/// Summary description for Class1.
/// </summary>
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DPI.Components
{
	public class Taxer
	{
		/*        Data        */

		/*		Methods		*/
		public static ProdTax[] calcTaxes(UOW uow, int prod, string zip, decimal amt)
		{
			return new Taxer().EstTaxes(uow, prod, zip, amt);
		}	
		/*		public static TaxItem[] calcTaxes(SqlTransaction xact, int prod, string zip, decimal amt)
				{
					return new Taxer().estDlvTaxes(xact, prod, zip, amt);
				}
		*/
		/*		public static TaxItem[] calcTaxes(int prod, string zip, decimal amt)
				{
					return new Taxer().estDlvTaxes(prod, zip, amt);
				}
		*/
		/*		Implementation		*/
		/*		TaxItem[] estDlvTaxes(SqlTransaction xact, int prod, string zip, decimal amt)
				{
					TaxItem[] tItems = Tax.findTaxes(xact, zip, taxCode(prod));	

					for (int i = 0; i < tItems.Length; i++)
						tItems[i].taxAmt = getTaxAmt(tItems[i], amt);

					return tItems;
				}
		*/
		ProdTax[] EstTaxes(UOW uow, int prod, string zip, decimal amt)
		{
//			ProdTax[] tItems = Tax.findTaxes(uow, zip, taxCode(uow, prod));	
			ProdTax[] tItems=new ProdTax[1];
/*
			for (int i = 0; i < tItems.Length; i++)
				tItems[i].taxAmt = getTaxAmt(tItems[i], amt);
*/
			return tItems;
		}
		/*		TaxItem[] estDlvTaxes(int prod, string zip, decimal amt)
				{
					TaxItem[] tItems = Tax.findTaxes(zip, taxCode(prod));	

					for (int i = 0; i < tItems.Length; i++)
						tItems[i].taxAmt = getTaxAmt(tItems[i], amt);

					return tItems;
				}
		*/
		decimal getTaxAmt(ProdTax ti, decimal amt)
		{
			if (ti.unitBased)
				return Money.round(ti.rate);

			return Money.round(ti.rate * amt);
		}
/*		string taxCode(UOW uow, int prodId)
		{
			Product prod = Product.find(uow, prodId);
			return prod.TaxCode;
		}
*/
	}
}
