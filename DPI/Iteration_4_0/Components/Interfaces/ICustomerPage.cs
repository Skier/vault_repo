
namespace DPI.Interfaces
{	
	public interface ICustomerPage
	{
		string LastName			{ get; set; }
		string FirstName		{ get; set; }
		string Birthday			{ get; set; }
		string Email			{ get; set; }
		string Contact			{ get; set; } 
		string Contact2			{ get; set; } 
		string PrevPhone		{ get; set; }
		string PrevILEC			{ get; set; }
		string FormattedName	{ get;		}
		int AccNumber			{ get; set;	}
	}

	public interface ICustomerFilter
	{
		int AccNumber			{ get; set; }
		string PhoneNumber		{ get; set; }
		string LastName			{ get; set; }
		string FirstName		{ get; set; }
	}
}