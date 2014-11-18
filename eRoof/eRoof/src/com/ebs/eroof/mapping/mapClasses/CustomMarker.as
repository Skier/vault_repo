package com.ebs.eroof.mapping.mapClasses
{
	import com.afcomponents.umap.overlays.Marker;
	import com.afcomponents.umap.styles.MarkerStyle;
	import com.afcomponents.umap.types.Align;

	public class CustomMarker extends Marker
	{
		public function CustomMarker(params:Object=null, style:MarkerStyle=null, icon:Class=null)
		{
			if (style == null)
            	style = new MarkerStyle();
            
            style.strokeThickness = 0;
            style.strokeAlpha = 0;
            style.fillAlpha = 0;
            
            if (icon)
            {
	            style.icon = icon;
                style.iconStyle.align = Align.fromString("middle-center");
                style.shadow = false;
            }
            
            super(params, style);
		}
	}
}