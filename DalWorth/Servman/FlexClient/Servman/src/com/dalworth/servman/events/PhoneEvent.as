package com.dalworth.servman.events
{
	import com.dalworth.servman.domain.Phone;
	
	import flash.events.Event;

	public class PhoneEvent extends Event
	{
		public static const PHONE_COMMIT:String = "phoneCommit";
		
		public var phone:Phone;
		
		public function PhoneEvent(type:String, phone:Phone, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.phone = phone;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}
	}
}