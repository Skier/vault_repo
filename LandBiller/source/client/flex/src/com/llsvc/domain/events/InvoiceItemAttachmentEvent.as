package com.llsvc.domain.events
{
	import com.llsvc.domain.InvoiceItemAttachment;
	
	import flash.events.Event;

	public class InvoiceItemAttachmentEvent extends Event
	{
		public static const INVOICE_ITEM_ATTACHMENT_IS_LOADED:String = "invoiceItemAttachmentIsLoaded";
		public static const REMOVE_ATTACHMENT:String = "invoiceItemAttachmentRemove";
		public static const OPEN_PDF_ATTACHMENT:String = "invoiceItemAttachmentOpenPdf";
		public static const OPEN_ATTACHMENT:String = "invoiceItemAttachmentOpen";
		
		public var invoiceItemAttachment:InvoiceItemAttachment;
		
		public function InvoiceItemAttachmentEvent(type:String, invoiceItemAttachment:InvoiceItemAttachment, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.invoiceItemAttachment = invoiceItemAttachment;
		}
		
		override public function clone():Event 
		{
			return new InvoiceItemAttachmentEvent(type, invoiceItemAttachment, bubbles, cancelable);
		}
		
	}
}
