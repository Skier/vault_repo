using System;
 
namespace DPI.Components
{
	public class ConcurrencyException : Exception
	{
		public ConcurrencyException() : base(
			"A row you were trying to update has been changed by another user. "
			+ "Please refresh your data and try again")
		{
		}
		public ConcurrencyException(string table) : base(
			"A row in the '" + table +"' table you were trying to update has been changed by another user. "
			+ "Please refresh your data and try again")
		{
		}
	}
}			