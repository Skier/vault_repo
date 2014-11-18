package com.llsvc.domain.events
{
	import com.llsvc.domain.Client;
	
	import flash.events.Event;

	public class ClientEvent extends Event
	{
		public static const CLIENT_IS_LOADED:String = "clientIsLoaded";
		
		public var client:Client;
		
		public function ClientEvent(type:String, client:Client, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.client = client;
		}
		
		override public function clone():Event 
		{
			return new ClientEvent(type, client, bubbles, cancelable);
		}
		
	}
}
