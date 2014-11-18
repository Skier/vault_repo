using System;

namespace DPI.Interfaces
{	
	public interface IPaymentLogPageDTO
	{
		bool IsNew									{ get; set; }
		int AccountPaymentLogId 					{ get; set; }
		int AccNumber								{ get; set; }
		DateTime Date								{ get; set; }
		string Description							{ get; set; }
		decimal Amount								{ get; set; }
		decimal Balance								{ get; set; }

		IAccountPaymentLog[] AccountPaymentLogs		{ get; }
	}
}