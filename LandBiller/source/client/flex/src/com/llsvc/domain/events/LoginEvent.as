package com.llsvc.domain.events
{
	import com.llsvc.domain.Login;
	
	import flash.events.Event;

	public class LoginEvent extends Event
	{
		public static const LOGIN_IS_LOADED:String = "loginIsLoaded";
		
		public var login:Login;
		
		public function LoginEvent(type:String, login:Login, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.login = login;
		}
		
		override public function clone():Event 
		{
			return new LoginEvent(type, login, bubbles, cancelable);
		}
		
	}
}
