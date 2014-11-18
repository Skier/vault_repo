using System;

namespace DPI.Components
{
	public class VerifoneMonthlyResult : VerifoneResult
	{
	#region Data
		protected string accStatus;
		protected decimal balance;
		protected DateTime currentDueDate;
		protected DateTime sDiscoDate;
	#endregion

	#region Properties
		public string AccStatus
		{
			get { return accStatus; }
		}
		public decimal Balance
		{
			get { return balance; }
		}
		public DateTime CurrentDueDate
		{
			get { return currentDueDate; }		
		}
		public DateTime SDiscoDate
		{
			get { return sDiscoDate; }
		}
	#endregion

	#region Constructors
		public VerifoneMonthlyResult (
			string _errorCode,
			string _message) : base (_errorCode, _message)
		{
		}
		public VerifoneMonthlyResult (
			int id,
			string _errorCode, 
			int _confNum, 
			int _accNumber, 			
			string _customerName, 
			DateTime _transactionTime, 			
			string _message,
			string _accStatus,
			decimal _balance,
			DateTime _currentDueDate,
			DateTime _sDiscoDate) : base(id, _errorCode, _confNum, _accNumber, _customerName, _transactionTime, _message)
		{
			this.accStatus = _accStatus;
			this.balance = _balance;
			this.currentDueDate = _currentDueDate;
			this.sDiscoDate = _sDiscoDate;
		}
	#endregion
	}
}