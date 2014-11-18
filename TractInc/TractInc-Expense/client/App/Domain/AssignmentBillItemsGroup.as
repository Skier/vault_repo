package App.Domain
{
	import mx.collections.ArrayCollection;
	import flash.events.EventDispatcher;
	import mx.binding.utils.ChangeWatcher;
	import common.StatusesRegistry;
	import mx.events.PropertyChangeEvent;
	import mx.events.PropertyChangeEventKind;
	import App.Entity.BillItemStatusDataObject;
	import App.Entity.BillItemDataObject;
	
	[Bindable]
	public class AssignmentBillItemsGroup extends EventDispatcher
	{
		private var _assetAssignment:AssetAssignment;
		public function get assetAssignment():AssetAssignment { 
			return _assetAssignment; 
		}
		public function set assetAssignment(assignment:AssetAssignment):void { 
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

		private var _status:BillItemStatusDataObject;
		public function get status():BillItemStatusDataObject { 
			return _status; 
		}
		public function set status(billItemStatus:BillItemStatusDataObject):void { 

			for each (var billItem:BillItem in _billItems) {
				if (billItemStatus.Status != BillItemStatusDataObject.BILL_ITEM_STATUS_CONFIRMED) {
					// TODO: !!!
					billItem.RelatedBillItemStatus = ActiveRecords.BillItemStatus.findByPrimaryKey(billItemStatus.Status);
				}
			}
			
			_status = billItemStatus; 
           	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, false, false, PropertyChangeEventKind.UPDATE, "status"));
		}
		
		private var _statusTemp:BillItemStatusDataObject;
		public function get statusTemp():BillItemStatusDataObject { 
			return _statusTemp; 
		}
		public function set statusTemp(billItemStatus:BillItemStatusDataObject):void { 

			for each (var billItem:BillItem in _billItems) {
				if (billItem.StatusTemp.Status != BillItemStatusDataObject.BILL_ITEM_STATUS_CONFIRMED 
					|| billItemStatus.Status == BillItemStatusDataObject.BILL_ITEM_STATUS_DECLINED) {
					billItem.StatusTemp = billItemStatus;
				}
			}
			
			_statusTemp = billItemStatus; 
           	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, false, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
		}
		
		private var _notesTemp:String;
		public function get notesTemp():String { 
			return _notesTemp;
		}
		public function set notesTemp(notes:String):void { 

			for each (var billItem:BillItem in _billItems) {
				if (billItem.StatusTemp.Status != BillItemStatusDataObject.BILL_ITEM_STATUS_CONFIRMED) {
					billItem.NotesTemp = notes;
				}
			}
			
			_notesTemp = notes; 
           	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, false, false, PropertyChangeEventKind.UPDATE, "notesTemp"));
		}
		
		public function get Status():String {
			if (_status) {
				return _status.Status;
			} else {
				var isCorrected:Boolean = false;
				var isSubmitted:Boolean = false;
				var isRejected:Boolean = false;
				var isApproved:Boolean = true;

				for each (var billItem:BillItem in _billItems) {
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
//			_status = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_SUBMITTED);
//			_status = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_SUBMITTED);
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
			
			for each (var billItem:BillItem in _billItems) {

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
            
            for each (var item:BillItem in _billItems) {
            	if (item.Status != BillItemStatus.BILL_ITEM_STATUS_APPROVED && item.Status != BillItemStatus.BILL_ITEM_STATUS_CONFIRMED) {
            		isApproved = false;
            	}
            	if (item.Status == BillItemStatus.BILL_ITEM_STATUS_REJECTED) {
            		isRejected = true;
            	}
            	if (item.Status == BillItemStatus.BILL_ITEM_STATUS_CORRECTED) {
            		isCorrected = true;
            	}
            	if (item.Status == BillItemStatus.BILL_ITEM_STATUS_SUBMITTED) {
            		isSubmitted = true;
            	}

            	if (item.Status == BillItemStatus.BILL_ITEM_STATUS_CONFIRMED) {
            		isConfirmed = true;
            	}

            }
            
            if (isRejected) {
            	_status = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_REJECTED);
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "status"));
            } else if (isCorrected) {
            	_status = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_CORRECTED);
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "status"));
            } else if (isSubmitted) {
            	_status = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_SUBMITTED);
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "status"));
            } else if (isConfirmed) {
            	_status = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_CONFIRMED);
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "status"));
            } else if (isApproved) {
            	_status = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_APPROVED);
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
            
            for each (var item:BillItem in _billItems) {
            	if (item.StatusTemp.Status != BillItemStatus.BILL_ITEM_STATUS_APPROVED  && item.StatusTemp.Status != BillItemStatus.BILL_ITEM_STATUS_CONFIRMED) {
            		isApproved = false;
            	}
            	if (item.StatusTemp.Status == BillItemStatus.BILL_ITEM_STATUS_REJECTED) {
            		isRejected = true;
            	}
            	if (item.StatusTemp.Status == BillItemStatus.BILL_ITEM_STATUS_CORRECTED) {
            		isCorrected = true;
            	}
            	if (item.StatusTemp.Status == BillItemStatus.BILL_ITEM_STATUS_SUBMITTED) {
            		isSubmitted = true;
            	}
            	if (item.StatusTemp.Status != BillItemStatus.BILL_ITEM_STATUS_CONFIRMED) {
            		isConfirmed = false;
            	}
            	if (item.StatusTemp.Status == BillItemStatus.BILL_ITEM_STATUS_DECLINED) {
            		isDeclined = true;
            	}
            }
            
            if (isRejected) {
            	_statusTemp = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_REJECTED);
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            } else if (isCorrected) {
            	_statusTemp = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_CORRECTED);
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            } else if (isSubmitted) {
            	_statusTemp = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_SUBMITTED);
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            } else if (isConfirmed) {
            	_statusTemp = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_CONFIRMED);
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            } else if (isApproved) {
            	_statusTemp = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_APPROVED);
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            } else if (isDeclined) {
            	_statusTemp = StatusesRegistry.instance.getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_DECLINED);
            	dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, true, false, PropertyChangeEventKind.UPDATE, "statusTemp"));
            }
            
		}

	}
}