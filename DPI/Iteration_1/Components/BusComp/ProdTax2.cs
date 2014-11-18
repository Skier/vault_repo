using System;
using System.Collections;

using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable] 
	public class ProdTax2 : IProdTax, ISummable
	{
		/*        Data        */
		int taxedProd;
		decimal taxAmt;
		int extRef;
		string taxCode;

		/*        Properties	*/
		public int TaxedProd
		{ 
			get {return taxedProd; }
			set { taxedProd = value; }
		}
		public string TaxType 
		{ 
			get { return taxCode; }
			set { taxCode = value; }
		}
		public string TaxCode { get {return taxCode;}}
		public decimal TaxAmt 
		{
			get { return decimal.Round(taxAmt, 2);  }
			set { taxAmt = decimal.Round(value, 2 ); }
		}
//		public string Description { get { return TaxTypeCol.GetTaxDescr(taxCode); }}
		public string Description { get { return TaxDescription.TaxTypeToString(TaxType); }}
		/*        Constructors			*/
		public ProdTax2() {}
		public ProdTax2(decimal unitPrice, ProdTax pt, int extRef) 
		{	
			this.taxedProd = pt.TaxedProd;
			this.extRef = extRef;
			this.taxCode = pt.TaxType;
			this.taxAmt = decimal.Round(pt.TaxRate * unitPrice, 2);

			if (pt.UnitBased)
				taxAmt = decimal.Round(pt.TaxRate, 2);
		}
		public ProdTax2(decimal taxAmt, string taxCode, int taxedProd)
		{
			this.taxAmt = taxAmt;
			this.taxCode = taxCode;
			this.taxedProd = taxedProd;
		}

		/*		Static methods		*/
		public static ProdTax2[] AddTaxes (ref ProdTax2[] sum, ProdTax2[] pTaxes)
		{
			if (sum == null)
				sum = new ProdTax2[0];
			
			Hashtable htable = new Hashtable();
			for (int i = 0; i < sum.Length; i++)
				htable.Add(sum[i].taxCode, sum[i]);
			
			for (int i = 0; i < pTaxes.Length; i++)
				if (htable.ContainsKey(pTaxes[i].taxCode))
					((ProdTax2)htable[pTaxes[i].TaxType]).taxAmt += pTaxes[i].TaxAmt;
				else
					htable.Add(pTaxes[i].taxCode, pTaxes[i]);

			
			DictionaryEntry[] de = new DictionaryEntry[htable.Count];
			htable.CopyTo(de, 0);

			ProdTax2[] ret = new ProdTax2[htable.Count];			
			for (int i = 0; i < de.Length; i++)
				ret[i] = (ProdTax2)de[i].Value;
 
			return ret;
		}
		void AddTax(decimal amt)
		{
			this.taxAmt += decimal.Round(amt, 2);
		}
		#region ISummable Members

		public string SumType {	get	{ return taxCode; }}
		public decimal Amount
		{
			get	{ return decimal.Round(taxAmt, 2); }
			set	{ taxAmt = decimal.Round(value, 2);	}
		}

		#endregion
	}
	public class CompareIProdTaxType : System.Collections.IComparer
	{
		public int Compare(object tax1 , object tax2)
		{
			return ((IProdTax)tax1).TaxType.CompareTo(((IProdTax)tax2).TaxType);
		}
	}
}