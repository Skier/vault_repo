package UI.manager
{
	
	import App.Domain.AssetAssignment;
	import mx.collections.ArrayCollection;
	import App.Domain.SubAfeStatus;
	import flash.events.IEventDispatcher;
	import App.Domain.Bill;
	import App.Domain.BillItem;
	import App.Domain.ActiveRecords;
	import App.Domain.BillItemStatus;
	import App.Domain.BillStatus;
	import common.StatusesRegistry;

	public class DetailItem // implements IEventDispatcher
	{

		public var afe:String;
		
		public var subAfe:String;
		
		public var totalDailyBill:int;
		
		public var dailyBillAmt:Number;
		
		public var otherAmt:Number;
		
		public var totalBill:Number;
		
		public var notes:String;

		public var assignment:AssetAssignment;

		[Bindable]
		public var items:ArrayCollection;
		
		public var summaryItem:SummaryItem;
		
		[Bindable]
		private var _status:BillItemStatus;
		
		[Bindable]
		public function get status():BillItemStatus {
			return _status;
		}
		
		public function set status(st:BillItemStatus):void {
			_status = st;
        	for each (var item:BillItem in items) {
        		item.RelatedBillItemStatus = st;
        	}
		}
		
		public function get Status():String {
			return _status.Status;
		}
		
		public function updateStatus():void {
           	var isApproved:Boolean = true;
           	var isRejected:Boolean = false;
            
            for each (var item:BillItem in items) {
            	if (item.Status != BillItemStatus.BILL_ITEM_STATUS_APPROVED) {
            		isApproved = false;
            	}
            	if (item.Status == BillItemStatus.BILL_ITEM_STATUS_REJECTED) {
            		isRejected = true;
            	}
            }
            
            if (isRejected) {
            	_status = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_REJECTED);
            } else if (isApproved) {
            	_status = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_APPROVED);
            }
            
//            summaryItem.updateStatus();
            
		}

	}

}
