using System;

namespace DPI.Interfaces
{
	public interface IVerifoneResult
	{		
		int Id						{ get; }
		string ErrorCode			{ get; }
		int ConfNum					{ get; }
		int AccNumber				{ get; }		
		string CustomerName			{ get; }
		DateTime TransactionTime	{ get; }		
		string Message				{ get; }
	}
}