package truetract.events
{
import flash.events.Event;
import tractIncClientApp.project.ProjectFilter;

public class ProjectFilterEvent extends Event
{
    public static const APPLY_PROJECT_FILTER:String = "applyProjectFilter";

    public var projectFilter:ProjectFilter;

    public function ProjectFilterEvent(type:String, projectFilter:ProjectFilter, 
        bubbles:Boolean=false, cancelable:Boolean=false)
    {
        super(type, bubbles, cancelable);

        this.projectFilter = projectFilter;
    }
    
    override public function clone():Event
    {
        return new ProjectFilterEvent(type, projectFilter, bubbles, cancelable);
    }
    
}
}