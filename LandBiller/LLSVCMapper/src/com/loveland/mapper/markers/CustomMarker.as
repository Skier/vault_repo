package com.loveland.mapper.markers
{
	import com.afcomponents.umap.overlays.Marker;
	import com.afcomponents.umap.styles.MarkerStyle;
	import com.afcomponents.umap.types.Align;
	
	import flash.display.Bitmap;
	import flash.events.MouseEvent;
	import flash.geom.Point;

	public class CustomMarker extends Marker
	{
		public function CustomMarker(param:Object=null, style:MarkerStyle=null, icon:Object=null, tooltipText:String=null)
		{
            if (icon!=null)
            {
                if (icon is Class)
                {
                	if (style == null)
                		style = new MarkerStyle();
                    style.strokeThickness = 0;
                    style.strokeAlpha = 0;
                    style.fillAlpha = 0;
                    style.icon = icon;
					style.iconStyle.align = Align.fromString("middle-center");
                }
                else if (icon is String)
                {
                    style.icon = icon;
                    trace(style.icon);
                }
            }
            
            super(param, style);

            this.addEventListener(MouseEvent.ROLL_OVER, onMouseOver);
            this.addEventListener(MouseEvent.ROLL_OUT, onMouseOut);
		}
		
        protected function onMouseOver(ev:MouseEvent):void
        {
        	trace("mouse over marker !");
        }    

        protected function onMouseOut(ev:MouseEvent):void
        {
        	trace("mouse out from marker !");
        }    
	}
}