using System;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class OrderSummaryProdFormatter 
	{
		public static string FormatPrice(IProdPrice prod, int month)
		{
			if (!WithinRange(prod, month))
				return "";

			if (prod.PackageId != 0)          
				return "";			// no price for components

			return EstPriceAmt(prod).ToString("C") + "&nbsp;&nbsp;";
		}
		static decimal EstPriceAmt(IProdPrice prod)
		{
//			if ((prod.StartServMon > 1) && (prod.OrdSumryStartMon2 == Const.ORD_Sumry_ZERO))
//				return 0m;  // Month 2 option: display with 0 price
		
			return prod.UnitPrice;
		}

		public static bool WithinRange(IProdPrice prod, int month)
		{
			int endMon = prod.EndServMon != 0 ? prod.EndServMon : int.MaxValue;
			
			if (month < prod.StartServMon)
				return false;

			if (month > endMon)
				return false;

			return true;
		}
	}
}