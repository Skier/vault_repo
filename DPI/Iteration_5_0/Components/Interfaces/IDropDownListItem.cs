using System;
using System.Collections;

namespace DPI.Interfaces
{
	public interface IDropDownListItem  : IComparable
	{
		string DDLText		{ get; set; } 
		string DDLValue		{ get; set; }
	}
}		