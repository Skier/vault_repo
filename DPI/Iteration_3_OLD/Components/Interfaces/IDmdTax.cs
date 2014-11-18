using System;
 
namespace DPI.Interfaces
{	
	public interface IDmdTax : ITaxInfo
	{
		int Id             { get; }
		IDmdItem DmdItm    { get; set; }
	}
}