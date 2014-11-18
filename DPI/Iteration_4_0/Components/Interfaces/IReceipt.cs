using System;
using System.Collections;

namespace DPI.Interfaces
{
	public interface IReceipt
	{		
		int    AccNumber { get; }
		string ConfNum   { get; set; }
		int    Demand    { get; }
		string   Pin	 { get; set; }
	}

	public interface IPinReceipt : IReceipt
	{		
		decimal  Commission			{ get; }
		string   Receipt_Text		{ get; set; }
		DateTime TransactionTime	{ get; }
		DictionaryEntry[] Entries   { get; }
	}
	public interface ICellPhoneReceipt :  IPinReceipt
	{
		bool   Pass				{ get; set; }
		string PhoneNumber		{ get; set; }
		string ErrMsg			{ get; }
		string Status			{ get; }
		bool   IsActivated		{ get; set; }
		bool   IsRefilled		{ get; set; }
		string Msl				{ get; set; }
		string Msid				{ get; set; }
		string Mdn				{ get; set; }
		string ControlNumber	{ get; set; }
	}	
	public interface IDebitCardReceipt :  IReceipt
	{
		bool     IsApproved { get; }
		string[] Msg        { get; }		
	}
	public interface IEnergyRcpt
	{
		bool IsEnrolled				{ get; set; }
		bool IsPaid					{ get; set; }
		string AccNumber			{ get; set; }
		string AccName				{ get; set; }
		string ESIID				{ get; set; }
		string Pin					{ get; set; }
		string ConfNum				{ get; set; }
		string ErrMsg				{ get; set; }
	}
}