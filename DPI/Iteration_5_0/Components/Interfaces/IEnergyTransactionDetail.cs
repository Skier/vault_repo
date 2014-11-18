using System;


namespace DPI.Interfaces
{
	public interface IEnergyTransactionDetail
	{
		int ID							{ get; }
		int TranId						{ get; set; }
		decimal TranAmt					{ get; set; }
		string Description				{ get; set; }
		IEnergy_Transactions EngTran	{ get; set; }
	}
}