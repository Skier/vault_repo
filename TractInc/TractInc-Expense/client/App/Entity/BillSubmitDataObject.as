package App.Entity
{
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.BillSubmitDataObject")]
	public class BillSubmitDataObject
	{
		
        public var BillId:int;
        
        public var Notes:Array;
        
        public var Attachments:Array;
        
        public var Status:String;
        
        public var BillItems:Array;
        
	}
}