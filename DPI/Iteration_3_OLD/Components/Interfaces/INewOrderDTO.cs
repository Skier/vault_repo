using System;

namespace DPI.Interfaces
{	
	public interface INewOrderDTO
	{	
		OrderType OrderType { get; set; }
		IAddr MailAddr      { get; set; }
		IAddr SvcAddr       { get; set; } 
		ICustInfo2 Cust     { get; set; }
		IDemand Dmd         { get; set; }
		string Zipcode      { get; set; }
		string ILEC         { get; set; }
		string TransNum     { get; set; }
		IPayInfo PayInfo	{ get; set; }			
		IReceipt Receipt	{ get; set; }
	}
}