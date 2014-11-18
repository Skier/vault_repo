//IDebitCardResponse
using System;
 
namespace DPI.Interfaces
{	
	public interface IDebitCardResponse
	{
		string RespCode { get; }
		string RespText { get; }
	}
}