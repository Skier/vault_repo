using System;

namespace DPI.Interfaces
{
	public interface IPayInfo : IDomObj, IId
	{
		// Properties
		string      PayClass			{ get;		}
		PaymentType	PaymentType			{ get; set; }  // Type of payment (check, cash, etc)
		int         DmdId				{ get; set; }
		IDemand		ParDemand			{ get; set; }
		string		Status				{ get; set; } 
		string		ConfNumber			{ get; set; }
		string		VFConf				{ get; set; } // DPI confirmation number from VF, W/L, or FinProd xactions
		bool		IsConfReq			{ get; set;	}
		DateTime	PayDate				{ get; set; }  // Date Time the money was paid
		
		decimal		TotalAmountDue		{ get; set; }  // Amount due
		decimal		TotalAmountPaid		{ get; set; }  // Amount paid
		decimal		AmountTendered		{ get; set; }  // Amount received from customer
		decimal		ChangeAmount		{ get;      }  // Amount tendered - amount paid		

		int			  TranNumber		{ get; set;	}
		PayInfoSource PayInfoSource     { get; set;		} //set;	}
		string		  CheckNumber		{ get; set;	}
		string		  CheckName		    { get; set;	}	
		IPayInfoTran  Tran				{ get; set;	}
		// Methods
		string	Validate(); 
		//decimal GetComAmt(IUOW uow, string storeCode);
		//void UpdateFromWireless(IUOW uow, string confNum);
		//void UpdateFromVerifone(IUOW uow, IReceipt rcpt);
	}
	public interface IPayInfoLocal : IPayInfo
	{
		decimal	LocalAmountDue  { get; set; }  // Amount Due for local service
		decimal LocalAmountPaid { get; set; }  // Amount paid for local service
		decimal LdAmount        { get; set; }  // Amount paid for "walk up" ld service 		
		decimal	LdAmountDue     { get; set; }  // Amount paid for walk up LD

		// Methods
		void SetAmts(decimal amtPaid, decimal localDue, decimal amtLDDue, decimal amtTendered);
		void PayInFull(decimal localDue, decimal amtLDDue, decimal amtTendered);	
	}
}