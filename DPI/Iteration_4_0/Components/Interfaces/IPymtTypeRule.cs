using System;

namespace DPI.Interfaces
{
	public interface IPymtTypeRule
	{
		int Id				{ get;		}
		string PymtRule		{ get; set; }
		string PymtType		{ get; set; }
	}
}
