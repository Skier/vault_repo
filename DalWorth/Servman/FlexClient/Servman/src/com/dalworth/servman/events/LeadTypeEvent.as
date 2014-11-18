package com.dalworth.servman.events
{
	import com.dalworth.servman.domain.LeadType;
	
	import flash.events.Event;
	
	public class LeadTypeEvent extends Event
	{
		public static const LEAD_TYPE_SAVED:String = "leadTypeSaved";
		
		public var leadType:LeadType;
		
		public function LeadTypeEvent(type:String, leadType:LeadType, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.leadType = leadType;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}

	}
}