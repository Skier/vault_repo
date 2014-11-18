using System;
//using System
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class EndOfDayDTO
	{
		public readonly DateTime Date; 
		public readonly string StoreCode;
 
		string  storeNumber;
		decimal localRev; 
		decimal otherRev;
	
		decimal localCom;
		decimal otherCom;
		
		decimal creditReceipts; 
		decimal otherReceipts;

		public EndOfDayDTO(DateTime date, string storeCode, string storeNumber)
		{
			this.Date = date;
			this.StoreCode = storeCode;
			this.storeNumber = storeNumber;
		}

		public int ControlNumber
		{
			get 
			{ 
				const int max = 9999;
				const int divisor = 11111;

				int cnt = (int)(Math.Abs(100 * LocalRev
										+ 200 * OtherRev 
										+ 300 * LocalCom
										+ 400 * OtherCom
										+ 500 * CreditReceipts
										+ 600 * OtherReceipts));
                
				if (cnt == 0)
					return 0;

				cnt %= divisor;

				if  (cnt  > max)
					return cnt - max; 
	
				return cnt;
			}
		}
	
		public decimal LocalRev
		{
			get { return decimal.Round(localRev, 2);  }
			set { localRev = decimal.Round(value, 2); }
		}
		public decimal LocalCom
		{
			get { return decimal.Round(localCom, 2);  }
			set { localCom = decimal.Round(value, 2); }
		}
		public decimal OtherRev
		{
			get { return decimal.Round(otherRev, 2);  }
			set { otherRev = decimal.Round(value, 2); }
		}
		public decimal OtherCom
		{
			get { return decimal.Round(otherCom, 2);  }
			set { otherCom = decimal.Round(value, 2); }
		}
		public decimal CreditReceipts
		{
			get { return decimal.Round(creditReceipts, 2);  }
			set { creditReceipts = decimal.Round(value, 2); }
		}
		public decimal OtherReceipts
		{
			get { return decimal.Round(otherReceipts, 2);  }
			set { otherReceipts = decimal.Round(value, 2); }
		}
		public string StoreNumber
		{
			get { return storeNumber;  }
			set { storeNumber = value; }
		}
	}
}