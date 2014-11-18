using System;
 
namespace DPI.Interfaces
{
	public interface IVerifone_Transaction
	{
		int Verifone_Transaction_ID	{ get; }
		string TrConfirm			{ get; }// set; }
		string TrLDConfirm			{ get; }//; set; }
		string TrNumber   			{ get; }//; set; }
		int AccNumber			    { get; }//; set; }
		string PhNumber			    { get; }//; set; }
		DateTime PayDate			{ get; }//; set; }
		string PayTime 			    { get; }//; set; }
		decimal LocalAmount 		{ get; }//; set; }
		decimal LDAmount			{ get; }//; set; }
		decimal ComAmount 			{ get; }//; set; }
		string ClerkID 			    { get; }//; set; }
		int Transaction_Type_ID 	{ get; }//; set; }
		int Transaction_Method_ID 	{ get; }//; set; }
		string StoreCode 			{ get; }//; set; }
		string ANI			        { get; }//; set; }

	}
}