package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.InvoiceItemDataObject")]
	public dynamic class InvoiceItemDataObject
	{
		
        public var InvoiceItemId:int;

        public var InvoiceItemTypeId:int;

        public var InvoiceId:int;

        public var BillItemId:int;

        public var AssetAssignmentId:int;

        public var InvoiceDate:String;

        public var Qty:int;

        public var InvoiceRate:Number;

        public var Status:String;

        public var IsSelected:Boolean;
        
        public var isNew:Boolean;
        
        public var BillItem:BillItemDataObject;
        
        public var Deleted:Boolean;
        
        public var Dummy:int;
        
        public var Notes:Array;

        public function setSelected(selected:Boolean):void 
        {
         	IsSelected = selected;
        }
        
	}
	
}
