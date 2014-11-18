package com.llsvc.domain.events
{
	import com.llsvc.domain.Company;
	
	import flash.events.Event;

	public class CompanyEvent extends Event
	{
		public static const COMPANY_IS_LOADED:String = "clientIsLoaded";
		
		public var company:Company;
		
		public function CompanyEvent(type:String, company:Company, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.company = company;
		}
		
		override public function clone():Event 
		{
			return new CompanyEvent(type, company, bubbles, cancelable);
		}
		
	}
}
