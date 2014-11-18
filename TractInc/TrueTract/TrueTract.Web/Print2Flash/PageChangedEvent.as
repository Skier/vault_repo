package Print2Flash
{
	import flash.events.Event;

	public class PageChangedEvent extends Event
	{
		private var _page:Number;
		public function get page():Number {
			return _page;
		}
		
		public function PageChangedEvent(page:Number)
		{
			super("onPageChanged");
			_page=page;
		}
		
	    override public function clone():Event {
	    	return new 	PageChangedEvent(page);
	    }
	}
}