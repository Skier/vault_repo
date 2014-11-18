using System;
 
namespace DPI.Interfaces 
{	
	public interface IDemand : IDomObj
	{
		int Id				 { get; set; }
		string DmdType		 { get; set; }
		int Statement		 { get; set; }
		ICustInfo2 Consumer	 { get; set; }
		int ConsId           { get; set; }
		string ConsumerAgent { get; set; }
		int Loc		         { get; set; } 
		int Ilec			 { get; }
		string Status		 { get; set; }

		bool IsUnderWF		 { get; set; }
		string Workflow		 { get; set; }
		string WFStep		 { get; set; }
		DateTime DmdDate	 { get; }
		DateTime StatusChangeDate  { get; }
		int		BillPayer	 { get; set; }
		string StoreCode	 { get; set; }
		int    Source        { get; set;}
		
		IOrderSum OrderSummary(IUOW uow);
		void ClearDmdItems();
		void AddDmdItem(IDmdItem di);
		void AddDmdItem(IDmdItem[] dis);
		IDmdItem[] GetDmdItems(IUOW uow);
		IDmdItem[] GetDmdItems();
	}
}