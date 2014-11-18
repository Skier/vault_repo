package com.dalworth.servman.events
{
	import com.dalworth.servman.domain.Job;
	
	import flash.events.Event;

	public class JobEvent extends Event
	{
		public static const JOB_CONNECT:String = "jobConnect";
		
		public var job:Job;
		
		public function JobEvent(type:String, job:Job, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.job = job;
		}
		
		override public function clone():Event
		{
			return new Event(type, bubbles, cancelable);
		}
	}
}