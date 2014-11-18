using System;
using System.Collections;

using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class TaxInfo : ISummable, ITaxInfo
	{
		/*        Data        */
		string taxId;
		int taxProd;
		decimal taxAmount;
		string taxCode;
        
		/*        Properties        */
		public string TaxId
		{
			get { return taxId; }
			set
			{
				taxId = value;
			}
		}
		public int TaxProd
		{
			get { return taxProd; }
			set { taxProd = value; }
		}
		public decimal TaxAmount
		{
			get { return taxAmount; }
			set
			{

				taxAmount = Decimal.Round(value, 2);
			}
		}
		public string TaxCode { get { return taxCode; } set { taxCode = value;}}
		public string Description { get { return TaxDescription.TaxTypeToString(TaxId); }}

		#region ISummable Members

		public string SumType { get	{ return taxId; }}
		public decimal Amount
		{
			get 
			{
				return taxAmount; 
			}

			set	
			{ 
				taxAmount = value;
			}
		}
		#endregion       
		/*        Constructors			*/
		public TaxInfo()
		{
		}
		public TaxInfo(IDmdTax tax) 
		{
			taxId =  tax.TaxId;
			taxProd = tax.TaxProd;
			taxAmount = decimal.Round(tax.TaxAmount, 2);
			taxCode = tax.TaxCode;
		}
	}
}