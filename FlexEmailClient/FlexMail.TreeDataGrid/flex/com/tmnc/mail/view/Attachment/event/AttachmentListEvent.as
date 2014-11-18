package com.tmnc.mail.view.Attachment.event
{
import flash.events.Event;

public class AttachmentListEvent extends Event
{
    public static const ADD_ATTACHMENT:String    = "addAttachment";
    public static const REMOVE_ATTACHMENT:String = "removeAttachment";
	
    public function AttachmentListEvent(type:String, bubbles:Boolean=true, cancelable:Boolean=false){
        super(type, bubbles, cancelable);
    }
 
     override public function clone():Event {
        return new AttachmentListEvent(type, bubbles, cancelable);
    }
   
}

}