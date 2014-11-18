package com.ebs.eroof.mapping.mapClasses
{
	import com.afcomponents.umap.overlays.Marker;
	import com.afcomponents.umap.styles.MarkerStyle;
	import com.ebs.eroof.mapping.events.MarkerEvent;
	
	import flash.display.Bitmap;
	import flash.events.MouseEvent;

	public class VertexMarker extends CustomMarker
	{
        [Embed(source="/assets/icons16/radio_button.png")]
        [Bindable]
        private var iconVertex:Class;
                
        [Embed(source="/assets/icons16/cross.png")]
        [Bindable]
        private var iconDelete:Class;
        
        private var imgDelete:Bitmap;

		public function VertexMarker(params:Object=null, style:MarkerStyle=null, icon:Class=null)
		{
			if (!icon)
				icon = iconVertex;

			super(params, style, icon);
			autoInfo = false;
			smartPosition = false;

			addEventListener(MouseEvent.ROLL_OVER, onMouseOver);
			addEventListener(MouseEvent.ROLL_OUT, onMouseOut);
		}
		
        private function drawToolBox():void 
        {
	        var image:Class = Class(iconDelete);
	        imgDelete = new image();
	        imgDelete.addEventListener(MouseEvent.CLICK, btnDeleteClickHandler);
	        
	        //addEventListener(MouseEvent.MOUSE_MOVE, onMouseMove);
	        addEventListener(MouseEvent.CLICK, onClick);
	        
	        addChild(imgDelete);
	        
	        imgDelete.y = -12;
	        imgDelete.x = 4;
        }
        
        private function removeToolBox():void 
        {
            if (contains(imgDelete))
            	removeChild(imgDelete);
            imgDelete.bitmapData.dispose();
        }

        private function onClick(event:MouseEvent):void 
        {
            if (event.localX > 8 && event.localY < 0) 
                dispatchEvent(new MarkerEvent(MarkerEvent.DELETE, this as Marker));
        }

        private function btnDeleteClickHandler(event:MouseEvent):void 
        {
        	if (event.currentTarget == imgDelete)
            	dispatchEvent(new MarkerEvent(MarkerEvent.DELETE, this as Marker));
        }

        protected function onMouseOver(ev:MouseEvent):void
        {
            drawToolBox();
        }    

        protected function onMouseOut(ev:MouseEvent):void
        {
            removeToolBox();
        }    
	}
}