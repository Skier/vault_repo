package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.WorkLogDataObject")]
	public class WorkLogDataObject
	{

		public var WorkLogId:int;

        public var BillItemId:int;

        public var LogMessage:String;
        
        public var BillItemInfo:BillItemDataObject;

	}
	
}