using System;

namespace DPI.Interfaces
{
	public interface ISSPostArgs
	{
		string Source			{ get; }
		string Pwd				{ get; }
		string Reason			{ get; }
		string ConfNum			{ get; }
		int TranId				{ get; } 
		string Retailer			{ get; } 
		string StoreId			{ get; } 
		string Upc				{ get; }
		decimal Price			{ get; }
		string ACode			{ get; }
		int ManId				{ get; }
		int AreaCode			{ get; }
		int Prefix				{ get; }
	}
}