package Print2Flash
{
	import flash.events.Event;

	public class ZoomChangedEvent extends Event
	{
		private var _zoom:Number;
		public function get zoom():Number {
			return _zoom;
		}
		
		public function ZoomChangedEvent(zoom:Number)
		{
			super("onZoomChanged");
			_zoom=zoom;
		}
		
	    override public function clone():Event {
	    	return new ZoomChangedEvent(zoom);
	    }
	}
}