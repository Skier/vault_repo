package common.events
{
	import flash.events.Event;
	import TractInc.Domain.User;
	
	public class LoginEvent extends Event
	{
		public var user:User;

	    public static const LOGIN_COMPLETE:String = "loginComplete";
		
	    public function LoginEvent(type:String, user:User,
	    	bubbles:Boolean=true, cancelable:Boolean=false)
	    {
	    	this.user = user;
	        super(type, bubbles, cancelable);
	    }
	 
		override public function clone():Event 
		{
	        return new LoginEvent(type, user, bubbles, cancelable);
	    }
	   
	}

}