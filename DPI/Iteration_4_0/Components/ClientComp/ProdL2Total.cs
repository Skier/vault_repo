using System;
using System.Text;
using System.Collections;

using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.ClientComp
{
	public class ProdL2Total
	{
		public static decimal CalcProdL2Total(IProdPrice[] prods)
		{
			decimal total = 0m;

			for (int i = 0; i < prods.Length; i++)
				if (prods[i].ProdSelState == ProdSelectionState.Selected)
					if (!ProdInfoCol.GetProd(prods[i].ProdId).IsExcludedFromTotalL2)
						total += prods[i].UnitPrice;
			
			return decimal.Round(total, 2);
		}
	}
}