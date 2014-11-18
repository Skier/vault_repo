package com.llsvc.domain.events
{
	import com.llsvc.domain.Invoice;
	
	import flash.events.Event;

	public class InvoiceEvent extends Event
	{
		public static const INVOICE_IS_LOADED:String = "invoiceIsLoaded";
		
		public var invoice:Invoice;
		
		public function InvoiceEvent(type:String, invoice:Invoice, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.invoice = invoice;
		}
		
		override public function clone():Event 
		{
			return new InvoiceEvent(type, invoice, bubbles, cancelable);
		}
		
	}
}
