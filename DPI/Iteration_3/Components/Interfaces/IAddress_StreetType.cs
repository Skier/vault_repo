using System;
 
namespace DPI.Interfaces
{	
	public interface IAddress_StreetType
	{
		int Address_StreetType_ID		{ get; }
		string StreetType				{ get; }
		string StreetTypeAbbr	{ get; }
	}
}