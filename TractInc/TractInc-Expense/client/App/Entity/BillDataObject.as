package App.Entity
{
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.BillDataObject")]
	public class BillDataObject
	{
		
		public static const BILL_STATUS_NEW: String       = "NEW";
		public static const BILL_STATUS_SUBMITTED: String = "SUBMITTED";
		public static const BILL_STATUS_REJECTED: String  = "REJECTED";
		public static const BILL_STATUS_CHANGED: String   = "CHANGED";
		public static const BILL_STATUS_CORRECTED: String = "CORRECTED";
		public static const BILL_STATUS_APPROVED: String  = "APPROVED";
		public static const BILL_STATUS_CONFIRMED: String = "CONFIRMED";
		public static const BILL_STATUS_DECLINED: String  = "DECLINED";
		public static const BILL_STATUS_VERIFIED: String  = "VERIFIED";
        
        public function assign(billInfo:BillDataObject):void {
        	Status = billInfo.Status;
        	TotalDailyBill = billInfo.TotalDailyBill;
        	DailyBillAmt = billInfo.DailyBillAmt;
        	OtherBillAmt = billInfo.OtherBillAmt;
        	TotalBillAmt = billInfo.TotalBillAmt;
        }
        
        public var BillId:int;

        public var Status:String;

        public var StartDate:String;

        public var AssetId:int;

        public var TotalDailyBill:int;

        public var DailyBillAmt:Number = 0;

        public var OtherBillAmt:Number = 0;

        public var TotalBillAmt:Number = 0;

        public var BillItems:Array;
        
        public var Notes:Array;
        
        public var Compositions:Array;
        
        public var Users:Array;
        
        public var AssetInfo:AssetDataObject;
        
        public var ChiefAssetInfo:AssetDataObject;
        
        public var UserInfo:AssetDataObject;
        
        public var Dummy:int;
        
        public function get AssetName():String {
        	if (null != AssetInfo) {
        		return AssetInfo.BusinessName;
        	} else {
        		return "";
        	}
        }
        
        public function get ChiefAssetName():String {
        	if (null != ChiefAssetInfo) {
        		return AssetInfo.BusinessName;
        	} else {
        		return "";
        	}
        }

            public function isBillEditable():Boolean {
                return (Status == BILL_STATUS_NEW)
                    || (Status == BILL_STATUS_REJECTED)
                    || (Status == BILL_STATUS_CHANGED);
            }
            
            public function get isReadOnly():Boolean {
            	return isBillEditable();
            }
            
            public function set isReadOnly(value:Boolean):void {
            	
            }
            
            public var toSubmit:Boolean = false;
            
            public var isSaved:Boolean = false;
            
            public var statusTemp:String;
            
            public function toTempFields():void {
            	statusTemp = Status;
            }
            
            public function fromTempFields():void {
            	Status = statusTemp;
            }
            
            public function get amountString():String {
            	return "$" + (Math.round(TotalBillAmt * 100) / 100).toFixed(2);
            }

	}
}