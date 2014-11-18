package com.llsvc.client.lms.events
{
import com.llsvc.domain.LeaseTract;

import flash.events.Event;

public class TractEvent extends Event
{
	public static const CREATE_TRACT:String = "CREATE_TRACT";
	public static const UPDATE_TRACT:String = "UPDATE_TRACT";
	public static const REMOVE_TRACT:String = "REMOVE_TRACT";
	
	public var leaseTract:LeaseTract;
	
	public function TractEvent(type:String, leaseTract:LeaseTract, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		
		this.leaseTract = leaseTract;
	}
	
}
}