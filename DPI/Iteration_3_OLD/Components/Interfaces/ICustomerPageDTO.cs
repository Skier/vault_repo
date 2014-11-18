
namespace DPI.Interfaces
{	
	public interface ICustomerPageDTO
	{
		int AccNumber			{ get; set; }
		string LastName			{ get; set; }
		string FirstName		{ get; set; }
		string Birthday			{ get; set; }
		string Email			{ get; set; }
		string Contact			{ get; set; } 
		string Contact2			{ get; set; } 
		string PrevPhone		{ get; set; }
		string PrevILEC			{ get; set; }	
		string Status			{ get; set; }	
		string StoreName		{ get; set; }

	}
}