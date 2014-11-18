using System;

namespace DPI.Interfaces
{
	public interface IWireless_Transactions
	{
		int Wireless_Transaction_ID			{ get; }
		int TrConfirm						{ get; set; }
		string TrNumber						{ get; set; }
		DateTime PayDateTime				{ get; set; }
		decimal Tran_Amount					{ get; set; }
		int Transaction_Method_ID			{ get; set; }
		string StoreCode					{ get; set; }
		string Clerkid						{ get; set; }
		int Wireless_product_ID				{ get; set; }
		string Pin							{ get; set; }
		decimal Commission					{ get; set; }
		string Status						{ get; set; }
		string Supplier_tran				{ get; set; }
		int AcctID							{ get; set; }
		IWireless_Custdata	Customer		{ get; set; }
		decimal ActivationFee				{ get; set; }
		decimal TaxAmt						{ get; set; }
	}
}