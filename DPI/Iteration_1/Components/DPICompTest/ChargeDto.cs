using System;
using DPI.Interfaces;
 
namespace DPI.Components
{	
	public class ChargeDto : IChargeDto
	{
		int dlvId;
		decimal amt;
		public int DlvId	 
		{ 
			get { return dlvId; }
			set { dlvId = value; }
		}
		public decimal Amt  
		{
			get { return amt; }
			set { amt = Decimal.Round(value, 2); }
		}
	}
}