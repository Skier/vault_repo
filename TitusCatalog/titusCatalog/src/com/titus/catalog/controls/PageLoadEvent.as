package com.titus.catalog.controls
{
	import com.titus.catalog.view.pdf.BitmapPdfPage;
	
	import flash.events.Event;
	

public class PageLoadEvent extends Event
{
	
	public var bitmap:BitmapPdfPage;
	
	public function PageLoadEvent(type:String, bitmap:BitmapPdfPage, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		this.bitmap = bitmap;
	}
	
	public override function clone():Event 
	{
		return new PageLoadEvent(type, bitmap, bubbles, cancelable);
	}
	
}
}