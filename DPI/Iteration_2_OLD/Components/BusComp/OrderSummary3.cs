//using System;
//using System.Collections;
//using DPI.Components;
//using DPI.Interfaces;
//
//namespace DPI.Components
//{
//	[Serializable]  
//	public class OrderSummary3  : IOrderSummary3
//	{
//		/*		Data		*/
//		IProdPrice[] products;
//		decimal prodSubTotal;
//		decimal taxAmt;
//		decimal prodSubTotalM2; 
//		decimal taxAmtM2;
//		IProdTax[] taxesM1;
//		IProdTax[] taxesM2;
// 
//		/*		Properties		*/
//		public IProdPrice[] Products  { get { return products; }}
//		public decimal ProdSubTotal   { get	{ return prodSubTotal; }}   // Derived?
//		public decimal TotalAmtDue	  {	get { return ProdSubTotal + TaxAmt; }} 
//		public decimal TaxAmt		  { get { return taxAmt; }}         // fees and taxes
//		public decimal ProdSubTotalM2 { get { return prodSubTotalM2; }} // Month 2 prices 
//		public decimal TotalAmtDueM2  { get { return ProdSubTotalM2 + TaxAmtM2	; }}
//		public decimal TaxAmtM2		  { get { return taxAmtM2; }}       // Month 2 fees and taxes
//		public IProdTax[] TaxesM1     { get { return taxesM1; }}        // Order taxes summary (all products)
//		public IProdTax[] TaxesM2     { get { return taxesM2; }}        // Month 2 taxes summary (all products)
//
//		/*		Constructors		*/
//
//		/*		Methods		*/
//		public static IOrderSummary3 BuildOrderSummary(UOW uow, ProdPrice[] prods, string zip, string ilecCode, OrderType otype)
//		{
//			//   1. For each product
//			//		  Find package components
//			//		  Add triggered fees
//			//		  Get taxes / surcharges
//			//   2. Remove duplicate fees if any
//			//   3. Sum taxes
//			//   4. Est summary fiels (or durive?)
//
//			return null;
//		}
//	}
//}