package com.llsvc.client.lms.events
{
import com.llsvc.client.lms.view.project.ProjectWrapper;

import flash.events.Event;

public class ProjectWrapperEvent extends Event
{
	public static const PROJECT_CHANGE:String = "projectPropertyChange";
	
	public var projectWrapper:ProjectWrapper;
	
	public function ProjectWrapperEvent(type:String, projectWrapper:ProjectWrapper, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		
		this.projectWrapper = projectWrapper;
	}
	
}
}