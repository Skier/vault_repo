package App.Entity
{
	import mx.collections.ArrayCollection;
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.BillItemCompositionDataObject")]
	public class BillItemCompositionDataObject
	{
		
        public var BillItemCompositionId:int;

        public var BillId:int;

		// public 
        public var BillItemTypeId:int;
        
        public var Amount:Number;
        
        public var Description:String;
        
        public var IsDeleted:Boolean;
        
        public var BillItems:Array;
        
        public var BillInfo:BillDataObject;
        
        public var AttachmentInfo:BillItemAttachmentDataObject;
        
        public var Notes:Array;
        
        public function isCompositeItemEditable():Boolean {
        	if (0 == BillItems.length) {
        		return true;
        	}
        	
        	for each (var item:BillItemDataObject in BillItems) {
        		if (item.isBillItemEditableOld()) {
        			return true;
        		}
        	}
        	return false;
        }
        
        public var Dummy:int;
        
	}
	
}
