using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;


namespace DPI.Components
{
    [Serializable] 
	public class ProdTax : IProdTax
	{
		/*        Data        */
		int taxedProdId; 
		decimal priceBeingTaxed;
		decimal rate;
		bool    unitBased;
		string  taxCode;

		/*        Properties	*/
		public int TaxedProdId 
		{ 
			get { return taxedProdId; }
			set { taxedProdId = value; }// OrderedProduct & OrderUtil writes to this var. ?
		}
		public int TaxedProd 
		{ 
			get {return taxedProdId; }
			set { taxedProdId = value; }
		}
		public string TaxCode  { get {return taxCode;     }}
		public string TaxType  { get { return taxCode;    }}
		public bool UnitBased  { get { return unitBased;  }}
		public decimal TaxRate { get { return rate;      }}
		public decimal Amount 
		{ 
			get { return TaxAmt; }
			set { TaxAmt = value; }
		}
		public string SumType { get { return TaxCode; }}
		public decimal TaxAmt 
				/* Need to know the amount being charged, so
		 * make sure PriceBeingTaxed is set before getting TaxAmt, 
		 * though 0 is valid and will still return a tax amount
		 *  if tax is unit-based.
		 */
		{
			set {}
			get
			{
				if (unitBased)
					return rate; 
				else
					// round so we don't have hundredths of pennies.
					return decimal.Round(rate * PriceBeingTaxed,2);  
			}
		}
		public string Description { get { return TaxTypeCol.GetTaxDescr(taxCode); }}
		public decimal PriceBeingTaxed { get { return priceBeingTaxed; }}

		/*        Constructors			*/
		public ProdTax(string taxCode, decimal rate, bool isUnitBased)//, string Description) 
		{
			this.taxCode        = taxCode;
			this.unitBased      = isUnitBased;
			this.rate           = rate;
		}
		/*		Static methods		*/
		public static void SetTaxedValues(ProdTax[] tis, decimal price)
		{
			for (int i=0; i < tis.GetLength(0); i++)
				tis[i].priceBeingTaxed = price;
		}
		public static ProdTax[] SumTaxes (ProdTax[] list1, ProdTax[] source)
		{
			int idx;
			ArrayList al = new ArrayList(list1);
			al.Sort(new CompareProdTaxType());

			for (int i=0; i < source.Length; i++)
			{
				idx=al.BinarySearch(source[i], new CompareProdTaxType());
				if (idx<0)
				{
					al.Add(source[i]);
					al.Sort(new CompareProdTaxType());
				}
				else
				{
					((ProdTax)al[idx]).priceBeingTaxed += source[i].PriceBeingTaxed;
				}
			}
			ProdTax[] ret=new ProdTax[al.Count];
			al.CopyTo(ret);
			return ret;
		}
	}
	public class CompareProdTaxType : System.Collections.IComparer
	{
		public int Compare(object tax1 , object tax2)
		{
//			ProdTax px = (ProdTax)x;
//			ProdTax py = (ProdTax)y;
//			int val = px.TaxType.CompareTo(py.TaxType);
//			return (val);

			return ((ProdTax)tax1).TaxType.CompareTo(((ProdTax)tax2).TaxType);
		}
	}
}