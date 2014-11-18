using System;

namespace DPI.Interfaces
{
	public interface IWIP
	{
		IWorkflow Workflow  { get; }
		string    StoreCode { get; }
		int       Prod      { get; set; }
		int       WLProd    { get; set; }
		string    ProdGroup { get; set; }

		object this[string attr] { get; set; }
	}
}