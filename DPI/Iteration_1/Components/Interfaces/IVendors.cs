using System;

namespace DPI.Interfaces
{
	public interface IVendors
	{
		int	   Vendor_id			{ get; }
		string Vendor_name			{ get; }
		string Vendor_address		{ get; }
		string Vendor_city			{ get; }
		string Vendor_state			{ get; }
		string Vendor_zip			{ get; }
		string Vendor_phone			{ get; }
		string Product_type			{ get; }
		string ProdCategory			{ get; }
		string Status				{ get; }
		string DefaultWSProvider	{ get; }
		bool   IsNpaNxxReq			{ get; }
	}
}