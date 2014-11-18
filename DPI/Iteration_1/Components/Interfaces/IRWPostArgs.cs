using System;

namespace DPI.Interfaces
{
	public interface IRWPostArgs
	{
		IUser User				{ get; }
		IPayInfo PayInfo		{ get; } 
 		decimal AmtPaid			{ get; } 
		decimal AmtTaxes		{ get; } 
		decimal AmtComm			{ get; }
		string TranType			{ get; }
		string ServType			{ get; }
		string PymtType			{ get; }
		string ReceitId			{ get; }
	}
}