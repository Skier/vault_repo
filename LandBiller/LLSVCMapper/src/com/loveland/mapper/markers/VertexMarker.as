package com.loveland.mapper.markers
{
	import com.afcomponents.umap.styles.MarkerStyle;
	import com.loveland.mapper.UI.MarkerToolBox;
	import com.loveland.mapper.UI.events.VertexEvent;
	
	import flash.display.Bitmap;
	import flash.events.MouseEvent;
	import flash.filters.GlowFilter;

	public class VertexMarker extends CustomMarker
	{
        [Embed(source="/assets/icons16/cross.png")]
        [Bindable]
        private var iconDelete:Class;
		
		public function VertexMarker(param:Object=null, style:MarkerStyle=null, icon:Object=null, tooltipText:String=null)
		{
			super(param, style, icon, tooltipText);
		}
		
		private var imgDelete:Bitmap;
		private function drawToolBox():void 
		{
//			PopUpManager.addPopUp(,this.core as DisplayObject
	        var image:Class = Class(iconDelete);
	        imgDelete = new image();
	        
	        addEventListener(MouseEvent.MOUSE_MOVE, onMouseMove);
	        addEventListener(MouseEvent.CLICK, onClick);
	        
	        this.addChild(imgDelete);
	        
	        imgDelete.y = -16;
	        imgDelete.x = 8;
	        
	        this.buttonMode = true;
		}
		
		private function removeToolBox():void 
		{
			if (contains(imgDelete))
				this.removeChild(imgDelete);
			imgDelete.bitmapData.dispose();
		}
		
		private var toolBox:MarkerToolBox = new MarkerToolBox();;
		private function showToolBox():void 
		{
			toolBox.vertex = this;
			this.addChild(toolBox);
		}
		
		private function hideToolBox():void 
		{
			this.removeChild(toolBox);
		}
		
		private function onMouseMove(event:MouseEvent):void 
		{
			trace("localX:" + event.localX.toString() + ", localY:" + event.localY.toString());
			if (event.localX > 8 && event.localY < 0) 
			{
				trace("rollOver delete button");
				if (imgDelete.filters.length == 0)
					imgDelete.filters.push(new GlowFilter());
			} else 
			{
				trace("rollOut delete button");
				if (imgDelete.filters.length > 0)
					imgDelete.filters.pop();
			}
		}
		
		private function onClick(event:MouseEvent):void 
		{
			if (event.localX > 8 && event.localY < 0) 
			{
				trace("click delete button");
				dispatchEvent(new VertexEvent(VertexEvent.DELETE, this));
			}
		}
		
        override protected function onMouseOver(ev:MouseEvent):void
        {
        	super.onMouseOver(ev);
        	drawToolBox();
        }    

        override protected function onMouseOut(ev:MouseEvent):void
        {
        	super.onMouseOut(ev);
        	removeToolBox();
        }    
	}
}