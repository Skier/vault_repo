package com.llsvc.domain.events
{
	import com.llsvc.domain.Address;
	
	import flash.events.Event;

	public class AddressEvent extends Event
	{
		public static const ADDRESS_IS_LOADED:String = "addressIsLoaded";
		
		public var address:Address;
		
		public function AddressEvent(type:String, address:Address, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.address = address;
		}
		
		override public function clone():Event 
		{
			return new AddressEvent(type, address, bubbles, cancelable);
		}
		
	}
}
