package com.llsvc.domain.events
{
	import com.llsvc.domain.ExpenceType;
	
	import flash.events.Event;

	public class ExpenceTypeEvent extends Event
	{
		public static const EXPENCE_TYPE_IS_LOADED:String = "expenceTypeIsLoaded";
		
		public var expenceType:ExpenceType;
		
		public function ExpenceTypeEvent(type:String, expenceType:ExpenceType, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.expenceType = expenceType;
		}
		
		override public function clone():Event 
		{
			return new ExpenceTypeEvent(type, expenceType, bubbles, cancelable);
		}
		
	}
}
