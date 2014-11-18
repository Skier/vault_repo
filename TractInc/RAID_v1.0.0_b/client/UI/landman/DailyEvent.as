package UI.landman
{
	
	import mx.collections.ArrayCollection;
	import mx.binding.utils.ChangeWatcher;
	import App.Domain.BillItem;
	import App.Domain.BillItemType;
	import App.Domain.Bill;
	import App.Domain.BillItemStatus;
	import App.Domain.AfeStatus;
	import App.Domain.SubAfeStatus;
	import common.StatusesRegistry;
	import common.TypesRegistry;
	import mx.collections.ListCollectionView;
	import App.Domain.AssetAssignment;
	import mx.events.FlexEvent;
	import mx.events.PropertyChangeEvent;
	import util.ArrayUtil;
	import util.DateUtil;
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;
	import mx.rpc.events.FaultEvent;
    import mx.rpc.Responder;
    import mx.controls.Alert;
	import calendar.MonthPeriodEvent;
    import calendar.MonthPeriodCell;
    import calendar.MonthPeriodGrid;

	public class DailyEvent extends MonthPeriodEvent
	{
		
		public var _billItems:ArrayCollection = new ArrayCollection();
		private var _group:DailyEventGroup = null;
		private var _status:BillItemStatus = null;
		private var _assignment:AssetAssignment = null;
		
		public function get group():DailyEventGroup {
			return _group;
		}
		
		public function set group(value:DailyEventGroup):void {
			_group = value;
		}
		
		public function get afe():String {
			return _assignment.AFE;
		}
		
		public function get subAfe():String {
			return _assignment.SubAFE;
		}
		
		public function get assignment():AssetAssignment {
			return _assignment;
		}
		
		private var _totalDailyBill:Number = 0;
		public function get totalDailyBill():Number { return _totalDailyBill;	}

		private var _dailyBillAmount:Number = 0;
		public function get dailyBillAmount():Number { return _dailyBillAmount;	}

		private var _otherBillAmount:Number = 0;
		public function get otherBillAmount():Number { return _otherBillAmount;	}

		private var _totalBillAmount:Number = 0;
		public function get totalBillAmount():Number { return _totalBillAmount;	}
		
		private var _dailyBillItem:BillItem = null;
		
		[Bindable]
		public function get dailyBillItem():BillItem { return _dailyBillItem; }
		public function set dailyBillItem(item:BillItem):void {
			if (null == item) {
				return;
			}
			if (1 != item.BillItemTypeId) {
				return;
			}
			if (null != _dailyBillItem) {
				removeBillItem(_dailyBillItem);
				_dailyBillItem = item;
				addBillItem(item);
			}
		}

		public function get status():BillItemStatus {
			return _status;
		}
		
		public function set status(newStatus:BillItemStatus):void {
			_status = newStatus;
			if (!isEditable()) {
				setStyle("backgroundColor", 0xC0C0C0);
				return;
			}
			
			switch (_status.Status) {
				case BillItemStatus.BILL_ITEM_STATUS_SUBMITTED:
					setStyle("backgroundColor", 0xA0A0A0);
					break;
				case BillItemStatus.BILL_ITEM_STATUS_CHANGED:
					setStyle("backgroundColor", 0x90B090);
					break;
				case BillItemStatus.BILL_ITEM_STATUS_NEW:
					setStyle("backgroundColor", 0x50B050);
					break;
				case BillItemStatus.BILL_ITEM_STATUS_REJECTED:
					setStyle("backgroundColor", 0xB05050);
					break;
				default:
					setStyle("backgroundColor", 0xD0D0D0);
					break;
			}
		}
		
		public override function isEditable():Boolean {
			return (BillItemStatus.BILL_ITEM_STATUS_CHANGED == status.Status
					|| BillItemStatus.BILL_ITEM_STATUS_NEW == status.Status
					|| BillItemStatus.BILL_ITEM_STATUS_REJECTED == status.Status)
				&& assignment.isEditable();
		}
		
		private var filter:ListCollectionView;
		
		private var isLoadingBillItems:Boolean = false;
		
		public function DailyEvent(group:DailyEventGroup, billItems:ArrayCollection = null, assignment:AssetAssignment = null) {
			super();
			_billItems.addEventListener(CollectionEvent.COLLECTION_CHANGE, updateBillItems);
			
			_group = group;
			if ((null == billItems) && (null != assignment)) {
				_assignment = assignment;
				description = subAfe;
				summary = afe + " " + _assignment.RelatedSubAfe.ShortName;
				status = StatusesRegistry.getInstance().getBillItemStatusByName(_group.bill.Status);
				date = new Date(_group.date.time + 1);
				ChangeWatcher.watch(_group.bill, ["RelatedBillStatus"], onBillStatusChanged);
			}
			
			for each (var billItem:BillItem in billItems) {
				isLoadingBillItems = true;
				addBillItem(billItem);
			}

			group.addEvent(this);
			if (isLoadingBillItems) {
				group.calculateTotals();
			}
			recalculateSummary();
			
			isLoadingBillItems = false;
		}
		
		public var billItems:ArrayCollection = new ArrayCollection();

		public function updateBillItems(event:CollectionEvent):void {
			billItems.removeAll();
			for each (var item:BillItem in _billItems) {
				if (filterDailyItem(item)) {
					billItems.addItem(item);
				}
			}
		}
		
		private function filterDailyItem(item:Object):Boolean {
			var billItem:BillItem = BillItem(item);
			return (1 != billItem.BillItemTypeId) && !billItem.IsMarkedToRemove;
		}
		
		public function removeBillItem(billItem:BillItem):void {
			if (!_billItems.contains(billItem)) {
				updateStatus();
				return;
			}

			_billItems.removeItemAt(_billItems.getItemIndex(billItem));

			if (0 == _billItems.length) {
				_group.removeEvent(this);
			}
			recalculateSummary();
			updateStatus();
		}
		
		public function addBillItem(billItem:BillItem):void {
			if (0 == _billItems.length) {
				_assignment = billItem.RelatedAssetAssignment;
				description = subAfe;
				summary = afe + " " + _assignment.RelatedSubAfe.ShortName;
				status = billItem.RelatedBillItemStatus;
				date = new Date(_group.date.time + 1);
				ChangeWatcher.watch(_group.bill, ["RelatedBillStatus"], onBillStatusChanged);
			}
			checkBillItem(billItem);
			
			var amount:Number = billItem.Qty * billItem.BillRate;
			if (BillItemType.BILL_ITEM_TYPE_DAILY_BILLING == billItem.BillItemTypeId) {
				if (null != _dailyBillItem) {
					throw new Error("More than one daily bill items");
				}
				_dailyBillItem = billItem;
				_totalDailyBill += billItem.Qty;
				_dailyBillAmount += amount;
			} else {
				_otherBillAmount += amount;
			}
			billItem.toTempFields();
			_billItems.addItem(billItem);
			correctStatus(billItem.RelatedBillItemStatus);
			
			_totalBillAmount += amount;
			recalculateSummary();
			
			billItem.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, onBillItemChanged);
		}
		
		public function changeDate(newGroup:DailyEventGroup, newDate:Date):Boolean {
			group.calendar.enabled = false;
			
			var existingDailyEvent:DailyEvent = null;
			for each (var event:DailyEvent in newGroup.events) {
				if (this.assignment.AssetAssignmentId == event.assignment.AssetAssignmentId) {
					existingDailyEvent = event;
					break;
				}
			}
			
			var itemsOk:Boolean = true;
            if (null != existingDailyEvent) {
            	itemsOk = processMerge(true, existingDailyEvent, newGroup);
            }
            
            if (itemsOk) {
            	date = new Date(newDate.time + 1);
            	itemsOk = processMerge(false, existingDailyEvent, newGroup);
            }
            
            if (!itemsOk) {
            	group.calendar.enabled = true;
            }
            
            return itemsOk;
		}
		
        protected override function removeEvent():void {
        	group.removeEvent(this);
        	recalculateSummary();
        }
        
        protected override function addEvent():void {
        	group.addEvent(this);
        	recalculateSummary();
        }
        
		public function processMerge(check:Boolean, existingDailyEvent:DailyEvent, newGroup:DailyEventGroup):Boolean {
            for each (var item:BillItem in _billItems) {
            	item.isSaved = true;
                if (null != existingDailyEvent) {
	                if (item.RelatedBillItemType.IsSingle) {
	                	for each (var existingItem:BillItem in existingDailyEvent._billItems) {
	                		if (!existingItem.IsDirty) {
	                			existingItem.isSaved = true;
	                		}
	                		if (existingItem.BillItemTypeId == item.BillItemTypeId) {
	                			if (check) {
	                				if (!item.RelatedBillItemType.IsPresetRate) {
	                					return false;
	                				}
	                			} else {
                					existingItem.Qty += item.Qty;
	                				existingItem.toTempFields();
	                				existingItem.isSaved = false;
	                				item.IsMarkedToRemove = true;
	                			}
	                		}
	                	}
                	}
                }
                
                if (!check) {
	                item.BillingDate = DateUtil.format(date);
    	            item.isSaved = false;
                	
            		if ((null != existingDailyEvent)
            				&& !item.IsMarkedToRemove) {
           				existingDailyEvent.addBillItem(item);
            		}
                }
            }
            
            if (check) {
            	return true;
            }
            
            if (null == existingDailyEvent) {
            	saveItems();
				group = newGroup;
				addEvent();
            } else {
            	existingDailyEvent.saveItems();
            	saveItems();
            }
            
            return true;
		}
		
		public function saveItems():void {
			for each (var item:BillItem in _billItems) {
                if (item.IsMarkedToRemove) {
    	           	item.remove(new Responder(
        	    		onItemSaved,
        				onItemFailed
            		));
                } else if (!item.isSaved) {
    	           	item.save(true, new Responder(
        	    		onItemSaved,
        				onItemFailed
            		));
                }
			}
		}
		
        private function onItemSaved(obj:Object):void {
        	BillItem(obj).isSaved = true;

        	for each (var item:BillItem in _billItems) {
        		if (!item.isSaved) {
        			return;
        		}
        	}
			group.calendar.enabled = true;
        }
        
        private function onItemFailed(event:FaultEvent):void {
            Alert.show("Cannot save bill items");
			group.calendar.enabled = true;
        }
        
		protected function checkBillItem(billItem:BillItem):void {
			if (_group.bill.BillId != billItem.BillId) {
				throw new Error("Cannot store bill item from another bill");
			} else if (date.time != (new Date(Date.parse(billItem.BillingDate))).time + 1) {
				throw new Error("Cannot store bill item from another day");
			}
		}
		
		protected function correctStatus(itemStatus:BillItemStatus):void {
			if (status != itemStatus) {
				if ((BillItemStatus.BILL_ITEM_STATUS_REJECTED == itemStatus.Status)
						&& ((BillItemStatus.BILL_ITEM_STATUS_APPROVED == status.Status)
						 || (BillItemStatus.BILL_ITEM_STATUS_CONFIRMED == status.Status))) {
					status = itemStatus;
				}
			}
		}
		
		private function onBillItemChanged(event:PropertyChangeEvent):void {
			if (event.property == "Qty" || event.property == "BillRate") {
				recalculateSummary();
			} else if (event.property == "RelatedBillItemStatus") {
				onBillItemStatusChanged(null);
			}
		}

		private function onBillItemStatusChanged(event:*):void {
			updateStatus();
		}

		private function onBillStatusChanged(event:*):void {
			updateStatus();
		}

		private function recalculateSummary():void {
			_totalDailyBill = 0;
			_dailyBillAmount = 0;
			_otherBillAmount = 0;
			_totalBillAmount = 0;
			
			for each (var billItem:BillItem in _billItems) {
				var amount:Number = billItem.Qty * billItem.BillRate;

				if (billItem.BillItemTypeId == 1) {
					_totalDailyBill += billItem.Qty;
					_dailyBillAmount += amount;
				} else {
					_otherBillAmount += amount;
				}

				_totalBillAmount += amount;
			}
			
			_group.updateTotals();
		}

		private function updateStatus():void {
           	var isApproved:Boolean = true;
           	var hasApproved:Boolean = false;
           	var isRejected:Boolean = false;
           	var isChanged:Boolean = false;
           	var isOnlyApprovedOrCorrected:Boolean = true;
           	var isNew:Boolean = true;
           	
           	if (1 == _billItems.length) {
           		status = _billItems[0].RelatedBillItemStatus;
           	}

            for each (var item:BillItem in _billItems) {
            	if (item.Status != BillItemStatus.BILL_ITEM_STATUS_NEW) {
            		isNew = false;
            	}
            	if ((item.Status != BillItemStatus.BILL_ITEM_STATUS_APPROVED)
            			|| (item.Status != BillItemStatus.BILL_ITEM_STATUS_CONFIRMED)) {
            		isApproved = false;
            	} else {
            		hasApproved = true;
            	}
            	if (item.Status == BillItemStatus.BILL_ITEM_STATUS_REJECTED) {
            		isRejected = true;
            		isOnlyApprovedOrCorrected = false;
            	}
            	if (item.Status == BillItemStatus.BILL_ITEM_STATUS_CHANGED) {
            		isChanged = true;
            		isOnlyApprovedOrCorrected = false;
            	}
            }
            
            if (isNew) {
            	status = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_NEW);
            } else if (isRejected) {
            	status = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_REJECTED);
            } else if (isApproved) {
            	status = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_APPROVED);
            } else if (isChanged && !isRejected) {
            	status = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_CHANGED);
            } else if (isOnlyApprovedOrCorrected) {
            	status = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_CORRECTED);
            }
		}
		
	}
	
}
