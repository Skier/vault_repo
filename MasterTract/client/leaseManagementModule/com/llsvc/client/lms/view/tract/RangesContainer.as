package com.llsvc.client.lms.view.tract
{
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;
	import mx.containers.VBox;
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;

	[Event(name="editRangeRequest", type="flash.events.Event")]
	[Event(name="deleteRangeRequest", type="flash.events.Event")]

	public class RangesContainer extends VBox
	{
		public function RangesContainer()
		{
			super();
		}
		
		private var _ranges:ArrayCollection;
		[Bindable]
		public function get ranges():ArrayCollection { return _ranges; }
		public function set ranges(value:ArrayCollection):void 
		{
			_ranges = value;
			_ranges.addEventListener(CollectionEvent.COLLECTION_CHANGE, rangesChangeHandler);
			refreshRanges();
		}
		
		private function rangesChangeHandler(event:CollectionEvent):void 
		{
			var i:uint;
			
		    switch (event.kind) {
		        case CollectionEventKind.ADD:
		            for (i = 0; i < event.items.length; i++) {
		            	addRangeBox(TractRange(event.items[i]));
		            }
		            break;
		
		        case CollectionEventKind.REMOVE:
		            for (i = 0; i < event.items.length; i++) {
		                removeRangeBox(TractRange(event.items[i]));
		            }
		            break;
		
		        case CollectionEventKind.RESET:
		            refreshRanges();
		            break;
		    }
		}
		
		private function addRangeBox(range:TractRange):void 
		{
			var rangeBox:TractsRangeRenderer = new TractsRangeRenderer();
			rangeBox.percentWidth = 100;
			rangeBox.range = range;
			addChild(rangeBox);
		}
		
		private function removeRangeBox(range:TractRange):void 
		{
			var rangeBox:TractsRangeRenderer;
			for (var i:int = 0; i < getChildren().length; i++) 
			{
				rangeBox = this.getChildAt(i) as TractsRangeRenderer;
				if (rangeBox.range == range) 
				{
					removeChildAt(i);
					return;
				}
			}
		}
		
		private function refreshRanges():void 
		{
			removeAllChildren();
			
			for each (var range:TractRange in ranges) 
			{
				addRangeBox(range);
			}
		}
	}
}