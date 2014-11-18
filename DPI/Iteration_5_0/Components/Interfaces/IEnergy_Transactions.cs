using System;


namespace DPI.Interfaces
{
	public interface IEnergy_Transactions
	{
		int ID						{ get; }
		int ConfirmNum				{ get; set; }
		DateTime PayDateTime		{ get; set; }
		decimal Tran_Amount			{ get; set; }
		string StoreCode			{ get; set; }
		string Clerkid				{ get; set; }
		string Pin					{ get; set; }
		decimal Commission			{ get; set; }
		string Status				{ get; set; }
		int AcctID					{ get; set; }
		decimal ActivationFee		{ get; set; }
		decimal TaxAmt				{ get; set; }
	}
}