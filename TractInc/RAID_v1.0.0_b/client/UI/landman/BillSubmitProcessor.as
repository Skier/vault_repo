package UI.landman
{
	import App.Domain.*;
	import mx.rpc.Responder;
	import weborb.data.DynamicLoadEvent;
	import weborb.data.ActiveCollection;
	import common.StatusesRegistry;
	import mx.rpc.events.FaultEvent;
	
	public class BillSubmitProcessor
	{
		
		private var _bill:Bill;
		private var _responder:Responder;
		private var _itemsCount:int;
		
		public function BillSubmitProcessor(bill:Bill, responder:Responder) {
			_bill = bill;
			_responder = responder;
			_bill.RelatedBillItem.removeAll();
			_bill.RelatedBillItem.IsLoaded = false;
			if (_bill.RelatedBillItem.IsLoaded) {
				processSubmit();
			} else {
				_bill.RelatedBillItem.addEventListener("loaded", onBillItemsLoaded);
			}
		}
		
        private function onBillItemsLoaded(evt:DynamicLoadEvent):void {
        	_bill.RelatedBillItem.removeEventListener("loaded", onBillItemsLoaded);
        	processSubmit();
        }

        private function processSubmit():void {
	       	_bill.DailyBillAmt = 0;
        	_bill.OtherBillAmt = 0;
    		_bill.TotalBillAmt = 0;
		    _bill.TotalDailyBill = 0;
		    _bill.isSaved = false;
		    
		    var items:ActiveCollection = _bill.RelatedBillItem;
		    _itemsCount = items.length;
		    
		    if (BillStatus.BILL_STATUS_CHANGED == _bill.Status) {
    			_bill.RelatedBillStatus = StatusesRegistry.getInstance().getBillStatusByName(BillStatus.BILL_STATUS_CORRECTED);
        	} else if (BillStatus.BILL_STATUS_NEW == _bill.Status) {
        		_bill.RelatedBillStatus = StatusesRegistry.getInstance().getBillStatusByName(BillStatus.BILL_STATUS_SUBMITTED);
	        } else if (null == _bill.RelatedBillStatus) {
   				_bill.RelatedBillStatus = StatusesRegistry.getInstance().getBillStatusByName(_bill.Status);
   			}
   			
       		for each (var item:BillItem in items) {
				var amount:Number = item.Qty * item.BillRate;

				if (item.BillItemTypeId == 1) {
					_bill.TotalDailyBill += item.Qty;
					_bill.DailyBillAmt += amount;
				} else {
					_bill.OtherBillAmt += amount;
				}

				_bill.TotalBillAmt += amount;

       			if (BillItemStatus.BILL_ITEM_STATUS_CHANGED == item.Status) {
       				item.RelatedBillItemStatus = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_CORRECTED);
       			} else if (BillItemStatus.BILL_ITEM_STATUS_NEW == item.Status) {
   					item.RelatedBillItemStatus = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_SUBMITTED);
       			} else if (null == item.RelatedBillItemStatus) {
       				item.RelatedBillItemStatus = StatusesRegistry.getInstance().getBillItemStatusByName(item.Status);
       			}
       			item.save(false, new Responder(
       				function(obj:Object):void {
       					_itemsCount --;
       					if ((0 == _itemsCount) && (_bill.isSaved)) {
       						_responder.result(BillItem(obj).RelatedBill);
       					}
       				},
       				function(evt:FaultEvent):void {
       					_responder.fault(evt);
       				}
       			));
   			}
       		_bill.save(false, new Responder(
       			function(obj:Object):void {
       				_bill.isSaved = true;
   					if ((0 == _itemsCount) && (_bill.isSaved)) {
   						_responder.result(obj);
   					}
       			},
       			function(evt:FaultEvent):void {
       				_responder.fault(evt);
       			}
       		));
        }
        
	}
	
}
