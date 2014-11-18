package com.llsvc.domain.events
{
	import com.llsvc.domain.State;
	
	import flash.events.Event;

	public class StateEvent extends Event
	{
		public static const STATE_IS_LOADED:String = "stateIsLoaded";
		
		public var state:State;
		
		public function StateEvent(type:String, state:State, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.state = state;
		}
		
		override public function clone():Event 
		{
			return new StateEvent(type, state, bubbles, cancelable);
		}
		
	}
}
