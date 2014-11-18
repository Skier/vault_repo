package com.titus.catalog.events
{
import com.titus.catalog.model.CatalogItem;

import flash.events.Event;

public class PageEvent extends Event
{
	public var page:int;
	
	public function PageEvent(type:String, pageNumber:int, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		this.page = pageNumber;
	}
	
	public override function clone():Event 
	{
		return new PageEvent(type, page, bubbles, cancelable);
	}
	
}
}