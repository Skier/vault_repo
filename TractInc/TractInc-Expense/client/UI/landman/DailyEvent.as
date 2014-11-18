package UI.landman
{
	
	import mx.collections.ArrayCollection;
	import mx.binding.utils.ChangeWatcher;
	import common.StatusesRegistry;
	import common.TypesRegistry;
	import mx.collections.ListCollectionView;
	import mx.events.FlexEvent;
	import mx.events.PropertyChangeEvent;
	import util.ArrayUtil;
	import util.DateUtil;
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;
	import mx.rpc.events.FaultEvent;
    import mx.rpc.Responder;
    import mx.controls.Alert;
    import calendar.Calendar;
    import calendar.CalendarDay;
    import util.NumberUtil;
    import flash.events.EventDispatcher;
    import App.Entity.BillItemDataObject;
    import App.Service.LandmanService;
    import mx.rpc.events.ResultEvent;
    import App.Entity.BillDataObject;
    import App.Entity.AssetAssignmentDataObject;
    import App.Entity.BillItemStatusDataObject;
    import App.Entity.BillItemTypeDataObject;

	[Bindable]
	public class DailyEvent extends EventDispatcher
	{
		
		public var _billItems:ArrayCollection = new ArrayCollection();
		private var _group:DailyEventGroup = null;
		private var _status:String = null;
		private var _assignment:AssetAssignmentDataObject = null;
		public var date:Date;
		public var description:String;
		public var summary:String;
		
		private var _compositeAmount:Number = 0;
		public function get compositeAmount():Number {
			return _compositeAmount;
		}
		public function set compositeAmount(value:Number):void {
			_compositeAmount = value;
			compositeAmountString = "";
		}
		
		public function get compositeAmountString():String 
		{
			return (Math.round(compositeAmount * 100) / 100).toFixed(2);
		}
		public function set compositeAmountString(value:String):void {
		}
		
		private var _selected:Boolean;
		public function get selected():Boolean { return _selected;}
		public function set selected(value:Boolean):void {
			_selected = value;
		}
		
		public function setSelected(value:Boolean):void {
			if (!isEditable) {
				return;
			}
			
			_selected = value;
		}
		
		public function get group():DailyEventGroup {
			return _group;
		}
		
		public function set group(value:DailyEventGroup):void {
			_group = value;
			isCompositionEditable = group.isCompositionEditable;
		}
		
		public function get afe():String {
			return _assignment.AFE;
		}
		
		public function get subAfe():String {
			return _assignment.SubAFE;
		}
		
		public function get subAfeShort():String {
			return _assignment.project.ShortName;
		}
		
		public function get daysString():String {
			return NumberUtil.fraction(totalDailyBill, 8);
		}
		
		public function set daysString(value:String):void {
		}
		
		public function get amountString():String {
			return (Math.round(totalBillAmount * 100) / 100).toFixed(2);
		}
		public function set amountString(value:String):void {
		}
		
		public function get assignment():AssetAssignmentDataObject {
			return _assignment;
		}
		public function set assignment(value:AssetAssignmentDataObject):void {
			_assignment = value;
       		for each(var item:BillItemDataObject in _billItems) {
       			item.isSaved = false;
       			item.AssetAssignmentId = value.AssetAssignmentId;
       			item.Afe = value.AFE;
       			item.SubAfe = value.SubAFE;
       		}
		}
		
		private var _totalDailyBill:Number = 0;
		public function get totalDailyBill():Number { return _totalDailyBill;	}

		private var _dailyBillAmount:Number = 0;
		public function get dailyBillAmount():Number { return _dailyBillAmount;	}

		private var _otherBillAmount:Number = 0;
		public function get otherBillAmount():Number { return _otherBillAmount;	}

		private var _totalBillAmount:Number = 0;
		public function get totalBillAmount():Number { return _totalBillAmount;	}
		
		private var _dailyBillItem:BillItemDataObject = null;
		
		public function get dailyBillItem():BillItemDataObject { return _dailyBillItem; }
		public function set dailyBillItem(item:BillItemDataObject):void {
			if ((null == item)
					|| _billItems.contains(_dailyBillItem)) {
				return;
			}
			if (1 != item.BillItemTypeId) {
				return;
			}
			if (null != _dailyBillItem) {
				removeBillItem(_dailyBillItem);
			}

			addBillItem(item);
		}

		public function get status():String {
			return _status;
		}
		
		public function set status(newStatus:String):void {
			_status = newStatus;
		}
			
		public function get isEditable():Boolean {
			return (BillItemStatusDataObject.BILL_ITEM_STATUS_CHANGED == status
					|| BillItemStatusDataObject.BILL_ITEM_STATUS_NEW == status
					|| BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED == status)
				&& assignment.isEditable();
		}
		public function set isEditable(value:Boolean):void {
		}
		
		public function isRemovable():Boolean {
			if (!isEditable) {
				return false;
			}
			
			var hasApproved:Boolean = false;
			for each (var item:BillItemDataObject in _billItems) {
				if (!item.isBillItemEditable()) {
					return false;
				}
			}
			
			return true;
		}
		
		public function canChangeProject():Boolean {
			if (!isEditable) {
				return false;
			}
			
			var hasApproved:Boolean = false;
			for each (var item:BillItemDataObject in _billItems) {
				if (!item.isBillItemEditableOld()) {
					return false;
				}
			}
			
			return true;
		}
		
		private var filter:ListCollectionView;
		
		private var isLoadingBillItems:Boolean = false;
		
		public function DailyEvent(group:DailyEventGroup, billItems:ArrayCollection = null, assignment:AssetAssignmentDataObject = null) {
			super();
			_billItems.addEventListener(CollectionEvent.COLLECTION_CHANGE, updateBillItems);
			
			_group = group;
			_assignment = assignment;
			if ((null == billItems) && (null != assignment)) {
				description = subAfe;
				summary = afe + " " + _assignment.project.ShortName;
				status = _group.bill.Status;
				date = new Date(_group.date.time + 1);
				ChangeWatcher.watch(_group.bill, ["RelatedBillStatus"], onBillStatusChanged);
			}
			
			for each (var billItem:BillItemDataObject in billItems) {
				isLoadingBillItems = true;
				addBillItem(billItem);
			}

			group.addEvent(this);
			if (isLoadingBillItems) {
				group.calculateTotals();
			}
			recalculateSummary();
			
			isCompositionEditable = group.isCompositionEditable;
			
			isLoadingBillItems = false;
		}
		
		public var billItems:ArrayCollection = new ArrayCollection();

		public function updateBillItems(event:CollectionEvent):void {
			billItems.removeAll();
			
			if (0 == _billItems.length) {
				_group.removeEvent(this);
			} else {
				for each (var item:BillItemDataObject in _billItems) {
					if (item.IsMarkedToRemove) {
						continue;
					}
					
					if (1 == item.BillItemTypeId) {
						_dailyBillItem = item;
					} else {
						billItems.addItem(item);
					} 
				}
			}
			daysString = "";
			recalculateSummary();
		}
		
		public function removeBillItem(billItem:BillItemDataObject):void {
			if (!_billItems.contains(billItem)) {
				updateStatus();
				return;
			}
			
			if (null != group.bill.BillItems) {
				if (-1 < group.bill.BillItems.indexOf(billItem)) {
					group.bill.BillItems.splice(group.bill.BillItems.indexOf(billItem), 1);
				}
			}

			_billItems.removeItemAt(_billItems.getItemIndex(billItem));

			if (0 == _billItems.length) {
				_group.removeEvent(this);
			}
			recalculateSummary();
		}
		
		public function addBillItem(billItem:BillItemDataObject):void {
			if (_billItems.contains(billItem)) {
				recalculateSummary();
				return;
			}
			
			if ((0 == _billItems.length)
					&& !_isSaving) {
				description = subAfe;
				summary = afe + " " + _assignment.project.ShortName;
				status = billItem.Status;
				date = new Date(_group.date.time + 1);
				ChangeWatcher.watch(_group.bill, ["RelatedBillStatus"], onBillStatusChanged);
			}
			
			checkBillItem(billItem);
			
			var amount:Number = billItem.Qty * billItem.BillRate;
			if (BillItemTypeDataObject.BILL_ITEM_TYPE_DAILY_BILLING == billItem.BillItemTypeId) {
				if (null != _dailyBillItem) {
					throw new Error("More than one daily bill items");
				}

				_dailyBillItem = billItem;
				_billItems.addItem(billItem);
				_totalDailyBill += billItem.Qty;
				_dailyBillAmount += amount;
			} else {
				_otherBillAmount += amount;
				_billItems.addItem(billItem);
			}
			billItem.toTempFields();
			correctStatus(billItem.Status);
			
			_totalBillAmount += amount;
			recalculateSummary();
			
			billItem.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, onBillItemChanged);
			
			isCompositionEditable = group.isCompositionEditable;
		}

        protected function removeEvent():void {
        	group.removeEvent(this);
        	recalculateSummary();
        }
        
        protected function addEvent():void {
        	group.addEvent(this);
        	recalculateSummary();
        }

		private var _responder:Responder = null;
		
		private var _isSaving:Boolean = false;
		
		public function saveItems(responder:Responder = null):void {
			_isSaving = true;
			group.calendar.enabled = false;

			var thisEvent:DailyEvent = this;
			LandmanService.getInstance().storeBillItems(this._group.bill, _billItems, new Responder(
				function(result:ResultEvent):void {
					var updatedBill:BillDataObject = BillDataObject(result.result);
					
					group.bill.assign(updatedBill);
					
					for each (var item:BillItemDataObject in _billItems) {
						if (item.IsMarkedToRemove) {
							_billItems.removeItemAt(_billItems.getItemIndex(item));
						}
					}
					
       				_isSaving = false;
       				
		        	if (null != responder) {
       					responder.result(result);
       				}
       				
					group.calendar.enabled = true;
				},
				function(event:FaultEvent):void {
		        	if (null != responder) {
        				responder.fault(event);
		        		responder = null;
       				}
       				
       				_isSaving = false;
       				
					group.calendar.enabled = true;
				}
			));
		}
		
		protected function checkBillItem(billItem:BillItemDataObject):void {
			if (_group.bill.BillId != billItem.BillId) {
				throw new Error("Cannot store bill item from another bill");
			} else if (date.time != (new Date(Date.parse(billItem.BillingDate))).time + 1) {
				throw new Error("Cannot store bill item from another day");
			}
		}
		
		protected function correctStatus(itemStatus:String):void {
			if (status != itemStatus) {
				if ((BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED == itemStatus)
						&& ((BillItemStatusDataObject.BILL_ITEM_STATUS_APPROVED == status)
							|| (BillItemStatusDataObject.BILL_ITEM_STATUS_CONFIRMED == status))) {
					status = itemStatus;
				}
			}
		}
		
		private function onBillItemChanged(event:PropertyChangeEvent):void {
			if (event.property == "Qty" || event.property == "BillRate") {
				recalculateSummary();
			} else if (event.property == "Status") {
				onBillItemStatusChanged(event);
			}
		}

		private function onBillItemStatusChanged(event:PropertyChangeEvent):void {
			if (null != event) {
				if ((String(event.newValue) == BillItemStatusDataObject.BILL_ITEM_STATUS_APPROVED)
						&& (String(event.oldValue) == BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED)) {
					event.newValue = BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED;
					return;
				}
			}
			
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
			
			for each (var billItem:BillItemDataObject in _billItems) {
				var amount:Number = billItem.Qty * billItem.BillRate;

				if (billItem.BillItemTypeId == 1) {
					_totalDailyBill += billItem.Qty;
					_dailyBillAmount += amount;
				} else {
					_otherBillAmount += amount;
				}

				_totalBillAmount += amount;
			}
			
			daysString = "";
			amountString = "";
			
			updateStatus();
			_group.updateTotals();
		}

		private function updateStatus():void {
           	if (1 == _billItems.length) {
           		status = _billItems[0].Status;
           	} else if (0 < _billItems.length) {
           		var currentStatus:String = BillItemDataObject(_billItems[0]).Status;
           		
	            for each (var item:BillItemDataObject in _billItems) {
	            	if ((0 == item.BillItemTypeIdTemp)
	            			|| ((null == item.StatusTemp) && (null == item.Status))) {
	            		continue;
	            	}
	            	
	            	if (item.StatusTemp == BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED) {
	            		currentStatus = BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED;
	            		break;
	            	}
	            	
	            	if ((item.StatusTemp == BillItemStatusDataObject.BILL_ITEM_STATUS_CHANGED)
	            			|| ((item.StatusTemp == BillItemStatusDataObject.BILL_ITEM_STATUS_NEW)
	            				&& (0 != item.BillItemCompositionId)
	            				&& (BillItemStatusDataObject.BILL_ITEM_STATUS_NEW != group.bill.Status))) {
	            		currentStatus = BillItemStatusDataObject.BILL_ITEM_STATUS_CHANGED;
	            	}
	            }
	            
	            status = currentStatus;
           	}
           	
           	isEditable = true;
		}
		
		private var _isCompositionEditable:Boolean = true;
		public function get isCompositionEditable():Boolean {
			return _isCompositionEditable && assignment.isEditable();
		}
		public function set isCompositionEditable(value:Boolean):void {
			_isCompositionEditable = value;
		}
		
	}
	
}
