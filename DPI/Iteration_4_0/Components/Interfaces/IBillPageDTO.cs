using System;

namespace DPI.Interfaces
{	
	public interface IBillPageDTO
	{
		int AccNumber			{ get; set; }		
		DateTime DueDate		{ get; set; }
		DateTime DiscoDate		{ get; set; }
		decimal PastDueAmt		{ get; set; }
		decimal CurrDueAmt		{ get; set; }
		decimal Balance			{ get; set; }
		decimal NextPymtAmount	{ get; set; }
		byte BillCycle			{ get; set; } 
	}
}