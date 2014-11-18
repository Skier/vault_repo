using System;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class PaymentLogPageDTO : IPaymentLogPageDTO
	{
		#region Member Variables
		
		bool isNew;
		int accNumber;
		int accountPaymentLogId;
		DateTime date;
		string description;
		decimal amount;
		decimal balance;

		IAccountPaymentLog[] accountPaymentLogs;

		#endregion

		#region Properties

		public bool IsNew
		{
			get { return isNew; }
			set { isNew = value; }
		}

		public int AccountPaymentLogId
		{
			get { return accountPaymentLogId; }
			set { accountPaymentLogId = value; }
		}

		public int AccNumber
		{
			get { return accNumber; }
			set { accNumber = value; }
		}

		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public decimal Amount
		{
			get { return amount; }
			set { amount = Decimal.Round(value, 2); }
		}

		public decimal Balance
		{
			get { return balance; }
			set { balance = Decimal.Round(value, 2); }
		}

		public IAccountPaymentLog[] AccountPaymentLogs
		{
			get { return accountPaymentLogs; }			
			set { accountPaymentLogs = value; }
		}

		#endregion
	}
}
