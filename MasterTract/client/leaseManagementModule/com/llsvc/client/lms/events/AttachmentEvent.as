package com.llsvc.client.lms.events
{
import com.llsvc.domain.DocumentAttachment;

import flash.events.Event;

public class AttachmentEvent extends Event
{
	public static const REMOVE_ATTACHMENT:String = "removeAttachment";
	public static const OPEN_ATTACHMENT:String = "openAttachment";
	public static const OPEN_PDF_ATTACHMENT:String = "openPdfAttachment";
	
	public var attachment:DocumentAttachment;
	
	public function AttachmentEvent(type:String, attachment:DocumentAttachment, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		
		this.attachment = attachment;
	}
	
}
}