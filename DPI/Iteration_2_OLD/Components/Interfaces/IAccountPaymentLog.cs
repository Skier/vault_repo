using System;

namespace DPI.Interfaces
{	
	public interface IAccountPaymentLog
	{		
		int Account_PaymentLog_ID			{ get; }				
		DateTime Date						{ get; set; }
		string Description					{ get; set; }
		decimal Amount						{ get; set; }
		decimal Balance						{ get; set; }		
	}
}