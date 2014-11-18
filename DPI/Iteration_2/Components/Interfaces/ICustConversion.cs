using System;
 
namespace DPI.Interfaces
{
	public interface ICustConversion
	{
		string   ConvName     { get; }
		int      ExclCorp     { get; }
		string   ExclAgent    { get; }
		DateTime StartDate    { get; }
		DateTime EndDate      { get; }
		string   Status       { get; }
		string   Description  { get; }
	}
}