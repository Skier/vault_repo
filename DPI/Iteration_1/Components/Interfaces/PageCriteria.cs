using System;
 
namespace DPI.Interfaces
{
	public interface IPageCriteria
	{
		string CritType { get; set; }
		object Criter   { get ; set; }
	}
}