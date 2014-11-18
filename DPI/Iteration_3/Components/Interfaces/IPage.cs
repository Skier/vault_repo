using System;

namespace DPI.Interfaces
{
	public interface IPage 
	{
		int Rows { get; set; }
		IPageCriteria Criteria { get; set; }
		PageDirection Direction  { get; set; }
	}
}
