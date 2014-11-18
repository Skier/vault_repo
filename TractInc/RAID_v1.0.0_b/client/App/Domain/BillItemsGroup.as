package App.Domain
{
	
	import mx.collections.ArrayCollection;
	import flash.events.EventDispatcher;
	import mx.binding.utils.ChangeWatcher;
	
	[Bindable]
	public class BillItemsGroup extends EventDispatcher
	{
		
		private var _parentAfe:Afe;
		public function get parentAfe():Afe { return _parentAfe; }
		public function set parentAfe(afe:Afe):void { _parentAfe = afe; }
		
		private var _parentSubAfe:SubAfe;
		public function get parentSubAfe():SubAfe { return _parentSubAfe; }
		public function set parentSubAfe(subAfe:SubAfe):void { _parentSubAfe = subAfe; }
		
		private var _totalDailyBill:Number = 0;
		public function get totalDailyBill():Number { return _totalDailyBill;	}

		private var _dailyBillAmount:Number = 0;
		public function get dailyBillAmount():Number { return _dailyBillAmount;	}

		private var _otherAmount:Number = 0;
		public function get otherAmount():Number { return _otherAmount;	}

		private var _totalBillAmount:Number = 0;
		public function get totalBillAmount():Number { return _totalBillAmount;	}

		private var _status:BillItemStatus;
		public function get status():BillItemStatus { return _status; }
		public function set status(billItemStatus:BillItemStatus):void { _status = billItemStatus; }

		private var _billItems:ArrayCollection = new ArrayCollection();
		public function get billItems():ArrayCollection { return _billItems; }
		
		private var _bill:Bill = null;

		public function addBillItem(billItem:BillItem):void {
			if (billItem == null) {
				return;
			}
			
			if (null == _bill) {
				_bill = billItem.RelatedBill;
			} else if (_bill.BillId != billItem.BillId) {
				throw new Error("Cannot add BillItem from another Bill");
			}
			
			ChangeWatcher.watch(billItem, ["Qty"], onBillItemChanged);
			ChangeWatcher.watch(billItem, ["BillRate"], onBillItemChanged);
			ChangeWatcher.watch(billItem, ["RelatedBillItemStatus"], onBillItemStatusChanged);
			
			billItems.addItem(billItem);
			
			updateStatus();
			recalculateSummary();
		}
		
		private function onBillItemChanged(event:*):void {
			recalculateSummary();
		}

		private function onBillItemStatusChanged(event:*):void {
			updateStatus();
		}

		private function recalculateSummary():void {
			totalDailyBill = 0;
			dailyBillAmount = 0;
			otherAmount = 0;
			totalBillAmount = 0;
			
			for each (var billItem:BillItem in _billItems) {
				var amount:Number = billItem.Qty * billItem.BillRate;

				if (billItem.BillItemTypeId == 1) {
					totalDailyBill += billItem.Qty;
					dailyBillAmount += amount;
				} else {
					otherAmount += amount;
				}

				totalBillAmount += amount;
			}
		}

		private function updateStatus():void {
           	var isApproved:Boolean = true;
           	var isRejected:Boolean = false;
            
            for each (var item:BillItem in _billItems) {
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
		}

	}

}
