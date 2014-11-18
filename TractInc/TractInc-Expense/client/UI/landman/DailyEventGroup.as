package UI.landman
{
	import flash.events.EventDispatcher;
	import mx.collections.ArrayCollection;
	import mx.collections.ListCollectionView;
	import mx.events.CollectionEvent;
	import flash.events.Event;
	import mx.events.PropertyChangeEvent;
	import mx.events.PropertyChangeEventKind;
	import common.StatusesRegistry;
	import mx.rpc.Responder;
	import calendar.Calendar;
	import calendar.CalendarDay;
	import mx.binding.utils.ChangeWatcher;
	import App.Entity.BillDataObject;
	import App.Service.LandmanService;
	import mx.controls.Alert;
	
	[Bindable]
	public class DailyEventGroup extends CalendarDay
	{
		
		public var totalDailyBill:Number = 0;
		public var dailyBillAmount:Number = 0;
		public var otherBillAmount:Number = 0;
		public var totalBillAmount:Number = 0;
		
		private var _date:Date = null;
		private var _bill:BillDataObject = null;
		private var _calendar:Calendar = null;
		private var _events:ArrayCollection = new ArrayCollection();
		private var _totalHours:int = 0;
		
		private var _composition:Composition;
		public function get composition():Composition {
			return _composition;
		}
		public function set composition(value:Composition):void {
			_composition = value;
			isCompositionEditable = true;
		}
		
		public override function getDate():Date {
			return _date;
		}
		
		public function get date():Date {
			return _date;
		}
		
		public function set date(value:Date):void {
			_date = value;
		}
		
		public function get bill():BillDataObject {
			return _bill;
		}
		
		public function get calendar():Calendar {
			return _calendar;
		}
		
		public function get events():ArrayCollection {
			return _events;
		}
		
		public function get totalHours():int {
			return _totalHours;
		}
		
		public function set totalHours(hours:int):void {
			_totalHours = hours;
		}
		
		public function DailyEventGroup(calendar:Calendar, date:Date, bill:BillDataObject) {
			super();
			_date = date;
			_bill = bill;
			_calendar = calendar;
			_events = new ArrayCollection();
			_events.addEventListener(CollectionEvent.COLLECTION_CHANGE, eventsChanged);
		}
		
		public function updateTotals():void {
			var oldTotalDailyBill:Number = totalDailyBill;
			var oldDailyBillAmount:Number = dailyBillAmount;
			var oldOtherBillAmount:Number = otherBillAmount;
			var oldTotalBillAmount:Number = totalBillAmount;

			calculateTotals();
			
			bill.TotalDailyBill += totalDailyBill - oldTotalDailyBill;
			bill.DailyBillAmt += dailyBillAmount - oldDailyBillAmount;
			bill.OtherBillAmt += otherBillAmount - oldOtherBillAmount;
			bill.TotalBillAmt += totalBillAmount - oldTotalBillAmount;
			
			eventsChanged(null);
		}
		
		public function calculateTotals():void {
			totalDailyBill = 0;
			dailyBillAmount = 0;
			otherBillAmount = 0;
			totalBillAmount = 0;
			
			for each (var event:DailyEvent in this.events) {
				totalDailyBill += event.totalDailyBill;
				dailyBillAmount += event.dailyBillAmount;
				otherBillAmount += event.otherBillAmount;
				totalBillAmount += event.totalBillAmount;
			}
		}
		
		public function addEvent(event:DailyEvent):void {
			if (_events.contains(event)) {
				return;
			}
			
			event.group = this;
			_events.addItem(event);
			eventsChanged(null);
       		ChangeWatcher.watch(event, ["selected"], onItemSelectionChange);
		}
		
		public function removeEvent(event:DailyEvent):void {
			var index:int = _events.getItemIndex(event);
			if (-1 == index) {
				return;
			}
			_events.removeItemAt(index);
			eventsChanged(null);
		}
		
		public function getEventByAssignmentId(assignmentId:int):DailyEvent {
			for each (var event:DailyEvent in _events) {
				if (event.assignment.AssetAssignmentId == assignmentId) {
					return event;
				}
			}
			return null;
		}
		
		private function eventsChanged(evt:Event):void {
			totalHours = 0;
			for each (var event:DailyEvent in _events) {
				totalHours += event.totalDailyBill;
			}
		}
		
		public function updateStatus(responder:Responder):void {
			LandmanService.getInstance().updateBillStatus(_bill.BillId, responder);
		}
		
		public function get isEditable():Boolean {
			for each (var project:DailyEvent in events) {
				if (project.isEditable) {
					return true;
				}
			}
			
			return false;
		}
		public function set isEditable(value:Boolean):void {
		}
		
		public function get isCompositionEditable():Boolean {
			var project:DailyEvent;
			for each (project in events) {
				if (!project.assignment.isEditable()) {
					return false;
				}
			}
			
			for each (project in events) {
				if (project.isCompositionEditable) {
					return true;
				}
			}
			
			return false;
		}
		public function set isCompositionEditable(value:Boolean):void {
		}
		
		public var selected:Boolean;
		public function setSelected(value:Boolean):void 
		{
       		for each (var project:DailyEvent in events) {
       			project.setSelected(value);
       		}

       		selected = value;
       		
       		updateAmounts();
		}
		
        private function onItemSelectionChange(evt:PropertyChangeEvent):void 
        {
        	if (composition.isActive) {
        		composition.isChanged = true;
        	}
        	
			var hasAnySelection:Boolean = false;
       		
			for each (var project:DailyEvent in events) {
       			if (project.selected) {
	       			hasAnySelection = true;
       			}
       		}
       		
       		selected = hasAnySelection;
       		
       		updateAmounts();
        }
        
		private var _compositeAmount:Number = 0;
		public function get compositeAmount():Number { return _compositeAmount; }
		public function set compositeAmount(value:Number):void 
		{
			_compositeAmount = value;
			compositeAmountString = "";
			
			updateAmounts();
		}
		
		public function set compositeAmountString(value:String):void {
		}
		public function get compositeAmountString():String {
			return (Math.round(compositeAmount * 100) / 100).toFixed(2);
		}
		
		private function updateAmounts():void 
		{
			var countSelected:int = 0;
       		
       		var project:DailyEvent;
       		
       		for each (project in events) {
       			if (project.selected) {
       				countSelected++;
       			}
       		}
       		
			var count:int = 0;
			var sum:Number = 0;
			
       		for each (project in events) {
       			if (project.selected) {
       				count++;
       				
       				if (count == countSelected) {
    					project.compositeAmount = compositeAmount - sum;
       				} else {
	       				project.compositeAmount = compositeAmount / countSelected;
	       				sum += project.compositeAmount;
       				}
       			} else {
       				project.compositeAmount = 0;
       			}
       		}
		}

	}
	
}
