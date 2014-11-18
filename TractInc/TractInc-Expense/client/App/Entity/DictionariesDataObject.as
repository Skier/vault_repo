package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.DictionariesDataObject")]
	public class DictionariesDataObject
	{
		
        public var BillStatuses:Array;

        public var BillItemStatuses:Array;

        public var InvoiceStatuses:Array;

        public var InvoiceItemStatuses:Array;
        
        public var BillItemTypes:Array;
        
        public var InvoiceItemTypes:Array;
        
        public var AssetTypes:Array;
        
        public var AFEStatuses:Array;
        
        public var ProjectStatuses:Array;

	}
	
}
