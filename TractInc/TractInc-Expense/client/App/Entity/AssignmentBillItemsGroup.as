package App.Entity
{
	import mx.collections.ArrayCollection;
	import flash.events.EventDispatcher;
	import mx.binding.utils.ChangeWatcher;
	import common.StatusesRegistry;
	import mx.events.PropertyChangeEvent;
	import mx.events.PropertyChangeEventKind;
	
	[Bindable]
	public class AssignmentBillItemsGroup extends EventDispatcher
	{
		private var _assetAssignment:AssetAssignmentDataObject;
		public function get assetAssignment():AssetAssignmentDataObject { 
			return _assetAssignment; 
		}
		public function set assetAssignment(assignment:AssetAssignmentDataObject):void { 
			_assetAssignment = assignment; 
		}
		
		private var _totalDailyBill:Number = 0;
		public function get totalDailyBill():Number { 
			return _totalDailyBill;	
		}

		private var _dailyBillAmount:Number = 0;
		public function get dailyBillAmount():Number { 
			return _dailyBillAmount;	
		}

		private var _otherAmount:Number = 0;
		public function get otherAmount():Number { 
			return _otherAmount;	
		}

		private var _totalBillAmount:Number = 0;
		public function get totalBillAmount():Number { 
			return _totalBillAmount;	
		}

		private var _status:String;
		public function get status():String{ 
			return _status; 
		}
		public function set status(billItemStatus:String):void { 

			for each (var billItem:BillItemDataObject in _billItems) {
				if (billItemStatus != BillItemStatusDataObject.BILL_ITEM_STATUS_CONFIRMED) {
					billItem.Status = billItemStatus;
				}
			}
			
			_status = billItemStatus; 
           	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, false, false, PropertyChangeEventKind.UPDATE, "status"));
		}
		
		private var _statusTemp:String;
		public function get statusTemp():String { 
			return _statusTemp; 
		}
		public function set statusTemp(billItemStatus:String):void { 

			for each (var billItem:BillItemDataObject in _billItems) {
				if (billItem.StatusTemp != BillItemStatusDataObject.BILL_ITEM_STATUS_CONFIRMED 
						|| billItemStatus == BillItemStatusDataObject.BILL_ITEM_STATUS_DECLINED) {
					billItem.StatusTemp = billItemStatus;
				}
			}
			
			_statusTemp = billItemStatus; 
           	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, false, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
		}
		
		public function get Status():String {
			if (_status) {
				return _status;
			} else {
				var isCorrected:Boolean = false;
				var isSubmitted:Boolean = false;
				var isRejected:Boolean = false;
				var isApproved:Boolean = true;

				for each (var billItem:BillItemDataObject in _billItems) {
					if (billItem.Status == BillItemStatusDataObject.BILL_ITEM_STATUS_CORRECTED) {
						isCorrected = true;
					}
					if (billItem.Status == BillItemStatusDataObject.BILL_ITEM_STATUS_SUBMITTED) {
						isSubmitted = true;
					}
					if (billItem.Status == BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED) {
						isRejected = true;
					}
					if (billItem.Status != BillItemStatusDataObject.BILL_ITEM_STATUS_APPROVED) {
						isApproved = false;
					}
				}
				
				if (isRejected) {
					return BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED;
				} else if (isCorrected) {
					return BillItemStatusDataObject.BILL_ITEM_STATUS_CORRECTED;
				} else if (isSubmitted) {
					return BillItemStatusDataObject.BILL_ITEM_STATUS_SUBMITTED;
				} else if (isApproved) {
					return BillItemStatusDataObject.BILL_ITEM_STATUS_APPROVED;
				} else {
					return "_undefined_";
				}
			}
		}

		private var _billItems:ArrayCollection = new ArrayCollection();
		public function get billItems():ArrayCollection { return _billItems; }

		public function AssignmentBillItemsGroup() {
			// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			_status = BillItemDataObject.BILL_ITEM_STATUS_SUBMITTED;
		}
		
		private var _dailyItemsCount:int = 0;
		public function get dailyItemsCount():int {
			return _dailyItemsCount;
		}
		public function set dailyItemsCount(value:int):void {
			_dailyItemsCount = value;
		}
		
		public function addBillItem(billItem:BillItemDataObject):void {
			if (billItem == null)
				return;
			
			ChangeWatcher.watch(billItem, ["Qty"], onBillItemChanged);
			ChangeWatcher.watch(billItem, ["BillRate"], onBillItemChanged);
			ChangeWatcher.watch(billItem, ["Status"], onBillItemStatusChanged);
			ChangeWatcher.watch(billItem, ["StatusTemp"], onStatusTempChanged);
			
			billItems.addItem(billItem);
			
			if (1 == billItem.BillItemTypeId) {
				dailyItemsCount++;
			}
			
			updateStatus();
			updateStatusTemp();
			recalculateSummary();
		}
		
		private function onBillItemChanged(event:*):void {
			recalculateSummary();
		}

		private function onBillItemStatusChanged(event:*):void {
			updateStatus();
		}

		private function onStatusTempChanged(event:*):void {
			updateStatusTemp();
		}

		private function recalculateSummary():void {
			
			_totalDailyBill = 0;
			_dailyBillAmount = 0;
			_otherAmount = 0;
			_totalBillAmount = 0;
			
			for each (var billItem:BillItemDataObject in _billItems) {

				var amount:Number = billItem.Qty * billItem.BillRate;

				if (billItem.BillItemTypeId == 1) {
					_totalDailyBill += billItem.Qty;
					_dailyBillAmount += amount;
				} else {
					_otherAmount += amount;
				}

				_totalBillAmount += amount;
			}
			
		}

		private function updateStatus():void {

			var isCorrected:Boolean = false;
			var isSubmitted:Boolean = false;
           	var isApproved:Boolean = true;
           	var isRejected:Boolean = false;
           	var isConfirmed:Boolean = false;
            
            for each (var item:BillItemDataObject in _billItems) {
            	if (item.Status != BillItemDataObject.BILL_ITEM_STATUS_APPROVED
            			&& item.Status != BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED) {
            		isApproved = false;
            	}
            	if (item.Status == BillItemDataObject.BILL_ITEM_STATUS_REJECTED) {
            		isRejected = true;
            	}
            	if (item.Status == BillItemDataObject.BILL_ITEM_STATUS_CORRECTED) {
            		isCorrected = true;
            	}
            	if (item.Status == BillItemDataObject.BILL_ITEM_STATUS_SUBMITTED) {
            		isSubmitted = true;
            	}

            	if (item.Status == BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED) {
            		isConfirmed = true;
            	}

            }
            
            if (isRejected) {
            	_status = BillItemDataObject.BILL_ITEM_STATUS_REJECTED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "status"));
            } else if (isCorrected) {
            	_status = BillItemDataObject.BILL_ITEM_STATUS_CORRECTED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "status"));
            } else if (isSubmitted) {
            	_status = BillItemDataObject.BILL_ITEM_STATUS_SUBMITTED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "status"));
            } else if (isConfirmed) {
            	_status = BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "status"));
            } else if (isApproved) {
            	_status = BillItemDataObject.BILL_ITEM_STATUS_APPROVED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "status"));
            }
            
		}

		private function updateStatusTemp():void {

			var isCorrected:Boolean = false;
			var isSubmitted:Boolean = false;
           	var isApproved:Boolean = true;
           	var isRejected:Boolean = false;
           	var isConfirmed:Boolean = true;
           	var isDeclined:Boolean = false;
            
            for each (var item:BillItemDataObject in _billItems) {
            	if (item.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_APPROVED
            			&& item.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED) {
            		isApproved = false;
            	}
            	if (item.StatusTemp == BillItemDataObject.BILL_ITEM_STATUS_REJECTED) {
            		isRejected = true;
            	}
            	if (item.StatusTemp == BillItemDataObject.BILL_ITEM_STATUS_CORRECTED) {
            		isCorrected = true;
            	}
            	if (item.StatusTemp == BillItemDataObject.BILL_ITEM_STATUS_SUBMITTED) {
            		isSubmitted = true;
            	}
            	if (item.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED) {
            		isConfirmed = false;
            	}
            	if (item.StatusTemp == BillItemDataObject.BILL_ITEM_STATUS_DECLINED) {
            		isDeclined = true;
            	}
            }
            
            if (isRejected) {
            	_statusTemp = BillItemDataObject.BILL_ITEM_STATUS_REJECTED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            } else if (isCorrected) {
            	_statusTemp = BillItemDataObject.BILL_ITEM_STATUS_CORRECTED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            } else if (isSubmitted) {
            	_statusTemp = BillItemDataObject.BILL_ITEM_STATUS_SUBMITTED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            } else if (isConfirmed) {
            	_statusTemp = BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            } else if (isApproved) {
            	_statusTemp = BillItemDataObject.BILL_ITEM_STATUS_APPROVED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            } else if (isDeclined) {
            	_statusTemp = BillItemDataObject.BILL_ITEM_STATUS_DECLINED;
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            }
            
		}

	}
}