package com.llsvc.domain.events
{
	import com.llsvc.domain.InvoiceItem;
	
	import flash.events.Event;

	public class InvoiceItemEvent extends Event
	{
		public static const INVOICE_ITEM_IS_LOADED:String = "invoiceItemIsLoaded";
		public static const INVOICE_ITEM_ACTION_ADD:String = "invoiceItemActionAdd";
		public static const INVOICE_ITEM_ACTION_REMOVE:String = "invoiceItemActionRemove";
		
		public var invoiceItem:InvoiceItem;
		
		public function InvoiceItemEvent(type:String, invoiceItem:InvoiceItem, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.invoiceItem = invoiceItem;
		}
		
		override public function clone():Event 
		{
			return new InvoiceItemEvent(type, invoiceItem, bubbles, cancelable);
		}
		
	}
}
