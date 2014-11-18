using System;
 
namespace DPI.Interfaces
{	
	public interface ICustInfo
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

	public interface ICustDataValidation
	{
		int AccNumber		{ get; set; }
		string WebPassword	{ get;set; }
	}
	public interface ICustInfo2 : ICustInfo
	{

		int			CustInfoID				{ get;		}
		string		CustInfoType			{ get;		} //set;	}
		string		Status					{ get; set;	}
		IAddr2		ServAddr				{ get; set; }
		IAddr2		MailAddr				{ get; set; }
		int			ServAddID				{ get; set; }
		int			MailAddID				{ get; set; }
		string		PhNumber				{ get; set; }
		string		IDNumber				{ get; set; }
		DateTime	IDExpirationDate		{ get; set; }
		string		Ssn						{ get; set; }
		DateTime	Dob						{ get; set; }
		string		IDType					{ get; set; }
		string		IDState					{ get; set; }
	}
}