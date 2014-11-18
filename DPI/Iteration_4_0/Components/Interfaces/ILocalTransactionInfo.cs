using System;
 
namespace DPI.Interfaces
{	
	public interface ILocalTransactionInfo
	{
	    int Transaction_Id							{get; }		
		string TrConfirm							{get; }
		int AccNumber								{get; }
		Transaction_Type_Id Transaction_Type_Id		{get; }
		string PhNumber								{get; }		
		DateTime PayDate							{get; }
		string PayTime								{get; } 
		decimal LocalAmount							{get; }
		decimal LDAmount							{get; }
		decimal Tax									{get; }
	}
}