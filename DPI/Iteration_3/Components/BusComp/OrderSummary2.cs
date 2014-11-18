//using System;
//using System.Collections;
//using DPI.Interfaces;
//using DPI.Components;
//
//namespace DPI.Components
//{
//	[Serializable]
//	public class OrderSummary2 : IOrderSummary2
//	{
//		IProdPrice[] prods;
//		decimal taxAmt; 
//
//		/*		Properties		*/
//		public IProdPrice[] Products 
//		{
//			get { return prods; }
//			set { prods = value ;}
//		}
//		public decimal ProdSubTotal { get { return calcTotal(); }}
//		public decimal TotalAmtDue  { get { return ProdSubTotal + taxAmt; }}
//		public decimal TaxAmt       { get { return taxAmt; }}
// 
//		/*		Constructors		*/
//		public OrderSummary2(OrderSummary sumry)
//		{
//			prods = ProdQual.SuppressZeroPrice(new UOW(), ProdPrice.ToProdPrice(ProdPrice.Conv(sumry.Prods)));
//			taxAmt = Decimal.Round(sumry.SubtotalTaxAmt, 2); 
//		}
//		public OrderSummary2(IProdPrice[] prods, decimal taxAmt)
//		{
//			this.prods = prods;
//			this.taxAmt = taxAmt;
//		}
//		/*		Methods		*/
//		/*		Implementation		*/
//		
//		decimal calcTotal()
//		{
//			decimal total = 0m;
//			for (int i = 0; i < prods.Length; i++)
//				total +=  prods[i].UnitPrice; 
//			
//			return total;
//		}
//	}
//}