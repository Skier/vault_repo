package com.dalworth.leadCentral.events
{
	import com.dalworth.leadCentral.domain.PhoneCall;
	
	import flash.events.Event;

	public class PhoneCallEvent extends Event
	{
		public static const PICK_UP:String = "pickUp";
		
		public var phoneCall:PhoneCall;
		
		public function PhoneCallEvent(type:String, phoneCall:PhoneCall, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.phoneCall = phoneCall;
		}
		
		override public function clone():Event 
		{
			return new Event(type, bubbles, cancelable);
		}
		
	}
}