package UI.landman
{
	import App.Domain.*;
	import mx.rpc.Responder;
	import weborb.data.ActiveCollection;
	import weborb.data.DynamicLoadEvent;
	import common.StatusesRegistry;
	import weborb.data.ActiveRecordEvent;
	
	public class StatusUpdateProcessor
	{
		
		private var _bill:Bill;
		private var _responder:Responder;
		
		public function StatusUpdateProcessor(bill:Bill, responder:Responder) {
			_bill = bill;
			_responder = responder;
			
			var items:ActiveCollection = _bill.RelatedBillItem;
			if (!items.IsLoaded) {
				items.addEventListener("loaded", onBillItemsLoaded);
			} else {
				processUpdateStatus();
			}
		}
		
		private function onBillItemsLoaded(evt:DynamicLoadEvent):void {
			ActiveCollection(evt.data).removeEventListener("loaded", onBillItemsLoaded);
			processUpdateStatus();
		}
		
		private function processUpdateStatus():void {
			var hasChanged:Boolean = false;
			var hasRejected:Boolean = false;
			var hasOnlyConfirmed:Boolean = true;
			
			/* if (BillStatus.BILL_STATUS_NEW == _bill.Status) {
				_responder.result(null);
				return;
			} */
			
			var items:ActiveCollection = _bill.RelatedBillItem;
			for each (var item:BillItem in items) {
				if (BillItemStatus.BILL_ITEM_STATUS_CHANGED == item.Status) {
					hasChanged = true;
				}
				if (BillItemStatus.BILL_ITEM_STATUS_REJECTED == item.Status) {
					hasRejected = true;
				}
				if (BillItemStatus.BILL_ITEM_STATUS_CONFIRMED != item.Status) {
					hasOnlyConfirmed = false;
				}
			}
			
			if (hasChanged && !hasRejected && (BillStatus.BILL_STATUS_NEW != _bill.Status)
					|| (hasOnlyConfirmed && BillStatus.BILL_STATUS_CHANGED != _bill.Status)) {
				_bill.RelatedBillStatus = StatusesRegistry.getInstance().getBillStatusByName(BillStatus.BILL_STATUS_CHANGED);
			}
			
			if (_bill.IsLocked) {
				_bill.addEventListener("loaded", onBillUnlocked);
			} else {
				_bill.save(false, _responder);
			}
		}
		
		private function onBillUnlocked(evt:Object):void {
			_bill.removeEventListener("loaded", onBillUnlocked);
			_bill.save(false, _responder);
		}
		
	}
	
}
