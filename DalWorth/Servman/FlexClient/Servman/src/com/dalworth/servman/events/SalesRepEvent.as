package com.dalworth.servman.events
{
	import com.dalworth.servman.domain.SalesRep;
	
	import flash.events.Event;

	public class SalesRepEvent extends Event
	{
		public static const SALES_REP_SAVE:String = "salesRepSave";
		public static const SALES_REP_SELECT:String = "salesRepSelect";
		
		public var salesRepUser:SalesRep;
		
		public function SalesRepEvent(type:String, salesRepUser:SalesRep, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.salesRepUser = salesRepUser;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}
	}
}