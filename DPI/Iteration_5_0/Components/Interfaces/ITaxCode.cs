using System;
 
namespace DPI.Interfaces
{	
	public interface ITaxCode
	{
		string TxCode       { get; }
		string Description  { get; }
		int    BillSoftTran { get; }
		int    BillSoftServ { get; } 
	}
}