package com.dalworth.leadCentral.events
{
	import com.dalworth.leadCentral.domain.LeadSourcePhone;
	
	import flash.events.Event;

	public class LeadSourcePhoneEvent extends Event
	{
		public static const PHONE_COMMIT:String = "phoneCommit";
		
		public var phone:LeadSourcePhone;
		
		public function LeadSourcePhoneEvent(type:String, phone:LeadSourcePhone, bubbles:Boolean=false, cancelable:Boolean=false)
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