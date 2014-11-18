package com.dalworth.servman.events
{
	import com.dalworth.servman.domain.PhoneCallWorkflow;
	
	import flash.events.Event;

	public class PhoneCallWorkflowEvent extends Event
	{
		public static const PHONE_CALL_WORKFLOW_SAVE:String = "phoneCallWorkflowSave";
		
		public var phoneCallWorkflow:PhoneCallWorkflow;
		
		public function PhoneCallWorkflowEvent(type:String, phoneCallWorkflow:PhoneCallWorkflow, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.phoneCallWorkflow = phoneCallWorkflow;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}
	}
}