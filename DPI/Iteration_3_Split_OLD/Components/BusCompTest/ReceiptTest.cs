using System;
using System.Collections;
using DPI.Interfaces;


namespace DPI.Services
{
	/// <summary>
	/// Summary description for ReceiptTest.
	/// </summary>
	public class ReceiptTest
	{
		public ReceiptTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}
/*
		public static void DumpReceipt(IReceipt rct)
		{
			Console.WriteLine("Account {0:G}", rct.AccNumber);
			Console.WriteLine("Conf: {0:G}", rct.ConfirmationNum);
			Console.WriteLine("Local paid {0:C}", rct.LocalAmtPaid);
			Console.WriteLine("Change {0:C}", rct.ChangeDue);
			//Console.WriteLine("All taxes: {0:C}",   .SubtotalTaxAmt);

			IOrderedProduct prod;
			for (int i = 0; i < rct.Summary2.Prods.Length; i++)
			{
				prod = rct.Summary.Prods[i];
				Console.WriteLine("{0:G}: {1:G} = {2:C}", prod.Prod.ProdId, prod.Prod.ProdName, prod.Prod.PriceAmt);

				for (int j = 0; j < prod.Components.Length; j++)
				{
					IProdPrice comp = prod.Components[j];
					Console.WriteLine("- Component {0:G}: {1:G} = {2:C}", comp.ProdId, comp.ProdName, comp.PriceAmt);
				}


				// fees
				for (int j = 0; j < prod.Fees.Length; j++)
				{
					IProdPrice fee = prod.Fees[j];
					Console.WriteLine("-- Fee {0:G}: {1:G} = {2:C}", fee.ProdId, fee.ProdName, fee.PriceAmt);
				}
				// taxes
				for (int j = 0; j < prod.Taxes.Length; j++)
				{
					IProdTax tax = prod.Taxes[j];
					Console.WriteLine("---- Tax {0:G} = {1:C}", tax.TaxDescription, tax.TaxAmt );
				}
				Console.WriteLine();
			}
			Console.WriteLine();
			Console.WriteLine();
		}
*/
		public static void DumpReceipt2(IReceipt rct)
		{
			Console.WriteLine("Account {0:G}", rct.AccNumber);
		//	Console.WriteLine("Conf: {0:G}", rct.ConfirmationNum);
		//	Console.WriteLine("Local paid {0:C}", rct.LocalAmtPaid);
			//Console.WriteLine("Change {0:C}", rct.ChangeDue);
		//	Console.WriteLine("All taxes: {0:C}", rct.Summary2.TaxAmt);

			//IOrderSummary2 os2 = rct.Summary2;


/*			IProdPrice prod;
			for (int i = 0; i < os2.Products.Length; i++)
			{
				prod = os2.Products[i];
				Console.WriteLine("ID: {0:G}: {1:G} = {2:C}.  PkgID:{3:G}", prod.ProdId, prod.ProdName, prod.PriceAmt, prod.PackageId);
				Console.WriteLine();
			}
		*/
			Console.WriteLine();
			Console.WriteLine();
		}
	}
}
