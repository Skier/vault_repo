using System;
 
namespace DPI.Interfaces
{
	public interface IWebQueType
	{
		string QueType   { get; }
		bool IsReversal	 { get; }
		bool IsPost		 { get; }
		bool IsReadOnly	 { get; }
	}
}