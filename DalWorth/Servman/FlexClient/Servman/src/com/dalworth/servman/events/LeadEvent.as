package com.dalworth.servman.events
{
	import com.dalworth.servman.domain.Lead;
	
	import flash.events.Event;

	public class LeadEvent extends Event
	{
		public static const LEAD_SAVE:String = "leadSave";
		
		public var lead:Lead;
		
		public function LeadEvent(type:String, lead:Lead, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.lead = lead;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}
	}
}