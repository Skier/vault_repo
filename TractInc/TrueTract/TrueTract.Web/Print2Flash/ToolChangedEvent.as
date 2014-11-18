package Print2Flash
{
	import flash.events.Event;

	public class ToolChangedEvent extends Event
	{
		private var _tool:String;
		public function get tool():String {
			return _tool;
		}
		
		public function ToolChangedEvent(tool:String)
		{
			super("onToolChanged");
			_tool=tool;
		}
		
	    override public function clone():Event {
	    	return new ToolChangedEvent(tool);
	    }		
	}
}