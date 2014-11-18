using System;
using DPI.Interfaces;

namespace DPI.Components
{
	public class VerifoneResult : IVerifoneResult
	{
	#region Data
		protected int id;
		protected string errorCode;
		protected int confNum;
		protected int accNumber;
		protected string customerName;
		protected DateTime transactionTime;
		protected string message;
	#endregion

	#region Properties
		public int Id					{ get { return id; }} 
		public int ConfNum				{ get { return confNum; }}
		public int AccNumber			{ get { return accNumber; }}
		public string CustomerName 		{ get { return customerName; }}
		public DateTime TransactionTime	{ get { return transactionTime; }}
		public string ErrorCode			{ get { return errorCode; }}
		public string Message			{ get { return message; }}
	#endregion
	
	#region Constructors
		public VerifoneResult (
			string _errorCode,
			string _message)
		{
			this.errorCode = _errorCode;
			this.message = _message;
		}
		public VerifoneResult (
			int id,
			string _errorCode, 
			int _confNum, 
			int _accNumber, 			
			string _customerName, 
			DateTime _transactionTime, 			
			string _message)
		{
			this.id = id;
			this.errorCode = _errorCode;
			this.confNum = _confNum;
			this.accNumber = _accNumber;
			this.customerName = _customerName;
			this.transactionTime = _transactionTime;
			this.message = _message;
		}
	#endregion
	}
}