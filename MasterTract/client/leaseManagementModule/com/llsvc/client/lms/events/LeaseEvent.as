package com.llsvc.client.lms.events
{
import com.llsvc.domain.Lease;

import flash.events.Event;

public class LeaseEvent extends Event
{
	public static const CREATE_LEASE:String = "CREATE_LEASE";
	public static const UPDATE_LEASE:String = "UPDATE_LEASE";
	public static const REMOVE_LEASE:String = "REMOVE_LEASE";
	
	public var lease:Lease;
	
	public function LeaseEvent(type:String, lease:Lease, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		
		this.lease = lease;
	}
	
}
}