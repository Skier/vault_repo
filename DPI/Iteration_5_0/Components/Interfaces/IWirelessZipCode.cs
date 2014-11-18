using System;

namespace DPI.Interfaces
{
	public interface IWirelessZipCode
	{
		string Zipcode						{ get; set; }
		string Zip_Postal_City				{ get; set; }
		string State						{ get; set; }
		string SPCS_Customer_Service_ID		{ get; set; }
	}
}