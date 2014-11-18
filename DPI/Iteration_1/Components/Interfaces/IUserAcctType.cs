using System;
 
namespace DPI.Interfaces
{	
	public interface IUserAcctType
	{
		string AcctType       { get; }
		bool IsAutoLoginOnly  { get; }
		bool IsStoreBased     { get; }
		bool   RequestClerkId { get; }
		string Role           { get; }
	}
}
