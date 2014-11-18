package AerSysCo.Events
{
	import flash.events.Event;
	import AerSysCo.Server.Category;

	public class CategoryEvent extends Event
	{
	    public static const CATEGORY_OR_MODEL_SELECTED:String = "categoryOrModelSelected";
	
	    public var initiator:Object;
	
	    public function CategoryEvent(type:String, initiator:Object, 
	        bubbles:Boolean=false, cancelable:Boolean=false)
	    {
	        super(type, bubbles, cancelable);
	
	        this.initiator = initiator;
	    }
	    
	    override public function clone():Event
	    {
	        return new CategoryEvent(type, initiator, bubbles, cancelable);
	    }
	    
	}
}