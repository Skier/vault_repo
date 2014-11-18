using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{
    [Serializable]  
	public class OrderSummary  : IOrderSummary
	{
		/*		Data		*/
		public OrderedProduct[] orderedProducts;
		internal decimal totalAmountDue;
		internal decimal subtotalTaxAmt;
		internal decimal subtotalProductAmt;
		internal OrderType oType;

		/*		Properties		*/
		public IOrderedProduct[] Prods {get {return this.orderedProducts;}}
		public decimal TotalAmountDue {get {return totalAmountDue;}}
		public decimal SubtotalTaxAmt {get {return subtotalTaxAmt;}}
		public decimal SubtotalProductAmt {get {return subtotalProductAmt;}}
		public OrderType OrderType {get {return oType;}} 

		/*		Constructors		*/
		protected OrderSummary() {}

		/*		Methods		*/
		public static OrderSummary BuildOrderSummary(UOW uow, ProdPrice[] jpProds, string zip, string ilec, OrderType type)
		{
			return BuildOrderSummary( uow, jpProds,  zip,  ilec,  type, 1);
		}

		public static OrderSummary BuildOrderSummary(UOW uow, ProdPrice[] jpProds, string zip, string ilec, OrderType type, int svcmonth)
		{
			/* **************
			 * needed :
			 * list of prodprices
			 * zip code for taxes
			 * order type (so we can filter fees)
			 * product composition/fees, with rules.
			 */

			ProdPrice[] prods = getSelected(jpProds);

			OrderSummary summ = new OrderSummary();
			ArrayList onceOnlyFees = new ArrayList();
			ArrayList al = new ArrayList(prods.Length);

			summ.totalAmountDue     = 0;
			summ.subtotalTaxAmt     = 0;
			summ.subtotalProductAmt = 0;
			summ.oType=type;

			for (int i = 0; i < prods.Length; i++)
			{
				OrderedProduct op = new OrderedProduct(uow, prods[i], zip, ilec, type, svcmonth);
//				op.prodp = prods[i];
				CheckFees(uow, onceOnlyFees, op, svcmonth);  // remove once only fees

				summ.totalAmountDue += op.TotalAmt;  // includes all fees & taxes
				summ.subtotalTaxAmt += op.taxesAmt + op.taxesOnFeesAmt;
				summ.subtotalProductAmt += op.Prod.UnitPrice;

				al.Add(op);
			}
			
			summ.orderedProducts = new OrderedProduct[al.Count];
			al.CopyTo(summ.orderedProducts);
			Console.WriteLine("Total order is {0:C}", summ.TotalAmountDue);
			return summ;
		}
		/*		Implementation		*/
		static ProdPrice[] getSelected(ProdPrice[] prods)
		{
			ArrayList alSelected = new ArrayList();
			
			for (int i = 0; i < prods.Length; i++)
				if (prods[i].ProdSelState == ProdSelectionState.Selected) 
					alSelected.Add(prods[i]); // adding a new product
			
			ProdPrice[] ppa = new ProdPrice[alSelected.Count];
			alSelected.CopyTo(ppa);
			return ppa;
		}

		static void CheckFees(UOW uow, ArrayList onceOnlyFees, OrderedProduct op, int svcMonth)
		{
			for (int i = 0; i < op.fees.Length; i++)
			{
				Product p = Product.find(uow, op.fees[i].ProdId);
				if (op.prod.IsInstallForEachInstance)
					continue;

				if (onceOnlyFees.Contains(op.prod.ProdSubclass))
				{
					op.RemoveFee(uow, op.fees[i].ProdId, svcMonth);  // this will recalc $$
					continue;
				}
				onceOnlyFees.Add(op.prod.ProdSubclass);
			}
		}
	}
}