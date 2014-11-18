package com.llsvc.maps.layers
{
	public class DynLayer extends BaseLayer
	{
		private var geoLayerName:String;
		
		public function DynLayer(geoServUrl:String, name:String, description:String)
		{
			super(geoServUrl);
			layerGroup = "Custom";
			layerName = (null == description ? name : description);
			geoLayerName = name;

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return this.geoLayerName;
		} 
	}
}