package com.titus.catalog.events
{
import com.titus.catalog.model.CatalogItem;

import flash.events.Event;

public class CatalogItemEvent extends Event
{
	public var catalogItem:CatalogItem;
	
	public function CatalogItemEvent(type:String, catalogItem:CatalogItem, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		this.catalogItem = catalogItem;
	}
	
	public override function clone():Event 
	{
		return new CatalogItemEvent(type, catalogItem, bubbles, cancelable);
	}
	
}
}