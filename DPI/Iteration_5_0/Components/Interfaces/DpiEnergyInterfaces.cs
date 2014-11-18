using System;

namespace DPI.Interfaces
{
	public interface IQuoteReq
	{
		string RatePlanName		{ get; set;	}
		int DwellingType		{ get; set;	}
		int UnitAge				{ get; set;	}
		int BedroomCount		{ get; set;	}
		int HeatingType			{ get; set;	}
		int WindowUnits			{ get; set;	}
		int AcUnits				{ get; set;	}
		int HasPool				{ get; set;	}
		int HasGarage			{ get; set;	}
		DateTime StartDate		{ get; set;	}
		decimal MonthlyRent		{ get; set;	}
		int SquareFootage		{ get; set;	}
	}
	public interface IAddressReq
	{
		string Address1			{ get; set;	}
        string Address2			{ get; set;	}
		string City				{ get; set;	}
		string State			{ get; set;	}
		string Zip				{ get; set;	}
		string Zip4				{ get; set;	}
	}
	public interface IPaymentReq
	{
		string AccountNumber			{ get; set;	}
		int PaymentMethod				{ get; set;	}
		decimal PaymentAmount			{ get; set;	}
		DateTime PaymentDate			{ get; set;	}
		string IdentificationNumber		{ get; set;	}
		string InstitutionName			{ get; set;	}
	}
}