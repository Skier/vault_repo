package com.dalworth.leadCentral.events
{
	import com.dalworth.leadCentral.domain.TrackingPhone;
	
	import flash.events.Event;

	public class TrackingPhoneEvent extends Event
	{
		public static const PHONE_COMMIT:String = "phoneCommit";
		
		public var phone:TrackingPhone;
		
		public function TrackingPhoneEvent(type:String, phone:TrackingPhone, bubbles:Boolean=false, cancelable:Boolean=false)
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