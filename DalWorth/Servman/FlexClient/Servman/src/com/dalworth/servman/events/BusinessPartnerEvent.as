package com.dalworth.servman.events
{
	import com.dalworth.servman.domain.BusinessPartner;
	
	import flash.events.Event;

	public class BusinessPartnerEvent extends Event
	{
		public static const BUSINESS_PARTNER_SAVE:String = "businessPartnerSave";
		public static const BUSINESS_PARTNER_SELECT:String = "businessPartnerSelect";
		
		public var businessPartner:BusinessPartner;
		
		public function BusinessPartnerEvent(type:String, businessPartner:BusinessPartner, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.businessPartner = businessPartner;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}
	}
}