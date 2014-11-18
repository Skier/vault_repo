using System;
 
namespace DPI.Interfaces
{
	public interface ITempAutologin
	{
		int    Id              { get; }
		string AcctName        { get; }
		string PW              { get; }
		string Token           { get; }
		string StoreCode       { get; }
		string TransactionType { get; } 
	}
}        