package com.titus.catalog.controls
{
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;
	

public class PagesQueue
{
	
	private var _items:ArrayCollection;
	
	public function PagesQueue(items:ArrayCollection)
	{
		_items = items;
		var sort:Sort = new Sort();
		sort.fields = [new SortField("priority", true)];
		_items.sort = sort;
		_items.refresh();
		
		startLoading();
	}
	
	private function startLoading():void {
		if (0 < _items.length) {
			var item:PagesQueueItem = PagesQueueItem(_items[0]);
			if ((0 == item.pages.length)
					|| (0 == item.priority)) {
				onCompleteLoad(null);
			} else {
				var loader:PagesLoader = new PagesLoader(item.pages);
				loader.addEventListener(Event.COMPLETE, onCompleteLoad);
			}
		}
	}
	
	private function onCompleteLoad(evt:Event):void {
		_items.removeItemAt(0);
		startLoading();
	}

}

}
