package com.ebs.eroof.mapping.events
{
	import com.afcomponents.umap.overlays.Marker;
	
	import flash.events.Event;

	public class MarkerEvent extends Event
	{
	    public static const DELETE:String = "deleteVertex";
	    public static const CHANGE:String = "changeVertex";

        public var marker:Marker;

		public function MarkerEvent(type:String, marker:Marker, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.marker = marker;
		}
		
	    override public function clone():Event
	    {
	        return new MarkerEvent(type, marker, bubbles, cancelable);
	    }
	}
}