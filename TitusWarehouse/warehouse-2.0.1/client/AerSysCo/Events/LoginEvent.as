package AerSysCo.Events
{
	import flash.events.Event;
	import AerSysCo.UI.Models.ASCUserUI;
	
	public class LoginEvent extends Event
	{
	    public static const LOGIN_REQUEST:String = "loginRequest";
	
		public var user:ASCUserUI;
	
	    public function LoginEvent(type:String, user:ASCUserUI, 
	        bubbles:Boolean=false, cancelable:Boolean=false)
	    {
	        super(type, bubbles, cancelable);
	
	        this.user = user;
	    }
	    
	    override public function clone():Event
	    {
	        return new LoginEvent(type, user, bubbles, cancelable);
	    }
	    
	}
}