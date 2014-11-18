using System;

namespace DPI.Interfaces
{
	/// <summary>
	/// Contains all the product-related information of an order.
	/// </summary>
	public interface IOrderSummary
	{
		IOrderedProduct[] Prods {get;}
		decimal TotalAmountDue {get;}
		decimal SubtotalTaxAmt {get;}
		decimal SubtotalProductAmt {get;}
		OrderType  OrderType {get;}
	//	string InfoMessage {get;}   // Informative message (bonus)
	//	string AdvMessage {get;}	// Advertising blurb (bonus)
	}

/*	public interface IOrderSummaryDto
	{
		IOrderSummary Summary {get;}
		IErrorDto Error {get;}
	}
*/	
}