package UI.landman
{
	import flash.events.EventDispatcher;
	import App.Domain.Bill;
	import mx.collections.ArrayCollection;
	import mx.collections.ListCollectionView;
	import mx.events.CollectionEvent;
	import flash.events.Event;
	import mx.events.PropertyChangeEvent;
	import mx.events.PropertyChangeEventKind;
	import App.Domain.BillItemStatus;
	import App.Domain.BillItem;
	import App.Domain.BillStatus;
	import common.StatusesRegistry;
	import weborb.data.DynamicLoadEvent;
	import weborb.data.ActiveCollection;
	import mx.rpc.Responder;
	import calendar.MonthPeriodGrid;
	
	public class DailyEventGroup extends EventDispatcher
	{
		
		private var _totalDailyBill:Number = 0;
		public function get totalDailyBill():Number { return _totalDailyBill;	}

		private var _dailyBillAmount:Number = 0;
		public function get dailyBillAmount():Number { return _dailyBillAmount;	}

		private var _otherBillAmount:Number = 0;
		public function get otherBillAmount():Number { return _otherBillAmount;	}

		private var _totalBillAmount:Number = 0;
		public function get totalBillAmount():Number { return _totalBillAmount;	}
		
		private var _date:Date = null;
		private var _bill:Bill = null;
		private var _calendar:MonthPeriodGrid = null;
		private var _events:ArrayCollection = new ArrayCollection();
		private var _totalHours:int = 0;
		
		public function get date():Date {
			return _date;
		}
		
		public function get bill():Bill {
			return _bill;
		}
		
		public function get calendar():MonthPeriodGrid {
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
		
		public function DailyEventGroup(calendar:MonthPeriodGrid, date:Date, bill:Bill) {
			super();
			_date = date;
			_bill = bill;
			_calendar = calendar;
			_events = new ArrayCollection();
			_events.addEventListener(CollectionEvent.COLLECTION_CHANGE, eventsChanged);
			_calendar.getCell(_date).active = bill.isBillEditable();
		}
		
		public function updateTotals():void {
			var oldTotalDailyBill:Number = _totalDailyBill;
			var oldDailyBillAmount:Number = _dailyBillAmount;
			var oldOtherBillAmount:Number = _otherBillAmount;
			var oldTotalBillAmount:Number = _totalBillAmount;

			calculateTotals();
			
			bill.TotalDailyBill += _totalDailyBill - oldTotalDailyBill;
			bill.DailyBillAmt += _dailyBillAmount - oldDailyBillAmount;
			bill.OtherBillAmt += _otherBillAmount - oldOtherBillAmount;
			bill.TotalBillAmt += _totalBillAmount - oldTotalBillAmount;
			
			eventsChanged(null);
		}
		
		public function calculateTotals():void {
			_totalDailyBill = 0;
			_dailyBillAmount = 0;
			_otherBillAmount = 0;
			_totalBillAmount = 0;
			
			for each (var event:DailyEvent in this.events) {
				_totalDailyBill += event.totalDailyBill;
				_dailyBillAmount += event.dailyBillAmount;
				_otherBillAmount += event.otherBillAmount;
				_totalBillAmount += event.totalBillAmount;
			}
		}
		
		public function addEvent(event:DailyEvent):void {
			event.group = this;
			_events.addItem(event);
			_calendar.addEvent(event);
			eventsChanged(null);
		}
		
		public function removeEvent(event:DailyEvent):void {
			var index:int = _events.getItemIndex(event);
			if (-1 == index) {
				return;
			}
			_events.removeItemAt(index);
			_calendar.removeEvent(event);
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
			
			_calendar.getCell(_date).hours = totalHours;
		}
		
		public function updateStatus(responder:Responder):void {
			var updateProcessor:StatusUpdateProcessor = new StatusUpdateProcessor(bill, responder);
		}
		
	}
	
}
