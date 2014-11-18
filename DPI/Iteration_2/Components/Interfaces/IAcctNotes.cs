using System;
 
namespace DPI.Interfaces
{	
	public interface IAcctNotes
	{
		string   User { get; }
		string   Text { get; set; }
		DateTime Date { get; }
	}
}