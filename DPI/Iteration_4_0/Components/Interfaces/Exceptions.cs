using System;
 
namespace DPI.Interfaces
{
	public class TransactionException : Exception
	{
		public TransactionException() : base()
		{
		}
		public TransactionException(string message) : base(message)
		{
		}
	}
}			