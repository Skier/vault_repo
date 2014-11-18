package com.llsvc.domain.events
{
	import com.llsvc.domain.Person;
	
	import flash.events.Event;

	public class PersonEvent extends Event
	{
		public static const PERSON_IS_LOADED:String = "personIsLoaded";
		
		public var person:Person;
		
		public function PersonEvent(type:String, person:Person, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.person = person;
		}
		
		override public function clone():Event 
		{
			return new PersonEvent(type, person, bubbles, cancelable);
		}
		
	}
}
