package com.titus.catalog.controls
{
	import com.titus.catalog.view.pdf.BitmapPdfPage;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	
	
public class PagesLoader
	extends EventDispatcher
{
	
	[Event(name="complete", type="flash.events.Event")]
	
	private var _leftToLoad:int;
	
	public function PagesLoader(pages:ArrayCollection)
	{
		_leftToLoad = pages.length;
		for each (var page:BitmapPdfPage in pages) {
			if (page.isLoaded
					|| (null == page.imageUrl)) {
				_leftToLoad--;
				continue;
			}
			
			page.addEventListener(Event.COMPLETE, onPageLoadComplete);
			page.load();
		}
		
		if (0 == _leftToLoad) {
			onPageLoadComplete(null);
		}
	}
	
	private function onPageLoadComplete(evt:Event):void {
		_leftToLoad--;
		if (0 == _leftToLoad) {
        	dispatchEvent(new Event(Event.COMPLETE));
		}
	}

}
	
}
