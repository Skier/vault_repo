package com.titus.catalog.events
{
import com.titus.catalog.model.CatalogItem;

import flash.events.Event;

public class CatalogItemEvent extends Event
{
	public var catalogItem:CatalogItem;
	public var isInternalChange:Boolean;
	
	public function CatalogItemEvent(type:String, catalogItem:CatalogItem, isInternalChange:Boolean = false, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		this.catalogItem = catalogItem;
		this.isInternalChange = isInternalChange;
	}
	
	public override function clone():Event 
	{
		return new CatalogItemEvent(type, catalogItem, isInternalChange, bubbles, cancelable);
	}
	
}
}