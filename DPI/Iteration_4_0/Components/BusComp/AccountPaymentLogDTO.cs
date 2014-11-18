using System;
using DPI.Interfaces;

namespace DPI.Components
{
	public class AccountPaymentLogDTO : IAccountPaymentLog
	{
		private int accountPaymentLogId;		
		private DateTime date;
		private string description;
		private decimal amount;
		private decimal balance;

		public int Account_PaymentLog_ID
		{
			get { return accountPaymentLogId; }
			set { accountPaymentLogId = value; }
		}

		public DateTime Date
		{
			get { return date; }
			set	{ date = value; }
		}

		public string Description
		{
			get { return description; }
			set	{ description = value; }			
		}

		public decimal Amount
		{
			get { return amount; }
			set { amount = Decimal.Round(value, 2);	}
		}

		public decimal Balance
		{
			get { return balance; }
			set	{ balance = Decimal.Round(value, 2); }
		}
	}
}