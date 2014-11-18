package com.dalworth.leadCentral.events
{
	import com.dalworth.leadCentral.domain.User;
	
	import flash.events.Event;
	
	public class UserEvent extends Event
	{
		public static const USER_SAVED:String = "userSaved";
		
		public var user:User;
		
		public function UserEvent(type:String, user:User, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.user = user;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}

	}
}