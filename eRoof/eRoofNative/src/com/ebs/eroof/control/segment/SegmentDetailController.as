package com.ebs.eroof.control.segment
{
	import flash.events.Event;
	
	import mx.core.UIComponent;
	
	public class SegmentDetailController
	{
		public static const CREATE_NEW_SEGMENT:String = "createNewSegment";
		
		private var model:SegmentDetailModel = SegmentDetailModel.getInstance();
		private var view:UIComponent;
		
		public function SegmentDetailController(view:UIComponent)
		{
			this.view = view;
			this.view.addEventListener(CREATE_NEW_SEGMENT, createNewSegmentHandler);
		}
		
		private function createNewSegmentHandler(ev:Event):void 
		{
			
		}

	}
}