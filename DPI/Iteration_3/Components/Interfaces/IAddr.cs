using System;

namespace DPI.Interfaces
{
	public interface IAddr
	{
		string StreetNum				{ get; set; } 
		string StreetPrefix				{ get; set; }
		string Street					{ get; set; }
		string StreetType				{ get; set; }
		string StreetSuffix				{ get; set; }
		string Unit						{ get; set; }
		string City						{ get; set; }
		string Zipcode					{ get; set; }
		string State					{ get; set;	}
		string UnitType					{ get; set; }
		string FormattedStreetAddress	{ get; }
		string FormattedCityStateZip	{ get; }	

		/*		Methods		*/	
		
	//	string GetState();
	  //  void SetState(string state);
	}
	public interface IAddr2 : IAddr
	{
		int    AddressID	{ get; }
	//	string AdrType		{ get; } //set; }
		string AdrStatus	{ get; set; }
	}
}		