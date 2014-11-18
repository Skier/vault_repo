package com.dalworth.servman.events
{
	import com.dalworth.servman.domain.Owner;
	
	import flash.events.Event;

	public class OwnerEvent extends Event
	{
		public static const OWNER_SAVE:String = "ownerSave";
		
		public var ownerUser:Owner;
		
		public function OwnerEvent(type:String, ownerUser:Owner, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.ownerUser = ownerUser;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}
	}
}