package com.ebs.eroof.mapping.mapClasses
{
	import com.afcomponents.umap.styles.MarkerStyle;

	public class FacilityMarker extends CustomMarker
	{
        [Embed(source="/assets/icons16/home.png")]
        [Bindable]
        private var iconMiddle:Class;
                
		public function FacilityMarker(params:Object=null, style:MarkerStyle=null, icon:Class=null)
		{
			if (!icon)
				icon = iconMiddle;

			super(params, style, icon);
		}
		
	}
}