using System;

namespace DPI.Interfaces
{
	/// <summary>
	/// Information needed on the customer's receipt
	/// </summary>
	public interface IReceipt
	{
		IOrderSummary Summary {get;}
		DateTime SaleDate {get;}
		// customer info object
		// mail addr object
		// svc addr object
		IPaymentInfo Payment {get;}
		decimal ChangeDue {get;}
		int ConfirmationNum {get;}
		
	}


	public interface IReceiptDto
	{
		IReceipt RctInfo {get;}
		IErrorDto Error {get;}
	}
}
