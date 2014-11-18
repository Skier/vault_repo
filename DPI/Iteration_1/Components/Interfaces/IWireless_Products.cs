using System;
using System.Collections;

namespace DPI.Interfaces
{
	public interface IWireless_Products
	{
		int Wireless_product_id			{ get; }
		string Product_name				{ get; }		
		int Supplier_id					{ get; }
		int Vendor_id					{ get; }
		string Vendor_Name				{ get; }
		string Soc						{ get; }
		string Expiration				{ get; }
		decimal Price					{ get; }
		DateTime Start_date				{ get; }
		DateTime End_date				{ get; }
		string Receipt_text				{ get; }
		int Product_commission_percent	{ get; }
		decimal Product_commission_flat	{ get; }
		decimal CommissionAmt           { get; }
		int ProdId						{ get; }
		string OverrideWSProvider       { get; }
		string UniProdName				{ get; }
		bool IsActivationReq			{ get; }
		bool IsPhoneReq					{ get; }
		string ReqItems					{ get; }
		bool IsXml						{ get; }
		bool IsPerValidationReq			{ get; }

		string GetReceipt(bool pass, DictionaryEntry[] entries);
	}
}