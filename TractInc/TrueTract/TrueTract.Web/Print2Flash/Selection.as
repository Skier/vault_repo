package Print2Flash
{
	import flash.events.Event;

	public class Selection extends Event
	{
		private var _sel:Object;
		public function get selection():Object {
			return _sel;
		}
		
		public function Selection(sel:Object)
		{
			super("onSelection");
			_sel=sel;
		}

	    override public function clone():Event {
	    	return new Selection(selection);
	    }				
	}
}