using System;
 
namespace DPI.Interfaces
{	
	public interface IAcctInfo
	{
		int      AccNumber   { get ;}
		string   Status		 { get; }
		string   FirstName   { get; }
		string   LastName    { get; }

		bool     IsActive    { get; } 
		
		// Sourced from Yonix
		decimal  PastDueAmt  { get; }   // = Yonix dbo.fnPastDueAmt
		decimal  CustDataBal { get; }	// = Yonix CustData.Balance
		decimal  CurrCharges { get; }   // = Yonix dbo.fnCurrentChargeAmt
		
		// Derived
		decimal  BalForward  { get; }	// CustDataBal - PastDueAmt 
		decimal  DueAmt      { get; }   // CurrCharges + PastDueAmt - CustDataBal 

		DateTime DueDate     { get; }
		DateTime DiscoDate	 { get; }   // = Yonix CustData.SDisco
		string   PhNumber    { get; }
		string   PhNumFormated { get; }
	}
}