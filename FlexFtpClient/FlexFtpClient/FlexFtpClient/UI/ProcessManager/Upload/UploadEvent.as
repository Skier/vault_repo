/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ProcessManager.Upload
{
	import flash.events.Event;
	
	public class UploadEvent extends Event
	{
		public var Item:UploadItemInfo;

	    public static const ERROR_LOAD_PROCESS:String = "errorLoadProcess";
	    public static const COMPLETE_LOAD_PROCESS:String = "completeLoadProcess";
		
	    public function UploadEvent(type:String, item:UploadItemInfo,
	    	bubbles:Boolean=true, cancelable:Boolean=false)
	    {
	    	this.Item = item;
	        super(type, bubbles, cancelable);
	    }
	 
		override public function clone():Event 
		{
	        return new UploadEvent(type, Item, bubbles, cancelable);
	    }
	   
	}

}