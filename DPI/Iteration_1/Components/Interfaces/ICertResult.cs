using System;
 
namespace DPI.Interfaces
{
	public interface ICertResult 
	{
		int Id            { get; }
		string Type       { get; set; }
		string StoreCode  { get; set; }
		string Coworker   { get; set; }
		string Name       { get; set; }
		DateTime CertDate { get; }
		string Status     { get; set; }
	}
}