package com.llsvc.domain.events
{
	import com.llsvc.domain.County;
	
	import flash.events.Event;

	public class CountyEvent extends Event
	{
		public static const COUNTY_IS_LOADED:String = "countyIsLoaded";
		
		public var county:County;
		
		public function CountyEvent(type:String, county:County, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.county = county;
		}
		
		override public function clone():Event 
		{
			return new CountyEvent(type, county, bubbles, cancelable);
		}
		
	}
}
