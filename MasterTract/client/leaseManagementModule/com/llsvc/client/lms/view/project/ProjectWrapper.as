package com.llsvc.client.lms.view.project
{
import com.llsvc.domain.Project;

[Bindable]	
public class ProjectWrapper
{
	public var project:Project;
	
	public var isLoading:Boolean;
	public var isDirty:Boolean;
	public var isSelected:Boolean;
	
	private var memento:ProjectMemento;
	
	public function get name():String 
	{
		if (project != null)
			return project.name
		else 
			return "";
	}
	
	public function set name(value:String):void 
	{
		if (project != null)
			project.name = value;
	}
	
	public function ProjectWrapper(proj:Project)
	{
		project = proj;
		
		isDirty = false;
		isLoading = false;
	}
	
	public function setMemento():void 
	{
		if (memento == null)
			memento = new ProjectMemento();
		
		memento.id = project.id;
		memento.name = project.name;
		memento.isActive = project.isActive;
		memento.client = project.client;
	}

	public function getMemento():void 
	{
		if (memento == null)
			return;
		
		project.id = memento.id;
		project.name = memento.name;
		project.isActive = memento.isActive;
		project.client = memento.client;
	}

}
}