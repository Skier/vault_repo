using System;
 
namespace DPI.Interfaces
{
	public interface IOrder
	{
		int Id            { get; }  // demand id
		int PayInfoId     { get; }  // pay info id
		string Name		  { get; }  // customer name
		string Phone	  { get; }  
		DateTime Date     { get; }  // payinfo 
		string ConfNumber { get; set;}  //payinfo 
		int    AccNumber  { get; set;}  // demand billpayer
		string OrderType  { get; } // demand type
	}
}