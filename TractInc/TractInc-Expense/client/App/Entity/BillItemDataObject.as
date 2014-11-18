package App.Entity
{
	
	import common.TypesRegistry;
	import App.Domain.BillItemType;
	import mx.collections.ArrayCollection;
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.BillItemDataObject")]
	public class BillItemDataObject
	{

		public static const BILL_ITEM_STATUS_NEW: String       = "NEW";
		public static const BILL_ITEM_STATUS_SUBMITTED: String = "SUBMITTED";
		public static const BILL_ITEM_STATUS_REJECTED: String  = "REJECTED";
		public static const BILL_ITEM_STATUS_CHANGED: String   = "CHANGED";
		public static const BILL_ITEM_STATUS_CORRECTED: String = "CORRECTED";
		public static const BILL_ITEM_STATUS_APPROVED: String  = "APPROVED";
		public static const BILL_ITEM_STATUS_CONFIRMED: String = "CONFIRMED";
		public static const BILL_ITEM_STATUS_DECLINED: String  = "DECLINED";
		public static const BILL_ITEM_STATUS_VERIFIED: String  = "VERIFIED";
        
        public var BillItemId:int;
        
        public var BillItemTypeId:int;

        public var BillId:int;

        public var AssetAssignmentId:int;

        public var BillingDate:String;

        public var Qty:int;

        public var BillRate:Number;

        public var Status:String;

        public var Notes:Array;
        
        public var NotesTemp:Array;

        public var BillItemCompositionId:int;

		public var AttachmentInfo:BillItemAttachmentDataObject;
		
		public var AttachmentInfoTemp:BillItemAttachmentDataObject;
		
		public var WorkLogInfo:WorkLogDataObject;
		
		public var WorkLogInfoTemp:WorkLogDataObject;
		
		public var Afe:String;
		
		public var SubAfe:String;

		//

        public var isSaved:Boolean = false;
        public var isSelected:Boolean = false;
        public function setSelected(selected:Boolean):void {
        	isSelected = selected;
        }
            
        public var QtyTemp:int;

        public var BillRateTemp:Number;

        private var _billItemTypeIdTemp:int;
        public function get BillItemTypeIdTemp():int {
        	return _billItemTypeIdTemp;
        }
        public function set BillItemTypeIdTemp(value:int):void {
        	if (-1 == value) {
        		_billItemTypeIdTemp = 0;
        		BillItemTypeName = "";
        		IsPresetRate = true;
        		IsCountable = false;
        		IsSingle = false;
        		return;
        	}
        	_billItemTypeIdTemp = value;
        	
        	var itemType:BillItemTypeDataObject;
        	for each (itemType in TypesRegistry.instance.billItemTypes) {
        		if (itemType.BillItemTypeId == value) {
        			break;
        		}
        	}
        	
        	BillItemTypeName = itemType.Name;
        	IsPresetRate = itemType.IsPresetRate;
        	IsCountable = itemType.IsCountable;
        	IsSingle = itemType.IsSingle;
        }
        
        private var _billItemTypeName:String;
        public function get BillItemTypeName():String {
        	return _billItemTypeName;
        }
        public function set BillItemTypeName(value:String):void {
        	_billItemTypeName = value;
        }
        
        private var _isPresetRate:Boolean;
        public function get IsPresetRate():Boolean {
        	return _isPresetRate;
        }
        public function set IsPresetRate(value:Boolean):void {
        	_isPresetRate = value;
        }
        
        private var _isCountable:Boolean;
        public function get IsCountable():Boolean {
        	return _isCountable;
        }
        public function set IsCountable(value:Boolean):void {
        	_isCountable = value;
        }
        
        private var _isSingle:Boolean;
        public function get IsSingle():Boolean {
        	return _isSingle;
        }
        public function set IsSingle(value:Boolean):void {
        	_isSingle = value;
        }
        
        private var _isMarkedToRemove:Boolean;
        public function get IsMarkedToRemove():Boolean {
        	return _isMarkedToRemove;
        }
		public function set IsMarkedToRemove(value:Boolean):void {
			_isMarkedToRemove = value;
		}
            
        public var StatusTemp:String;
            
        public function toTempFields():void {
        	QtyTemp = Qty;
           	BillRateTemp = BillRate;
            BillItemTypeIdTemp = BillItemTypeId;
            StatusTemp = Status;
            NotesTemp = (new ArrayCollection(Notes)).toArray();
            IsMarkedToRemove = false;
            
            if (null != AttachmentInfo) {
            	AttachmentInfoTemp = AttachmentInfo.clone();
            } else {
            	AttachmentInfoTemp = null;
            }
        }

        public function fromTempFields():void {
           	Qty = QtyTemp;
           	BillRate = BillRateTemp;
           	BillItemTypeId = BillItemTypeIdTemp;
           	Status = StatusTemp;
           	Notes = NotesTemp;
           	IsMarkedToRemove = false;
           	
           	if (null == AttachmentInfo) {
           		AttachmentInfo = AttachmentInfoTemp;
           	}
           	
           	if (null != AttachmentInfo
           			&& null != AttachmentInfoTemp) {
           		AttachmentInfo.assign(AttachmentInfoTemp);
           	}
        }
            
        public function isBillItemEditableOld():Boolean {
            return ((Status == BILL_ITEM_STATUS_NEW)
                || (Status == BILL_ITEM_STATUS_REJECTED)
                || (Status == BILL_ITEM_STATUS_CHANGED));
        }
            
        public function isBillItemEditable():Boolean {
            return isBillItemEditableOld()
                && (0 == BillItemCompositionId);
        }
        
        public var Dummy:int;
        
	}
	
}
