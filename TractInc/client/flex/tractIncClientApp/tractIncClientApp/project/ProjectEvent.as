package tractIncClientApp.project
{
import flash.events.Event;
import truetract.domain.Project;

public class ProjectEvent extends Event
{
    public static const OPEN_PROJECT:String = "openProject";

    public var project:Project;

    public function ProjectEvent(type:String, project:Project, 
        bubbles:Boolean=false, cancelable:Boolean=false)
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