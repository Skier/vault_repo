package com.llsvc.client.lms.events
{
import com.llsvc.domain.DocumentReference;

import flash.events.Event;

public class ReferenceEvent extends Event
{
	public static const CREATE_REFERENCE:String = "CREATE_REFERENCE";
	public static const UPDATE_REFERENCE:String = "UPDATE_REFERENCE";
	public static const REMOVE_REFERENCE:String = "REMOVE_REFERENCE";
	public static const OPEN_REFERENCE:String = "OPEN_REFERENCE";
	public static const OPEN_PDF_REFERENCE:String = "OPEN_PDF_REFERENCE";
	
	public var reference:DocumentReference;
	
	public function ReferenceEvent(type:String, reference:DocumentReference, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		
		this.reference = reference;
	}
	
}
}