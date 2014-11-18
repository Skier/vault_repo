package App.Entity
{
	import common.TypesRegistry;
	
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.DefaultBillRateDataObject")]
	public class DefaultBillRateDataObject
	{
		
        public var DefaultBillRateId:int;

        public var AssetId:int;

        private var _billItemTypeId:int;
        public function get BillItemTypeId():int {
        	return _billItemTypeId;
        }
        public function set BillItemTypeId(value:int):void {
        	_billItemTypeId = value;
        	BillItemTypeName = "";
        }
        
        public function get BillItemTypeName():String {
        	var itemType:BillItemTypeDataObject = TypesRegistry.instance.getBillItemTypeById(_billItemTypeId);
        	if (null != itemType) {
        		return itemType.Name;
        	}
        	return "";
        }
        public function set BillItemTypeName(value:String):void {
        }
        
        public function get IsPresetRate():Boolean {
        	return TypesRegistry.instance.getBillItemTypeById(_billItemTypeId).IsPresetRate;
        }

        public var BillRate:Number;
        
        public var Dummy:int;

	}
	
}
