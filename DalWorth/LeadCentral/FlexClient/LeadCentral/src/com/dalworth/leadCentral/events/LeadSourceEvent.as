package com.dalworth.leadCentral.events
{
	import com.dalworth.leadCentral.domain.LeadSource;
	
	import flash.events.Event;

	public class LeadSourceEvent extends Event
	{
		public static const LEAD_SOURCE_SAVE:String = "leadSourceSave";
		public static const LEAD_SOURCE_SELECT:String = "leadSourceSelect";
		
		public var leadSource:LeadSource;
		
		public function LeadSourceEvent(type:String, leadSource:LeadSource, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.leadSource = leadSource;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}
	}
}