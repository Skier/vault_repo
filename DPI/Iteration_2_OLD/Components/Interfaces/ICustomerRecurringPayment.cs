using System;

namespace DPI.Interfaces
{
	public interface ICustomerRecurringPayment
	{
		int			Id						{ get; }
		DateTime	DateInserted			{ get; set; }
		DateTime	DateModified			{ get; set; }
		string		UserId					{ get; set; }
		int			AccNumber				{ get; set; }
		string		BillingFirstName		{ get; set; }
		string		BillingLastName			{ get; set; }
		string		BillingAddress			{ get; set; }
		string		BillingCity				{ get; set; }
		string		BillingState			{ get; set; }
		string		BillingZip				{ get; set; }
		string		PhNumber				{ get; set; }
		string		EmailAddress			{ get; set; }
		bool		Active					{ get; set; }
		int			AccountTypeId			{ get; set; }
		string		BAccNumber				{ get; set; }
		string		BRouteNumber			{ get; set; }
		string		DLStateNumber			{ get; set; }
		string		ExpirationMonthYear		{ get; set; }
		string		CVV2					{ get; set; }
		int			Priority				{ get; set; }
	}
}