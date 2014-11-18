package com.dalworth.leadCentral.events
{
	import com.dalworth.leadCentral.domain.QbInvoice;
	
	import flash.events.Event;

	public class QbInvoiceEvent extends Event
	{
		public static const QBINVOICE_CONNECT:String = "qbInvoiceConnect";
		public static const QBINVOICE_DISCONNECT:String = "qbInvoiceDisconnect";
		
		public var qbInvoice:QbInvoice;
		
		public function QbInvoiceEvent(type:String, qbInvoice:QbInvoice, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.qbInvoice = qbInvoice;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}
	}
}