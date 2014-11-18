namespace DPI.Interfaces
{	
	public interface ICustomerFilter
	{
		string AccNumber		{ get; set; }
		string PhoneNumber		{ get; set; }
		string LastName			{ get; set; }
		string FirstName		{ get; set; }
		IAddr Addr				{ get; set; }
	}
}
