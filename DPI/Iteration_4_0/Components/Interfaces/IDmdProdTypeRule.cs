using System;
 
namespace DPI.Interfaces
{	
	public interface IDmdProdTypeRule
	{
		int Id          { get; }
		string DmdType  { get; }
		string ProdType { get; }
	}
}