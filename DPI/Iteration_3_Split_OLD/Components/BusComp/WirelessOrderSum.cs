using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class WirelessOrderSum  : IWirelessOrderSum
	{
		#region Data
		
		DmdItem[] items;
		IDemand  demand;

		#endregion

		#region Properties

		public IDmdItem[] Items { get { return items; }}
		public IDemand Demand   { get { return demand; }}
		public decimal TotalAmtDue   { get { return GetTotalAmtDue(); }}
		public decimal TaxAmt        { get { return GetTaxAmt(); }}
		public decimal ProdSubTotal	 { get { return GetProdSubTotal(); }}

		#endregion

		#region Constructors
		
		public WirelessOrderSum(IDemand demand, DmdItem[] items)
		{
			this.demand = demand;
			this.items = items;
		}

		#endregion

		#region Methods
		public decimal    GetTotalAmtDue ()
		{
			return decimal.Round(GetProdSubTotal() + GetTaxAmt(), 2);
		}
		public decimal    GetProdSubTotal()
		{
			return decimal.Round(GetProdAmt(), 2);
		}
		public decimal    GetProdAmt() // products only
		{
			decimal prodAmt = 0m;

			for (int i = 0; i < items.Length; i++)
				prodAmt += items[i].PriceAmt;
			
			return prodAmt;
		}
		

		// Taxes
		public ITaxInfo[] GetTaxes()
		{
			ArrayList ar = new ArrayList();

			for (int i = 0; i < items.Length; i++)
				ar.AddRange(items[i].Taxes);
							
			IDmdTax[] txs = new IDmdTax[ar.Count];
	
			if (ar.Count > 0)
				ar.CopyTo(txs);
			
			return ConvToTaxInfo(txs);
		}
		
				
		public decimal    GetTaxAmt      ()
		{
			ITaxInfo[] txs = GetTaxes();

			decimal taxAmount = 0m;	
			for (int i = 0; i < txs.Length; i++)
				taxAmount += txs[i].TaxAmount;

			return decimal.Round(taxAmount, 2);
		}
		#endregion

		#region Implementation

		ITaxInfo[] ConvToTaxInfo(IDmdTax[] taxes)
		{
			if (taxes == null)
				return new ITaxInfo[0];
			
			ITaxInfo[] ti = new TaxInfo[taxes.Length];
			for (int i = 0; i < ti.Length; i++)
				ti[i] = new TaxInfo(taxes[i]);
			
			return ti; 
		}

		#endregion 
	}
}