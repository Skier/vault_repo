package com.llsvc.domain.events
{
	import com.llsvc.domain.DefaultExpenceType;
	
	import flash.events.Event;

	public class DefaultExpenceTypeEvent extends Event
	{
		public static const DEFAULT_EXPENCE_TYPE_IS_LOADED:String = "defaultExpenceTypeIsLoaded";
		
		public var defaultExpenceType:DefaultExpenceType;
		
		public function DefaultExpenceTypeEvent(type:String, defaultExpenceType:DefaultExpenceType, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.defaultExpenceType = defaultExpenceType;
		}
		
		override public function clone():Event 
		{
			return new DefaultExpenceTypeEvent(type, defaultExpenceType, bubbles, cancelable);
		}
		
	}
}
