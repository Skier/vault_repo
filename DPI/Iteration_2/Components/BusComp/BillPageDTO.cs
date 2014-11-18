using System;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class BillPageDTO : IBillPageDTO
	{
		#region Member Variables

		private int accNumber;
		private DateTime dueDate;
		private DateTime discoDate;
		private decimal pastDueAmt;
		private decimal currDueAmt;
		private decimal balance;
		private decimal nextPymtAmount;
		private byte billCycle;

		#endregion

		#region Properties

		public int AccNumber
		{
			get { return accNumber; }
			set { accNumber = value; }
		}

		public DateTime DueDate
		{
			get { return dueDate; }
			set { dueDate = value; }
		}

		public DateTime DiscoDate
		{
			get { return discoDate; }
			set { discoDate = value; }
		}

		public decimal PastDueAmt
		{
			get { return pastDueAmt; }
			set { pastDueAmt = Decimal.Round(value, 2); }
		}

		public decimal CurrDueAmt
		{
			get { return currDueAmt; }
			set { currDueAmt = Decimal.Round(value, 2); }
		}

		public decimal Balance
		{
			get { return balance; }
			set { balance = Decimal.Round(value, 2); }
		}

		public decimal NextPymtAmount
		{
			get { return nextPymtAmount; }
			set { nextPymtAmount = value; }
		}

		public byte BillCycle
		{
			get { return billCycle; }
			set { billCycle = value; }
		}

		#endregion
	}
}
