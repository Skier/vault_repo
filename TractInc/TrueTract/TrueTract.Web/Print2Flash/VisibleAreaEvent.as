package Print2Flash
{
	import flash.events.Event;

	public class VisibleAreaEvent extends Event
	{
		private var _area:Object;
		public function get area():Object {
			return _area;
		}		
		
		public function VisibleAreaEvent(area:Object)
		{
			super("onVisibleAreaChanged");
			_area=area;
		}

	    override public function clone():Event {
	    	return new VisibleAreaEvent(area);
	    }		
	}
}