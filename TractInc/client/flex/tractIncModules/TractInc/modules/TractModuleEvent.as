package TractInc.modules
{
	import flash.events.Event;
	import TractInc.Domain.Module;
	
	public class TractModuleEvent extends Event
	{
		public var module:Module;

	    public static const TRACT_MODULE_EXCEPTION:String = "tractModuleException";
		
	    public function TractModuleEvent(type:String, module:Module,
	    	bubbles:Boolean=true, cancelable:Boolean=false)
	    {
	    	this.module = module;
	        super(type, bubbles, cancelable);
	    }
	 
		override public function clone():Event 
		{
	        return new TractModuleEvent(type, module, bubbles, cancelable);
	    }
	   
	}

}