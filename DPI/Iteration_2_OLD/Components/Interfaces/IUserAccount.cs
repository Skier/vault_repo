using System;
 
namespace DPI.Interfaces
{	
	public interface  IUserAccount
	{
		int    AcctId				{ get; }
		string AcctType				{ get; }
		string AcctName				{ get; }
		string Password				{ get; }
		string StoreCode			{ get; set; }
		int    CorpId				{ get; }
		string ClerkId				{ get; set; }
		string DisplayName			{ get; }
		string JobTitle				{ get; }
		string Status				{ get; }		
		bool IsCertRequired			{ get; }
		bool IsCertWithStoreReq		{ get; }
		DateTime Expiration			{ get; }
		bool PasswordReset			{ get; }
	}
}