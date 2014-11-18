using System;

namespace DPI.Interfaces
{
	public interface IUser
	{
		int	   AcctId			{ get; set; }
		string DisplayName		{ get; set; }
		string ClerkId			{ get; set; }
		string AcctType			{ get; set; }
		string JobTitle         { get; set; }
		bool HasCertificate		{ get; set; }
		string LoginStoreCode	{ get; set; }
		string Token            { get; set; }
		string StoreNumber		{ get; set; }
		string Role             { get; set; }
	}
}