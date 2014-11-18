using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class OrderSum  : IOrderSum, IOrderSummary2
	{
	#region Data
		
		DmdItem[] items;
		IDemand  demand;

	#endregion

	#region Properties

		public IDmdItem[] Items { get { return items; }}
		public IDemand Demand   { get { return demand; }}
		public IProdPrice[] Products { get { return GetPPs(); }}
		public decimal TotalAmtDue   { get { return GetTotalAmtDue(1); }}
		public decimal TaxAmt        { get { return GetTaxAmt(1); }}
		public decimal ProdSubTotal	 { get { return GetProdSubTotal(1); }}

	#endregion

	#region Constructors
		
		public OrderSum(IDemand demand, DmdItem[] items)
		{
			this.demand = demand;
			this.items = items;
		}

	#endregion

	#region Methods
		// order summary
		public decimal    GetTotalAmtDue (int month)
		{
			return decimal.Round(GetProdSubTotal(month) + GetTaxAmt(month), 2);
		}
		public decimal    GetProdSubTotal(int month)// products + fees
		{
			return decimal.Round(GetProdAmt(month) + GetFeeAmt(month), 2);
		}
		public string[][] GetMonthlySummary(int months)
		{
			string[][] res = new string[GetMatrixSize()][];
			int resRow = 0;

			for (int i = 0; i < Items.Length; i++)
			{
				string[] row = DoRow(items[i], months);
				AddRow(res, ref row, ref resRow);

				for (int j = 0; j < items[i].Components.Length; j++)
				{
					row = DoRow(items[i].Components[j], months);
					AddRow(res, ref row, ref resRow);
				}
				for (int j = 0; j < items[i].TagAlongs.Length; j++)
				{
					row = DoRow(items[i].TagAlongs[j], months);
					AddRow(res, ref row, ref resRow);
				}
			}
			res[res.Length- 3] = ProdSubRow(months);
			res[res.Length- 2] = FeesTaxesRow(months);
			res[res.Length- 1] = TotalRow(months);
			
			return res;
		}
			
		public IDmdItem[] GetProducts(int month)
		{
			if (items == null)
				return new IDmdItem[0];

			ArrayList ar = new ArrayList();

			for (int i = 0; i < items.Length; i++)
				if (Includes(items[i].Prod, month))
					ar.Add(items[i]);

			IDmdItem[] prods = new IDmdItem[ar.Count];
			ar.CopyTo(prods);
			return prods;

		}
		
		public IDmdItem[] GetProducts()
		{
			if (items == null)
				return new IDmdItem[0];

			return items;
		}				
		
		public string[][] GetMonthlySummary()
		{
			return GetMonthlySummary(1);
		}

		public decimal    GetProdSubTotal()// products + fees
		{
			return GetProdSubTotal(1);
		}	
	
		public decimal    GetTotalAmtDue()
		{
			return GetTotalAmtDue(1);
		}		
						
		
		// Product
		public decimal    GetProdAmt(int month) // products only
		{
			decimal prodAmt = 0m;

			for (int i = 0; i < items.Length; i++)
				if (Includes(items[i].Prod, month))
					prodAmt += items[i].PriceAmt;
			
			return prodAmt;
		}

		public decimal    GetProdAmt() // products only
		{
			return GetProdAmt(1);
		}


		// fees
		public decimal    GetFeeAmt      (int month)
		{
			decimal feeAmt = 0M;
	
			for (int i = 0; i < items.Length; i++)
				for (int j = 0; j < items[i].TagAlongs.Length; j++)
					if (Includes(items[i].TagAlongs[j].Prod, month))
						feeAmt += items[i].TagAlongs[j].EffPrice;
			
			return decimal.Round(feeAmt, 2);
		}

		public decimal    GetFeeAmt()
		{
			return  GetFeeAmt(1);
		}		

		
		// Taxes
		public ITaxInfo[] GetTaxes       (int month)
		{
			ArrayList ar = new ArrayList();

			for (int i = 0; i < items.Length; i++)
			{
				if (!Includes(items[i].Prod, month))  // skip the whole package if it's does not start this month
					continue;

				ar.AddRange(items[i].Taxes);
			
				for (int j = 0; j < items[i].TagAlongs.Length; j++)
					if (Includes(items[i].TagAlongs[j].Prod, month))
						ar.AddRange(items[i].TagAlongs[j].Taxes);
	

				for (int j = 0; j < items[i].Components.Length; j++)
					if (Includes(items[i].Components[j].Prod, month))
						ar.AddRange(items[i].Components[j].Taxes);
			}	
			IDmdTax[] txs = new IDmdTax[ar.Count];
	
			if (ar.Count > 0)
				ar.CopyTo(txs);
			
			return ConvToTaxInfo(txs);
		}
		ITaxInfo[] ConvToTaxInfo(IDmdTax[] taxes)
		{
			if (taxes == null)
				return new ITaxInfo[0];
			
			ITaxInfo[] ti = new TaxInfo[taxes.Length];
			for (int i = 0; i < ti.Length; i++)
				ti[i] = new TaxInfo(taxes[i]);
			
			return ti; 
		}
				
		public ITaxInfo[] GetTaxSummary  (int month)
		{
            ISummable[] txs = Grouper.Collapse(Grouper.ConvertTo(GetTaxes(month)));
			IDmdTax[] itax = new IDmdTax[txs.Length];
			
			for (int i = 0; i < itax.Length; i++)
				itax[i] = (IDmdTax)txs[i];
			
			return ConvToTaxInfo(itax);;
		}		
		public  ITaxInfo[] GetTaxes()
		{
			return GetTaxes(1);
		}
		public  ITaxInfo[] GetTaxSummary()
		{
			return GetTaxSummary(1);
		}		
		public decimal    GetTaxAmt      (int month)
		{
			 ITaxInfo[] txs = GetTaxes(month);

			decimal taxAmount = 0m;	
			for (int i = 0; i < txs.Length; i++)
				taxAmount += txs[i].TaxAmount;

			return decimal.Round(taxAmount, 2);
		}
		public decimal    GetTaxAmt      (int prod, int month)
		{
			decimal amount = 0m;
			 ITaxInfo[] taxes = GetTaxes(month);
			
			for (int i = 0; i < taxes.Length; i++)
				if (taxes[i].TaxProd == prod)
					amount += taxes[i].TaxAmount;
 
			return decimal.Round(amount, 2);
		}

		public decimal    GetTaxAmt()
		{
			return  GetTaxAmt(1);
		}

		public string[]   GetSumTaxDesc  (int month)
		{
			ISummable[] txs = Grouper.Collapse(Grouper.ConvertTo(GetTaxes(month)));
			string[] taxes = new string[txs.Length];

			for (int i = 0; i < taxes.Length; i++)
				taxes[i] = GetDescrWithAmt(txs[i]);

			return taxes;
		}

		string GetDescrWithAmt(ISummable tax)
		{
			return ((ITaxInfo)tax).Description + "  " + tax.Amount.ToString("C");
		}
		public string[]   GetSumTaxDesc()
		{
			return GetSumTaxDesc(1);
		}

	#endregion

	#region Implementation

		void AddRow(string[][] res, ref string[] row, ref int resRow)
		{
			if (row == null)
				return;

			res[resRow++] = row;
			row = null;
		}
		string[] DoRow(IDmdItem di, int months)
		{
			string[] row = new string[months + 1];
			row[0] = FirstColumn(di.Prod, di.PackageId);
			
			for (int i = 1; i < months + 1; i++)						
				row[i] = Price(di.Prod, di.PackageId, di.EffPrice, i);

			return row;
		}
		int GetMatrixSize()
		{
			int len = items.Length;

			for (int i = 0; i < items.Length; i++)
			{
				len += items[i].Components.Length;
				len += items[i].TagAlongs.Length;
			}

			return len += 3;  // extra rows for prod subtotal, taxes and total
		}

		string[]  ProdSubRow(int months)
		{
			string[] row = new string[months + 1];
			row[0] = "Subtotal Product";

			for (int i = 1; i < months + 1; i++)
				row[i] = GetProdSubTotal(i).ToString();

			return row;
		}

		string[]  TotalRow(int months)
		{
			string[] row = new string[months + 1];
			row[0] = "Total";
			for (int i = 1; i < months + 1; i++)
				row[i] = this.GetTotalAmtDue(i).ToString();

			return row;
		}
		string[]  FeesTaxesRow(int months)
		{
			string[] row = new string[months + 1];
			row[0] = "Taxes, Fees and Surcharges";
			for (int i = 1; i < months + 1; i++)
				row[i] = GetTaxAmt(i).ToString();

			return row;
		}
		string FirstColumn(int prod, int package)
		{
			if (package > 0)
				return "    " + ((ProdInfo)ProdInfoCol.GetProd(prod)).BillText;

			return ((ProdInfo)ProdInfoCol.GetProd(prod)).BillText;
		}
		string Price(int prod, int package, decimal price, int month)
		{
			if (package > 0)
				return string.Empty; 

			if (!Includes(prod, month))
				return string.Empty;

			return price.ToString();
		}
		bool Includes(int prod, int month)
		{ 
			int startMon = (ProdInfoCol.GetProd(prod)).StartServMon;
			int endMon   = (ProdInfoCol.GetProd(prod)).EndServMon;

			if (endMon == 0)
				endMon = 13;

			if (startMon > month)
				return false;

			return !(endMon < month);
		}

		IProdPrice[] GetPPs()
		{
			ArrayList ar = new ArrayList();
			for (int i = 0; i < Items.Length; i++) 
				ar.AddRange(  ((DmdItem)Items[i]).GetBillable());	
			
			IProdPrice[] pps = new IProdPrice[ar.Count];
			ar.CopyTo(pps);
			return pps;
		}

	#endregion 
	}
}