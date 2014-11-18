package com.llsvc.domain.events
{
	import com.llsvc.domain.Project;
	
	import flash.events.Event;

	public class ProjectEvent extends Event
	{
		public static const PROJECT_IS_LOADED:String = "projectIsLoaded";
		
		public var project:Project;
		
		public function ProjectEvent(type:String, project:Project, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.project = project;
		}
		
		override public function clone():Event 
		{
			return new ProjectEvent(type, project, bubbles, cancelable);
		}
		
	}
}
