package App.Entity
{
	import common.TypesRegistry;
	
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.RateByAssignmentDataObject")]
	public class RateByAssignmentDataObject
	{
		
        public var RateByAssignmentId:int;

        public var AssetAssignmentId:int;

        private var _billItemTypeId:int;
        public function get BillItemTypeId():int {
        	return _billItemTypeId;
        }
        public function set BillItemTypeId(value:int):void {
        	_billItemTypeId = value;
        	IsPresetRate = false;
        }

        public var BillRate:Number;

        public var InvoiceRate:Number;

        public var ShouldNotExceedRate:Boolean;

        public var Deleted:Boolean;
        
        public var Dummy:int;
        
        public function get BillItemTypeName():String {
        	return TypesRegistry.instance.getBillItemTypeById(BillItemTypeId).Name;
        }
        
        public var BillRateTemp:Number;
        
        public var InvoiceRateTemp:Number;
        
        public var ShouldNotExceedRateTemp:Boolean;

        public function toTempFields():void {
        	BillRateTemp = BillRate;
        	InvoiceRateTemp = InvoiceRate;
        	ShouldNotExceedRateTemp = ShouldNotExceedRate;
        	
        	if (1 == BillItemTypeId) {
        		BillRateTemp *= 8;
        		InvoiceRateTemp *= 8;
        	}
        }
        
        public function fromTempFields():void {
        	BillRate = BillRateTemp;
        	InvoiceRate = InvoiceRateTemp;
        	ShouldNotExceedRate = ShouldNotExceedRateTemp;
        	
        	if (1 == BillItemTypeId) {
        		BillRate /= 8;
        		InvoiceRate /= 8;
        	}
        }
        
        public function get IsPresetRate():Boolean {
        	return TypesRegistry.instance.getBillItemTypeById(BillItemTypeId).IsPresetRate;
        }
        public function set IsPresetRate(value:Boolean):void {
        }

	}
	
}
