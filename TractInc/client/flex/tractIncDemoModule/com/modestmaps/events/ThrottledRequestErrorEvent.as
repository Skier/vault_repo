/*
 * $Id: ThrottledRequestErrorEvent.as 292 2007-06-01 00:50:52Z migurski $
 */

package com.modestmaps.events
{
	import flash.net.URLRequest;
	import flash.display.Sprite;
	import com.modestmaps.core.Coordinate;

	public class ThrottledRequestErrorEvent extends ThrottledRequestEvent
	{
		public function ThrottledRequestErrorEvent(type:String, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
		}
	}
}