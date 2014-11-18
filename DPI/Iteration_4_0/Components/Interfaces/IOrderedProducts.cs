using System;

namespace DPI.Interfaces
{
	/// <summary>
	/// This interface is intended to be an item in a list of items ordered, used for confirmation.
	/// UI cannot change.
	/// </summary>
	public interface IOrderedProduct
	{
		IProdPrice Prod {get;}  // the ordered product
		IProdPrice[] Components {get;}  // any fees associated with this product
		IProdPrice[] Fees {get;}  // any fees associated with this product
		IProdTax[] Taxes {get;}
		string Action {get;}  // "Add," "Remove," etc.  Only for UI to display.
		decimal TotalAmt {get;}

	}
}