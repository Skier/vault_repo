package com.llsvc.domain.events
{
	import com.llsvc.domain.User;
	
	import flash.events.Event;

	public class UserEvent extends Event
	{
		public static const USER_IS_LOADED:String = "userIsLoaded";
		public static const LOGIN_SUCCESSFULL:String = "loginSuccessfull";
		public static const LOGIN_FAIL:String = "loginFail";
		public static const REGISTRATION_SUCCESSFULL:String = "registrationSuccessfull";
		public static const REGISTRATION_COMPLETE:String = "registrationComplete";
		public static const REGISTRATION_FAIL:String = "registrationFail";
		
		public var user:User;
		
		public function UserEvent(type:String, user:User, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.user = user;
		}
		
		override public function clone():Event 
		{
			return new UserEvent(type, user, bubbles, cancelable);
		}
		
	}
}
