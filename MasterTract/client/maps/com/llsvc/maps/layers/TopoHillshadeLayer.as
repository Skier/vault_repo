package com.llsvc.maps.layers
{
	public class TopoHillshadeLayer extends BaseLayer
	{
		public function TopoHillshadeLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "Topo";
			layerName = "Hillshade";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:hillshade1";
		} 
	}
}