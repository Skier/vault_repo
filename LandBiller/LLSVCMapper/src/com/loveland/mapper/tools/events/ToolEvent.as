package com.loveland.mapper.tools.events
{
	import flash.events.Event;

	public class ToolEvent extends Event
	{
	    public static const TOOL_ACTIVATE:String = "toolActivate";
	    public static const TOOL_DEACTIVATE:String = "toolDeactivate";
	    public static const TOOL_CLOSE_POLYGON:String = "toolClosePolygon";
	
		public var tool:Object;
	
	    public function ToolEvent(type:String, tool:Object, 
	       bubbles:Boolean=true, cancelable:Boolean=false)
	    {
	    	this.tool = tool;
	        super(type);
	    }
	            
	    override public function clone():Event
	    {
	        return new ToolEvent(type, tool, bubbles, cancelable);
	    }
	
	}
}