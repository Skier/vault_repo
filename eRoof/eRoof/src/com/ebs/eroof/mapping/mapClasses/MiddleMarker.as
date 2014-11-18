package com.ebs.eroof.mapping.mapClasses
{
	import com.afcomponents.umap.styles.MarkerStyle;

	public class MiddleMarker extends CustomMarker
	{
        [Embed(source="/assets/icons16/radio_button_uncheck.png")]
        [Bindable]
        private var iconMiddle:Class;
                
		public function MiddleMarker(params:Object=null, style:MarkerStyle=null, icon:Class=null)
		{
			if (!icon)
				icon = iconMiddle;

			super(params, style, icon);

			autoInfo = false;
			smartPosition = false;
		}
		
	}
}