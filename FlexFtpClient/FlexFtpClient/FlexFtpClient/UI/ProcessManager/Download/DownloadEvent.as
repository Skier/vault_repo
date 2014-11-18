/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ProcessManager.Download
{
	import flash.events.Event;
	
	public class DownloadEvent extends Event
	{

	    public static const ERROR_LOAD_PROCESS:String = "errorLoadProcess";
	    public static const COMPLETE_LOAD_PROCESS:String = "completeLoadProcess";
		
		public var Item:DownloadItemInfo;
		
	    public function DownloadEvent(type:String, item:DownloadItemInfo, 
	    	bubbles:Boolean=true, cancelable:Boolean=false)
	    {
	    	this.Item = item;
	        super(type, bubbles, cancelable);
	    }
	 
		override public function clone():Event 
		{
	        return new DownloadEvent(type, Item, bubbles, cancelable);
	    }
	   
	}

}