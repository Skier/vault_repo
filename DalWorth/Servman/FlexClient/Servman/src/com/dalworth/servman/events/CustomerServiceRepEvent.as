package com.dalworth.servman.events
{
	import com.dalworth.servman.domain.CustomerServiceRep;
	
	import flash.events.Event;

	public class CustomerServiceRepEvent extends Event
	{
		public static const CUSTOMER_SERVICE_REP_SAVE:String = "customerServiceRepSave";
		
		public var customerServiceRepUser:CustomerServiceRep;
		
		public function CustomerServiceRepEvent(type:String, customerServiceRepUser:CustomerServiceRep, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.customerServiceRepUser = customerServiceRepUser;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}
	}
}