package com.llsvc.domain.events
{

import com.llsvc.domain.User;
import flash.events.Event;

public class UserEvent extends Event
{
	public static const USER_CHANGED:String = "userPropertyChanged";
	public static const USER_SAVED:String = "userSaved";
	
	public var user:User;
	
	public function UserEvent(type:String, user:User, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		
		this.user = user;
	}
	
}
}