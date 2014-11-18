using System;

namespace DPI.Interfaces
{
	public interface IWireless_Custdata
	{
		IDomKey IKey				{ get; }
		int ID						{ get; }
		string ESN					{ get; set; }
		string PhNumber				{ get; set; }
		string SubscriberId			{ get; set; }
		string NameFirst			{ get; set; }
		string NameLast				{ get; set; }
		string Addr1				{ get; set; }
		string Addr2				{ get; set; }
		string City					{ get; set; }
		string State				{ get; set; }
		string Zip					{ get; set; }
		string Email				{ get; set; }
		string ContactNumber		{ get; set; }
        string WebPassword          { get; set; }
        bool IsWebPasswordTemporal  { get; set; }
	}
}